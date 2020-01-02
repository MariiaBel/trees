﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> tree = new AVLTree<int>();

            //tree.Add(1);
            //tree.Add(6);
            //tree.Add(2);
            //tree.Add(3);
            //tree.Add(4);
            //tree.Add(5);

            tree.Add(new int[] { 1, 2, 3, 9 });

            foreach (var item in tree)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(tree.Contains(9));
            tree.Remove(2);
            Console.WriteLine(tree.Contains(9));

            foreach (var item in tree)
            {
                Console.WriteLine(item);
            }

            tree.Clear();
            Console.ReadKey();
        }
    }
}
