using GameZone.Settings;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class AllowedMaxSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;
        public AllowedMaxSizeAttribute(int maxSize) {
            _maxSize = maxSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not  null)
            {
                if(file.Length > _maxSize)
                {
                    return new ValidationResult($"Max size Allowed is {FileSettings.MaxSizeInMB} MB");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult($"Invalid File");

        }

    }
}
