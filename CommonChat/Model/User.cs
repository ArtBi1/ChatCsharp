using System.ComponentModel.DataAnnotations;

namespace CommonChat.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
        public virtual ICollection<Message> FromMessages { get; set; }
        public virtual ICollection<Message> ToMessages { get; set; }
    }
}