    window.addEventListener('DOMContentLoaded', function(event){

    const baseRawUrl = 'http://localhost:5077';
    const baseUrl = `${baseRawUrl}/api`;
    function createUserRoleSimple(email){
        let url = `${baseUrl}/users/UserRoleSimple`;
        var data = {
            Email: email,
            role: 'User',
        }
        fetch(url, {
            headers: { "Content-Type": "application/json; charset=utf-8" },
            method: 'POST',
            body: JSON.stringify(data)
        }).then(response => {
            if(response.status === 200){
                console.log('El rol fue asignado a',email);
                return true;
            }
            else {
                response.json().then((error)=>{
                    console.log(response.status);
                    if(response.status === 400){           
                            console.log('El rol no fue asignado a ',email);
                            return false;
                    }
                });
                
            }
        });
    }
    function tiene_numeros(texto){
        for(i=0; i<texto.length; i++){
            if (indexOf(texto.charAt(i),0)!=-1){
                return 1;
            }
        }
        return 0;
    }
    function postUser(event){


        event.preventDefault();
        let url = `${baseUrl}/users/User`;

        let passEmail=false;
        let passPasword=false;
        let passPaswordConfirm=false;

        if(!Boolean(event.currentTarget.email.value)){  
            document.getElementById('email-alert').classList.add('active');
            passPasword=true;
        }else{
            document.getElementById('email-alert').classList.remove('active');
        }
        
        if(!Boolean(event.currentTarget.password.value)){
            document.getElementById('password-alert').classList.add('active');
            passPasword=true;
        }else{
            document.getElementById('password-alert').classList.remove('active');
        }
        if(!Boolean(event.currentTarget.passwordConfirm.value)){  
            document.getElementById('password-confirm-alert').classList.add('active');
            document.getElementById("password-confirm-alert").innerHTML= "<h4>Introduzca su Contraseña</h4>";
            passPaswordConfirm=true;
        }else{
            document.getElementById('password-confirm-alert').classList.remove('active'); 
        }

        let pass=event.currentTarget.password.value;
        if(pass.length < 5){
            document.getElementById('password-alert').classList.add('active');
            document.getElementById("password-alert").innerHTML= "<h4>Tiene que tener mas de 5 letras</h4>";
            return;
        }else{
            document.getElementById('password-alert').classList.remove('active');
        }
        if(  pass.search(/[0-9]/) < 0 ){
            document.getElementById('password-alert').classList.add('active');
            document.getElementById("password-alert").innerHTML= "<h4>Tiene que tener al menos un numero</h4>";
            return;
        }else{
            document.getElementById('password-alert').classList.remove('active');
        }
        
        if(  pass.search(/[A-Z]/) < 0 ){
            document.getElementById('password-alert').classList.add('active');
            document.getElementById("password-alert").innerHTML= "<h4>Tiene que tener al menos una letra Mayuscula</h4>";
            return;
        }else{
            document.getElementById('password-alert').classList.remove('active');
        }
        console.log(pass);
        console.log(pass.search(/[!#$%&?*"]/) );
        if(  pass.search(/[!#$%&?* "]/) < 0 ){
            document.getElementById('password-alert').classList.add('active');
            document.getElementById("password-alert").innerHTML= "<h4>Tiene que tener al menos un caracter especial (*,!,[ ,])</h4>";
            return;
        }else{
            document.getElementById('password-alert').classList.remove('active');
        }



        if(passPasword || passEmail || passPaswordConfirm){
            return;
        }
        //hasta aqui validadacion de datos no esten vacios
        var data = {
            Email: event.currentTarget.email.value,
            Password: event.currentTarget.password.value,
            ConfirmPassword: event.currentTarget.passwordConfirm.value
        }
        
        if(data.Password!=data.ConfirmPassword){
            document.getElementById('password-confirm-alert').classList.add('active');
            document.getElementById("password-confirm-alert").innerHTML= "<h4>Las contraseñas no son iguales</h4>";
            return;
        }else{
            document.getElementById('password-confirm-alert').classList.remove('active');
        }
        
        fetch(url, {
            headers: { "Content-Type": "application/json; charset=utf-8" },
            method: 'POST',
            body: JSON.stringify(data)
        }).then(response => {
            if(response.status === 200){
                createUserRoleSimple(data.Email);
                console.log('creado el usuario');
                document.getElementById("stateMessage").innerHTML= "Usuario creado exitosamente";
                document.getElementById("stateMessage").style.backgroundColor = "green";
                document.getElementById("stateMessage").style.color = "white";
                setTimeout(function(){window.location.href = 'login.html'},2000);
            }
            else {
                response.json().then((error)=>{
                    console.log(response.status);
                    if(response.status === 400){
                        
                            console.log('entro usuario no fue creado');
                            document.getElementById("stateMessage").innerHTML= "El usuario no fue creado.";
                            document.getElementById("stateMessage").style.backgroundColor = "rgba(255, 0, 0,0.6)";
                            document.getElementById("stateMessage").style.color = "white";   
                    }
                });
                
            }
        });


        
    }

    /*
    "Email": "alvarocori@gmail.com",
    "Password": "a_123ABCxyz",
    "ConfirmPassword": "a_123ABCxyz"
}
    */
    function redirectionLogin(){
        window.location.href = '/users/login.html'
    }
    function redirectionSingUp(){
        window.location.href = '/users/signUp.html'
    }
    document.getElementById('registrarse-btn').addEventListener('click',redirectionSingUp);
    document.getElementById('SignIn-box').addEventListener('submit',postUser);
    document.getElementById('session-login-btn').addEventListener('click',redirectionLogin);

});