namespace DataStructures.Models
{
    public class StructuredRecord
    {
        public char Name { get; }
        public int Lft { get; }
        public int Rgt { get; }

        public StructuredRecord(char name, int lft, int rgt)
        {
            Name = name;
            Lft = lft;
            Rgt = rgt;
        }
    }
}
