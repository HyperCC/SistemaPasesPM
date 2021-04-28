import React, { useState } from 'react';
import './App.css';
import { registrarUsuario } from '../../actions/UsuarioAction';

// pagina principal de registro
const Registro = () => {

    // atributos para el registro de usuario
    const [dataUsuario, setDataUsuario] = useState({
        Rut: '',
        Nombres: '',
        Apellidos: '',
        Correo: '',
        NoPerteneceEmpresa: '',
        RutEmpresa: '',
        NombreEmpresa: '',
        Captcha: ''
    });

    // asignar nuevos valores al state del registro
    const ingresarValoresMemoria = nuevoValor => {

        // obtener el valor
        const { atributo, valor } = nuevoValor.target;
        // asignar el valor
        setDataUsuario(anterior => ({
            ...anterior,
            [atributo]: valor
        }));
    };

    // boton para enviar el formulario
    const botonRegistrarUsuario = valoresActuales => {
        // cancelar el envio inmediato del formulario
        valoresActuales.preventDefault();

        // uso del action registrar
        registrarUsuario(dataUsuario).then(response => {
            console.log('se registro exitosamente el nuevo usuario. ', response);
        });
    };

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

                        <form>
                            {/* DATOS PRINCIPALES PARA USUARIO */}
                            <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-12">
                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="rut">Rut</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={dataUsuario.Rut} onChange={ingresarValoresMemoria} name="rut" id="rut" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="nombre">Nombres</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={dataUsuario.Nombres} onChange={ingresarValoresMemoria} name="nombre" id="nombre" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="apellido">Apellidos</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={dataUsuario.Apellidos} onChange={ingresarValoresMemoria} name="apellido" id="apellido" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="correo">Correo Electronico</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="email" value={dataUsuario.Correo} onChange={ingresarValoresMemoria} name="correo" id="correo" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md  bg-gray-100" />
                                </div>
                            </div>

                            <div class="mt-6 form-group px-4 md:px-8">
                                <label class="font-light  text-gray-800 select-none" for="noperteneceempresa">
                                    <input type="checkbox" value={dataUsuario.NoPerteneceEmpresa} onChange={ingresarValoresMemoria} id="noperteneceempresa" name="noperteneceempresa" value="checked" /> No pertenece a la Empresa
                            </label>
                            </div>

                            {/* DATOS PRINCIPALES PARA EMPRESA */}
                            <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-6">
                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="rutempresa">Rut Empresa</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={dataUsuario.RutEmpresa} onChange={ingresarValoresMemoria} name="rutempresa" id="rutempresa" class="w-full border-2  py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="nombreempresa">Nombre Empresa</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={dataUsuario.NombreEmpresa} onChange={ingresarValoresMemoria} name="nombreempresa" id="nombreempresa" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>
                            </div>

                            <div class="mt-6 form-group px-4 md:px-8">
                                <label class="font-light  text-gray-800 select-none" for="captcha">
                                    <input type="checkbox" value={dataUsuario.Captcha} onChange={ingresarValoresMemoria} id="captcha" name="captcha" value="checked" /> Captcha
                            </label>
                            </div>

                            {/* ENVIAR DATOS */}
                            <div class="mt-12 flex justify-center">
                                <button type="submit" onClick={botonRegistrarUsuario}
                                    class="bg-blue-900 hover:bg-blue-800 shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
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

export default Registro;