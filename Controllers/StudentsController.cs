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
        public ActionResult<List<Student>> Get() => _studentsService.Get();

        [HttpGet("{id:length(24)}", Name = "GetStudent")]
        public ActionResult<Student> Get(string id)
        {
            Student student = _studentsService.Get(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpPost]
        public ActionResult<Student> Create(Student student)
        {
            _studentsService.Create(student);

            return CreatedAtRoute("GetStudent", new { id = student.Id.ToString() }, student);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Student studentIn)
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

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            Student student = _studentsService.Get(id);

            if (student == null)
            {
                return NotFound();
            }

            _studentsService.Remove(student.Id);

            return NoContent();
        }
    }
}