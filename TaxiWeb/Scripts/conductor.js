$(document).ready(function () {
    $.ajax({
        "url": "http://localhost:58640/Conductor/Listar/",
        "type": "GET",
        "dataType": "json",
        "success": function (data) {
            var contenido = '';
            for (var i = 0; i < data.length; i++) {
                contenido += '<tr>';
                contenido += '<td>' + data[i].Nombre + '</td>';
                contenido += '<td>' + data[i].Apellido + '</td>';
                contenido += '<td>' + data[i].Cedula + '</td>';
                contenido += '<td>' + data[i].FechaNacimiento + '</td>';
                contenido += '<td>' + data[i].LicenciaConduccion + '</td>';
                contenido += '<td>' + data[i].ExpiracionLicencia + '</td>';
                if (data[i].Vehiculo[0]) {
                    contenido += '<td>' + data[i].Vehiculo[0].Placa + '</td>';
                } else {
                    contenido += '<td></td>';
                }
                contenido += '<td>0</td>';
                contenido += '<td><a href="Edit/ ' + data[i].Id + ' " class="material-icons">create</a></td>';
                contenido += '<td><a href="Details/ ' + data[i].Id + ' " class="material-icons">list</a></td>';
                contenido += '<td><a href="#" class="material-icons" onclick="mostrarModal(' + data[i].Id + ')">delete</a></td>';
                contenido += '</tr>';
                contenido += '</tr>';
            }
            $('#listaConductores').html(contenido);
        },
        "error": function (data) {
            alert("fue mal");
        }
    });

    $("#Filtrar").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#listaConductores tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});

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
            window.location.reload();
        },
        "error": function (data) {
            alert("fue mal");
        }
    });
}

function abrirModal() {
    $('#modalCrear').modal('open');
}

function crear() {
    $.ajax({
        "url": "http://localhost:58640/Conductor/Crear/",
        "type": "post",
        "data": {
            'cedula': $('#cedula').val(),
            'nombre': $('#nombre').val(),
            'apellido': $('#apellido').val(),
            'fechaNacimiento': $('#fechaNacimiento').val(),
            'licenciaConduccion': $('#licenciaConduccion').val(),
            'expiracionLicencia': $('#fechaExpiracion').val()
        },
        "dataType": "json",
        "success": function (datos) {
            if (datos.error) {
                alert(datos.error);
            } else {
                window.location.reload();
            }
            alert('bien');
        },
        "error": function () {
            alert('mal');
        }
    });
}