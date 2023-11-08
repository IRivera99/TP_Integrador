using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class Menu
    {
        private static Menu _instance;

        private string mainMenuOptions = 
            "1) Listar el estado de todos los operadores.\n" +
            "2) Listar el estado de todos los operadores en una localización. \n" +
            "3) Total recall. \n" +
            "4) Seleccionar un operador. \n" +
            "5) Agregar o remover un operador. \n" +
            "0) Salir. \n" +
            "Seleccione una opción";

        private string opt4Sub =
            "1) Enviarlo a una localización especial.\n" +
            "2) Indicar retorno al cuartel. \n" +
            "3) Cambiar estado. \n" +
            "0) Volver atrás \n" +
            "Seleccione una opción";

        private string opt5Sub =
            "1) Agregar un nuevo operador.\n" +
            "2) Eliminar un operador.\n" +
            "0) Volver atrás.\n" +
            "Seleccione una opción";

        private string opt5AddMenu =
            "1) Tipo de operador random.\n" +
            "2) Especifica tipo de operador.\n" +
            "0) Volver atrás.\n" +
            "Seleccione una opción";

        private string opt5DelMenu =
            "1) Operador random.\n" +
            "2) Indicar ID del operador.\n" +
            "0) Volver atrás.\n" +
            "Seleccione una opción";

        private Menu() { }

        public static Menu InicializeMenu()
        {
            if (_instance == null)
            {
                _instance = new Menu();
            }
            return _instance;
        }

        public int ShowCustomOptionsMenu(string optionList, int minOptionQ, int maxOptionQ)
        {
            int option = -2;

            while (option > maxOptionQ || option < minOptionQ)
            {
                Console.Clear();
                Console.WriteLine(optionList);

                option = InputControl.ReadIntOnly();
            }

            return option;
        }

        public int ShowMainMenu()
        {
            return ShowCustomOptionsMenu(mainMenuOptions, 0, 5);
        }

        public int ShowOp4Menu()
        {
            return ShowCustomOptionsMenu(opt4Sub, 0, 3);
        }

        public int ShowOp5Menu()
        {
            return ShowCustomOptionsMenu(opt5Sub, 0, 2);
        }

        public int ShowOp5AddMenu() 
        {
            return ShowCustomOptionsMenu(opt5AddMenu, 0, 2);
        }

        public int ShowOp5DelMenu()
        {
            return ShowCustomOptionsMenu(opt5DelMenu, 0, 2);
        }
    }
}
