import React, { useEffect, useState } from 'react';
import axios from 'axios';

export default function Paises(propiedades) {

    // datos obtenidos por la API
    const [paises, setPaises] = useState([]);
    // estado actual de la estraccion de datos
    const [currentStatus = false, setCurrentStatus] = useState([]);

    useEffect(() => {

        // obtener los datos desde 
        axios.get('https://restcountries.eu/rest/v2/all') // api con paises
            // resultado tiene todos los datos obtenidos 
            .then(resultado => {
                // guardar los paises
                setPaises(resultado.data);
                // verificar la obtencion de todos los datos
                setCurrentStatus(true);
            })
            .catch(error => {
                // verificar finalizacion de la operacion
                setCurrentStatus(true);
                console.log("Error durante la obtencion de los datos desde la API..", error);
            });

        //utilizar una sola vez este useEffect
    }, []);

    // impresion de los resultados 
    if (currentStatus) {
        return (
            <div>
                <ul>
                    {paises.map((pais, indice) =>
                        <li key={indice}> {pais.name} </li>
                    )}
                </ul>
            </div>
        );

    } else {
        return (
            <p>Se estan cargando los valores del web service..</p>
        )
    }
}

