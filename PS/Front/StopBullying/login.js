$('#btnLogin').click(function () {

    let usuario = $("#txtUsuario").val();
    let password = $("#txtPassword").val();

    if (usuario === "") {
        Swal.fire("Ingrese su Usuario");
    }
    else if (password === "") {
        Swal.fire("Ingrese su Password");
    }
    else {
        realizarLogin(usuario, password);
    }
});

function realizarLogin(usuario, password) {
    comando = {
        "Usuario": usuario,
        "Password": password
    };

    $.ajax({
        url: "https://localhost:5001/Direccion/Login",
        type: "POST",
        dataType: 'JSON',
        contentType: 'application/json',
        data: JSON.stringify(comando),

        success: function (result) {
            
            if (result.ok) {               
                Swal.fire("Login exitoso");
                // localStorage.setItem("emailUsuario", result.return.email);
                window.location.replace("./index.html")
            }
            else{
                realizarLogin2(usuario,password);
            }
   
        }
     //   error: function (error) {
        //    console.log(error);
      //  }

    });
}
function realizarLogin2(usuario, password){
    comando = {
        "Usuario": usuario,
        "Password": password
    };

    $.ajax({
        url: "https://localhost:5001/Nexo/Login",
        type: "POST",
        dataType: 'JSON',
        contentType: 'application/json',
        data: JSON.stringify(comando),

        success: function (result) {
            if (result.ok) {
                Swal.fire("Login exitoso");
                // localStorage.setItem("emailUsuario", result.return.email);
                window.location.replace("./indexNexo.html")
            }
            else {
                Swal.fire(result.error);
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}


