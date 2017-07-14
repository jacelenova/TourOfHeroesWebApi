using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using TourOfHeroes.Models;
using System.Data.Entity.SqlServer;

namespace TourOfHeroes.Controllers
{
    public class HeroesController : ApiController
    {
        private TourOfHeroesContext db = new TourOfHeroesContext();

        // GET: api/Heroes
        public IQueryable<Hero> GetHeroes(string searchTerm = "")
        {
            if (String.IsNullOrEmpty(searchTerm))
            {
                return db.Heroes;
            } else
            {
                var filteredHeroes = db.Heroes.Where(hero => hero.name.Contains(searchTerm));
                return filteredHeroes;
            }
            
        }

        // GET: api/Heroes/5
        [ResponseType(typeof(Hero))]
        public IHttpActionResult GetHero(int id)
        {
            Hero hero = db.Heroes.Find(id);
            if (hero == null)
            {
                return NotFound();
            }

            return Ok(hero);
        }

        // PUT: api/Heroes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHero(int id, Hero hero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hero.id)
            {
                return BadRequest();
            }

            db.Entry(hero).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Heroes
        [ResponseType(typeof(Hero))]
        public IHttpActionResult PostHero(Hero hero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Heroes.Add(hero);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hero.id }, hero);
        }

        // DELETE: api/Heroes/5
        [ResponseType(typeof(Hero))]
        public IHttpActionResult DeleteHero(int id)
        {
            Hero hero = db.Heroes.Find(id);
            if (hero == null)
            {
                return NotFound();
            }

            db.Heroes.Remove(hero);
            db.SaveChanges();

            return Ok(hero);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HeroExists(int id)
        {
            return db.Heroes.Count(e => e.id == id) > 0;
        }
    }
}