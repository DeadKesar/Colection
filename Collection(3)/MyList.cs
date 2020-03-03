using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Collections_2_
{
   // class MyList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
        class MyDictionary<TKey,TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISerializable, IDeserializationCallback
    {
       
       
        TKey[] keyCollection;
        TValue[] valueCollection;
        
        public MyDictionary()
        {
            keyCollection = new TKey[0];
            valueCollection = new TValue[0];
        }
        
        public int Count
        { get { if (valueCollection != null)
                    return valueCollection.Length;
                else
                    throw new Exception("Object not created");
            }
        }

        public bool IsReadOnly {get { return false; } }

        public bool IsFixedSize { get { return false; } }

        object syncRoot = new object();
        public object SyncRoot { get { return syncRoot; } } //в теории я передаю в лок объект для работы с ним. не уверен что можно передать сам объект..

        public bool IsSynchronized { get { return true; }   }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys { get { return this.keyCollection; } }

        ICollection<TValue> IDictionary<TKey, TValue>.Values { get { return this.valueCollection; } }

        int ICollection<KeyValuePair<TKey, TValue>>.Count { get { return this.keyCollection.Length; } }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly   {  get { return false; } }

        public ICollection Keys => throw new NotImplementedException();

        public ICollection Values => throw new NotImplementedException();

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => throw new NotImplementedException();

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => throw new NotImplementedException();

        public TValue this[TKey key] => throw new NotImplementedException();

        public object this[object key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        TValue IDictionary<TKey, TValue>.this[TKey key] {
            get { return this.valueCollection[Array.IndexOf<TKey>(keyCollection, key)]; } 
            set { this.valueCollection[Array.IndexOf<TKey>(keyCollection, key)] = value;  } 
        }

       
        bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
        {
            return keyCollection.Contains(key);
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            TKey[] tempKey = new TKey[keyCollection.Length + 1];
            TValue[] tempValue = new TValue[valueCollection.Length + 1];
            Array.Copy(this.keyCollection, 0, tempKey, 0, keyCollection.Length);
            Array.Copy(this.valueCollection, 0, tempValue, 0, valueCollection.Length);
            tempKey[tempKey.Length - 1] = key;
            tempValue[tempValue.Length -1] = value;
            this.keyCollection = tempKey;
            this.valueCollection = tempValue;
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            int index = Array.IndexOf<TKey>(keyCollection, key);
            this.RemoveAt(index);
            if (index >= 0)
            { return true; }
            else
            { return false; }
        }
        public bool RemoveAt(int index)
        {
            if (index == -1)
            {
                return false;
            }
            if (index >= 0 && index < keyCollection.Length)
            {
                TKey[] keyTemp = new TKey[keyCollection.Length - 1];
                TValue[] valueTemp = new TValue[valueCollection.Length - 1];
                Array.Copy(this.keyCollection, 0, keyTemp, 0, index - 1);
                Array.Copy(this.keyCollection, index + 1, keyTemp, index, keyTemp.Length);
                Array.Copy(this.valueCollection, 0, valueTemp, 0, index - 1);
                Array.Copy(this.valueCollection, index + 1, valueTemp, index, valueTemp.Length);
                this.keyCollection = keyTemp;
                this.valueCollection = valueTemp;
                return true;
            }
            else
                return false;
        }

        bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
           if (keyCollection.Contains(key))
            {
                value = this.valueCollection[Array.IndexOf<TKey>(keyCollection, key)];
                return true;
            }
            value = default;
            return false;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            TKey[] tempKey = new TKey[keyCollection.Length + 1];
            TValue[] tempValue = new TValue[valueCollection.Length + 1];
            Array.Copy(this.keyCollection, 0, tempKey, 0, keyCollection.Length);
            Array.Copy(this.valueCollection, 0, tempValue, 0, valueCollection.Length);
            tempKey[tempKey.Length - 1] = item.Key;
            tempValue[tempValue.Length - 1] = item.Value;
            this.keyCollection = tempKey;
            this.valueCollection = tempValue;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            this.keyCollection = new TKey[0];
            this.valueCollection = new TValue[0];
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            if (this.valueCollection[Array.IndexOf<TKey>(keyCollection, item.Key)].Equals( item.Value))
            {
                return true;
            }
            return false;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            int num = Count;
            TKey[] array1 = keyCollection;
            TValue[] array2 = valueCollection;
            for (int i = 0; i < num; i++)
            {
                if (array1[i] != null && array2!= null)
                {
                    array[arrayIndex++] = new KeyValuePair<TKey, TValue>(array1[i], array2[i]);
                }
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            int index = Array.IndexOf<TKey>(keyCollection, item.Key);
            this.RemoveAt(index);
            if (index >= 0)
            { return true; }
            else
            { return false; }
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }


        public IEnumerator<TValue> GetEnumerator()
        {
            return new MyEnumerato(this.valueCollection);
        }
        class MyEnumerato : IEnumerator<TValue>
        {
            TValue[] Collection;
            public MyEnumerato(TValue[] Col)
            {
                this.Collection = Col;
            }
            int position = -1;
            public TValue Current
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

        public bool Contains(object key)
        {
            throw new NotImplementedException();
        }

        public void Add(object key, object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnDeserialization(object sender)
        {
            throw new NotImplementedException();
        }

        /*
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
            temp[temp.Length-1] = item;
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
        */
    }
}
