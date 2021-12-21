window.addEventListener('DOMContentLoaded',async function(event){
    let brands = [];
    const baseRawUrl = 'http://localhost:5077';
    const baseUrl = `${baseRawUrl}/api`;
    const idbrand= sessionStorage.getItem("id_brand");



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

    function htmlbox_brands(list_raquets){
        let brandBlock = list_raquets.map( raquet => { 
        return `<div class="raquet" id="raquet-${raquet.id}"> 
                    <div class="image-contain" id="image-raquet-${raquet.id}">
                        
                    </div>
                    <div class="Description-contains">
                        <p><b>Modelo:</b> ${raquet.modelo}</p>
                        <p><b>Peso:</b> ${raquet.peso}</p>
                        <p><b>Cantidad en la tienda:</b> ${raquet.cantidad}</p>
                        <p><b>Descripcion:</b> ${raquet.descripcion}</p>
                        <p><b>Precio:</b> ${raquet.precio} $</p>
                    </div>

                </div>`}   );
        
        var brandsContent = brandBlock.join('');
        
        document.getElementById('raquets-container').innerHTML = brandsContent;

        if(idbrand==1){
            document.getElementById('image-raquet-1').innerHTML='<img src="/assets/raquet.jpg" class="avatar">';       
            document.getElementById('image-raquet-2').innerHTML='<img src="/assets/raquet.jpg" class="avatar">';
            document.getElementById('image-raquet-3').innerHTML='<img src="/assets/raquet.jpg" class="avatar">';
        }
        if(idbrand==2){
            document.getElementById('image-raquet-4').innerHTML='<img src="/assets/raquet.jpg" class="avatar">';       
            document.getElementById('image-raquet-5').innerHTML='<img src="/assets/raquet.jpg" class="avatar">';
            document.getElementById('image-raquet-6').innerHTML='<img src="/assets/raquet.jpg" class="avatar">';
            document.getElementById('image-raquet-7').innerHTML='<img src="/assets/raquet.jpg" class="avatar">';       
            document.getElementById('image-raquet-8').innerHTML='<img src="/assets/raquet.jpg" class="avatar">';
        }
        if(idbrand==4){
            document.getElementById('image-raquet-9').innerHTML='<img src="/assets/raquet.jpg" class="avatar">';
            document.getElementById('image-raquet-10').innerHTML='<img src="/assets/raquet.jpg" class="avatar">';
            document.getElementById('image-raquet-11').innerHTML='<img src="/assets/raquet.jpg" class="avatar">';
            
        }
    }
    function redirectionLogin(){
        window.location.href = '/users/login.html'
    }
    function redirectionSingUp(){
        window.location.href = '/users/signUp.html'
    }
    brands = await fetchGetRaquets();
    
    htmlbox_brands(brands);
    document.getElementById('registrarse-btn').addEventListener('click',redirectionSingUp);
    document.getElementById('session-login-btn').addEventListener('click',redirectionLogin);
    //document.getElementById('brand-1').addEventListener('click',redirectToRaquets);
    //document.getElementById('brand-2').addEventListener('click',redirectToRaquets);
    //document.getElementById('brand-4').addEventListener('click',redirectToRaquets);
});
