window.addEventListener('DOMContentLoaded', function(event){
    function authenticator(){
        let role;
        try{
            let jwt=sessionStorage.getItem("jwt");
            let jwtData = jwt.split('.')[1];
            let decodedJwtJsonData = window.atob(jwtData);
            let decodedJwtData = JSON.parse(decodedJwtJsonData);
            role = decodedJwtData['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        }catch{
            role="";
        }
        if(role=='User'){
            window.location.href = '../mainUser.html'
        }
        if(role!='Admin'){
            window.location.href = '../otherMain.html'
        }
    }






    //authenticator();
});

