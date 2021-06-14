import React, { useState, useEffect } from 'react';
//import CondicionActualEmpresa from './auxiliaresPerfilGeneral/CondicionActualEmpresa';
import DatosUsuario from './auxiliaresPerfilGeneral/DatosUsuario';
import TablaPases from './auxiliaresPerfilGeneral/TablaPases';
import { perfilUsuario } from '../../actions/UsuarioAction';
import { LanzarNoritificaciones } from '../avisos/LanzarNotificaciones';
import { useStateValue } from "../../contexto/store";
import { TablaAllUsuarios } from '../pases/TablaAllUsuarios';


// vista principal para el perfil general
const PerfilGeneral = () => {
    const [{ sesionUsuario }, dispatch] = useStateValue();
    const rolCuenta = JSON.parse(window.localStorage.getItem('data_current_usuario')).rol;

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

    // atributos para el perfil del usuario
    const [dataUsuario, setDataUsuario] = useState({});
    const [pasesUsuario, setPasesUsuario] = useState({});

    // codigo actual de la notificacion a mostrar
    const [currentNotification, setCurrentNotification] = useState('none');

    /*useEffect(() => {
        console.log('datos en use effect: ', pasesUsuario);
    }, [pasesUsuario]);*/

    // obtener los datos desde el perfil directamente.
    useEffect(() => {
        setCurrentNotification('inf-cdp000');
        setDataUsuario(JSON.parse(window.localStorage.getItem('data_current_usuario')));

        perfilUsuario(dispatch, rolCuenta).then(response => {

            console.log('probando si hay conexion');

            // si no hay respuesta del servidor
            if (typeof response !== 'undefined') {

                // si se reciben errores
                if (typeof response.data.errores !== 'undefined') {
                    console.log(response.data.errores.mensaje);
                    console.log('el tipo de error: ', response.data.errores.tipoError);
                    setCurrentNotification('err-pnrkv0');

                    // si toda la operacion salio ok
                } else {
                    setPasesUsuario(response.data);
                    //ingresarValoresMemoria(response.data);
                    console.log('el objeto obtenido correctamente es: ', response.data);
                    setCurrentNotification('exi-pe0000');
                    console.log('los valores en memoria son: ', dataUsuario);
                }

            } else {
                console.log('no hay conexion con la api');
                setCurrentNotification('err-nhc000');
            }
        });
    }, []);


    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto pb-8">
                <div class="sm:px-8 px-4">

                    <LanzarNoritificaciones codigo={currentNotification} />

                    <DatosUsuario datos={dataUsuario} />
                    <div class="h-8"></div>

                    {/*
                    <CondicionActualEmpresa documentos={dataDocumentosEmpresaPerfil} empresa={dataUsuario.NombreEmpresa} />
                    <div class="h-8"></div>
                    */}

                    {rolCuenta == 'ADMIN' ?
                        <TablaAllUsuarios datos={pasesUsuario.usuariosAll} _setCurrentNotification={setCurrentNotification} />
                        : <TablaPases currentRol={dataUsuario.rol} soloPases={pasesUsuario.pasesRel} />
                    }
                </div>
            </div>
        </div>
    );
}

export default PerfilGeneral;