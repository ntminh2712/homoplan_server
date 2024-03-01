using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SeminarAPI.Helpers
{
    public class PathFormFile : IFormFile
    {
        private readonly string _filePath;

        public PathFormFile(string filePath, string fileName)
        {
            _filePath = filePath;
            FileName = fileName;
        }

        public string FileName { get; }

        public string ContentType => "application/octet-stream"; // Adjust as needed

        public string ContentDisposition => $"form-data; name={FileName}; filename={FileName}";

        public IHeaderDictionary Headers => new HeaderDictionary();

        public long Length => new FileInfo(_filePath).Length;

        public string Name => Path.GetFileNameWithoutExtension(_filePath);

        public void CopyTo(Stream target)
        {
            using (var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                stream.CopyTo(target);
            }
        }

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            using (var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(target, 81920, cancellationToken);
            }
        }

        public Stream OpenReadStream()
        {
            return File.OpenRead(_filePath);
        }
    }
}
