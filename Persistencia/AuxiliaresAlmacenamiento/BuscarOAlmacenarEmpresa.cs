using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.AuxiliaresAlmacenamiento
{
    public static class BuscarOAlmacenarEmpresa
    {

        public static async Task<Empresa> BuscarOAgregarEmpresa(SistemaPasesContext context,
            string rutEmpresa,
            string nombreEmpresa)
        {
            // buscar si la empresa existe
            var buscarEmpresa = await context.Empresa
                .FirstOrDefaultAsync(e => e.Rut == rutEmpresa);

            if (buscarEmpresa == null)
            {
                // si no existe la persona se generar una nueva
                buscarEmpresa = new Empresa
                {
                    EmpresaId = new Guid(),
                    Rut = rutEmpresa,
                    Nombre = nombreEmpresa
                };
                await context.Empresa.AddAsync(buscarEmpresa);
            }

            // empresa encontrada o creada
            return buscarEmpresa;
        }

    }
}