﻿using System.Web;
using System.Web.Mvc;

namespace _4_01ImagePW
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
