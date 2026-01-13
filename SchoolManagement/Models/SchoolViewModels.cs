using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models 
{
    public class School {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Principal { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
    }

    public class Student {
        [Key]
        public int Id { get; set; }
        public int SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School? School { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Phone { get; set; }
    }

    // Thêm đoạn này để hết lỗi build
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}