import ClienteHttp from '../serviciosWeb/ClienteHttp';

// generar un nuevo pase generico
export const registrarPaseGenerico = pase => {
    return new Promise((resolve, eject) => {
        ClienteHttp.post('/Pases/ingresar', pase)
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                console.log('ERROR DEL RESPONSE EN REGISTRO DE PASE GENERICO: ', eject.error);
                resolve(error.response);
            });
    });
};