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
    private static double average(List<ushort> values)
    {
        double sum = 0.0;
        foreach(ushort val in values)
            sum += val;
        return sum / values.Count;
    }

    private static double variance(List<ushort> values)
    {
        double avg = average(values);
        double sum_squares = 0.0;
        foreach (ushort val in values)
            sum_squares += Math.Pow((val - avg), 2.0);
        return sum_squares / (values.Count - 1);
    }

    //Checks all intersections of the given board, and compares the variance of production across the board
    //Returns whether the variance is within certain bounds (low)
    public static bool acceptable_variance(Board game_board, double variance_limit)
    {
        //Find all intersection production values and add them to a list
        List<ushort> intersection_production_values = new List<ushort>();
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
            intersection_production_values.Add(intersection_production);
        }

        //Console.WriteLine("Variance limit: " + variance_limit);
        System.Diagnostics.Debug.WriteLine("Actual variance: " + variance(intersection_production_values));

        //Find the variance of the production values and compare them to the acceptable bounds
        return variance(intersection_production_values) < variance_limit;
    }
}