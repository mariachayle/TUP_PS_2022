$(document).ready(function () {
    $.ajax({
        url: "https://localhost:5001/Nexo/ObtenerNexo",
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
            Swal.fire("Problemas al conseguir datos del Coordinador de Curso");
        },
    })

    function cargarSelect(datos) {
        var html = "<option value=''> Seleccione una Opción </option>";
        $("#txtNexo").append(html);
        select = document.getElementById("txtNexo");
        for (let i = 0; i < datos.length; i++) {
            var option = document.createElement('option');
            option.value = datos[i].idNexo;
            option.text = datos[i].nombreNexo;
            select.add(option);
        }
    }
}),

    $("#txtNexo").change(function () {
        let id = $("#txtNexo").val();
        $.ajax({
            url: "https://localhost:5001/Nexo/ObtenerNexo/" + id,
            type: "GET",
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify(id),
            success: function (result) {
                if (result.ok) {
                    Swal.fire("Datos traidos con exito");
                    $("#txtNombreCoordinador").val(result.return.nombreNexo);
                    $("#txtTelefono").val(result.return.telNexo);
                    $("#txtDireccion").val(result.return.direccion);
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
    let id = $("#txtNexo").val();
    let nombre = $("#txtNombreCoordinador").val();
    let direccion = $("#txtDireccion").val();
    let telefono = $("#txtTelefono").val();
    let email = $("#txtEmail").val();
    if (id === "" ) {
        Swal.fire("Primero seleccione un coordinador");
    } else if (nombre === "" || direccion === "" || telefono === "" || email === "") {
        Swal.fire("Complete todos los campos");
    } else {
        updateDirectivo(id, nombre, direccion, telefono, email);
    }
});


function updateDirectivo(id, nombre, direccion, telefono, email) {
    comando = {
        "idNexo": parseInt(id),
        "nombreNexo": nombre,
        "telNexo": telefono,
        "mail": email,
        "direccion": direccion
    },

    $.ajax({
        url: "https://localhost:5001/Nexo/UpdateNexo",
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
    let id = $("#txtNexo").val();
    let isdeleted=true;
    if (id === "" ) {
        Swal.fire("Seleccione el coordinador a eliminar");
    } else {
        deleteCoordinador(id, isdeleted);
    }
});
 
function deleteCoordinador(id, isdeleted) {
    comando={
        "idNexo":id,
        "isDeleted":isdeleted
    }
    $.ajax({
        url: "https://localhost:5001/Nexo/DeleteNexo",
        type: "POST",
        dataType: 'JSON',
        contentType:'application/json',
        data: JSON.stringify(comando),
        success: function (result) {
            if (result.ok) {
                Swal.fire("Coordinador de curso eliminado");
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

