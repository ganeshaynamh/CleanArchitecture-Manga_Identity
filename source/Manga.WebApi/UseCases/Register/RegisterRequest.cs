namespace Manga.WebApi.UseCases.Register
{
    public class RegisterRequest
    {
        public string SSN { get; set; }
        public string UserName { get; set; }
        public double InitialAmount { get; set; }
    }
}