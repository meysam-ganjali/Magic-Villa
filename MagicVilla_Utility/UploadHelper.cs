


using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MagicVilla_Utility;

public class UploadHelper {
    private IHostingEnvironment _environment;

    public UploadHelper(IHostingEnvironment environment) {
        _environment = environment;
    }
    /// <summary>
    /// Path => $@"images\Cost\"
    /// </summary>
    /// <param name="file">IFromFile</param>
    /// <param name="path">$@"images\Cost\"</param>
    /// <returns></returns>
    public UploadDto UploadFile(IFormFile file, string path) {
        if (file != null) {
            string folder = path;
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
            if (!Directory.Exists(uploadsRootFolder)) {
                Directory.CreateDirectory(uploadsRootFolder);
            }


            if (file == null || file.Length == 0) {
                return new UploadDto() {
                    Status = false,
                    FileNameAddress = "",
                };
            }

            string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
            var filePath = Path.Combine(uploadsRootFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                file.CopyTo(fileStream);
            }

            return new UploadDto() {
                FileNameAddress = folder + fileName,
                Status = true,
            };
        }
        return null;
    }
}
public class UploadDto {
    public long Id { get; set; }
    public bool Status { get; set; }
    public string FileNameAddress { get; set; }
}
