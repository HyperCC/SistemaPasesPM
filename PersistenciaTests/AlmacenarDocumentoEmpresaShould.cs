﻿using Dominio.Auxiliares.ModelosPaseContratista;
using Dominio.Entidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.FileProviders;
using Moq;
using Persistencia;
using Persistencia.AuxiliaresAlmacenamiento;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PersistenciaTests
{
    /// <summary>
    /// Test para probar el almacenamiento de documentos
    /// </summary>
    public class AlmacenarDocumentoEmpresaShould
    {
        /// <summary>
        /// Guardar efectivamente el archivo
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AlmacenarArchivoEnSistema()
        {
            // --- Arrange ---
            // documento de prueba recibido desde el cliente
            DocumentoEmpresa documentoEmpresa = new DocumentoEmpresa
            {
                Documento = "RG9jdW1lbnRvIGRlIHBydWViYQ0KRXN0ZSBlcyB1biBkb2N1bWVudG8gcGFyYSBwcm9iYXIgZWwgYWxtYWNlbmFtaWVudG8geSBsZWN0dXJhIGRlbCBtaXNtbyBlbiBlbCBzaXN0ZW1hLg==",
                Extension = ".txt",
                TipoDocumento = "CERTIFICADO DE M UTUALIDAD",
                Obligatoriedad = false,
                FechaVencimiento = "19/08/2022"
            };

            // generar IHostingEnvironment
            var mockEnvironment = new Mock<IHostingEnvironment>();

            // configuraciones para IHostingEnvironment
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");
            mockEnvironment
                .Setup(x => x.WebRootPath)
                .Returns("~/");

            // base de datos en memoria 
            var options = new DbContextOptionsBuilder<SistemaPasesContext>()
                    .UseInMemoryDatabase(databaseName: "dbTest", new InMemoryDatabaseRoot())
                    .Options;
            var context = new SistemaPasesContext(options);

            await context.TipoDocumento.AddAsync(new TipoDocumento
            {
                Titulo = "CERTIFICADO DE MUTUALIDAD"
            });

            Empresa empresa = new Empresa
            {
                EmpresaId = new Guid(),
            };
            await context.Empresa.AddAsync(empresa);

            Pase pase = new Pase
            {
                PaseId = new Guid(),
                EmpresaId = empresa.EmpresaId
            };
            await context.Pase.AddAsync(pase);
            await context.SaveChangesAsync();

            // --- Act ---
            // almacenar el documento 
            Documento archivoAlmacenado = await AlmacenarDocumentosEmpresa.AgregarDocumentosEmpresa(
                documentoEmpresa
                , context
                , mockEnvironment.Object
                , pase.PaseId
                , empresa.EmpresaId);
            await context.SaveChangesAsync();

            // ---- Assert ---
            // verificar el lugar en el que se guardo 
            Assert.True(archivoAlmacenado.RutaDocumento != null);
            Assert.True(archivoAlmacenado.RutaDocumento.Length > 0);

        }
    }
}