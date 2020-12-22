//ColMagPersonajesManager.cs
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
    public class ColMagPersonajesManager : IColMagPersonajesManager
	{	
        private readonly ILogger<ColMagPersonajesManager> logger;
        private readonly ColMagContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string userId;

        public ColMagPersonajesManager(ILogger<ColMagPersonajesManager> logger,
                                ColMagContext context,
                                IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            //this.userId = ApplicationUserTokenHelper.GetIdFromAuthorization(httpContextAccessor.HttpContext.Request);
        }

        IQueryable<ColMagPersonajes> IColMagPersonajesManager.GetAll()
        {
            // Obtenemos el nombre del método para logging
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            IQueryable<ColMagPersonajes> result = null;
            
            try
            {
                // Loggeamos el inicio de la operación
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                // obtenemos los datos como IQueryable para optimizar oData
                result = context.ColMagPersonajes;
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

        ColMagPersonajes IColMagPersonajesManager.GetById(int keyColMagPersonajeId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            
            ColMagPersonajes result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColMagPersonajes.Where((x) => x.ColMagPersonajeId == keyColMagPersonajeId).FirstOrDefault();
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

        ColMagPersonajes IColMagPersonajesManager.Add(ColMagPersonajes row)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColMagPersonajes result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColMagPersonajes.Where((x) => x.ColMagPersonajeId == row.ColMagPersonajeId).FirstOrDefault();
                
                if (result == null)
                {   
                    result = row;
                    
                    context.Entry(row).Property("Usuario").CurrentValue = userId;
                    context.ColMagPersonajes.Add(row);
                }
                else 
                {
                    logger.Log(LogLevel.Error, $"Llave Duplicada: ColMagPersonajes({row.ColMagPersonajeId})");
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
        
        ColMagPersonajes IColMagPersonajesManager.Update(int keyColMagPersonajeId, Delta<ColMagPersonajes> changes)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColMagPersonajes result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColMagPersonajes.Where((x) => x.ColMagPersonajeId == keyColMagPersonajeId).FirstOrDefault();

                if (result == null)
                {
                    logger.Log(LogLevel.Error, $"Llave no Existe: ColMagPersonajes({keyColMagPersonajeId})");
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

        ColMagPersonajes IColMagPersonajesManager.Delete(int keyColMagPersonajeId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColMagPersonajes result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColMagPersonajes.Where((x) => x.ColMagPersonajeId == keyColMagPersonajeId).FirstOrDefault();

                if (result == null)
                {
                    logger.Log(LogLevel.Error, $"Llave no Existe: ColMagPersonajes({keyColMagPersonajeId})");
                    return null;
                }
                else 
                {
                    context.ColMagPersonajes.Remove(result);
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

        int IColMagPersonajesManager.SaveChanges()
        {
            return context.SaveChanges();
        }
	}
}