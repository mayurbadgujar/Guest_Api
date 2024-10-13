using System.ComponentModel.DataAnnotations;

namespace Guest_Api.Model
{
    public enum Title
    {
        Mr,
        Ms,
        Mrs,
        Dr
    }

    public class Guest
    {
        public Guid Id { get; set; }
        public Title Title { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public List<string> PhoneNumbers { get; set; }
    }
}
