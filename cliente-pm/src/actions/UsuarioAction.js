import ClienteHttp from '../serviciosWeb/ClienteHttp';

// registro de usuarios
export const registrarUsuario = usuario => {
    return new Promise((resolve, eject) => {
        ClienteHttp.post('/Usuario/registrar', usuario)
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                console.log('ERROR DEL RESPONSE EN REGISTRO USUARIO: ', eject);
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