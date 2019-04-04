using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Data;

namespace _4_01ImagePW.Models
{
    public class ViewModel
    { 

        public bool ViewImage { get; set; }
        public string Message { get; set; }
        public Images ImageCurrent { get; set; }
    }
}