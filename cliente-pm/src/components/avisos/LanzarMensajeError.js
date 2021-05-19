import { comment } from 'postcss';
import React from 'react';

// lista con posiles errores recibidos desde la API REST
export const MensajeError = codigo => {

    let ComponentNotification;

    // asignacion de la notificacion a mostrar
    switch (codigo) {
        case '':
            ComponentNotification = <div>

            </div>
            break;

        default:
            ComponentNotification = <div>

            </div>
            break;
    }

    return comment;
};