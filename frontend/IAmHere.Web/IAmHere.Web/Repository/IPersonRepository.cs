using IAmHere.Web.Models.Person;
using System;

namespace IAmHere.Web.Repository
{
    public interface IPersonRepository
    {
        List<PersonModel> GetAll();
        Task<AddPersonResponse> Add(PersonModel req);
    }
}
