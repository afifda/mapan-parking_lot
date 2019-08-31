using System;
using System.Collections.Generic;
using System.Text;

namespace parking_lot
{
    static class Constants
    {
        #region input
        public const string CREATE_PARKING_LOT = "create_parking_lot";
        public const string PARK = "park";
        public const string LEAVE = "leave";
        public const string STATUS = "status";
        public const string REGISTRATION_NUMBERS_FOR_CARS_WITH_COLOUR = "registration_numbers_for_cars_with_colour";
        public const string SLOT_NUMBERS_FOR_CARS_WITH_COLOUR = "slot_numbers_for_cars_with_colour";
        public const string SLOT_NUMBER_FOR_REGISTRATION_NUMBER = "slot_number_for_registration_number";
        #endregion

        #region output
        public const string PARKING_LOT_CREATED = "Created a parking lot with {0} slots";
        public const string SLOT_ALLOCATED = "Allocated slot number: {0}";
        public const string SLOT_NUMBER_FREE = "Slot number {0} is free";
        public const string FULL_PARKED = "Sorry, parking lot is full";
        public const string NOT_FOUND = "Not found";
        #endregion
    }
}
