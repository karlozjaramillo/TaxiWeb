function mostrarModal(idConductor) {
    $.ajax({
        "url": "http://localhost:58640/Conductor/VerConductor/" + idConductor,
        "type": "GET",
        "dataType": "json",
        "success": function (data) {
            var contenido = '<b>Cédula: </b>' + data.Cedula +
                '<br /><b>Nombre: </b>' + data.Nombre +
                '<br /><b>Apellido: </b>' + data.Apellido;

            $('#contenido').html(contenido);
            $('#idConductorEliminar').val(data.Id);
        },
        "error": function (data) {
            alert("fue mal");
        }        
    });

    $('#modalEliminar').modal('open');
}

function eliminarConductor() {
    var idConductor = $('#idConductorEliminar').val();
    $.ajax({
        "url": "http://localhost:58640/Conductor/EliminarConductor/" + idConductor,
        "type": "GET",
        "dataType": "json",
        "success": function (data) {
            alert("conductor eliminado");
        },
        "error": function (data) {
            alert("fue mal");
        }
    });
}