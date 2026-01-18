namespace Ambulance.DTO.PersonModels;

public class ImageDto
{
    public byte[] Bytes { get; set; }
    public string? ContentType { get; set; } // наприклад, ".jpg"
}
