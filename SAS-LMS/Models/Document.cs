using System;
using System.ComponentModel.DataAnnotations;

namespace SAS_LMS.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name of the document is required ")]
        [Display(Name = "Document Name")]
        public string DocName { get; set; }

        [Required(ErrorMessage = "Description of the Document is required")]
        [Display(Name = "Document Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a Document")]
        [Display(Name = "Document")]
        public byte[] file { get; set; }


        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        [EmailAddress]
        public string CreatedBy { get; set; }

        [Display(Name = "Course Id")]
        public int? CourseId { get; set; }

        [Display(Name = "Module Id")]
        public int? ModuleId { get; set; }

        [Display(Name = "Activity Id")]
        public int? ActivityId { get; set; }

        [Display(Name = "End Date for Submission")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? SubmitBy { get; set; }

    }
}