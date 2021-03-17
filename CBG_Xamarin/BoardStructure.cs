using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class HexPosition
{
    public short x_pos;
    public short y_pos;
    public short z_pos;

    public HexPosition(short x, short y, short z)
    {
        x_pos = x;
        y_pos = y;
        z_pos = z;
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
    HashSet<HexPosition> neighbors;

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
    harbor,
    sea
}

public class Hex
{
    public Resource type = Resource.brick;
    public ushort number = 8;
    public ushort direction = 1; //For harbors
}

public class Vertex
{

}

public class TileGenerationSet
{
    List<HexPosition> location_pool;
    List<Resource> resource_pool;
    List<ushort> number_pool;

    //TODO: probably add functions to this class specifying how to generate the tiles within this group
}

public class BoardGenerationConfig
{
    public string name;
    public short x_lower, x_upper, y_lower, y_upper, z_lower, z_upper; //Map bounds
    public List<TileGenerationSet> tile_groups; //Tile randomization groups

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
        return (BoardGenerationConfig)serializer.Deserialize(stream);
        stream.Dispose();
    }
}

public class Board
{
    public Dictionary<HexPosition, Hex> tiles = new Dictionary<HexPosition, Hex>();
    public Dictionary<VertexPosition, Vertex> intersections = new Dictionary<VertexPosition, Vertex>();

    //Map bounds constructor (creates an empty map of specified size)
    public Board(short x_lower, short x_upper, short y_lower, short y_upper, short z_lower, short z_upper)
    {
        //Add all tiles to the board
        for(short x = x_lower; x <= x_upper; x++)
        {
            for(short y = y_lower; y <= y_upper; y++)
            {
                for (short z = z_lower; z <= z_upper; z++)
                {
                    if(x + y + z == 0)
                    {
                        tiles.Add(new HexPosition(x, y, z), new Hex());
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
}