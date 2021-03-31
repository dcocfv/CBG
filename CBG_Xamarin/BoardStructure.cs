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

    public HexPosition(){}

    public HexPosition(short x, short y, short z)
    {
        x_pos = x;
        y_pos = y;
        z_pos = z;
    }

    public HexPosition(short x, short y, short z, ushort dir)
    {
        x_pos = x;
        y_pos = y;
        z_pos = z;
        direction = dir;
    }

    public List<HexPosition> GetNeighbors()
    {
        List<HexPosition> n = new List<HexPosition>();
        n.Add(new HexPosition(x_pos,   y_pos++, z_pos--));
        n.Add(new HexPosition(x_pos++, y_pos,   z_pos--));
        n.Add(new HexPosition(x_pos++, y_pos--, z_pos  ));
        n.Add(new HexPosition(x_pos,   y_pos--, z_pos++));
        n.Add(new HexPosition(x_pos--, y_pos,   z_pos++));
        n.Add(new HexPosition(x_pos--, y_pos++, z_pos  ));
        
        return n;
    }

    public override bool Equals(object obj)
    {
        var other = obj as HexPosition;
        if(other == null || x_pos != other.x_pos || y_pos != other.y_pos || z_pos != other.z_pos)
            return false;
        else
            return true;
    }

    public static bool operator ==(HexPosition x, HexPosition y) { return x.Equals(y); }
    public static bool operator !=(HexPosition x, HexPosition y) { return !(x == y); }
}

public class VertexPosition
{
    //Unordered set of 3 neighboring hex positions
    HashSet<HexPosition> neighbors = new HashSet<HexPosition>();

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
        if(other == null || neighbors != other.neighbors)
            return false;
        else
            return true;
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

        Console.WriteLine("res count: " + tileset.resource_pool.Count);
        Console.WriteLine("num count: " + tileset.number_pool.Count);

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

    //TODO: probably add functions to this class specifying how to generate the tiles within this group
}

public class BoardGenerationConfig
{
    public string name;
    public short x_lower, x_upper, y_lower, y_upper, z_lower, z_upper; //Map bounds
    public List<TileGenerationSet> tile_groups = new List<TileGenerationSet>(); //Tile randomization groups

    public void save_xml()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(BoardGenerationConfig));
        FileStream stream = File.Create(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + name + ".xml");
        serializer.Serialize(stream, this);
        stream.Dispose();
    }

    public static BoardGenerationConfig load_xml(string map_name)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(BoardGenerationConfig));
        FileStream stream = File.OpenRead(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + map_name + ".xml");
        BoardGenerationConfig saved_board = (BoardGenerationConfig)serializer.Deserialize(stream);
        stream.Dispose();
        return saved_board;
    }

    // default constructor for TileGeneration, used for default game of Catan, with 19 tiles, hex radius of 2,
    // standard distribution of hex tiles and chit numbers
    public BoardGenerationConfig()
    {
        name = "base_3-4";

        TileGenerationSet main_island = new TileGenerationSet();
        for (short x = -2; x <= 2; x++)
        {
            for (short y = -2; y <= 2; y++)
            {
                for (short z = -2; z <= 2; z++)
                {
                    if (x + y + z == 0)
                    {
                        main_island.location_pool.Add(new HexPosition(x, y, z));
                    }
                }
            }
        }

        main_island.resource_pool = new List<Resource>()
        {
            Resource.ore, Resource.ore, Resource.ore, Resource.brick, Resource.brick, Resource.brick,
            Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.wood, Resource.wood,
            Resource.wood, Resource.wood, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep,
            Resource.desert
        };

        main_island.number_pool = new List<ushort>()
        {
            2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12
        };



        TileGenerationSet harbors = new TileGenerationSet();
        harbors.location_pool.Add(new HexPosition(-3, 3, 0, 2));
        harbors.location_pool.Add(new HexPosition(-1, 3, -2, 3));
        harbors.location_pool.Add(new HexPosition(1, 2, -3, 3));
        harbors.location_pool.Add(new HexPosition(3, 0, -3, 4));
        harbors.location_pool.Add(new HexPosition(3, -2, -1, 5));
        harbors.location_pool.Add(new HexPosition(2, -3, 1, 5));
        harbors.location_pool.Add(new HexPosition(0, -3, 3, 0));
        harbors.location_pool.Add(new HexPosition(-2, -1, 3, 1));
        harbors.location_pool.Add(new HexPosition(-3, 1, 2, 1));

        harbors.resource_pool = new List<Resource>()
        {
            Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
        };


        
        TileGenerationSet sea = new TileGenerationSet();
        sea.location_pool.Add(new HexPosition(-2, 3, -1));
        sea.location_pool.Add(new HexPosition(0, 3, -3));
        sea.location_pool.Add(new HexPosition(2, 1, -3));
        sea.location_pool.Add(new HexPosition(3, -1, -2));
        sea.location_pool.Add(new HexPosition(3, -3, 0));
        sea.location_pool.Add(new HexPosition(1, -3, 2));
        sea.location_pool.Add(new HexPosition(-1, -2, 3));
        sea.location_pool.Add(new HexPosition(-3, 0, 3));
        sea.location_pool.Add(new HexPosition(-3, 2, 1));

        sea.resource_pool = new List<Resource>()
        {
            Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
        };



        tile_groups.Add(main_island);
        tile_groups.Add(harbors);
        tile_groups.Add(sea);
    }
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

    //Map type constructor
    //Given the name of the map, fetch the saved map config and load that map
    public Board(string map_name)
    {
        //Load the board config
        BoardGenerationConfig current_board_config = BoardGenerationConfig.load_xml(map_name);

        foreach(var tileset in current_board_config.tile_groups)
        {
            foreach(var location in tileset.location_pool)
            {
                tiles.Add(new HexPosition(location.x_pos, location.y_pos, location.z_pos, location.direction), new Hex(tileset));
            }
        }

        //Add all intersections
        foreach (KeyValuePair<HexPosition, Hex> tile in tiles)
        {
            //Check every intersection by inspecting the two mutual neighbors
            List<HexPosition> neighbors = tile.Key.GetNeighbors();
            for (int i = 0; i < 6; i++)
            {
                int j = i + 1 < 6 ? i + 1 : 0;
                //Check that both neighbors are in the board
                if (tiles.ContainsKey(neighbors[i]) && tiles.ContainsKey(neighbors[j]))
                {
                    intersections.TryAdd(new VertexPosition(tile.Key, neighbors[i], neighbors[j]), new Vertex());
                }
            }
        }
    }
}