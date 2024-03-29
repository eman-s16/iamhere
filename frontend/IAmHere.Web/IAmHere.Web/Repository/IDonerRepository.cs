using IAmHere.Web.Models.Doner;
using IAmHere.Web.Models.Person;

namespace IAmHere.Web.Repository
{
    public interface IDonerRepository
    {
        List<DonerModel> GetAll();
        Task<AddDonerResponse> Add(DonerModel req);
    }
}
