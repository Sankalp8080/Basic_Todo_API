namespace SwaggerTest.Models
{
    public class UserIM
    {
        public string? username { get; set; }
        public string? password { get; set; }

    }
    public class UserVM
    {
        public int slno { get; set; }
        public string? username { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? email { get; set; }
        public Guid uniquekey { get; set; }

    }
}
