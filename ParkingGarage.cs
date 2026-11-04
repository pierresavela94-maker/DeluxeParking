using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace DeluxeParking
{
    internal class ParkingGarage
    {
        public ParkingSpot[] Spots { get; set; }

        public double MinutePrice { get; private set; }

        public ParkingGarage(int numberOfSpots, double minutePrice)
        {
            MinutePrice = minutePrice;
            Spots = new ParkingSpot[numberOfSpots];
            for(int i = 0; i < numberOfSpots; i++)
            {
                Spots[i] = new ParkingSpot(i+1);
            }
        }

        public List<(ParkingSpot, Vehicle, Vehicle)> GetGarageInfo()
        {
            var garageInfo = new List<(ParkingSpot, Vehicle, Vehicle)>();

            for (int i = 0; i < Spots.Length; i++)
            {
                garageInfo.Add(Spots[i].GetInfo());
                if (!Spots[i].Empty && Spots[i].Occupants[0].Size > 1)
                {
                    i++;
                }
            }
            return garageInfo;
        }

        public bool CheckGarageEmpty()
        {
            foreach(var spot in Spots)
            {
                if (spot.Occupants.Count > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public ParkingSpot FindSpot(Vehicle vehicle)
        {
            switch (vehicle)
            {
                case Car:
                     return FindCarSpot();
                case Motorcycle:
                    return FindMcSpot();
                case Bus:
                    return FindBusSpot();
                default:
                    return null;
            }
        }
        private ParkingSpot FindCarSpot()
        {
            for(int i = 0;i < Spots.Length; i++)
            {
                if (!Spots[i].Empty)
                {
                    continue;
                }

                if (i == 0)
                {
                    if (!Spots[i + 1].Empty)
                    {
                        return Spots[i];
                    }
                }
                else if (i == Spots.Length-1)
                {
                    if (!Spots[i - 1].Empty)
                    {
                        return Spots[i];
                    }
                }
                else 
                {
                    if ((!Spots[i - 1].Empty && Spots[i].Empty && !Spots[i + 1].Empty))
                    {
                        return Spots[i];
                    }
                }
            }
            foreach (var spot in Spots)
            {
                if (spot.Empty)
                {
                    return spot;
                }
            }
            return null;
        }
        private ParkingSpot FindMcSpot()
        {
            foreach(var spot in Spots)
            {
                if (spot.FitMC && !spot.Empty)
                {
                    return spot;
                }
            }
            return FindCarSpot();
        }
        private ParkingSpot FindBusSpot()
        {
            for(int i = 0; i < Spots.Length - 1; i++)
            {
                if (Spots[i].Empty && Spots[i + 1].Empty)
                {
                    return Spots[i];
                }
            }
            return null;
        }

        public (bool Success, ParkingSpot Spot) ParkVehicle(Vehicle vehicle)
        {
            var parkingSpot = FindSpot(vehicle);
            if (parkingSpot == null)
            {
                return (false, null);
            }

            parkingSpot.Park(vehicle);

            if (vehicle.Size > 1)
            {
                int nextIndex = Spots.IndexOf(parkingSpot) + 1;
                Spots[nextIndex].Park(vehicle);
            }

            return (true, parkingSpot);
        }


        public (bool Success, double TimeParked, double ParkingPrice, Vehicle Vehicle) CheckOut(string regNumbOut)
        {
            foreach(var spot in Spots)
            {
                foreach(var vehicle in spot.Occupants)
                {
                    if(regNumbOut == vehicle.RegistrationNumber)
                    {
                        double timeParked = spot.CheckOut(vehicle);
                        double parkingPrice = timeParked * MinutePrice;
                        if(vehicle.Size > 1)
                        {
                            Spots[Spots.IndexOf(spot) + 1].CheckOut(vehicle);
                        }
                        return (true, timeParked, parkingPrice, vehicle);
                    }
                }
            }
            return (false, 0, 0, null);
        }
    }
}

