namespace DataStructures.Models
{
    public class ParentChildNode
    {
        public char Name { get; }
        public int Id { get; }
        public int? ParentId { get; }

        public ParentChildNode(char name, int id, int? parentId)
        {
            Name = name;
            Id = id;
            ParentId = parentId;
        }

        public bool IsRoot => !ParentId.HasValue;
    }
}
