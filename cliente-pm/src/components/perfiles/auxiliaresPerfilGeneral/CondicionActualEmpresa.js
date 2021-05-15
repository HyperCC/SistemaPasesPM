import React, { useState } from 'react';

// modulo con los documentos validos a la fecha para le empresa del usuario
const CondicionActualEmpresa = props => {

    // controla las columnas grises y blancas
    var registroColor = 1;

    const titulosDocumentosReales = {
        "CronogramaTrabajo": "Cronograma de Trabajo",
        "CertificadoMutualidad": "Certificado de Mutualidad",
        "CertificadoAccidentabilidad": "Certificado de Accidentabilidad",
        "ReglamentoInterno": "Reglamento Interno",

        "MatrizRiesgos": "Matriz de Riesgos",
        "ProcedimientoTrabajoSeguro": "Procedimiento Trabajo Seguro",
        "ProgramaPrevencionRiesgos": "Programa Prevención de Riesgos",
        "HdsSustanciasPelgrosas": "HDS Sustancias Pelgrosas"
    };

    return (
        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">

            <p class="text-2xl leading-tight mx-8 text-center md:text-left md:ml-16">
                Documentos Vigentes de Empresa {props.empresa}
            </p>

            <div class="mx-0 md:mx-8">
                <div class="grid md:grid-cols-2 grid-cols-1 mt-6 text-center md:gap-4 gap-2 min-w-full leading-normal">

                    {/* TABLA DE GENERAL Y LEGAL */}
                    <div class="overflow-x-auto">
                        <div class="inline-block min-w-full overflow-hidden shadow-md rounded-lg">
                            <table class="min-w-full leading-normal">
                                <thead>
                                    {/* HEADERS PARA LA TABLA */}
                                    <tr class="bg-azul-pm select-none text-sm uppercase text-gray-100 text-center border-b border-gray-200">
                                        <th></th>
                                        <th scope="col" class="px-5 py-3 font-normal">
                                            General y Legal
                                        </th>
                                        <th scope="col" class="px-5 py-3 font-normal">
                                            Fecha Vencimiento
                                        </th>
                                    </tr>
                                </thead>

                                <tbody>
                                    {props.documentos.map((value, index) => {

                                        {
                                            if (value.Titulo === "CronogramaTrabajo" ||
                                                value.Titulo === "CertificadoMutualidad" ||
                                                value.Titulo === "CertificadoAccidentabilidad" ||
                                                value.Titulo === "ReglamentoInterno") {

                                                {/* CAMBIAR DE COLOR EL REGISTRO SEGUN LOS PARES */ }
                                                { registroColor += 1 }

                                                return <tr key={index} class={registroColor % 2 == 0 ? "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap"
                                                    : "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap bg-gray-100"} >

                                                    <td class="p-4">
                                                        <i class="bi bi-check-circle-fill text-green-500 mr-2"></i>
                                                    </td>
                                                    <td class="py-4">
                                                        {value.Titulo}
                                                    </td>
                                                    <td class="p-4">
                                                        {value.FechaVencimiento}
                                                    </td>
                                                </tr>
                                            }
                                        }
                                    })}
                                </tbody>
                            </table>
                        </div>
                    </div>

                    {/* TABLA DE GESTION DE RIESGOS */}
                    <div class="overflow-x-auto">
                        <div class="inline-block min-w-full overflow-hidden shadow-md rounded-lg">
                            <table class="min-w-full leading-normal">
                                <thead>
                                    {/* HEADERS PARA LA TABLA */}
                                    <tr class="bg-azul-pm select-none text-sm uppercase text-gray-100 text-center border-b border-gray-200">
                                        <th></th>
                                        <th scope="col" class="px-5 py-3 font-normal">
                                            Gestion de Riesgos
                                        </th>
                                        <th scope="col" class="px-5 py-3 font-normal">
                                            Fecha Vencimiento
                                        </th>
                                    </tr>
                                </thead>

                                <tbody>
                                    {props.documentos.map((value, index) => {

                                        {
                                            if (value.Titulo === "MatrizRiesgos" ||
                                                value.Titulo === "ProcedimientoTrabajoSeguro" ||
                                                value.Titulo === "ProgramaPrevencionRiesgos" ||
                                                value.Titulo === "HdsSustanciasPelgrosas") {

                                                {/* CAMBIAR DE COLOR EL REGISTRO SEGUN LOS PARES */ }
                                                { registroColor += 1 }

                                                return <tr key={index} class={registroColor % 2 == 0 ? "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap"
                                                    : "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap bg-gray-100"} >

                                                    <td class="p-4">
                                                        <i class="bi bi-x-circle-fill text-red-500 mr-2"></i>
                                                    </td>
                                                    <td class="py-4">
                                                        {value.Titulo}
                                                    </td>
                                                    <td class="p-4">
                                                        {value.FechaVencimiento}
                                                    </td>
                                                </tr>
                                            }
                                        }
                                    })}
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>

        </div >
    );
};

export default CondicionActualEmpresa;