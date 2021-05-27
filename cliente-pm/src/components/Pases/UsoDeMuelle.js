import React, { useState } from 'react';
import AgregarPersona from './AgregarPersona';
import { DatosPase } from './DatosPase';
import { TablaTrabajadores } from './TablaTrabajadores';

export const UsoDeMuelle = (props) => {

    //Datos generales del pase
    const URL = '/SolicitudUsoDeMuelle';
    const TITULO = 'Uso de Muelle';

    // datos para enviar a la API
    const [dataPaseGeneral, setDataPaseGeneral] = useState({
        Area: null,
        RutEmpresa: null,
        NombreEmpresa: null,
        Motivo: null,
        ServicioAdjudicado: null,
        FechaInicio: null,
        FechaTermino: null,
        PersonasExternas: []
    });

    // asignar nuevos valores al state del registro
    const ingresarValoresMemoria = (name, date) => {
        setDataPaseGeneral(anterior => ({
            ...anterior, // mantener lo que existe antes
            [name]: date // solo cambiar el input mapeado
        }));
    };

    // tomar una lista de personas en memoria o iniciar una nueva lista
    const [personaExterna, setPersonaExterna] = useState(
        window.localStorage.getItem('lista_personas_externas') === null ?
            [] :
            JSON.parse(window.localStorage.getItem('lista_personas_externas')));

    // guardar una persona externa
    const guardarPersonaExterna = jsonPersona => {
        console.log('PERSONA EXTERNA RECIBIDA', jsonPersona);

        // agregar un nuevo usuario a la lista de personas externas
        setPersonaExterna(persona => [...persona, jsonPersona]);

        //window.localStorage.setItem('lista_personas_externas', JSON.stringify(personaExterna));
        console.log(window.localStorage.getItem('lista_personas_externas'));
    };

    // actualizar la memoria volatil para persistir los cambbios actuales
    const actualizarDatosEnMemoria = () => {

        // agregar la lista de usuario al pase
        setDataPaseGeneral(anterior => ({
            ...anterior, // mantener lo que existe antes
            ['PersonasExternas']: personaExterna
        }));

        // TODO: hacer la reasignacion de datos almacenados
        window.localStorage.setItem('datos_pase_general', JSON.stringify({
            'Area': dataPaseGeneral.Area,
            'RutEmpresa': dataPaseGeneral.RutEmpresa,
            'NombreEmpresa': dataPaseGeneral.NombreEmpresa,
            'Motivo': dataPaseGeneral.Motivo,
            'ServicioAdjudicado': dataPaseGeneral.ServicioAdjudicado,
            'FechaInicio': dataPaseGeneral.FechaInicio,
            'FechaTermino': dataPaseGeneral.FechaTermino,
        }));

        window.localStorage.setItem('lista_personas_externas', JSON.stringify(personaExterna));
        console.log('personas externas: ', window.localStorage.getItem('lista_personas_externas'));
        console.log('datos pase: ', window.localStorage.getItem('datos_pase_general'));
    };

    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">

                    <div>
                        <button onClick={actualizarDatosEnMemoria} class="bg-blue-700 text-white">salvar datos actuales</button>
                    </div>
                    <AgregarPersona _guardarPersonaExterna={guardarPersonaExterna} />

                    {/** Parte superior de la vista */}
                    <DatosPase _dataPaseGeneral={dataPaseGeneral} tituloPase={TITULO}
                        _ingresoValoresMemoria={ingresarValoresMemoria} />

                    <div class="h-8"></div>

                    {/** Parte inferior tabla de personas */}
                    <TablaTrabajadores datos={personaExterna} url={URL}
                        _guardarPersonaExterna={guardarPersonaExterna} />
                </div>
            </div>
        </div>
    );
};