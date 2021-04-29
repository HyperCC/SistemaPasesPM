import React from 'react';

const TablaPases = props => {
    return (
        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">

            <div class="mx-8 md:flex flex-row md:justify-between">
                <p class="text-2xl leading-tight text-center md:text-left md:ml-8 md:w-max">
                    Listado de Pases Solicitados
                </p>

                {/* Botones para crear nuevo pase y pases buscados */}
                <div class="text-end flex-none">
                    <form class="flex-none md:flex w-full space-x-3">
                        <button class="flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-green-600 rounded-md shadow-md hover:bg-yellow-400 focus:outline-none transition duration-500" type="submit">
                            Nuevo Pase
                        </button>
                    </form>
                </div>
            </div>

            <div class="mt-6 mx-0 md:mx-8 mb-2 md:mb-1 overflow-x-auto shadow-md rounded-lg">
                <div class="inline-block min-w-full overflow-hidden">
                    <table class="min-w-full leading-normal">
                        <thead>
                            {/* HEADERS PARA LA TABLA */}
                            <tr class="bg-gray-800 select-none text-sm uppercase text-gray-100 text-center border-b border-gray-200">
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
                            {props.datos.map((value, index) => {
                                return <tr key={index} class={index % 2 == 0 ? "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap"
                                    : "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap bg-gray-100"} >

                                    <td class="p-4">
                                        {value.FechaInicio}
                                    </td>
                                    <td class="p-4">
                                        {value.FechaTermino}
                                    </td>
                                    <td class="p-4">
                                        {value.Motivo}
                                    </td>
                                    <td class="p-4">
                                        {value.Area}
                                    </td>
                                    <td class="p-4">
                                        {value.Tipo}
                                    </td>

                                    {/* ELECCION DEL COLOR DEL ESTADO PARA EL PASE */}
                                    <td class="p-4">
                                        <span class={(() => {
                                            switch (value.Estado) {
                                                case "Finalizado": return "px-3 py-1 bg-purple-100 rounded-full font-semibold text-green-900 leading-tight mx-auto";
                                                case "Revision": return "px-3 py-1 bg-yellow-100 rounded-full font-semibold text-green-900 leading-tight mx-auto";
                                                case "Aprobado": return "px-3 py-1 bg-green-100 rounded-full font-semibold text-green-900 leading-tight mx-auto";
                                                default: return "px-3 py-1 bg-red-100 rounded-full font-semibold text-green-900 leading-tight mx-auto";
                                            }
                                        })()}>
                                            {value.Estado}
                                        </span>
                                    </td>

                                    <td class="p-4 space-x-1">
                                        <a href="#" class="rounded-md bg-green-600 hover:bg-yellow-400 text-white p-2 transition duration-500">
                                            Revisar
                                        </a>
                                        <a href="#" class="rounded-md bg-green-600 hover:bg-yellow-400 text-white p-2 transition duration-500">
                                            Editar
                                        </a>
                                    </td>
                                </tr>
                            })}
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