using System.Xml.Serialization;
using Newtonsoft.Json;

namespace CPUManager
{
    public class HandleFilesIO : IFileManager
    {
        public T ReadFile<T>(string filename)
        {
            string fileExtension = Path.GetExtension(filename).ToLower();

            switch (fileExtension)
            {
                case ".json":
                    string jsonContent = File.ReadAllText(filename);
                    T? cpuDataJson = JsonConvert.DeserializeObject<T>(jsonContent);
                    return cpuDataJson;
                case ".xml":
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    using (FileStream fileStream = new FileStream(filename, FileMode.Open))
                    {
                        T cpuDataXml = (T)serializer?.Deserialize(fileStream);
                        return cpuDataXml;
                    }
                default:
                    throw new ArgumentException($"Unsupported file extension: {fileExtension}");
            }
        }

        public void WriteToFile<T>(List<T> items)
        {
            string filePath = "./IOFiles/output.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (T item in items)
                {
                    writer.WriteLine(item?.ToString());
                }
            }
        }
    }
}
