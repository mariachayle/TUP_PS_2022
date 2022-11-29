//using System.Xml.Resolvers;
//using System.Xml.Xsl.Runtime;
//using System.Xml;
//using System.Xml.Xsl.Runtime;

using System.Linq.Expressions;
using System.Dynamic;
using System.Reflection.Emit;
using System.IO.Pipes;
using System.Net;
using System.Security.Principal;
using System.Xml.Schema;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;
//using Internal;
using System.CodeDom.Compiler;
using System.Net.NetworkInformation;
using System.Xml.Xsl;
//using System.Runtime.Intrinsics.Arm.Arm64;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.IO.MemoryMappedFiles;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.IO;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Runtime;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics.Contracts;
using System.Runtime.Intrinsics.X86;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using stopBullying.Comandos;
using stopBullying.Models;

using stopBullying.Resultados;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
//using System.Linq;


namespace StopBullyingController.Controllers
{
[ApiController]
[EnableCors("PS")]
//[Route("api/[controller]")]
public class StopBullyingController : ControllerBase
{
   
   private readonly  stopBullyingContext db = new stopBullyingContext();

 //  public StopBullyingController()
    //metodos

    //GET ESTADO
        [HttpGet]
        [Route("Estado/ObtenerEstados")]
        public ActionResult<Resultados> getEstados()
        {
            var resultado = new Resultados();
            try 
            {
                resultado.Ok = true;
                resultado.Return = db.Estados.ToList();
 
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Error al encontrar los estados";
 
                return resultado;
            }
        }

    //GET PRIORIDAD
        [HttpGet]
        [Route("Prioridad/ObtenerPrioridades")]
        public ActionResult<Resultados> getPrioridades()
        {
            var resultado = new Resultados();
            try 
            {
                resultado.Ok = true;
                resultado.Return = db.Prioridads.ToList();
 
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Error al encontrar las prioridades";
 
                return resultado;
            }
        }

    //POST DIRECCION
    [HttpPost]
        [Route("Direccion/CrearDirector")]
 
        public ActionResult<Resultados> PostDirector ([FromBody]ComandoCrearDirector comando)
        {
            var resultado = new Resultados();

            if(comando.NombreDirector.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese nombre de director";
                return resultado;
            } 
             if(comando.TelDirector.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese telefono de director";
                return resultado;
            } 
 
            if(comando.Mail.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese mail de director";
                return resultado;
            } 
            if(comando.Usuario.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese usuario";
                return resultado;
            } 
            if(comando.Password.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese password";
                return resultado;
            } 
             if(comando.IsDeleted==null)
            {
                comando.IsDeleted=false;
            }

           
  
            //funcion CREATE basica
            var director = new Direccion();
            director.NombreDirector = comando.NombreDirector;
            director.TelDirector = comando.TelDirector;
            director.Mail = comando.Mail;
            director.Usuario = comando.Usuario;
            director.Password = comando.Password;
            director.Direccion1 = comando.Direccion1;       
           
            db.Direccions.Add(director);
            db.SaveChanges();
 
            resultado.Ok = true;
            resultado.Return  =db.Direccions.ToList();
 
            return resultado;
        }

    //GET DIRECCION
        [HttpGet]
        [Route("Direccion/ObtenerDirector")]
        public ActionResult<Resultados> getDirector()
        {
            var resultado = new Resultados();
            try 
            {
                var directores = db.Direccions.Where(c => c.IsDeleted == false).ToList();
                resultado.Ok = true;
                resultado.Return = directores;
 
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Error al encontrar los directores";
 
                return resultado;
            }
        }

    //GET DIRECCION BY ID
        [HttpGet]
        [Route("Direccion/ObtenerDirectivo/{idDirector}")]
        public ActionResult<Resultados> getByIDDirector(int idDirector)
        {
            var resultado = new Resultados();
             try {
                 var director = db.Direccions.Where(c => c.IdDirector == idDirector).FirstOrDefault();
                 resultado.Ok = true;
                 resultado.Return = director;
 
                 return resultado;
             }
             catch (Exception ex)
             {
                 resultado.Ok = false;
                 resultado.CodigoError = 1;
                 resultado.Error = "Directivo no encontrado en la base de datos - " + ex.Message;
 
                 return resultado;
             }
        }


