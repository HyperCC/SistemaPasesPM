import React, {useState} from 'react'
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useHistory } from "react-router-dom";
import Popup from 'reactjs-popup';
import RutValidator from "w2-rut-validator"

export const AgregarPersonaContratista = () => {
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
    const [files, setFiles] = useState([]);

    const [datosPersona,setDatosPersona] = useState({
        Rut: '',
        Nombres: '',
        ApellidoPat: '',
        ApellidoMat: '',
        Nacionalidad: ''
    });

    const ingresarValoresMemoria = valorInput => {
        // obtener el valor
        const { name, value } = valorInput.target;

        if(name == "Rut"){
            if (!RutValidator.validate(value)){
                alert('Por favor ingrese el rut con el siguiente formato 11.111.111-1')
                setDatosPersona(anterior => ({
                    ...anterior, // mantener lo que existe antes
                    ['Rut']: '' // reseteamos el rut
                }));
                return;
            }
        }

        // actualizar el valor de algun otro valor
        // asignar el valor
        setDatosPersona(anterior => ({
            ...anterior, // mantener lo que existe antes
            [name]: value // solo cambiar el input mapeado
        }));
        
    };

    function onFileUpload(event) {
        event.preventDefault();
        // Get the file Id
        let id = event.target.id;
        // Create an instance of FileReader API
        let file_reader = new FileReader();
        // Get the actual file itself
        let file = event.target.files[0];
        
        file_reader.onload = () => {
            // After uploading the file
            // appending the file to our state array
            // set the object keys and values accordingly
            setFiles([...files, { file_id: id, uploaded_file: file_reader.result }]);
        };
        // reading the actual uploaded file
        file_reader.readAsDataURL(file);

        alert(
            `Selected file - ${file.name}`
          );
        }
    // handle submit button for form
    function handleSubmit(e) {
    e.preventDefault();
    console.log(files);
    }

    let history = useHistory();

    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">
                    <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
                        <p class="text-3xl leading-tight mx-8 text-center md:text-center md:ml-16">
                            Información Persona
                        </p>
                        <form onSubmit={handleSubmit} id="form" className="uploader" encType="multipart/form-data">

                            <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">
                                <div class="col-span-1 col-start-1 row-start-1"> <p>Rut</p> </div>
                                <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                                    <input type="text" value={datosPersona.Rut} onChange={ingresarValoresMemoria} name="Rut" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                                </div>

                                <div class="col-span-1 row-start-2"> <p>Nombres</p> </div>
                                <div class="col-span-1 md:col-span-1 row-start-2">
                                    <input type="text" value={datosPersona.Nombres} onChange={ingresarValoresMemoria} name="Nombres" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                                </div>

                                <div class="col-span-1 row-start-3"> <p>Apellido Paterno</p> </div>
                                <div class="col-span-1 row-start-3 md:col-span-1">
                                    <input type="text" value={datosPersona.ApellidoPat} onChange={ingresarValoresMemoria} name="ApellidoPaterno" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                                </div>
                                <div class="col-span-1 row-start-4"> <p>Contrato de Trabajo</p> </div>
                                <div class="col-span-1 row-start-4 md:col-span-1">
                                    <label for={0} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={0} type="file" style={{display: "none"}}/>
                                    
                                </div>

                                <div class="col-span-1 col-start-3 row-start-1 pl-14"> <p>Nacionalidad</p> </div>
                                <div class="col-span-1 col-start-4 row-start-1 md:col-span-1">
                                    <input type="text" value={datosPersona.Nacionalidad} onChange={ingresarValoresMemoria} name="Nacionalidad" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                                </div>

                                <div><p></p></div>
                                <div></div>

                                <div class="col-span-1 col-start-3 row-start-3 pl-14"> <p>Apellido Materno</p> </div>
                                <div class="col-span-1 col-start-4 row-start-3 md:col-span-1 row-start-2">
                                    <input type="text" value={datosPersona.ApellidoMat} onChange={ingresarValoresMemoria} name="Apellido Materno" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
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
                                    <Popup trigger={<button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500"> Agregar Anexo</button>} modal nested>
                                        { close => (
                                        <div className="modal">
                                            {/** 
                                            <button className="close" onClick={close}>
                                            &times;
                                            </button>
                                            */}
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
                                                        <input onChange={onFileUpload} id="Anexo" type="file" style={{display: "none"}}/>
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
                                    <label for={1} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={1} type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-2"> <p>Registro ODI</p> </div>
                                <div class="col-span-1 md:col-span-1 row-start-2">
                                    <label for={2} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={2} type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-3"> <p>Registro EPPs</p> </div>
                                <div class="col-span-1 row-start-3 md:col-span-1">
                                    <label for={3} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={3} type="file" style={{display: "none"}}/>
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
                                    <label for={4} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={4} type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-2"> <p>Altura Física</p> </div>
                                <div class="col-span-1 md:col-span-1 row-start-2">
                                    <label for={5} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={5} type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-3"> <p>Espacios Confinados</p> </div>
                                <div class="col-span-1 row-start-3 md:col-span-1">
                                    <label for={6} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={6} type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-4"> <p>Psicosensométrico</p> </div>
                                <div class="col-span-1 row-start-4 md:col-span-1">
                                    <label for={7} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={7} type="file" style={{display: "none"}}/>
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
                                    <label for={8} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={8} type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-2"> <p>Trabajo en Altura</p> </div>
                                <div class="col-span-1 md:col-span-1 row-start-2">
                                    <label for={9} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={9} type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-3"> <p>Operador de Equipo Móvil</p> </div>
                                <div class="col-span-1 row-start-3 md:col-span-1">
                                    <label for={10} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={10} type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-4"> <p>Rigger / Portalonero</p> </div>
                                <div class="col-span-1 row-start-4 md:col-span-1">
                                    <label for={11} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={11} type="file" style={{display: "none"}}/>
                                </div>

                                <div class="col-span-1 row-start-5"> <p>Otros</p> </div>
                                <div class="col-span-1 row-start-5 md:col-span-1">
                                    <label for={12} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                        Seleccionar archivo
                                    </label>
                                    <input onChange={onFileUpload} id={12} type="file" style={{display: "none"}}/>
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
                        </form>
                        <div class="flex justify-between items-center py-8 p-4">
                            <button onClick={() => history.goBack()} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                                Cancelar
                            </button>

                            <label for="submit-form" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">Guardar</label>
    
                        </div>
                    </div> 
                </div>
            </div>
        </div>
    )
}
