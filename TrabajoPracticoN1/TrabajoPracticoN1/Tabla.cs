using System;
using System.Collections.Generic;

namespace TP1
{
    public class Tabla
    {
        // Lista para almacenar las tablas creadas
        public static List<List<List<string>>> Tablas = new List<List<List<string>>>();
        // Dibuja la tabla en consola con formato

        // Recibe una lista de listas de cadenas (tabla) y la dibuja en la consola
        public static void DibujarTabla(List<List<string>> tabla)
        {
            if (tabla == null || tabla.Count == 0 || tabla[0].Count == 0)
                return;
            // Calcula el ancho de cada columna
            int[] anchoColumnas = new int[tabla[0].Count];
            for (int j = 0; j < tabla[0].Count; j++)
            {
                for (int i = 0; i < tabla.Count; i++)
                {
                    anchoColumnas[j] = Math.Max(anchoColumnas[j], tabla[i][j].Length);
                }
            }

            // Dibuja una linea horizontal de la tabla.
            void DibujarLinea()
            {
                Console.Write("+");
                foreach (var ancho in anchoColumnas)
                    Console.Write(new string('-', ancho + 2) + "+");
                Console.WriteLine();
            }

            DibujarLinea();

            // Dibuja las filas de la tabla.
            for (int i = 0; i < tabla.Count; i++)
            {
                Console.Write("|");
                for (int j = 0; j < tabla[i].Count; j++)
                {
                    if (i == 0) //La primera fila (títulos) se muestran de color azul.
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(" " + tabla[i][j].PadRight(anchoColumnas[j]) + " |");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" " + tabla[i][j].PadRight(anchoColumnas[j]) + " |");
                    }
                }
                Console.WriteLine();
                DibujarLinea();
            }
        }

        /// <summary>
        /// Solicita al usuario los títulos y las filas de una tabla.
        /// </summary>
        /// <returns>Una tabla representada como una lista de listas de cadenas.</returns>
        public static List<List<string>> ObtenerTablaDelUsuario()
        {
            Console.Write("\nIngrese los títulos de la tabla, separados por espacios: ");
            string[] titulos = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? new string[0];

            var tabla = new List<List<string>>();
            tabla.Add(new List<string>(titulos));

            Console.WriteLine("\nIngrese los datos de cada fila, separados por espacios (ingrese 'fin' para terminar):");
            string filaInput;
            while ((filaInput = Console.ReadLine()?.Trim())?.ToLower() != "fin")
            {
                var filaDatos = filaInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (filaDatos.Length != titulos.Length)
                {
                    Console.WriteLine($"Error: La fila debe tener {titulos.Length} columnas.");
                    continue;
                }
                tabla.Add(new List<string>(filaDatos)); //Agrega los titulos como la primera fila
            }

            return tabla;
        }


        /// <summary>
        /// Agrega una nueva fila a la tabla.
        /// </summary>
        /// <param name="tabla">La tabla a la que se agregará la fila.</param>
        /// <param name="nuevaFila">Los datos de la nueva fila.</param>
        public static void AgregarFila(List<List<string>> tabla, string[] nuevaFila)
        {
            if (nuevaFila.Length != tabla[0].Count)
            {
                Console.WriteLine($"Error: La fila debe tener {tabla[0].Count} columnas.");
                return;
            }
            tabla.Add(new List<string>(nuevaFila));
        }

        /// <summary>
        /// Agrega una nueva columna a la tabla.
        /// </summary>
        /// <param name="tabla">La tabla a la que se agregará la columna.</param>
        /// <param name="nuevoTitulo">El título de la nueva columna.</param>
        /// <param name="nuevaColumna">Los datos de la nueva columna.</param>
        public static void AgregarColumna(List<List<string>> tabla, string nuevoTitulo, string[] nuevaColumna)
        {
            if (nuevaColumna.Length != tabla.Count - 1)
            {
                Console.WriteLine($"Error: La columna debe tener {tabla.Count - 1} filas.");
                return;
            }
            tabla[0].Add(nuevoTitulo);
            for (int i = 1; i < tabla.Count; i++) //Agrega el titulo a la nueva columna
            {
                tabla[i].Add(nuevaColumna[i - 1]);
            }
        }

        /// <summary>
        /// Elimina una fila de la tabla.
        /// </summary>
        /// <param name="tabla">La tabla de la que se eliminará la fila.</param>
        /// <param name="indiceFila">El índice de la fila a eliminar.</param>
        public static void EliminarFila(List<List<string>> tabla, int indiceFila)
        {
            if (indiceFila < 1 || indiceFila >= tabla.Count)
            {
                Console.WriteLine("Índice de fila inválido.");
                return;
            }
            tabla.RemoveAt(indiceFila);
        }

        /// <summary>
        /// Elimina una columna de la tabla.
        /// </summary>
        /// <param name="tabla">La tabla de la que se eliminará la columna.</param>
        /// <param name="indiceColumna">El índice de la columna a eliminar.</param>
        public static void EliminarColumna(List<List<string>> tabla, int indiceColumna)
        {
            if (indiceColumna < 0 || indiceColumna >= tabla[0].Count)
            {
                Console.WriteLine("Índice de columna inválido.");
                return;
            }
            foreach (var fila in tabla)
            {
                fila.RemoveAt(indiceColumna);
            }
        }

        /// <summary>
        /// Edita el valor de una celda específica en la tabla.
        /// </summary>
        /// <param name="tabla">La tabla que contiene la celda.</param>
        /// <param name="fila">El índice de la fila de la celda.</param>
        /// <param name="columna">El índice de la columna de la celda.</param>
        /// <param name="nuevoValor">El nuevo valor para la celda.</param>
        public static void EditarCelda(List<List<string>> tabla, int fila, int columna, string nuevoValor)
        {
            if (fila < 0 || fila >= tabla.Count || columna < 0 || columna >= tabla[0].Count)
            {
                Console.WriteLine("Índices inválidos.");
                return;
            }
            tabla[fila][columna] = nuevoValor;
        }

        // Menú principal
        /// <summary>
        /// Muestra el menú principal para gestionar tablas.
        /// </summary>
        /// <returns>True si el usuario decide salir del menú, de lo contrario False.</returns>
        public static bool MostrarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n--- Menú ---");
                Console.WriteLine("1. Listar Tablas");
                Console.WriteLine("2. Crear Nueva Tabla"); // Opción para crear nuevas tablas
                Console.WriteLine("3. Editar Tabla");
                Console.WriteLine("4. Eliminar Tabla"); // Opción para eliminar tablas
                Console.WriteLine("5. Volver");


                Console.Write("Seleccione una opción (1-5): ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ListarTablas();
                        break;
                    case "2":
                        CrearNuevaTabla();
                        break;
                    case "3":
                        EditarTabla();
                        break;
                    case "4":
                        EliminarTabla();
                        break;

                    case "5":
                        return true;
                    default:
                        Console.WriteLine("Opción inválida. Presione Enter para intentar de nuevo.");
                        Console.ReadLine();
                        continue;
                }
            }
        }

        public static void ListarTablas()
        {
            Console.Clear();
            if (Tablas.Count == 0)
            {
                Console.WriteLine("Aún no se han creado tablas.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < Tablas.Count; i++)
            {
                Console.WriteLine($"\nTabla {i + 1}:");
                DibujarTabla(Tablas[i]);
            }
            Console.ReadKey();
        }

        public static void CrearNuevaTabla()
        {
            Console.Clear();
            var nuevaTabla = ObtenerTablaDelUsuario();
            Tablas.Add(nuevaTabla);
            Console.WriteLine("Tabla creada exitosamente.");
            Console.ReadKey();

        }


        public static void EliminarTabla()
        {
            Console.Clear();
            ListarTablas();
            if (Tablas.Count == 0)
            {

                return;
            }


            Console.Write("Ingrese el número de tabla a eliminar: ");

            if (int.TryParse(Console.ReadLine(), out int indiceTabla) && indiceTabla > 0 && indiceTabla <= Tablas.Count)

            {
                Tablas.RemoveAt(indiceTabla - 1);
                Console.WriteLine($"Tabla {indiceTabla} eliminada.");



            }
            else
            {
                Console.WriteLine("Índice de tabla inválido.");
            }
            Console.ReadKey();

        }

        public static void EditarTabla()
        {
            Console.Clear();
            ListarTablas();
            if (Tablas.Count == 0)
            {

                return;
            }

            Console.Write("Ingrese el número de tabla a editar: ");
            if (int.TryParse(Console.ReadLine(), out int indiceTabla) && indiceTabla > 0 && indiceTabla <= Tablas.Count)
            {

                List<List<string>> tablaSeleccionada = Tablas[indiceTabla - 1];


                while (true)
                {
                    Console.Clear();
                    DibujarTabla(tablaSeleccionada);

                    bool volverAlMenuPrincipal = MostrarSubMenuEditarTabla(tablaSeleccionada);
                    if (volverAlMenuPrincipal)

                    {
                        break; // Sale del bucle de edición y vuelve al menú principal
                    }
                    Console.WriteLine("¿Desea continuar modificando la tabla? (s/n):");
                    var continuar = Console.ReadLine()?.Trim().ToLower();
                    if (continuar != "s")
                    {
                        break;

                    }

                }

            }


            else
            {

                Console.WriteLine("Índice de tabla inválido.");
            }
        }



        public static bool MostrarSubMenuEditarTabla(List<List<string>> tabla)
        {
            while (true)

            {
                Console.WriteLine("\n--- Submenú Editar Tabla ---");
                Console.WriteLine("1. Editar celda");
                Console.WriteLine("2. Eliminar fila o columna");
                Console.WriteLine("3. Añadir fila o columna");
                Console.WriteLine("4. Volver"); 
                Console.Write("Seleccione una opción (1-4): ");
                string opcion = Console.ReadLine();
            }

        }
        public static void MostrarTodasLasTablas(List<List<List<string>>> todasLasTablas)
        {
            Console.Clear();
            if (todasLasTablas.Count == 0)
            {
                Console.WriteLine("Aún no se han creado tablas.");
            }
            else
            {
                for (int i = 0; i < todasLasTablas.Count; i++)
                {
                    Console.WriteLine($"\nTabla {i + 1}:");
                    DibujarTabla(todasLasTablas[i]);
                }
            }
        }
    }
}