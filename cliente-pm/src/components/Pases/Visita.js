import React, { useEffect, useState } from 'react';
import { DatosPase } from './DatosPase';
import { TablaTrabajadores } from './TablaTrabajadores';
import { useHistory } from "react-router-dom";
import { registrarPaseGenerico } from '../../actions/PaseAction';
import { LanzarNoritificaciones } from '../avisos/LanzarNotificaciones';



export const Visita = (props) => {

    //Datos generales del pase
    const URL = '/SolicitudVisita';
    const TITULO = 'Visita';
    const history = useHistory();

    const [actualizar, setActualizar] = useState(0);

    // codigo actual de la notificacion a mostrar
    const [currentNotification, setCurrentNotification] = useState('none');
    // posibles campos invalidos enviados por el usuario
    const [currentCamposInvalidos, setCurrentCamposInvalidos] = useState([]);

    // datos para enviar a la API
    const [dataPaseGeneral, setDataPaseGeneral] = useState(() => {
        const variables = JSON.parse(window.localStorage.getItem('datos_pase_general_visita'));
        if (variables === null) {
            return {
                Area: '',
                Motivo: '',
                RutEmpresa: '',
                NombreEmpresa: '',
                Tipo: 'VISITA',
                ServicioAdjudicado: '',
                Completitud: true, //bool
                FechaInicio: '',
                FechaTermino: '',
                PersonasExternas: []
            }
        } else {
            return {
                Area: variables.Area,
                Motivo: variables.Motivo,
                RutEmpresa: variables.RutEmpresa,
                NombreEmpresa: variables.NombreEmpresa,
                Tipo: variables.Tipo,
                ServicioAdjudicado: variables.ServicioAdjudicado,
                Completitud: false, //bool
                FechaInicio: variables.FechaInicio,
                FechaTermino: variables.FechaTermino,
                PersonasExternas: []
            }
        }
    });


    // asignar nuevos valores al state del registro
    const ingresarValoresMemoria = (name, date) => {
        setDataPaseGeneral(anterior => ({
            ...anterior, // mantener lo que existe antes
            [name]: date // solo cambiar el input mapeado
        }));
    };

    const actualizarPagina = () => {
        setActualizar(actualizar + 1);
    }


    // tomar una lista de personas en memoria o iniciar una nueva lista
    const [personaExterna, setPersonaExterna] = useState(
        window.localStorage.getItem('lista_personas_externas_visita') === null ?
            [] :
            JSON.parse(window.localStorage.getItem('lista_personas_externas_visita')));

    // al cargar la pagina verifica si hay persona externas por agregar
    useEffect(() => {
        const newPersona = window.localStorage.getItem('nueva_persona_externa_visita');

        // se almacenan los datos de agregar persona que se enviaron anteriormente
        if (newPersona !== null) {
            if (!JSON.stringify(personaExterna).includes(newPersona)) {
                setPersonaExterna(persona => [...persona, JSON.parse(newPersona)]);
                window.localStorage.removeItem('nueva_persona_externa_visita');
            }
        };
    }, [actualizar]);

    useEffect(() => {
        // esperar actualizacion de personas actuales
        window.localStorage.setItem('lista_personas_externas_visita', JSON.stringify(personaExterna));

        // agregar la lista de usuario al pase para enviar
        setDataPaseGeneral(anterior => ({
            ...anterior, // mantener lo que existe antes
            ['PersonasExternas']: personaExterna
        }));
    }, [personaExterna]);

    useEffect(() => {
        // esperar actualizacion de los datos generales en el pase
        window.localStorage.setItem('datos_pase_general_visita', JSON.stringify(dataPaseGeneral));
    }, [dataPaseGeneral])

    // actualizar la memoria volatil para persistir los cambbios actuales
    const enviarFormulario = inforFormulario => {

        inforFormulario.preventDefault();
        console.log('todos los datos para enviar: ', dataPaseGeneral);
        setCurrentNotification('inf-cgp000');

        // enviar los datos a la API
        registrarPaseGenerico(dataPaseGeneral).then(response => {

            // si existe alguna respuesta de la API
            if (typeof response !== 'undefined') {
                console.log('si existe conexion con la api. ', response);

                // si se reciben errores
                if (typeof response.data.errores != 'undefined') {
                    console.log('existen errores en la respuesta: ', response.data.errores.mensaje);

                    console.log('el tipo de error: ', response.data.errores.tipoError);
                    setCurrentNotification(response.data.errores.tipoError);
                    console.log('TIPO ACTUAL DE NOTIFICACION ', currentNotification);

                    if (typeof response.data.errores.listaErrores !== 'undefined')
                        setCurrentCamposInvalidos(response.data.errores.listaErrores);

                    // si toda la operacion salio ok
                } else {
                    console.log('toda la operacion fue correcta');
                    //window.localStorage.setItem('mensaje_success', 'exi-ptre00');
                    //window.localStorage.setItem('mensaje_success_showed', false);
                    setCurrentNotification('exi-pvre00');

                    const sleep = (milliseconds) =>
                        new Promise(resolve => setTimeout(resolve, milliseconds));
                    sleep(1000).then(() => {
                        cancelarGuardado();
                    });
                }

                // si no hay conexion con la API
            } else {
                setCurrentNotification('err-nhc000');
            }
        });
    };

    // cancelar el guardado del pase y limpiar memoria
    const cancelarGuardado = () => {
        window.localStorage.removeItem('lista_personas_externas_visita');
        window.localStorage.removeItem('datos_pase_general_visita');
        window.localStorage.removeItem('nueva_persona_externa_visita');
        history.push("/Perfil");
    };


    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="max-w-6xl mx-auto">
                <div class="py-8 sm:px-8 px-2">

                    <LanzarNoritificaciones codigo={currentNotification} camposInvalidos={currentCamposInvalidos} />

                    {/** Parte superior de la vista */}
                    <DatosPase _dataPaseGeneral={dataPaseGeneral} tituloPase={TITULO}
                        _ingresoValoresMemoria={ingresarValoresMemoria} />

                    <div class="h-8"></div>

                    {/** Parte inferior tabla de personas */}
                    <TablaTrabajadores datos={personaExterna} url={URL}
                        _enviarFormulario={enviarFormulario}
                        _cancelarGuardado={cancelarGuardado}
                        _actualizarPagina={actualizarPagina} />


                </div>
            </div>
        </div>
    );
};
