using System;

namespace LeagueOf2D
{
    // Enumerate the state of a node based on it's relation with the priority heap
    public enum HeapState { NEVER_IN_HEAP, CURRENT_IN_HEAP, LEFT_HEAP };

    /*\
     * Class that represents a node used in path finding
    \*/
    class PathNode
    {

        /*\
         * Attributes and Properties
        \*/

        // Pixel X and Y position
        public int x;
        public int y;
        // It's canonical distance to the destiny
        public int distance_to_destiny;
        // The cost to arrive at this pixel from player position
        public int cost_to_arrive_here;
        // This pixel priority in path finding
        public int priority;
        // The position of this pixel at the priority queue
        public int heap_position;
        // The state of this pixel at the priority queue
        public HeapState heap_state;
        // The information that this pixel is or not a empty space
        public bool obstructed;
        // The node that threw this node at queue
        public PathNode previous;
        public PathNode next;



        /*\
         * PathNode constructor
         * 
         * :x,y: this node position
        \*/
        public PathNode(int x, int y)
        {
            // Set the position
            this.x = x;
            this.y = y;

            // Initializes the heap state
            this.heap_state = HeapState.NEVER_IN_HEAP;
       }



        /*\
         * Method that initializes the destiny distance
         * 
         * :x_dest,y_dest: destination position
         * 
         * :return: nothing
        \*/
        public void InitDestiny(int x_dest, int y_dest)
        {
            // Finds the canonical distance
            this.distance_to_destiny = Math.Abs(x - x_dest) + Math.Abs(y - y_dest);
        }



        /*\
         * Method that compares the priority of two different nodes
         * 
         * :node1,node2: nodes to be compared
         * 
         * :return: -1 if node1<node2, 0 if node1=node2, 1 if node1>node2
        \*/
        public static int ComparePriority(PathNode node1, PathNode node2)
        {
            // First, compare priorities
            if (node1.priority > node2.priority)
            {
                return 1;
            }
            if (node1.priority < node2.priority)
            {
                return -1;
            }

            // If priority draws, compare distance to destiny
            if (node1.distance_to_destiny < node2.distance_to_destiny)
            {
                return 1;
            }
            if (node1.distance_to_destiny > node2.distance_to_destiny)
            {
                return -1;
            }

            // If priority and ditance draws, they are equal
            return 0;
        }
    }
}
