import React, { useState } from 'react';
import { InformacionPase } from './InformacionPases'
import { ListaPersonas } from './ListaPesonas'
import { useHistory, useLocation, withRouter } from "react-router-dom";
import { LanzarNoritificaciones } from '../../avisos/LanzarNotificaciones';
import { cambiarEstadoPaseGenerico } from '../../../actions/PaseAction';

export const RevisionPase = props => {

    // codigo actual de la notificacion a mostrar
    const [currentNotification, setCurrentNotification] = useState('none');
    // posibles campos invalidos enviados por el usuario
    const [currentCamposInvalidos, setCurrentCamposInvalidos] = useState([]);

    let data = useLocation();
    console.log("aqui estan los datos")
    console.log(data.state)
    let history = useHistory();
    let TITULO = data.state.tipo;

    const pase = {
        fechaInicio: data.state.fechaInicio,
        fechaInicio: data.state.fechaInicio,
        fechaTermino: data.state.fechaTermino,
        motivo: data.state.motivo,
        area: data.state.area,
        tipo: data.state.tipo,
        estado: data.state.estado,
        paseId: data.state.paseId
    }

    const CambiarEstadoPase = data => {
        setCurrentNotification('inf-cep000');

        cambiarEstadoPaseGenerico(data).then(response => {
            // si existe alguna respuesta de la API
            if (typeof response !== 'undefined') {
                console.log('si existe conexion con la api. ', response);

                // si se reciben errores
                if (typeof response.data.errores != 'undefined') {
                    console.log('existen errores en la respuesta: ', response.data.errores.mensaje);
                    setCurrentNotification(response.data.errores.tipoError);

                    if (typeof response.data.errores.listaErrores !== 'undefined')
                        setCurrentCamposInvalidos(response.data.errores.listaErrores);

                    // si toda la operacion salio ok
                } else {
                    console.log('toda la operacion fue correcta');
                    setCurrentNotification('exi-cee000');

                    const sleep = (milliseconds) =>
                        new Promise(resolve => setTimeout(resolve, milliseconds));
                    sleep(1000).then(() => {
                        history.push("/Perfil");
                    });
                }

                // si no hay conexion con la API
            } else {
                setCurrentNotification('err-nhc000');
            }
        });
    };

    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">

                    <LanzarNoritificaciones codigo={currentNotification} camposInvalidos={currentCamposInvalidos} />

                    {/** Parte superior de la vista */}
                    <InformacionPase _dataPaseGeneral={pase} tituloPase={TITULO} />

                    <div class="h-8"></div>

                    {/** Parte inferior tabla de personas */}
                    <ListaPersonas datos={data.state.personas} url={URL} tipoPase={pase.tipo}
                        opcionCambiarEstado={CambiarEstadoPase} identificador={pase.paseId} />

                </div>
            </div>
        </div>
    )

}
