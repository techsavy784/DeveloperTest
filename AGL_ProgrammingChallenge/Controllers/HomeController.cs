using AGL_ProgrammingChallenge.Helper;
using AGL_ProgrammingChallenge.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGL_ProgrammingChallenge.Controllers
{
    public class HomeController : Controller
    {
        static readonly IWebServiceRestClient RestClient = new WebServiceRestClient();
        public ActionResult Index()
        {
            List<PetModel> model = new List<PetModel>();
            List<PetOutput> outputModel = new List<PetOutput>();          
            foreach (var item in RestClient.GetAll())
            {
                model.Add(item);
            }  
            // grouping list by gender i.e "Male or Female"
            var groupedModel = model
                                .GroupBy(u => u.gender)                                
                                .Select(grp => new
                                {
                                gender = grp.Key,
                                 petList= grp
                                 .Where(pList => pList.pets != null)
                                 .SelectMany(pList => pList.pets)                                 
                                 .ToList()
                                })  
                                .OrderBy(mc => mc.petList.Min(dc => dc.name))
                                .ToList();
            // ordering list by pet name in ascending order
            foreach(var item in groupedModel)
            {
                PetOutput petOutput = new PetOutput();
                petOutput.OwnerGender = item.gender;
                petOutput.pets = item.petList.OrderBy(o => o.name).ToList();              
                outputModel.Add(petOutput);
            }
            return View(outputModel); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}