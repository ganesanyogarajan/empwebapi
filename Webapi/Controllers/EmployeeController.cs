using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Webapi.EF;


namespace Webapi.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeeDBEntities empDb = new EmployeeDBEntities();
        [HttpGet]
        [Route("api/employee/getall")]
        public HttpResponseMessage getAllEmpDetails()
        {
            var emplist = empDb.EmployeeMasters.Where(emp=>emp.IsActive==1).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, emplist);
        }
        [HttpPost]
        [Route("api/employee/addnew")]
        public HttpResponseMessage insertnewemp(EmployeeMaster emp)
        {
            emp.IsActive = 1;
            empDb.EmployeeMasters.Add(emp);
            empDb.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK,"Success");
        }
        [HttpGet]
        [Route("api/employee/getempdetails/{id}")]
        public HttpResponseMessage getempdetails(int id)
        {
            var empdetails = empDb.EmployeeMasters.Where(emp => emp.EmpId == id).FirstOrDefault();
            if(empdetails!=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, empdetails);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            
        }
        [HttpPost]
        [Route("api/employee/updateemp/{id}")]
        public HttpResponseMessage updateEmp(EmployeeMaster emp, int id)
        {
        var empdetails = empDb.EmployeeMasters.Where(employee => employee.EmpId == id).FirstOrDefault();
            if (empdetails != null)
            {
                empdetails.Name = emp.Name;
                empdetails.Address = emp.Address;
                empdetails.Role=emp.Role;
                empdetails.Department=emp.Department;
                empdetails.SkillSets=emp.SkillSets;
                empdetails.Date_of_Birth=emp.Date_of_Birth;
                empdetails.Date_of_Joining=emp.Date_of_Joining;
                empDb.Entry(empdetails).State = System.Data.Entity.EntityState.Modified;
                empDb.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, empdetails);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
        [HttpGet]
        [Route("api/employee/deleteemp/{id}")]
        public HttpResponseMessage DeleteEmp(int id)
        {
            var empdetails = empDb.EmployeeMasters.Where(employee => employee.EmpId == id).FirstOrDefault();
            if (empdetails != null)
            {
                empdetails.IsActive = 0;
                empDb.Entry(empdetails).State = System.Data.Entity.EntityState.Modified;
                empDb.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK,"Success");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
