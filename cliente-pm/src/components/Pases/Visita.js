import React, { useState } from 'react';
import { DatosPase } from './DatosPase';
import { TablaTrabajadores } from './TablaTrabajadores';

export const Visita = (props) => {

    //Datos generales del pase
    const url = '/SolicitudVisita';

    const dataPaseGeneral = {
        RutEmpresa: '2.333.444-5',
        NombreEmpresa: 'Nortek SPA'
    };

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
    

    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">

                    {/** Parte superior de la vista */}
                    
                    <DatosPase datos={dataPaseGeneral} />
                    
                    <div class="h-8"></div>
                    {/** Parte inferior tabla de personas */}
                    
                    <TablaTrabajadores datos={dataTablaGeneral} url = {url} />
                </div>
            </div>
        </div>
    )
}
