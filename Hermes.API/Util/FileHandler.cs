using Hermes.API.Extensions;

namespace Hermes.API.Util
{
    public class FileHandler
    {
        /// <summary>
        /// Save the image and returns the name of the file with its extension
        /// </summary>
        /// <param name="path">Path to save the file</param>
        /// /// <param name="folder">Folder to save uploads</param>
        /// <param name="file">File to get properties</param>
        public static async Task<string> SaveFileOnDirectory(string imageFilePath, string folder, string content, string filePathOrFileName)
        {
            
            string fileName = GetFileNameWithExtension(filePathOrFileName);
            byte[] imageData = Convert.FromBase64String(content.Base64WithoutHeader());

            imageFilePath += $"\\Uploads\\{folder}\\";

            //Verify folder exists
            VerifyPathExists(imageFilePath);
            imageFilePath += fileName;
            await File.WriteAllBytesAsync(imageFilePath, imageData);            
            return fileName;
        }

        public static string GetFileNameWithExtension(string fileName)
        {
            string name = fileName.Split(".")[0];
            string extension = fileName.Split(".")[1];
            return Encrypt.GetSha256(name) + "." + extension;
        }

        public static void VerifyPathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

        }
    }
}
