window.addEventListener('DOMContentLoaded', function(event){

    const baseRawUrl = 'http://localhost:5077';
    const baseUrl = `${baseRawUrl}/api`;

    function redirectToSignIn(event){
        window.location.href = 'signUp.html';
    }
    function loginUser(event){
        event.preventDefault();
        let url = `${baseUrl}/users/Login`;
        let passEmail=false;
        let passPasword=false;
        if(!Boolean(event.currentTarget.email.value)){  
            document.getElementById('email-alert').classList.add('active');
        }else{
            document.getElementById('email-alert').classList.remove('active');
            passEmail=true;
        }
        
        if(!Boolean(event.currentTarget.password.value)){
            document.getElementById('password-alert').classList.add('active');
        }else{
            document.getElementById('password-alert').classList.remove('active');
            passPasword=true;
        }

        if(!passPasword && !passEmail){
            return;
        }
        // Hasta aqui se valido que los campos no esten vacios



        let emailUser = event.currentTarget.email.value;
        var data = {
            email: event.currentTarget.email.value,
            password: event.currentTarget.password.value,
        }
        fetch(url, {
            headers: { "Content-Type": "application/json; charset=utf-8" },
            method: 'POST',
            body: JSON.stringify(data)
        }).then(response => {
            if(response.status === 200){
                response.json().then((data)=>{

                    sessionStorage.setItem("jwt", data.token);
                    sessionStorage.setItem("email",emailUser);
                    let jwt= data.token;
                    let jwtData = jwt.split('.')[1]
                    let decodedJwtJsonData = window.atob(jwtData)
                    let decodedJwtData = JSON.parse(decodedJwtJsonData)
                    let role = decodedJwtData['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
                    
                    
                    document.getElementById("loginMessage").innerHTML= "Usuario aceptado, espere a ser redireccionado.";
                    document.getElementById("loginMessage").style.backgroundColor = "green";
                    document.getElementById("loginMessage").style.color = "white";
                    setTimeout(function(){
                        if(role=='Admin'){
                            window.location.href = '../main.html'
                        }else{
                            window.location.href = '../mainUser.html'
                        }
                    },2000);
                });
                
            } 
            else {
                response.json()
                .then((error)=>{
                    if(response.status === 400){ 
                        let message = error.message;
                        if (error.message==undefined)
                            message = "Usuario no existente! Registrese";
                        document.getElementById("loginMessage").innerHTML= message;
                        document.getElementById("loginMessage").style.backgroundColor = "red";
                        document.getElementById("loginMessage").style.color = "white";
                    }
                    
                });
            }

        });
    }
    function redirectionLogin(){
        window.location.href = '/users/login.html'
    }
    function redirectionSingUp(){
        window.location.href = '/users/signUp.html'
    }
    document.getElementById('registrarse-btn').addEventListener('click',redirectionSingUp);
    document.getElementById('session-login-btn').addEventListener('click',redirectionLogin);

    document.getElementById('login-box').addEventListener('submit',loginUser);
    document.getElementById('Sinup-user').addEventListener('click',redirectToSignIn);
    
});

