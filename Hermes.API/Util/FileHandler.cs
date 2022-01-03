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
        public static string SaveFileOnDirectory(string path, string folder, IFormFile file)
        {
            path += $"\\Uploads\\{folder}\\";

            //Verify folder exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension = file.FileName.Split(".")[1];
            var fileName = Encrypt.GetSha256(file.FileName.Split(".")[0]) + "." + extension;   
            //Save image on folder
            using (FileStream fs = System.IO.File.Create(path + fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return fileName;
        }
    }
}
