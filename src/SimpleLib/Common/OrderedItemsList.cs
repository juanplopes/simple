using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Common
{
    public class OrderedItemsList<T> : LinkedList<T>
    {
        Dictionary<T, LinkedListNode<T>> positionDictionary;

        public OrderedItemsList()
        {
            positionDictionary = new Dictionary<T, LinkedListNode<T>>();
        }

        public void ForceFirst(T item)
        {
            lock (this)
            {
                LinkedListNode<T> node = null;
                positionDictionary.TryGetValue(item, out node);

                if (node == null)
                {
                    node = this.AddFirst(item);
                    positionDictionary[item] = node;
                }
                else
                {
                    this.Remove(node);
                    this.AddFirst(node);
                }
            }
        }

    }
}
