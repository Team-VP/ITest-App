using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ITestApp.Data.Models.Abstracts;

namespace ITestApp.Data.Models
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Tests = new HashSet<Test>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<Test> Tests { get; set; }
    }
}
