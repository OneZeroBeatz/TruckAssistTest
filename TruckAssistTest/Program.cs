using DataStructures;
using System;

namespace TruckAssistTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parentChildArray = new ParentChildRecord[]
            {
                new ParentChildRecord('a', 1, null),
                new ParentChildRecord('b', 2, 1),
                new ParentChildRecord('c', 3, 1),
                new ParentChildRecord('d', 4, 2),
                new ParentChildRecord('e', 5, 3),
                new ParentChildRecord('f', 6, 3),
                new ParentChildRecord('g', 7, 2),
                new ParentChildRecord('i', 8, 4),
                new ParentChildRecord('j', 9, 8)           
            };

            Tree graph = new Tree(parentChildArray);

            graph.DepthFirstSearch();

            Console.ReadLine();
        }
    }
}
