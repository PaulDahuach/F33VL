using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

using ASP.NET_MVC_Application.Models;
using Newtonsoft.Json;

namespace ASP.NET_MVC_Application.Controllers
{
    public class MaestroController : Controller
    {
        // GET: Maestro
        public ActionResult Index()
        {
            // Load the data for the Maestro
            var Maestros = Maestro.GetMaestros();

            // Return the view.
            return View(Maestros);
        }

        public ActionResult Create()
        {
            ViewBag.Submitted = false;
            var created = false;
            // Crear el Maestro
            if (HttpContext.Request.RequestType == "POST")
            {
                ViewBag.Submitted = true;
                // If the request is POST, get the values from the form
                var id              = Request.Form["id"];
                var denominacion    = Request.Form["denominacion"];
                var detalle         = Request.Form["detalle"];
                var valido          = false;
                
                if(Request.Form["valido"] == "on" || Request.Form["valido"] == "true") {
                    valido = true;
                }

                // Crear un nuevo objeto Maestro para estos detalles.
                Maestro Maestro = new Maestro()
                {
                    ID = Convert.ToInt16(id), 
                    Denominacion = denominacion,
                    Detalle = detalle,
                    Valido = Convert.ToBoolean(valido)
                };

                // Guardar el nuevo Maestro en la lista de Maestros
                var MaestroFile = Maestro.MaestroFile;
                var MaestroData = System.IO.File.ReadAllText(MaestroFile);
                List<Maestro> MaestroList = new List<Maestro>();
                MaestroList = JsonConvert.DeserializeObject<List<Maestro>>(MaestroData);
                if (MaestroList == null)
                {
                    MaestroList = new List<Maestro>();
                }
                MaestroList.Add(Maestro);

                // Ahora guardar la lista en disco
                System.IO.File.WriteAllText(MaestroFile, JsonConvert.SerializeObject(MaestroList));

                // marcar que se ha creado el maestro
                created = true;
            }

            if (created)
            {
                ViewBag.Message = "El nuevo Maestro se ha creado con éxito.";
            }
            else
            {
                ViewBag.Message = "Hubo un error mientras se creaba el Maestro.";
            }
            return View();
        }

        public ActionResult Update(int id)
        {
            if (HttpContext.Request.RequestType == "POST")
            {
                // Si es un POST, debe ser un form que nos estan enviando
                var denominacion  = Request.Form["denominacion"];
                var detalle       = Request.Form["detalle"];
                var valido        = Request.Form["valido"];

                // Recuperar todos los registros de Maestro
                var maestros = Maestro.GetMaestros();

                foreach (Maestro Maestro in maestros)
                {
                    // Find the Maestro
                    if (Maestro.ID == id)
                    {
                        // Maestro encontrado, ahora actualizar sus propiedades y guardar
                        Maestro.Denominacion=denominacion;
                        Maestro.Detalle = detalle;
                        Maestro.Valido = Convert.ToBoolean(valido == "on"? true: false);
                        break;
                    }
                }

                // Actualizar los maestros en disco
                System.IO.File.WriteAllText(Maestro.MaestroFile, JsonConvert.SerializeObject(maestros));

                // Agregar los detalle a la Vista
                Response.Redirect("~/Maestro/Index?Message=Maestro_Actualizado");
            }


            // Crear un objeto modelo.
            var clnt = new Maestro();
            // Obtener la lista de maestros
            var Maestros = Maestro.GetMaestros();
            // Buscar dentro de los Maestros
            foreach (Maestro Maestro in Maestros)
            {
                // Si el Id del maestro coincide...
                if (Maestro.ID == id)
                {
                    clnt = Maestro;
                }
                // No hay necesidad de seguir ejecutando la iteración
                break;
            }
            if (clnt == null)
            {
                // No se encontró ningun Maestro
                ViewBag.Message = "No se encontraron registros de Maestro";
            }
            return View(clnt);
        }

        public ActionResult Delete(int id)
        {
            // Obtener los maestros
            var Maestros = Maestro.GetMaestros();
            var deleted = false;
            // Eliminar el nombr específico
            foreach (Maestro Maestro in Maestros)
            {
                // Encontramos el registro
                if (Maestro.ID == id)
                {
                    // eliminar este maestro
                    var index = Maestros.IndexOf(Maestro);
                    Maestros.RemoveAt(index);

                    // Eliminado, ahora guardamos nuevamente los datos.
                    System.IO.File.WriteAllText(Maestro.MaestroFile, JsonConvert.SerializeObject(Maestros));
                    deleted = true;
                    break;
                }
            }

            // Add the process details to the ViewBag
            if (deleted)
            {
                ViewBag.Message = "El maestro fue eliminado con éxito.";
            }
            else
            {
                ViewBag.Message = "Hubo un error mientras se elimiaba el MaestroThere was an error while deleting the Maestro.";
            }
            return View();
        }
    }
}