import React from 'react';
import { NotificacionAdvertencia, NotificacionError, NotificacionExito, NotificacionInformacion } from './NotificacionesFlotantes';

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
    },
    {
        'cod': 'exi-ptre00',
        'mess': 'Pase de tipo Tripulante generado existosamente.'
    },
    {
        'cod': 'exi-pumre0',
        'mess': 'Pase de tipo Uso de Muelle generado existosamente.'
    },
    {
        'cod': 'exi-ppre00',
        'mess': 'Pase de tipo Proveedor de servicio generado existosamente.'
    },
    {
        'cod': 'exi-pcre00',
        'mess': 'Pase de tipo Contratista generado existosamente.'
    },
    {
        'cod': 'exi-pvre00',
        'mess': 'Pase de tipo Visita generado existosamente.'
    },
    {
        'cod': 'exi-cce000',
        'mess': 'Cambio de clave existoso.'
    },
    {
        'cod': 'exi-cre000',
        'mess': 'Cambio de rol exitoso'
    }
];

const listaMensajesInfo = [
    {
        'cod': 'inf-cvc000', // cargando validacion de credenciales
        'mess': 'Verificando las credenciales..'
    },
    {
        'cod': 'inf-cgu000', // cargando validacion de credenciales
        'mess': 'Guardando datos de usuario..'
    },
    {
        'cod': 'inf-cdp000', // cargando datos del perfil
        'mess': 'Cargando datos del perfil..'
    },
    {
        'cod': 'inf-cgp0000', // cargando guardado de pase
        'mess': 'Guardando pase..'
    },
    {
        'cod': 'inf-cgnc00', // cargando guardado de nueva clave
        'mess': 'Guardando nueva contraseña..'
    },
    {
        'cod': 'inf-ccr000', // cargando guardado del cambio de rol
        'mess': 'Cambiando el rol..'
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
    },
    {
        'cod': 'adv-pnmce0',
        'mess': 'La confirmacion de la nueva contraseña no coincide con la nueva contraseña.'
    },
    {
        'cod': 'adv-pde000',
        'mess': 'La nueva contraseña debe ser distinta a la actual.'
    },
    {
        'cod': 'adv-pse000',
        'mess': 'La nueva contraseña presenta los siguientes errores: '
    },
    {
        'cod': 'adv-rnee00',
        'mess': 'El rol para actualizar en el usuario no existe.'
    },
    {
        'cod': 'adv-rie000',
        'mess': 'El rol para actualizar es identico al actual.'
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
        'cod': 'err-pnrkv0', // plataforma no recibio un token valido
        'mess': 'La plataforma no ha podido cargar los datos del usuario.'
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

    for (const message in listaMensajesInfo)
        if (props.codigo === listaMensajesInfo[message].cod.toString())
            return <NotificacionInformacion texto={listaMensajesInfo[message].mess} openError={props.openNotificacion} />

    // asignacion de la notificacion a tipo advertencia
    for (const message in listaMensajesAdvertencia) {
        if ((props.codigo === 'adv-fie000' && listaMensajesAdvertencia[message].cod === props.codigo)
            || (props.codigo === 'adv-pse000' && listaMensajesAdvertencia[message].cod === props.codigo)) {

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