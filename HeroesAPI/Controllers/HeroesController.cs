using HeroesAPI.Data;
using HeroesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace HeroesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesController : Controller
    {
        private readonly HeroesAPIDbContext dbcontext;

        public HeroesController(HeroesAPIDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> GetHeroes()
        {
            return Ok(await dbcontext.Heroes.ToListAsync());
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetHero([FromRoute] Guid id)
        {
            var hero = await dbcontext.Heroes.FindAsync(id);

            if(hero==null)
            { return null; }
            return Ok(hero);
        }
        [HttpPost]
        public async Task<IActionResult> AddHero(AddHero addHeroes)
        {
            var hero = new Hero()
            {
                id = Guid.NewGuid(),
                name = addHeroes.name,
                description = addHeroes.description,
            };
           await dbcontext.Heroes.AddAsync(hero);
            await dbcontext.SaveChangesAsync();

            return Ok(hero);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateHero([FromRoute] Guid id,UpdateHero updateHero)
        {
            var hero = await dbcontext.Heroes.FindAsync(id);
            if (hero != null)
            {
                hero.name = updateHero.name;
                hero.description = updateHero.description;

                await dbcontext.SaveChangesAsync();
                return Ok(hero);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteHero(Guid id)
        {
            var hero = await dbcontext.Heroes.FindAsync(id);
            if (hero != null)
            {
                dbcontext.Heroes.Remove(hero);
                await dbcontext.SaveChangesAsync();
                return Ok(hero);
            }
            return NotFound();
        }
    }
}
