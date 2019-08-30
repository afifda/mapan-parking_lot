using parking_lot_services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace parking_lot_services.Services
{
    public class Park : IPark
    {
        public Park()
        {
            IsAvailable = true;
        }
        public Park(IVehicle _car)
        {
            Vehicle = _car;
            IsAvailable = false;
        }
        public int SlotNumber { get; set; }
        public bool IsAvailable { get; set; }
        public IVehicle Vehicle { get; set; }
        public void ParkIn(IVehicle _car)
        {
            Vehicle = _car;
            IsAvailable = false;
        }

        public void ParkOut()
        {
            Vehicle = null;
            IsAvailable = true;
        }
    }
}
