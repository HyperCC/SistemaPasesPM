import React, { useState } from 'react'
import { useHistory , useLocation } from 'react-router-dom';
import File from '../../../files/CARTA_CONSENTIMIENTO_plantilla[3801].docx';
import File2 from '../../../files/CARTA.pdf';
import File3 from '../../../files/VRA_17_2021.jpg';
import File4 from '../../../files/Excel.xlsx';
import File5 from '../../../files/sample.pdf';
import Popup from 'reactjs-popup';

import { Document, Page, pdfjs } from 'react-pdf';
import FileViewer from 'react-file-viewer';

pdfjs.GlobalWorkerOptions.workerSrc = `//cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.js`;

export const ArchivosEmpresa = props => {

    {/**  */}

    const [numPages, setNumPages] = useState(null);
    const [pageNumber, setPageNumber] = useState(1);
    let data = useLocation();
    let history = useHistory();

    function onDocumentLoadSuccess({ numPages }) {
        setNumPages(numPages);
    }

    function nextPage() {
        setPageNumber(pageNumber+1)
    }

    function backPage(){
        setPageNumber(pageNumber-1)
    }

    function base64DecodeUnicode(str) {
        // Convert Base64 encoded bytes to percent-encoding, and then get the original string.
        var percentEncodedStr = atob(str).split("").map(function(c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join('');

        function fixedEncodeURIComponent(str) {
            return encodeURIComponent(str).replace(/[!'()*]/g, function(c) {
                return '%' + c.charCodeAt(0).toString(16);
            });
        }    

        percentEncodedStr = fixedEncodeURIComponent(percentEncodedStr)

        return decodeURIComponent(percentEncodedStr);
    }

    //const files = data.state.documentosEmpresa;

    //console.log(files, "aqui")

    //var FilePrueba = base64DecodeUnicode(data.state.documentosEmpresa[0].documentoBase64)

    //const file = FilePrueba
    //var fileExtension = data.state.documentosEmpresa[0].extension.split('.').pop();
    //const type = fileExtension;
    //const history = useHistory();
    //console.log(type)
    //console.log(file)

    //var urlFile = "data:application/"+type+";base64,"+data.state.documentosEmpresa[0].documentoBase64;

    

    //const file = File
    // var fileExtension = file.split('.').pop();
    //const type = fileExtension;
    
    return(
        
        <div class="bg-gray-100 min-h-screen">
            <div class="md:max-w-6xl w-full mx-auto py-8">
                <div class="sm:px-8 px-4">
                    <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
                        <p class="text-2xl leading-tight mx-8 text-center md:text-left md:ml-16">
                            Documentos Empresa
                        </p>

                        <div class="mt-6 mx-0 md:mx-8 mb-2 md:mb-1 overflow-x-auto shadow-md rounded-lg">
                            <div class="inline-block min-w-full overflow-hidden">
                                <table class="min-w-full leading-normal">
                                    <thead>
                                        {/* HEADERS PARA LA TABLA */}
                                        <tr class="bg-azul-pm select-none text-sm uppercase text-gray-100 text-center border-b border-gray-200">
                                            <th scope="col" class="px-5 py-3 font-normal">
                                                Nombre Documento
                                            </th>
                                            <th scope="col" class="px-5 py-3 font-normal">
                                                Documento online
                                            </th>
                                            <th scope="col" class="px-5 py-3 font-normal">
                                                Descargar Documento
                                            </th>
                                            <th scope="col" class="px-5 py-3 font-normal">
                                                Fecha vencimiento
                                            </th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        {/* CICLO FOR CON TODOS LOS DATOS PARA CADA PASE */}
                                        {data.state.documentosEmpresa ?
                                            data.state.documentosEmpresa.length > 0 ?
                                            data.state.documentosEmpresa.map((value, index) => {
                                                    return <tr key={index} class={index % 2 == 0 ? "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap"
                                                        : "text-center border-b border-gray-200 text-sm text-gray-800 whitespace-nowrap bg-gray-100"} >

                                                        <td class="p-4">
                                                            {value.tituloDocumento}
                                                        </td>
                                                        <td class="p-4">
                                                            
                                                            {value.extension.split('.').pop() !="pdf" &&
                                                                <Popup trigger={<button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">Ver archivo</button>} modal nested>
                                                                {close => (
                                                                    <div className="modal">
                                                                    
                                                                        <button className="close" onClick={close}>
                                                                            &times;
                                                                        </button>

                                                                        <FileViewer
                                                                        fileType={value.extension.split('.').pop()}
                                                                        filePath={"data:application/"+value.extension.split('.').pop()+";base64,"+value.documentoBase64}
                                                                        />
                                                                        
                                                                    </div>
                                                                    
                                                                )}
                                                                </Popup>
                                                                
                                                            
                                                            }

                                                            {value.extension.split('.').pop()==="pdf"  &&

                                                                <div>

                                                                    <Popup trigger={<button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-1 select-none text-white rounded-md transition duration-500">Ver archivo</button>} modal nested>
                                                                    {close => (
                                                                        <div className="modal">
                                                                        
                                                                            <button className="close" onClick={close}>
                                                                                &times;
                                                                            </button>

                                                                            <Document
                                                                            file={"data:application/"+value.extension.split('.').pop()+";base64,"+value.documentoBase64}
                                                                            onLoadSuccess={onDocumentLoadSuccess}
                                                                            >
                                                                                <Page pageNumber={pageNumber} />
                                                                            </Document>
                                                                            <p>Page {pageNumber} of {numPages}</p>
                                                                            <button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500" onClick={backPage}>Anterior</button>
                                                                            <button class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500" onClick={nextPage}>Siguiente</button>

                                                                        </div>
                                                                        
                                                                    )}
                                                                    </Popup>

                                                                </div>

                                                            }

                                                        </td>
                                                        <td class="p-4">
                                                            descarga
                                                        </td>
                                                        <td class="p-4">
                                                            {value.fechaRegistro}
                                                        </td>
                                                    </tr>
                                                })
                                                : <tr class="text-center"><td class="p-4" colSpan="7">No hay documentos asociadas</td></tr>
                                            : <tr class="text-center"><td class="p-4" colSpan="7">No hay documentos asociadas</td></tr>
                                        }

                                    </tbody>
                                </table>
                                
                                <div class="h-8"></div>
                                <button type="button" onClick={() => history.goBack()}
                                    class="bg-verde-pm hover:bg-amarillo-pm shadow-md font-semibold px-5 py-2 select-none text-white rounded-md transition duration-500">
                                    Volver
                                </button>  
                            
                            </div>

 
                        </div>               
                    </div>
                </div>
            </div>
        </div>
    );
}