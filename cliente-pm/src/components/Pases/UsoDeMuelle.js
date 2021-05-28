import React, { useEffect, useState } from 'react';
import { DatosPase } from './DatosPase';
import { TablaTrabajadores } from './TablaTrabajadores';
import { useHistory } from "react-router-dom";

export const UsoDeMuelle = (props) => {

    //Datos generales del pase
    const URL = '/SolicitudUsoDeMuelle';
    const TITULO = 'Uso de Muelle';
    const history = useHistory();

    // datos para enviar a la API
    const [dataPaseGeneral, setDataPaseGeneral] = useState(() => {
        const variables = JSON.parse(window.localStorage.getItem('datos_pase_general_uso_muelle'));
        if (variables === null) {
            return {
                Area: null,
                RutEmpresa: null,
                NombreEmpresa: null,
                Motivo: null,
                ServicioAdjudicado: null,
                FechaInicio: null,
                FechaTermino: null,
                PersonasExternas: []
            }
        } else {
            return {
                Area: variables.Area,
                RutEmpresa: variables.RutEmpresa,
                NombreEmpresa: variables.NombreEmpresa,
                Motivo: variables.Motivo,
                ServicioAdjudicado: variables.ServicioAdjudicado,
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

    // tomar una lista de personas en memoria o iniciar una nueva lista
    const [personaExterna, setPersonaExterna] = useState(
        window.localStorage.getItem('lista_personas_externas_uso_muelle') === null ?
            [] :
            JSON.parse(window.localStorage.getItem('lista_personas_externas_uso_muelle')));

    // al cargar la pagina verifica si hay persona externas por agregar
    useEffect(() => {
        const newPersona = window.localStorage.getItem('nueva_persona_externa_uso_muelle');

        // se almacenan los datos de agregar persona que se enviaron anteriormente
        if (newPersona !== null) {
            if (!JSON.stringify(personaExterna).includes(newPersona)) {
                setPersonaExterna(persona => [...persona, JSON.parse(newPersona)]);
                window.localStorage.removeItem('nueva_persona_externa_uso_muelle');
            }
        };
    }, []);

    useEffect(() => {
        // esperar actualizacion de personas actuales
        window.localStorage.setItem('lista_personas_externas_uso_muelle', JSON.stringify(personaExterna));
    }, [personaExterna]);

    useEffect(() => {
        // esperar actualizacion de los datos generales en el pase
        window.localStorage.setItem('datos_pase_general_uso_muelle', JSON.stringify(dataPaseGeneral));
    }, [dataPaseGeneral])

    // guardar una persona externa
    const guardarPersonaExterna = jsonPersona => {
        console.log('PERSONA EXTERNA RECIBIDA', jsonPersona);

        // agregar un nuevo usuario a la lista de personas externas
        setPersonaExterna(persona => [...persona, jsonPersona]);

        //window.localStorage.setItem('lista_personas_externas_uso_muelle', JSON.stringify(personaExterna));
        console.log(window.localStorage.getItem('lista_personas_externas_uso_muelle'));
    };

    // actualizar la memoria volatil para persistir los cambbios actuales
    const actualizarDatosEnMemoria = () => {

        // agregar la lista de usuario al pase
        setDataPaseGeneral(anterior => ({
            ...anterior, // mantener lo que existe antes
            ['PersonasExternas']: personaExterna
        }));

        // TODO: hacer la reasignacion de datos almacenados
        window.localStorage.setItem('datos_pase_general_uso_muelle', JSON.stringify({
            'Area': dataPaseGeneral.Area,
            'RutEmpresa': dataPaseGeneral.RutEmpresa,
            'NombreEmpresa': dataPaseGeneral.NombreEmpresa,
            'Motivo': dataPaseGeneral.Motivo,
            'ServicioAdjudicado': dataPaseGeneral.ServicioAdjudicado,
            'FechaInicio': dataPaseGeneral.FechaInicio,
            'FechaTermino': dataPaseGeneral.FechaTermino,
        }));

        window.localStorage.setItem('lista_personas_externas_uso_muelle', JSON.stringify(personaExterna));
        console.log('personas externas: ', window.localStorage.getItem('lista_personas_externas_uso_muelle'));
        console.log('datos pase: ', window.localStorage.getItem('datos_pase_general_uso_muelle'));
    };

    // cancelar el guardado del pase y limpiar memoria
    const cancelarGuardado = () => {
        window.localStorage.removeItem('lista_personas_externas_uso_muelle');
        window.localStorage.removeItem('datos_pase_general_uso_muelle');
        window.localStorage.removeItem('nueva_persona_externa_uso_muelle');
        history.push("/Perfil");
    };


    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">

                    {/** Parte superior de la vista */}
                    <DatosPase _dataPaseGeneral={dataPaseGeneral} tituloPase={TITULO}
                        _ingresoValoresMemoria={ingresarValoresMemoria} />

                    <div class="h-8"></div>

                    {/** Parte inferior tabla de personas */}
                    <TablaTrabajadores datos={personaExterna} url={URL}
                        _guardarPersonaExterna={guardarPersonaExterna}
                        _cancelarGuardado={cancelarGuardado} />

                </div>
            </div>
        </div>
    );
};