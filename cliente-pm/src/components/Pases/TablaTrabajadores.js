import React, { useState, useEffect, useRef } from 'react'
import { useHistory } from "react-router-dom";
import Pagination from '../Pagination';
import { AgregarPersonaContratista } from './AgregarPersonaContratista';

import { Popup, Warper } from 'reactjs-popup';
import { DocumentosEmpresa } from './DocumentosEmpresa';
import AgregarPersona from './AgregarPersona';

export const TablaTrabajadores = props => {
    const url = props.url;
    let history = useHistory();

    //PAGINACION
    const [usuarios, setUsuarios] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [postsPerPage, setPostsPerPage] = useState(10);

    // Controlar pop-up
    const refContratista = useRef();
    const openContratista = () => refContratista.current.open();
    const closeContratista = () => refContratista.current.close();

    const refPases = useRef();
    const openPases = () => refPases.current.open()
    const closePases = () => refPases.current.close()


    // Persona para pase contratista

    const [personasContratista, setPersonaContratista] = useState({

        Rut: "",
        Nombres: "",
        PrimerApellido: "",
        SegundoApellido: "",
        Nacionalidad: "",
        DocumentosPersona: [],

    })


    // Obtener el indice inicial por pagina
    const offset = (currentPage - 1) * postsPerPage;
    //Borrar cuando se deje de probar   
    console.log(usuarios);
    //Funcion que cambia la pagina
    const onPageChanged = pageNumber => {
        switch (pageNumber) {
            case 'LEFT':
                if (currentPage > 1)
                    setCurrentPage(currentPage - 1);
                break;
            case 'RIGHT':
                if (currentPage < Math.ceil((props.datos ? props.datos.length : 0) / postsPerPage))
                    setCurrentPage(currentPage + 1);
                break;
            default:
                setCurrentPage(pageNumber);
                break;
        }
    };

    const sendDataContratista = (personaExterna, documentoPersona) => {

        // Datos Persona
        setPersonaContratista(anterior => ({
            ...anterior, // mantener lo que existe antes
            ["Rut"]: personaExterna.Rut,// solo cambiar el input mapeado
            ["Nombres"]: personaExterna.Nombres,// solo cambiar el input mapeado
            ["PrimerApellido"]: personaExterna.PrimerApellido,// solo cambiar el input mapeado
            ["SegundoApellido"]: personaExterna.SegundoApellido,// solo cambiar el input mapeado
            ["Nacionalidad"]: personaExterna.Nacionalidad,// solo cambiar el input mapeado
        }));

        // Documentos Persona
        setPersonaContratista(anterior => ({
            ...anterior, // mantener lo que existe antes
            ["DocumentosPersona"]: documentoPersona // solo cambiar el input mapeado
        }));

        // Cerramos el modal
        closeContratista()
    }

    const cerrarModalPases = () => {
        props._actualizarPagina();
        closePases();
    }

    useEffect(() => {

        if (props._guardarPersona)
            props._guardarPersona(personasContratista)

    }, [personasContratista]);


    useEffect(() => {
        const fetchUsers = async () => {
            setUsuarios(props.datos);
        };
        fetchUsers();
    }, []);


    return (
        <div class="bg-white p-4 sm:p-8 rounded-lg shadow-md">

            <div class="sm:mx-8 mx-0 sm:flex flex-row sm:justify-between">
                <p class="text-2xl leading-tight text-center md:text-left sm:ml-8 mb-0 sm:mb-4 sm:w-max">
                    Listado de Personas
                </p>

                {/* Botones para crear nuevo pase y pases buscados */}
                <div class="text-end flex-none">
                    <div class="flex-none md:flex w-full sm:space-x-3 space-x-2 sm:space-y-0 space-y-2">
                        {props.url == "/SolicitudContratista" &&
                            <Popup ref={refContratista} trigger={<button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">Agregar Persona Contratista</button>} modal nested>
                                <div className="modal">
                                    <AgregarPersonaContratista datos={props.datosPaseGeneral}
                                        _guardarPersonaC={sendDataContratista} />
                                </div>
                            </Popup>
                        }

                        {props.url != "/SolicitudContratista" &&
                            <Popup ref={refPases} trigger={<button
                                class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">Agregar Persona</button>} modal nested>
                                <div className="modal">
                                    <AgregarPersona cerrarModal={cerrarModalPases} />
                                </div>
                            </Popup>
                        }

                        <button type="submit" onClick={props._enviarFormulario}
                            class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                            Guardar
                        </button>

                        <button type="button" onClick={props._cancelarGuardado}
                            class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                            Cancelar
                        </button>
                    </div>
                </div>
            </div>
            
            <div class="sm:mx-8 mx-0 sm:flex flex-row mt-4 justify-content-end md:justify-end">
                <div class="relative inline-flex">
                    <div class="sm:text-right align-middle">Personas por página</div>
                    <svg class="w-2 h-2 absolute top-0 right-0 m-4 pointer-events-none" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 412 232">
                        <path d="M206 171.144L42.678 7.822c-9.763-9.763-25.592-9.763-35.355 0-9.763 9.764-9.763 25.592 0 35.355l181 181c4.88 4.882 11.279 7.323 17.677 7.323s12.796-2.441 17.678-7.322l181-181c9.763-9.764 9.763-25.592 0-35.355-9.763-9.763-25.592-9.763-35.355 0L206 171.144z" fill="#648299" fill-rule="nonzero" /></svg>
                    <select name="Estado"
                        onChange={e => {
                            setPostsPerPage(e.target.value);
                            setCurrentPage(1);
                        }
                        }
                        class="border border-gray-300 rounded-full text-gray-600 p-2 bg-gray-100 hover:border-gray-400 focus:outline-none appearance-none">
                        <option value='10'>10</option>
                        <option value='20'>20</option>
                        <option value='30'>30</option>
                    </select>
                </div>
            </div>

            <div class="mt-6 mx-0 sm:mx-8 mb-2 md:mb-1 overflow-x-auto shadow-md rounded-lg">
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
                                    Nacionalidad
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
                                    props.datos.slice(offset, offset + postsPerPage).map((value, index) => {
                                        return <tr key={index} class={index % 2 == 0 ? "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap"
                                            : "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap bg-gray-100"} >

                                            <td class="p-4">
                                                {value.Nombres} {value.PrimerApellido} {value.SegundoApellido}
                                            </td>
                                            <td class="p-4">
                                                {value.Rut === "" ? value.Pasaporte : value.Rut}
                                            </td>
                                            <td class="p-4">
                                                {value.Nacionalidad}
                                            </td>
                                            <td class="p-4 space-x-1">
                                                <button class="rounded-md bg-verde-pm hover:bg-amarillo-pm text-white p-2 transition duration-500">
                                                    Editar
                                                </button>
                                                <button class="rounded-md bg-verde-pm hover:bg-amarillo-pm text-white p-2 transition duration-500">
                                                    Eliminar
                                                </button>
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

            <div className="d-flex flex-row  align-items-center">
                <Pagination
                    postsPerPage={postsPerPage}
                    totalPosts={props.datos ?
                        props.datos.length > 0 ? props.datos.length : 0
                        : 0}
                    paginate={onPageChanged}
                />
            </div>

            {/* TODO:ver como se cambia esto mediante la paginacion */}

        </div>
    )
}
