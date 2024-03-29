using System.ComponentModel.DataAnnotations;

namespace IAmHere.Web.Models.Doner
{
    public class DonerModel
    {
        
        public int DonorID { get; set; }

    
        public string FirstName { get; set; }
     
        public string LastName { get; set; }
 
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? BloodType { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? MedicalConditions { get; set; }
    }
}
