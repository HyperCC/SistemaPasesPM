import React from 'react';
import '../../App.css';

function CambiarClave(props) {
    return (
        <div>
            <div class="bg-gray-100 min-h-screen">
                <div class="sm:py-16 sm:px-6 px-2 py-8">
                    <div class="max-w-xl mx-auto sm:px-6 px-4 pb-12 bg-white rounded-lg shadow-md">

                        <div class="text-center">
                            <p class="text-3xl text-grey-darkest pt-12 w-full select-none">
                                Cambiar Contraseña
                            </p>
                        </div>

                        <form>
                            {/* DATOS PRINCIPALES PARA USUARIO */}
                            <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-12">
                                
                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="Correo">Correo Electrónico</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="email" name="Correo" placeholder="e@mail.com" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="ContraseñaActual">Contraseña Actual</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text"  name="ContraseñaActual" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="NuevaContraseña">Nueva Contraseña</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" name="NuevaContraseña" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md  bg-gray-100" />
                                </div>
                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="RepetirContraseña">Repetir Contraseña</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" name="RepetirContraseña" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md  bg-gray-100" />
                                </div>
                            </div>

                            {/* ENVIAR DATOS */}
                            <div class="mt-12 flex justify-center">
                                <button type="submit"
                                    class="bg-azul-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                                    Guardar
                            </button>
                            </div>
                        </form>

                    </div>
                </div>
            </div>
        </div>
    );
}

export default CambiarClave;