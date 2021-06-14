import React, { useState, useEffect } from 'react';
import TablaPasesAprob from './TablaPasesAprob';

const AreaContacto = () => {

    const [dataUsuario, setDataUsuario] = useState({});

    {/** datos solo para probar */}

    const dataPasesGeneral = [
        {
            FechaInicio: '05/04/2021',
            FechaTermino: '13/04/2021',
            Motivo: 'Entrevista para desarrollo',
            Area: 'HSEQ',
            Tipo: 'Visita',
            Estado: 'FINALIZADO'
        },
        {
            FechaInicio: '12/04/2021',
            FechaTermino: '18/04/2021',
            Motivo: 'Muestra de avance',
            Area: 'Informatica',
            Tipo: 'Tripulante',
            Estado: 'REVISION'
        },
        {
            FechaInicio: '20/04/2021',
            FechaTermino: '25/04/2021',
            Motivo: 'Muestra de avance',
            Area: 'Informatica',
            Tipo: 'Visita',
            Estado: 'APROBADO'
        },
        {
            FechaInicio: '28/04/2021',
            FechaTermino: '30/04/2021',
            Motivo: 'Muestra de avance',
            Area: 'Informatica',
            Tipo: 'Uso muelle',
            Estado: 'FINALIZADO'
        }
    ];

    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">

                    <TablaPasesAprob soloPases={dataPasesGeneral} />

                </div>
            </div>
        </div>
    );

}

export default AreaContacto;