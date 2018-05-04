using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ITestApp.Web.Models.DashboardViewModels;

namespace ITestApp.Web.Models.TakeTestViewModels
{
    public class IndexViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int TestId { get; set; } // Id
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string TestName { get; set; }

        public TimeSpan Duration { get; set; }

        public TimeSpan TimeLeft { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string CategoryName { get; set; } // not from Test
        [Required]
        public IList<QuestionViewModel> Questions { get; set; }
        [Required]
        public DateTime StartedOn { get; set; }

        public DateTime SubmitedOn { get; set; }

    }
}
