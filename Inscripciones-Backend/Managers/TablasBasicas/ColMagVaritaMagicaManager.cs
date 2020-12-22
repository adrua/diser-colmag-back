//ColMagVaritaMagicaManager.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Inscripciones.TablasBasicas.Interfaces;
using Inscripciones.TablasBasicas.Models;

namespace Inscripciones.TablasBasicas.Managers
{
    public class ColMagVaritaMagicaManager : IColMagVaritaMagicaManager
	{	
        private readonly ILogger<ColMagVaritaMagicaManager> logger;
        private readonly ColMagContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string userId;

        public ColMagVaritaMagicaManager(ILogger<ColMagVaritaMagicaManager> logger,
                                ColMagContext context,
                                IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            //this.userId = ApplicationUserTokenHelper.GetIdFromAuthorization(httpContextAccessor.HttpContext.Request);
        }

        IQueryable<ColMagVaritaMagica> IColMagVaritaMagicaManager.GetAll()
        {
            // Obtenemos el nombre del método para logging
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            IQueryable<ColMagVaritaMagica> result = null;
            
            try
            {
                // Loggeamos el inicio de la operación
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                // obtenemos los datos como IQueryable para optimizar oData
                result = context.ColMagVaritaMagica;
                // retornamos la query
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Error, $"Error en: {methodName}\n{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                // Loggeamos el inicio de la operación
                logger.Log(LogLevel.Debug, $"Finalizada operación: {methodName}");
            }
            return result;
        }

        ColMagVaritaMagica IColMagVaritaMagicaManager.GetById(int keyColMagPersonajeId, int keyColMagVaritaMagicaId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            
            ColMagVaritaMagica result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColMagVaritaMagica.Where((x) => x.ColMagPersonajeId == keyColMagPersonajeId && x.ColMagVaritaMagicaId == keyColMagVaritaMagicaId).FirstOrDefault();
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Error, $"Error en: {methodName}\n{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                logger.Log(LogLevel.Debug, $"Finalizada operación: {methodName}");
            }
            return result;
        }

        ColMagVaritaMagica IColMagVaritaMagicaManager.Add(ColMagVaritaMagica row)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColMagVaritaMagica result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColMagVaritaMagica.Where((x) => x.ColMagPersonajeId == row.ColMagPersonajeId && x.ColMagVaritaMagicaId == row.ColMagVaritaMagicaId).FirstOrDefault();
                
                if (result == null)
                {   
                    result = row;
                    
                    context.Entry(row).Property("Usuario").CurrentValue = userId;
                    context.ColMagVaritaMagica.Add(row);
                }
                else 
                {
                    logger.Log(LogLevel.Error, $"Llave Duplicada: ColMagVaritaMagica({row.ColMagPersonajeId}, {row.ColMagVaritaMagicaId})");
                    return null;
                }
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Error, $"Error en: {methodName}\n{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                logger.Log(LogLevel.Debug, $"Finalizada operación: {methodName}");
            }
            return result;
        }
        
        ColMagVaritaMagica IColMagVaritaMagicaManager.Update(int keyColMagPersonajeId, int keyColMagVaritaMagicaId, Delta<ColMagVaritaMagica> changes)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColMagVaritaMagica result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColMagVaritaMagica.Where((x) => x.ColMagPersonajeId == keyColMagPersonajeId && x.ColMagVaritaMagicaId == keyColMagVaritaMagicaId).FirstOrDefault();

                if (result == null)
                {
                    logger.Log(LogLevel.Error, $"Llave no Existe: ColMagVaritaMagica({keyColMagPersonajeId}, {keyColMagVaritaMagicaId})");
                    return null;
                }
                else 
                {
                    changes.CopyChangedValues(result);

                    context.Entry(result).Property("Usuario").CurrentValue = userId;
                    context.Entry(result).Property("Fecha_Computador").CurrentValue = DateTime.Now;
                }
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Error, $"Error en: {methodName}\n{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                logger.Log(LogLevel.Debug, $"Finalizada operación: {methodName}");
            }
            return result;
        }

        ColMagVaritaMagica IColMagVaritaMagicaManager.Delete(int keyColMagPersonajeId, int keyColMagVaritaMagicaId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColMagVaritaMagica result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColMagVaritaMagica.Where((x) => x.ColMagPersonajeId == keyColMagPersonajeId && x.ColMagVaritaMagicaId == keyColMagVaritaMagicaId).FirstOrDefault();

                if (result == null)
                {
                    logger.Log(LogLevel.Error, $"Llave no Existe: ColMagVaritaMagica({keyColMagPersonajeId}, {keyColMagVaritaMagicaId})");
                    return null;
                }
                else 
                {
                    context.ColMagVaritaMagica.Remove(result);
                }
            }
            catch(Exception ex)
            {
                logger.Log(LogLevel.Error, $"Error en: {methodName}\n{ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                logger.Log(LogLevel.Debug, $"Finalizada operación: {methodName}");
            }
            return result;
        }

        int IColMagVaritaMagicaManager.SaveChanges()
        {
            return context.SaveChanges();
        }
	}
}