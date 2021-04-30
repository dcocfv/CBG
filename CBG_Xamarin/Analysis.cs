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
        //System.Diagnostics.Debug.WriteLine("Actual variance: " + variance(intersection_production_values));

        //Find the variance of the production values and compare them to the acceptable bounds
        return variance(intersection_production_values) < variance_limit;
    }

    // Checks the total resource production of the given board, comparing the total resource production of
    // each of the main 5 individual resources to make sure they are within the variance_limit. In reality, not
    // recommended as a setting for typical games, because unbalanced resources are a fun part of the game
    public static bool acceptable_distribution_tile(Board game_board, float brick, float ore, float sheep, float wheat, float wood)
    {
        ushort[] resource_production_values = new ushort[5];
        float[] resource_production_ratio = new float[5];

        float total_desired_production = Convert.ToSingle(brick + ore + sheep + wheat + wood);
        float[] desired_production_ratio = {
             brick/total_desired_production,
             ore/total_desired_production,
             sheep/total_desired_production,
             wheat/total_desired_production,
             wood/total_desired_production
        };

        System.Diagnostics.Debug.WriteLine("desired ratios: " + desired_production_ratio[0] + ", " +
            desired_production_ratio[1] + ", " + desired_production_ratio[2] + ", " +
            desired_production_ratio[3] + ", " + desired_production_ratio[4]);

        foreach(KeyValuePair<HexPosition, Hex> tile in game_board.tiles)
        {
            switch(tile.Value.type)
            {
                case Resource.brick:
                    resource_production_values[0] += production.production_from_number(tile.Value.number);
                    break;
                case Resource.ore:
                    resource_production_values[1] += production.production_from_number(tile.Value.number);
                    break;
                case Resource.sheep:
                    resource_production_values[2] += production.production_from_number(tile.Value.number);
                    break;
                case Resource.wheat:
                    resource_production_values[3] += production.production_from_number(tile.Value.number);
                    break;
                case Resource.wood:
                    resource_production_values[4] += production.production_from_number(tile.Value.number);
                    break;
            }
        }

        ushort total_resource_production = 0;
        for (int i = 0; i < 5; i++)
            total_resource_production += resource_production_values[i];

        float error = 0;
        for (int i = 0; i < 5; i++)
        {
            resource_production_ratio[i] = resource_production_values[i] / Convert.ToSingle(total_resource_production);
            error += Math.Abs(resource_production_ratio[i] - desired_production_ratio[i]);
        }

        if (error > 0.1)
            return false;

        System.Diagnostics.Debug.WriteLine("actual ratios: " + resource_production_ratio[0] + ", " +
            resource_production_ratio[1] + ", " + resource_production_ratio[2] + ", " +
            resource_production_ratio[3] + ", " + resource_production_ratio[4]);

        return true;
    }

    // Checks that no 6 or 8 is adjacent to each other on the game board. If it is, return false. Else return true
    public static bool no_6_8_adjacent(Board game_board)
    {
        foreach(KeyValuePair<HexPosition, Hex> tile in game_board.tiles)
        {
            if(tile.Value.number == 6 || tile.Value.number == 8)
            {
                List<HexPosition> neighbors = tile.Key.GetNeighbors();
                for(int i = 0; i < 6; i++)
                {
                    if(game_board.tiles.ContainsKey(neighbors[i]))
                    {
                        Hex hex;
                        game_board.tiles.TryGetValue(neighbors[i], out hex);
                        if (hex.number == 6 || hex.number == 8)
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}