import React from 'react';

const DatosUsuario = props => {
    return (
        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
            
            <p class="text-2xl leading-tight mx-8 text-center md:text-left md:ml-16">
                Perfil General - Información General
            </p>

            <div class="grid grid-cols-2 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">
                <div> <p>Nombre Completo:</p> </div>
                <div class="col-span-1 md:col-span-3"> <p>{props.datos.nombreCompleto}</p> </div>

                <div> <p>Rut:</p> </div>
                <div class="col-span-1 md:col-span-3"> <p>{props.datos.rut}</p> </div>

                <div> <p>Nombre Empresa:</p> </div>
                <div class="col-span-1 md:col-span-3"> <p>{props.datos.nombreEmpresa}</p> </div>
            </div>
        </div>
    );
}

export default DatosUsuario;