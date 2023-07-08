window.addEventListener('DOMContentLoaded',async function(event){
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
    function redirectionRol(){
        if(myRole()==""){
            mostrarPopUpAcess();
        }
    }
    function mostrarPopUpAcess(){
        document.getElementById('overlay-access').classList.add('active');
        document.getElementById('popup-access').classList.add('active');
    }
    function cerrarPopUPAcess(event){
        window.location.href = '../mainUser.html'
        document.getElementById('overlay-access').classList.remove('active');
        document.getElementById('popup-access').classList.remove('active');
    }
    function redirectionLogin(){
        debugger;
        window.location.href = '/users/login.html'
    }
    redirectionRol();
    document.getElementById('session-login-btn').addEventListener('click',redirectionLogin);
    document.getElementById('btn-cerrar-popup-access').addEventListener('click',cerrarPopUPAcess);
});
