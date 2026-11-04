using System;
using System.Collections.Generic;
using System.Text;

namespace DeluxeParking
{
    internal class Helpers
    {
        public static string GenerateRegistrationNumber()
        {
            string[] letters = { "A", "B", "C", "D", "E", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            string regNumb = "";

            while(regNumb.Length < 6) 
            {
                if(regNumb.Length < 3)
                {
                    regNumb += letters[Random.Shared.Next(letters.Length)];
                } 
                else
                {
                    regNumb += numbers[Random.Shared.Next(numbers.Length)];
                }
            }
            return regNumb;
        }

        public static string GenerateMcMake()
        {
            string[] mcMakes = { "Honda", "Yamaha", "Kawasaki", "Suzuki", "Harley-Davidson" };
            return mcMakes[Random.Shared.Next(mcMakes.Length)];
        }

        public static string GenerateColor()
        {
            string[] vehicleColors = { "Red", "Green", "Black", "White", "Blue", "Brown", "Yellow" };
            return vehicleColors[Random.Shared.Next(vehicleColors.Length)];
        }

        public static Vehicle GenerateVehicle()
        {
            switch (Random.Shared.Next(0, 3))
            {
                case 0:
                    return new Car();
                case 1:
                    return new Motorcycle();
                case 2:
                    return new Bus();
            }
            return null;
        }
    }
}
