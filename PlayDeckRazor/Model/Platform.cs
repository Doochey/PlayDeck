namespace PlayDeckRazor.Model;

public class Platform : IPlatform
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string? ImageURL { get; set; }
}