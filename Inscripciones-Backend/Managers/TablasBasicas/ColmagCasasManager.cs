//ColmagCasasManager.cs
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
    public class ColmagCasasManager : IColmagCasasManager
	{	
        private readonly ILogger<ColmagCasasManager> logger;
        private readonly ColmagContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string userId;

        public ColmagCasasManager(ILogger<ColmagCasasManager> logger,
                                ColmagContext context,
                                IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userId = ApplicationUserTokenHelper.GetIdFromAuthorization(httpContextAccessor.HttpContext.Request);
        }

        IQueryable<ColmagCasas> IColmagCasasManager.GetAll()
        {
            // Obtenemos el nombre del método para logging
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            IQueryable<ColmagCasas> result = null;
            
            try
            {
                // Loggeamos el inicio de la operación
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                // obtenemos los datos como IQueryable para optimizar oData
                result = context.ColmagCasas;
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

        ColmagCasas IColmagCasasManager.GetById(int keyColmagCasaId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            
            ColmagCasas result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColmagCasas.Where((x) => x.ColmagCasaId == keyColmagCasaId).FirstOrDefault();
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

        ColmagCasas IColmagCasasManager.Add(ColmagCasas row)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColmagCasas result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColmagCasas.Where((x) => x.ColmagCasaId == row.ColmagCasaId).FirstOrDefault();
                
                if (result == null)
                {   
                    result = row;
                    
                    context.Entry(row).Property("Usuario").CurrentValue = userId;
                    context.ColmagCasas.Add(row);
                }
                else 
                {
                    logger.Log(LogLevel.Error, $"Llave Duplicada: ColmagCasas({row.ColmagCasaId})");
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
        
        ColmagCasas IColmagCasasManager.Update(int keyColmagCasaId, Delta<ColmagCasas> changes)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColmagCasas result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColmagCasas.Where((x) => x.ColmagCasaId == keyColmagCasaId).FirstOrDefault();

                if (result == null)
                {
                    logger.Log(LogLevel.Error, $"Llave no Existe: ColmagCasas({keyColmagCasaId})");
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

        ColmagCasas IColmagCasasManager.Delete(int keyColmagCasaId)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            ColmagCasas result = null;

            try
            {
                logger.Log(LogLevel.Debug, $"Iniciando operación: {methodName}");
                result = context.ColmagCasas.Where((x) => x.ColmagCasaId == keyColmagCasaId).FirstOrDefault();

                if (result == null)
                {
                    logger.Log(LogLevel.Error, $"Llave no Existe: ColmagCasas({keyColmagCasaId})");
                    return null;
                }
                else 
                {
                    context.ColmagCasas.Remove(result);
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

        int IColmagCasasManager.SaveChanges()
        {
            return context.SaveChanges();
        }
	}
}