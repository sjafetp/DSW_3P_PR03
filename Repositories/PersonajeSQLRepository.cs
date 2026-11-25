using DSW_3P_PR03.Data;
using DSW_3P_PR03.Models;

namespace DSW_3P_PR03.Repositories;

public class PersonajeSQLRepository : IPersonajeRepository
{
    private readonly AppDbContext _context;

    public PersonajeSQLRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Personaje> ObtenerTodos()
    {
        return _context.Personajes.ToList();
    }

    public Personaje? ObtenerPorId(int id)
    {
        return _context.Personajes.Find(id);
    }

    public List<Personaje> ObtenerPorVideojuego(string nombre)
    {
        return _context.Personajes
            .Where(p => p.Videojuego == nombre)
            .ToList();
    }

    public List<Personaje> ObtenerPorNivel(int nivel)
    {
        return _context.Personajes
            .Where(p => p.Nivel >= nivel)
            .ToList();
    }

    public Personaje Agregar(Personaje nuevo)
    {
        nuevo.Id = 0;

        _context.Personajes.Add(nuevo);

        _context.SaveChanges();

        return nuevo;
    }

    public bool Actualizar(int id, Personaje personaje)
    {
        var actual = _context.Personajes.Find(id);

        if (actual == null)
            return false;

        actual.Nombre = personaje.Nombre;
        actual.Videojuego = personaje.Videojuego;
        actual.Clase = personaje.Clase;
        actual.Nivel = personaje.Nivel;
        actual.Poder = personaje.Poder;
        actual.Disponible = personaje.Disponible;
        actual.ImagenUrl = personaje.ImagenUrl;

        _context.SaveChanges();

        return true;
    }

    public bool Eliminar(int id)
    {
        var personaje = _context.Personajes.Find(id);

        if (personaje == null)
            return false;

        _context.Personajes.Remove(personaje);

        _context.SaveChanges();

        return true;
    }
}