$(document).ready(function() {
    $("#createTeamForm").validate({
        rules: {
            Name: {
                required: true,
                minlength: 3,
                maxlength: 50,
                nowhitespace: true
            },
            "Manager.FirstName": {
                required: true,
                minlength: 2,
                maxlength: 50,
                nowhitespace: true
   
            },
            "Manager.LastName": {
                required: true,
                minlength: 2,
                maxlength: 50,
                nowhitespace: true
            }
        },
        messages: {
            Name: {
                required: "Please specify team name",
                minlength: "Must be at least three characters",
                maxlength: "Must be no more than fifty characters",
                nowhitespace: "blah"
            }
            
        }
    });
});