using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MoviesApp.Filters
{
    public class ShortNameAttribute: ValidationAttribute
    {
        public int NameLength { get; }
       
        public ShortNameAttribute(int nameLength)
        {
            this.NameLength = nameLength;
           
        }

        public string GetErrorMessageName() => $"данное поле должно быть больше {NameLength} символов.";
       

        protected override ValidationResult IsValid(object  value, ValidationContext validationContext)
        {
            var kolvoSymbols = (string) value;
            if (kolvoSymbols.Length<NameLength)
            {
                return new ValidationResult(GetErrorMessageName());
            }
           

           return ValidationResult.Success;
        }
    }
}