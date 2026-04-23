function validarRegistro() {

    var nombre = document.getElementById("nombre").value.trim();
    var clave = document.getElementById("clave").value;
    var confirmar = document.getElementById("confirmarClave").value;

    var errorNombre = document.getElementById("errorNombre");
    var errorClave = document.getElementById("errorClave");
    var errorConfirmar = document.getElementById("errorConfirmar");

    var valido = true;

    errorNombre.innerText = "";
    errorClave.innerText = "";
    errorConfirmar.innerText = "";

    if (nombre.length < 6) {
        errorNombre.innerText = "El nombre debe tener al menos 6 caracteres";
        valido = false;
    }

    var regexClave = /^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$/;

    if (!regexClave.test(clave)) {
        errorClave.innerText = "Debe tener mínimo 8 caracteres, una mayúscula, un número y un símbolo";
        valido = false;
    }


    if (clave !== confirmar) {
        errorConfirmar.innerText = "Las contraseñas no coinciden";
        valido = false;
    }

    return valido;
}