using GameZone.CustomValidatioinAttribute;
using GameZone.CustomValidationAttribute;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel:GameViewModel
    {
       
        [AllowedExtensions(FileSettings.AllowedExtensions), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;


    }
}
