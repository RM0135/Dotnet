// See https://aka.ms/new-console-template for more information
using System;
using System.IO;

class Program
{
    static int totalValidas = 0;
    static int totalInvalidas = 0;
    static int totalVisa = 0;
static int totalMasterCard = 0;
static int totalAmex = 0;
static int totalDiscover = 0;

    static void Main(string[] args)
    {
        int opcion;

        do
        {
            Console.Clear();
            Console.WriteLine("=== VALIDADOR DE TARJETAS ===");
            Console.WriteLine("1. Validar una tarjeta");
            Console.WriteLine("2. Validar desde archivo");
            Console.WriteLine("3. Generar número válido");
            Console.WriteLine("4. Estadísticas");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");

            int.TryParse(Console.ReadLine(), out opcion);

            switch (opcion)
            {
                case 1:
                    ValidarTarjetaMenu();
                    break;

                case 2:
                    ValidarDesdeArchivoMenu();
                    break;

                case 3:
                    GenerarNumeroMenu();
                    break;

                case 4:
                    MostrarEstadisticas();
                    break;

                case 5:
                    Console.WriteLine("Hasta luego.");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Por favor solo escriba un número del 1 al 5.");
                    break;
            }

            if (opcion != 5)
            {
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcion != 5);
    }

    // ================= OPCIÓN 1 =================

    static void ValidarTarjetaMenu()
    {
        Console.Write("Ingrese el número de tarjeta: ");
        string numero = Console.ReadLine();

        bool valida = ValidarTarjeta(numero);
        string marca = IdentificarMarca(numero);

        Console.WriteLine("\nNúmero: " + numero);
        Console.WriteLine("Marca: " + marca);

        if (valida)
        {
            Console.WriteLine("Estado: VÁLIDA");
            totalValidas++;
        }
        else
        {
            Console.WriteLine("Estado: INVÁLIDA");
            totalInvalidas++;
        }
    }

    // ================= OPCIÓN 2 =================

    static void ValidarDesdeArchivoMenu()
    {
        Console.Write("Ingrese la ruta del archivo: ");
        string ruta = Console.ReadLine();

        ValidarDesdeArchivo(ruta);
    }

    static void ValidarDesdeArchivo(string ruta)
    {
        try
        {
            string[] lineas = File.ReadAllLines(ruta);

            foreach (string linea in lineas)
            {
                string numero = linea.Trim();

                if (numero == "")
                    continue;

                bool valida = ValidarTarjeta(numero);
                string marca = IdentificarMarca(numero);

                Console.WriteLine("--------------------------");
                Console.WriteLine("Número: " + numero);
                Console.WriteLine("Marca: " + marca);

                if (valida)
                {
                    Console.WriteLine("Estado: VÁLIDA");
                    totalValidas++;
                }
                else
                {
                    Console.WriteLine("Estado: INVÁLIDA");
                    totalInvalidas++;
                }
            }

            Console.WriteLine("--------------------------");
            Console.WriteLine("===== RESUMEN =====");
            Console.WriteLine("Tarjetas válidas: " + totalValidas);
            Console.WriteLine("Tarjetas inválidas: " + totalInvalidas);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al leer el archivo.");
            Console.WriteLine(ex.Message);
        }
    }

    // ================= MÉTODO LUHN =================

    static bool ValidarTarjeta(string numero)
    {
        int suma = 0;
        bool duplicar = false;

        for (int i = numero.Length - 1; i >= 0; i--)
        {
            if (!char.IsDigit(numero[i]))
                return false;

            int digito = (int)char.GetNumericValue(numero[i]);

            if (duplicar)
            {
                digito *= 2;

                if (digito > 9)
                    digito -= 9;
            }

            suma += digito;
            duplicar = !duplicar;
        }

        return suma % 10 == 0;
    }

    // ================= IDENTIFICAR MARCA =================

    static string IdentificarMarca(string numero)
    {
        if (numero.StartsWith("4") &&(numero.Length == 13 || numero.Length == 16))
        {
            return "Visa";
        }

        if (numero.Length == 16)
        {
            int prefijo = int.Parse(numero.Substring(0, 2));

            if (prefijo >= 51 && prefijo <= 55)
                return "MasterCard";
        }

        if (numero.Length == 15 &&
            (numero.StartsWith("34") || numero.StartsWith("37")))
        {
            return "American Express";
        }

        return "Desconocida";
    }

    // ================= OPCIÓN 3 =================

    static void GenerarNumeroMenu()
{
    string numero = GenerarNumeroValido();

    Console.WriteLine("===== GENERAR NÚMERO VÁLIDO =====");
    Console.WriteLine("Número generado: " + numero);
    Console.WriteLine("Marca: " + IdentificarMarca(numero));

    if (ValidarTarjeta(numero))
    {
        Console.WriteLine("Estado: VÁLIDA");
    }
    else
    {
        Console.WriteLine("Estado: INVÁLIDA");
    }
}
static string GenerarNumeroValido()
{
    Random random = new Random();

    while (true)
    {
        // Empieza con 4 para generar una Visa
        string numero = "4";

        // Genera los otros 15 dígitos
        for (int i = 0; i < 15; i++)
        {
            numero += random.Next(0, 10);
        }

        // Si pasa Luhn, lo devuelve
        if (ValidarTarjeta(numero))
        {
            return numero;
        }
    }
}
        

    // ================= OPCIÓN 4 =================

    static void MostrarEstadisticas()
    {
 
    Console.WriteLine("===== ESTADÍSTICAS =====");

    Console.WriteLine("Tarjetas válidas: " + totalValidas);
    Console.WriteLine("Tarjetas inválidas: " + totalInvalidas);
    Console.WriteLine("==================Estadisticas por tarjeta=============");
    Console.WriteLine("Visa: " + totalVisa);
    Console.WriteLine("MasterCard: " + totalMasterCard);
    Console.WriteLine("American Express: " + totalAmex);
    Console.WriteLine("Discover: " + totalDiscover);
    }
}