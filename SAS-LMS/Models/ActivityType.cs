using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAS_LMS.Models
{
    public class ActivityType
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Activity Type")]
        public string Name { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}