import ClienteHttp from '../serviciosWeb/ClienteHttp';

// registro de usuarios
export const registrarUsuario = usuario => {
    return new Promise((resolve, eject) => {
        ClienteHttp.post('/Usuario/registrar', usuario)
            .then(response => {
                resolve(response);
            });
    });
};

// login
export const loginUsuario = credenciales => {
    return new Promise((resolve, eject) => {
        ClienteHttp.post('/Usuario/login', credenciales)
            .then(response => {
                resolve(response);
            });
    });
};