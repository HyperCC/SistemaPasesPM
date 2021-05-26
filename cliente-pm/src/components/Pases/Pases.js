import React from 'react'

export const Pases = () => {
    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">
                    <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
                        <p class="text-4xl py-6 leading-tight mx-8 text-center md:ml-16">
                            Seleccione Tipo de Pase
                        </p>

                        <div class="content-center space-y-6 grid grid-cols-2 mt-6 mx-8 mb-2 md:mb-0">
                            <div></div>
                            <div></div>
                            <div>
                                <a href="/SolicitudVisita"
                                    className="w-44 text-center flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                     Visita General</a>
                            </div>
                            
                            <div> <p>Aquellas personas que permanecen en la empresa por un periodo de tiempo, no superior a 120hrs, estas personas no realizan trabajos operativos, intervenciones a procesos o sistemas</p> </div>
                            
                            <div>
                                <a href="/SolicitudContratista"
                                    className="w-44 text-center flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                     Contratista
                                </a>
                            </div>
                            
                            <div> <p>Todo personal que mantiene contrato vigente con empresas colaboradoras y con las cuales existe una relación contractual de prestación de servicios directos a la T y/o PMEJ</p> </div>

                            <div>
                                <a href="/SolicitudProveedor"
                                    className="w-44 text-center flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                     Proveedor
                                </a>
                            </div>
                            
                            <div> <p>Serán aquellas personas naturales o juridicas que esporádicamente realizan entregas de productos, por otro lado, los prestadores de servicios realizarán servicios de forma regular o no, pudiendo tener interacción con las operaciones o bien solo servicios de consultoría en oficinas</p> </div>

                            <div>
                                <a href="/SolicitudUsoDeMuelle"
                                    className="w-44 text-center flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                     Uso de Muelle
                                </a>
                            </div>
                            
                            <div> <p>Evaluación realizada a través del área de operaciones</p> </div>

                            <div>
                                <a href="/SolicitudTripulante"
                                    className="w-44 text-center flex-shrink-0 block px-4 py-2 md:mt-0 mt-4 md:mx-0 mx-auto text-base font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                     Tripulante
                                </a>
                            </div>
                            
                            <div> <p>Aquellas empresas que requieren que sus colaboradores realicen ingreso de tránsito en el muelle de Puerto Mejillones, deberán solicitar dicho ingreso a través de un correo dirigido a seguridad@puertomejillones.cl, este correo deberá ser enviado a través de las empresas de agenciamiento con que Puerto Mejillones mantiene relaciones comerciales, y validando en terreno al personal a través del documento Short pass y el</p> </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}
