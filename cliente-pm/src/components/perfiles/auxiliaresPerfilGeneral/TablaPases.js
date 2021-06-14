import React, {useEffect, useState} from 'react';
import { Link } from "react-router-dom";
import Pagination from '../../Pagination';

const TablaPases = props => {

    console.log(props.soloPases);
    const currentRol = props.currentRol;

    //PAGINACION
    const [pases, setPases] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [postsPerPage, setPostsPerPage] = useState(5);

    useEffect(() => {
        const fetchUsers = async () => {
            setPases(props.soloPases);
        };    
        fetchUsers();
    }, []);

    // Obtener el indice inicial por pagina
    const offset = (currentPage - 1) * postsPerPage;
    //Borrar cuando se deje de probar 
    console.log(pases);
    //Funcion que cambia la pagina
    const onPageChanged = pageNumber => {
        switch (pageNumber){
            case 'LEFT':
                if (currentPage > 1)
                    setCurrentPage(currentPage - 1);                
                break;
            case 'RIGHT':
                if (currentPage < Math.ceil((props.soloPases? props.soloPases.length: 0) / postsPerPage))
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
            <div class="mx-8 md:flex flex-row mt-4 justify-content-end md:justify-end">
                <div class="relative inline-flex">
                    <div class="md:text-right align-middle">Pases por página</div>
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
                                    props.soloPases.slice(offset, offset + postsPerPage).map((value, index) => {
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
                                                        case "FINALIZADO": return "px-3 py-1 bg-purple-100 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                        case "PENDIENTE": return "px-3 py-1 bg-yellow-100 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                        case "APROBADO": return "px-3 py-1 bg-green-100 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                        default: return "px-3 py-1 bg-red-100 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                    }
                                                })()}>
                                                    {value.estado}
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
            <div className="d-flex flex-row  align-items-center">
                <Pagination
                    postsPerPage={postsPerPage}
                    totalPosts={props.soloPases? 
                                    props.soloPases.length > 0 ? props.soloPases.length : 1 
                                    : 1}
                    paginate={onPageChanged}
                />
            </div>

            
        </div>
    );
}

export default TablaPases;