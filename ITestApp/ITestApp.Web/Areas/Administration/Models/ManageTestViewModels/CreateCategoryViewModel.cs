﻿using ITestApp.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITestApp.Web.Areas.Administration.Models.MangeTestsViewModels
{
    public class CreateCategoryViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}
