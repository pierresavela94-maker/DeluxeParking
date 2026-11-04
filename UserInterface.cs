using System;
using System.Collections.Generic;
using System.Text;

namespace DeluxeParking
{
    internal class UserInterface
    {
        private ParkingGarage Garage { get; set; }

        private string statusMessage;

        public UserInterface(ParkingGarage garage)
        {
            Garage = garage;
            statusMessage = "Select an option";
        }

        public void Run()
        {
            while (true)
            {
                DrawScreen();
                Console.CursorVisible = false;

                ConsoleKeyInfo key= Console.ReadKey(true);
                switch (key.KeyChar.ToString().ToUpper())
                {
                    case "N":
                        HandleCheckIn();
                        break;
                    case "R":
                        HandleCheckOut();
                        break;
                    case "Q":
                        return;
                    default:
                        statusMessage = "Select an option";
                        break;
                }
            }
        }

        private void DrawScreen()
        {
            DrawGarage();
            DrawMenu();
        }

        private void DrawGarage()
        {
            Console.Clear();

            Console.WriteLine("-- DELUXE PARKING " + new string('-', 11));

            List<(ParkingSpot, Vehicle, Vehicle)> garageInfo = Garage.GetGarageInfo();

            foreach (var (spot, v1, v2) in garageInfo)
            {
                string spotinfo = $"Spot {spot.SpotNumber}: ".PadRight(20);

                if (v1 == null && v2 == null)
                {
                    Console.WriteLine(spotinfo + "Available");
                }
                else if (v1 != null & v1.Size > 1)
                {
                    Console.WriteLine($"Spot {spot.SpotNumber}-{spot.SpotNumber + 1}: ".PadRight(20) + PrintVehilceInfo(v1));
                }
                else if(v1 != null && v2 != null)
                {
                    Console.WriteLine(spotinfo + PrintVehilceInfo(v1));
                    Console.WriteLine("".PadRight(20)+PrintVehilceInfo(v2));
                }
                else if(v1 != null)
                {
                    Console.WriteLine(spotinfo + PrintVehilceInfo(v1));
                }
            }
            Console.WriteLine(new string('-', 30));
            Console.WriteLine(statusMessage);
            Console.WriteLine(new string('-', 30));
        }

        private void HandleCheckOut()
        {
            if (Garage.CheckGarageEmpty())
            {
                statusMessage = "The Garage is empty. Unable to check out a vehicle.";
            }
            else
            {
                statusMessage = "Enter the registration number of the vehicle you want to check out";
                DrawGarage();
                Console.CursorVisible = true;

                Console.Write("Registration number: ");
                string regNumbOut = Console.ReadLine().ToUpper();
                var checkOutEvent = Garage.CheckOut(regNumbOut);

                if (!checkOutEvent.Success)
                {
                    statusMessage = "Invalid registration number. Please try again!";
                }
                else
                {
                    statusMessage = $"The {checkOutEvent.Vehicle.GetType().Name} with registration number {regNumbOut} have been checked out.\nMinutes parked: {checkOutEvent.TimeParked}\nCost: {checkOutEvent.ParkingPrice} kr";
                }
            }
        }
        private void HandleCheckIn()
        {
            var newVehicle = Helpers.GenerateVehicle();

            var parkingEvent = Garage.ParkVehicle(newVehicle);

            if (!parkingEvent.Success)
            {
                statusMessage = $"A {newVehicle.GetType().Name} arrives at the garage... There are no spots available. A vehicle needs to check out!";
            }
            else if(parkingEvent.Success && newVehicle.Size > 1)
            {
                statusMessage = $"A {newVehicle.GetType().Name} with registration number {newVehicle.RegistrationNumber} parks on Spot {parkingEvent.Spot.SpotNumber}-{parkingEvent.Spot.SpotNumber + 1}.";
            }
            else
            {
                statusMessage = $"A {newVehicle.GetType().Name} with registration number {newVehicle.RegistrationNumber} parks on Spot {parkingEvent.Spot.SpotNumber}.";
            }

        }

        private void DrawMenu()
        {
            Console.WriteLine("[N]ew Vehicle\n[R]emove Vehicle\n[Q]uit");
        }

        private string PrintVehilceInfo(Vehicle vehicle)
        {
            string uniqueInfo = "";

            if(vehicle is Car car)
            {
                uniqueInfo = car.ElectricCar ? "Electric" : "Not electric";
            }
            else if(vehicle is Motorcycle motorcycle)
            {
                uniqueInfo = motorcycle.Make;
            }
            else if(vehicle is Bus bus)
            {
                uniqueInfo = $"{bus.PassengerSeats} Seats";
            }

            return vehicle.GetType().Name.PadRight(20) + vehicle.RegistrationNumber.PadRight(20) + vehicle.Color.PadRight(20) + uniqueInfo.PadRight(20);
        }
    }
}
