using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TourOfHeroes.Models
{
    public class TourOfHeroesContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public TourOfHeroesContext() : base("name=TourOfHeroesContext")
        {
        }

        public System.Data.Entity.DbSet<TourOfHeroes.Models.Hero> Heroes { get; set; }
        public DbSet<TourOfHeroes.Models.TestModel> TestModels { get; set; }
        public DbSet<TourOfHeroes.Models.User> Users { get; set; }
    }
}
