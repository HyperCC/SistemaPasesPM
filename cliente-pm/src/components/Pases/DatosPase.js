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

            <div class="grid grid-cols-7 gap-6 md:grid-cols-6 mt-6 mx-8 mb-2 md:mb-0">
                <div class="col-span-1 col-start-1 row-start-1"> <p>Área</p> </div>
                <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                    <p>
                        <div class="relative inline-flex">
                            <svg class="w-2 h-2 absolute top-0 right-0 m-4 pointer-events-none" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 412 232"><path d="M206 171.144L42.678 7.822c-9.763-9.763-25.592-9.763-35.355 0-9.763 9.764-9.763 25.592 0 35.355l181 181c4.88 4.882 11.279 7.323 17.677 7.323s12.796-2.441 17.678-7.322l181-181c9.763-9.764 9.763-25.592 0-35.355-9.763-9.763-25.592-9.763-35.355 0L206 171.144z" fill="#648299" fill-rule="nonzero" /></svg>
                            <select name="Area" value={props._dataPaseGeneral.Area} onChange={data => cambiarValoresInput(data)} class="border border-gray-300 rounded-full text-gray-600 h-10 pl-5 pr-10 bg-white hover:border-gray-400 focus:outline-none appearance-none">
                                <option>Selecciones el Área</option>
                                <option value='Informatica'>Informatica</option>
                            </select>
                        </div>
                    </p>
                </div>

                <div class="col-span-1 row-start-2"> <p>Rut Empresa</p> </div>
                <div class="col-span-1 md:col-span-1 row-start-2">
                    <input type="text" name="RutEmpresa" value={props._dataPaseGeneral.RutEmpresa}
                        onChange={data => cambiarValoresInput(data)} class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                </div>

                <div class="col-span-1 row-start-3"> <p>Nombre Empresa</p> </div>
                <div class="col-span-1 row-start-3 md:col-span-1">
                    <input type="text" name="NombreEmpresa" value={props._dataPaseGeneral.NombreEmpresa}
                        onChange={data => cambiarValoresInput(data)} class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                </div>

                <div class="col-span-1 row-span-2 col-start-3 row-start-1 pl-14"><p>Motivo visita</p></div>
                <div class="col-span-3 row-span-2 col-start-4 row-start-1"><textarea name="Motivo" type="range"
                    placeholder="range...." class="border w-full app border-gray-300 p-2 my-2 rounded-md focus:outline-none focus:ring-2 ring-azul-pm"
                    value={props._dataPaseGeneral.Motivo} onChangeCapture={data => cambiarValoresInput(data)}></textarea></div>

                <div class="col-span-1 col-start-3 row-start-3 pl-14"> <p>Fecha Inicio</p> </div>
                <div class="col-span-1 row-start-3 md:col-span-1">
                    <DatePicker name="FechaInicio" dateFormat="dd/MM/yyyy" autoComplete="off"
                        selected={startDate} value={startDate} onChange={date => cambiarFechaInicio(date)} />
                </div>

                <div class="col-span-1 col-start-5 row-start-3 pl-14"> <p>Fecha Fin</p> </div>
                <div class="col-span-1 row-start-3 md:col-span-1">
                    <DatePicker name="FechaTermino" dateFormat="dd/MM/yyyy" autoComplete="off"
                        selected={finishtDate} value={finishtDate} onChange={date => cambiarFechaTermino(date)} />
                </div>

            </div>
        </div>
    );
};
