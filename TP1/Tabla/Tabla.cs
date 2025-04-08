using System;
using static TP1.Tabla.CaracteresTabla;

namespace TP1
{
    class Tabla
    {
        // Caracteres para dibujar la tabla
        public static class CaracteresTabla
        {
            public const char EsquinaSuperiorIzquierda = '╭';
            public const char EsquinaSuperiorDerecha = '╮';
            public const char EsquinaInferiorIzquierda = '╰';
            public const char EsquinaInferiorDerecha = '╯';
            public const char BordeHorizontal = '─';
            public const char BordeVertical = '│';
            public const char InterseccionSuperior = '┬';
            public const char InterseccionInferior = '┴';
            public const char InterseccionIzquierda = '├';
            public const char InterseccionDerecha = '┤';
            public const char InterseccionIntermedia = '┼';
        }

        // Colores disponibles
        public enum Color
        {
            Negro,
            Azul,
            Cian,
            Gris,
            Verde,
            Magenta,
            Rojo,
            Amarillo,
            Blanco,
            AzulOscuro,
            CianOscuro,
            GrisOscuro,
            VerdeOscuro,
            MagentaOscuro,
            RojoOscuro,
            AmarilloOscuro
        }

        public static void DibujarTabla(string[,] datos, Color colorEncabezado)
        {
            int filas = datos.GetLength(0);
            int columnas = datos.GetLength(1);

            // Traducir el color seleccionado a ConsoleColor
            ConsoleColor color = TraducirColor(colorEncabezado);

            // Obtener el ancho máximo de cada columna
            int[] anchoColMax = ObtenerAnchoColumna(datos, filas, columnas);

            // Dibujar borde horizontal superior
            DibujarBordeSuperior(columnas, anchoColMax);

            // Dibujar encabezado de la tabla
            for (int j = 0; j < columnas; j++)
            {
                int espacios = anchoColMax[j] - datos[0, j].Length;
                Console.Write(BordeVertical);
                Console.ForegroundColor = color; // Cambiar clor del texto
                Console.Write($" {datos[0, j]} ");
                Console.ResetColor(); // Reestablecer color original
                Console.Write(new string(' ', espacios));
            }
            Console.WriteLine(BordeVertical);

            // Dibujar borde horizontal intermedio
            DibujarBordeIntermedio(columnas, anchoColMax);

            // Dibujar el resto de la tabla
            for (int i = 1; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    int espacios = anchoColMax[j] - datos[i, j].Length;
                    Console.Write(BordeVertical);
                    Console.Write($" {datos[i, j]} ");
                    Console.Write(new string(' ', espacios));
                }
                Console.WriteLine(BordeVertical);
            }

            // Dibujar borde horizontal inferior
            DibujarBordeInferior(columnas, anchoColMax);
        }

        private static void DibujarBordeSuperior(int columnas, int[] anchoColMax)
        {
            Console.Write(EsquinaSuperiorIzquierda);
            for (int j = 0; j < columnas; j++)
            {
                Console.Write(new string(BordeHorizontal, anchoColMax[j] + 2));
                if (j < columnas - 1)
                {
                    Console.Write(InterseccionSuperior);
                }
            }
            Console.WriteLine(EsquinaSuperiorDerecha);
        }

        private static void DibujarBordeIntermedio(int columnas, int[] anchoColMax)
        {
            Console.Write(InterseccionIzquierda);
            for (int j = 0; j < columnas; j++)
            {
                Console.Write(new string(BordeHorizontal, anchoColMax[j] + 2));
                if (j < columnas - 1)
                {
                    Console.Write(InterseccionIntermedia);
                }
            }
            Console.WriteLine(InterseccionDerecha);
        }

        private static void DibujarBordeInferior(int columnas, int[] anchoColMax)
        {
            Console.Write(EsquinaInferiorIzquierda);
            for (int j = 0; j < columnas; j++)
            {
                Console.Write(new string(BordeHorizontal, anchoColMax[j] + 2));
                if (j < columnas - 1)
                {
                    Console.Write(InterseccionInferior);
                }
            }
            Console.WriteLine(EsquinaInferiorDerecha);
        }

        private static int[] ObtenerAnchoColumna(string[,] datos, int filas, int columnas)
        {
            int[] anchoColMax = new int[columnas];

            for (int j = 0; j < columnas; j++)
            {
                for (int i = 0; i < filas; i++)
                {
                    if (datos[i, j].Length > anchoColMax[j])
                    {
                        anchoColMax[j] = datos[i, j].Length;
                    }
                }
            }

            return anchoColMax;
        }

        private static ConsoleColor TraducirColor(Color color)
        {
            return color switch
            {
                Color.Negro => ConsoleColor.Black,
                Color.Azul => ConsoleColor.Blue,
                Color.Cian => ConsoleColor.Cyan,
                Color.Gris => ConsoleColor.Gray,
                Color.Verde => ConsoleColor.Green,
                Color.Magenta => ConsoleColor.Magenta,
                Color.Rojo => ConsoleColor.Red,
                Color.Amarillo => ConsoleColor.Yellow,
                Color.Blanco => ConsoleColor.White,
                Color.AzulOscuro => ConsoleColor.DarkBlue,
                Color.CianOscuro => ConsoleColor.DarkCyan,
                Color.GrisOscuro => ConsoleColor.DarkGray,
                Color.VerdeOscuro => ConsoleColor.DarkGreen,
                Color.MagentaOscuro => ConsoleColor.DarkMagenta,
                Color.RojoOscuro => ConsoleColor.DarkRed,
                Color.AmarilloOscuro => ConsoleColor.DarkYellow,
                _ => ConsoleColor.White // Color por defecto
            };
        }
    }
}
