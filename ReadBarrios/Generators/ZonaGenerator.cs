using BusinessLayer;
using BusinessLayer.Interfaces;
using Domain;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReadBarrios.Generators
{
    public class ZonaGenerator
    {
        private readonly string _pathIn;
        private readonly string _pathOut;
        private readonly IEspacioBusiness _espacioBusiness;

        public ZonaGenerator(IEspacioBusiness espacioBusiness ,string pathIn, string pathOut)
        {
            _espacioBusiness = espacioBusiness;
            _pathIn = pathIn;
            _pathOut = pathOut;
        }

        /// <summary>
        /// función para regenerar las zonas de los puntos (origen, destino)
        /// en base a los radios censales obtenidos, las zonas generadas y y las relaciones entre ellas
        /// </summary>
        public void ReloadZones()
        {
            // Obtengo los polígonos
            List<Espacio> polygons = GetPolygons();

            // Obtengo la hoja de excel
            using (ExcelPackage package = GetPackage(_pathIn))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[1];

                // Busco los indices de las LAT, LONG y ZONAS
                int lat_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Latitud_origen")).Start.Column;
                int lgn_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Longitud_origen")).Start.Column;
                int zona_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Zona_Origen")).Start.Column;

                int lat_d_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Latitud_destino")).Start.Column;
                int lgn_d_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Longitud_destino")).Start.Column;
                int zona_d_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Zona_Destino")).Start.Column;

                // Recorro las filas del excel
                for (int i = 2; i <= excelWorksheet.Dimension.Rows; i++)
                {
                    excelWorksheet.Cells[i, zona_o_j].Value = GetZona(excelWorksheet, i, lat_o_j, lgn_o_j, polygons);
                    excelWorksheet.Cells[i, zona_d_j].Value = GetZona(excelWorksheet, i, lat_d_j, lgn_d_j, polygons);
                }

                package.SaveAs(new FileInfo(_pathOut));
            }
        }

        public void ReloadZonesPerson()
        {
            // Obtengo los polígonos
            List<Espacio> polygons = GetPolygons();

            // Obtengo la hoja de excel
            using (ExcelPackage package = GetPackage(_pathIn))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[1];

                // Busco los indices de las LAT, LONG y ZONAS
                int lat_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Latitud")).Start.Column;
                int lgn_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Longitud")).Start.Column;
                int zona_o_j = excelWorksheet.Cells["1:1"].First(c => c.Value.ToString().Equals("Zona")).Start.Column;

                // Recorro las filas del excel
                for (int i = 2; i <= excelWorksheet.Dimension.Rows; i++)
                {
                    excelWorksheet.Cells[i, zona_o_j].Value = GetZona(excelWorksheet, i, lat_o_j, lgn_o_j, polygons);
                }

                package.SaveAs(new FileInfo(_pathOut));
            }
        }

        // Obtiene los RRCC
        private List<Espacio> GetPolygons()
        {
            // Busco los RRCC
            IEnumerable<Espacio> espacios = _espacioBusiness.GetByFilter(new Espacio() { IdCategoria = 110 });

            // Cargo las coordenadas en List<Coordenada>
            return espacios.Select(x => { x.Coordenadas = JsonConvert.DeserializeObject<List<Coordenada>>(x.CoordenadasStr); return x; }).ToList();
        }

        // Obtiene el Worksheets
        private ExcelPackage GetPackage(string fileName)
        {
            try
            {
                return new ExcelPackage(new FileInfo(fileName));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Si lat y long son NULL => ZONA NULL
        /// </summary>
        /// <param name="excelWorksheet"></param>
        /// <param name="i"></param>
        /// <param name="lat_j"></param>
        /// <param name="lgn_j"></param>
        /// <param name="polygons"></param>
        /// <returns></returns>
        private string GetZona(ExcelWorksheet excelWorksheet, int i, int lat_j, int lgn_j, List<Espacio> polygons)
        {
            // Obtengo el datos de las celdas
            double? lat_data = double.TryParse(excelWorksheet.Cells[i, lat_j].Value.ToString(), out double lat_o) ? (double?)lat_o : null;
            double? lgn_data = double.TryParse(excelWorksheet.Cells[i, lgn_j].Value.ToString(), out double lon_o) ? (double?)lon_o : null;

            if (lat_data.HasValue && lgn_data.HasValue)
            {
                Espacio rrcc = polygons.Find(polygon => (polygon.contains(new Coordenada(lat_data.Value, lgn_data.Value))));
                return EspacioBusiness.zonas[rrcc == null ? "-1" : rrcc.Codigo];
            }

            return null;
        }

    }
}
