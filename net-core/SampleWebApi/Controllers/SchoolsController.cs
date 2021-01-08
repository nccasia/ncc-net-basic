using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebApi.Models;
using System;
using System.Threading.Tasks;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public SchoolsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<School>> Get(Guid id)
        {
            var school = await dbContext.Schools.FirstOrDefaultAsync(i => i.Id == id);
            if (school == null)
            {
                return NotFound($"School with id {id} not found");
            }

            return Ok(school);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<School>> Create([FromBody] School school)
        {
            if (school == null || string.IsNullOrWhiteSpace(school.Name))
            {
                return BadRequest("Invalid input");
            }

            school.Id = Guid.NewGuid();
            dbContext.Schools.Add(school);

            await dbContext.SaveChangesAsync();

            return Ok(school);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<School>> Update([FromBody] School school)
        {
            if (school == null || string.IsNullOrWhiteSpace(school.Name))
            {
                return BadRequest("Invalid input");
            }

            var existingSchool = await dbContext.Schools.FirstOrDefaultAsync(i => i.Id == school.Id);
            if (existingSchool == null)
            {
                return NotFound($"School with id {school.Id} not found");
            }

            existingSchool.Name = school.Name;

            await dbContext.SaveChangesAsync();

            return Ok(existingSchool);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<School>> Delete(Guid id)
        {
            var school = await dbContext.Schools.FirstOrDefaultAsync(i => i.Id == id);
            if (school == null)
            {
                return NotFound($"School with id {id} not found");
            }

            dbContext.Schools.Remove(school);

            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
