using ShipBobUserOrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipBobUserOrderManagement.Areas.Admin.AdminVM
{
    public class CreateOrderVM
    {

        public Order Order { get; set; }

        public User User { get; set; }
    }
}
