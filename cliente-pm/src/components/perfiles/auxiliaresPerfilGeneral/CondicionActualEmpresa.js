import React from 'react';

const CondicionActualEmpresa = props => {
    return (
        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">

            <p class="text-2xl leading-tight mx-8 text-center md:text-left md:ml-16">
                Documentos Vigentes Empresa {props.datos}
            </p>

        </div>
    );
};

export default CondicionActualEmpresa;