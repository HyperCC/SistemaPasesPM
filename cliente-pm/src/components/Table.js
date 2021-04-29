import React from 'react'

const Table = () => {
    return (            
        <div class="container mx-auto px-4 sm:px-8 bg-white rounded px-4 flex flex-col justify-between leading-normal shadow">
            <div class="py-8">
                <div class="flex flex-row mb-1 sm:mb-0 justify-between w-full">
                    <h2 class="text-2xl leading-tight">
                        Listado de pases solicitados
                    </h2>
                    
                    <div class="text-end">
                        <form class="flex w-full max-w-sm space-x-3">
                            <button class="flex-shrink-0 px-4 py-2 text-base font-semibold text-white bg-verde-pm rounded-lg shadow-md hover:bg-amarillo-pm focus:outline-none focus:ring-2 focus:ring-purple-500 focus:ring-offset-2 focus:ring-offset-purple-200" type="submit">
                                Nuevo Pase
                            </button>
                            <div class=" relative ">
                                <input type="text" id="&quot;form-subscribe-Filter" class=" rounded-lg border-transparent flex-1 appearance-none border border-gray-300 w-full py-2 px-4 bg-white text-gray-700 placeholder-gray-400 shadow-sm text-base focus:outline-none focus:ring-2 focus:ring-purple-600 focus:border-transparent" placeholder="busqueda"/>
                            </div>
                            <button class="flex-shrink-0 px-4 py-2 text-base font-semibold text-white bg-verde-pm rounded-lg shadow-md hover:bg-amarillo-pm focus:outline-none focus:ring-2 focus:ring-purple-500 focus:ring-offset-2 focus:ring-offset-purple-200" type="submit">
                                Filtro
                            </button>
                        </form>
                    </div>
                </div>
                <div class="-mx-4 sm:-mx-8 px-4 sm:px-8 py-4 overflow-x-auto">
                    <div class="inline-block min-w-full shadow rounded-lg overflow-hidden">
                        <table class="min-w-full leading-normal">
                            <thead>
                                <tr>
                                    <th scope="col" class="text-center px-5 py-3 bg-white  border-b border-gray-200 text-gray-800  text-left text-sm uppercase font-normal">
                                        Fecha Inicio
                                    </th>
                                    <th scope="col" class="text-center px-5 py-3 bg-white  border-b border-gray-200 text-gray-800  text-left text-sm uppercase font-normal">
                                        Fecha Termino
                                    </th>
                                    <th scope="col" class="text-center px-5 py-3 bg-white  border-b border-gray-200 text-gray-800  text-left text-sm uppercase font-normal">
                                        Motivo
                                    </th>
                                    <th scope="col" class="text-center px-5 py-3 bg-white  border-b border-gray-200 text-gray-800  text-left text-sm uppercase font-normal">
                                        Area
                                    </th>
                                    <th scope="col" class="text-center px-5 py-3 bg-white  border-b border-gray-200 text-gray-800  text-left text-sm uppercase font-normal">
                                        Tipo
                                    </th>
                                    <th scope="col" class="text-center px-5 py-3 bg-white  border-b border-gray-200 text-gray-800  text-left text-sm uppercase font-normal">
                                        Estado
                                    </th>
                                    <th scope="col" class="text-center px-5 py-3 bg-white  border-b border-gray-200 text-gray-800  text-left text-sm uppercase font-normal">
                                        Acciones
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="text-center px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <p class="text-gray-900 whitespace-no-wrap">
                                            05/04/2021
                                        </p>
                                        
                                    </td>
                                    <td class="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <p class="text-center text-gray-900 whitespace-no-wrap">
                                            23/04/2021
                                        </p>
                                    </td>
                                    <td class="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <p class="text-center text-gray-900 whitespace-no-wrap">
                                            Entrevista para desarrollo
                                        </p>
                                    </td>
                                    <td class="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <p class="text-center text-gray-900 whitespace-no-wrap">
                                            HSEQ
                                        </p>
                                    </td>
                                    <td class="px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <p class="text-center text-gray-900 whitespace-no-wrap">
                                            Visita
                                        </p>
                                    </td>
                                    <td class="text-center px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <span class="relative inline-block px-3 py-1 font-semibold text-green-900 leading-tight">
                                            <span aria-hidden="true" class="absolute inset-0 bg-green-200 opacity-50 rounded-full">
                                            </span>
                                            <span class="relative">
                                                Finalizado
                                            </span>
                                        </span>
                                    </td>
                                    <td class="text-center px-5 py-5 border-b border-gray-200 bg-white text-sm space-x-1">
                                        <a href="#" class="p-1 rounded bg-verde-pm hover:bg-amarillo-pm text-white p-2 px-2">
                                            Revisar
                                        </a>
                                        <a href="#" class="p-1 rounded bg-verde-pm hover:bg-amarillo-pm text-white p-2 px-2">
                                            Editar
                                        </a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text-center px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <p class="text-gray-900 whitespace-no-wrap">
                                            28/04/2021
                                        </p>
                                        
                                    </td>
                                    <td class="text-center px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <p class="text-gray-900 whitespace-no-wrap">
                                            28/04/2021
                                        </p>
                                    </td>
                                    <td class="text-center px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <p class="text-gray-900 whitespace-no-wrap">
                                            Muestra de avance
                                        </p>
                                    </td>
                                    <td class="text-center px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <p class="text-gray-900 whitespace-no-wrap">
                                            Informatica
                                        </p>
                                    </td>
                                    <td class="text-center px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <p class="text-gray-900 whitespace-no-wrap">
                                            Visita
                                        </p>
                                    </td>
                                    <td class="text-center px-5 py-5 border-b border-gray-200 bg-white text-sm">
                                        <span class="relative inline-block px-3 py-1 font-semibold text-green-900 leading-tight">
                                            <span aria-hidden="true" class="absolute inset-0 bg-yellow-200 opacity-50 rounded-full">
                                            </span>
                                            <span class="relative">
                                                Revisión
                                            </span>
                                        </span>
                                    </td>
                                    <td class="text-center px-5 py-5 border-b border-gray-200 bg-white text-sm space-x-1">
                                        <a href="#" class="p-1 rounded bg-verde-pm hover:bg-amarillo-pm text-white p-2 px-2">
                                            Revisar
                                        </a>
                                        <a href="#" class="p-1 rounded bg-verde-pm hover:bg-amarillo-pm text-white p-2 px-2">
                                            Editar
                                        </a>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div class="px-5 bg-white py-5 flex flex-col xs:flex-row items-center xs:justify-between">
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
                </div>
            </div>
        </div>
    )
}

export default Table
