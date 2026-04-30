using Domain.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Interfaces.Persistence.Repositories
{
    public interface IPersonaRepository
    {
        Task<IEnumerable<PersonaEntity>> Select();
        void Insert(PersonaEntity entity);  
    }
}
