namespace CPUManager
{
    interface IFileManager
    {
        T ReadFile<T>(string filename);
        void WriteToFile<T>(List<T> items);
    }
}
