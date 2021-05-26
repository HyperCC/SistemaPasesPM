import React, { useState } from 'react';

const AgregarPersona = () => {

    // datos a guardar para el form
    const [personaExterna, setPersonaExterna] = useState({
        Nombres: null,
        PrimerApellido: null,
        SegundoApellido: null,
        Rut: '',
        Pasaporte: '',
        Nacionalidad: null
    });

    // cambio por radiobutton para el rut o pasaporte
    const [isRut, setIsRut] = useState(true);
    const changeIsRut = () => setIsRut(true);
    const changeNotIsRut = () => setIsRut(false);

    // asignar nuevos valores al state del registro
    const ingresarValoresMemoria = valorInput => {
        // obtener el valor
        const { name, value } = valorInput.target;

        // asignar el valor
        setPersonaExterna(anterior => ({
            ...anterior, // mantener lo que existe antes
            [name]: value // solo cambiar el input mapeado
        }));
    };

    return (
        <div>
            <div class="bg-gray-100 min-h-screen">
                <div class="sm:py-16 sm:px-6 px-2 py-8">
                    <div class="max-w-xl mx-auto sm:px-6 px-4 pb-12 bg-white rounded-lg shadow-md">

                        <div class="text-center">
                            <p class="text-3xl text-grey-darkest pt-12 w-full select-none">
                                Agregar Persona
                            </p>
                        </div>

                        <div>
                            <div>nombres: {personaExterna.Nombres}</div>
                            <div>primer apellido: {personaExterna.PrimerApellido}</div>
                            <div>segundo apellido: {personaExterna.SegundoApellido}</div>
                            <div>rut: {personaExterna.Rut}</div>
                            <div>pasaporte: {personaExterna.Pasaporte}</div>
                            <div>nacionalidad: {personaExterna.Nacionalidad}</div>
                        </div>

                        <form>
                            {/* DATOS PRINCIPALES PARA TRABAJADOR */}
                            <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-12">

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="Nombres">Nombres</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={personaExterna.Nombres} onChange={ingresarValoresMemoria} name="Nombres" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="PrimerApellido">Primer Apellido</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={personaExterna.PrimerApellido} onChange={ingresarValoresMemoria} name="PrimerApellido" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="SegundoApellido">Segundo Apellido</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={personaExterna.SegundoApellido} onChange={ingresarValoresMemoria} name="SegundoApellido" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                {/* eleccion de utilizacion de rut o pasaporte*/}
                                <div class="col-span-1 form-group">
                                    <div onClick={changeIsRut}>
                                        <label class="inline-flex items-center">
                                            <input type="radio" class="form-radio text-indigo-600 h-4 w-4" name="radio-colors" value="1" checked={isRut} />
                                            <span class="ml-2">Rut</span>
                                        </label>
                                    </div>

                                    <div onClick={changeNotIsRut}>
                                        <label class="inline-flex items-center">
                                            <input type="radio" class="form-radio text-green-500 h-4 w-4" name="radio-colors" value="2" />
                                            <span class="ml-2">Pasaporte</span>
                                        </label>
                                    </div>
                                </div>

                                <div class="col-span-2" >
                                    {isRut
                                        ? <input placeholder="ingrese el rut" type="text" name="Rut" value={personaExterna.Rut} onChange={ingresarValoresMemoria} class="w-full border-2 py-1 px-3 border-gray-200 rounded-md  bg-green-300" />
                                        : <input placeholder="ingrese el pasaporte" type="text" name="Pasaporte" value={personaExterna.Pasaporte} onChange={ingresarValoresMemoria} class="w-full border-2 py-1 px-3 border-gray-200 rounded-md  bg-red-300" />
                                    }
                                </div >

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="Nacionalidad">Nacionalidad</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={personaExterna.Nacionalidad} onChange={ingresarValoresMemoria} name="Nacionalidad" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md  bg-gray-100" />
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
};

export default AgregarPersona;
