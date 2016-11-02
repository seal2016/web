using SealPhoneAngular2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SealPhoneAngular2.Controllers
{
    public class TestController : ApiController
    {
        // GET: api/Login
        [EnableCors(origins: "*", headers: "*", methods: "get")]
        public Respuesta Get(int id)
        {
            List<DataConsumos> ListConsumos = new List<DataConsumos>();
            List<DataPagos> ListPagos = new List<DataPagos>();

            /*ListConsumos.Add(new DataConsumos()
            {
                C_EA = "1682.00",
                Periodo = 201609,
            });

            ListConsumos.Add(new DataConsumos()
            {
                C_EA = 1600.00m,
                Periodo = 201608,
            });

            ListConsumos.Add(new DataConsumos()
            {
                C_EA = 1582.00m,
                Periodo = 201607,
            });

            ListConsumos.Add(new DataConsumos()
            {                
                C_EA = 1700.00m,         
                Periodo = 201606,                
            });

            ListConsumos.Add(new DataConsumos()
            {
                C_EA = 1610.00m,
                Periodo = 201605,
            });

            ListConsumos.Add(new DataConsumos()
            {
                C_EA = 1650.00m,
                Periodo = 201604,
            });
            //--------------------------
            ListPagos.Add(new DataPagos()
            {
                CodigoPeriodoComercial = 201609,                
                MontoCobrado = 16.00m
                
            });

            ListPagos.Add(new DataPagos()
            {
                CodigoPeriodoComercial = 201608,
                MontoCobrado = 19.00m

            });

            ListPagos.Add(new DataPagos()
            {
                CodigoPeriodoComercial = 201607,
                MontoCobrado = 11.00m

            });

            ListPagos.Add(new DataPagos()
            {
                CodigoPeriodoComercial = 201605,
                MontoCobrado = 13.00m

            });

            ListPagos.Add(new DataPagos()
            {
                CodigoPeriodoComercial = 201604,
                MontoCobrado = 15.00m

            });

            ListPagos.Add(new DataPagos()
            {
                CodigoPeriodoComercial = 201603,
                MontoCobrado = 18.00m

            });*/

            Respuesta rpt = new Respuesta();
                rpt.OK = 1;
                rpt.Consumos = ListConsumos;
                rpt.Pagos = ListPagos;


                return rpt;
            }

        }
    }
