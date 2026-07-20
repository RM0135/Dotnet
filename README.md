# Dotnet
Dotnet solutions

# Validador de Tarjetas (Algoritmo de Luhn)

Aplicación de consola en C# (.NET 8.0) que valida números de tarjetas de crédito utilizando el **algoritmo de Luhn**, identifica la marca de la tarjeta (Visa, MasterCard, American Express, Discover) y permite generar números de tarjeta válidos de prueba.

## Características

- **Validación individual**: ingresa un número de tarjeta y verifica si es válido según el algoritmo de Luhn.
- **Validación por lote desde archivo**: lee un archivo de texto con varios números de tarjeta (uno por línea) y valida todos, mostrando un resumen final.
- **Generador de números válidos**: crea automáticamente un número de tarjeta (tipo Visa) que cumple el algoritmo de Luhn, útil para pruebas.
- **Estadísticas**: muestra el conteo acumulado de tarjetas válidas, inválidas y su distribución por marca.

## Requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) o superior.

## Estructura del proyecto

```
Luhn/
├── Luhn.csproj        # Archivo de proyecto (.NET 8.0)
├── Program.cs          # Código fuente principal
└── tarjeta.txt         # Archivo de ejemplo con números de tarjeta
```

## Cómo ejecutar

1. Clona o descarga este repositorio.
2. Desde la carpeta del proyecto, ejecuta:

   ```bash
   dotnet run
   ```

3. Se mostrará un menú interactivo en consola:

   ```
   === VALIDADOR DE TARJETAS ===
   1. Validar una tarjeta
   2. Validar desde archivo
   3. Generar número válido
   4. Estadísticas
   5. Salir
   ```

## Uso del menú

### 1. Validar una tarjeta
Solicita un número de tarjeta por teclado, indica si es **VÁLIDA** o **INVÁLIDA** según Luhn y muestra la marca detectada.

### 2. Validar desde archivo
Solicita la ruta de un archivo `.txt` con un número de tarjeta por línea (ver `tarjeta.txt` como ejemplo). Procesa cada línea, muestra el resultado individual y un resumen general al finalizar.

Ejemplo de contenido de archivo:
```
4532015112830366
5555555555554444
378282246310005
6011111111111117
4532015112830367
```

### 3. Generar número válido
Genera automáticamente un número de 16 dígitos que comienza con `4` (Visa) y que cumple el algoritmo de Luhn.

### 4. Estadísticas
Muestra el total de tarjetas válidas e inválidas procesadas durante la sesión, junto con el conteo por marca (Visa, MasterCard, American Express, Discover).

## Algoritmo de Luhn

El algoritmo recorre el número de derecha a izquierda:

1. Duplica cada segundo dígito (a partir del segundo desde la derecha).
2. Si el resultado de duplicar es mayor a 9, se le resta 9.
3. Suma todos los dígitos resultantes.
4. El número es válido si la suma total es múltiplo de 10.

## Identificación de marca

| Marca              | Regla de identificación                                    |
|---------------------|-------------------------------------------------------------|
| Visa                 | Comienza con `4` y tiene 13 o 16 dígitos                    |
| MasterCard           | 16 dígitos y prefijo entre `51` y `55`                       |
| American Express     | 15 dígitos y comienza con `34` o `37`                        |
| Discover             | No implementada actualmente (contador presente en estadísticas) |
| Desconocida          | No coincide con ninguna de las reglas anteriores              |

## Notas y posibles mejoras

- Actualmente la detección de **Discover** no está implementada en `IdentificarMarca`, aunque existe un contador `totalDiscover` en las estadísticas.
- Los contadores por marca (`totalVisa`, `totalMasterCard`, `totalAmex`, `totalDiscover`) se declaran pero no se incrementan en el código actual; podrían actualizarse dentro de `ValidarTarjetaMenu` y `ValidarDesdeArchivo` según la marca detectada.
- El generador de números (opción 3) solo produce tarjetas tipo Visa; podría extenderse para generar otras marcas.

## Licencia

Proyecto de uso educativo, sin licencia específica definida.
