import React from 'react';
import { withRouter } from 'react-router';
import { useStateValue } from '../contexto/store';
import Image from '../images/Blanco.png';
import { LanzarNoritificaciones } from './avisos/LanzarNotificaciones';

const Navbar = (props) => {
    // variables globales
    const [{ sesionUsuario }, dispatch] = useStateValue();

    // cerrar ession y eliminar credenciales del navegador
    const cerrarSesion = () => {
        console.log('cerrando la sesion actual');
        localStorage.removeItem('token_seguridad');

        const sleep = (milliseconds) =>
            new Promise(resolve => setTimeout(resolve, milliseconds));

        sleep(1000).then(() => {
            dispatch({
                type: 'SALIR_SESION',
                nuevoUsuario: null,
                autenticado: false
            });
            props.history.push('/');
        });
    };

    return sesionUsuario
        ? (sesionUsuario.autenticado == true ?
            <nav class="bg-azul-pm">
                <div class="max-w-7xl mx-auto px-2 sm:px-6 lg:px-8">
                    <div class="relative flex items-center justify-between h-16">

                        {/* logo de empresa */}
                        <div class="flex items-stretch justify-start">
                            <a href="/" class="flex items-center m-auto min-w-full">
                                <img class="block h-8 w-auto" src={Image} alt="Workflow" />
                            </a>
                        </div>

                        <div class="absolute inset-x-0 flex justify-center inset-y-auto">
                            <div class="text-2xl opacity-0 md:opacity-100 capitalize mx-auto text-white">Sistema para solicitud de pases</div>
                        </div>


                        {/* opciones al extremo derecho de la barra */}
                        <div class="absolute inset-y-0 right-0 flex items-center pr-2 sm:static sm:inset-auto sm:ml-6 sm:pr-0">
                            <a class="relative" href="/Perfil">
                                <div class="rounded bg-verde-pm text-white p-2 px-4 hover:bg-amarillo-pm transition duration-500">
                                    Perfil
                                </div>
                            </a>

                            <button class="ml-3 relative" onClick={cerrarSesion}>
                                <div class="rounded bg-verde-pm text-white p-2 px-4 hover:bg-amarillo-pm transition duration-500">
                                    Salir
                                </div>
                            </button>
                        </div>
                    </div>
                </div>
            </nav>
            : null)
        : null  
};

export default withRouter(Navbar);