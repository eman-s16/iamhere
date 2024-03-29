namespace IAmHere.Web.Models.Person
{
    public class AddPersonResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public PersonModel Person { get; set; }
    }
}
