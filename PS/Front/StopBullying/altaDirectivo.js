$("#btnAlta").click(function () {
    let nombre = $("#txtNombreDirectivo").val();
    let telefono = $("#txtTelefono").val();
    let direccion = $("#txtDireccion").val();
    let email = $("#txtEmail").val();
    let usuario = $("#txtUsuario").val();
    let password = $("#txtPassword").val();
    if (nombre === "" || telefono === "" || direccion === "" || usuario === "" || password === "") {
        Swal.fire("Ingrese los datos requeridos");
    } else {
        altaDirectivo(nombre, telefono, direccion, email, usuario, password);
    }
});
 
function altaDirectivo(nombre, telefono, direccion, email, usuario, password){
    comando = {
  "NombreDirector": nombre,
  "TelDirector": telefono,
  "Direccion1": direccion,
  "Mail": email,
  "Usuario": usuario,
  "Password": password
    }

 $.ajax({
 
        url: "https://localhost:5001/Direccion/CrearDirector",
        type: "POST",
        dataType: 'JSON',
        contentType:'application/json',
        data: JSON.stringify(comando),
        success: function (result) {
            if(result.ok){
                Swal.fire("Directivo creado correctamente!");
                setTimeout(function () { location.reload(); }, 1000);
         
            } else  
            {
                Swal.fire(result.error);
            }
        },
        error : function (error) {
            Swal.fire("Problemas con la creación del Coordinador");
   //       window.location.replace("./menu.html");
        },
    }) 
}

