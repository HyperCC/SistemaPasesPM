import React, { useEffect, useState } from 'react';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useLocation } from 'react-router-dom';
import { useHistory } from "react-router-dom";
import { TablaTrabajadores } from './TablaTrabajadores';
import { registrarPaseContratista, registrarPaseGenerico } from '../../actions/PaseAction';
import { LanzarNoritificaciones } from '../avisos/LanzarNotificaciones';
import Popup from 'reactjs-popup';
import { DocumentosEmpresa } from './DocumentosEmpresa';

export const Contratista = (props) => {

    const [startDate, setStartDate] = useState(new Date());
    const [finishtDate, setFinishDate] = useState(new Date());
    const url = "/SolicitudContratista";
    const location = useLocation();
    const history = useHistory();

    const [listaPersona, setListaPersona] = useState([]);
    const [countAux, setCountAux] = useState(0)

    const [datosPaseGeneral, setDatosPaseGeneral] = useState({
        Area: '',
        Motivo: '',
        RutEmpresa: '',
        NombreEmpresa: '',
        Tipo: 'CONTRATISTA',
        ServicioAdjudicado: '',
        Completitud: false,
        Observaciones: '',
        FechaInicio: '01/01/2022',
        FechaTermino: '01/01/2022',

        // listas de usuarios y documentos
        PeronasContratista: [],
        SeccionDocumentosEmpresa: [],

        // personas especiales
        AsesorDePrevencion: {
            Nombres: '',
            Apellidos: '',
            Rut: '',
            RegistroSns: '',
        },
    });

    // codigo actual de la notificacion a mostrar
    const [currentNotification, setCurrentNotification] = useState('none');
    // posibles campos invalidos enviados por el usuario
    const [currentCamposInvalidos, setCurrentCamposInvalidos] = useState([]);

    const ingresarValoresMemoria = valorInput => {
        // obtener el valor
        const { name, value } = valorInput.target;

        // actualizar el valor de algun otro valor
        // asignar el valor
        setDatosPaseGeneral(anterior => ({
            ...anterior, // mantener lo que existe antes
            [name]: value // solo cambiar el input mapeado
        }));

    };

    const guardarDocumentosEmpresa = (datosPrev, filesEmpresa) => {

        // Datos empresa
        setDatosPaseGeneral(anterior => ({
            ...anterior, // mantener lo que existe antes
            ["SeccionDocumentosEmpresa"]: filesEmpresa // solo cambiar el input mapeado
        }));

        // Datos Prev
        setDatosPaseGeneral(anterior => ({
            ...anterior, // mantener lo que existe antes
            ["AsesorDePrevencion"]: datosPrev // solo cambiar el input mapeado
        }));

        console.log("aqui")

    }

    const guardarPersona = (persona) => {

        if (countAux === 0) {
            setCountAux(countAux + 1);
        } else if (countAux === 1) {
            setListaPersona([persona]);
            setCountAux(countAux + 1);
        }
        else {
            setListaPersona([...listaPersona, persona]);
        }
        // Se añade la persona a la lista

    }

    useEffect(() => {

        // Se actualiza la lista en los datos generales
        setDatosPaseGeneral(anterior => ({
            ...anterior, // mantener lo que existe antes
            ["PeronasContratista"]: listaPersona, // solo cambiar el input mapeado
        }));

    }, [listaPersona]);

    // envio de los datos del formulario a la API 
    const enviarFormulario = inforFormulario => {
        inforFormulario.preventDefault();
        console.log('todos los datos para enviar: ', datosPaseGeneral);
        setCurrentNotification('inf-cgp000');

        // enviar los datos a la API
        registrarPaseContratista(datosPaseGeneral).then(response => {

            // si existe alguna respuesta de la API
            if (typeof response !== 'undefined') {
                console.log('si existe conexion con la api. ', response);

                // si se reciben errores
                if (typeof response.data.errores != 'undefined') {
                    console.log('existen errores en la respuesta: ', response.data.errores);

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
                    // limpiar las memorias locales
                    cancelarGuardado();
                }

                // si no hay conexion con la API
            } else {
                setCurrentNotification('err-nhc000');
            }
        });
    };

    const cancelarGuardado = () => {
        //window.localStorage.removeItem('lista_personas_externas_visita');
        //window.localStorage.removeItem('datos_pase_general_visita');
        //window.localStorage.removeItem('nueva_persona_externa_visita');
        history.push("/Perfil");
    };

    console.log(location.documentosPersonaContratisa);

    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">

                    <LanzarNoritificaciones codigo={currentNotification} camposInvalidos={currentCamposInvalidos} />

                    {/** Parte superior */}
                    <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
                        <p class="text-2xl leading-tight mx-8 text-center md:text-left md:ml-16">
                            Información general - Pase Contratista
                        </p>

                        <div class="grid grid-cols-7 gap-6 md:grid-cols-6 mt-6 mx-8 mb-2 md:mb-0">
                            <div class="col-span-1 col-start-1 row-start-1"> <p>Área</p> </div>
                            <div class="col-span-2 flex col-start-2 row-start-1 md:col-span-2">
                                <select name="Area" value={datosPaseGeneral.Area} onChange={ingresarValoresMemoria}
                                    class="bg-gray-100 p-2 rounded-full outline-none w-full border border-gray-300">
                                    <option>Seleccionar Área</option>
                                    <option value="Contabilidad">CONTABILIDAD</option>
                                    <option value="Ing. y Mantencion">ING. Y MANTENCIÓN</option>
                                    <option value="Operaciones">OPERACIONES</option>
                                    <option value="Finanzas">FINANZAS</option>
                                    <option value="Informatica">INFORMÁTICA</option>
                                    <option value="Comercial">COMERCIAL</option>
                                    <option value="Administracion">ADMINISTRACIÓN</option>
                                    <option value="Hseq">HSEQ</option>
                                    <option value="Personas">PERSONAS</option>
                                    <option value="Proyectos">PROYECTOS</option>
                                    <option value="Medio ambiente">MEDIO AMBIENTE</option>
                                    <option value="Mecanica pm">MECANICA PM</option>
                                    <option value="Electro-control pm">ELECTRO-CONTROL PM</option>
                                </select>
                            </div>

                            <div class="col-span-1 row-start-2"> <p>Rut Empresa</p> </div>
                            <div class="col-span-1 md:col-span-1 row-start-2">
                                <input type="text" value={datosPaseGeneral.RutEmpresa} onChange={ingresarValoresMemoria} name="RutEmpresa" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-3"> <p>Nombre Empresa</p> </div>
                            <div class="col-span-1 row-start-3 md:col-span-1">
                                <input type="text" value={datosPaseGeneral.NombreEmpresa} onChange={ingresarValoresMemoria} name="NombreEmpresa" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                            </div>
                            <div class="col-span-1 row-start-4"> <p>Servicio Adjudicado</p> </div>
                            <div class="col-span-1 row-start-4 md:col-span-1">
                                <input type="text" value={datosPaseGeneral.ServicioAdjudicado} onChange={ingresarValoresMemoria} name="ServicioAdjudicado" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-span-2 col-start-4 row-start-1"><p>Motivo</p></div>
                            <div class="col-span-3 row-span-2 col-start-5 row-start-1"><textarea type="range" value={datosPaseGeneral.Motivo} onChange={ingresarValoresMemoria} name="Motivo" placeholder="range...." class="border w-full app border-gray-300 p-2 my-2 rounded-md focus:outline-none focus:ring-2 ring-azul-pm"> </textarea></div>

                            <div class="col-span-1 flex col-start-3 row-start-3 pl-24"> <p>Fecha Inicio</p> </div>
                            <div class="col-span-1 row-start-3 pl-4 md:col-span-1">
                                <DatePicker selected={startDate} onChange={date => setStartDate(date)} />
                            </div>

                            <div class="col-span-1 col-start-5 row-start-3 pl-14"> <p>Fecha Fin</p> </div>
                            <div class="col-span-1 row-start-3 md:col-span-1">
                                <DatePicker selected={finishtDate} onChange={date => setFinishDate(date)} />
                            </div>

                            <div class="col-span-2 col-start-5 row-start-4">
                                <Popup trigger={<button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">Agregar Documentos empresa</button>} modal nested>
                                    {close => (
                                        <div className="modal">
                                            <button className="close" onClick={close}>
                                                &times;
                                            </button>
                                            <DocumentosEmpresa datos={datosPaseGeneral}
                                                _guardarDocumentosEmpresa={guardarDocumentosEmpresa} />

                                        </div>
                                    )}
                                </Popup>
                            </div>
                        </div>

                    </div>

                    <div class="h-8"></div>

                    {/** Parte inferior */}

                    <TablaTrabajadores datos={datosPaseGeneral} url={url}
                        _enviarFormulario={enviarFormulario}
                        _cancelarGuardado={cancelarGuardado}
                        _guardarPersona={guardarPersona} />
                </div>
            </div>
        </div>
    )
}
