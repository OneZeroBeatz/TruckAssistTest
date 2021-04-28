using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TruckAssistTest
{
    public class Tree
    {
        public readonly ParentChildRecord[] _parentChildRecords;
        public readonly bool[] _visited;

        public Tree(ParentChildRecord[] parentChildRecords)
        {
            _parentChildRecords = parentChildRecords;
            _visited = new bool[_parentChildRecords.Length + 1];
        }

        private void Traverse(ParentChildRecord record)
        {
            var stackForDFS = new Stack<ParentChildRecord>();
            stackForDFS.Push(record);
            _visited[record.Id] = true;
            Console.WriteLine($"Visited {record.Name}.");

            while (stackForDFS.Count != 0)
            {
                var children = GetChildrenFor(record);

                foreach (var child in children)
                {
                    Traverse(child);
                }

                stackForDFS.Pop();
            }
        }


        private ParentChildRecord[] GetChildrenFor(ParentChildRecord parent)
        {
            var children = _parentChildRecords.Where(record => record.ParentId == parent.Id).ToArray();
            return children;
        }

        private ParentChildRecord Root => _parentChildRecords.FirstOrDefault(record => record.IsRoot);
    
        public void DepthFirstSearch()
        {
            Console.WriteLine("DFS");
            Traverse(Root);
        }
    }
}
