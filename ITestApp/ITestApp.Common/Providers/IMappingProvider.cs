﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITestApp.Common.Providers
{
    public interface IMappingProvider
    {
        TDestination MapTo<TDestination>(object source);

        IQueryable<TDestination> ProjectTo<TDestination>(IQueryable<object> source);

        IEnumerable<TDestination> ProjectTo<TDestination>(IEnumerable<object> source);
    }
}
