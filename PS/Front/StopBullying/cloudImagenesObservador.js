'use strict';

const boton_foto = document.querySelector('#btnFileVictima');
const imagen = document.querySelector('#victimaFoto');

let widget_cloudinary = cloudinary.createUploadWidget ({
    cloudName: 'dcrhacuid',
    uploadPreset: 'present_sb'

}, (error, resultado) => {
    if (!error && resultado && resultado.event === 'success'){
        console.log ('Imagen subida con exito', resultado.info);
        imagen.src = resultado.info.secure_url;
    }
});

boton_foto.addEventListener('click', () => {
    widget_cloudinary.open();
}, false);