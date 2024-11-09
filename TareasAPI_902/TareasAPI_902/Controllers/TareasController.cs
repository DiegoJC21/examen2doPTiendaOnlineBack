using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasAPI_902.Models;

namespace TareasAPI_902.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        //Creando la variable de contexto de BD-
        private readonly Bdtareas902Context _baseDatos;

        public TareasController(Bdtareas902Context baseDatos)
        {
            _baseDatos = baseDatos;
        }
        //Metodo GET ListaTareas que devuelve la lista de todas las tareas en la BD.
        [HttpGet]
        [Route("ListaTareas")]
        public async Task<IActionResult> Lista()
        {
            var listaTareas = await
            _baseDatos.Tareas.ToListAsync();
            return Ok(listaTareas);
        }
        //Metodo POST ListaTareas que devuelve la lista de todas las tareas en la BD.
        [HttpPost]
        [Route("AgregarTarea")]
        public async Task<IActionResult> Agregar([FromBody]Tarea request)
        {
            await _baseDatos.Tareas.AddAsync(request);
            await _baseDatos.SaveChangesAsync();
            return Ok(request);
        }
        //Metodo Put ModificarTareas que permite modificar una tarea de la bd
        [HttpPut]
        [Route("ModificarTarea/{id:int}")]
        public async Task<IActionResult> Modificar(int id, [FromBody] Tarea request)
        {
            var tareaModificar = await
            _baseDatos.Tareas.FindAsync(id);
            if(tareaModificar == null)
            {
                return BadRequest("No existe la tarea");
            }

            tareaModificar.Nombre = request.Nombre;

            try
            {
                await _baseDatos.SaveChangesAsync();
            }
            catch (Exception ex) { 
                return NotFound();
            }
            return Ok();
        }
        //Metodo DELETE EliminarTarea que permite eliminar una tarea de la bd
        [HttpDelete]
        [Route("EliminarTarea/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var tareaEliminar = await
            _baseDatos.Tareas.FindAsync(id);
            if (tareaEliminar == null)
            {
                return BadRequest("No existe la tarea");
            }

            _baseDatos.Tareas.Remove(tareaEliminar);
            await _baseDatos.SaveChangesAsync();


            return Ok();
        }
    }
}
