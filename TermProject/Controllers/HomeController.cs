using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TermProject.Models;


namespace TermProject.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        DataClasses1DataContext dc = new DataClasses1DataContext();

       

        public ActionResult Index()
        {
            List<string> var2 = new List<string>();
            var var1 = dc.records.ToList();
            foreach (var  a in var1.Select(x=>x.category).Distinct())
            {
                var2.Add(a);
            }
            
            return View(var2);
        }

        public ActionResult ViewItems()
        {
            var var1 = dc.records.ToList();
            return View(var1);
        }

        public ActionResult AddItems()
        {
           
            return View();
        }

        public ActionResult Category()
        {
            List<string> var2 = new List<string>();
            var var1 = dc.records.ToList();
            foreach (var a in var1.Select(x => x.category).Distinct())
            {
                if( !var2.Contains(a.ToLower()))
                {
                     var2.Add(a.ToLower());
                }
               
               
            }

            return View(var2);
        }

        public ActionResult Add()
        {
            string name = Request["title"];
            string description = Request["description"];
            string price = Request["price"];
            string category = Request["category"];
            record p = new record();
            p.title = name;
            p.description = description;
            p.price = int.Parse(price);
            p.category = category;

            dc.records.InsertOnSubmit(p);

            dc.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditInfo(int id)
        {
            var var1 = dc.records.First(x => x.Id == id);
            return View(var1);
        }

        public ActionResult Edit(int id)
        {
            var a = dc.records.First(x => x.Id == id);
            a.title = Request["title"];
            a.description = Request["description"];
            a.price = int.Parse(Request["price"]);
            a.category = Request["category"];
            dc.SubmitChanges();
            

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var var1 = dc.records.First(x => x.Id == id);
            dc.records.DeleteOnSubmit(var1);
            dc.SubmitChanges();

            return RedirectToAction("Index");
        }
        

        public ActionResult ShowCategoryDetail()
        {
            string var1 = Request["category"];
            var var2 = dc.records.Where(x => x.category == var1); 

            return View(var2);

        }

    }
}
