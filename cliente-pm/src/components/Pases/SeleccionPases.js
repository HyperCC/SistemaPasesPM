import React from 'react'

export const Pases = () => {
    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-4xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">
                    <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
                        <p class="text-4xl py-6 leading-tight mx-8 text-center md:ml-16">
                            Seleccionar Tipo de Pase
                        </p>

                        <div class="content-center mt-6 mx-8 mb-2 md:mb-0">
                            <div class="grid grid-cols-4 md:gap-8 gap-4">

                                <div class="col-span-4 md:col-span-1">
                                    <a href="/SolicitudVisita"
                                        className="text-center block px-4 py-2 font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                        Visita General</a>
                                </div>
                                <div class="col-span-4 md:col-span-3">
                                    <p class="text-sm md:text-base">Aquellas personas que permanecen en la empresa por un periodo de tiempo, no superior a 120hrs, estas personas no realizan trabajos operativos, intervenciones a procesos o sistemas</p>
                                </div>

                                <div class="col-span-4 md:col-span-1 mt-4">
                                    <a href="/SolicitudContratista"
                                        className="text-center block px-4 py-2 font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                        Contratista
                                </a>
                                </div>
                                <div class="col-span-4 md:col-span-3 md:mt-4">
                                    <p class="text-sm md:text-base">Todo personal que mantiene contrato vigente con empresas colaboradoras y con las cuales existe una relación contractual de prestación de servicios directos a la T y/o PMEJ</p>
                                </div>

                                <div class="col-span-4 md:col-span-1 mt-4">
                                    <a href="/SolicitudProveedor"
                                        className="text-center block px-4 py-2 font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                        Proveedor
                                </a>
                                </div>
                                <div class="col-span-4 md:col-span-3 md:mt-4">
                                    <p class="text-sm md:text-base">Serán aquellas personas naturales o juridicas que esporádicamente realizan entregas de productos, por otro lado, los prestadores de servicios realizarán servicios de forma regular o no, pudiendo tener interacción con las operaciones o bien solo servicios de consultoría en oficinas</p>
                                </div>

                                <div class="col-span-4 md:col-span-1 mt-4">
                                    <a href="/SolicitudUsoDeMuelle"
                                        className="text-center block px-4 py-2 font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                        Uso de Muelle
                                </a>
                                </div>
                                <div class="col-span-4 md:col-span-3 md:mt-4">
                                    <p class="text-sm md:text-base">Evaluación realizada a través del área de operaciones</p>
                                </div>

                                <div class="col-span-4 md:col-span-1 mt-4">
                                    <a href="/SolicitudTripulante"
                                        className="text-center block px-4 py-2 font-semibold text-white bg-verde-pm rounded-md shadow-md hover:bg-amarillo-pm focus:outline-none transition duration-500">
                                        Tripulante
                                </a>
                                </div>
                                <div class="col-span-4 md:col-span-3 md:mt-4">
                                    <p class="text-sm md:text-base">Aquellas empresas que requieren que sus colaboradores realicen ingreso de tránsito en el muelle de Puerto Mejillones, deberán solicitar dicho ingreso a través de un correo dirigido a seguridad@puertomejillones.cl, este correo deberá ser enviado a través de las empresas de agenciamiento con que Puerto Mejillones mantiene relaciones comerciales, y validando en terreno al personal a través del documento Short pass y el</p>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    );
};