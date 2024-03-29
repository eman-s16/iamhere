using IAmHere.Web.Models.Doner;
using IAmHere.Web.Models.Person;
using Newtonsoft.Json;
using System.Text;

namespace IAmHere.Web.Repository
{
    public class DonerRepository:IDonerRepository
    {
        public readonly IConfiguration _configuration;
        public readonly string _apiUrl;


        public DonerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiUrl = _configuration.GetValue<string>("ApiURL") + "BloodDonor/" ?? "";
        }
        public List<DonerModel> GetAll()
        {
            List<DonerModel> list = new List<DonerModel>();
            try
            {
                string ApiUrl = _apiUrl + "getall";
                HttpClient client = new HttpClient();
                var response = client.GetAsync(ApiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    DonerModel obj = new DonerModel();
                    list = JsonConvert.DeserializeObject<List<DonerModel>>(responseString);
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public async Task<AddDonerResponse> Add(DonerModel req)
        {
            AddDonerResponse res = new AddDonerResponse();
            try
            {
                string apiUrl = _apiUrl + "Add";
                HttpClient client = new HttpClient();
                string json = JsonConvert.SerializeObject(req);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<AddDonerResponse>(responseString);

                }

            }
            catch (Exception ex)
            {
                // Handle any exceptions
                throw new Exception("An error occurred while processing the request.", ex);
            }
            return res;
        }
    }
}
