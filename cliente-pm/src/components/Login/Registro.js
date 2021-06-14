import React, { useState } from 'react';
import '../../App.css';
import { registrarUsuario } from '../../actions/UsuarioAction';
import ReCAPTCHA from "react-google-recaptcha";
import { LanzarNoritificaciones } from '../avisos/LanzarNotificaciones';
import { withRouter } from 'react-router';
import RutValidator from "w2-rut-validator"


// pagina principal de registro
const Registro = props => {

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

    // verificar si se marco la casilla
    const [checkNoPerteneceEmpresa, setCheckNoPerteneceEmpresa] = useState(false);
    // codigo actual de la notificacion a mostrar
    const [currentNotification, setCurrentNotification] = useState('none');
    // posibles campos invalidos enviados por el usuario
    const [currentCamposInvalidos, setCurrentCamposInvalidos] = useState([]);

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

            // actualizar el valor de algun otro valor
        }else {
            // asignar el valor
            setDataUsuario(anterior => ({
                ...anterior, // mantener lo que existe antes
                [name]: value // solo cambiar el input mapeado
            }));
        }
    };

    // validar el valor del captcha ya aceptado
    function onChange(value) {

        setDataUsuario(anterior => ({
            ...anterior, // mantener lo que existe antes
            ['Captcha']: true  // solo cambiar el input mapeado
        }));

        console.log('data usuario: ', dataUsuario);
    };

    // boton para enviar el formulario
    const botonRegistrarUsuario = infoFormulario => {

        // cancelar el envio inmediato del formulario
        infoFormulario.preventDefault();
        console.log('data usuario: ', dataUsuario);
        setCurrentNotification('inf-cgu000');

        // verificar que el captcha fue validado
        if (!dataUsuario.Captcha) {
            console.log('ANTES DE ENVIAR EL FORMULARIO SE DEBE VALIDAR EL CAPTCHA.');
            setCurrentNotification('adv-cnc000');
            return;
        }

        // verificar que el rut es valido
        if (!RutValidator.validate(dataUsuario.Rut)){
            alert('Por favor ingrese el rut con el siguiente formato 11.111.111-1')
            setDataUsuario(anterior => ({
                ...anterior, // mantener lo que existe antes
                ['Rut']: '' // reseteamos el rut
            }));
            return;
        }

        setCurrentNotification('inf-cgp0000'); // notificacion de carga de datos

        // uso del action registrar
        registrarUsuario(dataUsuario).then(response => {

            if (typeof response !== 'undefined') {
                console.log('la API esta conectada', response);

                // si se reciben errores
                if (typeof response.data.errores !== 'undefined') {
                    console.log(response.data.errores.mensaje);

                    console.log('el tipo de error: ', response.data.errores.tipoError);
                    setCurrentNotification(response.data.errores.tipoError);
                    console.log('TIPO ACTUAL DE NOTIFICACION ', currentNotification);

                    if (typeof response.data.errores.listaErrores !== 'undefined')
                        setCurrentCamposInvalidos(response.data.errores.listaErrores);

                    // si toda la operacion salio ok
                } else {
                    window.localStorage.setItem('mensaje_success', 'exi-re0000');
                    window.localStorage.setItem('mensaje_success_showed', false);
                    setCurrentNotification('exi-re0000');

                    const sleep = (milliseconds) =>
                        new Promise(resolve => setTimeout(resolve, milliseconds));
                    sleep(1000).then(() => {
                        props.history.push('/');
                    });
                }

                // si no hay conexion con la API
            } else {
                console.log('la API no esta conectada', response);
                setCurrentNotification('err-nhc000');
            }
        });
    };


    return (
        <div>
            <div class="bg-gray-100 min-h-screen">
                <div class="sm:py-16 sm:px-6 px-2 py-8">

                    <LanzarNoritificaciones codigo={currentNotification} camposInvalidos={currentCamposInvalidos} />

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
                                    <input type="text" value={dataUsuario.Rut} onChange={ingresarValoresMemoria} name="Rut" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus
                                    />
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
                                    <input type="checkbox" checked={checkNoPerteneceEmpresa} value={dataUsuario.NoPerteneceEmpresa} onClick={ingresarValoresMemoria} name="NoPerteneceEmpresa" /> No pertenece a la Empresa
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

                            <div class="grid md:grid-cols-3 grid-cols-1 px-4 md:px-8 gap-4 mt-6">
                                <div class="md:col-span-2 md:col-start-2">
                                    <div class="w-full">
                                        <label class="text-center font-light  text-gray-800 select-none" for="Captcha">
                                            <ReCAPTCHA
                                                sitekey="6LcoUMYaAAAAAPlwUFz02HrTJa5GJqnKhOrOoC6B"
                                                onChange={onChange}
                                            />
                                        </label>
                                    </div>
                                </div>
                            </div>

                            {/* ENVIAR DATOS */}
                            <div class="mt-12 flex justify-center">
                                <button type="submit" onClick={botonRegistrarUsuario}
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

export default withRouter(Registro);