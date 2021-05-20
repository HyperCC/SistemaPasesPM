import ClienteHttp from '../serviciosWeb/ClienteHttp';

// registro de usuarios
export const registrarUsuario = usuario => {
    console.log('usuairo:', usuario);

    return new Promise((resolve, eject) => {
        console.log('ANTES DE DEVOLVER LA PROMESA');
        ClienteHttp.post('/Usuario/registrar', usuario)
            .then(response => {
                resolve(response);
                console.log('response:', response);
                console.log('resolve:', resolve);
                console.log('eject:', eject);
            })
            .catch(error => {
                console.log('ERROR DEL RESPONSE CACHADO: ', eject);
                resolve(error.response);
            });
    });
};

// login
export const loginUsuario = credenciales => {
    return new Promise((resolve, eject) => {
        ClienteHttp.post('/Usuario/login', credenciales)
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                console.log(error);
                resolve(error.response);
            });
    });
};