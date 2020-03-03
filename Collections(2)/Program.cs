using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections_2_
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
            //list.Add((object)"str");//do not work
            MyList<int> myList = new MyList<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (int a in myList)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine(new string('-',30));
            var arr = myList.GetArray();
            foreach ( var a in arr)
                Console.WriteLine(a);
            Console.ReadLine();
        }

    }
    public static class Helper
    {
        public static T[] GetArray<T>(this IEnumerable<T> list)
        {
            int i = 0;
            T[] temp = new T[list.Count()];
            foreach (var a in list)
            {
                temp[i] = a;
                i++;
            }
            return temp;
        }
    }
}
