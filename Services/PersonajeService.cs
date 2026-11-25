using DSW_3P_PR03.Models;
using DSW_3P_PR03.Repositories;

namespace DSW_3P_PR03.Services;

public class PersonajeService : IPersonajeService
{
    private readonly IPersonajeRepository _repository;

    public PersonajeService(IPersonajeRepository repository)
    {
        _repository = repository;
    }

    public List<Personaje> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Personaje? ObtenerPorId(int id)
    {
        return _repository.ObtenerPorId(id);
    }

    public List<Personaje> ObtenerPorVideojuego(string nombre)
    {
        return _repository.ObtenerPorVideojuego(nombre);
    }

    public List<Personaje> ObtenerPorNivel(int nivel)
    {
        return _repository.ObtenerPorNivel(nivel);
    }

    private void ValidarPersonaje(Personaje personaje)
    {
        if (string.IsNullOrWhiteSpace(personaje.Nombre))
            throw new ArgumentException(
                "El nombre es obligatorio"
            );

        if (string.IsNullOrWhiteSpace(personaje.Videojuego))
            throw new ArgumentException(
                "El videojuego es obligatorio"
            );

        if (string.IsNullOrWhiteSpace(personaje.Clase))
            throw new ArgumentException(
                "La clase es obligatoria"
            );

        if (personaje.Nivel < 1 || personaje.Nivel > 100)
            throw new ArgumentException(
                "El nivel debe estar entre 1 y 100"
            );

        if (personaje.Poder <= 0)
            throw new ArgumentException(
                "El poder debe ser mayor que cero"
            );
    }

    public Personaje Agregar(Personaje nuevo)
    {
        nuevo.Disponible = true;

        ValidarPersonaje(nuevo);

        return _repository.Agregar(nuevo);
    }

    public bool Actualizar(int id, Personaje actualizado)
    {
        if (_repository.ObtenerPorId(id) == null)
            return false;

        ValidarPersonaje(actualizado);

        return _repository.Actualizar(id, actualizado);
    }

    public bool Eliminar(int id)
    {
        return _repository.Eliminar(id);
    }

    
}

