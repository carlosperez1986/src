"use strict";
   var connection = new signalR.HubConnectionBuilder()
            .withUrl("/signalr",  { transport: signalR.HttpTransportType.LongPolling }).build();
  

 
async function start() {
    try {
        await connection.start();
        console.log('connected');
    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};

connection.onclose(async () => {
    await start();
});
  
connection.on("message", (user) => { 
        console.log(user);
        var t = (user["count"] - 1);
        if(t > 0){
         $("#usuariosOnline").show();
            $("#usuariosOnline").html("Tu y " + t + " personas más estan viendo este paquete en este instante.");
        }else{
         $("#usuariosOnline").hide();
        }
        
});

connection.on("reconnect", (r) => { 
        console.log(r); 
        connection.start()
            .then(() => {
              console.log(this.connection);
          ""
            })
            .catch(function (err) {
        return console.log(err.toString());
});
});

(async()=>{ await start(); })();



//.done(function(x,y){ 
// console.log(connection);
//    console.log(x);
//    console.log(y);

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});
