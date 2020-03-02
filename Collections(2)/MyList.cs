using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections_2_
{
    class MyList<T> : IList<T>
    {
        T[] Collection;
        public int Count
        { get { if (Collection != null)
                    return Collection.Length;
                else
                    throw new Exception("Object not created");
            }
        }

        public bool IsReadOnly {get { return false; } }

        public T this[int index] { get => this.Collection[index]; set => this.Collection[index]=value; }

        public int IndexOf(T item)
        {
            if (this.Collection != null)
            {
                for (int i = 0; i < Collection.Length; i++)
                {
                    if (this.Collection[i].GetHashCode() == item.GetHashCode())
                    { return i; }
                }
                return -1;
            }
            throw new Exception("Object is not consisted");
        }

        public void Insert(int index, T item)
        {
            T[] temp = new T[Collection.Length + 1];
            Array.Copy(this.Collection, 0, temp, 0,index);
            Array.Copy(this.Collection, index, temp, index+1, temp.Length);
            temp[index] = item;
            this.Collection = temp;
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        
    }
}
