$(document).ready(function () {
    //you could do an immediately invoked function expression
    //(function () {
    var x = 0;
    var s = "";
    
    var theForm = $("#theForm");
    theForm.hide();

    var buyButton = $("#buyButton");
    buyButton.on("click", function () {
        console.log("you are buying");
    });

    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("you clicked on: " + $(this).text());
    });

    /*
      add an event handler.
      using anonymous function
    */
    //buyButton.addEventListener("click", function () {
    //    console.log("you are buying");
    //})
    //}) ();

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");
    $popupForm.hide();

    $loginToggle.on("click", function () {
        $popupForm.slideToggle(500);
    });
});
