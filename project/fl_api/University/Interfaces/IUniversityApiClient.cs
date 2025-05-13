using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Dtos;
using University.Models;

namespace University.Interfaces
{
    public interface IUniversityApiClient
    {
        Task<List<Insumo>> GetInsumosAsync();
        Task<Insumo?> GetInsumoByIdAsync(int id);
        Task<List<InsumoPorPractica>> GetInsumosPorPracticaAsync();


        Task<List<Alerta>> GetAlertasAsync();
        Task<List<Solicitud>> GetSolicitudesAsync();
        Task<List<Docente>> GetDocentesAsync();
        Task<Docente?> GetDocenteByIdAsync(int id);
        Task<List<Carrera>> GetCarrerasAsync();
        Task<Carrera?> GetCarreraByIdAsync(int id);
        Task<List<Semestre>> GetSemestresAsync();
        Task<Semestre?> GetSemestreByIdAsync(int id);
        Task<List<Materia>> GetMateriasAsync();
        Task<Materia?> GetMateriaByIdAsync(int id);
        Task<List<SolicitudUso>> GetSolicitudesUsoAsync();
        Task<SolicitudUso?> GetSolicitudUsoByIdAsync(int id);
        Task<List<Practica>> GetPracticasAsync();
        Task<Practica?> GetPracticaByIdAsync(int id);
        Task<List<Laboratorio>> GetLaboratoriosAsync();
        Task<Laboratorio?> GetLaboratorioByIdAsync(int id);
        Task<List<MovimientoInventario>> GetMovimientosInventarioAsync();
        Task<MovimientoInventario?> GetMovimientoInventarioByIdAsync(int id);
    }
}
