using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentsMangement.Data;
using StudentsMangement.Models;

namespace StudentsMangement.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly StudentDBContext _context;

        public StudentController(StudentDBContext context)
        {
            _context = context;
        }




        public  IActionResult Index(string search, string SortOrder, string SortBy, int PageNumber = 1)
        {
            ViewData["Searching"] = search;
            IEnumerable<StudentDetails> obj = _context.StudentDetails.Where(value => value.FirstName.Contains(search) ||
                value.LastName.Contains(search) ||
                value.Email.Contains(search) ||
                value.PhoneNumber.Contains(search) ||
                search == null).ToList();


            ViewBag.Totalpages = Math.Ceiling(obj.Count() / 5.0);
            ViewBag.PageNumber = PageNumber;
            obj = obj.Skip((PageNumber - 1) * 5).Take(5).ToList();


            ViewBag.SortOrder = SortOrder;
            switch (SortBy)
            {
                case "FirstName":
                    {

                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    obj = obj.OrderBy(x => x.FirstName).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    obj = obj.OrderByDescending(x => x.FirstName).ToList();
                                    break;
                                }
                            default:
                                {
                                    obj = obj.OrderBy(x => x.FirstName).ToList();
                                    break;
                                }

                        }

                        break;
                    }

                case "LastName":
                    {

                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    obj = obj.OrderBy(x => x.LastName).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    obj = obj.OrderByDescending(x => x.LastName).ToList();
                                    break;
                                }
                            default:
                                {
                                    obj = obj.OrderBy(x => x.LastName).ToList();
                                    break;
                                }

                        }
                        break;
                    }
                case "Email":
                    {

                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    obj = obj.OrderBy(x => x.Email).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    obj = obj.OrderByDescending(x => x.Email).ToList();
                                    break;
                                }
                            default:
                                {
                                    obj = obj.OrderBy(x => x.Email).ToList();
                                    break;
                                }

                        }
                        break;
                    }
                case "PhoneNumber":
                    {

                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    obj = obj.OrderBy(x => x.PhoneNumber).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    obj = obj.OrderByDescending(x => x.PhoneNumber).ToList();
                                    break;
                                }
                            default:
                                {
                                    obj = obj.OrderBy(x => x.PhoneNumber).ToList();
                                    break;
                                }

                        }
                        break;
                    }
                case "DateCreated":
                    {

                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    obj = obj.OrderBy(x => x.DateCreated).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    obj = obj.OrderByDescending(x => x.DateCreated).ToList();
                                    break;
                                }
                            default:
                                {
                                    obj = obj.OrderBy(x => x.DateCreated).ToList();
                                    break;
                                }

                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }


            return View(obj);
        }



        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentDetails = await _context.StudentDetails.Include(e => e.StudentAddress).Include(e => e.StudentCourse)
            .FirstOrDefaultAsync(m => m.StudentID == id);
            if (studentDetails == null)
            {
                return NotFound();
            }

            return View(studentDetails);
        }



        public IActionResult Create()
        {
            LoadCourses();
            return View();
        }



		private void LoadCourses()
        {
            var list = _context.CourseDetails.ToList();
            ViewBag.list = new SelectList(list, "CourseName", "CourseName");
        }


      



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDetails alldetails,string[] SelectedItems)
        {
            if (ModelState.IsValid)
            {
                StudentDetails user = new StudentDetails();
                user.StudentID = new Guid();
                user.FirstName = alldetails.FirstName;
                user.LastName = alldetails.LastName;
                user.PhoneNumber = alldetails.PhoneNumber;
                user.Email = alldetails.Email;
                user.DateCreated = alldetails.DateCreated;
                _context.StudentDetails.Add(user);
                await _context.SaveChangesAsync();


                


                StudentCourse course = new StudentCourse();
                course.CourseId = new Guid();
                course.CourseStudentID = user.StudentID;
                course.CourseName = string.Join(",", SelectedItems);
                course.CourseDuration = alldetails.StudentCourse.CourseDuration;
                _context.StudentCourse.Add(course);
                await _context.SaveChangesAsync();



                StudentAddress address = new StudentAddress();
                address.AddressId = new Guid();
                address.AddressStudentID = user.StudentID;
                address.AddressLineOne = alldetails.StudentAddress.AddressLineOne;
                address.AddressLineTwo = alldetails.StudentAddress.AddressLineTwo;
                address.City = alldetails.StudentAddress.City;
                address.Region = alldetails.StudentAddress.Region;
                address.Country = alldetails.StudentAddress.Country;
                _context.StudentAddress.Add(address);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(alldetails);

        }




        public async Task<IActionResult> Edit(Guid? id)
        {
             LoadCourses();
            var studentDetails = await _context.StudentDetails.Include(e => e.StudentAddress).Include(e =>
            e.StudentCourse)
            .FirstOrDefaultAsync(m => m.StudentID == id);
            studentDetails.SelectedItems = studentDetails.StudentCourse.CourseName.Split(',').ToArray();

            

           
            return View(studentDetails);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, StudentDetails alldetails, string[] SelectedItems)
        {
           
            if (ModelState.IsValid)
            {
                var user = _context.StudentDetails.Where(x => x.StudentID == id).FirstOrDefault();
                user.FirstName = alldetails.FirstName;
                user.LastName = alldetails.LastName;
                user.PhoneNumber = alldetails.PhoneNumber;
                user.Email = alldetails.Email;
                user.DateCreated = alldetails.DateCreated;
                _context.StudentDetails.Update(user);
               
                var course = _context.StudentCourse.Where(x => x.CourseStudentID == id).FirstOrDefault();
                course.CourseName = string.Join(",", SelectedItems);
                course.CourseDuration = alldetails.StudentCourse.CourseDuration;
                _context.StudentCourse.Update(course);
                
                var address = _context.StudentAddress.Where(x => x.AddressStudentID == id).FirstOrDefault();
                address.AddressLineOne = alldetails.StudentAddress.AddressLineOne;
                address.AddressLineTwo = alldetails.StudentAddress.AddressLineTwo;
                address.City = alldetails.StudentAddress.City;
                address.Region = alldetails.StudentAddress.Region;
                address.Country = alldetails.StudentAddress.Country;
                _context.StudentAddress.Update(address);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(alldetails);

        }



        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentDetails = await _context.StudentDetails.Include(e => e.StudentAddress).Include(e
            => e.StudentCourse)
            .FirstOrDefaultAsync(m => m.StudentID == id);
            if (studentDetails == null)
            {
                return NotFound();
            }

            return View(studentDetails);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var studentDetails = await _context.StudentDetails.FindAsync(id);
            if (studentDetails != null)
            {
                _context.StudentDetails.Remove(studentDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        



    }
}