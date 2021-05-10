import React, {useState} from 'react'
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

export const AgregarPersonaContratista = () => {
    const [startDate, setStartDate] = useState(new Date());

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
                                <input type="text" name="Rut" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-2"> <p>Nombres</p> </div>
                            <div class="col-span-1 md:col-span-1 row-start-2">
                                <input type="text" name="Nomres" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-3"> <p>Apellido Paterno</p> </div>
                            <div class="col-span-1 row-start-3 md:col-span-1">
                                <input type="text" name="ApellidoPaterno" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                            </div>
                            <div class="col-span-1 row-start-4"> <p>Contrato de Trabajo</p> </div>
                            <div class="col-span-1 row-start-4 md:col-span-1">
                                <input type="text" name="Contrato Trabajo" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-1"> <p>Nacionalidad</p> </div>
                            <div class="col-span-1 col-start-4 row-start-1 md:col-span-1">
                                <input type="text" name="Nacionalidad" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-2"> <p>Apellido Materno</p> </div>
                            <div class="col-span-1 col-start-4 row-start-2 md:col-span-1 row-start-2">
                                <input type="text" name="Apellido Materno" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-3"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 row-start-3 md:col-span-1">
                                <DatePicker selected={startDate} onChange={date => setStartDate(date)} />
                            </div>                            
                        </div>
                        
                        <p class="text-2xl leading-tight py-6 mx-8 md:text-left md:ml-16">
                            Anexo de Contrato
                        </p>

                        <div>


                        </div>    

                        {/** Parte de documentos */}

                        <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">

                            <div class="col-span-1 col-start-1 row-start-1"> <p>Registro RIOHS</p> </div>
                            <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                                <input type="text" name="RegistroRIOHS" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-2"> <p>Registro ODI</p> </div>
                            <div class="col-span-1 md:col-span-1 row-start-2">
                                <input type="text" name="RegistroODI" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-3"> <p>Registro EPPs</p> </div>
                            <div class="col-span-1 row-start-3 md:col-span-1">
                                <input type="text" name="RegistroEPPs" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                            </div>
                            
                            <div class="col-span-1 col-start-3 row-start-1"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-1 md:col-span-1">
                                <input type="text" name="FechaRIOHS" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-2"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-2 md:col-span-1 row-start-2">
                                <input type="text" name="FechaODI" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-3"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-3 md:col-span-1 row-start-2">
                                <input type="text" name="FechaEPPs" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>
                                
                        </div>   
                         


                        {/** Parte examenes */}

                        <p class="text-2xl leading-tight py-6 mx-8 md:text-left md:ml-16">
                            Exámenes Pre-ocupacionales / Ocupacionales
                        </p>

                        <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">

                            <div class="col-span-1 col-start-1 row-start-1"> <p>Basico</p> </div>
                            <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                                <input type="text" name="Basico" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-2"> <p>Altura Física</p> </div>
                            <div class="col-span-1 md:col-span-1 row-start-2">
                                <input type="text" name="AlturaFisica" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-3"> <p>Espacios Confinados</p> </div>
                            <div class="col-span-1 row-start-3 md:col-span-1">
                                <input type="text" name="EspaciosConfinados" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-4"> <p>Psicosensométrico</p> </div>
                            <div class="col-span-1 row-start-4 md:col-span-1">
                                <input type="text" name="Psicosensométrico" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                            </div>
                            
                            <div class="col-span-1 col-start-3 row-start-1"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-1 md:col-span-1">
                                <input type="text" name="FechaBasico" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-2"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-2 md:col-span-1 row-start-2">
                                <input type="text" name="FechaAltura" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-3"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-3 md:col-span-1 row-start-2">
                                <input type="text" name="FechaConfinados" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-4"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-4 md:col-span-1 row-start-2">
                                <input type="text" name="FechaPsicosensometrico" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>
                                
                        </div>  

                        {/** Parte Competencias Certificaciones */}

                        <p class="text-2xl leading-tight py-6 mx-8 md:text-left md:ml-16">
                            Competencias / Certificación
                        </p>

                        <div class="grid grid-cols-4 gap-4 md:grid-cols-4 mt-6 mx-8 mb-2 md:mb-0">

                            <div class="col-span-1 col-start-1 row-start-1"> <p>Soldador Calificado</p> </div>
                            <div class="col-span-1 col-start-2 row-start-1 md:col-span-1">
                                <input type="text" name="SoldadorCalificado" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-2"> <p>Trabajo en Altura</p> </div>
                            <div class="col-span-1 md:col-span-1 row-start-2">
                                <input type="text" name="TrabajoAltura" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-3"> <p>Operador de Equipo Móvil</p> </div>
                            <div class="col-span-1 row-start-3 md:col-span-1">
                                <input type="text" name="OperadorMovil" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-4"> <p>Rigger / Portalonero</p> </div>
                            <div class="col-span-1 row-start-4 md:col-span-1">
                                <input type="text" name="Rigger" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 row-start-5"> <p>Otros</p> </div>
                            <div class="col-span-1 row-start-5 md:col-span-1">
                                <input type="text" name="Otros" class="border-2 py-1 px-3 border-gray-300 rounded-md" />
                            </div>
                            
                            <div class="col-span-1 col-start-3 row-start-1"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-1 md:col-span-1">
                                <input type="text" name="FechaSoldador" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-2"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-2 md:col-span-1 row-start-2">
                                <input type="text" name="FechaTrabajoAltura" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-3"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-3 md:col-span-1 row-start-2">
                                <input type="text" name="FechaOperadorMovil" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-4"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-4 md:col-span-1 row-start-2">
                                <input type="text" name="FechaRigger" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>

                            <div class="col-span-1 col-start-3 row-start-5"> <p>Fecha Vencimiento</p> </div>
                            <div class="col-span-1 col-start-4 row-start-5 md:col-span-1 row-start-2">
                                <input type="text" name="FechaOtros" class="border-2  py-1 px-3 border-gray-300 rounded-md" />
                            </div>
                                
                        </div>  

                        <div class="flex justify-between items-center py-8 p-4">
                        <button type="submit" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                            Cancelar
                        </button>

                        <button type="submit" class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                            Guardar
                        </button>
                    </div>

                    </div> 
                </div>
            </div>
        </div>
    )
}
