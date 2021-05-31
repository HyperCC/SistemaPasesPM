import React, { useState } from 'react';

// notificacion en caso de operacion exitosa
export const NotificacionExito = contenido => {

    // controles del cierre de la notificacion flotante
    const [hideErrorExito, setHideErrorExito] = useState(false);

    function ButtonHideExito() {
        setHideErrorExito(true);
    }

    return (
        <div class="m-4 max-w-xl mx-auto">
            <div class=" text-center">
                <div class={hideErrorExito ? "hidden" : "p-2 bg-green-500 items-center relative text-gray-100 leading-none rounded-full flex lg:inline-flex"} role="alert">
                    <span class="flex rounded-full bg-green-400 uppercase px-2 py-1 text-xs font-bold mr-3">Exito</span>
                    <span class="font-semibold mr-2 text-left flex-auto">{contenido.texto}</span>
                    <span class="px-3">
                        <i class="bi bi-x text-xl fill-current text-white" onClick={ButtonHideExito} role="button"></i>
                    </span>
                </div>
            </div>
        </div>
    );
}

// notificacion de informacion de estado actual 
export const NotificacionInformacion = contenido => {

    // controles del cierre de la notificacion flotante
    const [hideErrorInfo, setHideErrorInfo] = useState(false);

    function ButtonHideInfo() {
        setHideErrorInfo(true);
    }

    return (
        <div class="m-4 max-w-xl mx-auto">
            <div class=" text-center">
                <div class={hideErrorInfo ? "hidden" : "p-2 bg-indigo-700 items-center relative text-gray-100 leading-none rounded-full flex lg:inline-flex"} role="alert">
                    <span class="flex rounded-full bg-indigo-500 uppercase px-2 py-1 text-xs font-bold mr-3">Info</span>

                    <span>
                        <img class="animate-spin" src="https://img.icons8.com/ios-glyphs/30/ffffff/spinner-frame-5.png" />
                    </span>

                    <span class="font-semibold mx-2 text-left flex-auto">{contenido.texto}</span>
                    <span class="px-3">
                        <i class="bi bi-x text-xl fill-current text-white" onClick={ButtonHideInfo} role="button"></i>
                    </span>
                </div>
            </div>
        </div>
    );
}

// notificacion en caso de errores del usaurio o avisos importantes para el usuaio
export const NotificacionAdvertencia = contenido => {

    // controles del cierre de la notificacion flotante
    const [hideErrorAdv, setHideErrorAdv] = useState(false);

    function ButtonHideAdv() {
        setHideErrorAdv(true);
    }

    return (
        <div class="m-4 max-w-xl mx-auto">
            <div class="text-center">
                <div class={hideErrorAdv ? "hidden" : "p-2 bg-yellow-300 items-center relative text-gray-900 leading-none rounded-full flex lg:inline-flex"} role="alert">
                    <span class="flex rounded-full bg-yellow-200 uppercase px-2 py-1 text-xs font-bold mr-3">Validacion</span>
                    <p class="font-semibold mr-2 text-left flex-auto">{contenido.texto}</p>
                    <span class="px-3">
                        <i class="bi bi-x text-xl fill-current text-black" onClick={ButtonHideAdv} role="button"></i>
                    </span>
                </div>
            </div>
        </div>
    );
}

// notificacion en caso que la plataforma no responda como se espera
export const NotificacionError = contenido => {

    // controles del cierre de la notificacion flotante
    const [hideErrorError, setHideErrorError] = useState(false);

    function ButtonHideError() {
        setHideErrorError(true);
    }

    return (
        <div class="m-4 max-w-xl mx-auto">
            <div class=" text-center">
                <div class={hideErrorError ? "hidden" : "p-2 bg-red-600 items-center relative text-gray-100 leading-none rounded-full flex lg:inline-flex"} role="alert">
                    <span class="flex rounded-full bg-red-400 uppercase px-2 py-1 text-xs font-bold mr-3">Error</span>
                    <span class="font-semibold mr-2 text-left flex-auto">{contenido.texto}</span>
                    <span class="px-3">
                        <i class="bi bi-x text-xl fill-current text-white" onClick={ButtonHideError} role="button"></i>
                    </span>
                </div>
            </div>
        </div>
    );
}

