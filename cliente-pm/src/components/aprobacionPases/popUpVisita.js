import React from 'react';
import Popup from 'reactjs-popup';

const popUpVisita = props => {

    <Popup trigger={<button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500"> Aprobar</button>} modal nested>
        { close => (
        <div className="modal">
    
            <button className="close" onClick={close}>
            &times;
            </button>
            
            <div className="header"> Confirmación Pase de Visita</div>
            <div className="content">

                <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">
                    <p>Estimado usuario,</p> 
                    <p>Recuerde que el pase de visita se otorga para el ingreso a toda persona que no realizará
                        trabajo operacionales y en el sector de transito de visitas y administrativa.
                        Toda persona que deba ingresar a zonas operacionales como visita, debera contar con la
                        inducción breve que lo habilita para aquello</p>                   
                </div>
            </div>

            <div class="actions flex justify-between py-8 px-8">

                <button
                    class="bg-verde-pm pl-4 hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500"
                    onClick={() => {
                    console.log('modal closed ');
                    close();
                    }}
                >
                    Solo acceso visita
                </button>

                <button
                    class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500"
                    onClick={() => {
                    console.log('modal closed ');
                    close();
                    }}
                >
                    Visita a área operacional
                </button>
            </div>
        </div>
        )}
    </Popup>


}

export default popUpVisita;