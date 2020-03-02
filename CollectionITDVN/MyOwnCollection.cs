using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace CollectionITDVN
{
    public class MyOwnCollection: IEnumerable
    {
        int[] Collection;

        public IEnumerator GetEnumerator()
        {
            //for (int i = 0; i < Collection.Length; i++)
            //{
            //    yield return Collection[i];
            //}
            yield return Collection[0];
            yield return Collection[1];
            yield return Collection[2];
            yield return Collection[3];
            yield return Collection[4];
           
        }

        public int[] IsEven(int[] arr)
        {
            var temp = from x in arr
                         where x % 2 == 0
                         select x;
            Collection = new int[temp.Count()];
            for (int i=0; i<Collection.Length;i++)
            {
                Collection[i] = temp.ElementAt(i);
            }
            return Collection;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
         }
    }
}
