import React, { useState , Component} from 'react';
import '../../App.css';
import { registrarUsuario } from '../../actions/UsuarioAction';
import ReCAPTCHA from "react-google-recaptcha";

// pagina principal de registro
export default function Registro() {
    var isVerified = false;

    function onChange(value) {
        alert("Captcha value:", value);
        isVerified = true;
        alert(isVerified);
    }

    function Verificar(){
        if (isVerified==true){
            alert(isVerified);
            botonRegistrarUsuario();
        }else{
            alert(isVerified)
        }
    }

    // atributos para el registro de usuario
    const [dataUsuario, setDataUsuario] = useState({
        Rut: '',
        Nombres: '',
        Apellidos: '',
        Correo: '',
        NoPerteneceEmpresa: false,
        RutEmpresa: '',
        NombreEmpresa: '',
        Captcha: false
    });

    const [checkCaptcha, setCheckCaptcha] = useState(false);
    const [checkNoPerteneceEmpresa, setCheckNoPerteneceEmpresa] = useState(false);

    // asignar nuevos valores al state del registro
    const ingresarValoresMemoria = valorInput => {
        // obtener el valor
        const { name, value } = valorInput.target;

        // actualizar el valor de NoPerteneceEmpresa segun el check
        if (name === 'NoPerteneceEmpresa') {
            setCheckNoPerteneceEmpresa(!checkNoPerteneceEmpresa);

            setDataUsuario(anterior => ({
                ...anterior, // mantener lo que existe antes
                [name]: !checkNoPerteneceEmpresa // solo cambiar el input mapeado
            }));

            // actualizar el valor de Captcha segun el check
        } else if (name === 'Captcha') {
            setCheckCaptcha(!checkCaptcha);

            setDataUsuario(anterior => ({
                ...anterior, // mantener lo que existe antes
                [name]: !checkCaptcha // solo cambiar el input mapeado
            }));

        } else {
            // asignar el valor
            setDataUsuario(anterior => ({
                ...anterior, // mantener lo que existe antes
                [name]: value // solo cambiar el input mapeado
            }));
        }
    };

    // boton para enviar el formulario
    const botonRegistrarUsuario = infoFormulario => {
        // cancelar el envio inmediato del formulario
        infoFormulario.preventDefault();

        console.log('data usuario: ', dataUsuario);

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
                                    <label class="font-light  text-gray-800 select-none" for="Rut">Rut</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={dataUsuario.Rut} onChange={ingresarValoresMemoria} name="Rut" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="Nombres">Nombres</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" name="Nombres" value={dataUsuario.Nombres} onChange={ingresarValoresMemoria} class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="Apellidos">Apellidos</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={dataUsuario.Apellidos} onChange={ingresarValoresMemoria} name="Apellidos" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="Correo">Correo Electronico</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="email" value={dataUsuario.Correo} onChange={ingresarValoresMemoria} name="Correo" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md  bg-gray-100" />
                                </div>
                            </div>

                            <div class="mt-6 form-group px-4 md:px-8">
                                <label class="font-light  text-gray-800 select-none" for="NoPerteneceEmpresa">
                                    <input type="checkbox" checked={checkNoPerteneceEmpresa} onClick={ingresarValoresMemoria} name="NoPerteneceEmpresa" /> No pertenece a la Empresa
                                </label>
                            </div>

                            {/* DATOS PRINCIPALES PARA EMPRESA */}
                            <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-6">
                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="RutEmpresa">Rut Empresa</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={dataUsuario.RutEmpresa} onChange={ingresarValoresMemoria} name="RutEmpresa" class="w-full border-2  py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="NombreEmpresa">Nombre Empresa</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={dataUsuario.NombreEmpresa} onChange={ingresarValoresMemoria} name="NombreEmpresa" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>
                            </div>

                            <div class="mt-6 items-center justify-center form-group px-4 md:px-8">
                                <label class="text-center font-light  text-gray-800 select-none" for="Captcha">
                                    <ReCAPTCHA
                                        sitekey="6LcoUMYaAAAAAPlwUFz02HrTJa5GJqnKhOrOoC6B"
                                        onChange={onChange}
                                    />
                                </label>
                            </div>

                            {/* ENVIAR DATOS */}
                            <div class="mt-12 flex justify-center">
                                <button type="submit" onClick={Verificar}
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
};