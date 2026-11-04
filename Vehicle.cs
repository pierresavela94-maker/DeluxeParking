using System;
using System.Collections.Generic;
using System.Text;

namespace DeluxeParking
{
    internal abstract class Vehicle
    {

        public string RegistrationNumber { get;  private set; }
        public string Color { get; private set; }
        public double Size { get; protected set; } 
        public DateTime CheckInTime  { get; private set; }

        public Vehicle()
        {
            RegistrationNumber = Helpers.GenerateRegistrationNumber();
            Color = Helpers.GenerateColor();
            CheckInTime = DateTime.Now;
        }
    }
    class Car : Vehicle
    {
        public bool ElectricCar { get; set; }

        public Car() : base()
        {
            Size = 1.0;
            switch (Random.Shared.Next(2))
            {
                case 0: ElectricCar = true; break;
                case 1: ElectricCar = false; break;
            }
        }
    }

    class Motorcycle : Vehicle
    {
        public string Make { get; set; }

        public Motorcycle() : base()
        {
            Size = 0.5;
            Make = Helpers.GenerateMcMake();
        }
    }

    class Bus : Vehicle
    {
        public int PassengerSeats { get; set; }
        public Bus() : base()
        {
            Size = 2;
            PassengerSeats = Random.Shared.Next(45, 61);
        }
    }
}

