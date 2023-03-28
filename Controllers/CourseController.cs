using u21442453_HW01_API.Models;
using u21442453_HW01_API.ViewModel;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace u21442453_HW01_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _repo;

        public CourseController(ICourseRepository courseRepository)
        {
            _repo = courseRepository;
        }

        [HttpGet]
        [Route("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var results = await _repo.GetAllCourseAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetCourse/{courseID}")]
        public async Task<IActionResult> GetCourse(int courseID)
        {
            try
            {
                var res = await _repo.GetCourseAync(courseID);
                if (res == null) return NotFound("Course does not exist.");
                return Ok(res);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact suppurt");
            }
        }

        [HttpPost]
        [Route("AddCourse")]
        public async Task<IActionResult> AddCourse(CourseViewModel cvm)
        {
            var course = new Course
            {
                Name = cvm.Name,
                Duration = cvm.Duration,
                Description = cvm.Description
            };

            try
            {
                _repo.Add(course);
                if(await _repo.SaveChangesAsync())
                {
                    return Ok(course);
                }
                
            }
            catch (Exception)
            {
                return BadRequest("Invalid transaction");
            }

            return BadRequest("Your request is invalid.");
        }

        [HttpPut]
        [Route("EditCourse/{courseID}")]
        public async Task<ActionResult<CourseViewModel>> EditCourse(int courseID, CourseViewModel cvm)
        {
            try
            {
                var existingCourse = await _repo.GetCourseAync(courseID);
                if (existingCourse == null) return NotFound("The course does not exist.");

                existingCourse.Name = cvm.Name;
                existingCourse.Duration = cvm.Duration;
                existingCourse.Description = cvm.Description;

                if (await _repo.SaveChangesAsync())
                {
                    return Ok(existingCourse);  
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Wrror. Please contafct support.");
            }
            return BadRequest("Your request is invalid");
        }

        [HttpDelete]
        [Route("DeleteCourse/{courseID}")]
        public async Task<IActionResult> DeleteCourse(int courseID)
        {
            try
            {
                var existingCourse = await _repo.GetCourseAync(courseID);
                if (existingCourse == null) return NotFound("The customer does not exist");

                _repo.Delete(existingCourse);

                if(await _repo.SaveChangesAsync()) return Ok(existingCourse);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return BadRequest("Your request is invalid.");
        }
    }
}
