using Newtonsoft.Json;
using OfficeOpenXml;
using ReadBarrios.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReadBarrios.Generators
{
    public class ZonaGenerator
    {
        public string PathIn { get; private set; }
        public string PathOut { get; private set; }

        public ZonaGenerator(string pathIn, string pathOut)
        {
            this.PathIn = pathIn;
            this.PathOut = pathOut;
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
            using (ExcelPackage package = GetPackage(this.PathIn))
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

                package.SaveAs(new FileInfo(this.PathOut));
            }
        }

        public void ReloadZonesPerson()
        {
            // Obtengo los polígonos
            List<Espacio> polygons = GetPolygons();

            // Obtengo la hoja de excel
            using (ExcelPackage package = GetPackage(this.PathIn))
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

                package.SaveAs(new FileInfo(this.PathOut));
            }
        }

        // Obtiene los RRCC
        private List<Espacio> GetPolygons()
        {
            // Busco los RRCC
            List<Espacio> espacios = Business.GetEspacioByFilter(new Espacio() { IdCategoria = 110 });

            // Cargo las coordenadas en List<Coordinate>
            return espacios.Select(x => { x.coordinates2 = JsonConvert.DeserializeObject<List<Coordinate>>(x.Coordenadas); return x; }).ToList();
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
                Espacio rrcc = polygons.Find(polygon => (polygon.contains(new Coordinate(lat_data.Value, lgn_data.Value))));
                return Business.zonas[rrcc == null ? "-1" : rrcc.Codigo];
            }

            return null;
        }

    }
}
