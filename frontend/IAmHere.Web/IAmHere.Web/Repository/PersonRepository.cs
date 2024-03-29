using IAmHere.Web.Models.Person;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace IAmHere.Web.Repository
{
    public class PersonRepository:IPersonRepository

    {
        public readonly IConfiguration _configuration;
        public readonly string _apiUrl;

        
        public PersonRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiUrl = _configuration.GetValue<string>("ApiURL") + "Person/" ?? "";
        }
        public List<PersonModel> GetAll()
        {
            List<PersonModel> list = new List<PersonModel>();
            try
            {
                string ApiUrl = _apiUrl + "getall";
                HttpClient client = new HttpClient();
                var response = client.GetAsync(ApiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    PersonModel obj = new PersonModel();
                    list = JsonConvert.DeserializeObject<List<PersonModel>>(responseString);
                }
            }
            catch (Exception ex)
            {

            }
            return list;
        }
        public async Task<AddPersonResponse> Add(PersonModel req)
        {
            AddPersonResponse res = new AddPersonResponse();
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
                  res = JsonConvert.DeserializeObject<AddPersonResponse>(responseString);
                    
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
