//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class Program
    {
        static void Main(string[] args)
        {
            Quarter cuartel = new Quarter("Cuartel General");

            const string homeOptions =
                "1) Listar el estado de todos los operadores.\n" +
                "2) Listar el estado de todos los operadores en una localización. \n" +
                "3) Total recall. \n" +
                "4) Seleccionar un operador. \n" +
                "5) Agregar o remover un operador. \n" +
                "0) Salir. \n" +
                "Seleccione una opción";            

            AddRandomOperatorsToQuarter(cuartel, 20);

            int option = -1;

            while(option != 0)
            {
                option = ShowMenu(homeOptions, 5);
                Console.WriteLine();

                if (option == 1)
                {
                    ListOperators(cuartel);
                    Console.ReadKey();
                }                    

                if (option == 2)
                {
                    ListOperatorsInLocation(cuartel);
                    Console.ReadKey();
                }                    

                if (option == 3)
                {
                    TotalRecall(cuartel);
                    Console.ReadKey();
                }                    

                if (option == 4)
                {
                    UseOperator(cuartel);
                    Console.ReadKey();
                }

                if (option == 5)
                {

                }
            }                            
            
        }

        static void AddRandomOperatorsToQuarter(Quarter quarter, int amount)
        {
            Random random = new Random();

            for (int i = 0; i < amount; i++)
            {
                bool added = false;
                Operator op = null;
                Locations location = GetRandomLocation(random);
                int type = random.Next(0, 3);

                while (!added)
                {
                    int id = random.Next(0, amount + 100);

                    if (type == 0)
                        op = new UAV(id, random.Next(30, 60), location);

                    if (type == 1)
                        op = new K9(id, random.Next(20, 50), location);

                    if (type == 2)
                        op = new M8(id, random.Next(15, 40), location);

                    added = quarter.AddOperator(op);
                }
            }

            Console.WriteLine(quarter.GetOperators().Count);
        }

        static Locations GetRandomLocation(Random random)
        {
            Array locations = Enum.GetValues(typeof(Locations));
            int index = random.Next(0, locations.Length);
            return (Locations)locations.GetValue(index);
        }

        static void ListOperators(Quarter quarter)
        {
            Console.WriteLine($"Operadores del cuartel: '{quarter.GetName()}' \n");
            foreach (Operator op in quarter.GetOperators())
            {
                Console.WriteLine(op.ToStringStateOnly() +
                    "\n------------------------------------");

            }
        }

        static void ListOperators(Quarter quarter, Locations location)
        {
            Console.WriteLine($"Operadores del cuartel '{quarter.GetName()}' en {location.ToString().Replace('_', ' ')}\n");
            foreach (Operator op in quarter.GetOperators(location))
            {
                Console.WriteLine(op.ToStringStateOnly() +
                    "\n------------------------------------");
            }
        }

        static void ListOperatorsInLocation(Quarter quarter)
        {
            Locations selectedLocation = SelectLocation();
            Console.WriteLine();
            ListOperators(quarter, selectedLocation);
        }

        static Locations SelectLocation()
        {
            int locationsAmount = Enum.GetValues(typeof(Locations)).Length;
            string locationsList = "Listado de localizaciones\n" + LocationsListToString() + "\nSelecciona una localización";
            int option = ShowMenu(locationsList, locationsAmount);
            Locations selectedLocation;

            while (!FindLocation(option, out selectedLocation))
            {
                option = ShowMenu(locationsList, locationsAmount);
            }

            return selectedLocation;
        }

        static string LocationsListToString()
        {
            string locationsString = string.Empty;
            Array locations = Enum.GetValues(typeof(Locations));

            for (int i = 0; i < locations.Length; i++)
            {
                locationsString += $"{i}- {locations.GetValue(i).ToString().Replace('_', ' ')}\n";
            }

            return locationsString;
        }       
        
        static int ShowMenu(string options, int optAmount)
        {
            int option = -1;           

            while (option > optAmount || option < 0)
            {
                Console.Clear();
                Console.WriteLine(options);
                try
                {
                    option = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Por favor ingrese solo números.");
                    Console.ReadKey();
                }
            }

            return option;
        }       
        
        static bool FindLocation(int index, out Locations location)
        {
            bool found = true;
            Array locations = Enum.GetValues(typeof(Locations));

            try
            {
                location = (Locations)locations.GetValue(index);
            }
            catch (IndexOutOfRangeException)
            {
                found = false;
                location = Locations.Cuartel;
                Console.WriteLine("No se encontro la locación");
                Console.ReadKey();
            }

            return found;
        }
        
        static void TotalRecall(Quarter quarter)
        {
            if (quarter.TotalRecall())
            {
                Console.WriteLine("Todos los operadores en el cuartel!");
            }
            else
            {
                Console.WriteLine("Hubo un error, algunos operadores no pudieron volver a base...");
            }
        }

        static void UseOperator(Quarter quarter)
        {
            string opt4Sub =
                "1) Enviarlo a una localización especial.\n" +
                "2) Indicar retorno al cuartel. \n" +
                "3) Cambiar estado. \n" +
                "0) Volver atrás \n" +
                "Seleccione una opción";
            Operator op;
            string entry = string.Empty;

            while (!entry.Equals("-1"))
            {
                Console.Clear();
                Console.WriteLine("Ingrese el ID del operador (-1 para volver atrás)");
                entry = Console.ReadLine();
                FindOperator(entry, quarter, out op);

                if (op != null && op.IsStandBy())
                {
                    Console.Clear();
                    Console.WriteLine("Selecciona otro operador, este está en Stand By");
                    Console.ReadKey();
                }

                if (op != null && !op.IsStandBy())
                {
                    int option = ShowMenu(opt4Sub, 3);
                    if (option == 1)
                    {
                        Locations selectedLocation = SelectLocation();
                        Console.Clear();
                        if(quarter.MakeOperatorTravel(op.GetId(), selectedLocation))
                        {
                            Console.WriteLine("Viaje realizado con éxito!");
                            Console.WriteLine(quarter.GetOperator(op.GetId()).ToString());
                        }
                        else
                        {
                            Console.WriteLine("No se pudo realizar el viaje...");
                        }
                        entry = "-1";
                    }
                    if (option == 2)
                    {
                        Console.Clear();
                        quarter.MakeOperatorTravel(op.GetId(), Locations.Cuartel);
                        Console.WriteLine("Operador enviado a base!");
                        entry = "-1";
                    }
                    if (option == 3)
                    {
                        Console.Clear();
                        quarter.ChangeOperatorState(op.GetId(), true);
                        Console.WriteLine("Operador en Stand By.");
                        entry = "-1";
                    }
                }
               
            }            
        }

        static bool FindOperator(string entry, Quarter quarter, out Operator op)
        {
            bool found = true;

            try
            {
                int id = int.Parse(entry);
                op = quarter.GetOperator(id);
            }
            catch (FormatException)
            {
                op = null;
                found = false;
                Console.WriteLine("Ingrese solo numeros.");
                Console.ReadKey();
            }

            if (op == null)
                found = false;

            return found;
        }        
    }
}
