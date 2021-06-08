import React, { useState } from 'react';
import { useHistory } from "react-router-dom";
import { cambiarRol } from '../../actions/RolAction';

export const TablaAllUsuarios = props => {
    const url = props.url;
    let history = useHistory();

    const cambiarCurrentRol = (datos) => {
        props._setCurrentNotification('inf-ccr000');

        // enviar los datos a la API
        cambiarRol(datos).then(response => {

            // si existe alguna respuesta de la API
            if (typeof response !== 'undefined') {
                console.log('si existe conexion con la api. ', response);

                // si se reciben errores
                if (typeof response.data.errores != 'undefined') {
                    console.log('existen errores en la respuesta: ', response.data.errores.mensaje);

                    console.log('el tipo de error: ', response.data.errores.tipoError);
                    props._setCurrentNotification(response.data.errores.tipoError);

                    // si toda la operacion salio ok
                } else {
                    console.log('toda la operacion fue correcta');
                    //window.localStorage.setItem('mensaje_success', 'exi-ptre00');
                    //window.localStorage.setItem('mensaje_success_showed', false);
                    props._setCurrentNotification('exi-cre000');
                }

                // si no hay conexion con la API
            } else {
                console.log('no hay conexion con la API');
                props._setCurrentNotification('err-nhc000');
            }
        });
    }

    return (
        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">

            <div class="mx-8 md:flex flex-row md:justify-between">
                <p class="text-2xl leading-tight text-center md:text-left md:ml-8 md:w-max">
                    Listado de Usuarios
                </p>
            </div>

            <div class="mt-6 mx-0 md:mx-8 mb-2 md:mb-1 overflow-x-auto shadow-md rounded-lg">
                <div class="inline-block min-w-full overflow-hidden">
                    <table class="min-w-full leading-normal">
                        <thead>
                            {/* HEADERS PARA LA TABLA */}
                            <tr class="bg-azul-pm select-none text-sm uppercase text-gray-100 text-center border-b border-gray-200">
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Nombre Completo
                                    </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Rut o Pasaporte
                                    </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Email
                                    </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Nombre Empresa
                                    </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Rol
                                    </th>
                                <th scope="col" class="px-5 py-3 font-normal">
                                    Acciones
                                    </th>
                            </tr>
                        </thead>

                        <tbody>
                            {/* CICLO FOR CON TODOS LOS DATOS PARA CADA PASE */}
                            {props.datos ?
                                props.datos.length > 0 ?
                                    props.datos.map((valor, index) => {

                                        // variables para cambiar el rol actual
                                        let currentRol = valor.rol;

                                        // datos a enviar para cambiar el rol
                                        let dataActualizarRol = {
                                            'RolActualizar': currentRol,
                                            'RolAnterior': valor.rol,
                                            'EmailUsuario': valor.email
                                        };

                                        // asignar nuevo rol
                                        const asignarNuevoRol = (nuevoValor) => {
                                            currentRol = nuevoValor.target.value;

                                            dataActualizarRol = {
                                                'RolActualizar': currentRol,
                                                'RolAnterior': valor.rol,
                                                'EmailUsuario': valor.email
                                            };

                                            console.log('OBJETO COMPLETO ', dataActualizarRol);
                                            console.log('VALOR ORIGINAL ', valor.rol);
                                            console.log('VALOR DEL ROL nuevo ', currentRol);
                                        }

                                        return <tr key={index} class={index % 2 == 0 ? "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap"
                                            : "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap bg-gray-100"} >

                                            <td class="p-4">
                                                {valor.nombreCompleto}
                                            </td>
                                            <td class="p-4">
                                                {valor.rut === "" ? valor.pasaporte : valor.rut}
                                            </td>
                                            <td class="p-4">
                                                {valor.email}
                                            </td>
                                            <td class="p-4">
                                                {valor.nombreEmpresa}
                                            </td>
                                            <td class="p-4">
                                                <select onChange={asignarNuevoRol} name="RolActualizar" class="bg-gray-200 p-2 rounded-md ">
                                                    <option selected={currentRol == 'SOLICITANTE' ? 'selected' : ''} value="SOLICITANTE" class="text-center">Solicitante</option>
                                                    <option selected={currentRol == 'CONTACTO' ? 'selected' : ''} value="CONTACTO">Contacto</option>
                                                    <option selected={currentRol == 'HSEQ' ? 'selected' : ''} value="HSEQ" >HSEQ</option>
                                                    <option selected={currentRol == 'JEFE_OPERACIONES' ? 'selected' : ''} value="JEFE_OPERACIONES">Jefe Operaciones</option>
                                                    <option selected={currentRol == 'OPIP' ? 'selected' : ''} value="OPIP">OPIP</option>
                                                    <option selected={currentRol == 'sin rol' ? 'selected' : ''} value="sin rol" class="text-center">Sin Rol</option>
                                                </select>
                                            </td>

                                            <td class="p-4 space-x-1">
                                                <button type="button" onClick={() => cambiarCurrentRol(dataActualizarRol)} class="rounded-md bg-verde-pm hover:bg-amarillo-pm text-white p-2 transition duration-500">
                                                    Guardar
                                                </button>

                                                <button type="button" class="rounded-md bg-verde-pm hover:bg-amarillo-pm text-white p-2 transition duration-500">
                                                    Eliminar
                                                 </button>
                                            </td>
                                        </tr>
                                    })
                                    : <tr class="text-center"><td class="p-4" colSpan="7">No hay usuarios creados</td></tr>
                                : <tr class="text-center"><td class="p-4" colSpan="7">No hay usuarios creados</td></tr>
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
    )
}
