﻿
$(document).ready(function () {
    var home = '/Home';

    $("#botao-login").click(function () {
        $.ajax({
            url: "/Login/GetLoginJson",
            type: "POST",
            data: {
                "usuario": $("#campo-usuario").val(),
                "senha": $("#campo-senha").val()
            },
            success: function (result) {
                console.log(result);
                alert("OK!");
                //$('#nome-user-login').text(result.user);
                //$(window.document.location).attr('href', home);
                ////ndow.location = 
            },
            error: function () {
                alert("Error!")
            }
        });
    });
});




///$(window.document.location).attr('href',novaURL); redirection para ir a pagina index