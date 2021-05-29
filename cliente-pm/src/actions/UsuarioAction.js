import ClienteHttp from '../serviciosWeb/ClienteHttp';

// registro de usuarios
export const registrarUsuario = usuario => {
    return new Promise((resolve, eject) => {
        ClienteHttp.post('/Usuario/registrar', usuario)
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                console.log('ERROR DEL RESPONSE EN REGISTRO USUARIO: ', error.toString());
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
                console.log('erro durante el login', error.toString());
                resolve(error.response);
            });
    });
};

// datos del perfil 
export const perfilUsuario = () => {
    return new Promise((resolve, eject) => {
        ClienteHttp.get('/Cuenta')
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                console.log(error);
                console.log('ERROR DEL RESPONSE EN PERFIL USUARIO: ', eject);
                resolve(error.response);
            });
    });
};

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
}