    //PUT DIRECCION
        [HttpPost] 
        [Route("Direccion/UpdateDireccion")]
 
        public ActionResult<Resultados> UpdateDirector([FromBody]ComandoActualizarDirector comando) 
        {
            var resultado = new Resultados();
 
            if(comando.NombreDirector.Equals(""))
            {
            resultado.Ok = false;
            resultado.Error = "ingrese el nombre del director";
            return resultado;
            }
             if(comando.TelDirector.Equals(""))
            {
            resultado.Ok = false;
            resultado.Error = "ingrese el telefono del director";
            return resultado;
            }
             if(comando.Mail.Equals(""))
            {
            resultado.Ok = false;
            resultado.Error = "ingrese el mail del director";
            return resultado;
            }
              if(comando.Direccion1.Equals(""))
            {
            resultado.Ok = false;
            resultado.Error = "ingrese la direccion del director";
            return resultado;
            }
 
            var direccion = db.Direccions.Where(c => c.IdDirector == comando.IdDirector).FirstOrDefault();
            if (direccion != null)
            {
            direccion.NombreDirector = comando.NombreDirector;
            direccion.TelDirector = comando.TelDirector;
            direccion.Mail = comando.Mail;
            direccion.Direccion1=comando.Direccion1;

            db.Direccions.Update(direccion);
            db.SaveChanges();
            }
            resultado.Ok = true;
            resultado.Return = db.Direccions.ToList();
            return resultado;
        }

    //POST NEXO
        [HttpPost]
        [Route("Nexo/CrearNexo")]
 
        public ActionResult<Resultados> PostNexo ([FromBody]ComandoCrearNexo comando)
        {
            var resultado = new Resultados();

            if(comando.NombreNexo.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese nombre de nexo";
                return resultado;
            } 
             if(comando.TelNexo.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese telefono de nexo";
                return resultado;
            } 
 
            if(comando.Mail.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese mail de nexo";
                return resultado;
            } 
            if(comando.Usuario.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese usuario";
                return resultado;
            } 
            if(comando.Password.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese password";
                return resultado;
            } 
             if(comando.IsDeleted==null)
            {
                comando.IsDeleted=false;
            }
  
            //funcion CREATE basica
            var nexo = new NexoAlumno();
            nexo.NombreNexo = comando.NombreNexo;
            nexo.TelNexo = comando.TelNexo;
            nexo.Mail = comando.Mail;
            nexo.Usuario = comando.Usuario;
            nexo.Password = comando.Password;
            nexo.Direccion = comando.Direccion;       
           
            db.NexoAlumnos.Add(nexo);
            db.SaveChanges();
 
            resultado.Ok = true;
            resultado.Return  =db.NexoAlumnos.ToList();
 
            return resultado;
        }

    //GET NEXO
        [HttpGet]
        [Route("Nexo/ObtenerNexo")]
        public ActionResult<Resultados> getNexos()
        {
            var resultado = new Resultados();
            try 
            {
                var nexo = db.NexoAlumnos.Where(c => c.IsDeleted == false).ToList();    
                resultado.Ok = true;
                resultado.Return = nexo;
 
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Error al encontrar los nexos con los alumnos";
 
                return resultado;
            }
        }

    //GET NEXO BY ID
        [HttpGet]
        [Route("Nexo/ObtenerNexo/{idNexo}")]
        public ActionResult<Resultados> getByIDNexo(int idNexo)
        {
            var resultado = new Resultados();
             try {
                 var nexo = db.NexoAlumnos.Where(c => c.IdNexo == idNexo).FirstOrDefault();
                 resultado.Ok = true;
                 resultado.Return = nexo;
 
                 return resultado;
             }
             catch (Exception ex)
             {
                 resultado.Ok = false;
                 resultado.CodigoError = 1;
                 resultado.Error = "Coordinador de Curso no encontrado en la base de datos - " + ex.Message;
 
                 return resultado;
             }
        }


    //PUT NEXO
    [HttpPost] 
        [Route("Nexo/UpdateNexo")]
 
