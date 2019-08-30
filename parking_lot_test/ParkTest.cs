using Microsoft.VisualStudio.TestTools.UnitTesting;
using parking_lot_services.Services;

namespace parking_lot_test
{
    [TestClass]
    public class ParkTest
    {
        [TestMethod]
        public void ParkIn_ValidCall_SetCarAndAvailability()
        {
            var parkService = new Park();
            var _car = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            parkService.ParkIn(_car);

            Assert.IsFalse(parkService.IsAvailable);
            Assert.AreSame(_car, parkService.Vehicle);
        }

        [TestMethod]
        public void ParkOut_ValidCall_SetCarAndAvailability()
        {
            var _car = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            var parkService = new Park(_car);
            parkService.ParkOut();

            Assert.IsTrue(parkService.IsAvailable);
            Assert.IsNull(parkService.Vehicle);
        }
    }
}
