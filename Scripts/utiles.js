var app = angular.module("encuestaApp", []);

app.controller("encuestaCtrl", function ($scope) {

    $scope.encuesta = {
        preguntas: []
    };

    $scope.agregarPregunta = function () {
        $scope.encuesta.preguntas.push({
            texto: "",
            tipo: "texto",
            obligatoria: false,
            opciones: []
        });
    };

    $scope.eliminarPregunta = function (index) {
        $scope.encuesta.preguntas.splice(index, 1);
    };

    $scope.eliminarOpcion = function (pregunta, index) {
        pregunta.opciones.splice(index, 1);
    };

});


function copiarLink() {
    var texto = document.getElementById("linkEncuesta").value;

    navigator.clipboard.writeText(texto).then(function () {
        document.getElementById("mensajeCopiado").style.display = "inline";
    });
}