        public ActionResult<Resultados> UpdateNexo([FromBody]ComandoActualizarNexo comando) 
        {
            var resultado = new Resultados();
 
            if(comando.NombreNexo.Equals(""))
            {
            resultado.Ok = false;
            resultado.Error = "ingrese el nombre del nexo con los alumnos";
            return resultado;
            }
            if(comando.TelNexo.Equals(""))
            {
            resultado.Ok = false;
            resultado.Error = "ingrese el telefono del nexo con los alumnos";
            return resultado;
            }
             if(comando.Mail.Equals(""))
            {
            resultado.Ok = false;
            resultado.Error = "ingrese el mail del nexo con los alumnos";
            return resultado;
            }
         
 
            var nexo = db.NexoAlumnos.Where(c => c.IdNexo == comando.IdNexo).FirstOrDefault();
            if (nexo != null)
            {
            nexo.NombreNexo = comando.NombreNexo;
            nexo.TelNexo = comando.TelNexo;
            nexo.Mail = comando.Mail;
            nexo.Direccion=comando.Direccion;
            
            db.NexoAlumnos.Update(nexo);
            db.SaveChanges();
            }
            resultado.Ok = true;
            resultado.Return = db.NexoAlumnos.ToList();
            return resultado;
        }

    //POST DENUNCIAS
    [HttpPost]
        [Route("Denuncias/CrearDenuncia")]
 
        public ActionResult<Resultados> PostDenuncia ([FromBody]ComandoCrearDenuncia comando)
        {
            var resultado = new Resultados();
              
            if(comando.NombreDenunciante.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese denunciante";
                return resultado;
            } 
            
            if(comando.NombreAgresor.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "ingrese nombre de agresor";
                return resultado;
            } 
            if(comando.Emergencia == null)
            {
                resultado.Ok = false;
                resultado.Error = "ingrese si es o no una emergencia";
                return resultado;
            } 

          if(comando.IdEstado.Equals(0)){
            comando.IdEstado=1;
          };
          if(comando.Emergencia.Equals(true)){
            comando.IdPrioridad=3;
          }else{
            comando.IdPrioridad=2;
          }
        //  DateTime dateAndTime=DateTime.Now;
         var date=DateTime.Now;

          if(comando.Fecha == null){
            comando.Fecha=date;
          };

          
         

            //funcion CREATE basica
            var denuncia = new Denuncia();
            denuncia.IdPrioridad = comando.IdPrioridad;  
            denuncia.NombreDenunciante = comando.NombreDenunciante;
            denuncia.NombreObservador = comando.NombreObservador;
            denuncia.NombreAgresor = comando.NombreAgresor;
            denuncia.Descripcion = comando.Descripcion;
            denuncia.Imagen = comando.Imagen;
            denuncia.IdEstado = comando.IdEstado;
            denuncia.Emergencia = comando.Emergencia;
            denuncia.Fecha = comando.Fecha;
            denuncia.Contacto = comando.Contacto;

            db.Denuncias.Add(denuncia);
            db.SaveChanges();
 
            resultado.Ok = true;
            resultado.Return  =db.Denuncias.ToList();
 
            return resultado;
        }

    //GET DENUNCIAS PENDIENTES
    [HttpGet]
        [Route("Denuncias/ObtenerDenunciasPendientes")]
        public ActionResult<Resultados> getPendiente()
        {
            var resultado = new Resultados();
            
                     
            try 
            {
                 var denuncia = db.Denuncias.Where(c => c.IdEstado.Equals(1)).ToList();
                resultado.Ok = true;
                resultado.Return = denuncia;
 
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Error al encontrar denuncias";
 
                return resultado;
            }
        }

       //GET DENUNCIAS IN PROGRESS
    [HttpGet]
        [Route("Denuncias/ObtenerDenunciasEnProgreso")]
        public ActionResult<Resultados> getInProgress()
        {
            var resultado = new Resultados();
            
            try 
            {
                 var denuncia = db.Denuncias.Where(c => c.IdEstado.Equals(2)).ToList();
                resultado.Ok = true;
                resultado.Return = denuncia;
 
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Error al encontrar denuncias";
 
                return resultado;
            }
        }

