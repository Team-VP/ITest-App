using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Models.TestViewModels
{
    public class TestViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public int RequiredTime { get; set; }

        [DataType(DataType.Text)]
        public string Author { get; set; }

        [DataType(DataType.Text)]
        public string Category { get; set; }

        [DataType(DataType.Text)]
        public string Status { get; set; }
    }
}
