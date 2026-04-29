document.addEventListener("DOMContentLoaded", function () {

    const formConsultaCP = document.getElementById("formConsultaCP");
    const txtcodigoPostal = document.getElementById("txtcodigoPostal");
    const btnConsultar = document.getElementById("btnConsultar");
    const btnLimpiar = document.getElementById("btnLimpiar");
    const mensajeValidacion = document.getElementById("mensajeValidacion");

    const seccionResultados = document.getElementById("seccionResultados");
    const lblNombreEstado = document.getElementById("lblNombreEstado");
    const lblCodigoEstado = document.getElementById("lblCodigoEstado");
    const ddlColonias = document.getElementById("ddlColonias");
    const contenedorCodigoColonia = document.getElementById("contenedorCodigoColonia");
    const lblCodigoColonia = document.getElementById("lblCodigoColonia");

    const mensajeSinResultados = document.getElementById("mensajeSinResultados");
    const mensajeErrorConsulta = document.getElementById("mensajeErrorConsulta");

    txtcodigoPostal.addEventListener("input", function () {
        let valor = txtcodigoPostal.value;

        valor = valor.replace(/[^0-9]/g, "");
        txtcodigoPostal.value = valor;

        const esValido = /^[0-9]{5}$/.test(valor);

        btnConsultar.disabled = !esValido;

        if (valor.length > 0 && !esValido) {
            mensajeValidacion.style.display = "block";
        } else {
            mensajeValidacion.style.display = "none";
        }
    });

    formConsultaCP.addEventListener("submit", async function (e) {
        e.preventDefault();

        limpiarMensajes();

        const codigoPostal = txtcodigoPostal.value;

        if (!/^[0-9]{5}$/.test(codigoPostal)) {
            mensajeValidacion.style.display = "block";
            btnConsultar.disabled = true;
            return;
        }

        try {
            const response = await api.SendGet(`PostalCode/${codigoPostal}`);

            //console.log(response);

            const result = typeof response === "string"
                ? JSON.parse(response)
                : response;

            if (!result || result.status !== "success") {
                limpiarResultados();

                mensajeErrorConsulta.textContent =
                    result?.messageDetail ||
                    result?.message ||
                    "No fue posible obtener la información del código postal.";

                mensajeErrorConsulta.style.display = "block";
                return;
            }

            pintarResultados(result.Data);

        } catch (error) {
            console.error(error);

            limpiarResultados();

            mensajeErrorConsulta.textContent =
                "Ocurrió un error al consultar la información. Intenta nuevamente.";

            mensajeErrorConsulta.style.display = "block";
        }
    });

    ddlColonias.addEventListener("change", function () {
        const codigoColonia = ddlColonias.value;

        if (!codigoColonia) {
            contenedorCodigoColonia.style.display = "none";
            lblCodigoColonia.textContent = "-";
            return;
        }

        lblCodigoColonia.textContent = codigoColonia;
        contenedorCodigoColonia.style.display = "block";
    });

    function pintarResultados(data) {
        limpiarResultados();

        if (!data) {
            mensajeSinResultados.style.display = "block";
            return;
        }

        const nombreEstado = data.Nombre;
        const codigoEstado = data.Codigo;
        const colonias = data.Colonias || [];

        if (!nombreEstado && !codigoEstado && colonias.length === 0) {
            mensajeSinResultados.style.display = "block";
            return;
        }

        lblNombreEstado.textContent = nombreEstado || "-";
        lblCodigoEstado.textContent = codigoEstado || "-";

        ddlColonias.innerHTML = "";
        ddlColonias.appendChild(crearOption("", "Seleccione una colonia"));

        colonias.forEach(function (colonia) {
            const option = crearOption(colonia.Codigo, colonia.Nombre);
            option.setAttribute("data-id", colonia.Codigo || "");

            ddlColonias.appendChild(option);
        });

        ddlColonias.disabled = colonias.length === 0;

        seccionResultados.style.display = "block";
    }

    function crearOption(value, text) {
        const option = document.createElement("option");
        option.value = value || "";
        option.textContent = text || "Sin nombre";
        return option;
    }

    function limpiarResultados() {
        seccionResultados.style.display = "none";

        lblNombreEstado.textContent = "-";
        lblCodigoEstado.textContent = "-";
        lblCodigoColonia.textContent = "-";

        ddlColonias.innerHTML = "";
        ddlColonias.appendChild(crearOption("", "Seleccione una colonia"));
        ddlColonias.disabled = true;

        contenedorCodigoColonia.style.display = "none";
    }

    function limpiarMensajes() {
        mensajeSinResultados.style.display = "none";
        mensajeErrorConsulta.style.display = "none";
    }

});