using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using DataWixard_WebAPI.Models;

namespace DataWixard_WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        private EmployeeDB_CRUDEntities2 empEntity = new EmployeeDB_CRUDEntities2();
       
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetEmployee()
        {
            try
            {
                List<EmpTable> empList = empEntity.EmpTables.ToList();
                return Ok(empList);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult AddUpdateEmployee(EmpTable emp)
        {
            string message = "Data Successfully Updated";
            try
            {
                EmpTable empNew = empEntity.EmpTables.SingleOrDefault(model => model.EmpId == emp.EmpId) ?? new EmpTable();
                empNew.EmpId = emp.EmpId;
                empNew.Name = emp.Name;
                empNew.Address = emp.Address;
                empNew.PhoneNumber = emp.PhoneNumber;

                if (emp.EmpId == 0)
                {
                    empEntity.EmpTables.Add(empNew);
                    message = "Employee Successfully Added";
                }
                empEntity.SaveChanges();
                return Ok(message);
            }
            catch(Exception e)
            {
                throw (e);
            }
    
        }

        [System.Web.Mvc.HttpPost]
        public IHttpActionResult EditEmployee(int id)
        {
            try
            {
                EmpTable emp = empEntity.EmpTables.Find(id);
                if (emp != null)
                {
                    empEntity.Entry(emp).State = EntityState.Modified;
                    empEntity.SaveChanges();
                    return (Ok(emp));
                }
                else
                {
                    return Ok("Updation Failed");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

      

        [System.Web.Http.HttpPost]
        public IHttpActionResult DeleteEmployee(int id)
        {
            EmpTable emp = empEntity.EmpTables.Find(id);
            if (emp != null)
            {
                empEntity.EmpTables.Remove(emp);
                empEntity.SaveChanges();
                return Ok("Successfully Deleted");
            }
            else
            {
                 return Ok("Delete Unsuccessful");
            }
        }
    }
}
