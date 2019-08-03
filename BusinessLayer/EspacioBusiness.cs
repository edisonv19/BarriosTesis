using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class EspacioBusiness
    {
        // Diccionario de relación de RRCC y zonas
        public static readonly Dictionary<string, string> zonas = new Dictionary<string, string>()
        {
            {"101","9"},
            {"102","9"},
            {"103","10"},
            {"104","10"},
            {"105","10"},
            {"106","10"},
            {"107","10"},
            {"108","10"},
            {"109","11"},
            {"110","10"},
            {"111","11"},
            {"112","12"},
            {"113","11"},
            {"114","11"},
            {"115","9"},
            {"116","9"},
            {"117","11"},
            {"118","11"},
            {"201","10"},
            {"202","14"},
            {"203","14"},
            {"204","14"},
            {"205","13"},
            {"206","13"},
            {"207","14"},
            {"208","14"},
            {"209","13"},
            {"210","14"},
            {"211","10"},
            {"212","10"},
            {"213","10"},
            {"214","14"},
            {"215","13"},
            {"301","12"},
            {"302","12"},
            {"303","12"},
            {"304","13"},
            {"305","13"},
            {"306","13"},
            {"307","13"},
            {"308","3"},
            {"309","3"},
            {"310","13"},
            {"311","13"},
            {"312","13"},
            {"313","3"},
            {"314","3"},
            {"315","2"},
            {"316","3"},
            {"401","1"},
            {"402","11"},
            {"403","11"},
            {"404","11"},
            {"405","12"},
            {"406","12"},
            {"407","12"},
            {"408","12"},
            {"409","12"},
            {"410","12"},
            {"411","12"},
            {"412","2"},
            {"413","2"},
            {"414","2"},
            {"415","11"},
            {"416","1"},
            {"417","1"},
            {"1201","8"},
            {"1202","16"},
            {"1203","15"},
            {"1204","16"},
            {"1205","7"},
            {"1206","15"},
            {"1207","15"},
            {"1208","15"},
            {"1209","15"},
            {"1210","15"},
            {"1211","15"},
            {"1212","15"},
            {"1213","9"},
            {"1214","9"},
            {"1215","8"},
            {"1216","Fuera de zona"},
            {"1217","Fuera de zona"},
            {"1218","8"},
            {"1219","8"},
            {"1220","7"},
            {"1221","7"},
            {"1301","6"},
            {"1302","7"},
            {"1303","6"},
            {"1304","7"},
            {"1305","7"},
            {"1306","6"},
            {"1307","6"},
            {"1308","6"},
            {"1309","5"},
            {"1310","5"},
            {"1312","Fuera de zona"},
            {"1313","4"},
            {"1314","13"},
            {"1315","13"},
            {"1316","5"},
            {"1317","13"},
            {"1318","13"},
            {"1319","13"},
            {"1320","6"},
            {"1311","5"},
            {"-1", "Fuera de zona"}
        };

        public static Espacio GetEspacioByCodigo(Espacio espacio)
        {
            var EspacioDA = new EspacioDataAccess();
            DataSet ds = EspacioDA.GetEspacioByCodigo(espacio);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return Espacio.GetFromDataRow(ds.Tables[0].Rows[0]);
            }
            return null;
        }

        public static List<Espacio> GetEspacioByFilter(Espacio espacio)
        {
            var EspacioDA = new EspacioDataAccess();
            DataSet ds = EspacioDA.GetEspacioByFilter(espacio);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return Espacio.GetFromDS(ds);
            }
            return null;
        }

        // inserta espacios y retorna los ids
        public static List<Espacio> InsertEspacios(List<Espacio> espacios)
        {
            var CodigoBS = new CodigoBusiness();
            var EspacioDA = new EspacioDataAccess();

            List<Espacio> espaciosNew = new List<Espacio>();

            // get idCategoria => Radio censal
            int? idCategoria = CodigoBS.GetCodigoByClave(new Codigo() { Grupo = "CategoriaEspacio", Clave = "RadioCensal" }).IdCodigo;

            // insert space
            foreach (Espacio espacio in espacios)
            {
                espacio.IdCategoria = idCategoria;
                espacio.IdPadre = GetEspacioByCodigo(new Espacio() { Codigo = zonas[espacio.Codigo] }).IdEspacio;
                espaciosNew.Add(EspacioDA.InsertEspacio(espacio));
            }

            return espaciosNew;
        }
    }
}
