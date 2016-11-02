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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApiDataController : ApiController
    {
        // GET: api/ApiData
        [EnableCors(origins: "*", headers: "*", methods: "get")]
        public Respuesta Get(int id)
        {
            Respuesta rpt = new Respuesta();

            List<DataConsumos> ListConsumos = new List<DataConsumos>();
            List<DataPagos> ListPagos = new List<DataPagos>();
            DeudaMes Deuda = new DeudaMes();

            int OK = 0;
            string connectionString = "Data Source=192.168.50.109;Initial Catalog=SComSeal;User ID=usuarioreporte;Password=reporte";
            string queryTipo = "[dbo].[spSuministroBuscaPorFiltro]";
            string queryStringConsumoMenor = "[dbo].[spSuministroCtaCteMenorConsultaPorCodigo]";
            string queryStringConsumosMayor = "[dbo].[spsuministroctactemayorconsultaporcodigo]";
            string queryStringPagos = "[dbo].[spSuministroCobranzaConsultaPorCodigo]";
            string queryStringDeudaMes = "[dbo].[spSuministroConsultaDatosComerciales]";

            String MayorMenor = "";



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryTipo, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@ItemBuscarPor1", "1"));
                command.Parameters.Add(new SqlParameter("@DatoBuscar1", Convert.ToString(id)));
                command.Parameters.Add(new SqlParameter("@ItemTipoBusqueda1", "0"));
                command.Parameters.Add(new SqlParameter("@ItemBuscarPor2", "0"));
                command.Parameters.Add(new SqlParameter("@DatoBuscar2", ""));
                command.Parameters.Add(new SqlParameter("@ItemTipoBusqueda2", "2"));
                command.Parameters.Add(new SqlParameter("@CodigoError", "0"));
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                        
                            try
                            {
                                MayorMenor = reader["MayorMenor"].ToString();
                                if (reader["NombreEstadoSuministro"].ToString() == "Normal")
                                {
                                    rpt.ERROR = 0;
                                }
                                else
                                {
                                    rpt.ERROR = 1;
                                    rpt.ERROR_MSG = "Suministro inactivo";
                                }
                            }
                            catch (Exception ex)
                            {

                            }                           
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {

                }
                String queryStringConsumos = "";
                if (rpt.ERROR == 0)
                {
                    if (MayorMenor == "0")
                    {
                        queryStringConsumos = queryStringConsumoMenor;
                    }
                    else
                    {
                        queryStringConsumos = queryStringConsumosMayor;
                    }
                    //---------------------------------------------------
                    //------------------------------------------------------------------------------
                    command = new SqlCommand(queryStringDeudaMes, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CodigoSuministro", id));
                    try
                    {
                        //connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            try
                            {
                                OK = 1;
                                String _UltimoDiaPago = "";
                                try
                                {
                                    _UltimoDiaPago = Convert.ToDateTime(reader["UltimoDiaPago"]).ToString("yyyy-MM-dd");
                                }
                                catch (Exception ex)
                                {

                                }
                                String _MontoDeudaTotal = Convert.ToDecimal(reader["MontoDeudaTotal"]).ToString("#.00");
                                int _PeriodoActual = Convert.ToInt32(reader["PeriodoActual"]);

                                Deuda.UltimoDiaPago = _UltimoDiaPago;
                                Deuda.MontoDeudaTotal = _MontoDeudaTotal;
                                Deuda.PeriodoActual = _PeriodoActual;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }

                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                    //----------------------------------------------------------------------

                    command = new SqlCommand(queryStringConsumos, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CodigoSuministro", id));
                    command.Parameters.Add(new SqlParameter("@NumeroMeses", "6"));
                    try
                    {
                        //connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            try
                            {
                                OK = 1;
                                int _Periodo = Convert.ToInt32(reader["Periodo"]);

                                if (_Periodo == Deuda.PeriodoActual)
                                {
                                    if (reader["Situacion"].ToString() == "Normal")
                                    {
                                        rpt.ERROR = 0;
                                    }
                                    else
                                    {
                                        rpt.ERROR = 1;
                                        rpt.ERROR_MSG = "Suministro inactivo";
                                    }
                                }

                                //String _CodigoSuministro = reader["CodigoSuministro"].ToString();
                                String _FechaPago = "";
                                try
                                {
                                    _FechaPago = Convert.ToDateTime(reader["FechaPago"]).ToString("yyyy-MM-dd");
                                }
                                catch (Exception ex)
                                {

                                }
                                String _C_EA = Math.Round(Convert.ToDecimal(reader["C_EA"]), 2) .ToString("#.00");
                                String _NroRecibo = reader["NroRecibo"].ToString();
                                String _Tarifa = reader["Tarifa"].ToString();

                                ListConsumos.Add(new DataConsumos()
                                {
                                    CodigoSuministro = "",
                                    C_EA = _C_EA,
                                    FechaPago = _FechaPago,
                                    NroRecibo = _NroRecibo,
                                    Periodo = _Periodo,
                                    Tarifa = _Tarifa
                                });
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                    //------------------------------------------------------------------------------
                    command = new SqlCommand(queryStringPagos, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CodigoSuministro", id));
                    command.Parameters.Add(new SqlParameter("@NumeroMeses", "6"));
                    try
                    {
                        //connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            try
                            {
                                OK = 1;
                                int _CodigoPeriodoComercial = Convert.ToInt32(reader["CodigoPeriodoComercial"]);
                                String _CodigoSuministro = reader["CodigoSuministro"].ToString();
                                String _FechaDigitacion = "";
                                try
                                {
                                    _FechaDigitacion = Convert.ToDateTime(reader["FechaDigitacion"]).ToString("yyyy-MM-dd");
                                }
                                catch (Exception ex)
                                {

                                }
                                String _MontoCobrado = Convert.ToDecimal(reader["MontoCobrado"]).ToString("#.00");
                                long _NumeroComprobante = Convert.ToInt64(reader["NumeroComprobante"]);
                                String _NombreContratista = reader["NombreContratista"].ToString();

                                ListPagos.Add(new DataPagos()
                                {
                                    CodigoPeriodoComercial = _CodigoPeriodoComercial,
                                    CodigoSuministro = _CodigoSuministro,
                                    FechaDigitacion = _FechaDigitacion,
                                    MontoCobrado = _MontoCobrado,
                                    NumeroComprobante = _NumeroComprobante,
                                    NombreContratista = _NombreContratista
                                });
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }

                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            List<CortesProgramados> cortes = new List<CortesProgramados>();
            try
            {
                DBComercialEntities db = new DBComercialEntities();
                db.SPVisitas();
                cortes = db.CortesProgramados.OrderByDescending(c => c.Fecha).Take(6).ToList();
                db.Dispose();
            }
            catch (Exception ex)
            {

            }
            
            rpt.OK = OK;
            rpt.Consumos = ListConsumos;
            rpt.Pagos = ListPagos;
            rpt.Deuda = Deuda;
            rpt.Cortes = cortes;
            return rpt;
        }
    }
}
