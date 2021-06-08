import React, { useState } from 'react';
import { loginUsuario } from '../../actions/UsuarioAction';
import '../../App.css';
import Image from '../../images/Blanco.png';
import { LanzarNoritificaciones } from '../avisos/LanzarNotificaciones';
import { useStateValue } from '../../contexto/store';
import { withRouter } from 'react-router';

const Home = props => {
    const [{ usuarioSesion }, dispatch] = useStateValue();

    // mostrar o no el password
    const [hidePassword, setHidePassword] = useState(true);
    const cambiarHidePassword = () => setHidePassword(!hidePassword);

    // atributos para el registro de usuario
    const [dataUsuario, setDataUsuario] = useState({
        Email: '',
        Password: ''
    });

    // codigo actual de la notificacion a mostrar
    const [currentNotification, setCurrentNotification] = useState('none');
    // posibles campos invalidos enviados por el usuario
    const [currentCamposInvalidos, setCurrentCamposInvalidos] = useState([]);
    //mostrar una notificacion
    const [currentOpenNotificacion, setCurrentOpenNotificacion] = useState(false);

    // asignar nuevos valores al state del login
    const ingresarValoresMemoria = valorInput => {
        // obtener el valor
        const { name, value } = valorInput.target;

        // actualizar el valor de las credenciales
        setDataUsuario(anterior => ({
            ...anterior, // mantener lo que existe antes
            [name]: value // solo cambiar el input mapeado
        }));
    };


    // boton para enviar el formulario
    const botonLoginUsuario = infoFormulario => {

        // cancelar el envio inmediato del formulario
        infoFormulario.preventDefault();
        console.log('data usuario: ', dataUsuario);
        setCurrentNotification('inf-cvc000');

        loginUsuario(dataUsuario, dispatch).then(response => {

            if (typeof response !== 'undefined') {

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
                    //window.localStorage.setItem('mensaje_success', 'exi-le0000');
                    //window.localStorage.setItem('mensaje_success_showed', false);
                    window.localStorage.setItem('token_seguridad', response.data.token);
                    window.localStorage.setItem('data_current_usuario', JSON.stringify(response.data));
                    console.log('EL LOGIN FUE EXISTO ', response.data.token);

                    setCurrentNotification('exi-le0000');
                    setCurrentOpenNotificacion(true);

                    const sleep = (milliseconds) =>
                        new Promise(resolve => setTimeout(resolve, milliseconds));
                    sleep(1000).then(() => {
                        props.history.push('/Perfil');
                    });
                }

                // si no hay conexion con el servidor pero si al cliente.
            } else {
                setCurrentNotification('err-nhc000');
                setCurrentOpenNotificacion(true);
            }
        });
    };

    return (
        <div>
            <div class="w-full grid grid-cols-1 md:grid-cols-2">
                <div class="w-full col-span-1">

                    <LanzarNoritificaciones codigo={currentNotification} camposInvalidos={currentCamposInvalidos} openNotificacion={currentOpenNotificacion} />

                    <div class="flex flex-col justify-center md:justify-start my-auto pt-8 md:pt-0 px-8 md:px-24 lg:px-32">
                        <p class="text-center text-5xl p-8 md:mt-8">Gestión de pases PMEJ</p>

                        <form class="flex flex-col pt-3 md:pt-8">
                            <div class="flex flex-col pt-4">
                                <label for="Email" class="text-lg">Correo Electronico</label>
                                <input type="email" name="Email" value={dataUsuario.Email} onChange={ingresarValoresMemoria} placeholder="your@email.com" autoFocus class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline" />
                            </div>

                            <div class="flex flex-col pt-4">
                                <label for="Password" class="text-lg">Contraseña</label>
                                <div class="relative">
                                    <input type={hidePassword ? "text" : "password"} name="Password" value={dataUsuario.Password} onChange={ingresarValoresMemoria} placeholder="Contraseña" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline" />

                                    <div class="absolute inset-y-0 right-0 pr-3 flex items-center leading-5" onClick={cambiarHidePassword}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" class={hidePassword ? "bi bi-eye-slash-fill text-gray-700" : "bi bi-eye-slash-fill text-gray-700 hidden"} viewBox="0 0 16 16">
                                            <path d="m10.79 12.912-1.614-1.615a3.5 3.5 0 0 1-4.474-4.474l-2.06-2.06C.938 6.278 0 8 0 8s3 5.5 8 5.5a7.029 7.029 0 0 0 2.79-.588zM5.21 3.088A7.028 7.028 0 0 1 8 2.5c5 0 8 5.5 8 5.5s-.939 1.721-2.641 3.238l-2.062-2.062a3.5 3.5 0 0 0-4.474-4.474L5.21 3.089z" />
                                            <path d="M5.525 7.646a2.5 2.5 0 0 0 2.829 2.829l-2.83-2.829zm4.95.708-2.829-2.83a2.5 2.5 0 0 1 2.829 2.829zm3.171 6-12-12 .708-.708 12 12-.708.708z" />
                                        </svg>
                                    </div>

                                    <div class="absolute inset-y-0 right-0 pr-3 flex items-center leading-5" onClick={cambiarHidePassword}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="currentColor" class={hidePassword ? "bi bi-eye-slash-fill text-gray-700 hidden" : "bi bi-eye-slash-fill text-gray-700"} viewBox="0 0 16 16">
                                            <path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0z" />
                                            <path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8zm8 3.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7z" />
                                        </svg>
                                    </div>

                                </div>
                            </div>

                            <button type="submit" onClick={botonLoginUsuario}
                                class="bg-azul-pm rounded-lg shadow-md hover:bg-amarillo-pm text-white font-bold text-lg p-2 mt-8 transition duration-500">
                                Ingresar
                            </button>

                        </form>
                        <div class="text-center mx-auto text-sm py-12">
                            <p>¿No posees una cuenta? <a href="/registro" class="bm-azul-pm hover:text-gray-700 underline font-semibold">Registrate Aquí</a></p>
                            <p class="py-1">¿No recuerdas su contraseña? <a href="/RecuperarContraseña" class="bm-azul-pm hover:text-gray-700 underline font-semibold">Recuperar Aquí</a></p>
                            <p>¿Desea cambiar contraseña? <a href="/CambiarContraseña" class="bm-azul-pm hover:text-gray-700 underline font-semibold">Cambiar Aquí</a></p>
                        </div>
                    </div>

                </div>
                <div class="md:min-h-screen flex justify-center align-middle col-span-1 bg-azul-pm">
                    <img class="m-auto inline-block p-8 md:p-16 bg-cover" src={Image} />
                </div>
            </div >
        </div >
    );
}

export default withRouter(Home);