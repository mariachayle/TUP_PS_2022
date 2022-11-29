
$("#btnEnviarVictima").click(function () {
    let nombreVictima = $("#txtNombreVictima").val();
    let nombreAgresor = $("#txtNombreAgresor").val();
    let descripcion = $("#txtDescripcion").val();
    let emergencia= $('#rbtTrue').is(':checked'); 
    let imagen = document.querySelector("#victimaFoto");
    let contacto = $("#contacto").val();

    if (nombreVictima === ""){
        Swal.fire("Por favor completa todos los campos requeridos para poder ayudarte");
    } else if (nombreAgresor === ""){
        Swal.fire("Por favor completa todos los campos requeridos para poder ayudarte");
    } else if(descripcion === "") {
       Swal.fire("Por favor completa todos los campos requeridos para poder ayudarte");
    } else if(!document.getElementById('rbtTrue').checked && !document.getElementById('rbtFalse').checked){
        Swal.fire("Por favor completa todos los campos requeridos para poder ayudarte");
    } else {
        altaDenuncia(nombreVictima, nombreAgresor, descripcion, imagen, emergencia, contacto);
    }
    

});

function altaDenuncia(nombreVictima,nombreAgresor, descripcion, imagen, emergencia, contacto){
    comando = {
  "nombreDenunciante": nombreVictima,
  "nombreAgresor": nombreAgresor,
  "descripcion": descripcion,
  "imagen": imagen.src,
  "emergencia": emergencia,
  "contacto": contacto
    };
    
 if(document.getElementById('rbtTrue').checked){
 $.ajax({
 
        url: "https://localhost:5001/Denuncias/CrearDenuncia",
        type: "POST",
        dataType: 'JSON',
        contentType:'application/json',
        data: JSON.stringify(comando),
        success: function (result) {
            if(result.ok){
                Swal.fire("Denuncia cargada, se te redireccionará para poder ayudarte");
                // reload pagina
   // localStorage.setItem("emailUsuario", result.return.email);
      //     window.location.replace("./menu.html"); 
          setTimeout(function(){location.href="https://api.whatsapp.com/send?text=Hola,%20necesito%20ayuda%20por%20favor!%20&phone=3515915563"}, 2000);
           //https://api.whatsapp.com/send?phone=543515915563&text=Hola,%20necesito%20ayuda%20por%20favor!"}, 2000);
            } else  
            {
                Swal.fire(result.error);
            }
        },
        error : function (error) {
            Swal.fire("Problemas con la carga de la denuncia");
   //       window.location.replace("./menu.html");
        },
    }) 
}
else{
    $.ajax({
 
        url: "https://localhost:5001/Denuncias/CrearDenuncia",
        type: "POST",
        dataType: 'JSON',
        contentType:'application/json',
        data: JSON.stringify(comando),
        success: function (result) {
            if(result.ok){
                Swal.fire("Denuncia cargada, tranquilo que vamos a trabajar para ayudarte");
                // reload pagina
   // localStorage.setItem("emailUsuario", result.return.email);
      //     window.location.replace("./menu.html"); 
         setTimeout(function(){ location.replace("./menu.html");}, 2000);
            } else  
            {
                Swal.fire(result.error);
            }
        },
        error : function (error) {
            Swal.fire("Problemas con la carga de la denuncia");
   //       window.location.replace("./menu.html");
        },
    }) 
};
}