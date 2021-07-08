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

// generar un nuevo pase contratista
export const registrarPaseContratista = pase => {

    console.log('EL PASE ENTERO RECIBIDO EN ACTION ', pase);

    return new Promise((resolve, eject) => {
        ClienteHttp.post('/Pases/ingresarContratista', pase)
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                console.log('ERROR DEL RESPONSE EN REGISTRO DE PASE CONTRATISTA: ', eject.error);
                resolve(error.response);
            });
    });
};

// cambiar estado pase generico
export const cambiarEstadoPaseGenerico = estado => {
    return new Promise((resolve, eject) => {
        ClienteHttp.post('/Pases/cambiarEstado', estado)
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                console.log('ERROR DEL RESPONSE EN CAMBIO DE ESTADO PARA PASE GENERICO: ', eject.error);
                resolve(error.response);
            });
    });
};