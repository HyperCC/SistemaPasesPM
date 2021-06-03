import React from 'react';
import { withRouter } from 'react-router';
import { useStateValue } from '../contexto/store';
import Image from '../images/Blanco.png';

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
            <nav className="flex justify-between items-center p-4 bg-azul-pm">
                <img src={Image} alt="website logo" className="h-12 rounded ab-20 shadow" />
                <p class="text-center text-2xl font-bold text-grey-800 text-white">Sistemas Pases de Accesos</p>
                <div className="flex space-x-2">
                    <a href="/"
                        className=" rounded bg-verde-pm text-white p-2 px-4 hover:bg-amarillo-pm">
                        Perfil
                </a>
                    <button onClick={cerrarSesion}
                        className=" rounded bg-verde-pm text-white p-2 px-4 hover:bg-amarillo-pm">
                        Salir
                </button>
                </div>
            </nav>
            : null)
        : null
};

export default withRouter(Navbar);