using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace University.Models
{
    public class SolicitudUso
    {
        [JsonPropertyName("id_solicitud")]
        public int IdSolicitud { get; set; }

        [JsonPropertyName("id_docente")]
        public int IdDocente { get; set; }

        [JsonPropertyName("id_practica")]
        public int IdPractica { get; set; }

        [JsonPropertyName("id_laboratorio")]
        public int IdLaboratorio { get; set; }

        [JsonPropertyName("fecha_solicitud")]
        public DateTime FechaSolicitud { get; set; }

        [JsonPropertyName("fecha_hora_inicio")]
        public DateTime FechaHoraInicio { get; set; }

        [JsonPropertyName("fecha_hora_fin")]
        public DateTime FechaHoraFin { get; set; }

        [JsonPropertyName("numero_estudiantes")]
        public int NumeroEstudiantes { get; set; }

        [JsonPropertyName("tamano_grupo")]
        public int TamanoGrupo { get; set; }

        [JsonPropertyName("numero_grupos")]
        public int NumeroGrupos { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; } = null!;

        [JsonPropertyName("observaciones")]
        public string Observaciones { get; set; } = null!;

        [JsonPropertyName("docente_nombre")]
        public string DocenteNombre { get; set; } = null!;

        [JsonPropertyName("practica_titulo")]
        public string PracticaTitulo { get; set; } = null!;

        [JsonPropertyName("laboratorio_nombre")]
        public string LaboratorioNombre { get; set; } = null!;
    }

}
