$('#btnEnviarPassword').click(function () {

    let mail = $("#txtEmail").val();
    let password1 = $("#txtPassword1").val();
    let password2 = $("#txtPassword2").val();

    if (mail === "") {
        Swal.fire("Ingrese su Email");
    }
    else if (password1 === "" || password2 === "" ) {
        Swal.fire("Ingrese su Password");
    }
    else if (password1 !== password2){
        Swal.fire("Las contraseñas no coinciden");
    }
    else{
        realizarLogin(mail, password2);
    }
});

function realizarLogin(mail, password) {
    comando = {
        "Mail": mail,
        "Password": password
    };

    $.ajax({
        url: "https://localhost:5001/Pass/RecuperarNexo",
        type: "POST",
        dataType: 'JSON',
        contentType: 'application/json',
        data: JSON.stringify(comando),

        success: function (result) {
            
            if (result.ok) {               
                Swal.fire("Password actualizada");
                // localStorage.setItem("emailUsuario", result.return.email);
                window.location.replace("./login.html")
            }
        }
    });

    $.ajax({
        url: "https://localhost:5001/Pass/RecuperarDirector",
        type: "POST",
        dataType: 'JSON',
        contentType: 'application/json',
        data: JSON.stringify(comando),

        success: function (result) {
            if (result.ok) {
                Swal.fire("Password actualizada");
                // localStorage.setItem("emailUsuario", result.return.email);
                window.location.replace("./login.html")
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
