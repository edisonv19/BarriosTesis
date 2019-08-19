using BusinessLayer.Interfaces;
using Domain;

namespace ReadBarrios.Generators
{
    public class CodigoGenerator
    {
        private readonly ICodigoBusiness _codigoBusiness;

        public CodigoGenerator(ICodigoBusiness codigoBusiness)
        {
            _codigoBusiness = codigoBusiness;
        }

        public Codigo GetCode(string code, string grupo)
        {
            return _codigoBusiness.GetCodigoByClave(new Codigo() { Clave = code, Grupo = grupo });
        }
    }
}
