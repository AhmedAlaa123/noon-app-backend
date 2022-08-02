using System.ComponentModel.DataAnnotations;

namespace noone.CustomAttributes
{
    public class ChecKDate : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            // check if Deliver is not inserted

            if (value == null) return new ValidationResult("*تاريخ التوصيل مطلوب!");

            DateTime deliverDate =Convert.ToDateTime(value.ToString());


            // subtract Deliver date from current date
            TimeSpan time = deliverDate.Subtract(DateTime.Now);
            
            if (time.TotalDays<=0) return new ValidationResult("*تاريخ التوصيل غير متاح");

            return ValidationResult.Success;


        }
    }
}
