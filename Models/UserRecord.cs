

namespace ECN.Models
{
    public class UserRecord
    {
        public static int User_ID { get; set; }
        public static string Username { get; set; }
        public static string Nombre { get; set; }
        public static string Correo { get; set; }
        public static string TipoUsuario { get; set; }
        public static int Telefono { get; set; }
        public static int Employee_ID { get; set; }

        public static Employee Employee { get; set; }
    }
}
