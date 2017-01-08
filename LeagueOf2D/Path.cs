using Microsoft.Xna.Framework;
using System;

namespace LeagueOf2D
{
    /*\
     * Class that helps an entity to find a path through the map
    \*/
    class Path
    {
        /*\
         * Attributes and Properties
        \*/

        // Map that informs if specific pixels are walls
        public Map obstruction_map;
        // Map used to run path finding
        public PathNode[][] path_map;
        // Priority queue
        public PathHeap heap;
        // Destiny of path
        public Vector2 destiny;
        // Border of specific entity
        public Border border;
        // Current pixel at path map
        public Vector2 current_pixel;



        /*\
         * Path constructor
         * 
         * :border: auxiliar structure to identify pixels of entity radius
         * :map: map where the entity is in
        \*/
        public Path (Border border, Map map)
        {
            this.border = border;
            this.obstruction_map = map;

            // Gets map dimensions
            int width = (int)map.size.X;
            int length = (int)map.size.Y;

            // Creates the path map and the priority queue
            this.path_map = new PathNode[width][];
            for(int i = 0; i < width; i++)
            {
                this.path_map[i] = new PathNode[length];
            }
            this.heap = new PathHeap(width * length);

            // For each pixel in map
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    // Creates a node to represent this pixel
                    this.path_map[x][y] = new PathNode(x, y);

                    // Marks if this pixel is obstructed
                    if (map.isObstructed(x, y))
                    {
                        this.path_map[x][y].obstructed = true;
                    }

                    // For each neighbour pixel at the border of the radius centered at this pixel
                    for (int i = 0; i < border.size; i++)
                    {
                        // Gets this border pixel
                        int X = border.radius_border[0, i] + x;
                        int Y = border.radius_border[1, i] + y;

                        // Verifies if it exists
                        if ((X >= 0) && (X < map.size.X) && (Y >= 0) && (Y < map.size.Y))
                        {
                            // If it is obstructed, marks it
                            if (map.isObstructed(X, Y))
                            {
                                this.path_map[x][y].obstructed = true;
                            }
                        }
                    }
                }
            }
        }



        /*\
         *  Method that prepares the map for a new calculation
         *  
         * :destiny: the end point of this map path
         * 
         * :return: nothing
        \*/
        public void InitializePath (Vector2 destiny)
        {
            this.destiny = destiny;

            // Resets the priority queue
            this.heap.Flush();

            // For each path node
            for(int x = 0; x < this.obstruction_map.size.X; x++)
            {
                for(int y = 0; y < this.obstruction_map.size.Y; y++)
                {
                    // Calculates the distance from this node to the destiny point
                    this.path_map[x][y].InitDestiny((int)destiny.X, (int)destiny.Y);

                    // Marks this node as never in heap
                    this.path_map[x][y].heap_state = HeapState.NEVER_IN_HEAP;

                    // Sets the previous node as null
                    this.path_map[x][y].previous = null;
                    this.path_map[x][y].next = null;
                }
            }
        }



        /*\
         *  Method that calculate a new path
         *  
         * :destiny: the end point of this map path
         * :start: the start point of this map path
         * 
         * :return: nothing
        \*/
        public void CreatePath (Vector2 destiny, Vector2 start)
        {
            this.current_pixel = new Vector2(start.X, start.Y);

            // Prepares the structurs for a new calculation
            this.InitializePath(destiny);

            // Gets start X and Y
            int X = (int) start.X;
            int Y = (int) start.Y;

            // Inserts the node at X,Y in the priority queue
            this.heap.Insert(this.path_map[X][Y]);

            // Sets the cost to arrive at this pixel as 0 ( it's the start point )
            this.path_map[X][Y].cost_to_arrive_here = 0;
            
            // Initializes the priority
            this.path_map[X][Y].priority = this.path_map[X][Y].distance_to_destiny;

            // While the priority queue is not empty
            while (!this.heap.isEmpty())
            {
                // Removes a node
                PathNode removed = this.heap.Remove();
                PathNode neighbour;

                // For each neighbour of this node
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        // Gets it's position
                        int tile_x = removed.x + x;
                        int tile_y = removed.y + y;

                        // Gets the neighbour
                        neighbour = this.path_map[tile_x][tile_y];

                        // If this neighbour is not a wall
                        if (!neighbour.obstructed)
                        {
                            // If this neighbour was never in heap
                            if (neighbour.heap_state == HeapState.NEVER_IN_HEAP)
                            {

                                // Sets the cost to go from removed pixel to this neighbour
                                if (x == y || x == (-1) * y)
                                {
                                    // If its a diagonal neighbour
                                    neighbour.cost_to_arrive_here = removed.cost_to_arrive_here + 14;
                                }
                                else
                                {
                                    // If its not a diagonal neighbour
                                    neighbour.cost_to_arrive_here = removed.cost_to_arrive_here + 10;
                                }

                                // Block that initializes neighbour priority
                                {
                                    int cost_to_arrive = neighbour.cost_to_arrive_here;
                                    int distance_to_destiny = neighbour.distance_to_destiny;

                                    neighbour.priority = cost_to_arrive + distance_to_destiny;
                                }

                                // Creates a path from removed node to this neighbour
                                removed.next = neighbour;
                                neighbour.previous = removed;

                                // If neighbour node is the destiny, the path has been found
                                if (tile_x == destiny.X && tile_y == destiny.Y)
                                {
                                    return;
                                }
                                // If neighbour node is not the destiny, continues searching, inserting it at the queue
                                else
                                {
                                    this.heap.Insert(neighbour);
                                }


                            }
                            // If this neighbour is already in heap
                            else if (neighbour.heap_state == HeapState.CURRENT_IN_HEAP)
                            {
                                // Creates an offer cost for it
                                int temp_cost;

                                // Sets the cost to go from removed pixel to this neighbour
                                if (x == y || x == (-1) * y)
                                {
                                    temp_cost = removed.cost_to_arrive_here + 14;
                                }
                                else
                                {
                                    temp_cost = removed.cost_to_arrive_here + 10;
                                }

                                // If this cost is better for this neighbour
                                if (temp_cost < neighbour.cost_to_arrive_here)
                                {
                                    // Updates this neighbour and make a path between it and removed node
                                    neighbour.cost_to_arrive_here = temp_cost;
                                    neighbour.priority = temp_cost + neighbour.distance_to_destiny;
                                    removed.next = neighbour;
                                    neighbour.previous = removed;

                                    // Updates this neighbour position in priority queue
                                    this.heap.InsertFix(neighbour.heap_position);
                                }
                            }
                        }
                    }
                }
            }
        }




        public bool Walk (float distance)
        {
            int x, y, next_x, next_y;
            PathNode neighbour;

            Console.WriteLine("So?\n");
            while (distance > 0)
            {
                x = (int)this.current_pixel.X;
                y = (int)this.current_pixel.Y;

                neighbour = this.path_map[x][y].next;
                
                if (neighbour == null)
                {
                    return false;
                }

                next_x = neighbour.x;
                next_y = neighbour.y;
                
                switch (Math.Abs(x - next_x) + Math.Abs(y - next_y))
                {
                    case 1:
                        distance -= 1;
                        break;

                    case 2:
                        distance -= (float)Math.Sqrt(2);
                        break;
                }
                this.current_pixel.X = neighbour.x;
                this.current_pixel.Y = neighbour.y;
            }

            return true;
        }
    }
}