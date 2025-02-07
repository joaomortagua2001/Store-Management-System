namespace RESTfulAPI.DTOs
{
    public class RegistarUserDto
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long? NIF { get; set; }
    }

}
