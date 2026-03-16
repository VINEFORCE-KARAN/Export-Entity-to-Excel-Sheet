namespace TestApp.Dto
{
    public class FileDto
    {
        public string FileName { get; set; }

        public string FileType { get; set; }

        public FileDto(string fileName, string fileType)
        {
            FileName = fileName;
            FileType = fileType;
        }
    }
}