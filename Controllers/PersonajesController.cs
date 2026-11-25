using Microsoft.AspNetCore.Mvc;
using DSW_3P_PR03.Models;
using DSW_3P_PR03.Services;

namespace DSW_3P_PR03.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonajesController : ControllerBase
{
    private readonly IPersonajeService _service;

    public PersonajesController(IPersonajeService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Personaje>), 200)]
    public IActionResult ObtenerTodos()
    {
        return Ok(_service.ObtenerTodos());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Personaje), 200)]
    [ProducesResponseType(404)]
    public IActionResult ObtenerPorId(int id)
    {
        var personaje = _service.ObtenerPorId(id);

        if (personaje == null)
            return NotFound();

        return Ok(personaje);
    }

    [HttpGet("videojuego/{nombre}")]
    [ProducesResponseType(typeof(List<Personaje>), 200)]
    public IActionResult ObtenerPorVideojuego(string nombre)
    {
        return Ok(_service.ObtenerPorVideojuego(nombre));
    }

    [HttpGet("nivel/{nivel:int}")]
    [ProducesResponseType(typeof(List<Personaje>), 200)]
    public IActionResult ObtenerPorNivel(int nivel)
    {
        return Ok(_service.ObtenerPorNivel(nivel));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Personaje), 201)]
    [ProducesResponseType(400)]
    public IActionResult Agregar(Personaje nuevo)
    {
        try
        {
            var creado = _service.Agregar(nuevo);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = creado.Id },
                creado
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public IActionResult Actualizar(
        int id, Personaje actualizado)
    {
        try
        {
            bool resultado = _service.Actualizar(id, actualizado);

            if (!resultado)
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult Eliminar(int id)
    {
        bool resultado = _service.Eliminar(id);

        if (!resultado)
            return NotFound();

        return NoContent();
    }
}