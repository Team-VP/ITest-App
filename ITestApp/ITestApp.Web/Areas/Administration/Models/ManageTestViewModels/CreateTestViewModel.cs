using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITestApp.Web.Areas.Administration.Models.MangeTestsViewModels
{
    public class CreateTestViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Range(1, 1000, ErrorMessage = "Time must be positive value, between 1 and 1000 minutes")]
        public int RequiredTime { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.Text)]
        public string Author { get; set; }

        [DataType(DataType.Text)]
        public string Category { get; set; }

        [DataType(DataType.Text)]
        public string Status { get; set; }

        public ICollection<CreateQuestionViewModel> Questions { get; set; }
    }
}
