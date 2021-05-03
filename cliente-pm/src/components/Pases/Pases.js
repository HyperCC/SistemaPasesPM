import React from 'react'


export const Pases = () => {
    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">
                    <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
                        <p class="text-4xl py-6 leading-tight mx-8 text-center md:ml-16">
                            Seleccine Tipo de Pase
                        </p>

                        <div class="content-start space-y-6 grid grid-cols-2 mt-6 mx-8 mb-2 md:mb-0">
                            
                            <div>
                                <a href="/"
                                    className="w-44 text-center flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                     Visita General
                                </a>
                            </div>
                            
                            <div> <p>Texto descripción pase visita</p> </div>
                            
                            <div>
                                <a href="/"
                                    className="w-44 text-center flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                     Contratista
                                </a>
                            </div>
                            
                            <div> <p>Texto descripción pase contratista</p> </div>

                            <div>
                                <a href="/"
                                    className="w-44 text-center flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                     Proveedor
                                </a>
                            </div>
                            
                            <div> <p>Texto descripción pase proovedor</p> </div>

                            <div>
                                <a href="/"
                                    className="w-44 text-center flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                     Uso de Muelle
                                </a>
                            </div>
                            
                            <div> <p>Texto descripción pase uso muelle</p> </div>

                            <div>
                                <a href="/"
                                    className="w-44 text-center flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                     Tripulante
                                </a>
                            </div>
                            
                            <div> <p>Texto descripción pase tripulante</p> </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}
