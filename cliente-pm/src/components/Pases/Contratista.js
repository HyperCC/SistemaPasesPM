import React, {useState} from 'react';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { TablaTrabajadores } from './TablaTrabajadores';


export const Contratista = () => {
    
    const [startDate, setStartDate] = useState(new Date());
    const [finishtDate, setFinishDate] = useState(new Date());
    const url = "/SolicitudContratista";

    const dataTablaGeneral = [{
        Nombre: 'x',
        RutPasaporte: 'x',
        Nacionalidad: 'x'
    }]

    const [datosPaseGeneral, setDatosPaseGeneral] = useState({
        Area: '',
        Motivo: '',
        RutEmpresa: '',
        NombreEmpresa: '',
        Tipo: 'CONTRATISTA',
        ServicioAdjudicado: '',
        Completitud: '',
        Observaciones: '',
        FechaInicio: '',
        FechaTermino: '',

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

    

    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">
                    
                    {/** Parte superior */}
                    <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
                        <p class="text-2xl leading-tight mx-8 text-center md:text-left md:ml-16">
                            Información general - Pase Contratista
                        </p>

                        <div class="grid grid-cols-7 gap-6 md:grid-cols-6 mt-6 mx-8 mb-2 md:mb-0">
                            <div class="col-span-1 col-start-1 row-start-1"> <p>Área</p> </div>
                            <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                                <p>
                                    <div class="relative inline-flex">
                                        <svg class="w-2 h-2 absolute top-0 right-0 m-4 pointer-events-none" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 412 232"><path d="M206 171.144L42.678 7.822c-9.763-9.763-25.592-9.763-35.355 0-9.763 9.764-9.763 25.592 0 35.355l181 181c4.88 4.882 11.279 7.323 17.677 7.323s12.796-2.441 17.678-7.322l181-181c9.763-9.764 9.763-25.592 0-35.355-9.763-9.763-25.592-9.763-35.355 0L206 171.144z" fill="#648299" fill-rule="nonzero"/></svg>
                                        <select value={datosPaseGeneral.Area} onChange={ingresarValoresMemoria} class="border border-gray-300 rounded-full text-gray-600 h-10 pl-5 pr-10 bg-white hover:border-gray-400 focus:outline-none appearance-none">
                                            <option>Selecciones el Área</option>
                                            <option>Informatica</option>
                                        </select>
                                    </div>
                                </p>
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

                            <div class="col-span-1 row-span-2 col-start-3 row-start-1 pl-14"><p>Motivo visita</p></div>
                            <div class="col-span-3 row-span-2 col-start-4 row-start-1"><textarea type="range" value={datosPaseGeneral.Motivo} onChange={ingresarValoresMemoria} name="MotivoVisita" placeholder="range...." class="border w-full app border-gray-300 p-2 my-2 rounded-md focus:outline-none focus:ring-2 ring-azul-pm"> </textarea></div>

                            <div class="col-span-1 col-start-3 row-start-3 pl-14"> <p>Fecha Inicio</p> </div>
                            <div class="col-span-1 row-start-3 md:col-span-1">
                                <DatePicker selected={startDate} onChange={date => setStartDate(date)} />
                            </div>

                            <div class="col-span-1 col-start-5 row-start-3 pl-14"> <p>Fecha Fin</p> </div>
                            <div class="col-span-1 row-start-3 md:col-span-1">
                                <DatePicker selected={finishtDate} onChange={date => setFinishDate(date)} />
                            </div>
                            
                            <div class="col-span-2 col-start-5 row-start-4">
                                <a href="/DocumentosEmpresa"
                                className="w-90 text-center flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                Adjuntar Documentación de Empresa
                                </a>
                            </div>
                            
                        </div>

                    </div>
                    
                    <div class="h-8"></div>

                    {/** Parte inferior */}

                    <TablaTrabajadores datos = {dataTablaGeneral} url = {url}/>               
                </div>
            </div>
        </div>
    )
}
