namespace CitasMedicas.Models.DTO
{
    public class PacienteDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NickUsuario { get; set; }
        public string Clave { get; set; }
        public string Nss { get; set; }
        public string NumTarjeta { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}