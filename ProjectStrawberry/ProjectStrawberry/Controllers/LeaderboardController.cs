using ProjectStrawberry.DAL;
using ProjectStrawberry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectStrawberry.Controllers
{
    public class LeaderboardController : Controller
    {
        // GET: Leaderboard
        public ActionResult Index()
        {
            var ctx = new ProjectStrawberryEntities();

            List<LeaderboardModel> list = new List<LeaderboardModel>();
            list = ctx.Characters.Select(c => new LeaderboardModel { Id = c.Id, Level = c.Level, Name = c.Name, Alive = c.Alive }).OrderByDescending(c => c.Level).Take(10). Where(c => c.Alive).ToList();

            return PartialView("index", list);
        }
    }
}