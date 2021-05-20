import React from 'react';
import { NotificacionAdvertencia, NotificacionError, NotificacionExito } from './NotificacionesFlotantes';

// mensajes de operaciones existosas
const listaMensajesExito = [
    {
        'cod': 'exi-uac000',
        'mess': 'El usuario fue registrado correctamente, debe revisar tu bandeja de Email para obtener su clave.'
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
        'mess': 'Los siguientes datos ingresados no pudieron ser aceptados'
    }
];

// errores por parte del servidor 
const listaMensajesErrores = [
    {
        'cod': 'err-umnge0',
        'mess': 'La plataforma no ha podido registrar al usuario'
    },
    {
        'cod': 'err-dbcng0',
        'mess': 'La plataforma no ha podid registrar los datos ingresados por el usuario'
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
            return <NotificacionExito texto={listaMensajesExito[message].mess} />;

    // asignacion de la notificacion a tipo advertencia
    for (const message in listaMensajesAdvertencia)
        if (props.codigo === listaMensajesAdvertencia[message].cod.toString())
            return <NotificacionAdvertencia texto={listaMensajesAdvertencia[message].mess} />;

    // asignacion de la notificacion por errores en el servidor
    for (const message in listaMensajesErrores)
        if (props.codigo === listaMensajesAdvertencia[message].cod.toString())
            return <NotificacionError texto={listaMensajesAdvertencia[message].mess} />;

    // en caso de haber un codigo de error no registrado
    return <NotificacionError texto="La plataforma presenta un error no reconocido, intente mas tarde." />
};