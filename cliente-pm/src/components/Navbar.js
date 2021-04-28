import React from 'react';

const Navbar = () => {
    return(
        <nav className="flex justify-between items-center p-4 bg-blue-900">
            <p class="text-2xl font-bold text-grey-800 text-white">Sistemas Pases de Accesos</p>
            <div className="flex space-x-2">
                <a href="/"
                className=" rounded bg-green-500 text-white p-2 px-4">
                    Perfil
                </a>
                <a href="/"
                className=" rounded bg-green-500 text-white p-2 px-4">
                    Salir
                </a>
            </div>
        </nav>
    );
};

export default Navbar;