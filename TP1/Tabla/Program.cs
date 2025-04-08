using static TP1.Tabla.Color;

namespace TP1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] datos = {
                { "Nombre", "Apellido", "N° Alumno" },
                { "Milena", "SERI", "35204" },
                { "Said", "QUIOTO", "12345" },
                { "Gianna", "CAVALLO", "39306" },
                { "Malena", "KLEIN", "39480" }
            };

            Tabla.DibujarTabla(datos, Cian);
        }
    }
}
