using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAS_LMS.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Course Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [FutureDate(ErrorMessage = "Please enter a valid future date.")]
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd M/yy h:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        // d/M/yy h:mm tt dd MMMM yyyy

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Participants")]
        public virtual ICollection<ApplicationUser> CourseParticipants { get; set; }


        [Display(Name = "Modules")]
        public virtual ICollection<Module> CourseModules { get; set; }

    }
}