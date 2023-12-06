﻿//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Integrador.Territory.Locations;

namespace TP_Integrador.Territory
{
    class Map
    {
        private static Map _instance;
        Dictionary<(int coordX, int coordY), Location> map;
        private static int maxQuarters = 3;
        private static int maxRecyclingPoints = 5;
        readonly Random rand = new Random();

        private Map()
        {
            map = new Dictionary<(int coordX, int coordY), Location> ();
        }

        private Map(int maxX, int maxY) 
        {
            map = new Dictionary<(int coordX, int coordY), Location>();            
            if((maxX *  maxY) < 8)
            {
                maxX = 3;
                maxY = 3;
            }
            MapBuilder(maxX, maxY);
            AsignMapToQuarters();
        }       

        private void MapBuilder(int maxX, int maxY)
        {
            List<LocationTypes> types = GetLocationsTypesList(maxX * maxY);
            int selector;
            int quarters = 0;
            string quarterName;

            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    selector = rand.Next(0, types.Count);

                    if (types[selector] == LocationTypes.Quarter)
                    {                        
                        switch (quarters)
                        {
                            case 0: quarterName = "Alfa"; break;
                            case 1: quarterName = "Beta"; break;
                            case 2: quarterName = "Gamma"; break;
                            default: quarterName = $"{i}{j}"; break;
                        }
                        map.Add((i, j), new Quarter($"Cuartel {quarterName}", i, j));
                        quarters++;
                    }
                    else
                    {
                        map.Add((i, j), new Location(i, j, types[selector]));
                    }
                    types.RemoveAt(selector);
                }
            }
        }

        private void AsignMapToQuarters()
        {
            List<Quarter> quarters = GetQuarterLocations();
            foreach (Quarter quarter in quarters)
            {
                quarter.AsignMap(this);
            }
        }

        private List<LocationTypes> GetLocationsTypesList(int locationsAmount)
        {
            List<LocationTypes> locationsType = new List<LocationTypes>();
            for (int i = 0; i < maxQuarters; i++)
            {
                locationsType.Add(LocationTypes.Quarter);
                locationsAmount--;
            }
            for (int i = 0; i < maxRecyclingPoints; i++)
            {
                locationsType.Add(LocationTypes.Recycling);
                locationsAmount--;
            }
            while (locationsAmount > 0)
            {
                locationsType.Add(GetLocationTypeByProbability());
                locationsAmount--;
            }
            return locationsType;
        }

        private LocationTypes GetLocationTypeByProbability()
        {
            LocationTypes type;
            int selector = rand.Next(0, 100);
            if (selector < 80)
            {
                type = LocationTypes.Common;
            }
            else if (selector < 85)
            {
                type = LocationTypes.Verter;
            }
            else if (selector < 95)
            {
                type = LocationTypes.Lake;
            }
            else
            {
                type = LocationTypes.ElectricVerter;
            }

            return type;
        }

        private Location GetUpLocation(Location location)
        {
            int coordX = location.GetCoordX() - 1;
            int coordY = location.GetCoordY();
            map.TryGetValue((coordX, coordY), out Location upLocation);

            return upLocation;
        }

        private Location GetDownLocation(Location location)
        {
            int coordX = location.GetCoordX() + 1;
            int coordY = location.GetCoordY();
            map.TryGetValue((coordX, coordY), out Location downLocation);

            return downLocation;
        }

        private Location GetLeftLocation(Location location)
        {
            int coordX = location.GetCoordX();
            int coordY = location.GetCoordY() - 1;
            map.TryGetValue((coordX, coordY), out Location leftLocation);

            return leftLocation;
        }

        private Location GetRightLocation(Location location)
        {
            int coordX = location.GetCoordX();
            int coordY = location.GetCoordY() + 1;
            map.TryGetValue((coordX, coordY), out Location rightLocation);

            return rightLocation;
        }

        public static Map GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Map();
            }
            return _instance;
        }

        public static Map GetInstance(int maxX, int maxY)
        {
            if (_instance == null)
            {
                _instance = new Map(maxX, maxY);
            }
            return _instance;
        }

        public static int GetMaxQuarters()
        {
            return maxQuarters;
        }

        public static int GetMaxRecyclingPoints()
        {
            return maxRecyclingPoints;
        }

        public Dictionary<(int, int), Location> GetMap()
        {
            return map;
        }

        public Location GetLocationInMap(int coordX,  int coordY)
        {
            map.TryGetValue((coordX, coordY), out Location location);
            
            return location;
        }        

        public List<Location> GetNeighbors(Location location)
        {
            List<Location> neighbours = new List<Location>
            {
                GetUpLocation(location),
                GetRightLocation(location),
                GetDownLocation(location),
                GetLeftLocation(location),                
            };            
            return neighbours;
        }

        public List<Quarter> GetQuarterLocations()
        {
            List<Quarter> quarters = new List<Quarter>();    

            foreach (Location location in map.Values)
            {
                if(location is Quarter quarter)
                {
                    quarters.Add(quarter);
                }
            }

            return quarters;
        }                

        public void ResetLocationsParameters()
        {
            foreach (Location location in map.Values)
            {
                location.ResetParameters();
            } 
        }

        public bool AddLocationsToMap(List<Location> locations)
        {
            bool added = false;

            if (_instance == null)
            {
                GetInstance();
            }

            try
            {
                foreach (Location location in locations)
                {
                    map.Add((location.CoordX, location.CoordY), location);
                }

                AsignMapToQuarters();

                added = true;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Localización/es nula/s, no se puede cargar...");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Error al cargar localización/es...");
            }        

            return added;
        }        

        public void DebugTypesPorcentages()
        {
            int common = 0, verter = 0, lake = 0, quarter = 0, recycling = 0, elecVert = 0;
            int max = map.Count;
            foreach (Location location in map.Values)
            {
                switch (location.GetLocationType())
                {
                    case LocationTypes.Common: common++; break;
                    case LocationTypes.Verter: verter++; break;
                    case LocationTypes.Lake: lake++; break;
                    case LocationTypes.Recycling: recycling++; break;
                    case LocationTypes.ElectricVerter: elecVert++; break;
                    case LocationTypes.Quarter: quarter++; break;
                }
            }
            Console.WriteLine($"Comm: {common} {common * 100 / max}% | " +
                $"Vert: {verter} {verter * 100 / max}% | " +
                $"Lake: {lake} {lake * 100 / max}% | " +
                $"Recy: {recycling} {recycling * 100 / max}% | " +
                $"ElVe: {elecVert} {elecVert * 100 / max}% | " +
                $"Qua: {quarter} {quarter * 100 / max}%");
        }        

        public void PrintRoute(List<Location> route, Location initialLocation)
        {
            int maxX = map.Last().Key.coordX + 1;
            int maxY = map.Last().Key.coordY + 1;

            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    Location loc = GetLocationInMap(i, j);
                    if (loc != null)
                    {
                        if (loc.GetCoords() == initialLocation.GetCoords())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write((int)loc.GetLocationType() + $" ");
                        }
                        else if (loc.GetLocationType() == LocationTypes.Lake)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write((int)loc.GetLocationType() + $" ");
                        }                        
                        else if (route.Contains(loc))
                        {
                            if(loc.GetCoords() == route.Last().GetCoords())
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write((int)loc.GetLocationType() + $" ");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write((int)loc.GetLocationType() + $" ");
                            }                            
                        }
                        else if (loc.GetLocationType() == LocationTypes.Verter || loc.GetLocationType() == LocationTypes.ElectricVerter)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write((int)loc.GetLocationType() + $" ");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write((int)loc.GetLocationType() + $" ");
                        }

                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }

        public void PrintMap()
        {
            int maxX = map.Last().Key.coordX + 1;
            int maxY = map.Last().Key.coordY + 1;

            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    Location loc = GetLocationInMap(i, j);
                    if (loc != null)
                    {
                        if (loc.GetLocationType() == LocationTypes.Quarter)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write((int)loc.GetLocationType() + $" ");
                        }
                        else if (loc.GetLocationType() == LocationTypes.Lake)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write((int)loc.GetLocationType() + $" ");
                        }
                        else if (loc.GetLocationType() == LocationTypes.Verter || loc.GetLocationType() == LocationTypes.ElectricVerter)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write((int)loc.GetLocationType() + $" ");
                        }                        
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write((int)loc.GetLocationType() + $" ");
                        }

                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }
    }
}
