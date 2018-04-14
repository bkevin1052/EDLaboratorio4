using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDLaboratorio4.DBContext;
using EDLaboratorio4.Models;
using Newtonsoft.Json;
using System.Linq;
namespace EDLaboratorio4.Controllers
{
    public class AlbumController : Controller
    {
        DefaultConnection db = DefaultConnection.getInstance;
        // GET: Album
        public ActionResult Index()
        {
            return View();
        }

        // GET: Album/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Album/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Album/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Album/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Album/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Album/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Album/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CargaArchivoAlbum()
        {
            return View();
        }

        //Post SubirArchivoPaises
        [HttpPost]
        public ActionResult CargaArchivoAlbum(HttpPostedFileBase file)
        {

            string filePath = string.Empty;
            Archivo modelo = new Archivo();
            if (file != null)
            {
                string ruta = Server.MapPath("~/Temp/");

                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                filePath = ruta + Path.GetFileName(file.FileName);

                string extension = Path.GetExtension(file.FileName);

                file.SaveAs(filePath);
                string[] separadores = { ",", "]" };
                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    Pais temp = new Pais();
                    LeerArchivo(json, 0, temp);
                }
                modelo.SubirArchivo(ruta, file);

            }

            return View();
        }

        public Pais LeerArchivo(dynamic json, int contador, Pais pais)
        {
            if (json != null)
            {
                if (contador == 0 || json.Value == null)
                {
                    if (contador == 0)
                    {
                        contador += 1;
                    }
                    dynamic array = JsonConvert.DeserializeObject(json.ToString());
                    foreach (var item in array)
                    {
                        pais = LeerArchivo(item, contador, pais);
                    }
                }
                else
                {
                    dynamic array = JsonConvert.DeserializeObject(json.Value.ToString());
                    if (contador == 1)
                    {
                        string llave = json.Name;
                        pais.Nombre = llave;
                        contador += 1;
                        foreach (var item in array)
                        {
                            pais = LeerArchivo(item, contador, pais);
                        }
                    }
                    else
                    {
                        if (json.Name == "faltantes")
                        {
                            foreach (var item in array)
                            {
                                Calcomania ncalcomania = new Calcomania();
                                ncalcomania.Estado = "Faltante";
                                ncalcomania.Numero = Convert.ToInt16(item.Value);
                                pais.Faltantes.Add(ncalcomania);
                            }
                            return pais;
                        }

                        if (json.Name == "coleccionadas")
                        {
                            foreach (var item in array)
                            {
                                Calcomania ncalcomania = new Calcomania();
                                ncalcomania.Estado = "Coleccionada";
                                ncalcomania.Numero = Convert.ToInt16(item.Value);
                                pais.Coleccionadas.Add(ncalcomania);
                            }
                            return pais;
                        }

                        if (json.Name == "cambios")
                        {
                            foreach (var item in array)
                            {
                                Calcomania ncalcomania = new Calcomania();
                                ncalcomania.Estado = "Disponible para cambio";
                                ncalcomania.Numero = Convert.ToInt16(item.Value);
                                pais.DisponibleCambio.Add(ncalcomania);
                            }
                            DefaultConnection.Album.Add(pais.Nombre, pais);
                            pais = null;
                            pais = new Pais();
                            return pais;

                        }
                    }
                }
            }
            return pais;
        }

    }
}
