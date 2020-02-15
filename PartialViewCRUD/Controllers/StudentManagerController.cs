using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartialViewCRUD.Models;
using PartialViewCRUD.ViewModel; 
namespace PartialViewCRUD.Controllers
{
    public class StudentManagerController : Controller
    {
        StudentManagerEntities db = new StudentManagerEntities();  
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult ListStudent( int pageNumber , int pageSize, string search )
        {
            var data = (from s in db.Student
                        join c in db.Class on s.IDClass equals c.ID
                        select new ViewModelStudent
                        {
                            ID = s.ID,
                            Name = s.Name,
                            Birthday = s.Birthday,
                            Address = s.Address,
                            Age = s.Age,
                            ClassName = c.Name,
                            IDClass = c.ID
                        });
            if ( search.Trim() != "")
            {
                data = data.Where(x => x.Name.ToLower().Contains(search.ToLower()));  
            }
            var pageCount = data.Count() % pageSize == 0 ? data.Count() / pageSize : data.Count() / pageSize + 1;   
            if( pageNumber <= pageCount)
            {
                var model = data.OrderBy(x => x.Name).Skip(pageSize * pageNumber - pageSize).Take(pageSize).ToList();
                ViewBag.pageCount = pageCount;
                ViewBag.pageNumber = pageNumber; 
                return PartialView(model); 
            }
            return PartialView(data.OrderBy( x => x.Name)); 
        }

        public PartialViewResult FormCreateEdit()
        {
            ViewBag.ListClass = db.Class.ToList();  
            return PartialView(); 
        }

        [HttpPost]
        public JsonResult Create(  string name , string address  , int age , DateTime birth   , int idClass)
        {
            List<CheckError> checkErrors = CheckError(name, address, age, birth, idClass);  
            if( checkErrors.Count() == 0)
            {
                Student student = new Student { Name = name, Address = address, Age = age, Birthday = birth, IDClass = idClass };
                db.Student.Add(student);
                db.SaveChanges();
            }
           
            return Json(checkErrors); 
        }
        [HttpPost]
        public JsonResult Update(int id , string name, string address, int age, DateTime birth, int idClass)
        {
            var model = db.Student.Find(id);
            model.Name = name;
            model.Address = address;
            model.Age = age;
            model.Birthday = birth;
            model.IDClass = idClass;  
            db.SaveChanges();
            return Json(true);
        }
        public List<CheckError> CheckError(string name, string address, int age, DateTime birth, int idClass)
        {
            List<CheckError> checkErrors = new List<CheckError>();  
            if( name == null || name.Trim() == "")
            {
                CheckError check = new CheckError { nameInput = "name", error = "Tên không được để trống" };
                checkErrors.Add(check);  
            }
            if (address == null || address.Trim() == "")
            {
                CheckError check = new CheckError { nameInput = "address", error = "Địa chỉ không được để trống" };
                checkErrors.Add(check);
            }
            if (age == 0)
            {
                CheckError check = new CheckError { nameInput = "age", error = "Tuổi không được để trống" };
                checkErrors.Add(check);
            }
            if (birth == null || birth == DateTime.MinValue)
            {
                CheckError check = new CheckError { nameInput = "birth", error = "Ngày sinh không được để trống" };
                checkErrors.Add(check);
            }
            return checkErrors;  
        }

        [HttpPost]
        public JsonResult AddOrEdit( int id , string name, string address, int age, DateTime birth, int idClass)
        {
            if( id == 0)
            {
                Student student = new Student { Name = name, Address = address, Age = age, Birthday = birth, IDClass = idClass };
                db.Student.Add(student);
                db.SaveChanges();
                return Json(true);

            }
            else
            {
                var model = db.Student.Find(id);
                model.Name = name;
                model.Address = address;
                model.Age = age;
                model.Birthday = birth;
                model.IDClass = idClass;
                db.SaveChanges();
                return Json(true);

            }
           
        }
    }
}