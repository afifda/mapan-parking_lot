using parking_lot_services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace parking_lot_services.Services
{
    public class Car : IVehicle
    {
        public string PlateNumber { get; set; }
        public string Colour { get; set; }
    }
}
