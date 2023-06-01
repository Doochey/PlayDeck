namespace PlayDeckRazor.Model;

public interface IPlatform
{
    public int ID { get; set; }
    public string Title { get; set; }
    /// <summary>
    /// Url of the image to be used as the platform logo
    /// </summary>
    public string? ImageURL { get; set; }
}