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
        if(role=='Admin'){
            window.location.href = '../main.html'
        }
        if(role!='User'){
            window.location.href = '../otherMain.html'
        }
    }

    function redirectionLogin(){
        window.location.href = '/users/login.html'
    }
    function redirectionSingUp(){
        window.location.href = '/users/signUp.html'
    }
    document.getElementById('registrarse-btn').addEventListener('click',redirectionSingUp);
    document.getElementById('session-login-btn').addEventListener('click',redirectionLogin);


    
    //authenticator();




});

