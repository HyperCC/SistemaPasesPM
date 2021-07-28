import {React, useState} from 'react'
import { useHistory, Link } from 'react-router-dom';
import Popup from 'reactjs-popup';

export const ListaPersonas = props => {
    const history = useHistory();
    const [descripcion, setDescripcion] = useState();
    const rolCuenta = window.localStorage.getItem('data_current_usuario') ?
        JSON.parse(window.localStorage.getItem('data_current_usuario')).rol
        : null;

    const ingresarValoresMemoria = valorInput => {
        // obtener el valor
        const { name, value } = valorInput.target;

        // asignar el valor
        setDescripcion(value);
    };

    const AprobarPaseActual = () => {
        props.opcionCambiarEstado({
            PaseId: props.identificador,
            NuevoEstado: rolCuenta == 'HSEQ' ? 'PREAPROBADO'
                : rolCuenta == 'JEFE_OPERACIONES' ? 'PREAPROBADO'
                    : 'APROBADO',
            Observacion: descripcion
        });
    };

    const RechazarPaseActual = () => {
        props.opcionCambiarEstado({
            PaseId: props.identificador,
            NuevoEstado: "RECHAZADO",
            Observacion: descripcion
        });
    };

    return (
        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">

            <div class="mx-8 md:flex flex-row md:justify-between">
                <p class="text-2xl leading-tight text-center md:text-left md:ml-8 md:w-max">
                    Listado de Personas
                </p>

                {/* Botones para aprobar o rechazar pases */}
                <div class="text-end flex-none">
                    <div class="flex-none md:flex w-full space-x-3">

                        {rolCuenta != 'SOLICITANTE' &&
                            <button type="button" onClick={AprobarPaseActual}
                                class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                                {rolCuenta == 'HSEQ' || rolCuenta == 'JEFE_OPERACIONES' ? 'Pre-aprobar' : 'Aprobar'}
                            </button>
                        }

                        {rolCuenta != 'SOLICITANTE' &&
                            <div>
                                <Popup trigger={<button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">Rechazar</button>} modal nested>
                                    {close => (
                                        <div className="modal">

                                            <button className="close" onClick={close}>
                                                &times;
                                            </button>

                                            <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">
                                                <div class="col-span-1 row-span-2 col-start-1 row-start-1"><p>Descripcion del rechazo</p></div>
                                                <div class="col-span-3 row-span-2 col-start-2 row-start-1">
                                                    <textarea type="range" value={descripcion} onChange={ingresarValoresMemoria} name="descripcion" placeholder="range...."
                                                        class="border w-full app border-gray-300 p-2 my-2 rounded-md focus:outline-none focus:ring-2 ring-azul-pm">
                                                    </textarea>
                                                </div>
                                            </div>

                                            <button type="button" onClick={RechazarPaseActual}
                                                class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                                                Rechazar
                                            </button>

                                        </div>

                                    )}
                                </Popup>

                            </div>
                        }

                        <button type="button" onClick={() => history.goBack()}
                            class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                            Volver
                        </button>
                    </div>
                </div>
            </div>

            <div class="mt-6 mx-0 md:mx-8 mb-2 md:mb-1 overflow-x-auto shadow-md rounded-lg">
                <div class="inline-block min-w-full overflow-hidden">
                    <table class="min-w-full leading-normal">
                        <thead>
                            {/* HEADERS PARA LA TABLA */}
                            <tr class="bg-azul-pm select-none text-sm uppercase text-gray-100 text-center border-b border-gray-200">
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Nombres
                                </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Apellidos
                                </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Rut o Pasaporte
                                </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Nacionalidad
                                </th>
                                <th>
                                    Documentos
                                </th>
                            </tr>
                        </thead>

                        <tbody>
                            {/* CICLO FOR CON TODOS LOS DATOS PARA CADA PASE */}
                            {props.datos ?
                                props.datos.length > 0 ?
                                    props.datos.map((value, index) => {
                                        return <tr key={index} class={index % 2 == 0 ? "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap"
                                            : "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap bg-gray-100"} >

                                            <td class="p-4">
                                                {value.nombres}
                                            </td>
                                            <td class="p-4">
                                                {value.primerApellido} {value.segundoApellido}
                                            </td>
                                            <td class="p-4">
                                                {value.rut === "" ? "P:" + value.pasaporte : "R: " + value.rut}
                                            </td>
                                            <td class="p-4">
                                                {value.nacionalidad}
                                            </td>
                                            <td>
                                                <Link class="rounded-md bg-verde-pm hover:bg-amarillo-pm text-white p-2 transition duration-500"
                                                    to={{
                                                        pathname: "/RevisarDocumentoEmpresa",
                                                        state: {

                                                            documentosEmpresa: value.documentoCompletosRel,

                                                        }
                                                    }}>
                                                    Documentos Persona
                                                </Link>
                                            </td>
                                        </tr>
                                    })
                                    : <tr class="text-center"><td class="p-4" colSpan="7">No hay personas asociadas</td></tr>
                                : <tr class="text-center"><td class="p-4" colSpan="7">No hay personas asociadas</td></tr>
                            }

                        </tbody>
                    </table>
                </div>
            </div>

            {/* TODO:ver como se cambia esto mediante la paginacion */}
            <div class="px-5 bg-white pt-5 flex flex-col xs:flex-row items-center xs:justify-between">
                <div class="flex items-center">
                    <button type="button" class="w-full p-4 border text-base rounded-l-xl text-gray-600 bg-white hover:bg-gray-100">
                        <svg width="9" fill="currentColor" height="8" class="" viewBox="0 0 1792 1792" xmlns="http://www.w3.org/2000/svg">
                            <path d="M1427 301l-531 531 531 531q19 19 19 45t-19 45l-166 166q-19 19-45 19t-45-19l-742-742q-19-19-19-45t19-45l742-742q19-19 45-19t45 19l166 166q19 19 19 45t-19 45z">
                            </path>
                        </svg>
                    </button>
                    <button type="button" class="w-full px-4 p-2 border-t border-b text-base text-indigo-500 bg-white hover:bg-gray-100 ">
                        1
                    </button>
                    <button type="button" class="w-full px-4 p-2 border text-base text-gray-600 bg-white hover:bg-gray-100">
                        2
                    </button>
                    <button type="button" class="w-full px-4 p-2 border-t border-b text-base text-gray-600 bg-white hover:bg-gray-100">
                        3
                    </button>
                    <button type="button" class="w-full px-4 p-2 border text-base text-gray-600 bg-white hover:bg-gray-100">
                        4
                    </button>
                    <button type="button" class="w-full p-4 border-t border-b border-r text-base  rounded-r-xl text-gray-600 bg-white hover:bg-gray-100">
                        <svg width="9" fill="currentColor" height="8" class="" viewBox="0 0 1792 1792" xmlns="http://www.w3.org/2000/svg">
                            <path d="M1363 877l-742 742q-19 19-45 19t-45-19l-166-166q-19-19-19-45t19-45l531-531-531-531q-19-19-19-45t19-45l166-166q19-19 45-19t45 19l742 742q19 19 19 45t-19 45z">
                            </path>
                        </svg>
                    </button>
                </div>
            </div>
        </div>
    );
};