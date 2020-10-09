function logearUsuario() {
    $.ajax({
        "url": "http://localhost:58640/Home/Login",
        "type": "POST",
        "data": {
            'usuario': $('#usuario').val(),
            'password': $('#password').val()
        },
        "dataType": "json",
        "success": function (datos) {
            // Hace algo.
            //alert("Funcionó");
            if (datos.error) {
                window.location.href = 'http://localhost:58640/Home/Error';
            } else {
                window.location.href = 'http://localhost:58640/Home/Index';
            }
        },
        "error": function () {
            // Hace algo.
            alert("Error");
        }
    });
}