// Models/PlanningModels.cs
namespace FL_Front_Web_SIN.Models
{
    public class PlanificacionForm
    {
        public DateTime Fecha { get; set; } = DateTime.Today;
        public string HoraStr { get; set; } = "08:00";
        public string Aula { get; set; } = string.Empty;
        public string Docente { get; set; } = string.Empty;
        public string Materiales { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public string Materia { get; set; } = string.Empty;
        public string Grupo { get; set; } = string.Empty;
        public int CantidadAlumnos { get; set; }
    }
}