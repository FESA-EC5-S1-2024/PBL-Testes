using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PBLprojectMVC.Models;
using PBLprojectMVC.DAO;

namespace hotel_management.Controllers
{
    public class StandardController<T> : Controller where T : StandardViewModel
    {
        // Standard Controller for CRUD operations
        protected bool NeedsAuthentication { get; set; } = true;
        protected StandardDAO<T> DAO { get; set; }

        // Default view names
        protected string IndexViewName { get; set; } = "Index";
        protected string FormViewName { get; set; } = "Form";

        public virtual IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

        // Saves the item in the database
        public virtual IActionResult Save(T model, string operation)
        {
            try
            {

                if (operation == "Insert")
                    DAO.Insert(model);
                else
                    DAO.Update(model);

                return RedirectToAction(IndexViewName);
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

        public virtual IActionResult Edit(int id)
        {
            try
            {
                ViewBag.Operation = "Update";
                var model = DAO.Get(id);

                if (model == null)
                    return RedirectToAction(IndexViewName);

                return View(FormViewName, model);
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

        // Deletes the item in the database
        public virtual IActionResult Delete(int id)
        {

            try
            {
                DAO.Delete(id);
                return RedirectToAction(IndexViewName);
            }
            catch (Exception error)
            {
                return View("Error", new ErrorViewModel(error.ToString()));
            }
        }

        

    }
}