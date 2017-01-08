using System.Diagnostics;

namespace LeagueOf2D
{
    /*\
     * Class that represents a priority queue of path nodes
    \*/
    class PathHeap
    {

        /*\
         * Attributes and Properties
        \*/

        // Priority queue's array
        public PathNode[] queue;
        // Max array size
        public int max_length;
        // Size in use currently
        public int length;



        /*\
         * PathHeap constructor
         * 
         * :max_size: max array size this instance will support
        \*/
        public PathHeap (int max_size)
        {
            this.max_length = max_size;

            // It has 0 elements at creation
            this.length = 0;

            // Creates the array
            this.queue = new PathNode[max_size];            
        }



        /*\
         * Method that removes every element of the array
         * 
         * :return: nothing
        \*/
        public void Flush ()
        {
            this.length = 0;
        }



        /*\
         * Method that verifies if this array is full
         * 
         * :return: true if it is full, false otherwise
        \*/
        public bool IsFull ()
        {
            return this.max_length == this.length;
        }



        /*\
         * Method that verifies if this array is empty
         * 
         * :return: true if it is empty, false otherwise
        \*/
        public bool isEmpty ()
        {
            return this.length == 0;
        }



        /*\
         * Method that swaps the position of two nodes at the queue
         * 
         * :n1,n2: nodes to be swapped
         * 
         * :return: nothing
        \*/
        public void Swap (PathNode n1, PathNode n2)
        {
            int i_aux;
            PathNode aux;

            // Swaps the position
            aux = this.queue[n1.heap_position];
            this.queue[n1.heap_position] = this.queue[n2.heap_position];
            this.queue[n2.heap_position] = aux;

            // Fix node's index field
            i_aux = n1.heap_position;
            n1.heap_position = n2.heap_position;
            n2.heap_position = i_aux;
        }



        /*\
         * Method that inserts a node at the queue
         * 
         * :node: node to be inserted
         * 
         * :return: nothing
        \*/
        public void Insert (PathNode node)
        {
            // If this heap can't accept more nodes, something wrong happened
            Debug.Assert((this.max_length > this.length), Error.Message_01(this.max_length,this.length));

            // If this heap can accept more nodes, insert
            // Insert the node
            this.queue[this.length] = node;

            // Initializes it's index field
            node.heap_position = this.length;

            // Initializes it's heap state
            node.heap_state = HeapState.CURRENT_IN_HEAP;

            // Warns that heap size increased
            this.length++;

            // Fix this node position to obbey priority order
            this.InsertFix(this.length - 1);
        }



        /*\
         * Method that fix a node position at queue ( inserted )
         * 
         * :son_index: index of the node that must be fixed
         * 
         * :return: nothing
        \*/
        public void InsertFix (int son_index)
        {
            int father_index;
            
            // Finds this node's father
            father_index = (int) (son_index - 1) / 2;

            // Verify if this father exists
            if (father_index >= 0)
            {
                // If this father has a higher priority ( This is a minor heap )
                if ( PathNode.ComparePriority(this.queue[father_index], this.queue[son_index]) == 1 )
                {
                    // Swap it with it's son
                    this.Swap(this.queue[father_index], this.queue[son_index]);

                    // Fix it's position
                    this.InsertFix(father_index);
                }
            }
        }



        /*\
         * Method that removes a node from the beggining of the queue
         * 
         * :return: removed node
        \*/
        public PathNode Remove ()
        {
            PathNode removed;

            // If there's no node to be removed, something wrong happened
            Debug.Assert((this.length > 0), Error.Message_02(this.length));

            // If there's a node to be removed, removes it
            // Swaps it with the last node
            this.Swap(this.queue[0], this.queue[this.length - 1]);

            // Save it's reference
            removed = this.queue[this.length - 1];
 
            // Warns that heap size decreased
            this.length--;

            // Fix the first node position to obbey priority order
            this.RemoveFix(0);

            // Initializes removed node's heap state
            removed.heap_state = HeapState.LEFT_HEAP;

            // Return this removed node
            return removed;
        }


        
        /*\
         * Method that fix a node position at queue ( removed )
         * 
         * :father_index: index of the node that must be fixed
         * 
         * :return: nothing
        \*/
        public void RemoveFix (int father_index)
        {
            int left_son_index;
            int right_son_index;
            bool both_sons_exists, one_son_exists;

            // Find this node's sonss
            left_son_index = 2 * father_index + 1;
            right_son_index = 2 * father_index + 2;

            // Verify if they exist
            one_son_exists = left_son_index < this.length;
            both_sons_exists = right_son_index < this.length;
            
            // If both sons exists
            if (both_sons_exists)
            {
                int trade_son;

                // Verify which one of them has the highest priority
                if (PathNode.ComparePriority(this.queue[left_son_index], this.queue[right_son_index]) == 1)
                {
                    trade_son = right_son_index;
                }
                else
                {
                    trade_son = left_son_index;
                }

                // Verify if the father has a higher priority than the highest son ( this is a minor heap )
                if (PathNode.ComparePriority(this.queue[father_index], this.queue[trade_son]) == 1)
                {
                    // Swaps it with it's son
                    this.Swap(this.queue[father_index], this.queue[trade_son]);

                    // Fix this son's position
                    RemoveFix(trade_son);
                }
            }
            // If only one son exists, it is the left one
            else if (one_son_exists)
            {
                // Verify if the father has a higher priority than this
                if (PathNode.ComparePriority(this.queue[father_index], this.queue[left_son_index]) == 1)
                {
                    // Swaps it with it's son
                    this.Swap(this.queue[father_index], this.queue[left_son_index]);

                    // Fix this son's position
                    RemoveFix(left_son_index);
                }
            }
        }
    }
}