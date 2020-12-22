//IColmagInscripcionesManager.cs
using System;
using System.Linq;
using Microsoft.AspNet.OData;

using Inscripciones.Procesos.Models;

namespace Inscripciones.Procesos.Interfaces
{
    public interface IColmagInscripcionesManager
	{
        IQueryable<ColmagInscripciones> GetAll();
        ColmagInscripciones GetById(int keyColmagInscripcionId);
        ColmagInscripciones Add(ColmagInscripciones row);
        ColmagInscripciones Update(int keyColmagInscripcionId, Delta<ColmagInscripciones> changes);
        ColmagInscripciones Delete(int keyColmagInscripcionId);
        int SaveChanges();
    }
}
