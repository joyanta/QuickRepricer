// Write your Javascript code.
(function () {
    $(".parallax").parallax();

    $(".button-collapse").sideNav();
 
    $(".dropdown-button").dropdown();

    $('.collapsible').collapsible();

    $('.modal-trigger').leanModal({
        dismissible: true, // Modal can be dismissed by clicking outside of the modal
        opacity: 0, // Opacity of modal background
        in_duration: 300, // Transition in duration
        out_duration: 200, // Transition out duration
        starting_top: '4%', // Starting top style attribute
        ending_top: '20%', // Ending top style attribute
        ready: function () {
            $(".button-collapse").sideNav('hide');
        }
    });

    $(".disabled").click(function (e) {
        e.preventDefault();
    });

})();