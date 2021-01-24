
function clear_address_fields() {
    $("#AddressLine1").val("");
    $("#City").val("");
    $("#State").val("");
}

$("#ZipCode").blur(function () {
    // The zipcode autocomplete works only for Brazil
    if ($("#Country").val() != "Brazil")
        return;

    var zipcode = $(this).val().replace(/\D/g, '');

    if (zipcode == "") {
        clear_address_fields();
        return;
    }

    if (!/^[0-9]{8}$/.test(zipcode)) {
        clear_address_fields();
        return;
    }

    $("#AddressLine1").val("...");
    $("#City").val("...");
    $("#State").val("...");

    $.getJSON("https://viacep.com.br/ws/" + zipcode + "/json/?callback=?", function (data) {

        if (!("erro" in data)) {
            $("#AddressLine1").val(data.logradouro);
            $("#City").val(data.localidade);
            $("#State").val(data.uf);
        } else {
            clear_address_fields();
        }
    });
});