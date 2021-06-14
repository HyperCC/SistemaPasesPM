import React from 'react'
import { useHistory } from 'react-router-dom';

export const InformacionPase = props => {
    const history = useHistory();

    return (

        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
            <p class="text-2xl leading-tight mx-8 text-center md:text-left md:ml-16">
                Información general - Pase {props.tituloPase}
            </p>

            <div class="grid md:grid-cols-7 grid-cols-3 gap-6 mt-6 mx-8 mb-2 md:mb-0">
                <div class="col-span-3">
                    <div class="grid grid-cols-3 gap-6">

                        {props._dataPaseGeneral.tipo == 'USOMUELLE' || props._dataPaseGeneral.tipo == 'TRIPULANTE' ? null :
                            <div>Área</div>
                        }
                        {props._dataPaseGeneral.tipo == 'USOMUELLE' || props._dataPaseGeneral.tipo == 'TRIPULANTE' ? null :
                            <div class="col-span-2">
                                <div class="py-1 px-3 bg-gray-100  outline-none rounded-md w-full" >
                                    {props._dataPaseGeneral.area}
                                </div>
                            </div>
                        }

                        <div>Tipo</div>
                        <div class="col-span-2">
                            <div class="py-1 px-3 bg-gray-100  outline-none rounded-md w-full" >
                                {props._dataPaseGeneral.tipo}
                            </div>
                        </div>

                        <div>Estado</div>
                        <div class="col-span-2">
                            <div class="py-1 px-3 bg-gray-100 outline-none rounded-md w-full">
                                {props._dataPaseGeneral.estado}
                            </div>
                        </div>
                    </div>
                </div>

                <div class="md:col-span-4 col-span-3">
                    <div class="grid md:grid-cols-4 grid-cols-3 gap-6">
                        <div>Motivo</div>
                        <div class="md:col-span-3 col-span-2">
                            <div class="bg-gray-100 w-full h-full p-2 rounded-md outline-none">
                                {props._dataPaseGeneral.motivo}
                            </div>
                        </div>
                    </div>

                    <div class="grid sm:grid-cols-4 grid-cols-2 gap-6 mt-6">
                        <div>Fecha Inicio</div>
                        <div class="bg-gray-100 rounded-md px-2">
                            {props._dataPaseGeneral.fechaInicio}
                        </div>

                        <div>Fecha Término</div>
                        <div class="bg-gray-100 rounded-md px-2">
                            {props._dataPaseGeneral.fechaTermino}
                        </div>
                    </div>
                </div>
            </div>

        </div>
    );
};