//IColMagVaritaMagicaManager.cs
using System;
using System.Linq;
using Microsoft.AspNet.OData;

using Inscripciones.TablasBasicas.Models;

namespace Inscripciones.TablasBasicas.Interfaces
{
    public interface IColMagVaritaMagicaManager
	{
        IQueryable<ColMagVaritaMagica> GetAll();
        ColMagVaritaMagica GetById(int keyColMagPersonajeId, int keyColMagVaritaMagicaId);
        ColMagVaritaMagica Add(ColMagVaritaMagica row);
        ColMagVaritaMagica Update(int keyColMagPersonajeId, int keyColMagVaritaMagicaId, Delta<ColMagVaritaMagica> changes);
        ColMagVaritaMagica Delete(int keyColMagPersonajeId, int keyColMagVaritaMagicaId);
        int SaveChanges();
    }
}
