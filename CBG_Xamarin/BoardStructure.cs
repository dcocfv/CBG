public class HexPosition
{
    short x_pos;
    short y_pos;
    short z_pos;

    public HexPosition(short x, short y, short z)
    {
        x_pos = x;
        y_pos = y;
        z_pos = z;
    }

    public System.Collections.Generic.List<HexPosition> GetNeighbors()
    {
        System.Collections.Generic.List<HexPosition> n = new System.Collections.Generic.List<HexPosition>();
        n.Add(new HexPosition(x_pos,   y_pos++, z_pos--));
        n.Add(new HexPosition(x_pos++, y_pos,   z_pos--));
        n.Add(new HexPosition(x_pos++, y_pos--, z_pos  ));
        n.Add(new HexPosition(x_pos,   y_pos--, z_pos++));
        n.Add(new HexPosition(x_pos--, y_pos,   z_pos++));
        n.Add(new HexPosition(x_pos--, y_pos++, z_pos  ));
        
        return n;
    }
}

public class VertexPosition
{
    //Unordered set of 3 neighboring hex positions
    System.Collections.Generic.HashSet<HexPosition> neighbors;

    public VertexPosition(HexPosition a, HexPosition b, HexPosition c)
    {
        neighbors.Add(a);
        neighbors.Add(b);
        neighbors.Add(c);
    }
}

enum Resource
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
    Resource type;
    ushort number;
    ushort direction; //For harbors
}

public class Vertex
{

}

public class TileGenerationSet
{
    System.Collections.Generic.List<HexPosition> location_pool;
    System.Collections.Generic.List<Resource> resource_pool;
    System.Collections.Generic.List<ushort> number_pool;
}

public class BoardGenerationConfig
{
    short x_lower, x_upper, y_lower, y_upper, z_lower, z_upper; //Map bounds
    System.Collections.Generic.List<TileGenerationSet> tile_groups; //Tile randomization groups
}

public class Board
{
    System.Collections.Generic.Dictionary<HexPosition, Hex> tiles;
    System.Collections.Generic.Dictionary<VertexPosition, Vertex> intersections;

    //Map bounds constructor
    public Board(short x_lower, short x_upper, short y_lower, short y_upper, short z_lower, short z_upper)
    {
        //Add all tiles to the board
        for(short x = x_lower; x < x_upper; x++)
        {
            for(short y = y_lower; y < y_upper; y++)
            {
                for (short z = z_lower; z < z_upper; z++)
                {
                    if(x + y + z == 0)
                        tiles.Add(new HexPosition(x, y, z), new Hex());
                }
            }
        }

        //Add all intersections
        foreach(System.Collections.Generic.KeyValuePair<HexPosition, Hex> tile in tiles)
        {
            //Check every intersection by inspecting the two mutual neighbors
            System.Collections.Generic.List<HexPosition> neighbors = tile.Key.GetNeighbors();
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