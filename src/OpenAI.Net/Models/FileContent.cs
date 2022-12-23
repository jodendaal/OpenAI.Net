namespace OpenAI.Net.Models
{
    public class FileContentInfo
    {
        public FileContentInfo(byte[] fileContent, string fileName) 
        {
            FileContent = fileContent;
            FileName = fileName;
        }
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }

        public static FileContentInfo Load(string file)
        {
            var bytes = File.ReadAllBytes(file);
            var name = new FileInfo(file).Name;
            return new FileContentInfo(bytes, name);
        }

        public void Save(string path)
        {
            File.WriteAllBytes(path, FileContent);
        }
    }
}
