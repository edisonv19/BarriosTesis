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

        public int GeneretePersons(string pathFile)
        {
            return _personaBusiness.InsertByExcel(pathFile);
        }
    }
}
