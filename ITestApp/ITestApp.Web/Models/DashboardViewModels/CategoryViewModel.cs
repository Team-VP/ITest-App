using ITestApp.Common.Constants;
using ITestApp.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Models.DashboardViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.MinTestCategoryLength)]
        [MaxLength(ModelConstants.MaxTestCategoryLength)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public ICollection<TestViewModel> Tests { get; set; }
    }
}
