import React from 'react';
import { Link } from "react-router-dom";

const TablaPases = props => {

    console.log(props.soloPases);
    const currentRol = props.currentRol;

    return (
        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">

            <div class="mx-8 md:flex flex-row md:justify-between">
                <p class="text-2xl leading-tight text-center md:text-left md:ml-8 md:w-max">
                    Listado de Pases {currentRol == 'SOLICITANTE' ? 'Solicitados' : 'Asignados'}
                </p>

                {/* Botones para crear nuevo pase y pases buscados */}
                {currentRol == 'SOLICITANTE' &&
                    <div class="text-end flex-none">
                        <form class="flex-none md:flex w-full space-x-3">
                            <a href="/SolicitudPases"
                                class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                                Nuevo Pase
                            </a>
                        </form>
                    </div>
                }
            </div>

            <div class="mt-6 mx-0 md:mx-8 mb-2 md:mb-1 overflow-x-auto shadow-md rounded-lg">
                <div class="inline-block min-w-full overflow-hidden">
                    <table class="min-w-full leading-normal">
                        <thead>
                            {/* HEADERS PARA LA TABLA */}
                            <tr class="bg-azul-pm select-none text-sm uppercase text-gray-100 text-center border-b border-gray-200">
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Fecha Inicio
                                </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Fecha Termino
                                </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Motivo
                                </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Area
                                </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Tipo
                                </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Estado
                                </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Acciones
                                </th>
                            </tr>
                        </thead>

                        <tbody>
                            {/* CICLO FOR CON TODOS LOS DATOS PARA CADA PASE */}
                            {props.soloPases ?
                                props.soloPases.length > 0 ?
                                    props.soloPases.map((value, index) => {
                                        return <tr key={index} class={index % 2 == 0 ? "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap"
                                            : "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap bg-gray-100"} >

                                            <td class="p-4">
                                                {value.fechaInicio}
                                            </td>
                                            <td class="p-4">
                                                {value.fechaTermino}
                                            </td>
                                            <td class="p-4">
                                                {value.motivo}
                                            </td>
                                            <td class="p-4">
                                                {value.area}
                                            </td>
                                            <td class="p-4 lowercase">
                                                {value.tipo}
                                            </td>

                                            {/* ELECCION DEL COLOR DEL ESTADO PARA EL PASE */}
                                            <td class="p-4">
                                                <span class={(() => {
                                                    switch (value.estado) {
                                                        case "FINALIZADO": return "px-3 py-1 bg-purple-200 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                        case "PENDIENTE": return "px-3 py-1 bg-yellow-200 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                        case "APROBADO": return "px-3 py-1 bg-green-200 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                        case "PREAPROBADO": return "px-3 py-1 bg-blue-200 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                        default: return "px-3 py-1 bg-red-200 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                    }
                                                })()}>
                                                    {value.estado == 'PREAPROBADO' ? 'pre-aprobado' : value.estado}
                                                </span>
                                            </td>

                                            <td class="p-4 space-x-1">
                                                <Link class="rounded-md bg-verde-pm hover:bg-amarillo-pm text-white p-2 transition duration-500"
                                                    to={{
                                                        pathname: "/RevisarPase",
                                                        state: {
                                                            fechaInicio: value.fechaInicio,
                                                            fechaTermino: value.fechaTermino,
                                                            motivo: value.motivo,
                                                            area: value.area,
                                                            tipo: value.tipo,
                                                            estado: value.estado,
                                                            personas: value.personaExternasRel,
                                                            paseId: value.paseId
                                                        }
                                                    }}>
                                                    Revisar
                                                </Link>

                                            </td>
                                        </tr>
                                    })
                                    : <tr class="text-center"><td class="p-4" colSpan="7">No hay pases registrados</td></tr>
                                : <tr class="text-center"><td class="p-4" colSpan="7">Cargando los pases registrados</td></tr>
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
}

export default TablaPases;