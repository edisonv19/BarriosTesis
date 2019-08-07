using Domain;
using Domain.DTOs;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BusinessLayer
{
    public class ExcelReaderBusiness
    {
        public void ReloadZonesPerson(string pathIn, string pathOut)
        {
            // Obtengo los polígonos
            List<PersonaExcelDto> personas = new List<PersonaExcelDto>();

            // Obtengo la hoja de excel
            using (ExcelPackage package = new ExcelPackage(new FileInfo(pathIn)))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[1];

                // Recorro las filas del excel
                for (int i = 1; i <= excelWorksheet.Dimension.Rows; i++)
                {
                    PersonaExcelDto persona = new PersonaExcelDto()
                    {
                        Zona        = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Calle       = excelWorksheet.Cells[i, 3].Value.ToString(),
                        NroPostal   = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Num_Correlativo = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Nombre      = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Ingreso     = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Sexo        = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Edad        = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Estudios    = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Ocupacion   = excelWorksheet.Cells[i, 3].Value.ToString(),
                        TipoZonaResidencial = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Anio        = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Estacion    = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Latitud     = excelWorksheet.Cells[i, 3].Value.ToString(),
                        Longitud    = excelWorksheet.Cells[i, 3].Value.ToString()
                    };

                    
                }
            }
        }
    }
}
