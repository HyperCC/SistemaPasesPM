import React, {useEffect, useState} from 'react'
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useHistory } from "react-router-dom";
import Popup from 'reactjs-popup';
import RutValidator from "w2-rut-validator";
import { DocumentosEmpresa } from './DocumentosEmpresa';
import moment from 'moment';

export const AgregarPersonaContratista = props => {

    let history = useHistory();
    const [contratoDate, setContratoDate] = useState(new Date());
    const [RIOHSDate, setRIOHSDate] = useState(new Date());
    const [ODIDate, setODIDate] = useState(new Date());
    const [EPPsDate, setEPPsDate] = useState(new Date());
    const [basicoDate, setBasicoDate] = useState(new Date());
    const [alturaDate, setAlturaDate] = useState(new Date());
    const [confinadosDate, setConfinadosDate] = useState(new Date());
    const [psicosensometricoDate, setPsicosensometricoDate] = useState(new Date());
    const [soldadorDate, setSoldadorDate] = useState(new Date());
    const [trabajadorAlturaDate, setTrabajadorAlturaDate] = useState(new Date());
    const [operadorDate, setOperadorDate] = useState(new Date());
    const [riggerDate, setRiggerDate] = useState(new Date());
    const [otrosDate, setOtrosDate] = useState(new Date());
    const [anexosDate, setAnexoDate] = useState(new Date());
    
    {/** Parte de manejo de los documentos */}
    const [documentoPersona, setDocumentoPersona] = useState([]);

    const [personaExterna, setPersonaExterna] = useState({
        Rut: '',
        Nombres: '',
        PrimerApellido: '',
        SegundoApellido: '',
        Nacionalidad: '',
    });

    const ingresarValoresMemoria = valorInput => {
        // obtener el valor
        const { name, value } = valorInput.target;

        // actualizar el valor de algun otro valor
        // asignar el valor
        setPersonaExterna(anterior => ({
            ...anterior, // mantener lo que existe antes
            [name]: value // solo cambiar el input mapeado
        }));
        
    };

    function singleFile(event){

        event.preventDefault();
        
        // prueba de codigo
        let idSimple;
        let filesPrueba = event.target.files;
        idSimple = event.target.id;
        let file_reader = new FileReader();
        var file = filesPrueba[0];

        // reading the actual uploaded file
        file_reader.readAsDataURL(file);

        file_reader.onload = () => {
            // After uploading the file
            // appending the file to our state array
            // set the object keys and values accordingly

             // Array para dejar solo la base 64 del archivo
             var arrayAux = [];
             arrayAux = file_reader.result.split(',');
 
             // variable para sacar la extension del archivo
             var extension = file.name.split('.').pop();
 
             // set the object keys and values accordingly
             setDocumentoPersona([...documentoPersona, {
                 Documento: arrayAux[1],
                 TipoDocumento: idSimple.toUpperCase(),
                 Obligariedad: true,
                 FechaVencimiento: "",
                 Extension: '.' + extension,
                 Descripcion: ""
             }]);
            
        };
    }

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "CONTRATO DE TRABAJO"
                    ? { ...documento, FechaVencimiento: moment(contratoDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [contratoDate]);

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "REGISTRO RIOHS"
                    ? { ...documento, FechaVencimiento: moment(RIOHSDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [RIOHSDate]);

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "REGISTRO ODI"
                    ? { ...documento, FechaVencimiento: moment(ODIDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [ODIDate]);

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "REGISTRO EPPS"
                    ? { ...documento, FechaVencimiento: moment(EPPsDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [EPPsDate]);

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "BASICO"
                    ? { ...documento, FechaVencimiento: moment(basicoDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [basicoDate]);

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "ALTURA FÍSICA"
                    ? { ...documento, FechaVencimiento: moment(alturaDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [alturaDate]);

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "ESPACIOS CONFINADOS"
                    ? { ...documento, FechaVencimiento: moment(confinadosDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [confinadosDate]);

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "PSICOSENSOMÉTRICO"
                    ? { ...documento, FechaVencimiento: moment(psicosensometricoDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [psicosensometricoDate]);

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "SOLDADOR CALIFICADO"
                    ? { ...documento, FechaVencimiento: moment(soldadorDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [soldadorDate]);

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "TRABAJO EN ALTURA"
                    ? { ...documento, FechaVencimiento: moment(trabajadorAlturaDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [trabajadorAlturaDate]);

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "OPERADOR DE EQUIPO MÓVIL"
                    ? { ...documento, FechaVencimiento: moment(operadorDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [operadorDate]);

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "RIGGER / PORTALONERO"
                    ? { ...documento, FechaVencimiento: moment(riggerDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [riggerDate]);   

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "OTROS"
                    ? { ...documento, FechaVencimiento: moment(otrosDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [otrosDate]);   

    useEffect(() => {
       
        setDocumentoPersona(
            documentoPersona.map( (documento) =>
            
                documento.TipoDocumento === "ANEXO"
                    ? { ...documento, FechaVencimiento: moment(anexosDate.toString()).format("DD/MM/YYYY")}
                    : { ...documento}
                
            )
        );

    }, [anexosDate]);     


    const sendData = () => { 

        //guardarFecha();

        //console.log(documentoPersona)

        props._guardarPersonaC(personaExterna, documentoPersona);
    };

    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">
                    <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
                        <p class="text-3xl leading-tight mx-8 text-center md:text-center md:ml-16">
                            Información Persona
                        </p>
                        

                            <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">
                                <div class="col-span-1 col-start-1 row-start-1"> <p>Rut</p> </div>
                                <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                                    <input type="text" value={personaExterna.Rut} onChange={ingresarValoresMemoria} name="Rut" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                                </div>

                                <div class="col-span-1 row-start-2"> <p>Nombres</p> </div>
                                <div class="col-span-1 md:col-span-1 row-start-2">
                                    <input type="text" value={personaExterna.Nombres} onChange={ingresarValoresMemoria} name="Nombres" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                                </div>

                                <div class="col-span-1 row-start-3"> <p>Apellido Paterno</p> </div>
                                <div class="col-span-1 row-start-3 md:col-span-1">
                                    <input type="text" value={personaExterna.PrimerApellido} onChange={ingresarValoresMemoria} name="PrimerApellido" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                                </div>
                                <div class="col-span-1 row-start-4"> <p>Contrato de Trabajo</p> </div>
                                <div class="col-span-1 row-start-4 md:col-span-1">
                                    <label for="Contrato de Trabajo" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Contrato de Trabajo" type="file" style={{display: "none"}}/>
                                    
                                </div>

                                <div class="col-span-1 col-start-3 row-start-1 pl-14"> <p>Nacionalidad</p> </div>
                                <div class="col-span-1 col-start-4 row-start-1 md:col-span-1">
                                    <input type="text" value={personaExterna.Nacionalidad} onChange={ingresarValoresMemoria} name="Nacionalidad" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                                </div>

                                <div><p></p></div>
                                <div></div>

                                <div class="col-span-1 col-start-3 row-start-3 pl-14"> <p>Apellido Materno</p> </div>
                                <div class="col-span-1 col-start-4 row-start-3 md:col-span-1 row-start-2">
                                    <input type="text" value={personaExterna.SegundoApellido} onChange={ingresarValoresMemoria} name="SegundoApellido" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                                </div>

                                <div class="col-span-1 col-start-3 row-start-4 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 row-start-4 md:col-span-1">
                                    <DatePicker selected={contratoDate} onChange={date => setContratoDate(date)} />
                                </div>                            
                            </div>
                            
                            <p class="text-2xl leading-tight py-6 mx-8 md:text-left md:ml-16">
                                Anexo de Contrato
                            </p>

                            <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">
                                <div class="col-span-1 col-start-1 row-start-1"> <p></p> </div>
                                <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                                    <Popup trigger={<button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">Agregar Anexo</button>} modal nested>
                                        { close => (
                                        <div className="modal">
                                           
                                            <div className="header"> Agregar Nuevo Anexo de Contrato </div>
                                            <div className="content">

                                                <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">
                                                    <div class="col-span-2 col-start-1 pl-14"> <p>Fecha Vencimiento</p> </div>
                                                    <div class="col-span-2 col-start-2 md:col-span-2">
                                                        <DatePicker selected={anexosDate} onChange={date => setAnexoDate(date)} />
                                                    </div>                
                                                    

                                                    <div class="col-span-2 col-start-1 pl-14"> <p>Contrato de Trabajo</p> </div>
                                                    <div class="col-span-2 col-start-2 md:col-span-2">
                                                        <label for="Anexo" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                                            Seleccionar archivo
                                                        </label>
                                                        <input onChange={singleFile} id="Anexo" type="file" style={{display: "none"}}/>
                                                    </div> 

                                                    <div class="col-span-1 row-span-2 col-start-1 pl-14"><p>Descripción</p></div>
                                                    <div class="col-span-3 row-span-2 col-start-2 "><textarea type="range" value="" onChange={ingresarValoresMemoria} name="Descripccion" placeholder="range...." class="border w-full app border-gray-300 p-2 my-2 rounded-md focus:outline-none focus:ring-2 ring-azul-pm"> </textarea></div>

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
                                                    Cerrar
                                                </button>

                                                <button
                                                    class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500"
                                                    onClick={() => {
                                                    console.log('modal closed ');
                                                    close();
                                                    }}
                                                >
                                                    Guardar
                                                </button>
                                            </div>
                                        </div>
                                        )}
                                    </Popup>
                                </div>
                            </div>    

                            {/** Parte de documentos */}

                            <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">

                                <div class="col-span-1 col-start-1 row-start-1"> <p>Registro RIOHS</p> </div>
                                <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                                    <label for="Registro RIOHS" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Registro RIOHS" type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-2"> <p>Registro ODI</p> </div>
                                <div class="col-span-1 md:col-span-1 row-start-2">
                                    <label for="Registro ODI" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Registro ODI" type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-3"> <p>Registro EPPs</p> </div>
                                <div class="col-span-1 row-start-3 md:col-span-1">
                                    <label for="Registro EPPs" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Registro EPPs" type="file" style={{display: "none"}}/>
                                </div>
                                
                                <div class="col-span-1 col-start-3 row-start-1 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-1 md:col-span-1">
                                    <DatePicker selected={RIOHSDate} onChange={date => setRIOHSDate(date)} />
                                </div>

                                <div class="col-span-1 col-start-3 row-start-2 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-2 md:col-span-1 row-start-2">
                                    <DatePicker selected={ODIDate} onChange={date => setODIDate(date)} />
                                </div>

                                <div class="col-span-1 col-start-3 row-start-3 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-3 md:col-span-1 row-start-2">
                                    <DatePicker selected={EPPsDate} onChange={date => setEPPsDate(date)} />    
                                </div>
                                    
                            </div>   
                            


                            {/** Parte examenes */}

                            <p class="text-2xl leading-tight py-6 mx-8 md:text-left md:ml-16">
                                Exámenes Pre-ocupacionales / Ocupacionales
                            </p>

                            <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">

                                <div class="col-span-1 col-start-1 row-start-1"> <p>Basico</p> </div>
                                <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                                    <label for="Basico" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Basico" type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-2"> <p>Altura Física</p> </div>
                                <div class="col-span-1 md:col-span-1 row-start-2">
                                    <label for="Altura Física" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Altura Física" type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-3"> <p>Espacios Confinados</p> </div>
                                <div class="col-span-1 row-start-3 md:col-span-1">
                                    <label for="Espacios Confinados" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Espacios Confinados" type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-4"> <p>Psicosensométrico</p> </div>
                                <div class="col-span-1 row-start-4 md:col-span-1">
                                    <label for="Psicosensométrico" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Psicosensométrico" type="file" style={{display: "none"}}/>
                                </div>
                                
                                <div class="col-span-1 col-start-3 row-start-1 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-1 md:col-span-1">
                                    <DatePicker selected={basicoDate} onChange={date => setBasicoDate(date)} /> 
                                </div>

                                <div class="col-span-1 col-start-3 row-start-2 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-2 md:col-span-1 row-start-2">
                                    <DatePicker selected={alturaDate} onChange={date => setAlturaDate(date)} /> 
                                </div>

                                <div class="col-span-1 col-start-3 row-start-3 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-3 md:col-span-1 row-start-2">
                                    <DatePicker selected={confinadosDate} onChange={date => setConfinadosDate(date)} /> 
                                </div>

                                <div class="col-span-1 col-start-3 row-start-4 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-4 md:col-span-1 row-start-2">
                                    <DatePicker selected={psicosensometricoDate} onChange={date => setPsicosensometricoDate(date)} /> 
                                </div>
                                    
                            </div>  

                            {/** Parte Competencias Certificaciones */}

                            <p class="text-2xl leading-tight py-6 mx-8 md:text-left md:ml-16">
                                Competencias / Certificación
                            </p>

                            <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">

                                <div class="col-span-1 col-start-1 row-start-1"> <p>Soldador Calificado</p> </div>
                                <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                                    <label for="Soldador Calificado" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Soldador Calificado" type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-2"> <p>Trabajo en Altura</p> </div>
                                <div class="col-span-1 md:col-span-1 row-start-2">
                                    <label for="Trabajo en Altura" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Trabajo en Altura" type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-3"> <p>Operador de Equipo Móvil</p> </div>
                                <div class="col-span-1 row-start-3 md:col-span-1">
                                    <label for="Operador de Equipo Móvil" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Operador de Equipo Móvil" type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-4"> <p>Rigger / Portalonero</p> </div>
                                <div class="col-span-1 row-start-4 md:col-span-1">
                                    <label for="Rigger / Portalonero" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Rigger / Portalonero" type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-5"> <p>Otros</p> </div>
                                <div class="col-span-1 row-start-5 md:col-span-1">
                                    <label for="Otros" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={singleFile} id="Otros" type="file" style={{display: "none"}}/>
                                </div>
                                
                                <div class="col-span-1 col-start-3 row-start-1 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-1 md:col-span-1">
                                    <DatePicker selected={soldadorDate} onChange={date => setSoldadorDate(date)} /> 
                                </div>

                                <div class="col-span-1 col-start-3 row-start-2 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-2 md:col-span-1 row-start-2">
                                    <DatePicker selected={trabajadorAlturaDate} onChange={date => setTrabajadorAlturaDate(date)} /> 
                                </div>

                                <div class="col-span-1 col-start-3 row-start-3 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-3 md:col-span-1 row-start-2">
                                    <DatePicker selected={operadorDate} onChange={date => setOperadorDate(date)} /> 
                                </div>

                                <div class="col-span-1 col-start-3 row-start-4 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-4 md:col-span-1 row-start-2">
                                    <DatePicker selected={riggerDate} onChange={date => setRiggerDate(date)} /> 
                                </div>

                                <div class="col-span-1 col-start-3 row-start-5 pl-14"> <p>Fecha Vencimiento</p> </div>
                                <div class="col-span-1 col-start-4 row-start-5 md:col-span-1 row-start-2">
                                    <DatePicker selected={otrosDate} onChange={date => setOtrosDate(date)} /> 
                                </div>
                                <input type="submit" id="submit-form" class="hidden"/>
                            </div>  
                        <div class="flex justify-between items-center py-8 p-4">
                            <button onClick={() => history.goBack()} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                                Cancelar
                            </button>

                            <button onClick={sendData} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                                Guardar
                            </button>
    
                        </div>
                    </div> 
                </div>
            </div>
        </div>
    )
}