           //GET DENUNCIAS EMERGENCIA
    [HttpGet]
        [Route("Denuncias/ObtenerDenunciasEmergencia")]
        public ActionResult<Resultados> getEmergencia()
        {
            var resultado = new Resultados();
            
            try 
            {
                 var denuncia = db.Denuncias.Where(c => (c.IdEstado.Equals(1) || c.IdEstado.Equals(2)) && c.IdPrioridad.Equals(3)).ToList();
                resultado.Ok = true;
                resultado.Return = denuncia;
 
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Error al encontrar denuncias";
 
                return resultado;
            }
        }

           //GET DENUNCIAS GENERAL
    [HttpGet]
        [Route("Denuncias/ObtenerDenuncias")]
        public ActionResult<Resultados> get()
        {
            var resultado = new Resultados();
            
            try 
            {
               
                var denuncia = db.Denuncias.Where(c => c.IdEstado.Equals(1) || c.IdEstado.Equals(2)).ToList();
                
                resultado.Ok = true;
                resultado.Return = denuncia;
 
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Error al encontrar denuncias";
 
                return resultado;
            }
        }

     //GET DENUNCIAS RESUELTAS
      [HttpGet]
        [Route("Denuncias/ObtenerDenunciasResueltas")]
        public ActionResult<Resultados> getResuelta()
        {
            var resultado = new Resultados();
            
            try 
            {
                 var denuncia = db.Denuncias.Where(c => c.IdEstado.Equals(3)).ToList();
                resultado.Ok = true;
                resultado.Return = denuncia;
 
                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Error al encontrar denuncias";
 
                return resultado;
            }
        }

    //GET BY ID TEST2
  /*     //GET BY ID comun
    [HttpGet]
        [Route("Denuncias/ObtenerDenunciasId/{Id}")]
        
        public Resultados getByID(int Id)
        {
            var resultado = new Resultados();
            Denuncia d=db.Denuncias.Where(c => c.IdDenuncia == Id).FirstOrDefault();
            try
            {
                db.Entry(d).Reference(x=> x.IdDirectorNavigation).Load();
                db.Entry(d).Reference(x=> x.IdEstadoNavigation).Load();
                db.Entry(d).Reference(x=> x.IdPrioridadNavigation).Load();
                db.Entry(d).Reference(x=> x.IdNexoNavigation).Load();
                
                 resultado.Ok = true;
                 resultado.Return = d;
                  return resultado;
             }catch (Exception ex){
                resultado.Ok = false;
                 resultado.CodigoError = 1;
                 resultado.Error = "Denuncia no encontrado en la base de datos - ";
                  return resultado;
             }
 
                 
        }
        

    //GET BY ID DTOS
    /*
        [HttpGet]
        [Route("Denuncias/ObtenerDenunciasIdConsulta/{IdDen}")]
        
        public Resultados getByIDTest(int IdDen)
        {
            var resultado = new Resultados();
            var consulta= (from _denuncia in db.Denuncias
                       join _estado in db.Estados on _denuncia.IdEstado equals _estado.IdEstado
                       join _prioridad in db.Prioridads on _denuncia.IdPrioridad equals _prioridad.IdPrioridad
                       join _nexoAlumnos in db.NexoAlumnos on _denuncia.IdNexo equals _nexoAlumnos.IdNexo
                       join _direccion in db.Direccions on _denuncia.IdDirector equals _direccion.IdDirector
                       where _denuncia.IdDenuncia == IdDen
                       select new
                       {
                         nombreDenunciante = _denuncia.NombreDenunciante,
                         nombreObservador = _denuncia.NombreObservador,
                         nombreAgresor = _denuncia.NombreAgresor,
                         idEstado = _denuncia.IdEstado,
                         estado = _denuncia.IdEstadoNavigation.Estado1,
                         idPrioridad = _denuncia.IdPrioridad,
                         prioridad = _denuncia.IdPrioridadNavigation.Prioridad1,
                         idNexo = _denuncia.IdNexo,
                         nombreCoordinadorACargo = _denuncia.IdNexoNavigation.NombreNexo,
                         idDirector = _denuncia.IdDirector,
                         nombreDirector = _denuncia.IdDirectorNavigation.NombreDirector
                       }
                      );           
                if(consulta!=null)
                {     
                 resultado.Ok = true;
                 resultado.Return = consulta;
                 return resultado;
                }else{
                 resultado.Ok = false;
                 resultado.CodigoError = 1;
                 resultado.Error = "Denuncia no encontrado en la base de datos - ";
                 return resultado;
                }
             }
        */

