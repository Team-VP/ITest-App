using System.ComponentModel.DataAnnotations;

namespace ITestApp.Web.Models.TakeTestViewModels
{
    public class AnswerViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(500)]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        //public bool IsCorrect { get; set; }
    }
}
