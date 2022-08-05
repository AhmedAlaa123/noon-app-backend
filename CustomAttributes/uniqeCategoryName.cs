using noone.Models;
using System.ComponentModel.DataAnnotations;

namespace noone.CustomAttributes
{
    public class uniqeCategoryName:ValidationAttribute
    {
       public NoonEntities context=new NoonEntities();
      
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value !=null)
            {
                Category category = this.context.Categories.FirstOrDefault(c => c.Name == (value.ToString().Trim()));
                if (category == null)
                    return ValidationResult.Success;

             return new ValidationResult("اسم الصنف موجود بالفعل ");
            }
            return new ValidationResult("ادخل اسم الصنف");
          
        }
    }
}
