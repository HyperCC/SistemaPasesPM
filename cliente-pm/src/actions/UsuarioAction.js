import ClienteHttp from '../serviciosWeb/ClienteHttp';
import axios from 'axios';

const instancia = axios.create();
instancia.CancelToken = axios.CancelToken;
instancia.isCancel = axios.isCancel;

// registro de usuarios
export const registrarUsuario = usuario => {
    return new Promise((resolve, eject) => {
        instancia.post('/Usuario/registrar', usuario)
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                console.log('ERROR DEL RESPONSE EN REGISTRO USUARIO: ', error.toString());
                resolve(error.response);
            });
    });
};

// cambiar la clave de un usuario
export const cambiarPassword = usuario => {
    return new Promise((resolve, eject) => {
        instancia.post('/Usuario/change', usuario)
            .then(response => {
                resolve(response);
            })
            .catch(error => {
                console.log('ERROR DEL RESPONSE EN CAMBIAR PASSWORD: ', error.toString());
                resolve(error.response);
            });
    });
}

// login
export const loginUsuario = (credenciales, dispatch) => {
    return new Promise((resolve, eject) => {
        instancia.post('/Usuario/login', credenciales)
            .then(response => {

                dispatch({
                    type: "INICIAR_SESION",
                    sesion: response.data,
                    autenticado: true
                });

                resolve(response);
            })
            .catch(error => {
                console.log('erro durante el login', error.toString());
                resolve(error.response);
            });
    });
};

// datos del perfil 
export const perfilUsuario = (dispatch) => {
    return new Promise((resolve, eject) => {
        ClienteHttp.get('/Cuenta')
            .then(response => {
                console.log('response', response);

                dispatch({
                    type: "INICIAR_SESION",
                    sesion: response.data,
                    autenticado: true,
                });

                resolve(response);
            })
            .catch(error => {
                console.log(error);
                console.log('ERROR DEL RESPONSE EN PERFIL USUARIO: ', eject);
                resolve(error.response);
            });
    });
};