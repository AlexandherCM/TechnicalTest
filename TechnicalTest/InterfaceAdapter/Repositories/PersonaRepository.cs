using App.Interfaces.Persistence.Repositories;
using Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTest.Models;
using TechnicalTest.Models.Entities;

namespace TechnicalTest.InterfaceAdapter.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        public Context _context { get; }

        public PersonaRepository(Context context)
            => _context = context;

        public void Insert(PersonaEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var persona = MapToModel(entity);

            _context.Personas.Add(persona);
            _context.SaveChanges();

            entity.ID = persona.ID;
        }

        public async Task<IEnumerable<PersonaEntity>> Select()
        {
            var personas = await _context.Personas
                .AsNoTracking()
                .OrderBy(x => x.ID)
                .ToListAsync();

            return personas.Select(MapToEntity);
        }

        private Persona MapToModel(PersonaEntity entity)
            => new Persona
            {
                ID = entity.ID,
                UserID = entity.UserID,
                Nombres = entity.Nombres,
                ApellidoUno = entity.ApellidoPaterno,
                ApellidoDos = entity.ApellidoMaterno,
                Hobby = entity.Hobby,
                FechaNacimiento = entity.FechaNacimiento,
                Correo = entity.Correo
            };

        private PersonaEntity MapToEntity(Persona model)
            => new PersonaEntity
            {
                ID = model.ID,
                UserID = model.UserID,
                Nombres = model.Nombres,
                ApellidoPaterno = model.ApellidoUno,
                ApellidoMaterno = model.ApellidoDos,
                Hobby = model.Hobby,
                FechaNacimiento = model.FechaNacimiento,
                Correo = model.Correo
            };
    }
}