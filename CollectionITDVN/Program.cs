using System;

namespace CollectionITDVN
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var col = new MyOwnCollection();
            col.IsEven(arr);
            foreach (int a in col)
            {
                Console.WriteLine(a+" ");
            }
            Console.WriteLine(new string('-',20));
            foreach (int a in col)
            {
                Console.WriteLine(a + " ");
            }
            Console.ReadLine();

        }
    }
}
