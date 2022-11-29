$(document).ready(function () {

  $.ajax({
    url: "https://localhost:5001/Denuncias/ObtenerDenuncias",
    type: "GET",

    success: function (result) {
      if (result.ok) {
        $('#denuncias').DataTable({
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
          "order": [[ 1, "desc" ]], //or asc 
                    "columnDefs" : [{"targets":1, "type":"date-eu"}],
           "ordering": true,
          "aaData": crearTabla(result.return)
        });
        //  crearTabla(result.return);
      }
      else {
        Swal.fire(result.error);
      }
    },
    error: function (error) {
      console.log(error);
    }
  });
});

/*
  function crearTabla(datos, tabla){
    datos.forEach(denuncia=>{
      
      let boton='';
      boton+= "<td><div class=\"table-data-feature\">"
      boton+= "<button class=\"btn-sm btn-info\" data-toggle=\"modal\" id=\"btnVerMas"+denuncia.idDenuncia+"\" dataTarget=\"modalDenuncia\"></button></div></td>"
      tabla.row.add([denuncia.nombreDenunciante, denuncia.nombreObservador, denuncia.nombreAgresor, denuncia.descripcion, denuncia.notas, boton]).draw();
    });
  }

*/
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

    // html += "<td>" + datos[i].notas + "</td>";
    //  html += "<td>" + datos[i].imagen + "</td>";
    //  html += "<td>" + + "</td>";
    html += '<td><a type="button" id="btnvermas"  data-toggle="modal" data-target="#modalDenuncia" onclick="getId(' + datos[i].idDenuncia + ')"  class="btn-sm btn-info" >Ver Denuncia</a></th>';
    // "<button type='button' class='btn btn-secondary btn-sm btn-block'>Eliminar</button>" + "</td>"; 
    html += '</tr>';

    $("#cuerpoTabla").append(html);
  }
}



function getId(id) {

  $.ajax({
    url: "https://localhost:5001/Denuncias/ObtenerDenunciasId/" + id,
    type: "GET",
    //  dataType: 'JSON',
    //  contentType:'application/json',
    //  data: JSON.stringify(id),
    success: function (result) {
      if (result.ok) {
        let Denuncia = result.return;

        Swal.fire("Datos traidos con exito");
        $("#nombreVictima").val(Denuncia.nombreDenunciante);
        $("#nombreAgresor").val(Denuncia.nombreAgresor);
        $("#nombreObservador").val(Denuncia.nombreObservador);
        $("#descripcion").val(Denuncia.descripcion);
        $("#imagen").val(Denuncia.imagen);
        imagen.src = $("#imagen").val();
        $("#contacto").val(Denuncia.contacto);
      }
      else {
        Swal.fire(result.error);
      }
    },
    error: function (result) {
      Swal.fire(result.error);
    }

  })
}