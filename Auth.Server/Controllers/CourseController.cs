using Auth.Server.Data;
using Auth.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Server.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly UserContext _userContext;

        public CourseController(UserContext userContext)
        {
            _userContext = userContext;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _userContext.Courses.ToListAsync();
            return Ok(courses);
        }

        // GET: api/courses/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _userContext.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        // POST: api/courses
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userContext.Courses.Add(course);
            await _userContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        // PUT: api/courses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _userContext.Entry(course).State = EntityState.Modified;

            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/courses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _userContext.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _userContext.Courses.Remove(course);
            await _userContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _userContext.Courses.Any(e => e.Id == id);
        }
    }
}
