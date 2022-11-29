$(document).ready(function () {

    $.ajax({
        url: "https://localhost:5001/Denuncias/ObtenerDenunciasEmergencia",
        type: "GET",

        success: function (result) {
            if (result.ok) {

                $('#example1').DataTable({
                    "language": {
                        "lengthMenu": "Mostrar _MENU_ registros por pagina",
                        "zeroRecords": "No hay registros aún",
                        "info": "Mostrando pagina _PAGE_ de _PAGES_",
                        "infoEmpty": "No se encontraron registros",
                       "paginate":{
                        "next":"Siguiente",
                        "previous":"Anterior"
                       },
                        "search":"Buscar"
                    },
                    "destroy": true, // In order to reinitialize the datatable
                    "pagination": true, // For Pagination
                    "sorting": true,
                    "order": [[1, "desc"]], //or asc 
                    "columnDefs": [{ "targets": 1, "type": "date-eu" }],
                    "ordering": true,
                    "aaData": crearTabla(result.return)
                });
            }
            else {
                Swal.fire(result.error);
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
})



function crearTabla(datos) {
    for (var i = 0; i < datos.length; i++) {
        var html = "<tr>";
        html += "<td hidden id='idDen'>" + datos[i].idDenuncia + "</td>";
        let fechaCorta = new Date(datos[i].fecha);
        let formato = fechaCorta.getDate() + "/" + (fechaCorta.getMonth() + 1) + "/" + fechaCorta.getFullYear();
        html += "<td>" + formato + "</td>";

        html += "<td>" + datos[i].nombreDenunciante + "</td>";
        if (datos[i].nombreObservador == null) {
            html += "<td>" + "---" + "</td>";
        } else {
            html += "<td>" + datos[i].nombreObservador + "</td>";
        };
        html += "<td>" + datos[i].nombreAgresor + "</td>";
        html += "<td>" + datos[i].descripcion + "</td>";
        if (datos[i].idPrioridad == 1) {
            html += "<td>" + "Baja" + "</td>";
        }
        else if (datos[i].idPrioridad == 2) {
            html += "<td>" + "Media" + "</td>";
        }
        else {
            html += "<td>" + "Alta" + "</td>";
        };
        if (datos[i].idEstado == 2) {
            html += "<td>" + "En Progreso" + "</td>";
        }
        else if (datos[i].idEstado == 3) {
            html += "<td>" + "Resuelto" + "</td>";
        } else {
            html += "<td>" + "Pendiente" + "</td>"
        };

        html += '<td><a type="button" id="btnDenuncia"  data-toggle="tooltip" onclick="getId(' + datos[i].idDenuncia + ')"  class="btn-sm btn-info" >Ver Denuncia</a></th>';
        // "<button type='button' class='btn btn-secondary btn-sm btn-block'>Eliminar</button>" + "</td>"; 
        html += '</tr>';
        $("#cuerpoTabla").append(html);
    }
}



function getId(id) {

    $.ajax({
        url: "https://localhost:5001/Denuncias/ObtenerDenunciasId/" + id,
        type: "GET",
        dataType: 'JSON',
        contentType: 'application/json',
        //  data: JSON.stringify(id),
        success: function (result) {
            if (result.ok) {
                let Denuncia = result.return;

                Swal.fire("Datos traidos con exito");
                $("#id").val(Denuncia.idDenuncia);
                $("#nombreVictima").val(Denuncia.nombreDenunciante);
                $("#nombreAgresor").val(Denuncia.nombreAgresor);
                $("#nombreObservador").val(Denuncia.nombreObservador);
                //    $("#estado").val(Denuncia.estado);
                $("#txtPrioridad").val(Denuncia.idPrioridad);
                $("#txtCoordinador").val(Denuncia.idNexo);
                $("#txtDirectivo").val(Denuncia.idDirector);
                $("#descripcion").val(Denuncia.descripcion);
                $("#imagen").val(Denuncia.imagen);
                imagen.src = $("#imagen").val();
                $("#txtNotas").val(Denuncia.notas);
                $("#contacto").val(Denuncia.contacto);

                document.getElementById("modalDetalle").removeAttribute("hidden");
                document.getElementById("tablaPrimera").setAttribute("hidden",true);

            }
            else {
                Swal.fire(result.error);
            }
        },
        error: function (result) {
            Swal.fire(result.error);
        }

    });

    $.ajax({
        url: "https://localhost:5001/Nexo/ObtenerNexo",
        type: "GET",
        success: function (result) {
            if (result.ok) {
                cargarSelectNexo(result.return)
            } else {
                swal.fire(result.error)
            }
        },
        error: function (error) {
            console.log(error)
        }

    })

    function cargarSelectNexo(datos) {
        var html = "<option value=''> Seleccione una Opción </option>";
        $("#txtCoordinador").append(html);
        select = document.getElementById("txtCoordinador");
        for (let i = 0; i < datos.length; i++) {
            var option = document.createElement('option');
            option.value = datos[i].idNexo;
            option.text = datos[i].nombreNexo;
            select.add(option);
        }
    };

    $.ajax({
        url: "https://localhost:5001/Direccion/ObtenerDirector",
        type: "GET",
        success: function (result) {
            if (result.ok) {
                cargarSelect(result.return)
            } else {
                swal.fire(result.error)
            }
        },
        error: function (error) {
            console.log(error)
        }

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
    };

    $.ajax({
        url: "https://localhost:5001/Prioridad/ObtenerPrioridades",
        type: "GET",
        success: function (result) {
            if (result.ok) {
                cargarSelectPrioridad(result.return)
            } else {
                swal.fire(result.error)
            }
        },
        error: function (error) {
            console.log(error)
        }

    })

    function cargarSelectPrioridad(datos) {
        var html = "<option value=''> Seleccione una Opción </option>";
        $("#txtPrioridad").append(html);
        select = document.getElementById("txtPrioridad");
        for (let i = 0; i < datos.length; i++) {
            var option = document.createElement('option');
            option.value = datos[i].idPrioridad;
            option.text = datos[i].prioridad1;
            select.add(option);
        }
    };
}

$("#btnCancelarModal").click(function(){
    document.getElementById("modalDetalle").setAttribute("hidden",true);
    document.getElementById("tablaPrimera").removeAttribute("hidden");

    setTimeout(function () { location.reload(); }, 300);
})

$("#btnUpdate").click(function () {
    let idDen = $("#id").val();
    let prioridad = $("#txtPrioridad").val();
    let idNexo = $("#txtCoordinador").val();
    let idDirector = $("#txtDirectivo").val();
    let notas = $("#txtNotas").val();
    if (idDirector === ""){
        Swal.fire("Por favor seleccione un Coordinador de Curso");
    }else if( idNexo === "" ){
        Swal.fire("Por favor seleccione un Directivo");
    }else if( prioridad === ""){
        Swal.fire("Por favor seleccione la Prioridad del Caso");
    }else if(notas === "") {
         Swal.fire("Por favor ingrese Notas de lo trabajado");
   
    } else {
        updateDenuncia(idDen,  idDirector, idNexo,prioridad, notas);
    }
});

function updateDenuncia(idDen,  idDirector, idNexo,prioridad, notas) {
    comando = {
        "idDenuncia": parseInt(idDen),
        "idNexo": parseInt(idNexo),
        "idDirector": parseInt(idDirector),
        "idPrioridad": parseInt(prioridad),
        "notas": notas

    },

        $.ajax({
            url: "https://localhost:5001/Denuncias/UpdateDenuncia",
            type: "POST",
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify(comando),
            success: function (result) {
                if (result.ok) {
                    Swal.fire("Denuncia actualizada correctamente");
                    document.getElementById("modalDetalle").setAttribute("hidden",true);
                    document.getElementById("tablaPrimera").removeAttribute("hidden");
                    // reload pagina
                    setTimeout(function () { location.reload(); }, 500);
                } else {
                    Swal.fire("Problemas en el Servidor");
                }
            },
            error: function (error) {
                Swal.fire("Problemas en el servidor");
            },
        });

}

$("#btnFinalizarM").click(function () {
    let idDenuncia = $("#id").val();
    let prioridad = $("#txtPrioridad").val();
    let idNexo = $("#txtCoordinador").val();
    let idDirector = $("#txtDirectivo").val();
    let notas = $("#txtNotas").val();
    if (idNexo === "") {
        Swal.fire("Debe seleccionar un Coordinador Responsable de la Denuncia");
    } else if (idDirector === "") {
        Swal.fire("Debe seleccionar un Directivo Responsable de la Denuncia");
    } else if (prioridad === "") {
        Swal.fire("Debe seleccionar una prioridad");
    } else if (notas === "") {
        Swal.fire("Debe ingresar en Notas como trató la denuncia");
    } else {
        finDenuncia(idDenuncia, idDirector,  idNexo, prioridad, notas);
    }
});

function finDenuncia(idDen, idDirector,  idNexo,prioridad, notas) {
    comando = {
        "idDenuncia": parseInt(idDen),
        "idNexo": parseInt(idNexo),
        "idDirector": parseInt(idDirector),
        "idPrioridad": parseInt(prioridad),
        "notas": notas
    },

        $.ajax({
            url: "https://localhost:5001/Denuncias/FinalizarDenuncia",
            type: "POST",
            dataType: 'JSON',
            contentType: 'application/json',
            data: JSON.stringify(comando),
            success: function (result) {
                if (result.ok) {
                    Swal.fire("Denuncia finalizada exitosamente");
                    document.getElementById("modalDetalle").setAttribute("hidden",true);
                    document.getElementById("tablaPrimera").removeAttribute("hidden");
                    // reload pagina
                    setTimeout(function () { location.reload(); }, 500);
                } else {
                    Swal.fire("Problema Server");
                }
            },
            error: function (error) {
                Swal.fire("Problemas en el servidor");
            },
        });

}