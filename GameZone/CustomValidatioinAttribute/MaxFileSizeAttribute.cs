﻿namespace GameZone.CustomValidatioinAttribute
{
    public class MaxFileSizeAttribute:ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }
        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"The Maximum allowed file size is {_maxFileSize}bytes");
                }
            }
            return ValidationResult.Success;
        }
    }
}
