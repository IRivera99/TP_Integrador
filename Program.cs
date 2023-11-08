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
        static Menu menu = Menu.InicializeMenu();

        static void Main(string[] args)
        {           
            Quarter cuartel = new Quarter("Cuartel General");        

            AddRandomOperatorsToQuarter(cuartel, 20);

            int option = -1;

            while(option != 0)
            {
                option = menu.ShowMainMenu();
                Console.WriteLine();

                if (option == 1)
                {
                    PrintOperators(cuartel.GetOperators());
                    Console.ReadKey();
                }                    

                if (option == 2)
                {
                    PrintOperatorsInLocation(cuartel);
                    Console.ReadKey();
                }                    

                if (option == 3)
                {
                    TotalRecallToQuarter(cuartel);
                    Console.ReadKey();
                }                    

                if (option == 4)
                {
                    UseOperatorFromQuarter(cuartel);
                    Console.ReadKey();
                }

                if (option == 5)
                {
                    AdminOperatorsFromQuarter(cuartel);
                }
            }                            
            
        }

        //Quarters functions
        static void AddRandomOperatorsToQuarter(Quarter quarter, int amount)
        {
            Random random = new Random();

            for (int i = 0; i < amount; i++)
            {
                bool added = false;
                Operator op = null;
                Locations location = GetRandomLocation(random);
                OperatorTypes type = GetRandomOperatorType(random);

                while (!added)
                {
                    int id = quarter.GetMaxId() + 1;

                    if (type == OperatorTypes.UAV)
                        op = new UAV(id, random.Next(30, 60), location);

                    if (type == OperatorTypes.K9)
                        op = new K9(id, random.Next(20, 50), location);

                    if (type == OperatorTypes.M8)
                        op = new M8(id, random.Next(15, 40), location);

                    added = quarter.AddOperator(op);
                }
            }

            Console.WriteLine(quarter.GetOperators().Count);
        }

        static void TotalRecallToQuarter(Quarter quarter)
        {
            Console.Clear();
            if (quarter.TotalRecall())
            {
                Console.WriteLine("Todos los operadores en el cuartel!");
            }
            else
            {
                Console.WriteLine("Algunos operadores no pudieron volver a base...");
            }
        }

        static void PrintOperators(List<Operator> operators)
        {
            foreach (Operator op in operators)
            {
                Console.WriteLine(op.ToStringStateOnly() +
                    "\n------------------------------------");

            }
        }

        static void PrintOperatorsInLocation(Quarter quarter)
        {
            Locations selectedLocation = SelectLocation();
            if (selectedLocation != (Locations)(-1))
            {
                List<Operator> operatorsInLocation = quarter.GetOperators().FindAll(o => o.GetLocation() == selectedLocation);
                Console.Clear();
                if (operatorsInLocation.Count > 0)
                {
                    Console.WriteLine($"Operadores en {selectedLocation}\n");
                    PrintOperators(operatorsInLocation);
                }
                else
                {
                    Console.WriteLine($"No hay operadores en {selectedLocation}");
                }
            }
        }

        static void UseOperatorFromQuarter(Quarter quarter)
        {
            Operator op;
            int id = 0;

            while (id != -1)
            {
                Console.Clear();
                Console.WriteLine("Ingrese el ID del operador (-1 para volver atrás)");
                id = InputControl.ReadIntOnly();
                op = quarter.GetOperator(id);

                if (op != null && op.IsStandBy())
                {
                    Console.Clear();
                    Console.WriteLine("Selecciona otro operador, este está en Stand By");
                    Console.ReadKey();
                }

                if (op != null && !op.IsStandBy())
                {
                    int option = menu.ShowOp4Menu();
                    if (option == 1)
                    {
                        Locations selectedLocation = SelectLocation();
                        if (selectedLocation != (Locations)(-1))
                        {
                            Console.Clear();
                            if (quarter.MakeOperatorTravel(op.GetId(), selectedLocation))
                            {
                                Console.WriteLine("Viaje realizado con éxito!");
                                Console.WriteLine(quarter.GetOperator(op.GetId()).ToString());
                            }
                            else
                            {
                                Console.WriteLine("No se pudo realizar el viaje...");
                            }
                            id = -1;
                        }
                    }
                    if (option == 2)
                    {
                        Console.Clear();
                        quarter.MakeOperatorTravel(op.GetId(), Locations.Cuartel);
                        Console.WriteLine("Operador enviado a base!");
                        id = -1;
                    }
                    if (option == 3)
                    {
                        Console.Clear();
                        quarter.ChangeOperatorState(op.GetId(), true);
                        Console.WriteLine("Operador en Stand By.");
                        id = -1;
                    }
                }
            }
        }        

        static void AdminOperatorsFromQuarter(Quarter quarter)
        {
            Console.Clear();
            int option = -1;
            while (option != 0)
            {
                option = menu.ShowOp5Menu();
                if (option == 1)
                {
                    int subOption = menu.ShowOp5AddMenu();

                    if (subOption != 0)
                    {
                        Random random = new Random();
                        Operator op = null;
                        Locations location = GetRandomLocation(random);
                        OperatorTypes type = (OperatorTypes)(-1);
                        bool added = false;

                        if (subOption == 1)
                        {
                            type = GetRandomOperatorType(random);
                        }

                        if (subOption == 2)
                        {
                            type = SelectOperatorType();
                        }

                        while (!added)
                        {
                            int id = quarter.GetMaxId() + 1;

                            if (type == OperatorTypes.UAV)
                                op = new UAV(id, random.Next(30, 60), location);

                            if (type == OperatorTypes.K9)
                                op = new K9(id, random.Next(20, 50), location);

                            if (type == OperatorTypes.M8)
                                op = new M8(id, random.Next(15, 40), location);

                            added = quarter.AddOperator(op);
                        }

                        Console.Clear();
                        Console.WriteLine("Operador añadido con éxito");
                        Console.WriteLine(op.ToString());
                        Console.ReadLine();
                    }
                }
                if (option == 2)
                {
                    int subOption = menu.ShowOp5DelMenu();
                    if (subOption != 0 && subOption == 1) 
                    {
                        Random random = new Random();
                        int id = random.Next(1,quarter.GetMaxId());
                        if (quarter.RemoveOperator(id))
                        {
                            Console.WriteLine($"Operador con eliminado con éxito!(ID {id})");
                        }
                        else
                        {
                            Console.WriteLine("Operador no encontrado...");
                        }
                        Console.ReadKey();
                    }
                    if (subOption != 0 && subOption == 2)
                    {
                        int id = 0;

                        while (id != -1)
                        {
                            Console.Clear();
                            Console.WriteLine("Ingrese el ID del operador (-1 para volver atrás)");
                            id = InputControl.ReadIntOnly();
                            bool removed = quarter.RemoveOperator(id);
                            if (removed)
                            {
                                Console.WriteLine("Operador eliminado con éxito!");
                                id = -1;
                            }
                            if (!removed && id != -1)
                            {
                                Console.WriteLine("Operador no encontrado...");
                            }
                            Console.ReadKey();
                        }
                    }
                }
            }
        }

        //Location functions
        static Locations GetRandomLocation(Random random)
        {
            Array locations = Enum.GetValues(typeof(Locations));
            int index = random.Next(0, locations.Length);
            return (Locations)locations.GetValue(index);
        }

        static Locations SelectLocation()
        {           
            Locations location;
            Array locationsArray = Enum.GetValues(typeof(Locations));
            string locationsList = "Tipo de operadores\n" + LocationsListToString() + "\nSelecciona una opción (Para cancelar ingrese -1)";
            int locationIndex = menu.ShowCustomOptionsMenu(locationsList, -1, locationsArray.Length - 1);

            try
            {
                location = (Locations)locationsArray.GetValue(locationIndex);
            }
            catch (IndexOutOfRangeException)
            {
                location = (Locations)(-1);
            }

            return location;
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
        
        //Operator types functions
        static OperatorTypes SelectOperatorType()
        {
            OperatorTypes type;            
            Array typesArray = Enum.GetValues(typeof(OperatorTypes));
            string typesList = "Tipo de operadores\n" + OperatorsListTypeToString() + "\nSelecciona una opción (Para cancelar ingrese -1)";
            int typeIndex = menu.ShowCustomOptionsMenu(typesList, -1, typesArray.Length - 1);

            try
            {
                type = (OperatorTypes)typesArray.GetValue(typeIndex);
            }
            catch (IndexOutOfRangeException)
            {
                type = (OperatorTypes)(-1);
            }

            return type;
        }

        static OperatorTypes GetRandomOperatorType(Random random)
        {
            Array typesArray = Enum.GetValues(typeof(OperatorTypes));
            int index = random.Next(0, typesArray.Length);
            return (OperatorTypes)typesArray.GetValue(index);
        }

        static string OperatorsListTypeToString()
        {
            string typesString = string.Empty;            
            Array types = Enum.GetValues(typeof(OperatorTypes));

            for (int i = 0; i < types.Length; i++)
            {
                typesString += $"{i}) {types.GetValue(i)}\n";
            }

            return typesString;
        }
    }
}
