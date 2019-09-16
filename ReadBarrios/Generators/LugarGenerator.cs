using BusinessLayer.Interfaces;

namespace ReadBarrios.Generators
{
    public class LugarGenerator
    {
        private ILugarBusiness _lugarBusiness;
        public LugarGenerator(ILugarBusiness lugarBusiness)
        {
            _lugarBusiness = lugarBusiness;
        }

        public int UpdateRadioCensal()
        {
            return _lugarBusiness.ReloadRadiosCensales();
        }
    }
}
