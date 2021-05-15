import React from 'react';
import CondicionActualEmpresa from './auxiliaresPerfilGeneral/CondicionActualEmpresa';
import DatosUsuario from './auxiliaresPerfilGeneral/DatosUsuario';
import TablaPases from './auxiliaresPerfilGeneral/TablaPases';

// vista principal para el perfil general
const PerfilGeneral = () => {

    const dataUsuarioGeneral = {
        NombreCompleto: 'Camilo Cortes Egaña',
        Rut: '2.333.444-5',
        NombreEmpresa: 'Nortek SPA'
    };

    //TODO: agregar datos de prueba y formatearlos
    const dataDocumentosEmpresaPerfil = [
        {
            Titulo: 'CronogramaTrabajo',
            FechaVencimiento: '12/03/2021'
        },
        {
            Titulo: 'CertificadoMutualidad',
            FechaVencimiento: '12/03/2021'
        },
        {
            Titulo: 'CertificadoAccidentabilidad',
            FechaVencimiento: '12/03/2021'
        },
        {
            Titulo: 'ReglamentoInterno',
            FechaVencimiento: '12/03/2021'
        },
        {
            Titulo: 'MatrizRiesgos',
            FechaVencimiento: '12/03/2021'
        },
        {
            Titulo: 'ProcedimientoTrabajoSeguro',
            FechaVencimiento: '12/03/2021'
        },
        {
            Titulo: 'ProcedimientoTrabajoSeguro',
            FechaVencimiento: '12/03/2021'
        },
        {
            Titulo: 'ProcedimientoTrabajoSeguro',
            FechaVencimiento: '12/03/2021'
        },
        {
            Titulo: 'ProgramaPrevencionRiesgos',
            FechaVencimiento: '12/03/2021'
        },
        {
            Titulo: 'HdsSustanciasPelgrosas',
            FechaVencimiento: '12/03/2021'
        },
        {
            Titulo: 'HdsSustanciasPelgrosas',
            FechaVencimiento: '12/03/2021'
        },
        {
            Titulo: 'HdsSustanciasPelgrosas',
            FechaVencimiento: '12/03/2021'
        },
    ];

    const dataPasesGeneral = [
        {
            FechaInicio: '05/04/2021',
            FechaTermino: '23/04/2021',
            Motivo: 'Entrevista para desarrollo',
            Area: 'HSEQ',
            Tipo: 'Visita',
            Estado: 'Finalizado'
        },
        {
            FechaInicio: '28/04/2021',
            FechaTermino: '28/04/2021',
            Motivo: 'Muestra de avance',
            Area: 'Informatica',
            Tipo: 'Visita',
            Estado: 'Revision'
        },
        {
            FechaInicio: '28/04/2021',
            FechaTermino: '28/04/2021',
            Motivo: 'Muestra de avance',
            Area: 'Informatica',
            Tipo: 'Visita',
            Estado: 'Aprobado'
        },
        {
            FechaInicio: '28/04/2021',
            FechaTermino: '28/04/2021',
            Motivo: 'Muestra de avance',
            Area: 'Informatica',
            Tipo: 'Visita',
            Estado: 'Rechazado'
        }
    ];

    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">

                    <DatosUsuario datos={dataUsuarioGeneral} />
                    <div class="h-8"></div>

                    <CondicionActualEmpresa documentos={dataDocumentosEmpresaPerfil} empresa={dataUsuarioGeneral.NombreEmpresa} />
                    <div class="h-8"></div>

                    <TablaPases datos={dataPasesGeneral} />
                </div>
            </div>
        </div>
    );
}

export default PerfilGeneral;