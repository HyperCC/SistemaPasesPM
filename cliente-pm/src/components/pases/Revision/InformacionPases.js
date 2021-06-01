import React from 'react'
import { useHistory } from 'react-router-dom';

export const InformacionPase = props => {
    const history = useHistory();

    return(

        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
            <p class="text-2xl leading-tight mx-8 text-center md:text-left md:ml-16">
                Información general - Pase {props.tituloPase}
            </p>

            <div class="grid grid-cols-7 gap-6 md:grid-cols-6 mt-6 mx-8 mb-2 md:mb-0">
                <div class="col-span-1 col-start-1 row-start-1"> <p>Área</p> </div>
                <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                    <p>
                        {props._dataPaseGeneral.area}
                    </p>
                </div>

                <div class="col-span-1 row-start-2"> <p>Tipo</p> </div>
                <div class="col-span-1 md:col-span-1 row-start-2">
                    <p>
                        {props._dataPaseGeneral.tipo}
                    </p>
                </div>

                <div class="col-span-1 row-start-3"> <p>Estado</p> </div>
                <div class="col-span-1 row-start-3 md:col-span-1">
                    <p>
                        {props._dataPaseGeneral.estado}
                    </p>
                </div>

                <div class="col-span-1 row-span-2 col-start-3 row-start-1 pl-14"><p>Motivo visita</p></div>
                <div class="col-span-3 row-span-2 col-start-4 row-start-1">
                    <p>{props._dataPaseGeneral.motivo}</p>
                </div>

                <div class="col-span-1 col-start-3 row-start-3 pl-14"> <p>Fecha Inicio</p> </div>
                <div class="col-span-1 row-start-3 md:col-span-1">
                    {props._dataPaseGeneral.fechaInicio}
                </div>

                <div class="col-span-1 col-start-5 row-start-3 pl-14"> <p>Fecha Fin</p> </div>
                <div class="col-span-1 row-start-3 md:col-span-1">
                    <p>{props._dataPaseGeneral.fechaTermino}</p>
                </div>

            </div>
        </div>
    )

}