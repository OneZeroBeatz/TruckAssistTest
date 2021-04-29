using DataStructures.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class Tree
    {
        private readonly ParentChildNode[] _parentChildNodes;
        private readonly bool[] _visited;
        private readonly NestedNode[] _structuredNested;
        private static int visitCounter = 1;

        public Tree(ParentChildNode[] parentChildNodes)
        {
            _parentChildNodes = parentChildNodes;
            _visited = new bool[_parentChildNodes.Length + 1];
            _structuredNested = new NestedNode[_parentChildNodes.Length + 1];
        }

        public Tree DepthFirstSearch()
        {
            return Traverse(Root);
        }

        private Tree Traverse(ParentChildNode node)
        {
            var inProgressNodes = new Stack<ParentChildNode>();
            inProgressNodes.Push(node);
            _visited[node.Id] = true;

            var left = visitCounter++;

            while (inProgressNodes.Count != 0)
            {
                var children = GetChildrenFor(node);

                if (children.Length == 0)
                    _structuredNested[node.Id] = new NestedNode(node.Name, left, visitCounter++);

                foreach (var child in children)
                {
                    Traverse(child);

                    if (!AllChildrenVisited(node))
                        continue;

                    _structuredNested[node.Id] = new NestedNode(node.Name, left, visitCounter++);
                }

                inProgressNodes.Pop();
            }

            return this;
        }

        public IEnumerable<NestedNode> BuildHierarchicallyStructuredForm()
        {
            return _structuredNested
                        .Where(r => r != null)
                        .OrderBy(r => r.Name);
        }

        private bool AllChildrenVisited(ParentChildNode parentNode)
        {
            var children = GetChildrenFor(parentNode);
            return children.All(c => _visited[c.Id]);
        }

        private ParentChildNode[] GetChildrenFor(ParentChildNode parentNode)
        {
            var children = _parentChildNodes.Where(childNode => childNode.ParentId == parentNode.Id).ToArray();
            return children;
        }

        private ParentChildNode Root => _parentChildNodes.FirstOrDefault(node => node.IsRoot);
    }
}
