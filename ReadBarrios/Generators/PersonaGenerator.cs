using BusinessLayer.Interfaces;

namespace ReadBarrios.Generators
{
    public class PersonaGenerator
    {
        private readonly IPersonaBusiness _personaBusiness;

        public PersonaGenerator(IPersonaBusiness personaBusiness)
        {
            _personaBusiness = personaBusiness;
        }

        public void GeneretePersons(string pathFile)
        {
            _personaBusiness.InsertByExcel(pathFile);
        }
    }
}
