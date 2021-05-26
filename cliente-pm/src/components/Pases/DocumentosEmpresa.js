import React, { useState } from 'react'
import { useHistory } from "react-router-dom";

export const DocumentosEmpresa = () => {
    
    const [openTab, setOpenTab] = React.useState(1);
    let history = useHistory();

    const [datosPrev,setDatosPrev] = useState({
        Rut: '',
        Nombres: '',
        Apellidos: '',
        RegistroSNS: ''
    });

    // asignar nuevos valores al state del registro
    const ingresarValoresMemoria = valorInput => {
        // obtener el valor
        const { name, value } = valorInput.target;

        // actualizar el valor de algun otro valor
        // asignar el valor
        setDatosPrev(anterior => ({
            ...anterior, // mantener lo que existe antes
            [name]: value // solo cambiar el input mapeado
        }));
        
    };
    
    const [files, setFiles] = useState([]);
    
    function onFileUpload(event) {
        event.preventDefault();
        // Get the file Id
        // let id = event.target.id;
        // Create an instance of FileReader API
        // let file_reader = new FileReader();
        // Get the actual file itself
        // let file = event.target.files[0];
        
        // prueba de codigo
        let idSimple;
        let filesPrueba = event.target.files;
        let file;

        if (filesPrueba.length == 1){
            idSimple = event.target.id;
            let file_reader = new FileReader();
            file = filesPrueba[0];

            file_reader.onload = () => {
                // After uploading the file
                // appending the file to our state array
                // set the object keys and values accordingly
                setFiles([...files, { file_id: idSimple, uploaded_file: file_reader.result }]);
                };
                // reading the actual uploaded file
            file_reader.readAsDataURL(file);
        }else{
            idSimple = event.target.id;
            for (let i = 0; i < filesPrueba.length; i++){
                let idFinal = parseInt(idSimple,10) + i;
                let file_reader = new FileReader();
                file = filesPrueba[i];
    
                file_reader.onloadend = () => {
                    // After uploading the file
                    // appending the file to our state array
                    // set the object keys and values accordingly
                    setFiles([...files, { file_id: idFinal, uploaded_file: file_reader.result }]);
                    };
                    // reading the actual uploaded file
                file_reader.readAsDataURL(file);
            }
        }
        
    }
    // handle submit button for form
    function handleSubmit(e) {
        e.preventDefault();
        console.log(files);
    }

    return (
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">
                
                <div className="bg-white p-4 md:p-8 rounded-lg shadow-md">
                    <div class="text-center">
                        <p class="text-3xl text-grey-darkest pt-4 py-2 w-full select-none">
                            Documentación Empresa
                        </p>
                    </div>
                    <div className="w-full">
                    <ul
                        className="flex mb-0 list-none flex-wrap pt-3 pb-4 flex-row"
                        role="tablist"
                    >
                        <li className="-mb-px mr-2 last:mr-0 flex-auto text-center">
                        <a
                            className={
                            "text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
                            (openTab === 1
                                ? "text-white bg-verde-pm"
                                : "text-white bg-amarillo-pm")
                            }
                            onClick={e => {
                            e.preventDefault();
                            setOpenTab(1);
                            }}
                            data-toggle="tab"
                            href="#link1"
                            role="tablist"
                        >
                            General y Legal
                        </a>
                        </li>
                        <li className="-mb-px mr-2 last:mr-0 flex-auto text-center">
                        <a
                            className={
                            "text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
                            (openTab === 2
                                ? "text-white bg-verde-pm"
                                : "text-white bg-amarillo-pm")
                            }
                            onClick={e => {
                            e.preventDefault();
                            setOpenTab(2);
                            }}
                            data-toggle="tab"
                            href="#link2"
                            role="tablist"
                        >
                            Asesor de Prevención de Riesgos
                        </a>
                        </li>
                        <li className="-mb-px mr-2 last:mr-0 flex-auto text-center">
                        <a
                            className={
                            "text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
                            (openTab === 3
                                ? "text-white bg-verde-pm"
                                : "text-white bg-amarillo-pm")
                            }
                            onClick={e => {
                            e.preventDefault();
                            setOpenTab(3);
                            }}
                            data-toggle="tab"
                            href="#link3"
                            role="tablist"
                        >
                            Gestión de Riesgos
                        </a>
                        </li>
                    </ul>
                    <div className="relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded">
                        <div className="px-4 py-5 flex-auto">
                        <div className="tab-content tab-space">
                            <div className={openTab === 1 ? "block" : "hidden"} id="link1">
                                <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-4">
                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="CronogramaTrabajo">Cronograma de Trabajo</label>
                                    </div>
                                    <div class="col-span-2">
                                        <label for={1} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                            Seleccionar archivo
                                        </label>
                                        <input onChange={onFileUpload} id={1} type="file" style={{display: "none"}}/>
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="CertificadoMutualidad">Certificado de Mutualidad</label>
                                    </div>
                                    <div class="col-span-2">
                                        <label for={2} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                            Seleccionar archivo
                                        </label>
                                        <input onChange={onFileUpload} id={2} type="file" style={{display: "none"}}/>
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="CertificadoAccidentabilidad">Certificado de Accidentabilidad</label>
                                    </div>
                                    <div class="col-span-2">
                                        <label for={3} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                            Seleccionar archivo
                                        </label>
                                        <input onChange={onFileUpload} id={3} type="file" style={{display: "none"}}/>
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="ReglamentoInterno">Reglamento Interno</label>
                                    </div>
                                    <div class="col-span-2">
                                        <label for={4} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                            Seleccionar archivo
                                        </label>
                                        <input onChange={onFileUpload} id={4} type="file" style={{display: "none"}}/>
                                    </div>
                                
                                </div>
                            </div>
                            <div className={openTab === 2 ? "block" : "hidden"} id="link2">

                                <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-4">
                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="Nombres">Nombres</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" value={datosPrev.Nombres} onChange={ingresarValoresMemoria} name="Nombres" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="Apellidos">Apellidos</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" value={datosPrev.Apellidos} onChange={ingresarValoresMemoria} name="Apellidos" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="Rut">Rut</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" value={datosPrev.Rut} onChange={ingresarValoresMemoria} name="Rut" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="RegistroSNS">Registro SNS</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" value={datosPrev.RegistroSNS} onChange={ingresarValoresMemoria} name="RegistroSNS" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>
                                
                                </div>

                            </div>
                            <div className={openTab === 3 ? "block" : "hidden"} id="link3">

                                <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-4">
                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="MatrizRiesgo">Matriz de Riesgos</label>
                                    </div>
                                    <div class="col-span-2">
                                        <label for={5} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                            Seleccionar archivo
                                        </label>
                                        <input onChange={onFileUpload} id={5} type="file" style={{display: "none"}}/>
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="ProcedimientoTrabajo">Procedimiento Trabajo Seguro</label>
                                    </div>
                                    <div class="col-span-2">
                                        <label for={7} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                            Seleccionar archivo
                                        </label>
                                        <input onChange={onFileUpload} id={7} multiple type="file" style={{display: "none"}}/>
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="ProgramaPrevencion">Programa Prevención de Riesgos</label>
                                    </div>
                                    <div class="col-span-2">
                                        <label for={6} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                            Seleccionar archivo
                                        </label>
                                        <input onChange={onFileUpload} id={6} type="file" style={{display: "none"}}/>
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="HDS">HDS Sustancias Peligrosas</label>
                                    </div>
                                    <div class="col-span-2">
                                        <label for={14} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">
                                            Seleccionar archivo
                                        </label>
                                        <input onChange={onFileUpload} id={14} type="file" multiple style={{display: "none"}}/>
                                    </div>
                            
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                    <div class="flex justify-between items-center p-4">
                        <button onClick={() => history.goBack()} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                            Cancelar
                        </button>

                        <button type="submit" onClick={handleSubmit} class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                            Guardar
                        </button>
                    </div>
                    </div>
                </div>

                </div>
            </div>
        </div>
    )
}
