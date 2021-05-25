import React from 'react';
import { NotificacionAdvertencia, NotificacionError, NotificacionExito } from './NotificacionesFlotantes';

// mensajes de operaciones existosas
const listaMensajesExito = [
    {
        'cod': 'exi-re0000',
        'mess': 'El usuario fue registrado correctamente, debe revisar tu bandeja de Email para obtener su clave.'
    },
    {
        'cod': 'exi-le0000',
        'mess': 'Inicio de sesion exitoso. Bienvenido.'
    },
    {
        'cod': 'exi-pe0000',
        'mess': 'Datos obtenidos para el perfil obtenidos correctamente.'
    }
];

// errores por parte del usuario
const listaMensajesAdvertencia = [
    {
        'cod': 'adv-cee000',
        'mess': 'El correo ingresado ya existe en los registros, debe agregar uno distinto.'
    },
    {
        'cod': 'adv-ree000',
        'mess': 'El rut ingresado ya existe en los registros, debe agregar uno distinto.'
    },
    {
        'cod': 'adv-fie000',
        'mess': 'Los siguientes datos ingresados no pudieron ser aceptados: '
    },
    {
        'cod': 'adv-cnee00',
        'mess': 'El Email o la contraseña ingresados no coinciden con los registros. Intente otra vez.'
    },
    {
        'cod': 'adv-pie000',
        'mess': 'El Email o la contraseña ingresados no coinciden con los registros. Intente otra vez.'
    },
    {
        'cod': 'adv-cnc000',
        'mess': 'El captcha debe ser completado para registrarse.'
    }
];

// errores por parte del servidor 
const listaMensajesErrores = [
    {
        'cod': 'err-umnge0',
        'mess': 'La plataforma no ha podido registrar al usuario.'
    },
    {
        'cod': 'err-dbcng0',
        'mess': 'La plataforma no ha podido registrar los datos ingresados por el usuario.'
    },
    {
        'cod': 'err-nhc000',
        'mess': 'No hay conexion con la plataforma actualmente.'
    }
];


// lista con posiles errores recibidos desde la API REST
export function LanzarNoritificaciones(props) {

    console.log('el codigo proporcionado es:', props.codigo);

    if (props.codigo === 'none')
        return <div class="hidden"></div>;

    // asignacion de la notificacion a tipo advertencia
    for (const message in listaMensajesExito)
        if (props.codigo === listaMensajesExito[message].cod.toString())
            return <NotificacionExito texto={listaMensajesExito[message].mess} openError={props.openNotificacion} />;

    // asignacion de la notificacion a tipo advertencia
    for (const message in listaMensajesAdvertencia) {
        if (props.codigo === 'adv-fie000' && listaMensajesAdvertencia[message].cod === props.codigo) {

            let arrayErrores = '';
            for (const invalidInput in props.camposInvalidos)
                arrayErrores += (props.camposInvalidos[invalidInput] + '\n');

            return <NotificacionAdvertencia texto={(listaMensajesAdvertencia[message].mess + ' - ' + arrayErrores)} openError={props.openNotificacion} />;

        } else if (props.codigo === listaMensajesAdvertencia[message].cod.toString())
            return <NotificacionAdvertencia texto={listaMensajesAdvertencia[message].mess} openError={props.openNotificacion} />;
    }

    // asignacion de la notificacion por errores en el servidor
    for (const message in listaMensajesErrores)
        if (props.codigo === listaMensajesErrores[message].cod.toString())
            return <NotificacionError texto={listaMensajesErrores[message].mess} openError={props.openNotificacion} />;

    // en caso de haber un codigo de error no registrado
    return <NotificacionError texto="La plataforma presenta un error no reconocido, intente mas tarde." />
};