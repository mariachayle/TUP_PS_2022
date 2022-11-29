$(document).ready(function () {
    $.ajax({
        url: "https://localhost:5001/Direccion/ObtenerDirector",
        type: "GET",
        success: function (result) {
            if (result.ok) {
                resultadoS = result.return;
                cargarSelect(result.return);
            } else {
                swal.fire(result.error);
            }
        },
        error: function (error) {
            Swal.fire("Problemas al conseguir datos de Alumnos");
        },
    })

    function cargarSelect(datos) {
        var html = "<option value=''> Seleccione una Opción </option>";
        $("#txtDirectivo").append(html);
        select = document.getElementById("txtDirectivo");
        for (let i = 0; i < datos.length; i++) {
            var option = document.createElement('option');
            option.value = datos[i].idDirector;
            option.text = datos[i].nombreDirector;
            select.add(option);
        }
    }
}),

    $("#txtDirectivo").change(function () {
        let id = $("#txtDirectivo").val();
        $.ajax({
            url: "https://localhost:5001/Direccion/ObtenerDirectivo/" + id,
            type: "GET",
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify(id),
            success: function (result) {
                if (result.ok) {
                    Swal.fire("Datos traidos con exito");
                    $("#txtNombreDirectivo").val(result.return.nombreDirector);
                    $("#txtTelefono").val(result.return.telDirector);
                    $("#txtDireccion").val(result.return.direccion1);
                    $("#txtEmail").val(result.return.mail);
                } else {
                    Swal.fire(result.error);
                }
            },
            error: function (error) {
                Swal.fire("Problemas en el servidor");
            },
        })
    });



// Update
$("#btnUpdate").click(function () {
    let id = $("#txtDirectivo").val();
    let nombre = $("#txtNombreDirectivo").val();
    let direccion = $("#txtDireccion").val();
    let telefono = $("#txtTelefono").val();
    let email = $("#txtEmail").val();
    if (id === "" ) {
        Swal.fire("Primero seleccione un directivo");
    } else if (nombre === "" || direccion === "" || telefono === "" || email === "") {
        Swal.fire("Complete todos los campos");
    } else {
        updateDirectivo(id, nombre, direccion, telefono, email);
    }
});


function updateDirectivo(id, nombre, direccion, telefono, email) {
    comando = {
        "idDirector": parseInt(id),
        "nombreDirector": nombre,
        "telDirector": telefono,
        "mail": email,
        "direccion1": direccion
    },

    $.ajax({
        url: "https://localhost:5001/Direccion/UpdateDireccion",
        type: "POST",
        dataType: 'JSON',
        contentType: 'application/json',
        data: JSON.stringify(comando),
        success: function (result) {
            if (result.ok) {
                Swal.fire("Se actualizó correctamente");
                // reload pagina
                setTimeout(function () { location.reload(); }, 1000);
              
            } else {
                Swal.fire("Problema en el Servidor");
            }
        },
        error: function (error) {
            Swal.fire("Problemas en el servidor");
        },
    });

}

//Delete
$("#btnEliminar").click(function () {
    let id = $("#txtDirectivo").val();
    let isdeleted=true;
    if (id === "" ) {
        Swal.fire("Seleccione el directivo a eliminar");
    } else {
        deleteDirectivo(id,isdeleted);
    }
});
 
function deleteDirectivo(id, isdeleted) {
    comando={
        "idDirector":id,
        "isDeleted":isdeleted
    }
    $.ajax({
        url: "https://localhost:5001/Direccion/DeleteDireccion",
        type: "POST",
        dataType: 'JSON',
        contentType:'application/json',
        data: JSON.stringify(comando),
        success: function (result) {
            if (result.ok) {
                Swal.fire("Directivo eliminado");
                // reload pagina
                setTimeout(function(){ location.reload();}, 1000);
        }else { 
            Swal.fire("Problemas en el servidor");
        }
        },
    });
}
$("#btnCancelar").click(function(){
    location.reload();
})
