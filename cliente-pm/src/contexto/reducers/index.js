import sesionUsuarioReducer from './SesionUsuarioReducer';

export const mainReducer = ({sesionUsuario, openSnackbar}, action) => {
    return {
        sesionUsuario : sesionUsuarioReducer(sesionUsuario, action)
    }
}