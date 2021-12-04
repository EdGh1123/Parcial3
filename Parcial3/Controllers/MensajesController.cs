using Parcial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parcial3.Controllers
{
    public class MensajesController : Controller
    {
        // GET: Mensajes
        TelPersEntities1 Conexion = new TelPersEntities1();
        public ActionResult Index()
        {
            var id = long.Parse(System.Web.HttpContext.Current.Session["IdConversacion"].ToString());
            if(id != null)
            {
                var oMensajeDetalles = (from d in Conexion.Mensajes
                                        where d.ConversacionId == id
                                        select d).ToList<Mensajes>();

                ViewBag.Mensajes = oMensajeDetalles;
            }

            return View();
        }
        public ActionResult Crear(string Mensaje)
        {
            var id = long.Parse(System.Web.HttpContext.Current.Session["IdConversacion"].ToString());
            Mensajes Men = new Mensajes();
            Men.ConversacionId = id;
            Men.CostoMensaje = 200;
            Men.MensajeDescripcion = Mensaje;

            Conexion.Mensajes.Add(Men);
            Conexion.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Cancelar()
        {
            
            return RedirectToAction("Index","Home");
        }
    }
}