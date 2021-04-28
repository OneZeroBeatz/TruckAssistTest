using DataStructures.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class Tree
    {
        private readonly ParentChildRecord[] _parentChildRecords;
        private readonly bool[] _visited;
        private readonly StructuredRecord[] _structuredRecords;
        private static int visitCounter = 1;

        public Tree(ParentChildRecord[] parentChildRecords)
        {
            _parentChildRecords = parentChildRecords;
            _visited = new bool[_parentChildRecords.Length + 1];
            _structuredRecords = new StructuredRecord[_parentChildRecords.Length + 1];
        }

        public Tree DepthFirstSearch()
        {
            return Traverse(Root);
        }

        private Tree Traverse(ParentChildRecord record)
        {
            var stackForDFS = new Stack<ParentChildRecord>();
            stackForDFS.Push(record);
            _visited[record.Id] = true;

            var left = visitCounter++;

            while (stackForDFS.Count != 0)
            {
                var children = GetChildrenFor(record);

                if (children.Length == 0)
                    _structuredRecords[record.Id] = new StructuredRecord(record.Name, left, visitCounter++);

                foreach (var child in children)
                {
                    Traverse(child);

                    if (!AllChildrenVisited(record))
                        continue;

                    _structuredRecords[record.Id] = new StructuredRecord(record.Name, left, visitCounter++);
                }

                stackForDFS.Pop();
            }

            return this;
        }

        public IEnumerable<StructuredRecord> BuildHierarchicallyStructuredForm()
        {
            return _structuredRecords
                        .Where(r => r != null)
                        .OrderBy(r => r.Name);
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
    }
}
