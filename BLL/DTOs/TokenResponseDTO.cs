using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class TokenResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresInMinutes { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
