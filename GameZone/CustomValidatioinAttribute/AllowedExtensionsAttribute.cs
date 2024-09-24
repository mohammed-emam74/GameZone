using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace GameZone.CustomValidationAttribute
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            // Split extensions and remove any leading dot
            _allowedExtensions = allowedExtensions.Split(',')
                .Select(ext => ext.Trim().TrimStart('.').ToLowerInvariant())
                .ToArray();
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                // Get the file extension without the leading dot
                var extension = Path.GetExtension(file.FileName).TrimStart('.').ToLowerInvariant();

                // Check if the extension is allowed
                var isAllowed = _allowedExtensions.Contains(extension);
                if (!isAllowed)
                {
                    return new ValidationResult($"Only the following extensions are allowed: {string.Join(", ", _allowedExtensions)}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
