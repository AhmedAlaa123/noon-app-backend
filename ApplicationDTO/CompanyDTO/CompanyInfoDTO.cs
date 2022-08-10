using System.ComponentModel.DataAnnotations;
namespace noone.ApplicationDTO.CompanyDTO

{
    public class CompanyInfoDTO
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string BrandImage { get; set; }

    }
}