using System;
using System.Activities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Collections_2_
{
    class MyList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList
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

        public bool IsFixedSize { get { return false; } }

        object syncRoot = new object();
        public object SyncRoot { get { return syncRoot; } } //в теории я передаю в лок объект для работы с ним. не уверен что можно передать сам объект..

        public bool IsSynchronized { get { return true; }
}

        object IList.this[int index] { get { return this.Collection[index]; } set { this.Collection[index] = (T)value; } }


        public int Add(object value)
        {
            this.Add((T)value);
            return this.Collection.Length;
            
        }

        public bool Contains(object value)
        {
            return this.Contains<T>((T)value);
        }

        public int IndexOf(object value)
        {
            return this.IndexOf((T)value);
        }

        public void Insert(int index, object value)
        {
            this.Insert(index, (T)value);
        }

        public void Remove(object value)
        {
            Remove((T)value);
        }

        public void CopyTo(Array array, int index)
        {
            Collection.CopyTo(array, index);
        }
        #region IListRealisation
        public T this[int index] { get => this.Collection[index]; set => this.Collection[index] = value; }

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
            Array.Copy(this.Collection, 0, temp, 0, index);
            Array.Copy(this.Collection, index, temp, index + 1, temp.Length);
            temp[index] = item;
            this.Collection = temp;
        }

        public void RemoveAt(int index)
        {
            if (index == -1)
            {
                return;
            }
            if (index >= 0 && index < Collection.Length)
            {
                T[] temp = new T[Collection.Length - 1];
                Array.Copy(this.Collection, 0, temp, 0, index - 1);
                Array.Copy(this.Collection, index + 1, temp, index, temp.Length);
                this.Collection = temp;
            }
            else
                throw new Exception("Index is not in Array");
        }

        public void Add(T item)
        {
            T[] temp = new T[Collection.Length + 1];
            Array.Copy(this.Collection, 0, temp, 0, Collection.Length);
            temp[temp.Length] = item;
            this.Collection = temp;
        }

        public void Clear()
        {
            Collection = new T[0];
        }

        public bool Contains(T item)
        {
            return Collection.Contains<T>(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            int index = this.IndexOf(item);
            this.RemoveAt(index);
            if (index >= 0)
            { return true; }
            else
            { return false; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerato(this.Collection);
        }
        class MyEnumerato : IEnumerator<T>
        {
            T[] Collection;
            public MyEnumerato(T[] Col)
            {
                this.Collection = Col;
            }
            int position = -1;
            public T Current
            {
                get
                {
                    if (position != -1 && position < Collection.Length)
                    {
                        return Collection[position];
                    }
                    else
                    { throw new InvalidOperationException("How we can be here?"); }
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if (position != -1 && position < Collection.Length)
                    {
                        return (object)Collection[position];
                    }
                    else
                    { throw new InvalidOperationException("How we can be here?"); }
                }
            }

            #region dispose
            bool disposed = false;
            SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (disposed)
                    return;

                if (disposing)
                {
                    handle.Dispose();
                    // Free any other managed objects here.
                    //
                }

                disposed = true;
            }
            #endregion

            public bool MoveNext()
            {
                if (position < this.Collection.Length - 1)
                {
                    position++;
                    return true;
                }
                else
                    return false;
            }

            public void Reset()
            {
                position = -1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        #endregion

    }
}
