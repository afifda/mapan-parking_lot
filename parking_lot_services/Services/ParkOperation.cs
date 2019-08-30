using parking_lot_services.Interface;
using System;
using System.Collections.Generic;
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
                throw new ArgumentException("Slot Number", "Slot Number must be greater than zero");
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
            var result = new List<IPark>();
            for (var i = 0; i < ParkingLot.Count; i++)
            {
                if (!ParkingLot[i].IsAvailable)
                {
                    result.Add(ParkingLot[i]);
                }
            }
            return result;
        }

        public IList<string> GetPlateNumbersByColour(string colour)
        {
            var result = new List<string>();
            for (var i = 0; i < ParkingLot.Count; i++)
            {
                if (ParkingLot[i].Vehicle.Colour == colour)
                {
                    result.Add(ParkingLot[i].Vehicle.PlateNumber);
                }
            }
            return result;
        }

        public int GetSlotNumberByPlateNumber(string plateNumber)
        {
            var result = 0;
            for (var i = 0; i < ParkingLot.Count; i++)
            {
                if (ParkingLot[i].Vehicle.PlateNumber == plateNumber)
                {
                    result = ParkingLot[i].SlotNumber;
                    break;
                }
            }
            return result;
        }

        public IList<int> GetSlotNumbersByColours(string colour)
        {
            var result = new List<int>();
            for (var i = 0; i < ParkingLot.Count; i++)
            {
                if (ParkingLot[i].Vehicle.Colour == colour)
                {
                    result.Add(ParkingLot[i].SlotNumber);
                }
            }
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
