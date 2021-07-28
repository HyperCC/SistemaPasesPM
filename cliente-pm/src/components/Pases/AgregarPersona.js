import React, { useEffect, useState } from 'react';
import { useHistory } from "react-router-dom";
import RutValidator from "w2-rut-validator";


const AgregarPersona = props => {

    const history = useHistory();

    // datos a guardar para el form
    const [personaExterna, setPersonaExterna] = useState({
        Nombres: '',
        PrimerApellido: '',
        SegundoApellido: '',
        Rut: '',
        Pasaporte: '',
        Nacionalidad: '',
        DocumentosInduccion: []
    });

    // cambio por radiobutton para el rut o pasaporte
    const [isRut, setIsRut] = useState(true);
    const changeIsRut = () => setIsRut(true);
    const changeNotIsRut = () => setIsRut(false);
    const [paises, setPaises] = useState([]);
    const [documentosInduccion, setDocumentosInduccion] = useState([]);

    // consumir la api de los paises
    useEffect(() => {
        fetch('https://sccnlp-piloto.dirtrab.cl/api/Mantenedor/getNacionalidad')
            .then((response) => response.json())
            .then((data) =>
                setPaises(data));
    }, []);


    // asignar nuevos valores al state del registro
    const ingresarValoresMemoria = valorInput => {
        // obtener el valor
        const { name, value } = valorInput.target;

        // asignar el valor
        setPersonaExterna(anterior => ({
            ...anterior, // mantener lo que existe antes
            [name]: value // solo cambiar el input mapeado
        }));
    };

    // agregar documento induccion
    const handleMultiFileChosen = async (file) => {
        return new Promise((resolve, reject) => {
            let reader = new FileReader();
            reader.onload = () => {
                resolve(reader.result)
            };
            reader.onerror = reject;
            reader.readAsDataURL(file);
            console.log(file)
        })
    }

    let multipleFile = async (event) => {
        event.preventDefault();

        // prueba de codigo
        let id = event.target.id
        console.log(id)
        let newFile = [];

        const results = await Promise.all(Array.from(event.target.files).map(async (file) => {

            const fileContents = await handleMultiFileChosen(file);
            // Array para dejar solo la base 64 del archivo
            var arrayAux = [];
            arrayAux = fileContents.split(',');

            // variable para sacar la extension del archivo
            var extension = file.name.split('.').pop();

            newFile.push({
                Documento: arrayAux[1],
                TipoDocumento: id.toUpperCase(),
                Obligatoriedad: false,
                FechaVencimiento: "",
                Extension: '.' + extension
            })
            //console.log(newFile);
        }))

        setDocumentosInduccion(...documentosInduccion, newFile);
        //console.log(results, "results")
        console.log(documentosInduccion)
    }

    // actualizar DocumentosInduccion del usaurio
    useEffect(() => {
        setPersonaExterna(anterior => ({
            ...anterior, // mantener lo que existe antes
            ['DocumentosInduccion']: documentosInduccion // solo cambiar el input mapeado
        }));
    }, [documentosInduccion]);

    // enviar persona completa para almacenarlo
    const GuardarUnaPersona = infoFormulario => {

        if (personaExterna.Pasaporte.length === 0)
            if (!RutValidator.validate(personaExterna.Rut)) {
                alert('Por favor ingrese el rut con el siguiente formato: 11222333-1')
                setPersonaExterna(anterior => ({
                    ...anterior, // mantener lo que existe antes
                    ['Rut']: '' // reseteamos el rut
                }));
                return;
            }

        infoFormulario.preventDefault();
        //window.alert("The URL of this page is: " + window.location.hostname + ":" + window.location.port);

        const currentLocation = window.location.href;

        if (currentLocation.includes('SolicitudVisita')) {
            window.localStorage.setItem('nueva_persona_externa_visita', JSON.stringify(personaExterna));
            history.push("/SolicitudVisita");

        } else if (currentLocation.includes('SolicitudContratista')) {
            window.localStorage.setItem('nueva_persona_externa_contratista', JSON.stringify(personaExterna));
            history.push("/SolicitudContratista");

        } else if (currentLocation.includes('SolicitudProveedor')) {
            window.localStorage.setItem('nueva_persona_externa_proveedor', JSON.stringify(personaExterna));
            history.push("/SolicitudProveedor");

        } else if (currentLocation.includes('SolicitudUsoDeMuelle')) {
            window.localStorage.setItem('nueva_persona_externa_uso_muelle', JSON.stringify(personaExterna));
            history.push("/SolicitudUsoDeMuelle");

        } else if (currentLocation.includes('SolicitudTripulante')) {
            window.localStorage.setItem('nueva_persona_externa_tripulante', JSON.stringify(personaExterna));
            history.push("/SolicitudTripulante");

        }

        //props._guardarPersonaExterna(personaExterna);
        //window.localStorage.setItem('nueva_persona_externa', JSON.stringify(personaExterna));
        props.cerrarModal()
    };

    return (
        <div>
            <div class="bg-gray-100 min-h-screen">
                <div class="sm:py-16 sm:px-6 px-2 py-8">
                    <div class="max-w-xl mx-auto sm:px-6 px-4 pb-12 bg-white rounded-lg shadow-md">

                        <div class="text-center">
                            <p class="text-3xl text-grey-darkest pt-12 w-full select-none">
                                Agregar Persona {props.faker}
                            </p>
                        </div>

                        <div>
                            {/* DATOS PRINCIPALES PARA TRABAJADOR */}
                            <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-12">

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="Nombres">Nombres</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={personaExterna.Nombres} onChange={ingresarValoresMemoria}
                                        name="Nombres" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="PrimerApellido">Primer Apellido</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={personaExterna.PrimerApellido} onChange={ingresarValoresMemoria}
                                        name="PrimerApellido" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="SegundoApellido">Segundo Apellido</label>
                                </div>
                                <div class="col-span-2">
                                    <input type="text" value={personaExterna.SegundoApellido} onChange={ingresarValoresMemoria}
                                        name="SegundoApellido" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" />
                                </div>

                                {/* eleccion de utilizacion de rut o pasaporte*/}
                                <div class="col-span-1 form-group">
                                    <div onClick={changeIsRut}>
                                        <label class="inline-flex items-center">
                                            <input type="radio" class="form-radio text-indigo-600 h-4 w-4" name="radio-colors" value="1" checked={isRut} />
                                            <span class="ml-2">Rut</span>
                                        </label>
                                    </div>

                                    <div onClick={changeNotIsRut}>
                                        <label class="inline-flex items-center">
                                            <input type="radio" class="form-radio text-green-500 h-4 w-4" name="radio-colors" value="2" />
                                            <span class="ml-2">Pasaporte</span>
                                        </label>
                                    </div>
                                </div>

                                <div class="col-span-2" >
                                    {isRut
                                        ? <input placeholder="ingrese el rut" type="text" name="Rut" value={personaExterna.Rut}
                                            onChange={ingresarValoresMemoria} class="w-full border-2 py-1 px-3 border-gray-200 rounded-md" />
                                        : <input placeholder="ingrese el pasaporte" type="text" name="Pasaporte" value={personaExterna.Pasaporte}
                                            onChange={ingresarValoresMemoria} class="w-full border-2 py-1 px-3 border-gray-200 rounded-md" />
                                    }
                                </div >

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none" for="Nacionalidad">Nacionalidad</label>
                                </div>
                                <div class="col-span-2">
                                    <div>
                                        <select name="Nacionalidad" class="w-full border-2 py-1 px-3 border-gray-200 lowercase rounded-md  bg-gray-100" value={personaExterna.Nacionalidad} onChange={ingresarValoresMemoria}>
                                            {paises ?
                                                paises.length > 0 ?
                                                    paises.map((value, index) => {
                                                        return <option value={value.gentilicio} key={index}>{value.gentilicio}</option>
                                                    })
                                                    : null
                                                : null}
                                        </select>
                                    </div>
                                </div>

                                <div class="col-span-1 form-group">
                                    <label class="font-light text-gray-800 select-none">Archivo de induccion</label>
                                </div>

                                <div class="col-span-2">
                                    <label for="Documento Induccion" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Subir archivo
                                    </label>
                                    <input onChange={multipleFile} id="Documento Induccion" multiple type="file" style={{ display: "none" }} />
                                </div>

                            </div>


                            {/* ENVIAR DATOS */}
                            <div class="mt-12 flex justify-center">
                                <button type="submit" onClick={GuardarUnaPersona}
                                    class="bg-azul-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                                    Guardar
                                </button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    );
};

export default AgregarPersona;