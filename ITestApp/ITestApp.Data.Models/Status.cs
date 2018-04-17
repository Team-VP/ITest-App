using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITestApp.Data.Models.Abstracts;

namespace ITestApp.Data.Models
{
    public class Status : BaseEntity
    {
        public Status()
        {
            Tests = new HashSet<Test>();
        }

        [Required]
        public string Name { get; set; }

        public ICollection<Test> Tests { get; set; }
    }
}