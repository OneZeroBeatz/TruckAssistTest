namespace DataStructures.Models
{
    public class NestedNode
    {
        public char Name { get; }
        public int Lft { get; }
        public int Rgt { get; }

        public NestedNode(char name, int lft, int rgt)
        {
            Name = name;
            Lft = lft;
            Rgt = rgt;
        }
    }
}
