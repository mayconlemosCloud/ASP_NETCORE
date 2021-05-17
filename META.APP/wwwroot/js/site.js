


$('#Visao').on('change', function () {
    var empty = ''
    alert('limpando')
    $('#table_id > tbody').empty()
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/home/GetPorVisao",
        data: JSON.stringify({ Modo: parseInt(this.value) }),
        success: function (response) {
            if (response.success) {
                console.log(response.responseText)
                var emissoras = '';
                var data = response.responseText
                $.each(data, function (key, value) {
                    emissoras += '<tr>';                   
                    emissoras += '<td>' + value.emissoras + '</td>';
                    emissoras += '<td>' + value.audiencias.pontos_audiencia + '</td>';
                    emissoras += '<td>' + value.audiencias.data_hora_audiencia + '</td>';              
                    emissoras += '<tr>';
                });
                $('#table_id').append(emissoras);
                var emissorasSelect = ''
                $.each(data, function (key, value) {

                    emissorasSelect += '<option id="nomeEmissora" value="' + value.emissoras + '" >' + value.emissoras + '</option>'
                });

                $('#optEmissora').append(emissorasSelect)


            } else {

                console.log(response.responseText);
            }
        },
        error: function (response) {
            alert("error!" + response.responseText);  // 
        }
    })
});


function CadastrarEmissora() {
    console.log('Teste')
    var nomeEmissora = $('#NomeEmissora').val();

    $.ajax({
        type: "POST",
       
        contentType: "application/json; charset=utf-8",
        url: "/home/InsertEmissoras",
        data: JSON.stringify({ emissoras: nomeEmissora}),
        success: function (response) {
            if (response.success) {
                alert(response.responseText);
            } else {
               
                alert(response.responseText);
            }
        },
        error: function (response) {
            alert("error!" + response.responseText );  // 
        }
          })
}

function CadastrarAudiencia() {
    
    var pontosaudiencia = parseInt($('#Pontos_audiencia').val());
    var nomeEmissora = $('#optEmissora ').val()
     
       


    console.log(typeof(pontosaudiencia))
    $.ajax({
        type: "POST",

        contentType: "application/json; charset=utf-8",
        url: "/home/InsertAudiencia",
        data: JSON.stringify({ Emissora_audiencia: nomeEmissora, Pontos_audiencia: pontosaudiencia }),
        success: function (response) {
            if (response.success) {
                alert(response.responseText);
                $('#Pontos_audiencia').val() =''
            } else {

                alert(response.responseText);
            }
        },
        error: function (response) {
            alert("error!" + response.responseText);  // 
        }
    })
}

function CarregarEmissoras() {
    $('#optEmissora > option').empty().remove()
  
    $.ajax({
        type: "GET",

        contentType: "application/json; charset=utf-8",
        url: "/home/Emissoras",       
        success: function (response) {
            if (response.success) {
                console.log(response.responseText)
                var emissoras = '';
                var data = response.responseText
                $.each(data, function (key, value) {
                    emissoras += '<tr id="trEmissora">';
                    emissoras += '<td>' + value.id + '</td>';
                    emissoras += '<td>' + value.emissoras + '</td>';
                    emissoras += '<td>' + '<a href="/home/EditarEmissora/'  + value.id +'  ">Editar</a>  ' + '</td>';
                    emissoras += '<td>' + '<a href="/home/DeletarEmissora/' + value.id +'  ">Deletar</a> ' +' </td>';
                    emissoras += '<tr>';
                });
                $('#table_id_emissora').append(emissoras);
                var emissorasSelect = ''
                $.each(data, function (key, value) {               
                 
                    emissorasSelect += '<option id="nomeEmissora" value="'+value.emissoras+'" >' + value.emissoras + '</option>'
                });

                $('#optEmissora').append(emissorasSelect)
             

            } else {

                console.log(response.responseText);
            }
        },
        error: function (response) {
            alert("error!" + response.responseText);  // 
        }
    })
}


$.validator.addMethod(
    "regex",
    function (value, element, regexp) {
        var re = new RegExp(regexp);
        return this.optional(element) || re.test(value);
    },
    "Please check your input."
);

$("#form1").validate({
             rules: {
                NomeEmissora: { required: true, regex: "^[a-zA-Z0-9]+" }

                    },
             messages: {
                 NomeEmissora: { required: 'O nome não pode ser vazio', regex: "Não é permitido caracteres especiais "}
                }

});






