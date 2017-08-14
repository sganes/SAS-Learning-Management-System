using System;
using System.ComponentModel.DataAnnotations;

namespace SAS_LMS.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Activity Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }

        public int ModuleId { get; set; }

        public virtual Module Module { get; set; }

        public virtual ActivityType ActivityType { get; set; }

    }


}