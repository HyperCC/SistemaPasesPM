import React from 'react';


export const AgregarAnexo = () => {
    
    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">

                    <div class="text-center">
                        <p class="text-3xl text-grey-darkest pt-12 w-full select-none">
                            Agregar Nuevo Anexo de Contrato
                        </p>
                    </div>

                    <form>
                        
                        {/* Datos de los anexos */}
                        <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-12">
                        
                            <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-4">
                                <div class="col-span-1 form-group">
                                    <label class="text-gray-800 select-none" for="FechaVencimientoAnexo">Fecha de Vencimiento</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" name="FechaVencimientoAnexo" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="text-gray-800 select-none" for="Anexo">Documento Anexo</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" name="Anexo" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="text-gray-800 select-none" for="CronogramaTrabajo">Descripción</label>
                                </div>
                                <div class="col-span-2">
                                <textarea type="range" placeholder="range...." class="border w-full app border-gray-300 p-2 my-2 rounded-md focus:outline-none focus:ring-2 ring-azul-pm"> </textarea>
                                </div>
                            </div>

                        </div>

                    </form>
                
                </div>
            </div>
        </div>
    )
}
