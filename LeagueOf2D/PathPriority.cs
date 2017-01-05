using System;
using System.Drawing;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LeagueOf2D
{
    class PathPriority
    {
        private Player player;
        private Map obstruction_map;
        public PathNode[,] path_map;
        private PathHeap heap;
        private Vector2 size;
        private Vector2 destiny;

        public PathPriority (Player player)
        {
            this.player = player;
            this.obstruction_map = this.player.map;
            this.size = this.obstruction_map.size;

            this.path_map = new PathNode[(int) this.size.X, (int) this.size.Y];
            this.heap = new PathHeap((int)(this.size.X * this.size.Y));
            for (int x = 0; x < this.size.X; x++)
            {
                for (int y = 0; y < this.size.Y; y++)
                {
                    this.path_map[x, y] = new PathNode(x, y);
                }
            }
        }



        public void InitializePath (Vector2 destiny)
        {
            this.destiny = destiny;
            this.heap.Flush();
            for(int x=0; x < this.size.X; x++)
            {
                for(int y=0; y < this.size.Y; y++)
                {
                    this.path_map[x, y].InitDestiny((int)destiny.X, (int)destiny.Y);
                    this.path_map[x, y].cost_to_arrive_here = int.MaxValue;
                    this.path_map[x, y].heap_position = -1;
                    this.path_map[x, y].heap_state = -1;
                    this.path_map[x, y].father = null;
                    this.path_map[x, y].priority = -1;
                }
            }
        }

        public void CreatePath (Vector2 destiny)
        {
            this.InitializePath(destiny);

            int start_x = (int) this.player.position.X;
            int start_y = (int) this.player.position.Y;
            Console.Write(start_x + "  " + start_y);
            this.heap.Insert(this.path_map[start_x, start_y]);

            this.path_map[start_x, start_y].cost_to_arrive_here = 0;
            this.path_map[start_x, start_y].priority = this.path_map[start_x, start_y].distance_to_destiny;

            int[,] border = this.player.radius_border;
            int size = this.player.border_size;

            while (!this.heap.isEmpty())
            {
                PathNode removed = this.heap.Remove();
                PathNode neighbour;

                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        int tile_x = removed.x + x;
                        int tile_y = removed.y + y;

                        neighbour = this.path_map[tile_x, tile_y];

                        if (neighbour.heap_state == -1)
                        {

                            bool obstructed = this.obstruction_map.isObstructed(tile_x, tile_y);

                            if (!obstructed)
                            {

                                if ( x == y || x == (-1) * y )
                                {
                                    neighbour.cost_to_arrive_here = removed.cost_to_arrive_here + 14;
                                }
                                else
                                {
                                    neighbour.cost_to_arrive_here = removed.cost_to_arrive_here + 10;
                                }

                                {
                                    int cost_to_arrive = neighbour.cost_to_arrive_here;
                                    int distance_to_destiny = neighbour.distance_to_destiny;

                                    neighbour.priority = cost_to_arrive + distance_to_destiny;
                                }

                                neighbour.father = removed;
                                
                                if ( tile_x == destiny.X && tile_y == destiny.Y)
                                {
                                    return;
                                }
                                else
                                {
                                    this.heap.Insert(neighbour);
                                }
                            }
                        }
                        else if (neighbour.heap_state == 0)
                        {
                            int temp_cost;
                        
                            if (x == y || x == (-1) * y)
                            {
                                temp_cost = removed.cost_to_arrive_here + 14;
                            }
                            else
                            {
                                temp_cost = removed.cost_to_arrive_here + 10;
                            }

                            if ( temp_cost < neighbour.cost_to_arrive_here)
                            {
                                neighbour.cost_to_arrive_here = temp_cost;
                                neighbour.priority = temp_cost + neighbour.distance_to_destiny;
                                neighbour.father = removed;

                                this.heap.InsertFix(neighbour.heap_position);
                            }
                        }
                    }
                }
            }
        }
    }
}