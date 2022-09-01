using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly WebAppContext _db;
        public DepartmentController(IConfiguration configuration, WebAppContext db)
        {
            _configuration = configuration;
            _db = db;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_db.Department);
        }


        [HttpPost]
        public JsonResult Post(Department dep)
        {
            _db.Department.Add(dep);
            _db.SaveChanges();
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            var query = _db.Department.Find(dep.DepartmentId);
            if (query == null)
                return new JsonResult("Department not found.");

            query.DepartmentName = dep.DepartmentName;

             _db.SaveChanges();
            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var query = _db.Department.Find(id);
            if (query == null)
                return new JsonResult("Department not found.");

            _db.Department.Remove(query);

            _db.SaveChanges();
            return new JsonResult("Deleted Successfully");
        }
    }
}
