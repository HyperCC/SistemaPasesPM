import React from 'react'

const PerfilGeneral = () => {
    return (
        <div className="container mx-auto px-4 sm:px-8 bg-white rounded px-4 flex flex-col justify-between leading-normal shadow">
            <div className="py-8">
                <h2 class="text-2xl leading-tight">
                    Perfil General - Información General
                </h2>

                <div className="py-4">
                    <h3 className="py-2">
                        Nombre Completo: 
                    </h3>
                    <h3 className="py-2">
                        Nombre Empresa: 
                    </h3>
                    <h3 className="py-2">
                        Rut: 
                    </h3>
                </div>  
            </div>          
        </div>
    )
}

export default PerfilGeneral
