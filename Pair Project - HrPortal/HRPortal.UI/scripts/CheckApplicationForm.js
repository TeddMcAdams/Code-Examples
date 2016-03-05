function checkBeforeSubmit() {
    "use strict";
    var submitReady;
    submitReady = true;
    // Check if last name is empty
    if (document.getElementById("LastName").value === "" || document.getElementById("LastName").value.replace(/\s/g, "").length === 0)
    {
        document.getElementById("LastName").classList.add("alert");
        document.getElementById("LastName").classList.add("alert-danger");
        submitReady = false;
    }
    else
    {
        document.getElementById("LastName").classList.remove("alert");
        document.getElementById("LastName").classList.remove("alert-danger");
    }
    // Check if first name is empty
    if (document.getElementById("FirstName").value === "" || document.getElementById("FirstName").value.replace(/\s/g, "").length === 0)
    {
        document.getElementById("FirstName").classList.add("alert");
        document.getElementById("FirstName").classList.add("alert-danger");
        submitReady = false;
    }
    else
    {
        document.getElementById("FirstName").classList.remove("alert");
        document.getElementById("FirstName").classList.remove("alert-danger");
    }
    // Check if phone is empty
    if (document.getElementById("Phone").value === "" || document.getElementById("Phone").value.replace(/\s/g, "").length === 0)
    {
        document.getElementById("Phone").classList.add("alert");
        document.getElementById("Phone").classList.add("alert-danger");
        submitReady = false;
    }
    else
    {
        document.getElementById("Phone").classList.remove("alert");
        document.getElementById("Phone").classList.remove("alert-danger");
    }
    // Check if why us is empty
    if (document.getElementById("WhyInterested").value === "" || document.getElementById("WhyInterested").value.replace(/\s/g, "").length === 0)
    {
        document.getElementById("WhyInterested").classList.add("alert");
        document.getElementById("WhyInterested").classList.add("alert-danger");
        submitReady = false;
    }
    else
    {
        document.getElementById("WhyInterested").classList.remove("alert");
        document.getElementById("WhyInterested").classList.remove("alert-danger");
    }
    // Submit if required is filled out
    if (submitReady === true)
    {
        document.getElementById("applicationForm").submit();
    }
}