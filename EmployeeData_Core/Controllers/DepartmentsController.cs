using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeData_Core.Models;

namespace EmployeeData_Core.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly EmployeeContext _context;

        public DepartmentsController(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDepartment = await _context.tblDepartment
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tblDepartment == null)
            {
                return NotFound();
            }

            return View(tblDepartment);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDepartment = await _context.tblDepartment.SingleOrDefaultAsync(m => m.Id == id);
            if (tblDepartment == null)
            {
                return NotFound();
            }
            return View(tblDepartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentName")] tblDepartment tblDepartment)
        {
            if (id != tblDepartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblDepartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tblDepartmentExists(tblDepartment.Id))
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
            return View(tblDepartment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDepartment = await _context.tblDepartment
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tblDepartment == null)
            {
                return NotFound();
            }

            return View(tblDepartment);
        }

        private bool tblDepartmentExists(int id)
        {
            return _context.tblDepartment.Any(e => e.Id == id);
        }

        #region CHECK_DUPLICATION
        [HttpPost]
        public IActionResult IsAvailable([FromBody] tblDepartment _dep)
        {
            if (_dep.Id == 0)
            {
                var Check = _context.tblDepartment.Where(x => x.DepartmentName == _dep.DepartmentName).Count();

                if (Check > 0)
                {
                    return Json(false);
                }
                else
                {
                    return Json(true);
                }
            }
            else
            {
                var Check = _context.tblDepartment.Where(x => x.DepartmentName == _dep.DepartmentName && x.Id != _dep.Id).Count();

                if (Check > 0)
                {
                    return Json("Department Already Exist");
                }
                else
                {
                    return Json(true);
                }
            }
        }
        #endregion

        #region POST_JSON_DELETE
        [HttpPost]
        public JsonResult Delete([FromBody]tblDepartment _dep)
        {
            try
            {
                var _resp = _context.tblDepartment.Where(x => x.Id == _dep.Id).SingleOrDefault();
                _context.tblDepartment.Remove(_resp);
                _context.SaveChanges();

                return Json(200);
            }
            catch (Exception ex)
            {
                return Json(300);
            }
        }
        #endregion

        #region POST_JSON_CREATE
        [HttpPost]
        public IActionResult CreateDepartment([FromBody]tblDepartment _dep)
        {
            try
            {
                tblDepartment _obj = new tblDepartment();
                _obj.DepartmentName = _dep.DepartmentName;
                _context.tblDepartment.Add(_obj);
                _context.SaveChanges();

                return Json(200);
            }
            catch (Exception ex)
            {
                return Json(300);
            }
        }
        #endregion

        #region JSON_REQ_GETALLDATA
        [HttpGet]
        public JsonResult GetAllData()
        {
            var _data = (from c in _context.tblDepartment
                         select new tblDepartment
                         {
                             Id = c.Id,
                             DepartmentName = c.DepartmentName
                         }).ToList();

            return Json(_data);

        }
        #endregion
    }
}
