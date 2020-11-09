using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Models;
using StudentsAPI.Services;

namespace StudentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsService _studentsService;

        public StudentsController(StudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        [HttpGet]
        public ActionResult<List<Student>> Get()
        {
            try
            {
                return _studentsService.Get();
            }
            catch (System.Exception)
            {
                return Problem(statusCode: 500);
            }
        }

        [HttpGet("{id:length(24)}", Name = "GetStudent")]
        public ActionResult<Student> Get(string id)
        {
            try
            {
                Student student = _studentsService.Get(id);

                if (student == null)
                {
                    return NotFound();
                }

                return student;
            }
            catch (System.Exception)
            {
                return Problem(statusCode: 500);
            }
        }

        [HttpPost]
        public ActionResult<Student> Create(Student student)
        {
            try
            {
                _studentsService.Create(student);

                return CreatedAtRoute("GetStudent", new { id = student.Id.ToString() }, student);
            }
            catch (System.Exception)
            {
                return Problem(statusCode: 500);
            }
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Student studentIn)
        {
            try
            {
                Student student = _studentsService.Get(id);

                if (student == null)
                {
                    return NotFound();
                }

                studentIn.Id = id;
                _studentsService.Update(id, studentIn);

                return NoContent();
            }
            catch (System.Exception)
            {
                return Problem(statusCode: 500);
            }
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            try
            {
                Student student = _studentsService.Get(id);

                if (student == null)
                {
                    return NotFound();
                }

                _studentsService.Remove(student.Id);

                return NoContent();
            }
            catch (System.Exception)
            {
                return Problem(statusCode: 500);
            }
        }
    }
}