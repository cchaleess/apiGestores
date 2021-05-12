using apiGestores.Context;
using apiGestores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;



namespace apiGestores
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestoresController : Controller
    {
        private readonly AppDbContext context;
    
        public GestoresController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/<GestoresController>

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.gestores_bd.ToList());

            } catch (Exception ex)

            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<GestoresController>/5
        [HttpGet("{id}", Name = "GetGestor")]               //Retorna 1 solo registro
        public ActionResult Get(int id)
        {
            try
            {
                var gestor = context.gestores_bd.FirstOrDefault(g => g.id == id);   //Uso de LINQ
                return Ok(gestor);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<GestoresController>
        [HttpPost]
        public ActionResult Post([FromBody] GestoresBD gestor) //Inserto registros
        {

            try
            {

                context.gestores_bd.Add(gestor);
                context.SaveChanges();
                return CreatedAtRoute("GetGestor", new { id = gestor.id }, gestor); //se devuelve el registro insertado con el id generado

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // PUT api/<GestoresController>/5
         [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] GestoresBD gestor)//buscamos cual es el registro que se quiere modificar
        {
            try
            {

                if (gestor.id == id)

                {
                    context.Entry(gestor).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetGestor", new { id = gestor.id }, gestor);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // DELETE api/<GestoresController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var gestor = context.gestores_bd.FirstOrDefault(g => g.id == id);

                if(gestor != null)
                {

                    context.gestores_bd.Remove(gestor);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
