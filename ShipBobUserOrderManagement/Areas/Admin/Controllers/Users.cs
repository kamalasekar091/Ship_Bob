using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShipBobUserOrderManagement.Services.Infrastructures;
using ShipBobUserOrderManagement.ViewModels;
using ShipBobUserOrderManagement.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipBobUserOrderManagement.Area.Admin.Controllers
{

    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class Users : Controller
    {

        private readonly IUser _userRepository;

        private readonly IOrder _orderRepository;


        public Users(IUser userRepository, IOrder orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            var users = _userRepository.GetAll();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Create(Models.User user)
        {
            if (user==null)
            {
                return View();
            }
            _userRepository.Insert(user);
            _userRepository.Save();
            return RedirectToAction("Index");

        }
        /*
        [HttpPost]
        public JsonResult Create([FromBody] ViewUser theViewUser)
        {
            User theUser = new User();
            theUser.FirstName = theViewUser.FirstName;
            theUser.LastName = theViewUser.LastName;
            return Json(_userRepository.Insert(theUser));

            //return View();
        }

        */

        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }



       

        public void Update([FromBody] ViewUser theViewUser)
        {
            User theUser = new User();
            theUser.Id = theViewUser.id;
            theUser.FirstName = theViewUser.FirstName;
            theUser.LastName = theViewUser.LastName;
            _userRepository.Update(theUser);
            _userRepository.Save();

            //return View();
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            _userRepository.Delete(id);
            _userRepository.Save();
           return Json(_userRepository.GetAll());
        }

        public JsonResult GetAll()
        {

            var allUser = _userRepository.GetAll();
            return Json(allUser);
        }


        

    }
}
