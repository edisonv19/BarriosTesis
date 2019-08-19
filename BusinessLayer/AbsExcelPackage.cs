using OfficeOpenXml;
using System;
using System.IO;

namespace BusinessLayer
{
    public abstract class AbsExcelPackage
    {
        protected ExcelPackage GetPackage(string fileName)
        {
            try
            {
                return new ExcelPackage(new FileInfo(fileName));
            }
            catch (Exception)
            {
                throw new Exception("Error to create binary file. The path can be failed.");
            }
        }
    }
}