    //GET BY ID comun
    [HttpGet]
        [Route("Denuncias/ObtenerDenunciasId/{IdDenuncia}")]
        
        public ActionResult<Resultados> getByID(int IdDenuncia)
        {
            var resultado = new Resultados();
             try {
                 var denuncia = db.Denuncias.Where(c => c.IdDenuncia == IdDenuncia).FirstOrDefault();
                 resultado.Ok = true;
                 resultado.Return = denuncia;
 
                 return resultado;
             }
             catch (Exception ex)
             {
                 resultado.Ok = false;
                 resultado.CodigoError = 1;
                 resultado.Error = "Denuncia no encontrado en la base de datos - " + ex.Message;
 
                 return resultado;
             }
        }

    //PUT DENUNCIAS
    [HttpPost] 
        [Route("Denuncias/UpdateDenuncia")]
 
        public ActionResult<Resultados> UpdateDenuncia([FromBody]ComandoActualizarDenuncia comando) 
        {
            var resultado = new Resultados();
 
           
 
            var denuncia = db.Denuncias.Where(c => c.IdDenuncia == comando.IdDenuncia).FirstOrDefault();
             if(comando.IdEstado.Equals(1)){
                    comando.IdEstado=2;
                }else{
                    comando.IdEstado=2;
                }

            if (denuncia != null)
            { 
            denuncia.IdEstado = comando.IdEstado;
            denuncia.IdPrioridad=comando.IdPrioridad;
            denuncia.Notas=comando.Notas;
            denuncia.IdDirector=comando.IdDirector;
            denuncia.IdNexo=comando.IdNexo;
            db.Denuncias.Update(denuncia);
            db.SaveChanges();
            }
        
            resultado.Ok = true;
            resultado.Return = db.Denuncias.ToList();
            return resultado;
        }

    [HttpPost] 
        [Route("Denuncias/FinalizarDenuncia")]
 
        public ActionResult<Resultados> FinalizarDenuncia([FromBody]ComandoActualizarDenuncia comando) 
        {
            var resultado = new Resultados();
             if(comando.IdEstado.Equals(1)){
                    comando.IdEstado=3;
                }else{
                    comando.IdEstado=3;
                }
           
 
            var denuncia = db.Denuncias.Where(c => c.IdDenuncia == comando.IdDenuncia).FirstOrDefault();
            if (denuncia != null)
            {
                
            denuncia.IdEstado = comando.IdEstado;
            denuncia.IdPrioridad=comando.IdPrioridad;
            denuncia.Notas=comando.Notas;
            denuncia.IdDirector=comando.IdDirector;
            denuncia.IdNexo=comando.IdNexo;
            db.Denuncias.Update(denuncia);
            db.SaveChanges();
            }
        
            resultado.Ok = true;
            resultado.Return = db.Denuncias.ToList();
            return resultado;
        }


    //LOGIN DIRECCION
    [HttpPost]
        [Route("Direccion/Login")]
 
        public ActionResult<Resultados> PostLoginDireccion ([FromBody]ComandoLogin comando)
        {
                       
            var resultado = new Resultados();
            var usuario= comando.Usuario.Trim();
            var password= comando.Password;
        
            try   
            {
                   var login = db.Direccions.FirstOrDefault(x => x.Usuario.Equals(usuario) && x.Password.Equals(password) && x.IsDeleted==false); 

            if (login !=null)
            {
                resultado.Ok = true;
            resultado.Return  =login;
            }
         else{
             resultado.Ok = false;
            resultado.Error = "Usuario o Password incorrectas";
         }
            return resultado;
        }
        catch (Exception ex){
   resultado.Ok = false;
            resultado.Error = "usuario no encontrado";
            return resultado;
        }
        }

   //LOGIN NEXO
 
