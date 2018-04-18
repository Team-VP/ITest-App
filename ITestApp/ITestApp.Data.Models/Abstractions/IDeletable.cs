using System;
using System.Collections.Generic;
using System.Text;

namespace ITestApp.Data.Models.Abstracts
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
