using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class HexPosition
{
    public short x_pos = 0;
    public short y_pos = 0;
    public short z_pos = 0;
    public ushort direction = 0; //For harbors
    public bool fog;

    public HexPosition(){}

    public HexPosition(short x, short y, short z, ushort dir = 0, bool f = false)
    {
        x_pos = x;
        y_pos = y;
        z_pos = z;
        direction = dir;
        fog = f;
    }

    public HexPosition(short x, short y, ushort dir = 0, bool f = false)
    {
        x_pos = x;
        y_pos = y;
        z_pos = (short)(0 - x - y);
        direction = dir;
        fog = f;
    }

    public List<HexPosition> GetNeighbors()
    {
        List<HexPosition> n = new List<HexPosition>();
        n.Add(new HexPosition(x_pos,   (short)(y_pos + 1), (short)(z_pos - 1)));
        n.Add(new HexPosition((short)(x_pos + 1), y_pos, (short)(z_pos - 1)));
        n.Add(new HexPosition((short)(x_pos + 1), (short)(y_pos - 1), z_pos  ));
        n.Add(new HexPosition(x_pos, (short)(y_pos - 1), (short)(z_pos + 1)));
        n.Add(new HexPosition((short)(x_pos - 1), y_pos, (short)(z_pos + 1)));
        n.Add(new HexPosition((short)(x_pos - 1), (short)(y_pos + 1), z_pos  ));
        
        return n;
    }

    public override bool Equals(object obj)
    {
        var other = obj as HexPosition;
        return x_pos == other.x_pos && y_pos == other.y_pos && z_pos == other.z_pos;
    }

    public override int GetHashCode()
    {
        return x_pos.GetHashCode() + y_pos.GetHashCode() + z_pos.GetHashCode();
    }

    public static bool operator ==(HexPosition x, HexPosition y) { return x.Equals(y); }
    public static bool operator !=(HexPosition x, HexPosition y) { return !(x == y); }
}

public class VertexPosition
{
    //Unordered set of 3 neighboring hex positions
    public HashSet<HexPosition> neighbors = new HashSet<HexPosition>();

    public VertexPosition(){}

    public VertexPosition(HexPosition a, HexPosition b, HexPosition c)
    {
        neighbors.Add(a);
        neighbors.Add(b);
        neighbors.Add(c);
    }

    public override bool Equals(object obj)
    {
        var other = obj as VertexPosition;
        return neighbors.SetEquals(other.neighbors);
    }

    public override int GetHashCode()
    {
        int code = 0;
        foreach(HexPosition h in neighbors)
        {
            code += h.GetHashCode();
        }
        return code;
    }

    public static bool operator ==(VertexPosition x, VertexPosition y) { return x.Equals(y); }
    public static bool operator !=(VertexPosition x, VertexPosition y) { return !(x == y); }
}

public enum Resource
{
    brick,
    ore,
    sheep,
    wheat,
    wood,
    gold,
    desert,
    harbor_brick,
    harbor_ore,
    harbor_sheep,
    harbor_wheat,
    harbor_wood,
    harbor_any,
    sea
}

public class Hex
{
    public Resource type;
    public ushort number;

    public Hex(TileGenerationSet tileset)
    {
        Random rand = new Random();

        //Console.WriteLine("res count: " + tileset.resource_pool.Count);
        //Console.WriteLine("num count: " + tileset.number_pool.Count);

        type = tileset.resource_pool[rand.Next(tileset.resource_pool.Count)];
        tileset.resource_pool.RemoveAt(tileset.resource_pool.FindIndex(res => res == type));

        if(type == Resource.desert || type == Resource.sea || type == Resource.harbor_brick || type == Resource.harbor_ore || type == Resource.harbor_sheep || type == Resource.harbor_wheat || type == Resource.harbor_wood || type == Resource.harbor_any)
            number = 0;
        else
        {
            number = tileset.number_pool[rand.Next(tileset.number_pool.Count)];
            tileset.number_pool.RemoveAt(tileset.number_pool.FindIndex(num => num == number));
        }
    }
}

