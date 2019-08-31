using parking_lot_services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace parking_lot_services.Services
{
    public class ParkOperation : IParkOperation
    {
        private IList<IPark> ParkingLot { get; set; }
        public ParkOperation()
        {
            ParkingLot = new List<IPark>();
        }
        public void CreateParkingLot(int lotCount)
        {
            if (lotCount < 1)
            {
                throw new ArgumentException("Slot Number must be greater than zero.", "Slot Number");
            }
            for (var i = 1; i <= lotCount; i++)
            {
                var _park = new Park()
                {
                    SlotNumber = i
                };
                ParkingLot.Add(_park);
            }
        }

        public IList<IPark> GetParkingLot()
        {
            var result = (from p in ParkingLot
                          where !p.IsAvailable
                          select p).ToList();
            return result;
        }

        public IList<string> GetPlateNumbersByColour(string colour)
        {
            var result = (from p in ParkingLot
                          where p.Vehicle.Colour.Equals(colour)
                          select p.Vehicle.PlateNumber).ToList();
            return result;
        }

        public int GetSlotNumberByPlateNumber(string plateNumber)
        {
            var result = (from p in ParkingLot
                          where p.Vehicle.PlateNumber.Equals(plateNumber)
                          select p.SlotNumber).FirstOrDefault();
            return result;
        }

        public IList<int> GetSlotNumbersByColours(string colour)
        {
            var result = (from p in ParkingLot
                          where p.Vehicle.Colour.Equals(colour)
                          select p.SlotNumber).ToList();
            return result;
        }

        public IPark Leave(int slotNumber)
        {
            for (var i = 0; i < ParkingLot.Count; i++)
            {
                if (ParkingLot[i].SlotNumber == slotNumber &&
                    !ParkingLot[i].IsAvailable)
                {
                    ParkingLot[i].ParkOut();
                    return ParkingLot[i];
                }
            }
            return null;
        }

        public IPark Enter(IVehicle car)
        {
            if (ParkingLot.Any(p => p.Vehicle != null && p.Vehicle.PlateNumber == car.PlateNumber))
            {
                throw new ArgumentException("The same registration number already parked in.", "PlateNumber");
            }
            for (var i = 0; i < ParkingLot.Count; i++)
            {
                if (ParkingLot[i].IsAvailable)
                {
                    ParkingLot[i].ParkIn(car);
                    return ParkingLot[i];
                }
            }
            return null;
        }
    }
}
