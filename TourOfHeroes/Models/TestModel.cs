using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourOfHeroes.Models
{
    public class TestModel
    {
        [Range(0, 10)]
        public int id { get; set; }
        public string name { get; set; }
    }
}