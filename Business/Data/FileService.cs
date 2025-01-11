using System.Diagnostics;
using System.Text.Json;
using ContactListApp.Business.Models;
using ContactListApp.Business.Interfaces;

namespace ContactListApp.Business.Data;

public class FileService : IFileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public FileService(string directoryPath = "Data", string fileName = "list.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
        _jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
    }

    public void SaveListToFile(List<ContactEntity> list)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            var json = JsonSerializer.Serialize(list, _jsonSerializerOptions);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error saving file: {ex.Message}");
        }
    }


    public List<ContactEntity> LoadListFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new List<ContactEntity>();

            var json = File.ReadAllText(_filePath);
            var list = JsonSerializer.Deserialize<List<ContactEntity>>(json, _jsonSerializerOptions);

            return list?.Where(c => !string.IsNullOrWhiteSpace(c.Email)).ToList() ?? new List<ContactEntity>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading file: {ex.Message}");
            return new List<ContactEntity>();
        }
    }
}
