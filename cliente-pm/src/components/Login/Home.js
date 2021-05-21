import React, { useState } from 'react';
import { loginUsuario } from '../../actions/UsuarioAction';
import '../../App.css';
import Image from '../../images/Blanco.png';
import { LanzarNoritificaciones } from '../avisos/LanzarNotificaciones';

const Home = () => {

    // atributos para el registro de usuario
    const [dataUsuario, setDataUsuario] = useState({
        Email: '',
        Password: ''
    });

    // codigo actual de la notificacion a mostrar
    const [currentNotification, setCurrentNotification] = useState('none');
    // posibles campos invalidos enviados por el usuario
    const [currentCamposInvalidos, setCurrentCamposInvalidos] = useState([]);

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
    const botonRegistrarUsuario = infoFormulario => {

        // cancelar el envio inmediato del formulario
        infoFormulario.preventDefault();
        console.log('data usuario: ', dataUsuario);

        loginUsuario(dataUsuario).then(response => {
            console.log('se registro exitosamente el nuevo usuario. ', response);

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
                window.localStorage.setItem('mensaje_success', 'exi-le0000');
                window.localStorage.setItem('mensaje_success_showed', false);
                setCurrentNotification('exi-le0000');
            }
        });
    };

    return (
        <div>
            <div class="w-full flex flex-wrap">
                <div class="w-full md:w-1/2 flex flex-col">

                    <LanzarNoritificaciones codigo={currentNotification} camposInvalidos={currentCamposInvalidos} />

                    <div class="flex flex-col justify-center md:justify-start my-auto pt-8 md:pt-0 px-8 md:px-24 lg:px-32">
                        <p class="text-center text-5xl">Gestión de pases PMEJ</p>
                        <form class="flex flex-col pt-3 md:pt-8">

                            <div class="flex flex-col pt-4">
                                <label for="Email" class="text-lg">Correo Electronico</label>
                                <input type="email" name="Email" value={dataUsuario.Email} onChange={ingresarValoresMemoria} placeholder="your@email.com" autoFocus class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline" />
                            </div>

                            <div class="flex flex-col pt-4">
                                <label for="Password" class="text-lg">Contraseña</label>
                                <input type="password" name="Password" value={dataUsuario.Password} onChange={ingresarValoresMemoria} placeholder="Contraseña" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline" />
                            </div>

                            <button type="submit" onClick={botonRegistrarUsuario}
                                class="bg-azul-pm rounded-lg shadow-md hover:bg-amarillo-pm text-white font-bold text-lg  p-2 mt-8 transition duration-500">
                                Ingresar
                            </button>

                        </form>
                        <div class="text-center pt-12 pb-12">
                            <p>¿No posees una cuenta? <a href="/registro" class="bm-azul-pm hover:text-gray-700 underline font-semibold">Registrate Aquí</a></p>
                            <p class="p-1">¿No recuerdas su contraseña? <a href="/RecuperarContraseña" class="bm-azul-pm hover:text-gray-700 underline font-semibold">Recuperar Aquí</a></p>
                            <p>¿Desea cambiar contraseña? <a href="/CambiarContraseña" class="bm-azul-pm hover:text-gray-700 underline font-semibold">Cambiar Aquí</a></p>
                        </div>
                    </div>

                </div>
                <div class="w-1/2  h-screen justify-center shadow-2xl bg-azul-pm">
                    <img class="m-auto bg-cover rounded place-self-center" src={Image}></img>
                </div>

            </div>
        </div>
    );
}

export default Home;