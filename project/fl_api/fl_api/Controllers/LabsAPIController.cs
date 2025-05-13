using Microsoft.AspNetCore.Mvc;
using University.Interfaces;
using University.Models;

namespace fl_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabsApiController : ControllerBase
    {
        private readonly IUniversityApiClient _api;

        public LabsApiController(IUniversityApiClient api)
        {
            _api = api;
        }

        [HttpGet("insumos")]
        public async Task<ActionResult<List<Insumo>>> GetInsumos()
        {
            var data = await _api.GetInsumosAsync();
            return Ok(data);
        }

        [HttpGet("insumos/{id}")]
        public async Task<ActionResult<Insumo>> GetInsumoById(int id)
        {
            var insumo = await _api.GetInsumoByIdAsync(id);
            if (insumo == null)
                return NotFound();
            return Ok(insumo);
        }

        [HttpGet("alertas")]
        public async Task<ActionResult<List<Alerta>>> GetAlertas()
        {
            var data = await _api.GetAlertasAsync();
            return Ok(data);
        }

        [HttpGet("docentes")]
        public async Task<ActionResult<List<Docente>>> GetDocentes()
        {
            var data = await _api.GetDocentesAsync();
            return Ok(data);
        }

        [HttpGet("docentes/{id}")]
        public async Task<ActionResult<Docente>> GetDocenteById(int id)
        {
            var docente = await _api.GetDocenteByIdAsync(id);
            if (docente == null)
                return NotFound();
            return Ok(docente);
        }

        [HttpGet("solicitudes")]
        public async Task<ActionResult<List<Solicitud>>> GetSolicitudes()
        {
            var data = await _api.GetSolicitudesAsync();
            return Ok(data);
        }

        [HttpGet("carreras")]
        public async Task<ActionResult<List<Carrera>>> GetCarreras()
        {
            var data = await _api.GetCarrerasAsync();
            return Ok(data);
        }

        [HttpGet("carreras/{id}")]
        public async Task<ActionResult<Carrera>> GetCarreraById(int id)
        {
            var carrera = await _api.GetCarreraByIdAsync(id);
            if (carrera == null)
                return NotFound();
            return Ok(carrera);
        }

        [HttpGet("semestres")]
        public async Task<ActionResult<List<Semestre>>> GetSemestres()
        {
            var data = await _api.GetSemestresAsync();
            return Ok(data);
        }

        [HttpGet("semestres/{id}")]
        public async Task<ActionResult<Semestre>> GetSemestreById(int id)
        {
            var semestre = await _api.GetSemestreByIdAsync(id);
            if (semestre == null)
                return NotFound();
            return Ok(semestre);
        }

        [HttpGet("materias")]
        public async Task<ActionResult<List<Materia>>> GetMaterias()
        {
            var data = await _api.GetMateriasAsync();
            return Ok(data);
        }

        [HttpGet("materias/{id}")]
        public async Task<ActionResult<Materia>> GetMateriaById(int id)
        {
            var materia = await _api.GetMateriaByIdAsync(id);
            if (materia == null)
                return NotFound();
            return Ok(materia);
        }

        [HttpGet("solicitudes-uso")]
        public async Task<ActionResult<List<SolicitudUso>>> GetSolicitudesUso()
        {
            var data = await _api.GetSolicitudesUsoAsync();
            return Ok(data);
        }

        [HttpGet("solicitudes-uso/{id}")]
        public async Task<ActionResult<SolicitudUso>> GetSolicitudUsoById(int id)
        {
            var solicitud = await _api.GetSolicitudUsoByIdAsync(id);
            if (solicitud == null)
                return NotFound();
            return Ok(solicitud);
        }

        [HttpGet("practicas")]
        public async Task<ActionResult<List<Practica>>> GetPracticas()
        {
            var data = await _api.GetPracticasAsync();
            return Ok(data);
        }

        [HttpGet("practicas/{id}")]
        public async Task<ActionResult<Practica>> GetPracticaById(int id)
        {
            var practica = await _api.GetPracticaByIdAsync(id);
            if (practica == null)
                return NotFound();
            return Ok(practica);
        }

        [HttpGet("laboratorios")]
        public async Task<ActionResult<List<Laboratorio>>> GetLaboratorios()
        {
            var data = await _api.GetLaboratoriosAsync();
            return Ok(data);
        }

        [HttpGet("laboratorios/{id}")]
        public async Task<ActionResult<Laboratorio>> GetLaboratorioById(int id)
        {
            var laboratorio = await _api.GetLaboratorioByIdAsync(id);
            if (laboratorio == null)
                return NotFound();
            return Ok(laboratorio);
        }

        [HttpGet("movimientos-inventario")]
        public async Task<ActionResult<List<MovimientoInventario>>> GetMovimientosInventario()
        {
            var data = await _api.GetMovimientosInventarioAsync();
            return Ok(data);
        }

        [HttpGet("movimientos-inventario/{id}")]
        public async Task<ActionResult<MovimientoInventario>> GetMovimientoInventarioById(int id)
        {
            var movimiento = await _api.GetMovimientoInventarioByIdAsync(id);
            if (movimiento == null)
                return NotFound();
            return Ok(movimiento);
        }
    }
}
