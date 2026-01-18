namespace AmbulanceSystem.DAL;

public interface IImageService
{
    string SaveImage(string sourceImagePath);
    byte[] LoadImage(string relativePath);
}
