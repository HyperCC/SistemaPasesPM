import React, { useState, useEffect } from 'react';
import { useHistory } from "react-router-dom";
import { cambiarRol } from '../../actions/RolAction';
import Pagination from '../Pagination';

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
                    props._setCurrentNotification('exi-cre000');
                }

                // si no hay conexion con la API
            } else {
                console.log('no hay conexion con la API');
                props._setCurrentNotification('err-nhc000');
            }
        });
    };

    const [usuarios, setUsuarios] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [postsPerPage, setPostsPerPage] = useState(5);

    useEffect(() => {
        const fetchUsers = async () => {
            setUsuarios(props.datos);
        };
        fetchUsers();
    }, []);

    // Obtener el indice inicial por pagina
    const offset = (currentPage - 1) * postsPerPage;
    //Borrar cuando se deje de probar
    console.log("Paso por aqui");
    console.log(usuarios);
    //Funcion que cambia la pagina
    const onPageChanged = pageNumber => {
        switch (pageNumber){
            case 'LEFT':
                if (currentPage > 1)
                    setCurrentPage(currentPage - 1);                
                break;
            case 'RIGHT':
                if (currentPage < Math.ceil((usuarios? usuarios.length: 0) / postsPerPage))
                    setCurrentPage(currentPage + 1);                    
                break;
            default:
                setCurrentPage(pageNumber);
                break;
        }       
    };

    return (

        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
            <div class="mx-8 md:flex flex-row md:justify-between">
                <p class="text-2xl leading-tight text-center md:text-left md:ml-8 md:w-max">
                    Listado de Usuarios
                </p>

                <div>
                    <div class="md:text-right">Usuarios por página</div>
                    <div class="relative inline-flex">
                        <svg class="w-2 h-2 absolute top-0 right-0 m-4 pointer-events-none" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 412 232">
                            <path d="M206 171.144L42.678 7.822c-9.763-9.763-25.592-9.763-35.355 0-9.763 9.764-9.763 25.592 0 35.355l181 181c4.88 4.882 11.279 7.323 17.677 7.323s12.796-2.441 17.678-7.322l181-181c9.763-9.764 9.763-25.592 0-35.355-9.763-9.763-25.592-9.763-35.355 0L206 171.144z" fill="#648299" fill-rule="nonzero" /></svg>
                        <select name="Estado" 
                                onChange={e => {
                                                setPostsPerPage(e.target.value);
                                                setCurrentPage(1);
                                            }
                                        } 
                                class="border border-gray-300 rounded-full text-gray-600 p-2 bg-gray-100 hover:border-gray-400 focus:outline-none appearance-none">
                            <option value='5'>5</option>
                            <option value='10'>10</option>
                            <option value='25'>25</option>
                            <option value='50'>50</option>
                        </select>
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

                                    props.datos.slice(offset, offset + postsPerPage).map((valor, index) => {

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

            <div className="d-flex flex-row  align-items-center">
                <Pagination
                    postsPerPage={postsPerPage}
                    totalPosts={props.datos ?
                        props.datos.length > 0 ? props.datos.length : 0
                        : 0}
                    paginate={onPageChanged}
                />
            </div>
        </div>
    )
}
