// TODO: Agregar parámetro para ambiar el color de las letras de la fila de títulos

using System;

namespace TP1
{
    class Tabla
    {
        // Caracteres para dibujar la tabla
        public static readonly char EsquinaSuperiorIzquierda = '╭';
        public static readonly char EsquinaSuperiorDerecha = '╮';
        public static readonly char EsquinaInferiorIzquierda = '╰';
        public static readonly char EsquinaInferiorDerecha = '╯';
        public static readonly char BordeHorizontal = '─';
        public static readonly char BordeVertical = '│';
        public static readonly char InterseccionSuperior = '┬';
        public static readonly char InterseccionInferior = '┴';
        public static readonly char InterseccionIzquierda = '├';
        public static readonly char InterseccionDerecha = '┤';
        public static readonly char InterseccionIntermedia = '┼';

        public static void DibujarTabla(string[,] datos)
        {
            int filas = datos.GetLength(0);
            int columnas = datos.GetLength(1);

            // Obtener el ancho máximo de cada columna
            int[] anchoColMax = ObtenerAnchoColumna(datos, filas, columnas);

            // Dibujar borde horizontal superior
            DibujarBordeSuperior(columnas, anchoColMax);

            // Dibujar encabezado de la tabla
            for (int j = 0; j < columnas; j++)
            {
                int espacios = anchoColMax[j] - datos[0, j].Length;
                Console.Write(BordeVertical);
                Console.Write($" {datos[0, j]} ");
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
    }
}
