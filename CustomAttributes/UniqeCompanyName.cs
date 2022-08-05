using noone.Models;
using System.ComponentModel.DataAnnotations;

namespace noone.CustomAttributes
{
    public class UniqeCompanyName:ValidationAttribute
    {
        public NoonEntities context = new NoonEntities();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                Company comp = this.context.Companies.FirstOrDefault(c => c.Name == (value.ToString().Trim()));
                if (comp == null)
                    return ValidationResult.Success;

                return new ValidationResult("اسم الشركة موجود يجب ان يكون الاسم مميزا ");
            }
            return new ValidationResult("ادخل اسم الشركة");

        }
    }
}

