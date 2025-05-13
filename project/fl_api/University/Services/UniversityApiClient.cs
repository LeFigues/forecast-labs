using System.Net.Http.Json;
using University.Dtos;
using University.Interfaces;
using University.Models;

namespace University.Services
{
    public class UniversityApiClient : IUniversityApiClient
    {
        private readonly HttpClient _httpClient;

        public UniversityApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Insumo>> GetInsumosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Insumo>>("/insumos") ?? new();
        }

        public async Task<Insumo?> GetInsumoByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Insumo>($"/insumos/{id}");
        }

        public async Task<List<InsumoPorPractica>> GetInsumosPorPracticaAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<InsumoPorPractica>>("/insumos-por-practica") ?? new();
        }

        public async Task<List<Alerta>> GetAlertasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Alerta>>("/alertas") ?? new();
        }

        public async Task<List<Solicitud>> GetSolicitudesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Solicitud>>("/solicitudes") ?? new();
        }

        public async Task<List<Docente>> GetDocentesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Docente>>("/docentes") ?? new();
        }

        public async Task<Docente?> GetDocenteByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Docente>($"/docentes/{id}");
        }

        public async Task<List<Carrera>> GetCarrerasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Carrera>>("/carreras") ?? new();
        }

        public async Task<Carrera?> GetCarreraByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Carrera>($"/carreras/{id}");
        }

        public async Task<List<Semestre>> GetSemestresAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Semestre>>("/semestres") ?? new();
        }

        public async Task<Semestre?> GetSemestreByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Semestre>($"/semestres/{id}");
        }

        public async Task<List<Materia>> GetMateriasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Materia>>("/materias") ?? new();
        }

        public async Task<Materia?> GetMateriaByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Materia>($"/materias/{id}");
        }

        public async Task<List<SolicitudUso>> GetSolicitudesUsoAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<SolicitudUso>>("/solicitudes-uso") ?? new();
        }

        public async Task<SolicitudUso?> GetSolicitudUsoByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<SolicitudUso>($"/solicitudes-uso/{id}");
        }

        public async Task<List<Practica>> GetPracticasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Practica>>("/practicas") ?? new();
        }

        public async Task<Practica?> GetPracticaByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Practica>($"/practicas/{id}");
        }

        public async Task<List<Laboratorio>> GetLaboratoriosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Laboratorio>>("/laboratorios") ?? new();
        }

        public async Task<Laboratorio?> GetLaboratorioByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Laboratorio>($"/laboratorios/{id}");
        }

        private class MovimientoInventarioResponse
        {
            public List<MovimientoInventario> Data { get; set; } = new();
        }

        public async Task<List<MovimientoInventario>> GetMovimientosInventarioAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<MovimientoInventarioResponse>("/movimientos-inventario");
            return response?.Data ?? new();
        }

        public async Task<MovimientoInventario?> GetMovimientoInventarioByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<MovimientoInventario>($"/movimientos-inventario/{id}");
        }
    }
}
