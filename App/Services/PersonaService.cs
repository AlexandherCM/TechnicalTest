using App.DTOs;
using App.Interfaces.Persistence.Repositories;
using App.Services.Helpers;
using App.ViewModels;
using Domain.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class PersonaService
{
    private IPersonaRepository _personaRepository;
    private PersonNormalizationHelper _normalizationHelper = new PersonNormalizationHelper();

    public PersonaService(IPersonaRepository personaRepository)
    {
        _personaRepository = personaRepository;
        _normalizationHelper = new PersonNormalizationHelper();
    }


    public RequestViewModel AgregarPersona(PersonaViewModel viewModel)
    {
        try
        {
            RequestViewModel validacion = ValidarDatosPersona(viewModel);

            if (validacion != null)
                return validacion;

            var nombreProcesado = _normalizationHelper.ProcesarNombreCompleto(
                viewModel.NombreCompleto,
                viewModel.FechaNacimiento
            );

            var personaEntity = MapperToModel(viewModel, nombreProcesado);

            _personaRepository.Insert(personaEntity);

            return CrearGoodRequest(
                "¡Éxito!",
                "La persona se registró correctamente.",
                DiseñarHTML(nombreProcesado.PreposicionesEncontradas)
            );
        }
        catch (Exception ex)
        {
            return CrearBadRequest(
                "¡Error!",
                $"Error al procesar la solicitud: {ex.Message}"
            );
        }
    }

    private PersonaEntity MapperToModel(PersonaViewModel viewModel, NombreProcesadoDTO dto)
        => new PersonaEntity
        {
            UserID = dto.UserID,
            Nombres = dto.Nombres,
            ApellidoMaterno = dto.ApellidoMaterno,
            ApellidoPaterno = dto.ApellidoPaterno,
            Hobby = viewModel.Hobby,
            FechaNacimiento = viewModel.FechaNacimiento,
            Correo = viewModel.Correo
        };

    private RequestViewModel CrearGoodRequest(string titulo, string leyenda, string html)
    {
        return new RequestViewModel
        {
            Titulo = titulo,
            Leyenda = leyenda,
            Estado = true,
            Html = html
        };
    }
    private RequestViewModel CrearBadRequest(string titulo, string html)
    {
        return new RequestViewModel
        {
            Titulo = titulo,
            Leyenda = "No fue posible procesar la solicitud.",
            Estado = false,
            Html = html
        };
    }

    private RequestViewModel ValidarDatosPersona(PersonaViewModel viewModel)
    {
        var errores = new List<string>();

        if (viewModel == null)
        {
            errores.Add("La información de la persona es obligatoria.");
            return CrearBadRequest("¡Datos inválidos!", ConstruirListaErroresHtml(errores));
        }

        ValidarNombreCompleto(viewModel.NombreCompleto, errores);
        ValidarHobby(viewModel.Hobby, errores);
        ValidarFechaNacimiento(viewModel.FechaNacimiento, errores);
        ValidarCorreo(viewModel.Correo, errores);

        if (errores.Any())
        {
            return CrearBadRequest(
                "¡Datos inválidos!",
                ConstruirListaErroresHtml(errores)
            );
        }

        return null;
    }

    private string DiseñarHTML(List<string> PreposicionesEncontradas)
    {
        var preposicionesHtml = PreposicionesEncontradas.Any()
            ? string.Join("", PreposicionesEncontradas
                .Select(p => $"<li>{p}</li>"))
            : "<hr /><li>No se encontraron preposiciones.</li>";

        return
            $@"
                <hr />
                <strong>Preposiciones encontradas:</strong>
                <ul style='text-align:left;'>
                    {preposicionesHtml}
                </ul>
            ";
    }
    private string ConstruirListaErroresHtml(List<string> errores)
    {
        var items = string.Join("", errores.Select(error => $"<li>{error}</li>"));

        return $@"
        <p>Revisa la información capturada antes de continuar.</p>
        <hr />
        <ul style='text-align:left;'>
            {items}
        </ul>";
    }

    private void ValidarNombreCompleto(string nombreCompleto, List<string> errores)
    {
        if (string.IsNullOrWhiteSpace(nombreCompleto))
        {
            errores.Add("El nombre completo es obligatorio.");
            return;
        }

        string nombreNormalizado = _normalizationHelper.NormalizarEspacios(nombreCompleto);

        if (nombreNormalizado.Length < 5)
            errores.Add("El nombre completo es demasiado corto.");

        if (_normalizationHelper.TieneCaracteresNoPermitidos(nombreNormalizado))
            errores.Add("El nombre solo puede contener letras, espacios, acentos y ñ.");

        var palabras = nombreNormalizado
            .Split(' ')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();

        if (palabras.Count < 3)
            errores.Add("Debe capturar al menos nombre, apellido paterno y apellido materno.");

        if (palabras.All(x => _normalizationHelper.EsPreposicion(x)))
            errores.Add("El nombre completo no puede estar compuesto solo por preposiciones.");
    }

    private void ValidarFechaNacimiento(DateTime fechaNacimiento, List<string> errores)
    {
        if (fechaNacimiento == DateTime.MinValue)
        {
            errores.Add("La fecha de nacimiento es obligatoria.");
            return;
        }

        DateTime hoy = DateTime.Today;
        DateTime fecha = fechaNacimiento.Date;

        if (fecha > hoy)
        {
            errores.Add("La fecha de nacimiento no puede ser futura.");
            return;
        }

        int edad = CalcularEdad(fecha, hoy);

        if (edad < 5)
            errores.Add("La persona debe ser mayor de 5 años.");
    }

    private int CalcularEdad(DateTime fechaNacimiento, DateTime fechaActual)
    {
        int edad = fechaActual.Year - fechaNacimiento.Year;

        if (fechaNacimiento.Date > fechaActual.AddYears(-edad))
            edad--;

        return edad;
    }

    private void ValidarCorreo(string correo, List<string> errores)
    {
        if (string.IsNullOrWhiteSpace(correo))
        {
            errores.Add("El correo electrónico es obligatorio.");
            return;
        }

        bool esCorreoValido = Regex.IsMatch(
            correo.Trim(),
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
        );

        if (!esCorreoValido)
            errores.Add("El formato del correo electrónico no es válido.");
    }

    private void ValidarHobby(string hobby, List<string> errores)
    {
        if (string.IsNullOrWhiteSpace(hobby))
        {
            errores.Add("El hobby es obligatorio.");
            return;
        }

        if (hobby.Trim().Length < 3)
            errores.Add("El hobby debe contener al menos 3 caracteres.");
    }

}