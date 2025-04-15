using System;
using System.Collections.Generic;

namespace TP1
{
    /// <summary>
    /// Clase estática que gestiona las operaciones relacionadas con tablas, como creación, edición, eliminación y visualización.
    /// </summary>
    public static class GestorTabla
    {
        //Lista que contiene todas las tablas creadas.
        private static List<List<List<string>>> todasLasTablas = new List<List<List<string>>>();
        
        /// <summary>
        /// Muestra el menú de gestión de tablas y permite al usuario interactuar con las tablas.
        /// </summary>
        public static void MostrarMenuTablas()
        {
            while (true)
            {
                // Limpiar la consola y mostrar el menú de tablas
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════╗");
                Console.WriteLine("║        MENÚ DE TABLAS        ║");
                Console.WriteLine("╠══════════════════════════════╣");
                Console.WriteLine("║ 1. Listar Tablas             ║");
                Console.WriteLine("║ 2. Editar celda              ║");
                Console.WriteLine("║ 3. Eliminar fila o columna   ║");
                Console.WriteLine("║ 4. Añadir fila o columna     ║");
                Console.WriteLine("║ 5. Crear nueva tabla         ║");
                Console.WriteLine("║ 6. Volver                    ║");
                Console.WriteLine("╚══════════════════════════════╝");
                Console.Write("Seleccione una opción (1-6): ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        // Listar todas las tablas creadas
                        Tabla.MostrarTodasLasTablas(todasLasTablas);
                        break;

                    case "2":
                    case "3":
                    case "4":
                        // Verificar si hay tablas disponibles antes de operar
                        if (todasLasTablas.Count == 0)
                        {
                            Console.WriteLine("\nAún no hay tablas. Cree una primero.");
                            break;
                        }

                        // El usuario puede seleccionar una tabla para operar
                        int indice = SeleccionarTabla();
                        if (indice == -1) break;

                        var tabla = todasLasTablas[indice];
                        Tabla.DibujarTabla(tabla);

                        //Realiza la accion seleccionada por el usuario
                        if (opcion == "2") EditarCelda(tabla);
                        if (opcion == "3") EliminarFilaOColumna(tabla);
                        if (opcion == "4") AnadirFilaOColumna(tabla);
                        break;

                    case "5":
                        var nuevaTabla = Tabla.ObtenerTablaDelUsuario();
                        todasLasTablas.Add(nuevaTabla);
                        break;

                    case "6":
                        return;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                Console.WriteLine("\nPresione Enter para continuar...");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Permite al usuario seleccionar una tabla de la lista.
        /// </summary>
        /// <returns>El índice de la tabla seleccionada o -1 si la selección es inválida.</returns>
        private static int SeleccionarTabla()
        {
            Console.WriteLine("\nSeleccione el número de tabla:");
            for (int i = 0; i < todasLasTablas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Tabla {i + 1}");
            }

            if (int.TryParse(Console.ReadLine(), out int seleccion) &&
                seleccion >= 1 && seleccion <= todasLasTablas.Count)
            {
                return seleccion - 1;
            }

            Console.WriteLine("Selección inválida.");
            return -1;
        }

        /// <summary>
        /// Permite al usuario editar el valor de una celda específica en una tabla.
        /// </summary>
        /// <param name="tabla">La tabla en la que se editará la celda.</param>
        private static void EditarCelda(List<List<string>> tabla)
        {
            Console.Write("Ingrese fila (0 para encabezado): ");
            int.TryParse(Console.ReadLine(), out int fila);

            Console.Write("Ingrese columna: ");
            int.TryParse(Console.ReadLine(), out int columna);

            // Validar que la fila y columna estén dentro de los límites de la tabla
            // La fila 0 es el encabezado, así que se resta 1 para acceder a la lista de datos
            if (fila < 0 || fila >= tabla.Count || columna < 0 || columna >= tabla[0].Count)
            {
                Console.WriteLine("Índices inválidos.");
                return;
            }

            Console.WriteLine($"Valor actual: {tabla[fila][columna]}");
            Console.Write("Nuevo valor: ");
            string nuevoValor = Console.ReadLine();
            Tabla.EditarCelda(tabla, fila, columna, nuevoValor);
        }

        /// <summary>
        /// Permite al usuario eliminar una fila o columna de una tabla.
        /// </summary>
        /// <param name="tabla">La tabla de la que se eliminará la fila o columna.</param>
        private static void EliminarFilaOColumna(List<List<string>> tabla)
        {
            Console.WriteLine("1. Eliminar fila\n2. Eliminar columna");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                //Elimina una columna
                Console.Write("Ingrese número de fila a eliminar: ");
                if (int.TryParse(Console.ReadLine(), out int fila))
                {
                    Tabla.EliminarFila(tabla, fila);
                }
            }
            else if (opcion == "2")
            {
                Console.Write("Ingrese número de columna a eliminar: ");
                if (int.TryParse(Console.ReadLine(), out int col))
                {
                    Tabla.EliminarColumna(tabla, col);
                }
            }
        }

        /// <summary>
        /// Permite al usuario añadir una fila o columna a una tabla.
        /// </summary>
        /// <param name="tabla">La tabla a la que se añadirá la fila o columna.</param>
        private static void AnadirFilaOColumna(List<List<string>> tabla)
        {
            Console.WriteLine("1. Añadir fila\n2. Añadir columna");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                //Añade una nueva fila
                string[] fila = new string[tabla[0].Count];
                for (int i = 0; i < fila.Length; i++)
                {
                    Console.Write($"Ingrese valor para '{tabla[0][i]}': ");
                    fila[i] = Console.ReadLine();
                }
                Tabla.AgregarFila(tabla, fila);
            }
            else if (opcion == "2")
            {
                //Añade una nueva columnaGestiona la opción seleccionada por el usuario.
                Console.Write("\nIngrese el nombre del nuevo título: ");
                string titulo = Console.ReadLine();
                string[] columna = new string[tabla.Count - 1];

                for (int i = 1; i < tabla.Count; i++)
                {
                    Console.Write($"Ingrese valor para fila {i}: ");
                    columna[i - 1] = Console.ReadLine();
                }

                Tabla.AgregarColumna(tabla, titulo, columna);
            }
        }

    }
}