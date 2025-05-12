namespace fl_front.Models
{
    public class HealthCheckItem
    {
        public string? Status { get; set; }
        public string? Detail { get; set; }

        // Este bloque es para casos como PythonFiles, que no tiene "status" ni "detail"
        public bool? InputFolderExists { get; set; }
        public bool? OutputFolderExists { get; set; }
        public bool? PermanentFolderExists { get; set; }
        public bool? ScriptExtractJsonExists { get; set; }
        public bool? PythonExeExists { get; set; }
    }
}
