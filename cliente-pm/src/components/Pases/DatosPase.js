import React, { useState } from 'react';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import moment from 'moment';

export const DatosPase = props => {

    // variables para las fechas
    const [startDate, setStartDate] = useState(null);
    const [finishtDate, setFinishDate] = useState(null);

    // modificar las fechas con useState del pase
    const cambiarFechaInicio = (date) => {
        setStartDate(date);
        props._ingresoValoresMemoria('FechaInicio', moment(date.toString()).format("DD/MM/YYYY"));
    };
    const cambiarFechaTermino = (date) => {
        setFinishDate(date);
        props._ingresoValoresMemoria('FechaTermino', moment(date.toString()).format("DD/MM/YYYY"));
    };

    // modificar el resto de valores en inputs
    const cambiarValoresInput = (inputValue) => {
        const { name, value } = inputValue.target;
        props._ingresoValoresMemoria(name, value);
    }


    return (
        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
            <p class="text-2xl leading-tight mx-8 text-center md:text-left md:ml-16">
                Información general - Pase {props.tituloPase}
            </p>

            <div class="grid md:grid-cols-7 grid-cols-3 gap-6 mt-6 mx-8 mb-2 md:mb-0">
                <div class="col-span-3">
                    <div class="grid grid-cols-3 gap-6">

                        {props.tituloPase == 'Uso de Muelle' || props.tituloPase == 'Tripulante' ? null :
                            <div>Área</div>
                        }
                        {props.tituloPase == 'Uso de Muelle' || props.tituloPase == 'Tripulante' ? null :
                            <div class="col-span-2 flex">
                                <select name="Area" value={props._dataPaseGeneral.Area} onChange={data => cambiarValoresInput(data)}
                                    class="bg-gray-100 p-2 rounded-full outline-none w-full border border-gray-300">
                                    <option>Seleccionar Área</option>
                                    <option value="Contabilidad">CONTABILIDAD</option>
                                    <option value="Ing. y Mantencion">ING. Y MANTENCIÓN</option>
                                    <option value="Operaciones">OPERACIONES</option>
                                    <option value="Finanzas">FINANZAS</option>
                                    <option value="Informatica">INFORMÁTICA</option>
                                    <option value="Comercial">COMERCIAL</option>
                                    <option value="Administracion">ADMINISTRACIÓN</option>
                                    <option value="Hseq">HSEQ</option>
                                    <option value="Personas">PERSONAS</option>
                                    <option value="Proyectos">PROYECTOS</option>
                                    <option value="Medio ambiente">MEDIO AMBIENTE</option>
                                    <option value="Mecanica pm">MECANICA PM</option>
                                    <option value="Electro-control pm">ELECTRO-CONTROL PM</option>
                                </select>
                            </div>
                        }

                        <div>Rut Empresa</div>
                        <div class="col-span-2">
                            <input type="text" name="RutEmpresa" value={props._dataPaseGeneral.RutEmpresa}
                                onChange={data => cambiarValoresInput(data)} class="border py-1 px-3 border-gray-300 bg-gray-100  outline-none rounded-md w-full" />
                        </div>

                        <div>Nombre Empresa</div>
                        <div class="col-span-2">
                            <input type="text" name="NombreEmpresa" value={props._dataPaseGeneral.NombreEmpresa}
                                onChange={data => cambiarValoresInput(data)} class="border py-1 px-3 border-gray-300 bg-gray-100 outline-none rounded-md w-full" />
                        </div>
                    </div>
                </div>

                <div class="md:col-span-4 col-span-3">
                    <div class="grid md:grid-cols-4 grid-cols-3 gap-6">
                        <div>Motivo</div>
                        <div class="md:col-span-3 col-span-2">
                            <textarea name="Motivo" type="range"
                                class="bg-gray-100 border w-full h-full border-gray-300 p-2 rounded-md outline-none"
                                value={props._dataPaseGeneral.Motivo} onChangeCapture={data => cambiarValoresInput(data)}>
                            </textarea>
                        </div>
                    </div>

                    <div class="grid sm:grid-cols-4 grid-cols-2 gap-6 mt-6">
                        <div>Fecha Inicio</div>
                        <div class="bg-gray-100 border border-gray-300 rounded-md">
                            <DatePicker name="FechaInicio" dateFormat="dd/MM/yyyy" autoComplete="off"
                                selected={startDate} value={startDate} onChange={date => cambiarFechaInicio(date)} />
                        </div>

                        <div>Fecha Término</div>
                        <div class="bg-gray-100 border border-gray-300 rounded-md">
                            <DatePicker name="FechaTermino" dateFormat="dd/MM/yyyy" autoComplete="off"
                                selected={finishtDate} value={finishtDate} onChange={date => cambiarFechaTermino(date)} />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    );
};
