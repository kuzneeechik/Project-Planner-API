using System.ComponentModel.DataAnnotations;

namespace Project_Planner_API.Models.StudentModels
{
    public class LogInModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [MinLength(8)]
        [RegularExpression(@"(.*[a-zA-Z]+.*\d+.*)|(.*\d+.*[a-zA-Z]+.*)")]
        public required string Password { get; set; }
    }
}
