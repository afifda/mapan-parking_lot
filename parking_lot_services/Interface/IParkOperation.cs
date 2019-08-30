using System;
using System.Collections.Generic;
using System.Text;

namespace parking_lot_services.Interface
{
    public interface IParkOperation
    {
        void CreateParkingLot(int lotCount);
        IPark Enter(IVehicle car);
        IPark Leave(int slotNumber);
        IList<IPark> GetParkingLot();
        IList<string> GetPlateNumbersByColour(string colour);
        int GetSlotNumberByPlateNumber(string plateNumber);
        IList<int> GetSlotNumbersByColours(string colour);
    }
}
