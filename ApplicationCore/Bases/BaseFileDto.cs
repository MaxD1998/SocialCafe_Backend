namespace ApplicationCore.Bases;

public class BaseFileDto
{
    public string ContentType { get; set; }

    public byte[] Data { get; set; }

    public string Description { get; set; }

    public bool IsPublic { get; set; }

    public string Name { get; set; }
}