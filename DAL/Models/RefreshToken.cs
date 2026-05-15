using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class RefreshToken
    {
        [Key]
        public int TokenId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
        public RefreshToken()
        {
            IsRevoked = false;
            CreatedAt = DateTime.Now;
            ExpiryDate = DateTime.Now.AddDays(7);
        }
    }
}
