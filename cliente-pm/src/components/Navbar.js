import React from 'react';
import Image from '../images/Blanco.png';

const Navbar = () => {
    return(
        <nav className="flex justify-between items-center p-4 bg-blue-900">
            <img src={Image} alt="website logo" className="h-12 rounded ab-20 shadow" />
            <p class="text-center text-2xl font-bold text-grey-800 text-white">Sistemas Pases de Accesos</p>
            <div className="flex space-x-2">
                <a href="/"
                className=" rounded bg-green-500 text-white p-2 px-4 hover:bg-yellow-400">
                    Perfil
                </a>
                <a href="/"
                className=" rounded bg-green-500 text-white p-2 px-4 hover:bg-yellow-400">
                    Salir
                </a>
            </div>
        </nav>
    );
};

export default Navbar;