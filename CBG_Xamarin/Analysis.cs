using System;
using System.Collections.Generic;

public static class production
{
    //Translates a production number (2..12) to a measure of production (1..5)
    public static ushort production_from_number(ushort n)
    {
        return (ushort)((Math.Abs(n - 7) * -1) + 6);
    }
}

public static class analyzer
{
    //Checks all intersections of the given board, and compares the variance of production across the board
    //Returns whether the variance is within certain bounds (low)
    public static bool acceptable_variance(Board game_board)
    {
        float sum = 0;
        foreach(KeyValuePair<VertexPosition, Vertex> intersection in game_board.intersections)
        {
            ushort intersection_production = 0;
            foreach(HexPosition neighbor in intersection.Key.neighbors)
            {
                if(game_board.tiles[neighbor].number != 0)
                    intersection_production += production.production_from_number(game_board.tiles[neighbor].number);
                else if(game_board.tiles[neighbor].type == Resource.harbor_brick || game_board.tiles[neighbor].type == Resource.harbor_ore || game_board.tiles[neighbor].type == Resource.harbor_sheep || game_board.tiles[neighbor].type == Resource.harbor_wheat || game_board.tiles[neighbor].type == Resource.harbor_wood || game_board.tiles[neighbor].type == Resource.harbor_any)
                    intersection_production += 3; //Placeholder value for harbors
            }
            sum += intersection_production;
        }


        Console.WriteLine("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB");

        float avg = sum / game_board.intersections.Count;
        Console.WriteLine("Sum:  " + sum);
        Console.WriteLine("# of intersections:  " + game_board.intersections.Count);
        Console.WriteLine("Average production:  " + avg);

        return true;
    }
}