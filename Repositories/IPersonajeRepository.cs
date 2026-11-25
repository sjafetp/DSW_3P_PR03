using DSW_3P_PR03.Models;

namespace DSW_3P_PR03.Repositories;

public interface IPersonajeRepository
{
    List<Personaje> ObtenerTodos();
    Personaje? ObtenerPorId(int id);
    List<Personaje> ObtenerPorVideojuego(string nombre);
    List<Personaje> ObtenerPorNivel(int nivel);
    Personaje Agregar(Personaje nuevo);
    bool Actualizar(int id, Personaje personaje);
    bool Eliminar(int id);
}