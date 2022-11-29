$(document).ready(function () {
    $.ajax({
      url: "https://localhost:5001/Nexo/ObtenerNexo",
      type: "GET",

      success: function (result) {
        if (result.ok) {
          $('#example2').DataTable({
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
           // "sorting": true,
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
  });

  function crearTabla(datos) {
    for (var i = 0; i < datos.length; i++) {
      var html = "<tr>";
      html += "<td>" + datos[i].nombreNexo+ "</td>";
      html += "<td>" + datos[i].telNexo + "</td>";
      html += "<td>" + datos[i].direccion + "</td>";
      html += "<td>" + datos[i].mail + "</td>";
      html += "</tr>";
      $("#cuerpoTabla").append(html);
    }
  } 