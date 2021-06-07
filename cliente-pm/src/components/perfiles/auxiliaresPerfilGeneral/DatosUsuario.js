import React, { useState } from 'react';
import DatePicker from "react-datepicker";

// tabla con los datos del perfil del usuario, datos del solicitante o filtros de los admins
const DatosUsuario = props => {

    // variables para las fechas
    const [startDate, setStartDate] = useState(null);
    const [finishtDate, setFinishDate] = useState(null);

    // modificar las fechas con useState del pase
    const cambiarFechaInicio = (date) => {
        setStartDate(date);
    };
    const cambiarFechaTermino = (date) => {
        setFinishDate(date);
    };

    // ajuste del titulo
    const currentRol = props.datos.rol;
    const rolFormateado = currentRol == 'ADMIN' ?
        'Administrador'
        : currentRol == 'SOLICITANTE' ?
            'Perfil General'
            : currentRol == 'HSEQ' ?
                'HSEQ'
                : currentRol == 'JEFEO_PERACIONES' ?
                    'Jefe de Operaciones'
                    : currentRol == 'CONTACTO' ?
                        'Contacto'
                        : currentRol == 'OPIP' ?
                            'OPIP'
                            : 'Rol no Reconocido';

    return (
        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">

            <div class="text-2xl leading-tight mx-8 text-center md:text-left md:ml-16">
                Perfil {rolFormateado} - {currentRol == 'SOLICITANTE' ? 'Informaicon General' : 'Filtros'}
            </div>

            {currentRol == 'SOLICITANTE' ?
                // perfil del solicitante
                <div class="grid grid-cols-2 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">
                    <div> <p>Nombre Completo:</p> </div>
                    <div class="col-span-1 md:col-span-3"> <p>{props.datos.nombreCompleto}</p> </div>

                    <div> <p>Rut:</p> </div>
                    <div class="col-span-1 md:col-span-3"> <p>{props.datos.rut}</p> </div>

                    <div> <p>Nombre Empresa:</p> </div>
                    <div class="col-span-1 md:col-span-3"> <p>{props.datos.nombreEmpresa}</p> </div>
                </div>

                // perfil de los nosolicitantes
                : <div class="grid grid-cols-2 gap-4 md:grid-cols-8 mt-6 mx-8 mb-2 md:mb-0">
                    <div class="md:text-right">Fecha Inicio</div>
                    <DatePicker name="FechaInicio" dateFormat="dd/MM/yyyy" autoComplete="off"
                        selected={startDate} value={startDate} onChange={date => cambiarFechaInicio(date)} />

                    <div class="md:text-right">Fecha Término</div>
                    <DatePicker name="FechaTermino" dateFormat="dd/MM/yyyy" autoComplete="off"
                        selected={finishtDate} value={finishtDate} onChange={date => cambiarFechaTermino(date)} />

                    <div class="md:text-right">Tipo</div>
                    <div>
                        <div class="relative inline-flex">
                            <svg class="w-2 h-2 absolute top-4 right-4 pointer-events-none" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 412 232">
                                <path d="M206 171.144L42.678 7.822c-9.763-9.763-25.592-9.763-35.355 0-9.763 9.764-9.763 25.592 0 35.355l181 181c4.88 4.882 11.279 7.323 17.677 7.323s12.796-2.441 17.678-7.322l181-181c9.763-9.764 9.763-25.592 0-35.355-9.763-9.763-25.592-9.763-35.355 0L206 171.144z" fill="#648299" fill-rule="nonzero" /></svg>
                            <select name="Tipo" class="border border-gray-300 rounded-full text-gray-600 p-2 bg-gray-100 hover:border-gray-400 focus:outline-none appearance-none">
                                <option>Ninguno</option>
                                <option value='Visita'>Visita</option>
                                <option value='Contratista'>Contratista</option>
                                <option value='Proveedor'>Proveedor</option>
                                <option value='Tripulante'>Tripulante</option>
                                <option value='Uso de Muelle'>Uso de Muelle</option>
                            </select>
                        </div>
                    </div>

                    <div class="md:text-right">Estado</div>
                    <div>
                        <div class="relative inline-flex">
                            <svg class="w-2 h-2 absolute top-0 right-0 m-4 pointer-events-none" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 412 232">
                                <path d="M206 171.144L42.678 7.822c-9.763-9.763-25.592-9.763-35.355 0-9.763 9.764-9.763 25.592 0 35.355l181 181c4.88 4.882 11.279 7.323 17.677 7.323s12.796-2.441 17.678-7.322l181-181c9.763-9.764 9.763-25.592 0-35.355-9.763-9.763-25.592-9.763-35.355 0L206 171.144z" fill="#648299" fill-rule="nonzero" /></svg>
                            <select name="Estado" class="border border-gray-300 rounded-full text-gray-600 p-2 bg-gray-100 hover:border-gray-400 focus:outline-none appearance-none">
                                <option>Ninguno</option>
                                <option value='Aprobado'>Aprobado</option>
                                <option value='Rechazado'>Rechazado</option>
                                <option value='Pendiente'>Pendiente</option>
                                <option value='Finalizado'>Finalizado</option>
                            </select>
                        </div>
                    </div>
                </div>
            }
        </div>
    );
}

export default DatosUsuario;