    [HttpPost]
        [Route("Nexo/Login")]
 
        public ActionResult<Resultados> PostLoginNexo ([FromBody]ComandoLogin comando)
        {
                       
            var resultado = new Resultados();
            var usuario= comando.Usuario.Trim();
            var password= comando.Password;
        
            try  
            {
                   var login = db.NexoAlumnos.FirstOrDefault(x => x.Usuario.Equals(usuario) && x.Password.Equals(password) && x.IsDeleted==false); 
            if (login !=null)
            {
                resultado.Ok = true;
            resultado.Return  =login;
            }
         else{
             resultado.Ok = false;
            resultado.Error = "Usuario o Password incorrectas";
         }
            return resultado;
              }
        catch (Exception ex){
            resultado.Ok = false;
            resultado.Error = "usuario no encontrado";
            return resultado;
        }
        }

   // DELETE LOGICO NEXO
   /* [HttpDelete] 
        [Route("Nexo/DeleteNexo/{idNexo}")]
 
       public ActionResult<Resultados> DeleteNexo(int idNexo) 
        {
            var resultado = new Resultados();
 
            var nexo = db.NexoAlumnos.Where(c => c.IdNexo == idNexo).FirstOrDefault();
            db.NexoAlumnos.Remove(nexo);
            db.SaveChanges();

            resultado.Ok=true;
            resultado.Return = db.NexoAlumnos.ToList();
            return resultado;
        }
*/
   
        [HttpPost] 
        [Route("Nexo/DeleteNexo")]
 
        public ActionResult<Resultados> DeleteNexo([FromBody]ComandoEliminarNexo comando) 
        {
            var resultado = new Resultados();
            var nexo = db.NexoAlumnos.Where(c => c.IdNexo == comando.IdNexo).FirstOrDefault();
            if(comando.IsDeleted==false){
                comando.IsDeleted=true;      
            }
            if (nexo != null)
            {
            nexo.IsDeleted = comando.IsDeleted;
            db.NexoAlumnos.Update(nexo);
            db.SaveChanges();
            }
            resultado.Ok = true;
            resultado.Return = db.NexoAlumnos.ToList();
            return resultado;
        }

        //DELETE LOGICO DIRECTIVO
    /*    [HttpDelete] 
        [Route("Direccion/DeleteDireccion/{idDirector}")]
 
        public ActionResult<Resultados> DeleteDirector(int idDirector) 
        {
            var resultado = new Resultados();
 
            var direccion = db.Direccions.Where(c => c.IdDirector == idDirector).FirstOrDefault();
            db.Direccions.Remove(direccion);
            db.SaveChanges();

            resultado.Ok=true;
            resultado.Return = db.Direccions.ToList();
            return resultado;
        }*/

        [HttpPost] 
        [Route("Direccion/DeleteDireccion")]
 
        public ActionResult<Resultados> DeleteDirector([FromBody]ComandoEliminarDirector comando) 
        {
            var resultado = new Resultados();
            var direccion = db.Direccions.Where(c => c.IdDirector == comando.IdDirector).FirstOrDefault();
            if(comando.IsDeleted==false){
                comando.IsDeleted=true;
            }
            if (direccion != null)
            {
            direccion.IsDeleted = comando.IsDeleted;

            db.Direccions.Update(direccion);
            db.SaveChanges();
            }
            resultado.Ok = true;
            resultado.Return = db.Direccions.ToList();
            return resultado;
        }


    //GET REPORT 1 casos totales
    
        [HttpGet]
        [Route("Denuncias/ObtenerDenunciasReporteTotal")]
        
