using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOf2D
{
    class PathHeap
    {
        private int max_length;
        private int length;
        private PathNode[] queue;

        public PathHeap (int max_size)
        {
            this.max_length = max_size;
            this.length = 0;
            this.queue = new PathNode[max_size];            
        }

        public void Flush ()
        {
            this.length = 0;
        }

        public bool IsFull ()
        {
            return this.max_length == this.length;
        }

        public bool isEmpty ()
        {
            return this.length == 0;
        }


        public void Change (PathNode n1, PathNode n2)
        {
            int i_aux;
            PathNode aux;

            aux = this.queue[n1.heap_position];
            this.queue[n1.heap_position] = this.queue[n2.heap_position];
            this.queue[n2.heap_position] = aux;

            i_aux = n1.heap_position;
            n1.heap_position = n2.heap_position;
            n2.heap_position = i_aux;
        }


        public void Insert (PathNode node)
        {
            this.queue[this.length] = node;
            node.heap_position = this.length;
            node.heap_state = 0;
            this.length++;
            this.InsertFix(this.length-1);
        }

        public void InsertFix (int son_index)
        {
            int father_index;

            father_index = (int) (son_index - 1) / 2;


            if (father_index >= 0)
            {
                if ( PathNode.ComparePriority(this.queue[father_index], this.queue[son_index]) == 1 )
                {
                    this.Change(this.queue[father_index], this.queue[son_index]);

                    this.InsertFix(father_index);
                }
            }
        }



        public PathNode Remove ()
        {
            PathNode removed;

            this.Change(this.queue[0], this.queue[this.length-1]);

            removed = this.queue[this.length-1];

            this.length--;

            this.RemoveFix(0);

            removed.heap_state = 1;

            return removed;
        }



        public void RemoveFix (int father_index)
        {
            int left_son_index;
            int right_son_index;
            bool left_son_exists, right_son_exists, both_sons_exists, one_son_exists;

            left_son_index = 2 * father_index + 1;
            right_son_index = 2 * father_index + 2;

            left_son_exists = left_son_index < this.length;
            right_son_exists = right_son_index < this.length;

            both_sons_exists = left_son_exists && right_son_exists;
            one_son_exists = left_son_exists || right_son_exists;

            if (both_sons_exists)
            {
                int trade_son;
                if (PathNode.ComparePriority(this.queue[left_son_index], this.queue[right_son_index]) == 1)
                {
                    trade_son = right_son_index;
                }
                else
                {
                    trade_son = left_son_index;
                }

                if (PathNode.ComparePriority(this.queue[trade_son], this.queue[father_index]) == -1)
                {

                    this.Change(this.queue[father_index], this.queue[trade_son]);

                    RemoveFix(trade_son);
                }
            }
            else if (one_son_exists)
            {
                if (PathNode.ComparePriority(this.queue[left_son_index], this.queue[father_index]) == -1)
                {
                    this.Change(this.queue[father_index], this.queue[left_son_index]);

                    RemoveFix(left_son_index);
                }
            }
        }

    }
}