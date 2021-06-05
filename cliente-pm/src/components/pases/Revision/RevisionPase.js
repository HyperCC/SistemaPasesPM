import React from 'react'
import { InformacionPase } from './InformacionPases'
import { ListaPersonas } from './ListaPesonas'
import { useHistory, useLocation, withRouter } from "react-router-dom";

export const RevisionPase = props => {
    let data = useLocation();
    console.log("aqui estan los datos")
    console.log(data.state)
    let history = useHistory();
    let TITULO = data.state.tipo;
    
    let pase = {
        fechaInicio: data.state.fechaInicio,
        fechaInicio: data.state.fechaInicio,
        fechaTermino: data.state.fechaTermino,
        motivo: data.state.motivo,
        area: data.state.area,
        tipo: data.state.tipo,
        estado: data.state.estado
        }

    return(
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">

                    {/** Parte superior de la vista */}
                    <InformacionPase _dataPaseGeneral={pase} tituloPase={TITULO} />

                    <div class="h-8"></div>

                    {/** Parte inferior tabla de personas */}
                    <ListaPersonas datos={data.state.personas} url={URL} />

                </div>
            </div>
        </div>
    )

}
