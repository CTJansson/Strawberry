using ProjectStrawberry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProjectStrawberry.DAL;
using System.IO;
using System.Text.RegularExpressions;

namespace ProjectStrawberry.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        public ActionResult Index()
        {
            DAL.ProjectStrawberryEntities ctx = new DAL.ProjectStrawberryEntities();
            string currentUserId = currentUserId = User.Identity.GetUserId();
            bool doUserHaveCharacter = ctx.Characters.Any(c => c.AccountId == currentUserId && c.Alive);

            if (!doUserHaveCharacter)
            {
                List<SelectListItem> GenderOptions = new List<SelectListItem>();
                List<SelectListItem> ClassOptions = new List<SelectListItem>();
                List<SelectListItem> ProfessionOptions = new List<SelectListItem>();
                List<SelectListItem> AvatarOptions = new List<SelectListItem>();

                foreach (var item in ctx.Classes)
                {
                    ClassOptions.Add(new SelectListItem { Text = item.ClassName, Value = item.Id.ToString() });
                }

                foreach (var item in ctx.Genders)
                {
                    GenderOptions.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }

                ViewBag.GenderId = GenderOptions;
                ViewBag.ClassId = ClassOptions;
                ViewBag.ProfessionId = ProfessionOptions;
                ViewBag.AvatarFemales = Directory.EnumerateFiles(Server.MapPath("~/Content/Images/AvatarFemales/")).Select(a => Path.GetFileNameWithoutExtension(a));
                ViewBag.AvatarMales = Directory.EnumerateFiles(Server.MapPath("~/Content/Images/AvatarMales/")).Select(a => Path.GetFileNameWithoutExtension(a));

                return View();
            }

            return RedirectToAction("Game");
        }

        [HttpPost]
        public ActionResult Index(CharacterModel model)
        {
            DAL.ProjectStrawberryEntities ctx = new DAL.ProjectStrawberryEntities();
            string currentUserId = User.Identity.GetUserId();

            if (ctx.Characters.Any(c => c.AccountId == currentUserId && c.Alive))
            {
                return RedirectToAction("Game");
            }
            
            Match checkNameAndEdit = Regex.Match(model.Name, @"(?i)^[a-z]+");
            model.Name = checkNameAndEdit.ToString();

            bool attributeIs60 = model.Axe + model.Dagger + model.Evasion + model.Mace + model.Parry + model.Polearm + model.Quickness + model.Spear + model.Stamina + model.Strength + model.Sword + model.Vitality == 60;
            bool doesNameExist = ctx.Characters.Any(c => c.Name == model.Name && c.Alive);
            
            if (ModelState.IsValid && attributeIs60 && !doesNameExist)
            {
                var character = new Character
                {
                    AccountId = currentUserId,
                    Avatar = model.Avatar,
                    Axe = model.Axe,
                    Block = model.Block,
                    Class = ctx.Classes.FirstOrDefault(c => c.Id == model.ClassId),
                    Dagger = model.Dagger,
                    Evasion = model.Evasion,
                    Experience = 0,
                    Gender = ctx.Genders.FirstOrDefault(g => g.Id == model.GenderId),
                    Gold = 5000,
                    Level = 1,
                    Mace = model.Mace,
                    Name = model.Name,
                    Parry = model.Parry,
                    Polearm = model.Polearm,
                    Quickness = model.Quickness,
                    Spear = model.Spear,
                    Stamina = model.Stamina,
                    Strength = model.Strength,
                    Sword = model.Sword,
                    Vitality = model.Vitality,
                    Alive = true,
                };

                if (character.Class.ClassName == "Assassin")
                {
                    character.Quickness += 2;
                    character.Strength += 1;
                }
                else if (character.Class.ClassName == "Bounty Hunter")
                {
                    character.Strength += 2;
                    character.Quickness += 1;
                }
                else if (character.Class.ClassName == "Crusader")
                {
                    character.Vitality += 2;
                    character.Stamina += 1;
                }
                else if (character.Class.ClassName == "Duelist")
                {
                    character.Evasion += 1;
                    character.Strength += 1;
                    character.Quickness += 1;
                }
                else if (character.Class.ClassName == "Fighter")
                {
                    character.Strength += 2;
                    character.Vitality += 1;
                }
                else if (character.Class.ClassName == "Guard")
                {
                    character.Vitality += 2;
                    character.Parry += 1;
                }
                else if (character.Class.ClassName == "Thief")
                {
                    character.Quickness += 2;
                    character.Evasion += 1;
                }

                character.Health = character.Vitality;

                ctx.Characters.Add(character);
                ctx.SaveChanges();

                return RedirectToAction("Game");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Game()
        {
            DAL.ProjectStrawberryEntities ctx = new DAL.ProjectStrawberryEntities();
            string currentUserId = User.Identity.GetUserId();

            #region FighterModel
            CharacterModel character = ctx.Characters.Select(f => new CharacterModel
            {
                AccountId = f.AccountId,
                Avatar = f.Avatar + ".jpg",
                Axe = f.Axe,
                Block = f.Block,
                Class = ctx.Classes.Select(c => new ClassModel { Id = c.Id, Name = c.ClassName }).FirstOrDefault(c => c.Id == f.ClassId),
                Dagger = f.Dagger,
                Evasion = f.Evasion,
                Experience = f.Experience,
                Gender = ctx.Genders.Select(g => new GenderModel { Id = g.Id, Name = g.Name }).FirstOrDefault(g => g.Id == f.GenderId),
                Gold = f.Gold,
                Health = f.Health,
                Level = f.Level,
                Mace = f.Mace,
                Name = f.Name,
                Parry = f.Parry,
                Polearm = f.Polearm,
                Quickness = f.Quickness,
                Stamina = f.Stamina,
                Strength = f.Strength,
                Sword = f.Sword,
                Vitality = f.Vitality,
                Alive = f.Alive,
            }).FirstOrDefault(f => f.AccountId == currentUserId && f.Alive);
            #endregion

            return View(character);
        }

        public ActionResult Delete()
        {
            string currentUserId = User.Identity.GetUserId();

            var ctx = new ProjectStrawberryEntities();

            Character character = ctx.Characters.FirstOrDefault(c => c.AccountId == currentUserId && c.Alive);
            character.Alive = false;
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}