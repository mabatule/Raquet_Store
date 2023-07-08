window.addEventListener('DOMContentLoaded', function(event){
    
    function myRole(){
        try{
            let jwt=sessionStorage.getItem("jwt");
            let jwtData = jwt.split('.')[1];
            let decodedJwtJsonData = window.atob(jwtData);
            let decodedJwtData = JSON.parse(decodedJwtJsonData);
            return decodedJwtData['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        }catch{
            return "";
        }
    }
    function logOut()
    {
        window.sessionStorage.removeItem('jwt');
        window.location.href = "../mainUser.html"; 
        document.getElementById('user-btn').classList.remove("activate");

        document.getElementById('logout-btn').classList.remove("activate");
        document.getElementById('registrarse-btn').classList.remove("low");
        document.getElementById('session-login-btn').classList.remove("low");
}
    function authenticator(){
        if(myRole()=='Admin' || myRole()=='User'){
            document.getElementById('user-btn').innerHTML='<i class="fas fa-user"></i> '+sessionStorage.getItem("email");;
            document.getElementById('user-btn').classList.add("activate");

            document.getElementById('logout-btn').classList.add("activate");
            document.getElementById('registrarse-btn').classList.add("low");
            document.getElementById('session-login-btn').classList.add("low");
        }
    }
    document.getElementById("logout-btn").addEventListener('click', logOut);
    authenticator();
});