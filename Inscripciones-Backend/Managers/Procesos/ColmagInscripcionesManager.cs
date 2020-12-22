//ColmagInscripcionesManager.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Inscripciones.Procesos.Interfaces;
using Inscripciones.Procesos.Models;
using Inscripciones.TablasBasicas.Models;

namespace Inscripciones.Procesos.Managers
{
    public class ColmagInscripcionesManager : IColmagInscripcionesManager
	{	
        private readonly ILogger<ColmagInscripcionesManager> logger;
        private readonly ColMagContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string userId;

        public ColmagInscripcionesManager(ILogger<ColmagInscripcionesManager> logger,
                                ColMagContext context,
                                IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            //this.userId = ApplicationUserTokenHelper.GetIdFromAuthorization(httpContextAccessor.HttpContext.Request);
        }

        IQueryable<ColmagInscripciones> IColmagInscripcionesManager.GetAll()
        {
            // Obtenemos el nombre del método para logging
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            IQueryable<ColmagInscripciones> result = null;
            
            try
            {
                // Loggeamos el inicio de la operación
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                // obtenemos los datos como IQueryable para optimizar oData
                result = context.ColmagInscripciones;
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

        ColmagInscripciones IColmagInscripcionesManager.GetById(int keyColmagInscripcionId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            
            ColmagInscripciones result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColmagInscripciones.Where((x) => x.ColmagInscripcionId == keyColmagInscripcionId).FirstOrDefault();
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

        ColmagInscripciones IColmagInscripcionesManager.Add(ColmagInscripciones row)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColmagInscripciones result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColmagInscripciones.Where((x) => x.ColmagInscripcionId == row.ColmagInscripcionId).FirstOrDefault();
                
                if (result == null)
                {   
                    result = row;
                    
                    context.Entry(row).Property("Usuario").CurrentValue = userId;
                    context.ColmagInscripciones.Add(row);
                }
                else 
                {
                    logger.Log(LogLevel.Error, $"Llave Duplicada: ColmagInscripciones({row.ColmagInscripcionId})");
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
        
        ColmagInscripciones IColmagInscripcionesManager.Update(int keyColmagInscripcionId, Delta<ColmagInscripciones> changes)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColmagInscripciones result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColmagInscripciones.Where((x) => x.ColmagInscripcionId == keyColmagInscripcionId).FirstOrDefault();

                if (result == null)
                {
                    logger.Log(LogLevel.Error, $"Llave no Existe: ColmagInscripciones({keyColmagInscripcionId})");
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

        ColmagInscripciones IColmagInscripcionesManager.Delete(int keyColmagInscripcionId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColmagInscripciones result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColmagInscripciones.Where((x) => x.ColmagInscripcionId == keyColmagInscripcionId).FirstOrDefault();

                if (result == null)
                {
                    logger.Log(LogLevel.Error, $"Llave no Existe: ColmagInscripciones({keyColmagInscripcionId})");
                    return null;
                }
                else 
                {
                    context.ColmagInscripciones.Remove(result);
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

        int IColmagInscripcionesManager.SaveChanges()
        {
            return context.SaveChanges();
        }
	}
}