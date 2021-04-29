using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Context
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("GroupId")]
        public ICollection<User> Students { get; set; }
    }
}
