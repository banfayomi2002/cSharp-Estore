$(function () {
    //click on tab disable click if errors - register popup
    $('#sample_popup').modal('show');

    $('#myTabs .nav, nav-tabs').click(function (e) {
        $('#form0').validate();
        if ($('#form0').valid()) {
            return true;
        }
        else {
            return false;
        }
    });//tabs
 
});//main Jquery function
function logout() {
    document.getElementById("logoutForm").submit();
}//logout() 



$('a.btn-danger').on('click', function(e){
    pcd = $(this).attr("data-prodcd");
    $('#qty').val('0');
    $('#ajaxMsg').text('');
    $('#detailsGraphic').attr('src', $('#Graphic' + pcd).attr('src'));
    $('#detailsProductName').text($('#Name' + pcd).text());
    $('#detailsDescription').text($('#Descr' + pcd).data('description'));
    $('#detailsProductcode').val(pcd);
    $('#detailsPrice').text($('#Price' + pcd).text());
}); //details

// event handler for finding branch button click
$('#findthem').click(function () {
    var address = $("#address").val(); // address textbox
    geocoder = new google.maps.Geocoder(); // Aservice for converting between an address to LatLng
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) { // only if google gives us the OK
            lat = results[0].geometry.location.lat();
            lng = results[0].geometry.location.lng();
            $.getJSON("api/closebranches/" + lat + "/" + lng + "/", null, function (loc, textStatus, jqXHR) {
                locations = loc;
                $("#maps_popup").modal("show")
            }).error(function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus + " - " + errorThrown);
                alert("there was a problem connecting to the host, try again later")
            });
        }
    });
});// findthem

// once the maps modal pops up config the div
// and build the map
$('#maps_popup').on("shown.bs.modal", function () {
    var width = $(this).find(".modal-body").width();
    $('#map_canvas').css("height", width * .6);
    $('#map_canvas').css("width", width - 5);
    init_map(lat, lng, locations);
});

//Display found branches

function init_map(lat, lng, myPositions) {
    var myLatLng = new google.maps.LatLng(lat, lng);
    var map_canvas = $("#map_canvas")[0];
    var options = {
        zoom: 10,
        center: myLatLng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(map_canvas, options);
    var center = map.getCenter();
    var i2 = 0;
    var infowindow = null;
    infowindow = new google.maps.InfoWindow({ content: "holding..." });

    for (i = 0; i < myPositions.length; i++) {
        i2 = i + 1;
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(myPositions[i].Latitude, myPositions[i].Longitude),
            map: map,
            animation: google.maps.Animation.DROP,
            icon: "/Images/marker" + i2 + ".png",
            title: "Store# " + myPositions[i].BranchID + " " + myPositions[i].Street + ", "
                    + myPositions[i].City + ", " + myPositions[i].Region,
            html: "<div class='infoW'>" + "Store# " + myPositions[i].BranchID + "<br/>" +
                    myPositions[i].Street + ", " + myPositions[i].City + "<br/>" +
                    myPositions[i].Distance.toFixed(2) + "km</div>"
        });
        google.maps.event.addListener(marker, 'click', function () {
            infowindow.setContent(this.html); // added .html to the marker object.
            infowindow.close();
            infowindow.open(map, this);
        });
    }
    google.maps.event.trigger(map, "rezise");
    map.setCenter(center);
}//init_map







