using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Models.TestViewModels
{
    public class CreateTestViewModel
    {
        public IEnumerable<PostCategoryViewModel> Categories { get; set; }
        public PostTestViewModel Test { get; set; }
    }
}
