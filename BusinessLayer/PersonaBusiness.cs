using DataAccessLayer;
using Domain;

namespace BusinessLayer
{
    public class PersonaBusiness
    {
        public Persona Insert(Persona persona)
        {
            var PersonaDA = new PersonaDataAccess();

            persona = PersonaDA.Insert(persona);

            return persona;
        }
    }
}
