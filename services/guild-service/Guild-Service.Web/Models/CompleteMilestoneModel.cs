using System.ComponentModel.DataAnnotations;

namespace UserService.Web.Models
{
    public class CompleteMilestoneModel
    {
        [Required]
        public int MilestoneId { get; set; }
        
        [Required]
        public int UserId { get; set; }
    }
}