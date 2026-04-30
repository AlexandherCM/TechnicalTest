document.addEventListener("DOMContentLoaded", function () {

    var txtcodigoPostal = document.getElementById("txtcodigoPostal");
    var btnConsultar = document.getElementById("btnConsultar");
    var btnLimpiar = document.getElementById("btnLimpiar");
    var mensajeValidacion = document.getElementById("mensajeValidacion");

    var seccionResultados = document.getElementById("seccionResultados");
    var mensajeError = document.getElementById("mensajeError");

    // Si esta página no tiene el formulario de CP, no ejecutes nada.
    if (!txtcodigoPostal || !btnConsultar || !btnLimpiar || !mensajeValidacion) {
        return;
    }

    txtcodigoPostal.addEventListener("input", function () {
        var valor = txtcodigoPostal.value;

        valor = valor.replace(/[^0-9]/g, "");
        txtcodigoPostal.value = valor;

        var esValido = /^[0-9]{5}$/.test(valor);

        btnConsultar.disabled = !esValido;

        if (valor.length > 0 && !esValido) {
            mensajeValidacion.style.display = "block";
        } else {
            mensajeValidacion.style.display = "none";
        }
    });

    btnLimpiar.addEventListener("click", function () {
        txtcodigoPostal.value = "";
        btnConsultar.disabled = true;
        mensajeValidacion.style.display = "none";

        if (seccionResultados) {
            seccionResultados.style.display = "none";
        }

        if (mensajeError) {
            mensajeError.style.display = "none";
        }
    });

});