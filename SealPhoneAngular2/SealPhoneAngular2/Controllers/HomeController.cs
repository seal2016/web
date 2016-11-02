using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SealPhoneAngular2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Lat = Request.QueryString["lat"];
            ViewBag.Lon = Request.QueryString["lon"];
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ConsultaDeuda()
        {
            return View();
        }

        public ActionResult UltimosCosumos()
        {
            return View();
        }

        public ActionResult LugaresPago()
        {
            return View();
        }

        public ActionResult CortesProgramados()
        {
            return View();
        }

        public ActionResult CortesNoProgramados()
        {
            return View();
        }

        public ActionResult ConsultaReclamo()
        {
            return View();
        }

        public ActionResult ReqCambioNombre()
        {
            return View();
        }

        public ActionResult ReqNuevoSuministro()
        {
            return View();
        }

        public ActionResult ReqAumentoCarga()
        {
            return View();
        }

        public ActionResult ReqCorteDefinitivo()
        {
            return View();
        }

        public ActionResult ReqReubicacionMedidor()
        {
            return View();
        }

        public ActionResult UltimosPagos()
        {
            return View();
        }
    }
}