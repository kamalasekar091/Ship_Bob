using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShipBobUserOrderManagement.Services.Infrastructures;
using ShipBobUserOrderManagement.ViewModels;
using ShipBobUserOrderManagement.Models;
using ShipBobUserOrderManagement.Areas.Admin.AdminVM;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipBobUserOrderManagement.Area.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class Orders : Controller
    {

        private readonly IUser _userRepository;

        private readonly IOrder _orderRepository;


        public Orders(IUser userRepository, IOrder orderRepository)
        {

            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var orders = _orderRepository.GetAll();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            Models.User user = _userRepository.GetById(id);

            if (user == null)
            {
                throw new Exception("Order Not found");
            }

            var ObjectForView = new CreateOrderVM
            {
                Order = new Order(),
                User = _userRepository.GetById(id)
            };

            return View(ObjectForView);
        }




        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateOrderVM ordersVM)
        {
            if (ModelState.IsValid)
            {
                return View();
            }

            Order theOrder = new Order();

            theOrder.Name = ordersVM.Order.Name;
            theOrder.UserId = ordersVM.User.Id;
            theOrder.Name = ordersVM.Order.Name;
            theOrder.TrackingID = ordersVM.Order.TrackingID;
            theOrder.StreetAddress = ordersVM.Order.StreetAddress;
            theOrder.City = ordersVM.Order.City;
            theOrder.State = ordersVM.Order.State;
            theOrder.ZipCode = ordersVM.Order.ZipCode;


            _orderRepository.Insert(theOrder);
            _orderRepository.Save();
            return RedirectToAction("Index");

        }


        public IActionResult Update(int id)
        {
            Models.Order order = _orderRepository.GetById(id);

            if (order == null)
            {
                throw new Exception("Order Not found");
            }
            return View(order);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(Models.Order theOrders)
        {


            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                _orderRepository.Update(theOrders);
                _orderRepository.Save();
                return RedirectToAction("Index", "Orders", "Admin");
            }


        }



        public IActionResult Delete(int id)
        {
            Models.Order order = _orderRepository.GetById(id);

            if (order == null)
            {
                throw new Exception("Order Not found");
            }
            return View(order);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(Models.Order theOrders)
        {
            var order = _orderRepository.GetById(theOrders.Id);

            if (order==null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                _orderRepository.Delete(theOrders.Id);
                _orderRepository.Save();
                return RedirectToAction("Index", "Orders", "Admin");
            }


        }



        /*

        [HttpPost]
        public JsonResult Create([FromBody] ViewOrder theViewUser)
        {
             Order theUser = new Order(theViewUser);

             _orderRepository.Insert(theUser);
              _orderRepository.Save();

               return Json(_orderRepository.GetAll()); ;

            return null;
        }

    */
        [HttpGet]
        public JsonResult GetAll()
        {

            var allOrders = _orderRepository.GetAll();
            return Json(allOrders);
        }


        [HttpGet("{id}")]
        public JsonResult GetOrderForaUser(int id)
        {

            return Json(_orderRepository.GetAllOrdersAssociatedWithUser(id));
        }

        /*
        [HttpPut]
        public JsonResult Update([FromBody] ViewOrder theViewUser)
        {

             Order theUser = new Order(theViewUser);

              _orderRepository.Update(theUser);
              _orderRepository.Save();
              var allOrders = _orderRepository.GetById(theUser.Id);
              return Json(allOrders);

            //return null;
        }
        */



        /*
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            _orderRepository.Delete(id);
            _orderRepository.Save();
            return Json(_orderRepository.GetAll());
        }
        */


    }
}
