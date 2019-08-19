using BusinessLayer.Interfaces;
using Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace ReadBarrios.Generators
{
    public class EspacioGenerator
    {
        private readonly string _pathIn;
        private readonly IEspacioBusiness _espaciosBusiness;

        public EspacioGenerator(IEspacioBusiness espacioBusiness, string pathIn)
        {
            _espaciosBusiness = espacioBusiness;
            _pathIn = pathIn;
        }


        public void Generate()
        {
            // Read the file
            XDocument doc = XDocument.Load(_pathIn);

            // Get placemarks
            List<XElement> placemarks = doc.Descendants().Where(x => x.Name.LocalName == "Placemark").ToList();

            // Get rrcc
            List<Espacio> espacios = this.GetRRCC(placemarks);

            // Inserto los espacios
            _espaciosBusiness.InsertList(espacios);
        }

        private List<Espacio> GetRRCC(List<XElement> placemarks)
        {
            List<Espacio> rrcc = new List<Espacio>();

            // go over the placemarks
            foreach (XElement x in placemarks)
            {
                Espacio espacio = new Espacio();
                espacio.Codigo = GetValueByLocalName(x, "name").Replace("\n", string.Empty); ;
                espacio.Descripcion = espacio.Codigo;
                espacio.Coordenadas = GetCoordinadas(GetValueByLocalName(x, "Coordenadas"));
                espacio.CoordenadasStr = JsonConvert.SerializeObject(espacio.CoordenadasStr);

                rrcc.Add(espacio);
            }

            return rrcc;
        }

        private string GetValueByLocalName(XElement elemen, string localName)
        {
            // get attributes placeMark each
            List<XElement> elements = elemen.Descendants().ToList();
            return elements.Where(y => y.Name.LocalName == localName).Select(i => i.Value).LastOrDefault();
        }

        private List<Coordenada> GetCoordinadas(string CoordenadasStr)
        {
            List<Coordenada> coordenadas = new List<Coordenada>();

            // Saco los saltos de linea
            CoordenadasStr = CoordenadasStr.Replace("\n", string.Empty);
            // Saco los espacios en blanco
            CoordenadasStr = CoordenadasStr.Replace("              ", ";");
            // Elimino los espacios sobrantes
            CoordenadasStr = CoordenadasStr.Replace(" ", string.Empty);
            // elimino el primer delimitador ";"
            CoordenadasStr = CoordenadasStr.Remove(0, 1);
            // guardo los caracteres en un arreglo
            string[] CoordenadasArray = CoordenadasStr.Split(';');

            foreach (string coor in CoordenadasArray)
            {
                string[] coordenadaPart = coor.Split(",");
                Coordenada coordenada = new Coordenada(double.Parse(coordenadaPart[1], CultureInfo.InvariantCulture), double.Parse(coordenadaPart[0], CultureInfo.InvariantCulture));
                coordenadas.Add(coordenada);
            }

            return coordenadas;
        }
    }
}
