import React, { useState } from 'react';
import { DatosPase } from './DatosPase';
import { TablaTrabajadores } from './TablaTrabajadores';

export const Proveedor = (props) => {

    //Datos generales del pase
    const URL = '/SolicitudProveedor';
    const TITULO = 'Proveedor';

    // datos para enviar a la API
    const [dataPaseGeneral, setDataPaseGeneral] = useState({
        Area: null,
        RutEmpresa: null,
        NombreEmpresa: null,
        Motivo: null,
        ServicioAdjudicado: null,
        FechaInicio: null,
        FechaTermino: null
    });

    const dataTablaGeneral = [
        {
            Nombre: 'Daniel Castillo Vasquez',
            RutPasaporte: '11.222.333-4',
            Nacionalidad: 'Chilena'
        },
        {
            Nombre: 'Mariá Angelica Domínguez Soto',
            RutPasaporte: '55.666.777-8',
            Nacionalidad: 'Chilena'
        },
        {
            Nombre: 'Nombre prueba',
            RutPasaporte: '11.111.111-1',
            Nacionalidad: 'Chilena'
        },
        {
            Nombre: 'Nombre prueba 2',
            RutPasaporte: '22.222.222-2',
            Nacionalidad: 'Chilena'
        }
    ];

    // asignar nuevos valores al state del registro
    const ingresarValoresMemoria = (name, date) => {
        setDataPaseGeneral(anterior => ({
            ...anterior, // mantener lo que existe antes
            [name]: date // solo cambiar el input mapeado
        }));
    };


    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">

                    {/** Parte superior de la vista */}
                    <DatosPase _dataPaseGeneral={dataPaseGeneral} tituloPase={TITULO}
                        _ingresoValoresMemoria={ingresarValoresMemoria} />

                    <div class="h-8"></div>

                    {/** Parte inferior tabla de personas */}
                    <TablaTrabajadores datos={dataTablaGeneral} url={URL} />
                </div>
            </div>
        </div>
    );
};