using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace ASP.NET_MVC_Application.Models
{
    public class Maestro
    {
        public static string MaestroFile = HttpContext.Current.Server.MapPath("~/App_Data/Maestros.json");

        public int ID { get; set; }
        public string Denominacion { get; set; }
        public string Detalle { get; set; }
        public bool Valido { get; set; }

        public static List<Maestro> GetMaestros()
        {
            List<Maestro> Maestros = new List<Maestro>();
            if (File.Exists(MaestroFile))
            {
                // El maestro existe..
                string content = File.ReadAllText(MaestroFile);
                // Deserializar los objetos
                Maestros = JsonConvert.DeserializeObject<List<Maestro>>(content);

                // Devuelve la lista de registros de Maestro, ya sea vacia o completa.
                return Maestros;
            }
            else
            {
                // Crear el maestro
                File.Create(MaestroFile).Close();
                // Escribir algun dato en él; [] significa una matriz vacía, 
                File.WriteAllText(MaestroFile, "[]");

                // Volver a ejecutar la funcion ahora que tenemos el maestro
                GetMaestros();
            }
            return Maestros;
        }
    }
}