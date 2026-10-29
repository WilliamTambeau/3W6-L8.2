using ZombieParty.Models;

namespace ZombieParty.Utility
{
    public interface IImageFileManager
    {
        public string? UploadImage(IFormCollection form, bool edit, string? oldImagePath = null);
    }

    public class ImageFileManager : IImageFileManager
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageFileManager(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string? UploadImage(IFormCollection form, bool edit, string? oldImagePath = null)
        {
            // Vous devez copier la logique du contrôleur à cet endroit
            var files = form.Files; //nouvelle image récupérée

            if (files.Count > 0)
            {
                // Il faut utiliser la DI pour obtenir webHostEnvironment
                string webRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString();// Nom fichier généré, unique
                var uploads = webRootPath + AppConstants.ImagePath;// chemin pour les image
                var extension = Path.GetExtension(files[0].FileName); // extraire l'extension du fichier

                // Créer un cannal pour transférer le fichier 
                using (var filesStreams = new FileStream(uploads + fileName + extension, FileMode.Create))
                {
                    files[0].CopyTo(filesStreams);
                }

                // Je supprime l'ancien fichier si c'est une édition
                if (edit && !string.IsNullOrWhiteSpace(oldImagePath))
                {
                    try
                    {
                        System.IO.File.Delete(uploads + oldImagePath);
                    }
                    catch { }
                }

                // Il faudra retourner la valeur dans notre nouvelle fonction
                return fileName + extension;
            }
            // Vous devez retourner fileName + extension quand files.Count est plus grand que 0 

            // Sinon on retourne l’ancien imagePath
            return oldImagePath;
        }
    }

}
