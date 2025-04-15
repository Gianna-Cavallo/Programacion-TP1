namespace TP1
{
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Configuración de la consola para UTF-8, para soportar caracteres especiales.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Limpia la consola y establece el color de texto de bienvenida.
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("¡Bienvenido al Programa!\n");
            Console.ResetColor();

            //Bucle que permite mostrar el menú principal hasta que el usuario decida salir.
            while (true)
            {
                Console.WriteLine("╔═══════════════════════════════╗");
                Console.WriteLine("║   --- Menú Principal ---      ║");
                Console.WriteLine("║═══════════════════════════════║");
                Console.WriteLine("║ 1. Sopa de letras             ║");
                Console.WriteLine("║ 2. Tabla                      ║");
                Console.WriteLine("║ 3. Salir                      ║");
                Console.WriteLine("╚═══════════════════════════════╝");

                Console.Write("Seleccione una opción (1-3): ");
                string opcion = Console.ReadLine()?.Trim();

                // Verifica si la opción es nula o está vacía
                switch (opcion)
                {
                    case "1":
                        // Gestiona la opción seleccionada por el usuario.
                        Console.Clear();
                        SopaDeLetras.MostrarMenuSopa();
                        break;

                    case "2":
                        // Si la opción es 2, muestra el menú de tablas.
                        Console.Clear();
                        GestorTabla.MostrarMenuTablas();
                        break;

                    case "3":
                        // Si la opción es 3, sale del programa.
                        Console.WriteLine("Gracias por usar el programa. ¡Hasta luego!");
                        return;

                    default:
                        // Si la opción no es válida, muestra un mensaje de error.
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}