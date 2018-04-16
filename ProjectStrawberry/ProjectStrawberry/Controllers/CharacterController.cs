using ProjectStrawberry.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectStrawberry.Controllers
{
    public class CharacterController : Controller
    {
        // GET: Character
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCharacter(int id)
        {
            var ctx = new ProjectStrawberryEntities();

            //var character = ctx.Characters.Select().FirstOrDefault(c => c.Id == id);

            return View();
        }
    }
}