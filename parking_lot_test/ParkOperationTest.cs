using Microsoft.VisualStudio.TestTools.UnitTesting;
using parking_lot_services.Interface;
using parking_lot_services.Services;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace parking_lot_test
{
    [TestClass]
    public class ParkOperationTest
    {
        private ParkOperation parkOperationService;

        [TestInitialize]
        public void Initialize()
        {
            parkOperationService = new ParkOperation();            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateParkingLot_InvalidNumbers_ThrowException()
        {
            GenerateParkingLot(0);
        }

        [TestMethod]
        public void CreateParkingLot_ValidNumbers_ParkingLotCreated()
        {
            var ParkingLot = GenerateParkingLot(1);

            Assert.AreEqual(1, ParkingLot.Count);
        }

        [TestMethod]
        public void GetParkingLot_ValidCall_ParkingLotStatusReturned()
        {
            var _car = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            var ParkingLot = GenerateParkingLot(1);
            parkOperationService.Enter(_car);
            var result = (IList<IPark>)parkOperationService.GetParkingLot();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GetPlateNumbersByColour_ColourExists_PlateNumbersReturned()
        {
            var _car = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            var ParkingLot = GenerateParkingLot(1);
            parkOperationService.Enter(_car);
            var plateNumbers = (IList<string>)parkOperationService.GetPlateNumbersByColour("Black");

            Assert.IsNotNull(plateNumbers);
            Assert.AreEqual(1, plateNumbers.Count);
        }

        [TestMethod]
        public void GetPlateNumbersByColour_ColourNotExists_NoRecordReturned()
        {
            var _car = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            var ParkingLot = GenerateParkingLot(1);
            parkOperationService.Enter(_car);
            var plateNumbers = (IList<string>)parkOperationService.GetPlateNumbersByColour("White");

            Assert.AreEqual(0, plateNumbers.Count);
        }

        [TestMethod]
        public void GetSlotNumberByPlateNumber_PlateNumberExists_SlotNumberReturned()
        {
            var _car = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            var ParkingLot = GenerateParkingLot(1);
            parkOperationService.Enter(_car);
            var slotNumber = (int)parkOperationService.GetSlotNumberByPlateNumber("XX-12345-ABC");

            Assert.AreEqual(1, slotNumber);
        }

        [TestMethod]
        public void GetSlotNumberByPlateNumber_PlateNumberNotExists_NoRecordReturned()
        {
            var _car = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            var ParkingLot = GenerateParkingLot(1);
            parkOperationService.Enter(_car);
            var slotNumber = (int)parkOperationService.GetSlotNumberByPlateNumber("XX-12345-XYZ");

            Assert.AreEqual(0, slotNumber);
        }

        [TestMethod]
        public void GetSlotNumbersByColours_ColoursExists_SlotNumbersReturned()
        {
            var _car = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            var ParkingLot = GenerateParkingLot(1);
            parkOperationService.Enter(_car);
            var slotNumbers = (IList<int>)parkOperationService.GetSlotNumbersByColours("Black");

            Assert.IsNotNull(slotNumbers);
            Assert.AreEqual(1, slotNumbers.Count);
        }

        [TestMethod]
        public void GetSlotNumbersByColours_ColoursNotExists_NoRecordReturned()
        {
            var _car = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            var ParkingLot = GenerateParkingLot(1);
            parkOperationService.Enter(_car);
            var slotNumbers = (IList<int>)parkOperationService.GetSlotNumbersByColours("White");

            Assert.AreEqual(0, slotNumbers.Count);
        }

        [TestMethod]
        public void Leave_CarExists_StatusReturned()
        {
            var _car = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            var ParkingLot = GenerateParkingLot(1);
            parkOperationService.Enter(_car);
            var _carLeave = (IPark)parkOperationService.Leave(1);

            Assert.IsTrue(ParkingLot[0].IsAvailable);
        }

        [TestMethod]
        public void Leave_CarNotExists_NullReturned()
        {
            var ParkingLot = GenerateParkingLot(1);
            var _carLeave = (IPark)parkOperationService.Leave(1);

            Assert.IsNull(_carLeave);
        }

        [TestMethod]
        public void Enter_SlotAvailable_StatusReturned()
        {
            var _car = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            var ParkingLot = GenerateParkingLot(1);
            var _carPark = (IPark)parkOperationService.Enter(_car);

            Assert.IsFalse(ParkingLot[0].IsAvailable);
            Assert.AreEqual(_carPark.Vehicle.PlateNumber, ParkingLot[0].Vehicle.PlateNumber);
            Assert.AreEqual(_carPark.Vehicle.Colour, ParkingLot[0].Vehicle.Colour);
        }

        [TestMethod]
        public void Enter_SlotNotAvailable_NullsReturned()
        {
            var _car1 = new Car()
            {
                PlateNumber = "XX-12345-ABC",
                Colour = "Black"
            };
            var _car2 = new Car()
            {
                PlateNumber = "XX-67890-ABC",
                Colour = "White"
            };
            var ParkingLot = GenerateParkingLot(1);
            parkOperationService.Enter(_car1);
            var _carPark = (IPark)parkOperationService.Enter(_car2);

            Assert.IsNull(_carPark);
        }

        private IList<IPark> GenerateParkingLot(int slotCount)
        {
            parkOperationService.CreateParkingLot(slotCount);
            Type parkOperationType = parkOperationService.GetType();
            PropertyInfo property = parkOperationType.GetProperty("ParkingLot", BindingFlags.NonPublic | BindingFlags.Instance);
            var ParkingLot = (IList<IPark>)property.GetValue(parkOperationService);
            return ParkingLot;
        }
    }
}
