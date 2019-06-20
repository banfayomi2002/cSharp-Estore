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

