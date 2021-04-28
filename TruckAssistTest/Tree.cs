using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TruckAssistTest
{
    public class Tree
    {
        private readonly ParentChildRecord[] _parentChildRecords;
        private readonly bool[] _visited;
        private static int visitCounter = 1;

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

            var left = visitCounter++;
            Console.WriteLine($"[{record.Name}] - left: {left}");
            var right = 0;

            while (stackForDFS.Count != 0)
            {
                var children = GetChildrenFor(record);
                if (children.Length == 0) {
                    right = visitCounter++;
                    Console.WriteLine($"[{record.Name}] - right: {right}");
                }

                foreach (var child in children)
                {
                    Traverse(child);

                    if (!AllChildrenVisited(record))
                        continue;

                    right = visitCounter++;
                    Console.WriteLine($"[{record.Name}] - right: {right}");
                }

                stackForDFS.Pop();
            }

        }

        private bool AllChildrenVisited(ParentChildRecord parent)
        {
            var children = GetChildrenFor(parent);
            return children.All(c => _visited[c.Id]);
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
