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
            'Solicitante'
            : currentRol == 'HSEQ' ?
                'HSEQ'
                : currentRol == 'JEFE_OPERACIONES' ?
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
                <div class="grid grid-cols-3 gap-4 md:grid-cols-6 mt-6 mx-8 mb-2 md:mb-0">
                    <div> <p>Nombre Completo:</p> </div>
                    <div class="col-span-2 py-1 px-3 bg-gray-100 capitalize rounded-md">
                        <p>{props.datos.nombreCompleto}</p>
                    </div>

                    <div> <p>Nombre Empresa:</p> </div>
                    <div class="col-span-2 py-1 px-3 bg-gray-100 uppercase rounded-md">
                        <p>{props.datos.nombreEmpresa}</p>
                    </div>

                    <div> <p>Rut:</p> </div>
                    <div class="col-span-2 py-1 px-3 bg-gray-100 rounded-md">
                        <p>{props.datos.rut}</p>
                    </div>
                </div>

                // perfil de los nosolicitantes
                : <div class="grid grid-cols-2 gap-6 md:grid-cols-8 mt-6 mx-8 mb-2 md:mb-0">
                    <div>Fecha Inicio</div>
                    <div class="bg-gray-100 border border-gray-300 rounded-md outline-none">
                        <DatePicker name="FechaInicio" dateFormat="dd/MM/yyyy" autoComplete="off"
                            selected={startDate} value={startDate} onChange={date => cambiarFechaInicio(date)} />
                    </div>

                    <div>Fecha Término</div>
                    <div class="bg-gray-100 border border-gray-300 rounded-md outline-none">
                        <DatePicker name="FechaTermino" dateFormat="dd/MM/yyyy" autoComplete="off"
                            selected={finishtDate} value={finishtDate} onChange={date => cambiarFechaTermino(date)} />
                    </div>

                    <div class="col-span-2 grid grid-cols-3 gap-6">
                        <div>Tipo</div>
                        <div class="flex col-span-2">
                            <select name="Tipo" class="bg-gray-100 p-2 rounded-full outline-none w-full border border-gray-300">
                                <option>Ninguno</option>
                                <option value='Visita'>Visita</option>
                                <option value='Contratista'>Contratista</option>
                                <option value='Proveedor'>Proveedor</option>
                                <option value='Tripulante'>Tripulante</option>
                                <option value='Uso de Muelle'>Uso de Muelle</option>
                            </select>
                        </div>
                    </div>

                    <div class="col-span-2 grid grid-cols-3 gap-6">
                        <div>Estado</div>
                        <div class="flex col-span-2">
                            <select name="Estado" class="bg-gray-100 p-2 rounded-full outline-none w-full border border-gray-300">
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