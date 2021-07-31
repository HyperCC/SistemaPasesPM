import sesionUsuarioReducer from './SesionUsuarioReducer';

export const mainReducer = ({ sesionUsuario }, action) => {
    return {
        sesionUsuario: sesionUsuarioReducer(sesionUsuario, action)
    };
};