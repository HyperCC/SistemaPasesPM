import ClienteHttp from '../serviciosWeb/ClienteHttp';

// generar un nuevo pase generico
export const cambiarRol = datos => {
    return new Promise((resolve, eject) => {
        ClienteHttp.post('/Rol/cambiar', datos)
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                console.log('ERROR DEL RESPONSE DEL CAMBIO DE ROL: ', eject.error);
                resolve(error.response);
            });
    });
};