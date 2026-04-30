using App.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Services.Helpers
{
    public class PersonNormalizationHelper
    {

        private readonly string[] _preposiciones = new[] { "de", "del", "la", "las", "los", "el" };

        public bool TieneCaracteresNoPermitidos(string texto)
           => !Regex.IsMatch(texto, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$");

        public string NormalizarEspacios(string texto)
           => Regex.Replace(texto.Trim(), @"\s+", " ");

        public bool EsPreposicion(string palabra)
        {
            if (string.IsNullOrWhiteSpace(palabra))
            {
                return false;
            }

            return _preposiciones.Contains(palabra.ToLower());
        }

        public NombreProcesadoDTO ProcesarNombreCompleto(string nombreCompleto, DateTime fechaNacimiento)
        {
            if (string.IsNullOrWhiteSpace(nombreCompleto))
            {
                throw new ArgumentException("El nombre completo es obligatorio.");
            }

            string nombreSinEspaciosDobles = NormalizarEspacios(nombreCompleto);

            if (TieneCaracteresNoPermitidos(nombreSinEspaciosDobles))
            {
                throw new ArgumentException("El nombre contiene caracteres no permitidos.");
            }

            List<string> palabras = nombreSinEspaciosDobles
                .Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(ConvertirPalabraATitulo)
                .ToList();

            if (palabras.Count < 3)
            {
                throw new ArgumentException("Debe capturar al menos nombre, apellido paterno y apellido materno.");
            }

            List<string> preposicionesEncontradas = ObtenerPreposicionesEncontradas(palabras);

            var resultadoMaterno = ExtraerApellidoDesdeElFinal(palabras);
            string apellidoMaterno = FormatearApellido(resultadoMaterno.Apellido);

            var resultadoPaterno = ExtraerApellidoDesdeElFinal(resultadoMaterno.Restantes);
            string apellidoPaterno = FormatearApellido(resultadoPaterno.Apellido);

            string nombre = string.Join(" ", resultadoPaterno.Restantes);

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("No fue posible identificar el nombre de la persona.");
            }

            string nombreCompletoNormalizado = FormatearNombreCompleto(
                nombre,
                apellidoPaterno,
                apellidoMaterno
            );

            string userId = GenerarUserId(
                nombre,
                apellidoPaterno,
                apellidoMaterno,
                fechaNacimiento
            );

            return new NombreProcesadoDTO
            {
                NombreCompletoNormalizado = nombreCompletoNormalizado,
                Nombres = nombre,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                PreposicionesEncontradas = preposicionesEncontradas,
                UserID = userId
            };
        }

        private ApellidoSplitDTO ExtraerApellidoDesdeElFinal(List<string> palabras)
        {
            if (palabras == null || palabras.Count == 0)
            {
                throw new ArgumentException("No hay palabras suficientes para procesar el apellido.");
            }

            var apellido = new List<string>();

            int index = palabras.Count - 1;

            // Tomamos la última palabra como núcleo del apellido.
            apellido.Insert(0, palabras[index]);
            index--;

            while (index >= 0 && EsPreposicion(palabras[index]))
            {
                apellido.Insert(0, palabras[index]);
                index--;
            }

            var restantes = palabras
                .Take(index + 1)
                .ToList();

            return new ApellidoSplitDTO
            {
                Apellido = apellido,
                Restantes = restantes
            };
        }

        private string ConvertirPalabraATitulo(string palabra)
        {
            CultureInfo cultura = new CultureInfo("es-MX");

            string palabraMinuscula = palabra.ToLower(cultura);

            TextInfo textInfo = cultura.TextInfo;

            return textInfo.ToTitleCase(palabraMinuscula);
        }

        private List<string> ObtenerPreposicionesEncontradas(List<string> palabras)
            => palabras
                .Select(p => p.ToLower())
                .Where(p => _preposiciones.Contains(p))
                .Distinct()
                .OrderBy(p => p)
                .ToList();
       

        private string FormatearApellido(List<string> palabrasApellido)
        {
            return string.Join(" ", palabrasApellido.Select(ConvertirPalabraATitulo));
        }

        private string FormatearNombreCompleto(string nombre, string apellidoPaterno, string apellidoMaterno)
        {

            string completo = $"{nombre} {apellidoPaterno} {apellidoMaterno}";

            return string.Join(" ",
                completo
                    .Split(' ')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(p =>
                    {
                        if (EsPreposicion(p))
                        {
                            return p.ToLower();
                        }

                        return ConvertirPalabraATitulo(p);
                    })
            );
        }

        private string GenerarUserId(string nombre, string apellidoPaterno, string apellidoMaterno, DateTime fechaNacimiento)
        {
            string parteApellidoPaterno = TomarLetrasSinPreposiciones(apellidoPaterno, 2);
            string parteApellidoMaterno = TomarLetrasSinPreposiciones(apellidoMaterno, 1);
            string parteNombre = TomarLetrasSinPreposiciones(nombre, 1);

            string fecha = fechaNacimiento.ToString("ddMMyy");

            return $"{parteApellidoPaterno}{parteApellidoMaterno}{parteNombre}{fecha}".ToUpper();
        }

        private string TomarLetrasSinPreposiciones(string texto, int cantidad)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return string.Empty;
            }

            string palabraBase = texto
                .Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .FirstOrDefault(x => !EsPreposicion(x));

            if (string.IsNullOrWhiteSpace(palabraBase))
            {
                return string.Empty;
            }

            palabraBase = palabraBase.Trim();

            if (palabraBase.Length <= cantidad)
            {
                return palabraBase;
            }

            return palabraBase.Substring(0, cantidad);
        }

    }
}
