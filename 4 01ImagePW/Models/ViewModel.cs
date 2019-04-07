using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Library.Data;

namespace _4_01ImagePW.Models
{
    public class ViewModel
    { 
        //adding a blank comment just for the fun of it!
        public bool ViewImage { get; set; }
        public string Message { get; set; }
        public Images ImageCurrent { get; set; }
    }
}