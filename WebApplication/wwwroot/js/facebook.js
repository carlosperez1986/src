function InitialiseFacebook(appId, url) {

    window.fbAsyncInit = function () {
        FB.init({
            appId: appId,
            status: true,
            cookie: true,
            xfbml: true
        });

        FB.login(
         function (response) {
           var credentials = { uid: response.authResponse.userID, accessToken: response.authResponse.accessToken };
           console.log(response);
           SubmitLogin(credentials);
         }
        // publish_actions,
   , { auth_type: 'reauthorize', scope: 'manage_pages, email, read_page_mailboxes, read_insights,  publish_pages, user_photos, ads_read, ads_management, instagram_basic, instagram_manage_insights, read_insights, instagram_manage_comments, read_audience_network_insights' }   );

       // @*FB.Event.subscribe('auth.login', function (response) {
       //     var credentials = { uid: response.authResponse.userID, accessToken: response.authResponse.accessToken };
       //     console.log(credentials);
       //     SubmitLogin(credentials);
       // });*@

        FB.getLoginStatus(function (response) {
            if (response.status === 'connected') {
               // alert("user is logged into fb");
            }
            else if (response.status === 'not_authorized') { //alert("user is not authorised");
             }
            else { //alert("user is not conntected to facebook"); 
             }

        });
        
      
        function SubmitLogin(credentials) {
            $.ajax({
                url: "/Admin/Login",
                type: "POST",
                data: credentials,
                error: function () {
                    alert("error logging in to your facebook account.");
                },
                success: function (data) {
                    if(url != "cuentas"){
                        location.href= "Index";
                    }else{
                   
                        $("#ModalCuentasFacebook").modal();
                        
                        var DataHtml = "";
                    
                        $.each(data,function(x, item)
                        {
                         
                            DataHtml += '<div data-selected="false" id="'+item.id+'" data-id="'+item.id+'" class="col-md-12 grid-margin stretch-card">'+
                              '<div class="card">'+
                               
                                '<div class="card-body" style="padding: .88rem .81rem!important;">'+
                                  '<div class="d-flex justify-content-between">'+
                                  '<img src="'+item.profile_picture_url+'" class="img-sm rounded" alt="profile image">'+
                                  '<div class="ml-2" style="width: 70%;">'+
                                    '<h6 class=""><div class="mb-2"> <label class="form-check-label">'+ 
                                    '<i class="input-helper"></i>'+item.username + '</label></div><i class="mdi mdi-instagram text-instagram icon-xs"></i>'+item.media_count+'</h6>'+
                                 
                                    //'p class="mt-2 text-primary">  <i class="mdi mdi-instagram text-youtube icon-md"></i>Designer</p>'+
                               
                                 // '<button type="button" class="btn btn-danger" data-toggle="modal" data-target="#exampleModal-4" data-whatever="@fat">Open modal for @fat</button>' +
                                   '</div>'+
                                   '<div class=""> '+ 
                                    '  <button class="btn btn-primary btn-sm btn-icon-text" onclick=Seleccionarcuenta($(this),"'+item.id+'")>Seleccionar</button>'+
                                    '</div>'+
                                '</div>'+
                              '</div>'+
                              '</div>'+
                            '</div>';
 
                            console.log( item );
                        });
                        
                        $(".modal-body").html(DataHtml);
                       
                    }
                }
            });
        }

    };

    (function (d) {
        var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
        if (d.getElementById(id)) {
            return;
        }
        js = d.createElement('script');
        js.id = id;
        js.async = true;
        js.src = "//connect.facebook.net/en_US/all.js";
        ref.parentNode.insertBefore(js, ref);
    } (document));

};
var lista = [];
function Seleccionarcuenta(item, id)
{ 
            console.log(id);
            //$('#'+id).attr('data-selected','true');
            if($(item).hasClass("btn-primary"))
            {
                lista.push(id);
                $('#'+id).attr('data-selected','true');
                $('#'+id +' .card').css("border","1px solid #b3e6b3");
                
                console.log($('#'+id).prop('data-selected'));
                $(item).removeClass("btn-primary").addClass("btn-success").text("").append('<i style="margin:0px !important" class="mdi mdi-check btn-icon-append"></i>');
                 
            }
            else
            {
                var i = lista.indexOf(id);
                console.log(i);
                lista.splice(i,1);
                
                $('#'+id).attr('data-selected','false');
                $(item).removeClass("btn-success").addClass("btn-primary").text("Seleccionar");
                $('#'+id +' .card').css("border","1px solid #e9e8ef");
                $("#"+id+" i.mdi-check").remove();
              
            }
            
            console.log(lista);
}
