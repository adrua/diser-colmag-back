//IColmagCasasManager.cs
using System;
using System.Linq;
using Microsoft.AspNet.OData;

using Inscripciones.TablasBasicas.Models;

namespace Inscripciones.TablasBasicas.Interfaces
{
    public interface IColmagCasasManager
	{
        IQueryable<ColmagCasas> GetAll();
        ColmagCasas GetById(int keyColmagCasaId);
        ColmagCasas Add(ColmagCasas row);
        ColmagCasas Update(int keyColmagCasaId, Delta<ColmagCasas> changes);
        ColmagCasas Delete(int keyColmagCasaId);
        int SaveChanges();
    }
}
