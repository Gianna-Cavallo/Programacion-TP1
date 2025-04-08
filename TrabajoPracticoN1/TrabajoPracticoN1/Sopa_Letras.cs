using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TP1
{
    class SopaDeLetras
    {   // Las dos primeras variables se inicializan en -1, ya que el cero puede ser una posición válida dentro de la matriz,
        // lo que podría causar confusión o errores en ciertas circunstancias.
        // Por lo tanto, es una buena práctica inicializarlas con un valor que no se utilice dentro de los rangos válidos.
        // De esta manera, estas variables indican que aún no tienen un valor asignado o válido.
        static int filaInicio = -1; //Estas variables son para guardar los valores de las palabras encontradas por el usuario si es que son correctas (esto es manejado
        static int columnaInicio = -1; //mas adelante).
        static int longitudPalabra = 0; //A estas variables globales se les coloca static porque se usan en metodos static. Es una regla de c#.
        static string orientacionPalabra = "";
        static bool palabraEncontrada = false; //Esta es para determinar si la palabra encontrada es correcta. Es decir, si el usuario coloco bien la palabra, fila, columna y orientación.


        public static void MostrarMenuSopa()
        {
            char[,] MatrizFacil = new char[5, 5];
            char[,] MatrizIntermedia = new char[7, 7];
            char[,] MatrizDificil = new char[10, 10];

            int opcion = 0; // inicializamos la variabele aqui, porque sino en el while no podemos acceder si está dentro del do
            do
            {

                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║    Seleccione el nivel de dificultad ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║ 1. Fácil (5x5)                       ║");
                Console.WriteLine("║ 2. Intermedio (7x7)                  ║");
                Console.WriteLine("║ 3. Difícil (10x10)                   ║");
                Console.WriteLine("║ 4. Salir                             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.Write("Opción: ");
                string input = Console.ReadLine(); // sea lo que sea que escriba el ususario (ya sea numeros) se guarda como una caden de texto (string)

                if (int.TryParse(input, out opcion)) /// 'TryParse' intenta convertir el texto que escribió el usuario a un número entero.
                // 'input' es la cadena que ingresó el usuario, y 'out opcion' guarda el número convertido si la conversión es exitosa
                // Si el usuario ingresa algo que no es un número (por ejemplo, una letra o palabra), no entra al 'if' y se ejecuta el 'else'.
                // Si el usuario ingresa un número válido pero que no está en las opciones del 'switch', se ejecuta el 'default' dentro del 'switch'.

                {
                    switch (opcion)
                    {
                        case 1:
                            InicializarVariables();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("TEMATICA: Animales");
                            Console.WriteLine("Encuentra las dos palabras de esta sopa de letras");
                            Console.ResetColor();
                            GenerarMatrizAleatoria(MatrizFacil);//Se genera la matriz con letras random mediante la funcion GenerarMatrizAleatoria
                            InsertarPalabraHorizontal(MatrizFacil, 4, 0, "PERRO"); //Insertamos una palabra horizontal
                            InsertarPalabraVertical(MatrizFacil, 1, 4, "GATO"); //Luego una vertical
                            ImprimirMatriz(MatrizFacil); //Finalmente, con las letras random y las palabras insertadas, se imprime la matriz
                            ValidarPalabra(MatrizFacil, 2); //Validar si la palabra encontrada por el usuario es o no correcta. El 2 pasado como argumento representa la 
                                                            //cantidad de palabras a encntrar en esta sopa.
                            break;
                        case 2:
                            InicializarVariables();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("TEMATICA: Paises");
                            Console.WriteLine("Encuentra las tres palabras de esta sopa de letras");
                            Console.ResetColor();
                            GenerarMatrizAleatoria(MatrizIntermedia);
                            InsertarPalabraHorizontal(MatrizIntermedia, 1, 1, "ITALIA");
                            InsertarPalabraHorizontal(MatrizIntermedia, 6, 2, "CHILE");
                            InsertarPalabraVertical(MatrizIntermedia, 3, 0, "PERU");
                            ImprimirMatriz(MatrizIntermedia);
                            ValidarPalabra(MatrizIntermedia, 3);
                            break;
                        case 3:
                            InicializarVariables();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("TEMATICA: Frutas");
                            Console.WriteLine("Encuentra las cuatro palabras de esta sopa de letras");
                            Console.ResetColor();
                            GenerarMatrizAleatoria(MatrizDificil);
                            InsertarPalabraVertical(MatrizDificil, 1, 0, "FRUTILLA");
                            InsertarPalabraVertical(MatrizDificil, 2, 4, "BANANA");
                            InsertarPalabraVertical(MatrizDificil, 1, 9, "MANZANA");
                            InsertarPalabraHorizontal(MatrizDificil, 0, 1, "NARANJA");
                            ImprimirMatriz(MatrizDificil);
                            ValidarPalabra(MatrizDificil, 4);
                            break;
                        case 4:
                            Console.WriteLine("Fin del juego");
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Entrada inválida. Por favor, ingrese un número entre 1 y 4.");
                            Console.ResetColor();
                            break;
                    }
                }
                else // Si el usaurio ingresa cualqueier otra cosa que no sea un numero entre 1 y 4
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número entre 1 y 4.");
                    Console.ResetColor();
                }
                ;
            } while (opcion != 4); // mientras que le usuario no presione 4, se mostrará el menu
        }
         

        static void InicializarVariables()
        { // Estas variables deben ser reiniciadas al comenzar a jugar con una nueva matriz, porque sino quedarán conservando los valores de la partida anterior (xq son globales y estaticas)
            // resaltando posiciones de la matriz nueva que no corresponden. 
            filaInicio = 0; // Se usa para saber desde qué fila imprimir en color la palabra en la matriz.
            columnaInicio = 0; //  Guarda la columna donde empieza la palabra encontrada por el usuario. Se usa para saber desde qué columna imprimir la palabra
            longitudPalabra = 0; // Guarda la longitud de la palabra encontrada. Con este valor sabemos cuántas letras hay que resaltar en la impresión de la matriz.
            orientacionPalabra = ""; // sirve para saber cómo imprimir la palabra en rojo
            palabraEncontrada = false; //  Es un booleano que indica si el usuario ya encontró una palabra. Si es true, significa que matriz sabe que debe resaltar la palabra encontrada al imprimirla.
        }

        static void GenerarMatrizAleatoria(char[,] matriz) // genera una matriz con caracteres aleatorios
        {
            Random r = new Random(); // Se crea una instancia de la clase Random para generar números aleatorios. Esta clase viene incluida en .NET. En el namespace System que es
            // quien engloba las otras clases bascias como Console, String, Math

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = (char)r.Next(65, 91); // Generamos un número aleatorio entre 65 y 90, que corresponden a las letras mayusculas en la tabla ASCII (A-Z)
                                                         // Convertimos el número en un carácter usando (char) y lo asignamos a la posición [i,j] de la matriz
                }
            }
        }

        static void InsertarPalabraHorizontal(char[,] matriz, int fila, int columna, string palabra) // le pasamos la fila y la columna en la que se debe insertar la palabra
        {
            for (int j = 0; j < palabra.Length && j < matriz.GetLength(1); j++) // 'j' va marcando la posicion de cada letra de la palabra y la posicion de las columnas
                                                                                // // El bucle se ejecuta mientras:
                                                                                //  j no se pase del largo de la palabra
                                                                                // y tampoco se exceda de los limites de la matriz (cantidad de columnas)
                                                                                // Nota: Si la palabra la ingresara el usuario, deberíamos agregar una validación previa para avisar si se excede.
                                                                                // Sin embargo, como en este caso las palabras ya son fijas y fueron pensadas para la matriz, no es necesario.
            {
                matriz[fila, columna + j] = palabra[j]; // la fila es fija, mientras que las posiciones de las columnas van avanzando una posicion hacia la derecha para
                // ir insertando las letras de la palabra.
            }
        }

        static void InsertarPalabraVertical(char[,] matriz, int fila, int columna, string palabra)
        {
            for (int i = 0; i < palabra.Length && i < matriz.GetLength(0); i++) // 'i' va marcando la posicion de las letras de la palabra y de la posicion de las filas
            {
                matriz[fila + i, columna] = palabra[i]; // // La columna se mantiene fija, mientras que la fila se incrementa para insertar cada letra de la palabra hacia abajo.
            }
        }

        static void ImprimirMatriz(char[,] matriz) // esta funcion va a imprimir la matriz ya sea normal (sin colores) o con las palabaras resaltadas que haya encontrado el usuario
        {
            for (int i = 0; i < matriz.GetLength(0); i++) //  Recorremos cada fila de la matriz
            {
                for (int j = 0; j < matriz.GetLength(1); j++) // Recorremos cada columna de la fila actual.
                {
                    if (palabraEncontrada) // verificamos si se encontró una palabra, si no, la matriz se imprime  de forma normal.
                    { // Si encontró una palabra, se verifica si es vertical u horizontal

                        // 1) Verifica si la palabra es horizontal (sino salta al if de abajo).
                        // 2) Verifica que estemos en la misma fila donde empieza la palabra encontrada (si no es asi, sale del if y sigue imprimiendo la matriz de forma normal).
                        // 3) Verfica que la columna (j) esté dentro del rango que ocupa la palabra (que esté entre la columna inicio y entre el maximo de esa palabra,
                        // que se calcula asi:  j >= columnaInicio && j < columnaInicio + longitudPalabra).
                        if (orientacionPalabra == "HORIZONTAL" && i == filaInicio && j >= columnaInicio && j < columnaInicio + longitudPalabra)
                        {
                            Console.ForegroundColor = ConsoleColor.Red; // aplica el color rojo. Una vez aplicado, sale del if y va hacia el Console.Write de abajo que pinta la letra
                        }
                        else if (orientacionPalabra == "VERTICAL" && j == columnaInicio && i >= filaInicio && i < filaInicio + longitudPalabra)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                    }
                    Console.Write($"{matriz[i, j]}  "); // Escribe la letra correspondiente de la matriz, ya sea a color o normal (depende de donde venga la letra, si pasó por los condicionles 
                    // de colores, o si es una letra normal.
                    Console.ResetColor(); // Una vez impresa la letra, se resetea el color para poder imprimir con normalidad el resto de la matriz
                }
                Console.WriteLine(); // salto de linea para las filas
            }
        }

        static void ValidarPalabra(char[,] matriz, int cantidadPalabras)
        {
            int palabrasEncontradas = 0; // Declaramos un contador que va a llevar registro de la cantidad de palabras correctas encontradas por el usuario.

            while (palabrasEncontradas < cantidadPalabras) // Iniciamos un bucle que se repetirá hasta que el jugador encuentre todas las palabras correctas 
            // No importa si se equivoca: el bucle solo avanza cuando acierta.
            {
                Console.Write("Que palabra encontraste? ");
                string palabraUsuario = Console.ReadLine().ToUpper(); // convertimos la respuesta del usuario a mayusculas para no tener problemas luego en las validaciones
                Console.Write("En que fila? ");
                int fila = int.Parse(Console.ReadLine());
                Console.Write("En que columna? ");
                int columna = int.Parse(Console.ReadLine());
                Console.Write("Vertical u horizontal? ");
                string orientacion = Console.ReadLine().ToUpper();

                string PalabraMatriz = ""; // Variable que va a ir almacenando las letras de la matriz según la posición que haya indicado el usuario.

                if (orientacion == "HORIZONTAL") // Si el usuario indicó que la palabra que encontró está en horizontal...
                {
                    for (int j = columna; j < columna + palabraUsuario.Length && j < matriz.GetLength(1); j++)
                    {
                        PalabraMatriz += matriz[fila, j]; // vamos almacenando las letras de la matriz que estén en la posición indicada por el usuario, mientras las columnas avanzan hacia la derecha.
                    }
                    if (PalabraMatriz == palabraUsuario)
                    { // Si la palabra formada desde la matriz es igual a la palabra que el usuario indicó haber encontrado...

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Felicidades, encontraste la palabra en la posicion correcta"); // se muestra un mensaje en pantalla
                        Console.ResetColor();                                                             // Guardamos los datos de la palabra encontrada para luego poder imprimir la matriz con la palabra en color, usando estas variables

                        filaInicio = fila;
                        columnaInicio = columna;
                        longitudPalabra = palabraUsuario.Length;
                        orientacionPalabra = orientacion;
                        palabraEncontrada = true;
                        palabrasEncontradas++; // incrementamos la cantidad de palabras encontradas
                        ImprimirMatriz(matriz); // llamamos a imprimir la matriz con la palabra resaltada
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Palabra o posiciones incorrectas. inténtalo de nuevo");
                        Console.ResetColor();
                    }
                }

                else if (orientacion == "VERTICAL")
                {
                    for (int i = fila; i < fila + palabraUsuario.Length && i < matriz.GetLength(0); i++)
                    {
                        PalabraMatriz += matriz[i, columna];
                    }
                    if (PalabraMatriz == palabraUsuario)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Felicidades, encontraste la palabra en la posicion correcta");
                        Console.ResetColor();
                        filaInicio = fila;
                        columnaInicio = columna;
                        longitudPalabra = palabraUsuario.Length;
                        orientacionPalabra = orientacion;
                        palabraEncontrada = true;
                        palabrasEncontradas++;
                        ImprimirMatriz(matriz);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" Palabra o posiciones incorrectas. Inténtalo de nuevo");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opcion invalida"); // Si el usaurio no escribe bien las palabras 'Vertical' u 'Horizontal'
                    Console.ResetColor();
                }
        }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Felicidades! encontraste todas las palabras"); // Una vez que el usuario haya encontrado todas las palabras, se sale del bucle while y se muestra este mensaje.
            // Volviendo al menú para jugar de nuevo el juego.
        Console.ResetColor();
        }
}
}