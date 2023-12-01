using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TP_Integrador.Territory;
using TP_Integrador.Territory.Locations;

namespace TP_Integrador
{
    internal class SaveSystem
    {
        private static SaveSystem _instance;
        private static string homePath = Path.GetFullPath("../../");
        private static string filesPath = homePath + "Files/";
        private static string mapFilePath = filesPath;
        private static string mapFileName = "map.map";        

        private SaveSystem() { }

        public static SaveSystem GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SaveSystem();
            }
            return _instance;
        }

        public bool SaveMap(Map map)
        {
            bool saved = false;

            if (!Directory.Exists(mapFilePath))
            {
                Directory.CreateDirectory(mapFilePath);
                Console.WriteLine($"Directorio de mapas creado en {mapFilePath}");
            }

            if (!File.Exists(mapFilePath + mapFileName))
            {
                try
                {
                    if(map == null)
                    {
                        throw new NoNullAllowedException();
                    }
                    File.Create(mapFilePath + mapFileName);
                    JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true};
                    string jsonString = string.Empty;
                    foreach (Location location in map.GetMap().Values)
                    {
                        jsonString += JsonSerializer.Serialize(location, options) + "\n";
                    }
                                        
                    StreamWriter streamWriter = new StreamWriter(mapFilePath + mapFileName);
                    streamWriter.Write(jsonString);
                    
                }
                catch (NoNullAllowedException nullException)
                {
                    Console.WriteLine("Error, el mapa es nulo...");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error...");
                }
            }
            else
            {
                Console.WriteLine("El mapa no existe");
            }

            return saved;
        }
    }
}
