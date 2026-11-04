using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DeluxeParking
{
    internal class ParkingSpot
    {
        public int SpotNumber { get; set; }
        public List<Vehicle> Occupants { get; private set; }
        public bool FitMC => Empty || (Occupants.Count < 2 && Occupants[0].Size == 0.5);
        public bool Empty => Occupants.Count == 0;

        public ParkingSpot(int spotNumber)
        {
            Occupants = new List<Vehicle>();
            SpotNumber = spotNumber;
        }

        public void Park(Vehicle vehicle)
        {
            Occupants.Add(vehicle);
        }

        public double CheckOut(Vehicle vehicle)
        {
            Occupants.Remove(vehicle);
            DateTime checkOutTime = DateTime.Now;
            TimeSpan parkingTime = checkOutTime - vehicle.CheckInTime;
            return Math.Ceiling(parkingTime.TotalMinutes);     
        }

        public (ParkingSpot Spot, Vehicle Vehicle1, Vehicle Vehicle2) GetInfo()
        {
            if (Empty)
            {
                return (this, null, null);
            }
            else if (Occupants.Count > 1)
            {
                return (this, Occupants[0], Occupants[1]);               
            }
            else
            {
                return (this, Occupants[0], null);
            }
        }
    }
}
