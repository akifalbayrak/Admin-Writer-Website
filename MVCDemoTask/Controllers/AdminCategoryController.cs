using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;

namespace MVCDemoTask.Controllers
{
    public class AdminCategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal());

        [Authorize(Roles ="B")]
        public ActionResult Index()
        {
            var categoryValues = cm.GetList();
            return View(categoryValues);
        }
        [HttpGet]
        public ActionResult AddCategory() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            CategoryValidator cv = new CategoryValidator();
            ValidationResult results = cv.Validate(category);
            if (results.IsValid) 
            {
                cm.CategoryAdd(category);
                return RedirectToActionPermanent("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public ActionResult DeleteCategory(int id)
        {
            var categoryValue = cm.GetById(id);
            categoryValue.CategoryStatus = false;
            cm.CategoryDelete(categoryValue);
            return RedirectToAction("Index");
        }
        public ActionResult ActiveCategory(int id)
        {
            var categoryValue = cm.GetById(id);
            categoryValue.CategoryStatus = true;
            cm.CategoryDelete(categoryValue);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {

            List<SelectListItem> valueCategory = new List<SelectListItem>
            {
                new SelectListItem { Text = "True", Value = "true" },
                new SelectListItem { Text = "False", Value = "false" }
            };

            ViewBag.vlc = valueCategory;
            var categoryValue = cm.GetById(id);
            return View(categoryValue);

        }

        [HttpPost]
        public ActionResult EditCategory(Category p)
        {
            cm.CategoryUpdate(p);
            return RedirectToAction("Index");
        }
    }
}
