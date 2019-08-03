using Newtonsoft.Json;
using ReadBarrios.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ReadBarrios.Generators
{
    public class SpaceGenerator
    {
        public string PathIn { get; private set; }

        public SpaceGenerator(string pathIn)
        {
            this.PathIn = pathIn;
        }


        public void Generate()
        {
            // Read the file
            XDocument doc = XDocument.Load(this.PathIn);

            // Get placemarks
            List<XElement> placemarks = doc.Descendants().Where(x => x.Name.LocalName == "Placemark").ToList();

            // Get rrcc
            List<Espacio> espacios = this.GetRRCC(placemarks);

            // Inserto los espacios
            Business.InsertEspacios(espacios);
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
                espacio.coordinates = GetCoordinadas(GetValueByLocalName(x, "coordinates"));
                espacio.Coordenadas = JsonConvert.SerializeObject(espacio.coordinates);

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

        private List<Coordenada> GetCoordinadas(string coordinatesStr)
        {
            List<Coordenada> coordenadas = new List<Coordenada>();

            // Saco los saltos de linea
            coordinatesStr = coordinatesStr.Replace("\n", string.Empty);
            // Saco los espacios en blanco
            coordinatesStr = coordinatesStr.Replace("              ", ";");
            // Elimino los espacios sobrantes
            coordinatesStr = coordinatesStr.Replace(" ", string.Empty);
            // elimino el primer delimitador ";"
            coordinatesStr = coordinatesStr.Remove(0, 1);
            // guardo los caracteres en un arreglo
            string[] coordinatesArray = coordinatesStr.Split(';');

            foreach (string coor in coordinatesArray)
            {
                string[] coordenadaPart = coor.Split(",");
                Coordenada coordenada = new Coordenada() { latitude = decimal.Parse(coordenadaPart[1], CultureInfo.InvariantCulture), longitude = decimal.Parse(coordenadaPart[0], CultureInfo.InvariantCulture) };
                coordenadas.Add(coordenada);
            }

            return coordenadas;
        }
    }
}
