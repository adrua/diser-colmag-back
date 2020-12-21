using System.Collections.Generic;

namespace Inscripciones_Backend.Security.Models
{
    public class User 
    {
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 
        public IEnumerable<string> Roles { get; set; }
        public bool IsReadOnly { get; set; }
    }    
}
