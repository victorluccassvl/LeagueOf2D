using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOf2D
{
    class PathNode
    {
        public int x;
        public int y;
        public int distance_to_destiny;
        public int cost_to_arrive_here;
        public int priority;
        public int heap_position;
        public int heap_state;
        public PathNode father;

        public PathNode(int x, int y)
        {
            this.x = x;
            this.y = y;

            this.cost_to_arrive_here = int.MaxValue;
            this.priority = -1;
            this.heap_position = -1;
            this.heap_state = -1;
       }

        public void InitDestiny(int x_dest, int y_dest)
        {
            this.distance_to_destiny = Math.Abs(x - x_dest) + Math.Abs(y - y_dest);
        }

        public static int ComparePriority(PathNode node1, PathNode node2)
        {
            if (node1.priority > node2.priority)
            {
                return 1;
            }
            if (node1.priority < node2.priority)
            {
                return -1;
            }
            if (node1.distance_to_destiny < node2.distance_to_destiny)
            {
                return 1;
            }
            if (node1.distance_to_destiny > node2.distance_to_destiny)
            {
                return -1;
            }
            return 0;
        }
    }
}
