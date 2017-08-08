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

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Participants")]
        public virtual ICollection<ApplicationUser> CourseParticipants { get; set; }


        [Display(Name = "Modules")]
        public virtual ICollection<Module> CourseModules { get; set; }

        public bool EndCourse { get; set; }

    }
}