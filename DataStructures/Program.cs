using DataStructures.Models;
using System;

namespace DataStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var parentChildArray = new ParentChildNode[]
            {
                new ParentChildNode('a', 1, null),
                new ParentChildNode('b', 2, 1),
                new ParentChildNode('c', 3, 1),
                new ParentChildNode('d', 4, 2),
                new ParentChildNode('e', 5, 3),
                new ParentChildNode('f', 6, 3),
                new ParentChildNode('g', 7, 2),
                new ParentChildNode('i', 8, 4),
                new ParentChildNode('j', 9, 8)
            };

            Tree graph = new Tree(parentChildArray);

            var hierarchicallyStructuredRecords = graph
                                                    .DepthFirstSearch()
                                                    .BuildHierarchicallyStructuredForm();

            Console.WriteLine("Name,\tlft,\trgt,");

            foreach (var record in hierarchicallyStructuredRecords)
            {
                Console.WriteLine($"{record.Name},\t{record.Lft},\t{record.Rgt},");
            }

            Console.ReadLine();
        }
    }
}
