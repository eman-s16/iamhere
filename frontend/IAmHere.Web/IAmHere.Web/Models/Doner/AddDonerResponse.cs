using IAmHere.Web.Models.Person;

namespace IAmHere.Web.Models.Doner
{
    public class AddDonerResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public DonerModel Doner { get; set; }
    }
}
