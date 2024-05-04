using System.ComponentModel.DataAnnotations;

namespace SwaggerTest.Models
{
    public class UserToDoModelIM 
    {
        [Key]
        public int slno {  get; set; }  
        public int userId { get; set; }
        public string? task { get; set; }
        public DateTime? remainderDate { get; set; }
        public int taskpriority { get; set; }
        public int markasdone { get;set; }

    }
    public class UserToDoModelVM
    {
        [Key]
        public int slno { get; set; }
        public int userId { get; set; }
        public string? task { get; set; }
        public DateTime? remainderDate { get; set; }
        public int taskpriority { get; set; }
        public int markasdone { get; set; }


    }
}
