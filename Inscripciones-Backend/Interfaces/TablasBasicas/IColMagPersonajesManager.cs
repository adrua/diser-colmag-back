//IColMagPersonajesManager.cs
using System;
using System.Linq;
using Microsoft.AspNet.OData;

using Inscripciones.TablasBasicas.Models;

namespace Inscripciones.TablasBasicas.Interfaces
{
    public interface IColMagPersonajesManager
	{
        IQueryable<ColMagPersonajes> GetAll();
        ColMagPersonajes GetById(int keyColMagPersonajeId);
        ColMagPersonajes Add(ColMagPersonajes row);
        ColMagPersonajes Update(int keyColMagPersonajeId, Delta<ColMagPersonajes> changes);
        ColMagPersonajes Delete(int keyColMagPersonajeId);
        int SaveChanges();
    }
}
