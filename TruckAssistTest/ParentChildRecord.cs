namespace DataStructures
{
    public class ParentChildRecord
    {
        public char Name { get; }
        public int Id { get; }
        public int? ParentId { get; }

        public ParentChildRecord(char name, int id, int? parentId)
        {
            Name = name;
            Id = id;
            ParentId = parentId;
        }

        public bool IsRoot => !ParentId.HasValue;
    }
}
