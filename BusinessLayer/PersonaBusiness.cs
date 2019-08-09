using DataAccessLayer;
using DataAccessLayer.Interfaces;
using Domain;

namespace BusinessLayer
{
    public class PersonaBusiness
    {
        private IPersonaRepository _personaRepository;

        public PersonaBusiness()
        {
            _personaRepository = new PersonaDataAccess();
        }

        public Persona Insert(Persona persona)
        {
            persona.IdPersona = _personaRepository.Insert(persona);

            return persona;
        }
    }
}
