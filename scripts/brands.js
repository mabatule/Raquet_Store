window.addEventListener('DOMContentLoaded',async function(event){
    let brands = [];
    const baseRawUrl = 'http://localhost:5077';
    const baseUrl = `${baseRawUrl}/api`;



    async function fetchGetBrands(){
        const url = `${baseUrl}/brands`;
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

    function htmlbox_brands(list_brands){
        let brandBlock = list_brands.map( brand => { 
        return `<div class="brand" id="brand-${brand.id}"> 
                    <div class="image-contain" id="image-brand-${brand.id}">
                        
                    </div>
                    <div class="Description-contains">

                    <!--  esta comentado ahora
                    <p><b>Nombre:</b> ${brand.nombre}</p>
                    <p><b>Pais:</b> ${brand.pais}</p>
                    <p><b>Descripcion:</b> ${brand.descripcion}</p>
                    -->
                        
                    </div>

                </div>`}   );
        
        var brandsContent = brandBlock.join('');
        
        document.getElementById('brands-container').innerHTML = brandsContent;

            
        document.getElementById('image-brand-1').innerHTML='<img src="/assets/gearbox.jpg" class="avatar">';
        document.getElementById('image-brand-2').innerHTML='<img src="/assets/eforce.jpg" class="avatar">';
        document.getElementById('image-brand-4').innerHTML='<img src="/assets/prokennex.jpg" class="avatar">';
        
        
    }
    function redirectToRaquets(data){
        let id=data.currentTarget.id.split('-')[1];
        sessionStorage.setItem("id_brand", id);
        window.location.href = 'raquets.html';
    }
    function redirecOnclick(brands){
        brands.forEach(element => {
            document.getElementById('brand-'+element.id).addEventListener('click',redirectToRaquets);
        });
    }
    function redirectionLogin(){
        window.location.href = '/users/login.html'
    }
    function redirectionSingUp(){
        window.location.href = '/users/signUp.html'
    }
    brands = await fetchGetBrands();
    htmlbox_brands(brands);
    redirecOnclick(brands);
    document.getElementById('registrarse-btn').addEventListener('click',redirectionSingUp);
    document.getElementById('session-login-btn').addEventListener('click',redirectionLogin);

    //document.getElementById('brand-1').addEventListener('click',redirectToRaquets);
    //document.getElementById('brand-2').addEventListener('click',redirectToRaquets);
    //document.getElementById('brand-4').addEventListener('click',redirectToRaquets);
});
