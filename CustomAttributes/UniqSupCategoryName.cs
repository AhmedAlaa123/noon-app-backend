using noone.Models;
using System.ComponentModel.DataAnnotations;

namespace noone.CustomAttributes
{
    public class UniqSupCategoryName:ValidationAttribute
    {
        public NoonEntities context = new NoonEntities();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                SubCategory sup = this.context.SubCategories.FirstOrDefault(e => e.Name == (value.ToString().Trim()));
                if (sup == null)
                    return ValidationResult.Success;

                return new ValidationResult(" الاسم موجود بالفعل يجب ان يكون مميزا ");
            }
            return new ValidationResult("ادخل الاسم ");


        }

    }
}
