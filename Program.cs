using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace DeluxeParking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ParkingGarage deluxeParking = new(numberOfSpots:15, minutePrice:1.5);
            UserInterface garageUI = new(deluxeParking);

            garageUI.Run();
        }
    }
}
