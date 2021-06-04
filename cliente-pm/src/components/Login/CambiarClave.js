import React, { useState } from 'react';
import { withRouter } from 'react-router';
import { cambiarPassword } from '../../actions/UsuarioAction';
import '../../App.css';
import { LanzarNoritificaciones } from '../avisos/LanzarNotificaciones';

const CambiarClave = props => {

    // atributos para el registro de usuario
    const [dataUsuario, setDataUsuario] = useState({
        Email: '',
        Password: '',
        NewPassword: '',
        ConfirmedNewPassword: ''
    });

    // codigo actual de la notificacion a mostrar
    const [currentNotification, setCurrentNotification] = useState('none');
    // posibles campos invalidos enviados por el usuario
    const [currentCamposInvalidos, setCurrentCamposInvalidos] = useState([]);

    // cambair el estado de las variables
    const ingresarValoresMemoria = valorInput => {
        const { name, value } = valorInput.target;

        setDataUsuario(anterior => ({
            ...anterior, // mantener lo que existe antes
            [name]: value // solo cambiar el input mapeado
        }));
    };

    // boton para enviar el formulario
    const botonCambiarClaveUsuario = infoFormulario => {
        infoFormulario.preventDefault();
        console.log('data usuario: ', dataUsuario);

        setCurrentNotification('inf-cgnc00'); // notificacion de carga de datos

        cambiarPassword(dataUsuario).then(response => {

            // si es que hay respuesta
            if (typeof response !== 'undefined') {
                console.log('la API esta conectada', response);

                // si se reciben errores
                if (typeof response.data.errores !== 'undefined') {
                    console.log('se recibireron los siguientes errores: ', response.data.errores.mensaje);
                    setCurrentNotification(response.data.errores.tipoError);

                    // lista de errores de formato (opcional dependiendo del endpoint)
                    if (typeof response.data.errores.listaErrores !== 'undefined')
                        setCurrentCamposInvalidos(response.data.errores.listaErrores);

                    // si la operacion fue exitosa
                } else {
                    console.log('la operacion cambiar clave fue exitosa');
                    setCurrentNotification('exi-cce000');

                    const sleep = (milliseconds) =>
                        new Promise(resolve => setTimeout(resolve, milliseconds));
                    sleep(1000).then(() => {
                        props.history.push('/');
                    });
                }

                // si no hay respuesta
            } else {
                console.log('la API esta conectada', response);
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
                                    <input type="email" value={dataUsuario.Email} onChange={ingresarValoresMemoria} name="Email" placeholder="e@mail.com" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light  text-gray-800 select-none" for="ContraseñaActual">Contraseña Actual</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="password" value={dataUsuario.Password} onChange={ingresarValoresMemoria} name="Password" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="NuevaContraseña">Nueva Contraseña</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="password" value={dataUsuario.NewPassword} onChange={ingresarValoresMemoria} name="NewPassword" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md  bg-gray-100" />
                                </div>
                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="RepetirContraseña">Repetir Contraseña</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="password" value={dataUsuario.ConfirmedNewPassword} onChange={ingresarValoresMemoria} name="ConfirmedNewPassword" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md  bg-gray-100" />
                                </div>
                            </div>

                            {/* ENVIAR DATOS */}
                            <div class="mt-12 flex justify-center">
                                <button type="submit" onClick={botonCambiarClaveUsuario}
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

export default withRouter(CambiarClave);