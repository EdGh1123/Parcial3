using Microsoft.AspNet.Identity;
using Parcial3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parcial3.Controllers
{
    public class MJController : Controller
    {
        // GET: MJ
        TelPersEntities1 Conexion = new TelPersEntities1();
        public ActionResult Index()
        {
            var usuario = User.Identity.GetUserId();
            var oUser = (from d in Conexion.AspNetUsers
                            where (d.Id != usuario)
                            select d).ToList<AspNetUsers>();
            var user = (from d in Conexion.AspNetUsers
                            where (d.Id == usuario)
                            select d).ToList<AspNetUsers>();

            ViewBag.user=new SelectList(user, "Id", "PhoneNumber");
            ViewBag.user2=new SelectList(oUser, "Id", "PhoneNumber");
            return View();
        }
        public ActionResult Conversacion(string IdPersona, string IdPersona2)
        {
                var conversacion = (from d in Conexion.Conversacion
                                where (d.Id_persona1 == IdPersona.ToString() && d.Id_persona2 == IdPersona2.ToString()) || (d.Id_persona1 == IdPersona2.ToString() && d.Id_persona2 == IdPersona.ToString())
                                select d).FirstOrDefault();
            if (conversacion == null)
            {
                Conversacion conv = new Conversacion();
                conv.Id_persona1 = IdPersona.ToString();
                conv.Id_persona2 = IdPersona2.ToString();
                Conexion.Conversacion.Add(conv);
                Conexion.SaveChanges();
                var id = Conexion.Conversacion.Max(e => e.Id);
                Session["IdConversacion"] = id;
                return RedirectToAction("Index", "Mensajes");
            }
            else
            {
                Session["IdConversacion"] = conversacion.Id;
                return RedirectToAction("Index", "Mensajes");

            }
            
        }
    }
}