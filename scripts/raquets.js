window.addEventListener('DOMContentLoaded',async function(event){
    let raquets = [];
    const baseRawUrl = 'http://localhost:5077';
    const baseUrl = `${baseRawUrl}/api`;
    const idbrand= sessionStorage.getItem("id_brand");

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
    async function fetGetRaquet(id)
    {
        //debugger;
        let response = await fetch(`${baseUrl}/brands/${idbrand}/raquet/${id}`);
        if(response.status === 200){
            var data = await response.json();
            document.getElementById('h1-model').innerHTML = data.modelo;
            document.getElementById('p-Peso').innerHTML = data.peso;
            document.getElementById('p-Cantidad').innerHTML = data.cantidad;
            document.getElementById('p-Descripcion').innerHTML = data.descripcion;
            document.getElementById('p-Precio').innerHTML = `${data.precio}$` ;

            
            let raquetsClass=Object.values(document.getElementsByClassName('image-contain-popup')) ;

            let staticImage=`<img src="${baseRawUrl}/${data.mainPhoto}" >`;
            raquetsClass.forEach(subclass => {
                document.getElementById(subclass.id).innerHTML=staticImage;
            });
            return data;
        } else { 
            var error = await response.text();
            return "";
            alert(error)
        }
        
    }

    async function fetchGetRaquets(){
        const url = `${baseUrl}/brands/${idbrand}/raquet`;
        let response = await fetch(url);
        try{
            if(response.status == 200){
                let data = await response.json();
                return data;
            } else {
                var errorText = await response.text();
                alert(errorText);
            }
        } catch(error){
            var errorText = await error.text();
            alert(errorText);
        }
        return [];
    }
    async function fetchDeleteRaquet(data)
    {
        let id=data.currentTarget.className;
       //debugger;
        const url =`${baseUrl}/brands/${idbrand}/raquet/${id}`;
        fetch(url, {
            headers: { "Content-Type": "application/json; charset=utf-8" },
            method: 'DELETE',
        }).then((response) => {
            if (response.status === 200) {
                //alert("Raquet Delete successfuly");
                
                document.getElementById('content-eliminar-btn-id').classList.add('low');
                document.getElementById('message-eliminar').innerHTML = 'Se elimino correctamente! <i class="fas fa-check-circle"></i>';
                
                setTimeout(function(){
                    //document.getElementById("preloader").style.display="none";
                    //window.location.href = '../main.html'
                    cerrarPopUPDelete(data);
                    window.location.href = '../raquets.html'
                },2000);
                //document.getElementById('content-eliminar-btn-id').classList.remove('low');
            } else {
                response.text().then((data) => {
                    console.log(data);
                });
            }
        }).catch((response) => {
            console.log(data);
        }); 
        raquets =await fetchGetRaquets();
        

    }
    async function fetchPutRaquet(event)
    {
        let id=event.currentTarget.classList.add;
        event.preventDefault();
        
        let modelo= event.currentTarget.form.modelo.value;
        let peso= parseInt(event.currentTarget.form.peso.value);
        let precio=parseInt(event.currentTarget.form.precio.value);
        let descripcion=event.currentTarget.form.descripcion.value;
        let cantidad=parseInt(event.currentTarget.form.cantidad.value);
        let brandId= parseInt(idbrand);
        let mainImagen = event.currentTarget.form.mainImagen.files[0];

        const formData = new FormData();
        formData.append('modelo', modelo);
        formData.append('peso', parseInt(peso));
        formData.append('precio', parseInt(precio));
        formData.append('descripcion', descripcion);
        formData.append('cantidad', parseInt(cantidad));
        formData.append('brandId', brandId);
        formData.append('mainImagen', mainImagen);

        //var raquetJson = JSON.stringify(raquet)
        //debugger;
        const url =`${baseUrl}/brands/${idbrand}/raquet/Form/${id}`;
        fetch(url, {
            //headers: { "Content-Type": "application/json; charset=utf-8" },
            body: formData,
            method: 'PUT'
        }).then((response) => {
            if (response.status === 200) {
                
                document.getElementById('form-editar').classList.add('low');
                document.getElementById('message-editar').innerHTML = 'Se Actualizo correctamente! <i class="fas fa-check-circle"></i>';
                setTimeout(function(){
                    //document.getElementById("preloader").style.display="none";
                    //window.location.href = '../main.html'
                    
                    cerrarPopUPEdit(event);
                    window.location.href = '../raquets.html'
                },2000);
                
                
                
                //document.getElementById('form-editar').classList.remove('low');
            } else {
                //alert("raquet Update failure");
                document.getElementById('form-editar').classList.add('low');
                document.getElementById('message-editar').innerHTML = 'Se Actualizo correctamente! <i class="fas fa-check-circle"></i>';
                setTimeout(function(){
                    //document.getElementById("preloader").style.display="none";
                    //window.location.href = '../main.html'
                    
                    cerrarPopUPEdit(event);
                    window.location.href = '../raquets.html'
                },2000);
                response.text().then((data) => {
                console.log(data);
                });
            }
        });
        raquets =await fetchGetRaquets();
    }
    function postRaquet(event){

        event.preventDefault();
        let url = `${baseUrl}/brands/${idbrand}/raquet/form`;  
        const formData = new FormData();
        let modelo= event.currentTarget.form.modelo.value;
        let peso= event.currentTarget.form.peso.value;
        let precio=event.currentTarget.form.precio.value;
        let descripcion=event.currentTarget.form.descripcion.value;
        let cantidad=event.currentTarget.form.cantidad.value;
        let brandId= parseInt(idbrand)
        let mainImagen = event.currentTarget.form.mainImagen.files[0];
        
        
        let modeloV= false;
        let pesoV= false;
        let precioV=false;
        let descripcionV=false;
        let cantidadV=false;
        let mainImagenV = false;
        //let secondImagen = event.currentTarget.secondImagen.files[0];


        if(!Boolean(modelo) ){  
            document.getElementById('modelo-alert').classList.add('active');
            
        }else{
            document.getElementById('modelo-alert').classList.remove('active');
            modeloV=true;
            
        }
        if(!Boolean(peso)){ 
            document.getElementById('peso-alert').innerHTML='<p>Ingrese su Peso</p>';
            document.getElementById('peso-alert').classList.add('active');
            
        }else{
            if( isNaN(peso) ){
                document.getElementById('peso-alert').innerHTML='<p> El peso tiene que ser un numero entero </p>';
                document.getElementById('peso-alert').classList.add('active');
                
            }else{
                document.getElementById('peso-alert').classList.remove('active');
                pesoV=true;
            }
        }
        if(!Boolean(precio)){ 
            document.getElementById('precio-alert').innerHTML='<p>Ingrese su precio</p>';
            document.getElementById('precio-alert').classList.add('active');
            
        }else{
            if( isNaN(precio) ){
                document.getElementById('precio-alert').innerHTML='<p> El precio tiene que ser un numero entero </p>';
                document.getElementById('precio-alert').classList.add('active');
                
            }else{
                document.getElementById('precio-alert').classList.remove('active');
                precioV=true;
            }
        }
        if(!Boolean(descripcion) ){  
            document.getElementById('descripcion-alert').innerHTML='<p>Ingrese su descripcion</p>';
            document.getElementById('descripcion-alert').classList.add('active');
            
        }else{
            document.getElementById('descripcion-alert').classList.remove('active');
            descripcionV=true;
        }
        if(!Boolean(cantidad)){ 
            document.getElementById('cantidad-alert').innerHTML='<p>Ingrese su cantidad</p>';
            document.getElementById('cantidad-alert').classList.add('active');
            
        }else{
            if( isNaN(cantidad) ){
                document.getElementById('cantidad-alert').innerHTML='<p> La cantidad tiene que ser un numero entero </p>';
                document.getElementById('cantidad-alert').classList.add('active');
                
            }else{
                document.getElementById('cantidad-alert').classList.remove('active');
                cantidadV=true;
            }
        }
        if(!Boolean(mainImagen) ){  
            document.getElementById('mainImagen-alert').innerHTML='<p>Ingrese su foto</p>';
            document.getElementById('mainImagen-alert').classList.add('active');

        }else{
            document.getElementById('mainImagen-alert').classList.remove('active');
            mainImagenV=true;
        }

        if(modeloV && pesoV && precioV && descripcionV && cantidadV && mainImagenV) {
            console.log('pass ok');
        }else{
            return;
        }
        formData.append('modelo', modelo);
        formData.append('peso', parseInt(peso));
        formData.append('precio', parseInt(precio));
        formData.append('descripcion', descripcion);
        formData.append('cantidad', parseInt(cantidad));
        formData.append('brandId', brandId);
        formData.append('mainImagen', mainImagen);

        fetch(url, {
            method: 'POST',
            headers: { 
            //"Authorization": `Bearer ${sessionStorage.getItem("jwt")}`
            },
            body: formData
        }).then(response => {
            if(response.status === 201){
                document.getElementById('form-raquet').classList.add('low');
                document.getElementById('message-create').innerHTML = 'Se Creo correctamente! <i class="fas fa-check-circle"></i>';
                setTimeout(function(){
                    //document.getElementById("preloader").style.display="none";
                    //window.location.href = '../main.html'
                    cerrarPopUPCreate(event);
                    location.reload();
                },2000);
                
            } else {
                response.text()
                .then((error)=>{
                    alert(error);
                });
            }
        });
    }

    function htmlbox_raquets(list_raquets){
        
        let brandBlock = list_raquets.map( raquet => { 
        return `<div class="raquet" id="raquet-${raquet.id}"> 
                    <div class="Description-contains">
                        <h1>${raquet.modelo}</h1>
                        <!--
                            <p><b>Modelo:</b> ${raquet.modelo}</p>
                            <p><b>Peso:</b> ${raquet.peso}</p>
                            <p><b>Cantidad en la tienda:</b> ${raquet.cantidad}</p>
                            <p><b>Descripcion:</b> ${raquet.descripcion}</p>
                            <p><b>Precio:</b> ${raquet.precio} $</p>
                            -->
                    </div>
                    <div class="image-contain" id="image-raquet-${raquet.id}">
                            <img src="${baseRawUrl}/${raquet.mainPhoto}" >
                    </div>
                    <div class="buttons-contains" id="button-contain-${raquet.id}" >
                        
                    </div>
                </div>`}   );
                
        if(myRole()=="Admin"){
            let addeee=brandBlock;
            addeee.push(`<div class="raquet" id="content-add-btn">
                            <div class="content-btn-add"> 
                                <button id="agregar-btn"> <h1><i class="fas fa-plus"></i></h1></button>
                                <h1>Agregar</h1>
                            </div>   
                        </div>`);
        }        
        
        
        var raquetsContent = brandBlock.join('');
        document.getElementById('raquets-container').innerHTML = raquetsContent;


        let classButtonsRaquets= Object.values(document.getElementsByClassName('buttons-contains')) ;
        if(myRole()=="Admin"){
            
            //let buttonsAdmin=' <button id="Editar-btn">Editar</button>    <button id="Eliminar-btn">Eliminar</button>';
            classButtonsRaquets.forEach(subclass => {
                var idSubclass=subclass.id.split('-')[2];
                document.getElementById(subclass.id).innerHTML=`<button class="btn-detalles" id="Detalle-btn-${idSubclass}">Previsualizar</button> <button class="btn-editar" id="Editar-btn-${idSubclass}">Editar</button>    <button class="btn-eliminar" id="Eliminar-btn-${idSubclass}">Eliminar</button>`;
            });
            
        }else{
            
            if(myRole()=="User"){
               //let buttonsUser='<button id="buyCar-btn">Añadir al carrito</button> <button id="Detalle-btn">Mas Detalles</button>';
                classButtonsRaquets.forEach(subclass => {
                    var idSubclass=subclass.id.split('-')[2];
                    document.getElementById(subclass.id).innerHTML=`<button class="btn-buy" id="buyCar-btn-${idSubclass}">Añadir al carrito</button> <button class="btn-detalles" id="Detalle-btn-${idSubclass}">Mas Detalles</button>`;
                });
            }
            else{
                //let buttonsOtherAndUser='<button id="Detalle-btn">Mas Detalles</button>';
                classButtonsRaquets.forEach(subclass => {
                    var idSubclass=subclass.id.split('-')[2];
                    document.getElementById(subclass.id).innerHTML=`<button class="btn-detalles" id="Detalle-btn-${idSubclass}">Mas Detalles</button>`;
                });
            }
        }


        //Asignacion de Imagenes Estaticas
        let raquetsClass=Object.values(document.getElementsByClassName('image-contain')) ;

        /*let staticImage='<img src="/assets/raquet.jpg" class="avatar">';
        raquetsClass.forEach(subclass => {
            document.getElementById(subclass.id).innerHTML=staticImage;
        });*/
        
    }
    function mostrarPopUpDetails(data){
        let currentIdClass=data.currentTarget.id;
        let idClass=currentIdClass.split('-')[2];
        fetGetRaquet(idClass);
        document.getElementById('overlay').classList.add('active');
        document.getElementById('popup').classList.add('active');
    }
    function cerrarPopUPDetails(event){
        event.preventDefault();
        document.getElementById('overlay').classList.remove('active');
        document.getElementById('popup').classList.remove('active');
    } 
    window.onclick = function(event) {
        var modal = document.getElementById("overlay");
        var modal2 = document.getElementById("overlay-eliminar");
        var modal3 = document.getElementById("overlay-editar");
        var modal4 = document.getElementById("overlay-create");
        if (event.target == modal || event.target==modal2 || event.target==modal3 || event.target==modal4) {
            document.getElementById('overlay').classList.remove('active');
            document.getElementById('popup').classList.remove('active');

            document.getElementById('overlay-eliminar').classList.remove('active');
            document.getElementById('popup-eliminar').classList.remove('active');

            document.getElementById('overlay-editar').classList.remove('active');
            document.getElementById('popup-editar').classList.remove('active');

            document.getElementById('overlay-create').classList.remove('active');
            document.getElementById('popup-create').classList.remove('active');
            location.reload();
        }
    }

    
    function mostrarPopUpDelete(data){
        let currentIdClass=data.currentTarget.id;
        let idClass=currentIdClass.split('-')[2];
        document.getElementById('si-btn').classList.add(idClass);
        document.getElementById('overlay-eliminar').classList.add('active');
        document.getElementById('popup-eliminar').classList.add('active');
    }

    function cerrarPopUPDelete(event){
        event.preventDefault();
        document.getElementById('overlay-eliminar').classList.remove('active');
        document.getElementById('popup-eliminar').classList.remove('active');
    } 

    function mostrarPopUpCreate(data){
        document.getElementById('overlay-create').classList.add('active');
        document.getElementById('popup-create').classList.add('active');
    }

    function cerrarPopUPCreate(event){
        event.preventDefault();
        document.getElementById('overlay-create').classList.remove('active');
        document.getElementById('popup-create').classList.remove('active');
    } 
    async function mostrarPopUpEdit(data){
        let currentIdClass=data.currentTarget.id;
        let idClass=currentIdClass.split('-')[2];
        let raquet= await fetGetRaquet(idClass);
        document.getElementById('btn-editar-aceptar').classList.add=idClass; 
        document.getElementById('input-modelo-editar').value=raquet.modelo;
        document.getElementById('input-peso-editar').value=raquet.peso;
        document.getElementById('input-precio-editar').value=raquet.precio;
        document.getElementById('input-descripcion-editar').value=raquet.descripcion;
        document.getElementById('input-cantidad-editar').value = raquet.cantidad;
        document.getElementById('overlay-editar').classList.add('active');
        document.getElementById('popup-editar').classList.add('active');
    }
    function cerrarPopUPEdit(event){
        event.preventDefault();
        document.getElementById('overlay-editar').classList.remove('active');
        document.getElementById('popup-editar').classList.remove('active');
    } 



    function redirectionLogin(){
        window.location.href = '/users/login.html'
    }
    function redirectionSingUp(){
        window.location.href = '/users/signUp.html'
    }
    function selectionDetails(){
        let listdetailsClass= Object.values(document.getElementsByClassName('btn-detalles')) ;
        listdetailsClass.forEach(subclass => {
            document.getElementById(subclass.id).addEventListener('click', mostrarPopUpDetails);
        });
    }

    function selectionDelete(){
        let listdetailsClass= Object.values(document.getElementsByClassName('btn-eliminar')) ;
        listdetailsClass.forEach(subclass => {
            document.getElementById(subclass.id).addEventListener('click', mostrarPopUpDelete);
        });
    }
    function selectionEdit(){
        let listdetailsClass= Object.values(document.getElementsByClassName('btn-editar')) ;
        listdetailsClass.forEach(subclass => {
            document.getElementById(subclass.id).addEventListener('click', mostrarPopUpEdit);
        });
    }
    raquets = await fetchGetRaquets(); 
    


    htmlbox_raquets(raquets);





    selectionDetails();
    document.getElementById('btn-cerrar-popup').addEventListener('click',cerrarPopUPDetails );

    selectionDelete();
    document.getElementById('btn-cerrar-popup-eliminar').addEventListener('click',cerrarPopUPDelete );
    document.getElementById('no-btn').addEventListener('click',cerrarPopUPDelete );
    document.getElementById('si-btn').addEventListener('click',fetchDeleteRaquet );
    
    
    selectionEdit()
    document.getElementById('btn-cerrar-popup-editar').addEventListener('click',cerrarPopUPEdit);
    document.getElementById('btn-editar-aceptar').addEventListener('click',fetchPutRaquet);
    document.getElementById('btn-editar-cancelar').addEventListener('click',cerrarPopUPEdit);

    //create
    //document.getElementById('create-raquet-form-frm').addEventListener('submit',postRaquet);
    if(myRole()=="Admin"){
        document.getElementById('btn-create-aceptar').addEventListener('click',postRaquet);
        document.getElementById('agregar-btn').addEventListener('click',mostrarPopUpCreate);
        document.getElementById('btn-create-cancelar').addEventListener('click',cerrarPopUPCreate);
        document.getElementById('btn-cerrar-popup-create').addEventListener('click',cerrarPopUPCreate);
    }
    

    document.getElementById('registrarse-btn').addEventListener('click',redirectionSingUp);
    document.getElementById('session-login-btn').addEventListener('click',redirectionLogin);
    //document.getElementById('Detalle-btn').addEventListener('click',DetalleRaquet);


    //document.getElementById('brand-1').addEventListener('click',redirectToRaquets);
    //document.getElementById('brand-2').addEventListener('click',redirectToRaquets);
    //document.getElementById('brand-4').addEventListener('click',redirectToRaquets);
});
