using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TasksEvaluation.Core;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Core.Interfaces.IServices;
using TasksEvaluation.Web.ViewModels;

namespace TasksEvaluation.Web.Controllers
{
	public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private readonly IUnitOfWork _unitOfWork;
        public StudentController(IStudentService studentService , IUnitOfWork unitOfWork ,IGroupService groupService)
        {
            _studentService = studentService;
            _unitOfWork = unitOfWork;
            _groupService = groupService;
                
        }
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetStudentsIncludeGroups();
            return View(students);
        }


        public ActionResult Create()
        {
            var selectedGroup = new GroupDto() { Id = 0, Title = "Select Group" };
            var allGroups = _groupService.GetAll().Result.ToList();
            allGroups.Add(selectedGroup);

            ViewBag.GroupId = new SelectList(allGroups, nameof(Group.Id), nameof(Group.Title)
                , allGroups.Find(c => c.Id == 0).Id);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateViewModel studentVM)
        {
			if (ModelState.IsValid)
            {
				var student = new StudentDto
				{
					FullName = studentVM.FullName,
					MobileNumber = studentVM.MobileNumber,
					Email = studentVM.Email,
					GroupId = studentVM.GroupId,

				};

				if (studentVM.ProfilePic != null)
				{
					string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
					string fileName = Guid.NewGuid() + Path.GetExtension(studentVM.ProfilePic.FileName);
					string filePath = Path.Combine(uploadsFolder, fileName);


					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await studentVM.ProfilePic.CopyToAsync(fileStream);
					}

					student.ProfilePic = @$"/uploads/{fileName}" ;
				}

				await _studentService.Create(student);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(GetAllStudents));
            }

            return View(studentVM);
        }
        public async Task<IActionResult> Edit(int? id)
        
        {

            if (id is null || !_studentService.Any().Result)
                return NotFound();
            var student = await _studentService.GetById(id.Value);
            var studentVM = new StudentCreateViewModel
            {
                Id = student.Id,
                FullName = student.FullName ,
                Email = student.Email ,
                MobileNumber = student.MobileNumber ,
                GroupId = student.GroupId ,
                
            };
           
            string fullPicPath = @$"wwwroot{student.ProfilePic}";
			using (var fileStream = new FileStream(fullPicPath, FileMode.Open))
			{
				studentVM.ProfilePic = new FormFile(
					baseStream: fileStream,
					baseStreamOffset: 0,
					length: fileStream.Length,
					name: nameof(student.ProfilePic),
					fileName: Path.GetFileName(student.ProfilePic)
				);
				 

			}


			var allGroups = _groupService.GetAll().Result.ToList();
			ViewBag.GroupId = new SelectList(allGroups, nameof(Group.Id), nameof(Group.Title)
				, student.GroupId);
			return View(studentVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(StudentCreateViewModel studentVM)
        {
            if (studentVM is null)
                return NotFound();


            if (ModelState.IsValid)
            {

				var student = new StudentDto
				{
                    Id = studentVM.Id ,
					FullName = studentVM.FullName,
					MobileNumber = studentVM.MobileNumber,
					Email = studentVM.Email,
					GroupId = studentVM.GroupId,

				};
				string picFileName = studentVM.ProfilePic.FileName;
				string uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
				string picPath = Path.Combine(uploadsFolderPath, studentVM.ProfilePic.FileName);

				if ( !System.IO.File.Exists(picPath))
                {
					// string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
					picFileName = Guid.NewGuid() + Path.GetExtension(studentVM.ProfilePic.FileName);
					string newPicPath = Path.Combine(uploadsFolderPath, picFileName);


					using (var fileStream = new FileStream(newPicPath, FileMode.Create))
					{
						await studentVM.ProfilePic.CopyToAsync(fileStream);
					}


				}
				student.ProfilePic = @$"/uploads/{picFileName}";


				try
				{
                    _studentService.Update(student);

                    await _unitOfWork.Complete();
                }
                catch (DBConcurrencyException)
                {
                    if (!_studentService.Contains(student).Result)
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(GetAllStudents));
            }

            return View(studentVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || !_studentService.Any().Result)
                return NotFound();
            var student = await _studentService.GetById(id.Value);
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if ( !_studentService.Any().Result)
                return NotFound();

            _studentService.Delete(id);
			await _unitOfWork.Complete();
			return RedirectToAction(nameof(GetAllStudents));
        }

        public IActionResult LoginAsStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginAsStudent( LoginAsStudentVM studentVM)
        {
            if(ModelState.IsValid)
            {
                if (! _studentService.Contains(s => s.Email == studentVM.Email).Result)
                      return NotFound("Student Email Not Found!!!");
				return RedirectToAction(nameof(AssignmentController.GetAllAssignments), "Assignment");

			}
			return View(studentVM);
        }


    }
}
