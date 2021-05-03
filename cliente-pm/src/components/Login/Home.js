import React from 'react';
import '../../App.css';
import Image from '../../images/Blanco.png';

const Home = () => {
    return (
        <div>
            
            <div class="w-full flex flex-wrap">
                <div class="w-full md:w-1/2 flex flex-col">
                    <div class="flex flex-col justify-center md:justify-start my-auto pt-8 md:pt-0 px-8 md:px-24 lg:px-32">
                        <p class="text-center text-5xl">Gestión de pases PMEJ</p>
                        <form class="flex flex-col pt-3 md:pt-8" onsubmit="event.preventDefault();">
                            <div class="flex flex-col pt-4">
                                <label for="email" class="text-lg">Correo Electronico</label>
                                <input type="email" id="email" placeholder="your@email.com" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline">
                                </input>
                            </div>
                            <div class="flex flex-col pt-4">
                                <label for="password" class="text-lg">Contraseña</label>
                                <input type="contraseña" id="contraseña" placeholder="Contraseña" class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mt-1 leading-tight focus:outline-none focus:shadow-outline">
                                </input>
                            </div>
                            <input type="submit" value="Entrar" class="bg-azul-pm rounded-lg shadow-md hover:bg-amarillo-pm text-white font-bold text-lg hover:bg-gray-700 p-2 mt-8"></input>       
                        </form>
                        <div class="text-center pt-12 pb-12">
                            <p>¿No posees una cuenta? <a href="/registro" class="bm-azul-pm hover:bg-verde-pm underline font-semibold">Registrate Aqui.</a></p>
                            <p>¿No recuerdas tu contraseña? <a href="/contraseña" class="bm-azul-pm hover:bg-verde-pm underline font-semibold">Recuperar Aqui</a></p>
                        </div>
                    </div>    
                    
                </div>
                <div class="w-1/2  h-screen justify-center shadow-2xl bg-azul-pm">
                    <img class="m-auto h-26 rounded place-self-center" src={Image}></img>
                </div>

            </div>

        </div>
    );
}

export default Home;