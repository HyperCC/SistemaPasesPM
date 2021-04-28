import React from 'react';
import './App.css';

export default function Registro() {
    return (
        <div>
            <div class="bg-gray-100 min-h-screen">
                <div class="sm:py-16 sm:px-6 px-2 py-8">
                    <div class="max-w-xl mx-auto sm:px-6 px-4 pb-12 bg-white rounded-lg shadow-md">

                        <div class="text-center">
                            <p class="text-3xl text-grey-darkest pt-12 w-full select-none">
                                Registro de Usuario
                            </p>
                        </div>

                        <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-12">
                            <div class="col-span-1 form-group">
                                <label class="font-light  text-gray-800 select-none" for="rut">Rut</label>
                            </div>
                            <div class="col-span-2">
                                <input type="text" name="rut" id="rut" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                            </div>

                            <div class="col-span-1 form-group">
                                <label class="font-light  text-gray-800 select-none" for="nombre">Nombres</label>
                            </div>
                            <div class="col-span-2">
                                <input type="text" name="nombre" id="nombre" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                            </div>

                            <div class="col-span-1 form-group">
                                <label class="font-light  text-gray-800 select-none" for="apellido">Apellidos</label>
                            </div>
                            <div class="col-span-2">
                                <input type="text" name="apellido" id="apellido" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                            </div>

                            <div class="col-span-1 form-group">
                                <label class="font-light  text-gray-800 select-none" for="correo">Correo Electronico</label>
                            </div>
                            <div class="col-span-2">
                                <input type="email" name="correo" id="correo" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md  bg-gray-100" />
                            </div>
                        </div>

                        <div class="mt-6 form-group px-4 md:px-8">
                            <label class="font-light  text-gray-800 select-none" for="noperteneceempresa">
                                <input type="checkbox" id="noperteneceempresa" name="noperteneceempresa" /> No pertenece a la Empresa
                            </label>
                        </div>

                        <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-6">
                            <div class="col-span-1 form-group">
                                <label class="font-light  text-gray-800 select-none" for="rutempresa">Rut Empresa</label>
                            </div>
                            <div class="col-span-2">
                                <input type="text" name="rutempresa" id="rutempresa" class="w-full border-2  py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                            </div>

                            <div class="col-span-1 form-group">
                                <label class="font-light  text-gray-800 select-none" for="nombreempresa">Nombre Empresa</label>
                            </div>
                            <div class="col-span-2">
                                <input type="text" name="nombreempresa" id="nombreempresa" class="w-full border-1 py-1 px-3 border-gray-800 rounded-md bg-gray-100" />
                            </div>
                        </div>

                        <div class="mt-6 form-group px-4 md:px-8">
                            <label class="font-light  text-gray-800 select-none" for="captcha">
                                <input type="checkbox" id="captcha" name="captcha" /> Captcha
                            </label>
                        </div>

                        <div class="mt-12 flex justify-center">
                            <button class="bg-blue-900 hover:bg-blue-800 shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                                Guardar
                            </button>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    );
}