public class Vertex
{

}

public class TileGenerationSet
{
    public List<HexPosition> location_pool = new List<HexPosition>();
    public List<Resource> resource_pool = new List<Resource>();
    public List<ushort> number_pool = new List<ushort>();

    public TileGenerationSet(){}
}

public class Board
{
    public Dictionary<HexPosition, Hex> tiles = new Dictionary<HexPosition, Hex>();
    public Dictionary<VertexPosition, Vertex> intersections = new Dictionary<VertexPosition, Vertex>();

    //Map bounds constructor (creates an empty map of specified size)
    public Board(short x_lower, short x_upper, short y_lower, short y_upper, short z_lower, short z_upper)
    {
        TileGenerationSet defaultGenSet = new TileGenerationSet();

        //Add all tiles to the board
        for(short x = x_lower; x <= x_upper; x++)
        {
            for(short y = y_lower; y <= y_upper; y++)
            {
                for (short z = z_lower; z <= z_upper; z++)
                {
                    if(x + y + z == 0)
                    {
                        tiles.Add(new HexPosition(x, y, z), new Hex(defaultGenSet));
                    }
                }
            }
        }

        //Add all intersections
        foreach(KeyValuePair<HexPosition, Hex> tile in tiles)
        {
            //Check every intersection by inspecting the two mutual neighbors
            List<HexPosition> neighbors = tile.Key.GetNeighbors();
            for(int i = 0; i < 6; i++)
            {
                int j = i + 1 < 6 ? i + 1 : 0;
                //Check that both neighbors are in the board
                if(tiles.ContainsKey(neighbors[i]) && tiles.ContainsKey(neighbors[j]))
                {
                    intersections.TryAdd(new VertexPosition(tile.Key, neighbors[i], neighbors[j]), new Vertex());
                }
            }
        }
    }

    //Default constructor
    //Initializes a map with 7 tiles and 6 intersections
    public Board() : this(-1, 1, -1, 1, -1, 1){}

    //Map name constructor
    public Board(string map_name)
    {
        //Load the board configuration
        List<TileGenerationSet> current_board_config = Maps.load_map(map_name);

        //Add all tiles
        foreach(var tileset in current_board_config)
        {
            //Check that config is valid
            int count = 0;
            foreach(var r in tileset.resource_pool)
            {
                if(r == Resource.wheat || r == Resource.brick || r == Resource.ore || r == Resource.sheep || r == Resource.wood || r == Resource.gold)
                    count++;
            }
            if(!(tileset.location_pool.Count == tileset.resource_pool.Count && tileset.number_pool.Count == count))
                throw new Exception("Map config sizes do not match: " + tileset.location_pool.Count + ", " + tileset.resource_pool.Count + ", " + tileset.number_pool.Count);

            foreach(HexPosition location in tileset.location_pool)
            {
                tiles.Add(new HexPosition(location.x_pos, location.y_pos, location.z_pos, location.direction, location.fog), new Hex(tileset));
            }
        }

        //Add all intersections
        foreach(KeyValuePair<HexPosition, Hex> tile in tiles)
        {
            //Check every intersection by inspecting the two mutual neighbors
            List<HexPosition> neighbors = tile.Key.GetNeighbors();
            for(int i = 0; i < 6; i++)
            {
                int j = i + 1 < 6 ? i + 1 : 0;
                //Check that both neighbors are in the board
                if(tiles.ContainsKey(neighbors[i]) && tiles.ContainsKey(neighbors[j])
                //Check that the intersection has at least one producing tile
                && !(tile.Value.number == 0 && tiles[neighbors[i]].number == 0 && tiles[neighbors[j]].number == 0))
                {
                    intersections.TryAdd(new VertexPosition(tile.Key, neighbors[i], neighbors[j]), new Vertex());
                }
            }
        }
    }
}