using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SwaggerTest.Models
{
    public class UserIM
    {
        [Key]
        public int slno { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? email { get; set; }
        public Guid uniquekey { get; set; }
        public int isActive { get; set; }

    }
    public class UserVM
    {
        [Key]
        public int slno { get; set; }
        public string? username { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? email { get; set; }
        public Guid uniquekey { get; set; }
        public int isActive { get; set; }

    }
    [Keyless]
    public class TokenModel
    {
       
        public string? Token { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
    }
}
