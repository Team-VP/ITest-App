using ITestApp.Data.Models.Abstracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITestApp.Data.Models
{
    public class User : IdentityUser, IAuditable, IDeletable
    {
        public User()
        {
            Tests = new HashSet<Test>();
            UserTests = new HashSet<UserTest>();
        }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Test> Tests { get; set; }

        public ICollection<UserTest> UserTests { get; set; }
    }
}
