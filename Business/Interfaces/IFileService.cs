using ContactListApp.Business.Models;

namespace ContactListApp.Business.Interfaces;

public interface IFileService
{
    void SaveListToFile(List<ContactEntity> list);
    List<ContactEntity> LoadListFromFile();
}
