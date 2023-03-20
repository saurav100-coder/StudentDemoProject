using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSampleProject.Models;
using StudentSampleProject.Repository;

namespace StudentSampleProject.Controllers
{
    public class StudentController : Controller
    {
        // GET: StudentController
        public ActionResult GetAllStudents()
        {
            StuRepository stuRepository = new StuRepository();
            ModelState.Clear();
            return View(stuRepository.GetAllStudents());
        }
        // GET: Employee/AddEmployee    
        public ActionResult AddEmployee()
        {
            return View();
        }

        // POST: Employee/AddEmployee    
        [HttpPost]
        public ActionResult AddEmployee(StudentModel stu)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    StuRepository stuRepository = new StuRepository();
                    if(stuRepository.AddStudent(stu))
                    {
                        ViewBag.Message = "Student details added successfully";
                        return RedirectToAction("GetAllStudents");
                    }
                }
                return View();
            }
            catch
            {
            return View();
            }
        }
        public ActionResult EditStudentDetails(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            StuRepository stuRepository = new StuRepository();
            ModelState.Clear();
            var result = stuRepository.GetAllStudents().Find(Stu => Stu.StudId == id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // POST: Employee/AddEmployee    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudentDetails(int id, [Bind] StudentModel stu)
        {

            if (id != stu.StudId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                StuRepository stuRepository = new StuRepository();
                stuRepository.UpdateStudentDetails(stu);
                return RedirectToAction("GetAllStudents");
            }
            return View(stu);
        }
        public ActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            StuRepository stuRepository = new StuRepository();
            ModelState.Clear();
            var result = stuRepository.GetAllStudents().Find(Stu => Stu.StudId == id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            StuRepository stuRepository = new StuRepository();
            stuRepository.DeleteStudentDetails(id);
            return RedirectToAction("GetAllStudents");
        }
    }
}
