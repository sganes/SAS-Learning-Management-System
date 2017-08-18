using System.ComponentModel.DataAnnotations;

namespace SAS_LMS.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Document Name")]
        public string DocName { get; set; }

        [Display(Name = "Document Description")]
        public string Description { get; set; }

        [Display(Name = "Course Id")]
        public int? CourseId { get; set; }

        [Display(Name = "Module Id")]
        public int? ModuleId { get; set; }

        [Display(Name = "Activity Id")]
        public int? ActivityId { get; set; }
    }
}