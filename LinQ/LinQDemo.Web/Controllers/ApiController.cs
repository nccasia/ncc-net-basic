using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinQDemo.Common;
using LinQDemo.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinQDemo.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IStudentService studentService;
        private readonly ILinQToXmlService linQToXmlService;

        public ApiController(IStudentService studentService, ILinQToXmlService linQToXmlService)
        {
            this.studentService = studentService;
            this.linQToXmlService = linQToXmlService;
        }

        [HttpGet("getall")]
        public ActionResult GetAll()
        {
            return Ok(studentService.GetAll());
        }

        [HttpGet("getbyclass")]
        public ActionResult GetByClass(long classId)
        {
            return Ok(studentService.GetByClass(classId));
        }

        [HttpGet("getbygrade")]
        public ActionResult GetByGrade(int grade)
        {
            return Ok(studentService.GetByGrade(grade));
        }

        [HttpGet("test")]
        public ActionResult Test()
        {
            var testResult =
                from s in studentService.GetAll()
                where s.DateOfBirth.Year < 2014
                select s.FullName;

            return Ok(testResult);
        }

        [HttpGet("testxml")]
        public ActionResult TestXml()
        {
            return Ok(linQToXmlService.Test());
        }
    }
}
