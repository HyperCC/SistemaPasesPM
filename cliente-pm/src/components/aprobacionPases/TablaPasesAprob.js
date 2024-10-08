import React, { useState } from 'react';
import Popup from 'reactjs-popup';
import SelectSearch from 'react-select-search';

const TablaPases = props => {
    

    const [filtro, setFiltro] = useState(null);
    const [tipo, setTipo] = useState('')
    const [estado, setEstado] = useState('');

    console.log(filtro)

    // Filtro de estado
    const filtroEstado = [].concat(props.soloPases).sort(function(b,a){
        if(a.Estado === "FINALIZADO") return -1;
        return 0;
    })
    if(filtro == null){
        setFiltro(filtroEstado);
    }
    

    // Filtro por fecha de inicio

    var filtroIni =[].concat(props.soloPases).sort(function(a,b){
            if(a.FechaInicio > b.FechaInicio){
                return -1;
            }if(a.FechaInicio < b.FechaInicio){
                return 1;
            }
            return 0;
        });
     
    // Filtro por fecha de termino

    var filtroFin = [].concat(props.soloPases).sort(function(a,b){
                    if(a.FechaTermino > b.FechaTermino){
                        return -1;
                    }if(a.FechaTermino < b.FechaTermino){
                        return 1;
                    }
                    return 0;
                });  
                
    // Filtro por tipo de pase

    var filtroTipo = [].concat(props.soloPases).sort(function(a,b){
        if(a.Tipo == tipo){
            return -1;
        }
        return 0;
    }); 

    // Filtro por estado del pase

    var filtroEstadoB = [].concat(props.soloPases).sort(function(a,b){
        if(a.Estado == estado){
            return -1;
        }
        return 0;
    }); 

    // Funciones para cambiar filtro

    function showIni(){
        setFiltro(filtroIni);
    }

    function showFin(){
        setFiltro(filtroFin);
    }

    function showTipo(){
        setFiltro(filtroTipo)
    }

    function showEstado(){
        setFiltro(filtroEstadoB)
    }

    if(tipo!=''){
        showTipo()
        setTipo('') 
    }

    if(estado!=''){
        setFiltro('')
        showEstado()
        setEstado('')
    }


    return (
        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">

            <div class="mx-8 md:flex flex-row md:justify-between">
                <p class="text-2xl leading-tight text-center md:text-left md:ml-8 md:w-max">
                    Listado de Pases Solicitados
                </p>
            </div>

            <div class="mx-8 md:flex flex-row py-2">
               
                <button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500"
                    onClick={() => showIni()}>
                    Fecha Inicio
                </button>

                <button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500"
                    onClick={() => showFin()}>
                    Fecha Termino
                </button>

                <div class="">
                    <SelectSearch
                    value={tipo}
                    onChange={setTipo}
                    placeholder="Seleccione el tipo de pase"
                    options={[
                        { value: 'Visita', name: 'Visita' },
                        { value: 'Contratista', name: 'Contratista' },
                        { value: 'Proveedor', name: 'Proveedor' },
                        { value: 'Tripulante', name: 'Tripulante' },
                        { value: 'Uso muelle', name: 'Uso muello' },
                        ]}
                    />
                </div>

                <div>
                <SelectSearch
                    value={estado}
                    onChange={setEstado}
                    placeholder="Seleccione el estado"
                    options={[
                        { value: 'FINALIZADO', name: 'Finalizado' },
                        { value: 'APROBADO', name: 'Aprobado' },
                        { value: 'REVISION', name: 'Revisión' },
                        ]}
                    />
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
                            {filtro && filtro.map((value, index) => {
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
                                    <td class="p-4 lowercase">
                                        {value.Tipo}
                                    </td>

                                    {/* ELECCION DEL COLOR DEL ESTADO PARA EL PASE */}
                                    <td class="p-4">
                                        <span class={(() => {
                                            switch (value.Estado) {
                                                case "FINALIZADO": return "px-3 py-1 bg-purple-100 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                case "PENDIENTE": return "px-3 py-1 bg-yellow-100 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                case "APROBADO": return "px-3 py-1 bg-green-100 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                                default: return "px-3 py-1 bg-red-100 rounded-full font-semibold text-green-900 leading-tight mx-auto lowercase";
                                            }
                                        })()}>
                                            {value.Estado}
                                        </span>
                                    </td>

                                    <td class="p-4 space-x-1">
                                        <Popup trigger={<button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500"> Aprobar</button>} modal nested>
                                            { close => (
                                            <div className="modal">
                                        
                                                <button className="close" onClick={close}>
                                                &times;
                                                </button>
                                                
                                                <div className="header"> Datos Pase Solicitado </div>
                                                <div className="content">

                                                    <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">
                                                        <div class="col-span-2 col-start-1 pl-14"> <p>Fecha Inicio</p> </div>
                                                        <div class="col-span-2 col-start-2 md:col-span-2">
                                                            <p>{value.FechaInicio}</p>
                                                        </div>     

                                                        <div class="col-span-2 col-start-1 pl-14"> <p>Fecha Termino</p> </div>
                                                        <div class="col-span-2 col-start-2 md:col-span-2">
                                                            <p>{value.FechaTermino}</p>
                                                        </div>     

                                                        <div class="col-span-2 col-start-1 pl-14"> <p>Motivo</p> </div>
                                                        <div class="col-span-2 col-start-2 md:col-span-2">
                                                            <p>{value.Motivo}</p>
                                                        </div>    

                                                        <div class="col-span-2 col-start-1 pl-14"> <p>Tipo de Pase</p> </div>
                                                        <div class="col-span-2 col-start-2 md:col-span-2">
                                                            <p>{value.Tipo}</p>
                                                        </div>         
                                                        
                                                    </div>
                                                </div>

                                                <div class="actions flex justify-between py-8 px-8">
                        
                                                    <button
                                                        class="bg-verde-pm pl-4 hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500"
                                                        onClick={() => {
                                                        console.log('modal closed ');
                                                        close();
                                                        }}
                                                    >
                                                        Rechazar
                                                    </button>

                                                    <button
                                                        class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500"
                                                        onClick={() => {
                                                            console.log('modal closed ');
                                                            close();
                                                        }}
                                                    >
                                                        Aprobar
                                                    </button>
                                                </div>
                                            </div>
                                            )}
                                        </Popup>
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