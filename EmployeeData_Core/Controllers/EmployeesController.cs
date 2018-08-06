using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeData_Core.Models
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var _data = (from c in _context.tblEmployee
                         select new tblEmployee
                         {
                             Id = c.Id,
                             FirstName = c.FirstName,
                             LastName = c.LastName,
                             EmailId = c.EmailId,
                             Address = c.Address,
                             Qualification = c.Qualification,
                             Department = _context.tblDepartment.Where(x => x.Id == c.DepartmentId).SingleOrDefault().DepartmentName.ToString()
                         });

            return View(await _data.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.tblEmployee
                .SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Create()
        {
            var ListData = (from c in _context.tblDepartment
                            select new
                            {
                                DepartmentId = c.Id,
                                DepartmentName = c.DepartmentName
                            }).ToList();

            ViewBag.Departments = ListData;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,EmailId,DepartmentId,Address,Qualification")] tblEmployee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            var ListData = (from c in _context.tblDepartment
                            select new
                            {
                                DepartmentId = c.Id,
                                DepartmentName = c.DepartmentName
                            }).ToList();

            ViewBag.Departments = ListData;

            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.tblEmployee.SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FirstName,LastName,EmailId,DepartmentId,Address,Qualification")] tblEmployee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.tblEmployee
                .SingleOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var employee = await _context.tblEmployee.SingleOrDefaultAsync(m => m.Id == id);
            _context.tblEmployee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(long id)
        {
            return _context.tblEmployee.Any(e => e.Id == id);
        }
    }
}
