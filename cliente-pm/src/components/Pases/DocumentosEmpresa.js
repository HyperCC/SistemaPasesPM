import React from 'react'

export const DocumentosEmpresa = () => {
    
    const [openTab, setOpenTab] = React.useState(1);

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
                            Asesor de Prevención
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
                            Riesgos
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
                                        <input type="text" name="CronogramaTrabajo" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="CertificadoMutualidad">Certificado de Mutualidad</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" name="CertificadoMutualidad" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="CertificadoAccidentabilidad">Certificado de Accidentabilidad</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" name="CertificadoAccidentabilidad" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="ReglamentoInterno">Reglamento Interno</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" name="ReglamentoInterno" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>
                                
                                </div>
                            </div>
                            <div className={openTab === 2 ? "block" : "hidden"} id="link2">

                                <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-4">
                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="Nombres">Nombres</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" name="Nombres" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="Apellidos">Apellidos</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" name="Apellidos" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="Rut">Rut</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" name="Rut" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="RegistroSNS">Registro SNS</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" name="RegistroSNS" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>
                                
                                </div>

                            </div>
                            <div className={openTab === 3 ? "block" : "hidden"} id="link3">

                                <div class="grid grid-cols-3 px-4 md:px-8 gap-4 mt-4">
                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="MatrizRiesgo">Matriz de Riesgos</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" name="MatrizRiesgo" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="ProcedimientoTrabajo">Procedimiento Trabajo Seguro</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" name="ProcedimientoTrabajo" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="ProgramaPrevencion">Programa Prevención de Riesgos</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" name="ProgramaPrevencion" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>

                                    <div class="col-span-1 form-group">
                                        <label class="text-gray-800 select-none" for="HDS">HDS Sustancias Peligrosas</label>
                                    </div>
                                    <div class="col-span-2">
                                        <input type="text" name="HDS" class="w-full border-2 py-1 px-3 border-gray-200 rounded-md bg-gray-100" autoFocus />
                                    </div>
                            
                                </div>
                            </div>
                        </div>
                        </div>
                    </div>
                    <div class="flex justify-between items-center p-4">
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
        </div>
    )
}