        public Resultados getAll()
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                   
                   group _denuncia by new{_denuncia.Fecha.Value.Year} into g
                   orderby g.Count()
                    select new
                       {
                         anio=g.Key,
                         cantidad=g.Count()
                         
                     }
                      );    
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }

        [HttpGet]
        [Route("Denuncias/ReporteTotalEstado")]
        
        public Resultados getAllEstado()
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                join _estado in db.Estados on _denuncia.IdEstado equals _estado.IdEstado
                  where _denuncia.Fecha.Value.Year == 2022
                   group _denuncia by new{_estado.Estado1} into g
                   
                   select new
                    {
                         Estados=g.Key,
                         cantidad =g.Select(x=> x.IdDenuncia).Count()    
                     }
                      );     
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }

        [HttpGet]
        [Route("Reporte/total")]
        
        public Resultados getAllTotalEstado(DateTime param1, DateTime param2)
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                join _estado in db.Estados on _denuncia.IdEstado equals _estado.IdEstado
                  where _denuncia.Fecha >= param1 && _denuncia.Fecha <= param2
                   group _denuncia by new{_estado.Estado1} into g
                   
                   select new
                    {
                         Estados=g.Key,
                         cantidad =g.Select(x=> x.IdDenuncia).Count()    
                     }
                      );     
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }

    //GET REPORT 2 casos emergencia
    [HttpGet]
        [Route("Denuncias/ObtenerDenunciasReporteEmergencia")]
        
        public Resultados getAllEmergencia()
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                  where _denuncia.Emergencia == true
                   group _denuncia by new{_denuncia.Fecha.Value.Year} into g
                   orderby g.Count()
                   select new
                       {
                         
                         anio = g.Key,
                         cantidad =g.Count()
                         
                     }
                      );    
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }

        
    
     [HttpGet]
        [Route("Denuncias/ReporteEmergenciaEstado")]

           public Resultados getAllEmergenciaEstado()
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                    join _estado in db.Estados on _denuncia.IdEstado equals _estado.IdEstado
                  where _denuncia.Emergencia == true && _denuncia.Fecha.Value.Year == 2022
                   group _denuncia by new{_estado.Estado1} into g
                   
                   select new
                    {
                         Estados=g.Key,
                         cantidad =g.Select(x=> x.IdDenuncia).Count()    
                     }
                      );    
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }

           [HttpGet]
        [Route("Reporte/totalEmergencia")]
        
        public Resultados getAllTotalEstadoEmergencia(DateTime param1, DateTime param2)
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                join _estado in db.Estados on _denuncia.IdEstado equals _estado.IdEstado
                  where  _denuncia.Emergencia == true &&_denuncia.Fecha >= param1 && _denuncia.Fecha <= param2
                   group _denuncia by new{_estado.Estado1} into g
                   
                   select new
                    {
                         Estados=g.Key,
                         cantidad =g.Select(x=> x.IdDenuncia).Count()    
                     }
                      );     
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }
        
      /*esto si funciona
        public Resultados getAllEmergenciaEstado()
        {
            var emergenciaGrouped = db.Denuncias.Where(a=>a.Emergencia == true)
                     .GroupBy(n => n.Fecha.Value.Year)
                     
                     .Select(group =>
                         new
                         {
                             anio = group.Key,
                           //  Denuncias = group.ToList(),
                             cantidad = group.Count()
                         });
           
        
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return =emergenciaGrouped;
            return resultado;        
        }
*/


    //GET REPORT 3 casos pendientes
     [HttpGet]
        [Route("Denuncias/ObtenerDenunciasReportePendientes")]
        
        public Resultados getAllPendientes()
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                  where _denuncia.IdEstado == 1
                   group _denuncia by new{_denuncia.Fecha.Value.Year} into g
                   orderby g.Count()
                    select new
                       {
                        anio=g.Key,
                         cantidad=g.Select(x=> x.IdDenuncia).Count()
                         
                     }
                      );    
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }

        [HttpGet]
        [Route("Denuncias/ObtenerDenunciasReporteProgreso")]
        
        public Resultados getAllInProgress()
        {
           
           var denuncias= (from _denuncia in db.Denuncias
                  where _denuncia.IdEstado == 2
                   group _denuncia by new{_denuncia.Fecha.Value.Year} into g
                   orderby g.Count()
                    select new
                       {
                        anio=g.Key,
                         cantidad=g.Select(x=> x.IdDenuncia).Count()
                         
                     }
                      );    
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denuncias;
            return resultado;         
        }

        [HttpGet]
        [Route("Reporte/totalEstado")]
        
        public Resultados getAllTotalEstadoEstado(DateTime param1, DateTime param2)
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                join _estado in db.Estados on _denuncia.IdEstado equals _estado.IdEstado
                  where _denuncia.IdEstado == 1 && _denuncia.Fecha >= param1 && _denuncia.Fecha <= param2
                   group _denuncia by new{_estado.Estado1} into g
                   
                   select new
                    {
                         Estados=g.Key,
                         cantidad =g.Select(x=> x.IdDenuncia).Count()    
                     }
                      );     
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }

             [HttpGet]
        [Route("Reporte/totalEstadoProgreso")]
        
        public Resultados getAllTotalEstadoEstadoResuelto(DateTime param1, DateTime param2)
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                join _estado in db.Estados on _denuncia.IdEstado equals _estado.IdEstado
                  where  _denuncia.IdEstado == 2 && _denuncia.Fecha >= param1 && _denuncia.Fecha <= param2
                   group _denuncia by new{_estado.Estado1} into g
                   
                   select new
                    {
                         Estados=g.Key,
                         cantidad =g.Select(x=> x.IdDenuncia).Count()    
                     }
                      );     
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }



    //GET REPORT 4 casos resueltos
      [HttpGet]
        [Route("Denuncias/ObtenerDenunciasReporteResueltos")]
        
        public Resultados getAllResueltos()
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                  where _denuncia.IdEstado == 3
                   group _denuncia by new{_denuncia.Fecha.Value.Year} into g
                   orderby g.Count() descending
                    select new
                       {
                         anio=g.Key,
                         cantidad=g.Select(x=> x.IdDenuncia).Count()
                         
                     }
                      );    
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }

         [HttpGet]
        [Route("Denuncias/ReporteResueltosEstado")]

           public Resultados getAllResueltoEstado()
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                    join _estado in db.Estados on _denuncia.IdEstado equals _estado.IdEstado
                  where  _denuncia.Fecha.Value.Year == 2022 && _denuncia.IdEstado == 3
                   group _denuncia by new{_denuncia.Emergencia} into g
                   
                   select new
                    {
                         Denuncias=g.Key,
                         cantidad =g.Select(x=> x.IdDenuncia).Count()    
                     }
                      );    
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }

             [HttpGet]
        [Route("Reporte/totalResuelto")]
        
        public Resultados getAllTotalEmergenciaResuelto(DateTime param1, DateTime param2)
        {
           
           var denunciasT= (from _denuncia in db.Denuncias
                join _estado in db.Estados on _denuncia.IdEstado equals _estado.IdEstado
                  where _denuncia.Fecha >= param1 && _denuncia.Fecha <= param2 && _denuncia.IdEstado==3
                   group _denuncia by new{_denuncia.Emergencia} into g
                   
                   select new
                    {
                         Denuncias=g.Key,
                         cantidad =g.Select(x=> x.IdDenuncia).Count()    
                     }
                      );     
            
            var resultado = new Resultados();
            resultado.Ok=true;
            resultado.Return = denunciasT;
            return resultado;         
        }

        ///RECUPERAR PASSWORD
        
        [HttpPost] 
        [Route("Pass/RecuperarNexo")]
 
        public ActionResult<Resultados> PassNexo([FromBody]ComandoRecuperarPasswordNexo comando) 
        {
            var resultado = new Resultados();
            var nexo = db.NexoAlumnos.Where(c => c.Mail == comando.Mail).FirstOrDefault();
            
            if (nexo != null)
            {
            nexo.Password = comando.Password;

            db.NexoAlumnos.Update(nexo);
            db.SaveChanges();
            }
            resultado.Ok = true;
            resultado.Return = db.NexoAlumnos.ToList();
            return resultado;
        } 

        [HttpPost] 
        [Route("Pass/RecuperarDirector")]
 
        public ActionResult<Resultados> PassDirector([FromBody]ComandoRecuperarPasswordDirector comando) 
        {
            var resultado = new Resultados();
            var director = db.Direccions.Where(c => c.Mail == comando.Mail).FirstOrDefault();
            
            if (director != null)
            {
            director.Password = comando.Password;

            db.Direccions.Update(director);
            db.SaveChanges();
            }
            resultado.Ok = true;
            resultado.Return = db.Direccions.ToList();
            return resultado;
        } 

}
}

