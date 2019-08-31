using parking_lot_services.Interface;
using parking_lot_services.Services;
using System;
using System.IO;

namespace parking_lot
{
    class parking_lot
    {
        static IParkOperation parkOperationService;
        static void Main(string[] args)
        {
            var command = string.Empty;
            parkOperationService = new ParkOperation();
            try
            {
                if (args == null || args.Length == 0)
                {
                    while (command != "exit")
                    {
                        command = Console.ReadLine();
                        DoActionCommand(command);
                    }
                }
                else if (args != null && args.Length == 1)
                {
                    var file = new StreamReader(args[0]);
                    while ((command = file.ReadLine()) != null)
                    {
                        DoActionCommand(command);
                    }

                    file.Close();
                }
                else if (args != null && args.Length != 1)
                {
                    Console.WriteLine("The input for running the app was incorrect. Please run without any argument or enter the file name.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private static void DoActionCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command)) { throw new ArgumentNullException("command", "command could not be empty"); };
            try
            {
                var splittedCommands = command.Split(' ');
                switch (splittedCommands[0])
                {
                    case Constants.CREATE_PARKING_LOT:
                        CreateParkingLot(splittedCommands);
                        break;
                    case Constants.PARK:
                        Park(splittedCommands);
                        break;
                    case Constants.LEAVE:
                        Leave(splittedCommands);
                        break;
                    case Constants.STATUS:
                        GetStatus(splittedCommands);
                        break;
                    case Constants.REGISTRATION_NUMBERS_FOR_CARS_WITH_COLOUR:
                        RegistrationNumbersForCarsWithColour(splittedCommands);
                        break;
                    case Constants.SLOT_NUMBERS_FOR_CARS_WITH_COLOUR:
                        SlotNumbersForCarsWithColour(splittedCommands);
                        break;
                    case Constants.SLOT_NUMBER_FOR_REGISTRATION_NUMBER:
                        SlotNumberForRegistrationNumber(splittedCommands);
                        break;
                    default:
                        Console.WriteLine("Command was not recognized. Please enter valid command.");
                        break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        private static void SlotNumberForRegistrationNumber(string[] splittedCommands)
        {
            if (splittedCommands.Length != 2)
            {
                throw new ArgumentException(string.Concat(Constants.SLOT_NUMBER_FOR_REGISTRATION_NUMBER, " should be followed by registration number"), "Command");
            }

            var result = parkOperationService.GetSlotNumberByPlateNumber(splittedCommands[1]);
            if (result == 0)
            {
                Console.WriteLine(Constants.NOT_FOUND);
                return;
            }

            Console.WriteLine(result);
        }
        private static void SlotNumbersForCarsWithColour(string[] splittedCommands)
        {
            if (splittedCommands.Length != 2)
            {
                throw new ArgumentException(string.Concat(Constants.SLOT_NUMBERS_FOR_CARS_WITH_COLOUR, " should be followed by colour"), "Command");
            }

            var result = parkOperationService.GetSlotNumbersByColours(splittedCommands[1]);
            if (result == null || result.Count == 0)
            {
                Console.WriteLine(Constants.NOT_FOUND);
                return;
            }

            Console.WriteLine(string.Join(", ", result));
        }

        private static void RegistrationNumbersForCarsWithColour(string[] splittedCommands)
        {
            if (splittedCommands.Length != 2)
            {
                throw new ArgumentException(string.Concat(Constants.REGISTRATION_NUMBERS_FOR_CARS_WITH_COLOUR, " should be followed by colour"), "Command");
            }

            var result = parkOperationService.GetPlateNumbersByColour(splittedCommands[1]);
            if (result == null || result.Count == 0)
            {
                Console.WriteLine(Constants.NOT_FOUND);
                return;
            }

            Console.WriteLine(string.Join(", ", result));
        }

        private static void GetStatus(string[] splittedCommands)
        {
            if (splittedCommands.Length != 1)
            {
                throw new ArgumentException(string.Concat(Constants.STATUS, "should leave alone"), "Command");
            }

            var result = parkOperationService.GetParkingLot();
            if (result == null || result.Count == 0)
            {
                Console.WriteLine("No car parked in our parking lot.");
                return;
            }

            Console.WriteLine("{0,-10} {1,-16} {2,-10}", "Slot No.", "Registration No", "Colour");
            foreach (var park in result)
            {
                Console.WriteLine("{0,-10} {1,-16} {2,-10}", park.SlotNumber, park.Vehicle.PlateNumber, park.Vehicle.Colour);
            }
        }

        private static void Leave(string[] splittedCommands)
        {
            int number = 0;
            if (splittedCommands.Length != 2 || !int.TryParse(splittedCommands[1], out number))
            {
                throw new ArgumentException(string.Concat(Constants.LEAVE, " should be followed by valid slot number"), "Command");
            }

            var parkInfo = parkOperationService.Leave(number);
            if (parkInfo == null)
            {
                Console.WriteLine(Constants.NOT_FOUND);
                return;
            }

            Console.WriteLine(string.Format(Constants.SLOT_NUMBER_FREE, parkInfo.SlotNumber));
        }

        private static void Park(string[] splittedCommands)
        {
            if (splittedCommands.Length != 3)
            {
                throw new ArgumentException(string.Concat(Constants.PARK, " should be followed by registration number and colour"), "Command");
            }

            var car = new Car()
            {
                PlateNumber = splittedCommands[1],
                Colour = splittedCommands[2]
            };

            var parkInfo = parkOperationService.Enter(car);
            if (parkInfo == null)
            {
                Console.WriteLine(Constants.FULL_PARKED);
                return;
            }

            Console.WriteLine(string.Format(Constants.SLOT_ALLOCATED, parkInfo.SlotNumber));
        }

        private static void CreateParkingLot(string[] splittedCommands)
        {
            int number = 0;
            if (splittedCommands.Length != 2 || !int.TryParse(splittedCommands[1], out number))
            {
                throw new ArgumentException(string.Concat(Constants.CREATE_PARKING_LOT, " should be followed by valid integer"), "Command");
            }

            parkOperationService.CreateParkingLot(number);

            Console.WriteLine(string.Format(Constants.PARKING_LOT_CREATED, number));
        }
    }
}
