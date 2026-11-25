namespace DSW_3P_PR03.Models;

public class Personaje
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Videojuego { get; set; } = "";
    public string Clase { get; set; } = "";
    public int Nivel { get; set; }
    public int Poder { get; set; }
    public bool Disponible { get; set; }
    public string ImagenUrl { get; set; } = "";
}