using System;

namespace ConsoleAppVS2017
{
    /// <summary>
    /// Array based implementation of Min Heap 
    /// </summary>
    /// <typeparam name="T">T should be IComparable</typeparam>
    public class MinHeap<T> where T : IComparable<T>
    {
        //This will be used in case default contructor is called for initializing MinHeap
        private const int DefaultHeapSize = 10;

        /// <summary>
        /// Heap storage array
        /// </summary>
        private readonly T[] heap;

        /// <summary>
        /// lastItemIndex keeps track of position where new item needs be added
        /// </summary>
        private int lastItemIndex;

        /// <summary>
        /// Gives count of elements currently present in Heap
        /// </summary>
        public int Count { get { return lastItemIndex; } }

        #region Constructors

        public MinHeap() : this(DefaultHeapSize)
        {
        }

        public MinHeap(int initialSize)
        {
            //TODO: Check against predefined MaxSize and throw some checked exception
            this.heap = new T[initialSize];
            lastItemIndex = 0;
        }

        #endregion

        /// <summary>
        /// Get min element of heap without pulling it out
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (lastItemIndex == 0)
            {
                throw new InvalidOperationException("Heap Empty");
            }
            return heap[0];
        }

        /// <summary>
        /// Add a new element in heap
        /// </summary>
        /// <param name="item">Item to be added</param>
        public void Push(T item)
        {
            //Validate if heap has space left for adding new element
            if (lastItemIndex == heap.Length)
            {
                //TODO: Implement Grow function and throw in case heap reaches max permissible size limit
                throw new OverflowException("Heap overflowed");
            }

            heap[lastItemIndex] = item;
            //Add element at the end of tree and increment array index pointer
            lastItemIndex++;

            //Print();
            //Console.Write("\t\t");


            //Adding a new element may break min heap property "All root elements should be smaller than their child nodes"
            HeapUp();
            
            //Print();
        }

        /// <summary>
        /// Remove min element of heap
        /// </summary>
        /// <returns>Min element of heap</returns>
        public T Pop()
        {
            if (lastItemIndex == 0)
            {
                throw new InvalidOperationException("Heap Empty");
            }

            T result = heap[0];

            //Print();
            //Console.Write("\t\t");

            //After removing min element we need to rebalance the tree by moving one of the child nodes up
            HeapDown();
            
            //Print();

            return result;
        }

        #region Private Functions

        /// <summary>
        /// Hear we begin from last item of heap and bubble it up till we find a node where both its child are bigger than it
        /// To bubble up we find Index of parent node which is equal to (index of current item - 1) / 2
        /// and swap if current item is smaller than its parent
        /// We repeat steps till heap property gets true or root node is reached
        /// </summary>
        private void HeapUp()
        {
            int itemIndex = lastItemIndex - 1;
            do
            {
                int parentIndex = (itemIndex - 1) / 2;
                if (heap[itemIndex].CompareTo(heap[parentIndex]) < 0)
                {
                    Swap(itemIndex, parentIndex);
                    itemIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }
            while (itemIndex != 0);
        }

        /// <summary>
        /// Here we move the last item of tree at top position and then rebalance tree by swapping smaller of two child nodes with root node
        /// We keep iterating till we have reached end of tree or tree gets balanced
        /// </summary>
        private void HeapDown()
        {
            lastItemIndex--;
            //Move last item at top
            heap[0] = heap[lastItemIndex];
            //replace moved item with default value, null for reference types, 0 for int, etc
            heap[lastItemIndex] = default(T);

            int currentItemIndex = 0;
            while (currentItemIndex != lastItemIndex)
            {
                int minChildIndex = FindIndexOfMinChild(currentItemIndex);
                if (minChildIndex != -1 && heap[currentItemIndex].CompareTo(heap[minChildIndex]) > 0)
                {
                    Swap(currentItemIndex, minChildIndex);
                    currentItemIndex = minChildIndex;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Finds smaller of two child nodes
        /// </summary>
        /// <param name="currentItemIndex"></param>
        /// <returns>-1 if its lead node  else index of smaller child or only child available</returns>
        private int FindIndexOfMinChild(int currentItemIndex)
        {
            int leftChildIndex = (2 * currentItemIndex) + 1;
            int rightChildIndex = (2 * currentItemIndex) + 2;

            //Current item is last item of heap
            if (leftChildIndex >= lastItemIndex && rightChildIndex >= lastItemIndex)
            {
                return -1;
            }

            if (leftChildIndex >= lastItemIndex && rightChildIndex < lastItemIndex)
            {
                return rightChildIndex;
            }
            if (rightChildIndex >= lastItemIndex && leftChildIndex < lastItemIndex)
            {
                return leftChildIndex;
            }
            if (heap[leftChildIndex].CompareTo(heap[rightChildIndex]) < 0)
            {
                return leftChildIndex;
            }
            else
            {
                return rightChildIndex;
            }
        }

        private void Swap(int index1, int index2)
        {
            T temp = heap[index1];
            heap[index1] = heap[index2];
            heap[index2] = temp;
        }

        private void Print()
        {
            Console.WriteLine(string.Join(",", heap));
        }

        #endregion
    }
}
