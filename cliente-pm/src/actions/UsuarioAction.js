import ClienteHttp from '../serviciosWeb/ClienteHttp';

export const registrarUsuario = usuario => {
    return new Promise((resolve, eject) => {
        ClienteHttp.post('/Usuario/registrar').then(response => {
            resolve(response);
        });
    });
};