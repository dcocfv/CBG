using System.Collections.Generic;

public static class Maps
{
    public static List<TileGenerationSet> load_map(string map_name)
    {
        List<TileGenerationSet> map = new List<TileGenerationSet>();

        switch(map_name)
        {
            case "base_3":
            case "base_4":
            {
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
                harbors.location_pool.Add(new HexPosition(-3, 3, 0, 5));
                harbors.location_pool.Add(new HexPosition(-1, 3, -2, 0));
                harbors.location_pool.Add(new HexPosition(1, 2, -3, 0));
                harbors.location_pool.Add(new HexPosition(3, 0, -3, 1));
                harbors.location_pool.Add(new HexPosition(3, -2, -1, 2));
                harbors.location_pool.Add(new HexPosition(2, -3, 1, 2));
                harbors.location_pool.Add(new HexPosition(0, -3, 3, 3));
                harbors.location_pool.Add(new HexPosition(-2, -1, 3, 4));
                harbors.location_pool.Add(new HexPosition(-3, 1, 2, 4));

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

                map.Add(main_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            case "base_5":
            case "base_6":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                for (short x = -2; x <= 3; x++)
                {
                    for (short y = -2; y <= 3; y++)
                    {
                        for (short z = -4; z <= 2; z++)
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
                    Resource.desert,
                    Resource.desert, Resource.wheat, Resource.wheat, Resource.wood, Resource.wood, Resource.sheep, Resource.sheep, Resource.ore, Resource.ore, Resource. brick, Resource.brick
                };

                main_island.number_pool = new List<ushort>()
                {
                    2, 5, 4, 6, 3, 9, 8, 11, 11, 10, 6, 3, 8, 4, 8, 10, 11, 12, 10, 5, 4, 9, 5, 9, 12, 3, 12, 6
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(-1, -2, 3, 3));
                harbors.location_pool.Add(new HexPosition(-3, 0, 3, 4));
                harbors.location_pool.Add(new HexPosition(-3, 2, 1, 5));
                harbors.location_pool.Add(new HexPosition(-3, 3, 0, 5));
                harbors.location_pool.Add(new HexPosition(-2, 4, -2, 5));
                harbors.location_pool.Add(new HexPosition(1, 4, -5, 0));
                harbors.location_pool.Add(new HexPosition(3, 2, -5, 1));
                harbors.location_pool.Add(new HexPosition(4, 0, -4, 2));
                harbors.location_pool.Add(new HexPosition(4, -3, -1, 2));
                harbors.location_pool.Add(new HexPosition(2, -3, 1, 3));
                harbors.location_pool.Add(new HexPosition(1, -3, 2, 2));

                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any,
                    Resource.harbor_sheep, Resource.harbor_any
                };

                TileGenerationSet sea = new TileGenerationSet();
                sea.location_pool.Add(new HexPosition(-2, -1, 3));
                sea.location_pool.Add(new HexPosition(-3, 1, 2));
                sea.location_pool.Add(new HexPosition(-3, 4, -1));
                sea.location_pool.Add(new HexPosition(-1, 4, -3));
                sea.location_pool.Add(new HexPosition(0, 4, -4));
                sea.location_pool.Add(new HexPosition(2, 3, -5));
                sea.location_pool.Add(new HexPosition(4, 1, -5));
                sea.location_pool.Add(new HexPosition(4, -1, -3));
                sea.location_pool.Add(new HexPosition(4, -2, -2));
                sea.location_pool.Add(new HexPosition(3, -3, 0));
                sea.location_pool.Add(new HexPosition(0, -3, 3));

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            case "seafarers_3_1":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                for (short x = -3; x <= 1; x++)
                {
                    for (short y = 0; y <= 3; y++)
                    {
                        for (short z = -2; z <= 1; z++)
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
                    Resource.wheat, Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood
                };

                main_island.number_pool = new List<ushort>()
                {
                    2, 3, 4, 5, 5, 6, 6, 8, 8, 9, 10, 10, 11, 11
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(-3, 1, 2, 3));
                harbors.location_pool.Add(new HexPosition(-4, 3, 1, 5));
                harbors.location_pool.Add(new HexPosition(-3, 4, -1, 0));
                harbors.location_pool.Add(new HexPosition(-2, 4, -2, 5));
                harbors.location_pool.Add(new HexPosition(0, 3, -3, 1));
                harbors.location_pool.Add(new HexPosition(1, 2, -3, 0));
                harbors.location_pool.Add(new HexPosition(1, -1, 0, 3));
                harbors.location_pool.Add(new HexPosition(0, -1, 1, 2));

                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
                };
                
                TileGenerationSet other_island = new TileGenerationSet();
                other_island.location_pool.Add(new HexPosition(-3, 0, 3));
                other_island.location_pool.Add(new HexPosition(-2, -1, 3));
                other_island.location_pool.Add(new HexPosition(-1, -2, 3));
                other_island.location_pool.Add(new HexPosition(0, -2, 2));
                other_island.location_pool.Add(new HexPosition(1, -3, 2));
                other_island.location_pool.Add(new HexPosition(1, -2, 1));
                other_island.location_pool.Add(new HexPosition(2, -3, 1));
                other_island.location_pool.Add(new HexPosition(2, -2, 0));
                other_island.location_pool.Add(new HexPosition(3, -3, 0));
                other_island.location_pool.Add(new HexPosition(3, -2, -1));
                other_island.location_pool.Add(new HexPosition(3, -1, -2));
                other_island.location_pool.Add(new HexPosition(3, 0, -3));

                other_island.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.gold, Resource.gold, Resource.wheat, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.sheep
                };

                other_island.number_pool = new List<ushort>()
                {
                    3, 4, 4, 5, 8, 9, 10, 12
                };

                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -4; x <= 4; x++)
                {
                    for (short y = -4; y <= 4; y++)
                    {
                        for (short z = -4; z <= 4; z++)
                        {
                            if (x + y + z == 0 && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)) && !other_island.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(other_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            case "seafarers_4_1":
            {
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
                    Resource.desert, Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood, Resource.wood
                };

                main_island.number_pool = new List<ushort>()
                {
                    2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(-3, 3, 0, 5));
                harbors.location_pool.Add(new HexPosition(-1, 3, -2, 0));
                harbors.location_pool.Add(new HexPosition(1, 2, -3, 0));
                harbors.location_pool.Add(new HexPosition(3, 0, -3, 1));
                harbors.location_pool.Add(new HexPosition(3, -2, -1, 2));
                harbors.location_pool.Add(new HexPosition(2, -3, 1, 2));
                harbors.location_pool.Add(new HexPosition(0, -3, 3, 3));
                harbors.location_pool.Add(new HexPosition(-2, -1, 3, 4));
                harbors.location_pool.Add(new HexPosition(-3, 1, 2, 4));

                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
                };
                
                TileGenerationSet other_island = new TileGenerationSet();
                other_island.location_pool.Add(new HexPosition(-2, -2, 4));
                other_island.location_pool.Add(new HexPosition(-1, -3, 4));
                other_island.location_pool.Add(new HexPosition(0, -4, 4));
                other_island.location_pool.Add(new HexPosition(1, -4, 3));
                other_island.location_pool.Add(new HexPosition(2, -5, 3));
                other_island.location_pool.Add(new HexPosition(3, -5, 2));
                other_island.location_pool.Add(new HexPosition(2, -4, 2));
                other_island.location_pool.Add(new HexPosition(3, -4, 1));
                other_island.location_pool.Add(new HexPosition(4, -5, 1));
                other_island.location_pool.Add(new HexPosition(4, -4, 0));
                other_island.location_pool.Add(new HexPosition(4, -3, -1));
                other_island.location_pool.Add(new HexPosition(4, -2, -2));
                other_island.location_pool.Add(new HexPosition(4, -1, -3));

                other_island.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.gold, Resource.gold, Resource.wheat, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.sheep, Resource.wood
                };

                other_island.number_pool = new List<ushort>()
                {
                    2, 3, 4, 5, 6, 8, 9, 10, 11
                };

                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -3; x <= 5; x++)
                {
                    for (short y = -6; y <= 3; y++)
                    {
                        for (short z = -4; z <= 5; z++)
                        {
                            if (x + y + z == 0 && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)) && !other_island.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(other_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            case "seafarers_5_1":
            case "seafarers_6_1":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                for (short x = -3; x <= 3; x++)
                {
                    for (short y = -3; y <= 2; y++)
                    {
                        for (short z = -2; z <= 3; z++)
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
                    Resource.desert,
                    Resource.desert, Resource.wheat, Resource.wheat, Resource.wood, Resource.wood, Resource.sheep, Resource.sheep, Resource.ore, Resource.ore, Resource. brick, Resource.brick
                };

                main_island.number_pool = new List<ushort>()
                {
                    2, 5, 4, 6, 3, 9, 8, 11, 11, 10, 6, 3, 8, 4, 8, 10, 11, 12, 10, 5, 4, 9, 5, 9, 12, 3, 12, 6
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(1, 2, -3, 0));
                harbors.location_pool.Add(new HexPosition(3, 0, -3, 1));
                harbors.location_pool.Add(new HexPosition(4, -3, -1, 2));
                harbors.location_pool.Add(new HexPosition(3, -4, 1, 2));
                harbors.location_pool.Add(new HexPosition(1, -4, 3, 3));
                harbors.location_pool.Add(new HexPosition(-1, -3, 4, 4));
                harbors.location_pool.Add(new HexPosition(-2, -2, 4, 3));
                harbors.location_pool.Add(new HexPosition(-4, 0, 4, 4));
                harbors.location_pool.Add(new HexPosition(-4, 2, 2, 5));
                harbors.location_pool.Add(new HexPosition(-3, 3, 0, 5));
                harbors.location_pool.Add(new HexPosition(-1, 3, -2, 5));

                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
                };
                
                TileGenerationSet other_island = new TileGenerationSet();
                other_island.location_pool.Add(new HexPosition(-3, 4, -1));
                other_island.location_pool.Add(new HexPosition(-2, 4, -2));
                other_island.location_pool.Add(new HexPosition(-1, 4, -3));
                other_island.location_pool.Add(new HexPosition(1, 3, -4));
                other_island.location_pool.Add(new HexPosition(2, 2, -4));
                other_island.location_pool.Add(new HexPosition(3, 1, -4));
                other_island.location_pool.Add(new HexPosition(-3, -2, 5));
                other_island.location_pool.Add(new HexPosition(-2, -3, 5));
                other_island.location_pool.Add(new HexPosition(-1, -4, 5));
                other_island.location_pool.Add(new HexPosition(1, -5, 4));
                other_island.location_pool.Add(new HexPosition(2, -5, 3));
                other_island.location_pool.Add(new HexPosition(3, -5, 2));

                other_island.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.gold, Resource.gold, Resource.gold, Resource.wheat, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.sheep, Resource.wood
                };

                other_island.number_pool = new List<ushort>()
                {
                    2, 3, 4, 5, 6, 8, 9, 10, 11, 12
                };

                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -4; x <= 4; x++)
                {
                    for (short y = -6; y <= 5; y++)
                    {
                        for (short z = -5; z <= 6; z++)
                        {
                            if (x + y + z == 0 && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)) && !other_island.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(other_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            case "seafarers_3_2":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                main_island.location_pool.Add(new HexPosition(-1, 3, -2));
                main_island.location_pool.Add(new HexPosition(-2, 3, -1));
                main_island.location_pool.Add(new HexPosition(-3, 3, 0));
                main_island.location_pool.Add(new HexPosition(-2, 2, 0));
                main_island.location_pool.Add(new HexPosition(-1, 2, -1));
                main_island.location_pool.Add(new HexPosition(-1, 1, 0));
                main_island.location_pool.Add(new HexPosition(1, 2, -3));
                main_island.location_pool.Add(new HexPosition(1, 1, -2));
                main_island.location_pool.Add(new HexPosition(2, 1, -3));
                main_island.location_pool.Add(new HexPosition(2, 0, -2));
                main_island.location_pool.Add(new HexPosition(-2, 0, 2));
                main_island.location_pool.Add(new HexPosition(-2, -1, 3));
                main_island.location_pool.Add(new HexPosition(-1, -1, 2));
                main_island.location_pool.Add(new HexPosition(-1, -2, 3));
                main_island.location_pool.Add(new HexPosition(1, -1, 0));
                main_island.location_pool.Add(new HexPosition(1, -2, 1));
                main_island.location_pool.Add(new HexPosition(2, -2, 0));
                main_island.location_pool.Add(new HexPosition(2, -3, 1));
                main_island.location_pool.Add(new HexPosition(3, -2, -1));
                main_island.location_pool.Add(new HexPosition(3, -3, 0));

                main_island.resource_pool = new List<Resource>()
                {
                    Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood, Resource.wood
                };

                main_island.number_pool = new List<ushort>()
                {
                    2, 3, 3, 4, 4, 5, 5, 5, 6, 6, 8, 8, 9, 9, 9, 10, 10, 11, 11, 12
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(-2, 4, -2, 5));
                harbors.location_pool.Add(new HexPosition(0, 2, -2, 2));
                harbors.location_pool.Add(new HexPosition(-3, 2, 1, 4));
                harbors.location_pool.Add(new HexPosition(2, 2, -4, 1));
                harbors.location_pool.Add(new HexPosition(1, 0, -1, 4));
                harbors.location_pool.Add(new HexPosition(-1, 0, 1, 1));
                harbors.location_pool.Add(new HexPosition(-2, -2, 4, 4));
                harbors.location_pool.Add(new HexPosition(0, -1, 1, 5));
                harbors.location_pool.Add(new HexPosition(3, -4, 1, 2));

                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
                };
                
                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -4; x <= 4; x++)
                {
                    for (short y = -4; y <= 4; y++)
                    {
                        for (short z = -4; z <= 4; z++)
                        {
                            if (x + y + z == 0 && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            case "seafarers_4_2":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                main_island.location_pool.Add(new HexPosition(-1, 3, -2));
                main_island.location_pool.Add(new HexPosition(-2, 3, -1));
                main_island.location_pool.Add(new HexPosition(-3, 3, 0));
                main_island.location_pool.Add(new HexPosition(-3, 2, 1));
                main_island.location_pool.Add(new HexPosition(-2, 1, 1));
                main_island.location_pool.Add(new HexPosition(-2, 2, 0));
                main_island.location_pool.Add(new HexPosition(-1, 2, -1));

                main_island.location_pool.Add(new HexPosition(1, 2, -3));
                main_island.location_pool.Add(new HexPosition(1, 1, -2));
                main_island.location_pool.Add(new HexPosition(2, 1, -3));
                main_island.location_pool.Add(new HexPosition(3, 0, -3));

                main_island.location_pool.Add(new HexPosition(-3, 0, 3));
                main_island.location_pool.Add(new HexPosition(-2, -1, 3));
                main_island.location_pool.Add(new HexPosition(-1, -1, 2));
                main_island.location_pool.Add(new HexPosition(-1, -2, 3));

                main_island.location_pool.Add(new HexPosition(0, 0, 0));
                main_island.location_pool.Add(new HexPosition(1, -1, 0));
                main_island.location_pool.Add(new HexPosition(1, -2, 1));
                main_island.location_pool.Add(new HexPosition(2, -1, -1));
                main_island.location_pool.Add(new HexPosition(2, -2, 0));
                main_island.location_pool.Add(new HexPosition(2, -3, 1));
                main_island.location_pool.Add(new HexPosition(3, -2, -1));
                main_island.location_pool.Add(new HexPosition(3, -3, 0));

                main_island.resource_pool = new List<Resource>()
                {
                    Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood, Resource.wood, Resource.wood
                };

                main_island.number_pool = new List<ushort>()
                {
                    2, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 8, 8, 9, 9, 9, 10, 10, 10, 11, 11, 11, 12
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(-2, 4, -2, 5));
                harbors.location_pool.Add(new HexPosition(-3, 4, -1, 0));
                harbors.location_pool.Add(new HexPosition(-1, 1, 0, 2));

                harbors.location_pool.Add(new HexPosition(0, 2, -2, 4));
                harbors.location_pool.Add(new HexPosition(2, 2, -4, 0));
                    
                harbors.location_pool.Add(new HexPosition(-2, -2, 4, 4));
                harbors.location_pool.Add(new HexPosition(0, -2, 2, 1));

                harbors.location_pool.Add(new HexPosition(1, 0, -1, 1));
                harbors.location_pool.Add(new HexPosition(1, -3, 2, 4));

                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
                };
                
                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -4; x <= 4; x++)
                {
                    for (short y = -4; y <= 4; y++)
                    {
                        for (short z = -4; z <= 4; z++)
                        {
                            if (x + y + z == 0 && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            case "seafarers_5_2":
            case "seafarers_6_2":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                main_island.location_pool.Add(new HexPosition(-2, 3));
                main_island.location_pool.Add(new HexPosition(-3, 3));
                main_island.location_pool.Add(new HexPosition(-3, 2));
                main_island.location_pool.Add(new HexPosition(-2, 2));
                main_island.location_pool.Add(new HexPosition(-1, 2));
                main_island.location_pool.Add(new HexPosition(1, 2));
                main_island.location_pool.Add(new HexPosition(2, 1));
                main_island.location_pool.Add(new HexPosition(1, 1));
                main_island.location_pool.Add(new HexPosition(2, 0));
                main_island.location_pool.Add(new HexPosition(3, 0));
                main_island.location_pool.Add(new HexPosition(3, -1));
                main_island.location_pool.Add(new HexPosition(1, -1));
                main_island.location_pool.Add(new HexPosition(2, -2));
                main_island.location_pool.Add(new HexPosition(1, -2));
                main_island.location_pool.Add(new HexPosition(2, -3));
                main_island.location_pool.Add(new HexPosition(3, -3));
                main_island.location_pool.Add(new HexPosition(-2, 0));
                main_island.location_pool.Add(new HexPosition(-3, 0));
                main_island.location_pool.Add(new HexPosition(-2, -1));
                main_island.location_pool.Add(new HexPosition(-1, -1));
                main_island.location_pool.Add(new HexPosition(-1, -2));
                main_island.location_pool.Add(new HexPosition(-3, -2));
                main_island.location_pool.Add(new HexPosition(-3, -3));
                main_island.location_pool.Add(new HexPosition(-2, -3));
                main_island.location_pool.Add(new HexPosition(-2, -4));
                main_island.location_pool.Add(new HexPosition(-1, -4));
                main_island.location_pool.Add(new HexPosition(-1, -5));
                main_island.location_pool.Add(new HexPosition(1, -5));
                main_island.location_pool.Add(new HexPosition(2, -5));
                main_island.location_pool.Add(new HexPosition(2, -6));
                main_island.location_pool.Add(new HexPosition(3, -5));
                main_island.location_pool.Add(new HexPosition(3, -6));
                
                main_island.resource_pool = new List<Resource>()
                {
                    Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.brick, Resource.brick, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.ore, Resource.ore, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood, Resource.wood, Resource.wood, Resource.wood, Resource.wood
                };

                main_island.number_pool = new List<ushort>()
                {
                    2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6, 8, 8, 8, 9, 9, 9, 9, 10, 10, 10, 10, 11, 11, 12, 12
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(-2, 4, (ushort)0));
                harbors.location_pool.Add(new HexPosition(-2, 1, (ushort)2));
                harbors.location_pool.Add(new HexPosition(0, 2, (ushort)5));
                harbors.location_pool.Add(new HexPosition(4, -1, (ushort)1));
                harbors.location_pool.Add(new HexPosition(1, 0, (ushort)4));
                harbors.location_pool.Add(new HexPosition(4, -4, (ushort)2));
                harbors.location_pool.Add(new HexPosition(-3, 1, (ushort)5));
                harbors.location_pool.Add(new HexPosition(-4, -1, (ushort)5));
                harbors.location_pool.Add(new HexPosition(-2, -5, (ushort)3));
                harbors.location_pool.Add(new HexPosition(1, -4, (ushort)5));
                harbors.location_pool.Add(new HexPosition(4, -7, (ushort)2));

                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
                };
                
                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -4; x <= 4; x++)
                {
                    for (short y = -7; y <= 4; y++)
                    {
                        for (short z = -4; z <= 7; z++)
                        {
                            if (x + y + z == 0 && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            /*case "seafarers_3_3":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                main_island.location_pool.Add(new HexPosition());
                
                main_island.resource_pool = new List<Resource>()
                {
                    Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood, Resource.wood
                };

                main_island.number_pool = new List<ushort>()
                {
                    3, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 11, 11, 12
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition());
                harbors.location_pool.Add(new HexPosition());
                harbors.location_pool.Add(new HexPosition());
                harbors.location_pool.Add(new HexPosition());
                harbors.location_pool.Add(new HexPosition());
                harbors.location_pool.Add(new HexPosition());
                harbors.location_pool.Add(new HexPosition());
                harbors.location_pool.Add(new HexPosition());
                
                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
                };
                
                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -4; x <= 4; x++)
                {
                    for (short y = -7; y <= 4; y++)
                    {
                        for (short z = -4; z <= 7; z++)
                        {
                            if (x + y + z == 0 && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }*/
            case "seafarers_3_4":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                for (short x = -3; x <= 1; x++)
                {
                    for (short y = -1; y <= 2; y++)
                    {
                        for (short z = 0; z <= 2; z++)
                        {
                            if (x + y + z == 0)
                            {
                                main_island.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }
                main_island.location_pool.Add(new HexPosition(-1, 2));
                main_island.location_pool.Add(new HexPosition(1, -2));
                main_island.location_pool.Add(new HexPosition(2, -2));

                main_island.resource_pool = new List<Resource>()
                {
                    Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood, Resource.wood
                };

                main_island.number_pool = new List<ushort>()
                {
                    2, 3, 4, 4, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11
                };

                TileGenerationSet desert = new TileGenerationSet();
                desert.location_pool.Add(new HexPosition(1, 0));
                desert.location_pool.Add(new HexPosition(2, -1));
                desert.location_pool.Add(new HexPosition(3, -2));
                
                desert.resource_pool = new List<Resource>()
                {
                    Resource.desert, Resource.desert, Resource.desert
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(0, 1, (ushort)1));
                harbors.location_pool.Add(new HexPosition(3, -3, (ushort)2));
                harbors.location_pool.Add(new HexPosition(0, -2, (ushort)2));
                harbors.location_pool.Add(new HexPosition(-2, -1, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-3, 0, (ushort)3));
                harbors.location_pool.Add(new HexPosition(-4, 2, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-3, 3, (ushort)0));
                harbors.location_pool.Add(new HexPosition(-2, 3, (ushort)5));

                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
                };
                
                TileGenerationSet other_island = new TileGenerationSet();
                other_island.location_pool.Add(new HexPosition(1, 1));
                other_island.location_pool.Add(new HexPosition(2, 0));
                other_island.location_pool.Add(new HexPosition(3, -1));
                other_island.location_pool.Add(new HexPosition(-3, -1));
                other_island.location_pool.Add(new HexPosition(-2, -2));
                other_island.location_pool.Add(new HexPosition(-1, -3));
                other_island.location_pool.Add(new HexPosition(0, -3));
                other_island.location_pool.Add(new HexPosition(1, -4));
                other_island.location_pool.Add(new HexPosition(2, -4));
                other_island.location_pool.Add(new HexPosition(3, -4));
                
                other_island.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.gold, Resource.gold, Resource.wheat, Resource.wheat, Resource.ore, Resource.ore, Resource.sheep, Resource.wood
                };

                other_island.number_pool = new List<ushort>()
                {
                    3, 4, 5, 5, 6, 8, 9, 11
                };

                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -4; x <= 4; x++)
                {
                    for (short y = -5; y <= 3; y++)
                    {
                        for (short z = -3; z <= 5; z++)
                        {
                            if (x + y + z == 0 && !desert.location_pool.Contains(new HexPosition(x, y, z)) && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)) && !other_island.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(desert);
                map.Add(other_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            case "seafarers_4_4":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                for (short x = -3; x <= 3; x++)
                {
                    for (short y = -3; y <= 1; y++)
                    {
                        for (short z = 0; z <= 2; z++)
                        {
                            if (x + y + z == 0)
                            {
                                main_island.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }
                main_island.location_pool.Add(new HexPosition(-1, 2));
                main_island.location_pool.Add(new HexPosition(-2, 2));

                main_island.resource_pool = new List<Resource>()
                {
                    Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood, Resource.wood, Resource.wood
                };

                main_island.number_pool = new List<ushort>()
                {
                    3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12
                };

                TileGenerationSet desert = new TileGenerationSet();
                desert.location_pool.Add(new HexPosition(1, 0));
                desert.location_pool.Add(new HexPosition(2, -1));
                desert.location_pool.Add(new HexPosition(3, -2));
                
                desert.resource_pool = new List<Resource>()
                {
                    Resource.desert, Resource.desert, Resource.desert
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(0, 1, (ushort)0));
                harbors.location_pool.Add(new HexPosition(3, -4, (ushort)3));
                harbors.location_pool.Add(new HexPosition(2, -4, (ushort)2));
                harbors.location_pool.Add(new HexPosition(0, -3, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-2, -1, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-3, 0, (ushort)3));
                harbors.location_pool.Add(new HexPosition(-4, 2, (ushort)5));
                harbors.location_pool.Add(new HexPosition(-3, 2, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-2, 3, (ushort)0));

                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
                };
                
                TileGenerationSet other_island = new TileGenerationSet();
                other_island.location_pool.Add(new HexPosition(1, 1));
                other_island.location_pool.Add(new HexPosition(2, 0));
                other_island.location_pool.Add(new HexPosition(3, -1));
                other_island.location_pool.Add(new HexPosition(-3, -1));
                other_island.location_pool.Add(new HexPosition(-3, -2));
                other_island.location_pool.Add(new HexPosition(-2, -3));
                other_island.location_pool.Add(new HexPosition(-1, -3));
                other_island.location_pool.Add(new HexPosition(-1, -4));
                other_island.location_pool.Add(new HexPosition(0, -4));
                other_island.location_pool.Add(new HexPosition(1, -5));
                other_island.location_pool.Add(new HexPosition(2, -5));
                other_island.location_pool.Add(new HexPosition(3, -5));
                
                other_island.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.gold, Resource.gold, Resource.wheat, Resource.wheat, Resource.wheat, Resource.brick, Resource.ore, Resource.ore, Resource.ore, Resource.sheep
                };

                other_island.number_pool = new List<ushort>()
                {
                    2, 3, 4, 5, 6, 8, 9, 10, 11, 12
                };

                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -4; x <= 4; x++)
                {
                    for (short y = -6; y <= 3; y++)
                    {
                        for (short z = -3; z <= 6; z++)
                        {
                            if (x + y + z == 0 && !desert.location_pool.Contains(new HexPosition(x, y, z)) && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)) && !other_island.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(desert);
                map.Add(other_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            case "seafarers_5_4":
            case "seafarers_6_4":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                for (short x = -1; x <= 1; x++)
                {
                    for (short y = -3; y <= 1; y++)
                    {
                        for (short z = -1; z <= 3; z++)
                        {
                            if (x + y + z == 0)
                            {
                                main_island.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }
                for (short x = -3; x <= 0; x++)
                {
                    for (short y = 2; y <= 4; y++)
                    {
                        for (short z = -3; z <= -1; z++)
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
                    Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.brick, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood, Resource.wood
                };

                main_island.number_pool = new List<ushort>()
                {
                    12, 9, 6, 4, 3, 11, 5, 12, 10, 6, 2, 5, 11, 8, 10, 3, 9, 4, 9, 10, 8
                };

                TileGenerationSet desert = new TileGenerationSet();
                for (short y = -4; y <= 0; y++)
                {
                    for (short z = -2; z <= 2; z++)
                    {
                        if (2 + y + z == 0)
                        {
                            desert.location_pool.Add(new HexPosition(2, y, z));
                        }
                    }
                }

                desert.resource_pool = new List<Resource>()
                {
                    Resource.desert, Resource.desert, Resource.desert, Resource.desert, Resource.desert
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(1, 3, (ushort)1));
                harbors.location_pool.Add(new HexPosition(1, 1, (ushort)1));
                harbors.location_pool.Add(new HexPosition(1, -4, (ushort)3));
                harbors.location_pool.Add(new HexPosition(-1, -3, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-2, -1, (ushort)5));
                harbors.location_pool.Add(new HexPosition(-2, 1, (ushort)5));
                harbors.location_pool.Add(new HexPosition(-2, 2, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-3, 3, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-4, 5, (ushort)5));
                harbors.location_pool.Add(new HexPosition(-2, 5, (ushort)0));
                
                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
                };
                
                TileGenerationSet other_island = new TileGenerationSet();
                for (short y = -6; y <= 1; y++)
                {
                    for (short z = -4; z <= 3; z++)
                    {
                        if (3 + y + z == 0)
                        {
                            other_island.location_pool.Add(new HexPosition(3, y, z));
                        }
                    }
                }
                other_island.location_pool.Add(new HexPosition(2, 2));
                other_island.location_pool.Add(new HexPosition(2, 1));
                for (short y = -3; y <= 2; y++)
                {
                    for (short z = 1; z <= 6; z++)
                    {
                        if (-3 + y + z == 0)
                        {
                            other_island.location_pool.Add(new HexPosition(-3, y, z));
                        }
                    }
                }
                other_island.location_pool.Add(new HexPosition(-2, -3));
                other_island.location_pool.Add(new HexPosition(-2, -4));
                other_island.location_pool.Add(new HexPosition(-1, -5));
                other_island.location_pool.Add(new HexPosition(0, -5));
                other_island.location_pool.Add(new HexPosition(1, -6));

                other_island.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.gold, Resource.gold, Resource.gold, Resource.wheat, Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood
                };

                other_island.number_pool = new List<ushort>()
                {
                    8, 2, 4, 11, 9, 3, 8, 10, 6, 3, 5, 12, 11, 6, 4, 5, 2
                };

                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -4; x <= 4; x++)
                {
                    for (short y = -7; y <= 5; y++)
                    {
                        for (short z = -5; z <= 7; z++)
                        {
                            if (x + y + z == 0 && !desert.location_pool.Contains(new HexPosition(x, y, z)) && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)) && !other_island.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(desert);
                map.Add(other_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            case "seafarers_3_5":
            case "seafarers_4_5":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                for (short x = -1; x <= 1; x++)
                {
                    for (short y = -3; y <= 2; y++)
                    {
                        for (short z = -3; z <= 3; z++)
                        {
                            if (x + y + z == 0)
                            {
                                main_island.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }
                main_island.location_pool.Add(new HexPosition(-1, 3));

                main_island.resource_pool = new List<Resource>()
                {
                    Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood, Resource.wood
                };

                main_island.number_pool = new List<ushort>()
                {
                    2, 3, 3, 4, 4, 5, 5, 6, 6, 8, 8, 9, 9, 10, 10, 11, 11, 12
                };
                                
                TileGenerationSet other_island = new TileGenerationSet();
                other_island.location_pool.Add(new HexPosition(3, 0));
                other_island.location_pool.Add(new HexPosition(3, -1));
                other_island.location_pool.Add(new HexPosition(3, -3));
                other_island.location_pool.Add(new HexPosition(3, -4));
                other_island.location_pool.Add(new HexPosition(3, -5));
                other_island.location_pool.Add(new HexPosition(1, -5));
                other_island.location_pool.Add(new HexPosition(-1, -4));
                other_island.location_pool.Add(new HexPosition(-3, -2));
                other_island.location_pool.Add(new HexPosition(-3, -1));
                other_island.location_pool.Add(new HexPosition(-3, 0));
                other_island.location_pool.Add(new HexPosition(-3, 2));
                other_island.location_pool.Add(new HexPosition(-3, 3));

                other_island.resource_pool = new List<Resource>()
                {
                    Resource.gold, Resource.gold, Resource.wheat, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.desert, Resource.desert, Resource.desert, Resource.sheep, Resource.wood
                };

                other_island.number_pool = new List<ushort>()
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(4, 0, (ushort)1));
                harbors.location_pool.Add(new HexPosition(4, -3, (ushort)1));
                harbors.location_pool.Add(new HexPosition(2, -6, (ushort)2));
                harbors.location_pool.Add(new HexPosition(-2, -4, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-4, 0, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-4, 4, (ushort)5));
                
                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any
                };
                
                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -4; x <= 4; x++)
                {
                    for (short y = -6; y <= 4; y++)
                    {
                        for (short z = -4; z <= 6; z++)
                        {
                            if (x + y + z == 0 && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !other_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea,                     Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea

                };

                map.Add(main_island);
                map.Add(other_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
            case "seafarers_5_5":
            case "seafarers_6_5":
            {
                TileGenerationSet main_island = new TileGenerationSet();
                for (short x = -1; x <= 1; x++)
                {
                    for (short y = -6; y <= 2; y++)
                    {
                        for (short z = -3; z <= 7; z++)
                        {
                            if (x + y + z == 0)
                            {
                                main_island.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }
                main_island.location_pool.Add(new HexPosition(-1, 3));
                main_island.location_pool.Add(new HexPosition(1, -7));

                main_island.resource_pool = new List<Resource>()
                {
                    Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.wheat, Resource.brick, Resource.brick, Resource.brick, Resource.brick, Resource.brick, Resource.brick, Resource.ore, Resource.ore, Resource.ore, Resource.ore, Resource.ore, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.sheep, Resource.wood, Resource.wood, Resource.wood, Resource.wood, Resource.wood, Resource.wood, Resource.wood
                };

                main_island.number_pool = new List<ushort>()
                {
                    2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 8, 8, 8, 9, 9, 9, 10, 10, 10, 11, 11, 11, 12
                };
                                
                TileGenerationSet other_island = new TileGenerationSet();
                other_island.location_pool.Add(new HexPosition(3, 0));
                other_island.location_pool.Add(new HexPosition(3, -1));
                other_island.location_pool.Add(new HexPosition(3, -3));
                other_island.location_pool.Add(new HexPosition(3, -4));
                other_island.location_pool.Add(new HexPosition(3, -6));
                other_island.location_pool.Add(new HexPosition(3, -7));
                other_island.location_pool.Add(new HexPosition(-3, 3));
                other_island.location_pool.Add(new HexPosition(-3, 2));
                other_island.location_pool.Add(new HexPosition(-3, 0));
                other_island.location_pool.Add(new HexPosition(-3, -1));
                other_island.location_pool.Add(new HexPosition(-3, -3));
                other_island.location_pool.Add(new HexPosition(-3, -4));

                other_island.resource_pool = new List<Resource>()
                {
                    Resource.gold, Resource.gold, Resource.gold, Resource.wheat, Resource.brick, Resource.ore, Resource.desert, Resource.desert, Resource.desert, Resource.desert, Resource.sheep, Resource.sheep
                };

                other_island.number_pool = new List<ushort>()
                {
                    0, 0, 0, 0, 0, 0, 0, 0
                };

                TileGenerationSet harbors = new TileGenerationSet();
                harbors.location_pool.Add(new HexPosition(4, -2, (ushort)2));
                harbors.location_pool.Add(new HexPosition(4, -3, (ushort)1));
                harbors.location_pool.Add(new HexPosition(4, -7, (ushort)2));
                harbors.location_pool.Add(new HexPosition(4, -8, (ushort)2));
                harbors.location_pool.Add(new HexPosition(-4, -2, (ushort)5));
                harbors.location_pool.Add(new HexPosition(-4, -1, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-4, 2, (ushort)4));
                harbors.location_pool.Add(new HexPosition(-4, 4, (ushort)5));

                harbors.resource_pool = new List<Resource>()
                {
                    Resource.harbor_brick, Resource.harbor_ore, Resource.harbor_sheep, Resource.harbor_wheat, Resource.harbor_wood, Resource.harbor_any, Resource.harbor_any, Resource.harbor_any
                };
                
                TileGenerationSet sea = new TileGenerationSet();
                for (short x = -4; x <= 4; x++)
                {
                    for (short y = -8; y <= 4; y++)
                    {
                        for (short z = -4; z <= 8; z++)
                        {
                            if (x + y + z == 0 && !main_island.location_pool.Contains(new HexPosition(x, y, z)) && !other_island.location_pool.Contains(new HexPosition(x, y, z)) && !harbors.location_pool.Contains(new HexPosition(x, y, z)))
                            {
                                sea.location_pool.Add(new HexPosition(x, y, z));
                            }
                        }
                    }
                }

                sea.resource_pool = new List<Resource>()
                {
                    Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea, Resource.sea
                };

                map.Add(main_island);
                map.Add(other_island);
                map.Add(harbors);
                map.Add(sea);
                break;
            }
        }

        return map;
    }
}