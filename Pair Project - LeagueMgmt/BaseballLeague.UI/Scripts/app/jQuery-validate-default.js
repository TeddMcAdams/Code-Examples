$.validator.setDefaults({
    highlight: function (element) {
        $(element).closest(".form-group").removeClass("has-success").addClass("has-error");
    },
    unhighlight: function(element) {
        $(element).closest(".form-group").removeClass("has-error").addClass("has-success");
    },
    errorElemenet: "span",
    errorClass: "help-block",
    errorPlacement: function (error, element) {

        var err = eval("(" + xhr.responseText + ")");
        $(element).closest(".form-control").attr("placeholder", err.Message);
    }
});