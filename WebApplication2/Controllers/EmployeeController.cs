using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly WebAppContext _db;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env, WebAppContext db)
        {
            _configuration = configuration;
            _db = db;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_db.Employees);
        }


        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            _db.Employees.Add(emp);
            _db.SaveChanges();
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            var query = _db.Employees.Find(emp.EmployeeId);
            if (query == null)
                return new JsonResult("Employee not found.");

            query.EmployeeName = emp.EmployeeName;
            query.Department = emp.Department;
            query.DateOfJoining = emp.DateOfJoining;
            query.PhotoFileName = emp.PhotoFileName;

            _db.SaveChanges();
            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var query = _db.Employees.Find(id);
            if (query == null)
                return new JsonResult("Employee not found.");

            _db.Employees.Remove(query);

            _db.SaveChanges();
            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename= postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using(var stream=new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);

            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
    }
}
