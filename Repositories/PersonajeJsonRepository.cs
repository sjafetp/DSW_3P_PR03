using DSW_3P_PR03.Models;
using System.Text.Json;

namespace DSW_3P_PR03.Repositories;

public class PersonajeJsonRepository : IPersonajeRepository
{
    private readonly string _ruta;

    private readonly JsonSerializerOptions _opciones = new()
    {
        WriteIndented = true
    };

    public PersonajeJsonRepository(IWebHostEnvironment environment)
    {
        _ruta = Path.Combine(environment.ContentRootPath,"Data","personajes.json");

        Directory.CreateDirectory(
            Path.GetDirectoryName(_ruta)!
        );

        if (!File.Exists(_ruta))
        {
            File.WriteAllText(_ruta, "[]");
        }
    }

    private List<Personaje> leer()
    {
        string json = File.ReadAllText(_ruta);

        return JsonSerializer.Deserialize<List<Personaje>>(json)
            ?? new List<Personaje>();
    }

    private void Guardar(List<Personaje> datos)
    {
        string json = JsonSerializer.Serialize(datos, _opciones);

        File.WriteAllText(_ruta, json);
    }

    public List<Personaje> ObtenerTodos()
    {
        return leer();
    }

    public Personaje? ObtenerPorId(int id)
    {
        return leer().FirstOrDefault(p => p.Id == id);
    }

    public List<Personaje> ObtenerPorVideojuego(string nombre)
    {
        return leer()
            .Where(p => p.Videojuego.Equals(
                nombre,
                StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Personaje> ObtenerPorNivel(int nivel)
    {
        return leer()
            .Where(p => p.Nivel >= nivel)
            .ToList();
    }

    public Personaje Agregar(Personaje nuevo)
    {
        var datos = leer();

        nuevo.Id = datos.Count == 0
            ? 1
            : datos.Max(p => p.Id) + 1;

        datos.Add(nuevo);

        Guardar(datos);

        return nuevo;
    }

    public bool Actualizar(int id, Personaje personaje)
    {
        var datos = leer();

        var existente = datos.FirstOrDefault(p => p.Id == id);

        if (existente == null)
            return false;

        existente.Nombre = personaje.Nombre;
        existente.Videojuego = personaje.Videojuego;
        existente.Clase = personaje.Clase;
        existente.Nivel = personaje.Nivel;
        existente.Poder = personaje.Poder;
        existente.Disponible = personaje.Disponible;
        existente.ImagenUrl = personaje.ImagenUrl;

        Guardar(datos);

        return true;
    }

    public bool Eliminar(int id)
    {
        var datos = leer();

        var existente = datos.FirstOrDefault(p => p.Id == id);

        if (existente == null)
            return false;

        datos.Remove(existente);

        Guardar(datos);

        return true;
    }
}