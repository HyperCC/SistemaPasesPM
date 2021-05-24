import React, { useState } from 'react';
import CondicionActualEmpresa from './auxiliaresPerfilGeneral/CondicionActualEmpresa';
import DatosUsuario from './auxiliaresPerfilGeneral/DatosUsuario';
import TablaPases from './auxiliaresPerfilGeneral/TablaPases';
import { perfilUsuario } from '../../actions/UsuarioAction';
import { LanzarNoritificaciones } from '../avisos/LanzarNotificaciones';


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


    // atributos para el perfil del usuario
    const [dataUsuario, setDataUsuario] = useState({});
    // codigo actual de la notificacion a mostrar
    const [currentNotification, setCurrentNotification] = useState('none');


    // asignar nuevos valores al state del login
    function ingresarValoresMemoria(valorInput) {
        setDataUsuario(valorInput);
    };


    // boton para enviar el formulario
    const recolectarDatosPerfil = () => {

        perfilUsuario().then(response => {

            if (typeof response !== 'undefined') {

                // si se reciben errores
                if (typeof response.data.errores !== 'undefined') {
                    console.log(response.data.errores.mensaje);

                    console.log('el tipo de error: ', response.data.errores.tipoError);
                    //setCurrentNotification(response.data.errores.tipoError);
                    //console.log('TIPO ACTUAL DE NOTIFICACION ', currentNotification);

                    //if (typeof response.data.errores.listaErrores !== 'undefined')
                    //setCurrentCamposInvalidos(response.data.errores.listaErrores);

                    // si toda la operacion salio ok
                } else {
                    window.localStorage.setItem('mensaje_success', 'exi-le0000');
                    window.localStorage.setItem('mensaje_success_showed', false);
                    console.log('se tomaron odos los datos');
                    ingresarValoresMemoria(response.data);
                    //setCurrentNotification('exi-le0000');
                }

                // si no hay conexion con el servidor pero si al cliente.
            } else {
                setCurrentNotification('err-nhc000');
            }

        });
    };

    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">

                    {recolectarDatosPerfil()}
                    <LanzarNoritificaciones codigo={currentNotification} />

                    <DatosUsuario datos={dataUsuario} />
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