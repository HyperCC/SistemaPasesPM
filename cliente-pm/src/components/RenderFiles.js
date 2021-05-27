import React, { useState } from 'react'
import File from '../files/CARTA_CONSENTIMIENTO_plantilla[3801].docx';
import File2 from '../files/CARTA.pdf';
import File3 from '../files/VRA_17_2021.jpg';
import File4 from '../files/Excel.xlsx';
import File5 from '../files/sample.pdf';

import { Document, Page, pdfjs } from 'react-pdf';
import FileViewer from 'react-file-viewer';

pdfjs.GlobalWorkerOptions.workerSrc = `//cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.js`;

export default function RenderFiles() {

    {/**  */}

    const [numPages, setNumPages] = useState(null);
    const [pageNumber, setPageNumber] = useState(1);

    function onDocumentLoadSuccess({ numPages }) {
        setNumPages(numPages);
    }

    function nextPage() {
        setPageNumber(pageNumber+1)
    }

    function backPage(){
        setPageNumber(pageNumber-1)
    }

    const file = File
    var fileExtension = file.split('.').pop();
    const type = fileExtension;
    
    if(type == "docx"){
        return(
            <div class="bg-gray-100 min-h-screen">
                <div class="md:max-w-6xl w-full mx-auto py-8">
                    <div class="sm:px-8 px-4">
                
                        <FileViewer
                        fileType={type}
                        filePath={file}
                        />
                    </div>
                </div>
            </div>
        );
    }
    if(type == "pdf"){

        return (
            <div class="bg-gray-100 min-h-screen">
                <div class="md:max-w-6xl w-full mx-auto py-8">
                    <div class="sm:px-8 px-4">
                        <div class="bg-white p-4 md:p-8 rounded-lg shadow-md">
                            <Document
                                file={file}
                                onLoadSuccess={onDocumentLoadSuccess}

                            >
                                <Page pageNumber={pageNumber} />
                            </Document>
                            <p>Page {pageNumber} of {numPages}</p>
                            <button onClick={backPage}>Anterior</button>
                            <button onClick={nextPage}>Siguiente</button>
                        </div>
                    </div>
                </div>
            </div>
        )

    }
}
