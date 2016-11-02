using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SealPhoneAngular2.Models
{
    public class Respuesta
    {
        public Respuesta()
        {
            Consumos = new List<DataConsumos>();
            Pagos = new List<DataPagos>();
        }
        public int OK;
        public int ERROR = 1;
        public string ERROR_MSG = "Suministro no encontrado";
        public List<DataConsumos> Consumos;
        public List<DataPagos> Pagos;
        public DeudaMes Deuda;
        public List<CortesProgramados> Cortes;
    }

    public class DataConsumos
    {
        public string CodigoSuministro;
        public int Periodo;
        public String Tarifa;
        public String C_EA;
        public String FechaPago;
        public String NroRecibo;
    }

    public class DataPagos
    {
        public string CodigoSuministro;
        public int CodigoPeriodoComercial;
        public long NumeroComprobante;
        public String MontoCobrado;
        public String FechaDigitacion;
        public String NombreContratista;
    }

    public class DeudaMes
    {
        public int PeriodoActual;
        public String MontoDeudaTotal;
        public String UltimoDiaPago;
    }

    public class UserAD
    {
        public String DisplayName { get; set; }
        public String Mail { get; set; }
        public String AccountName { get; set; }
        public String Password { get; set; }
        public bool UserAdmin { get; set; }
        public bool Responsable { get; set; }
        public String Area { get; set; }
    }
}