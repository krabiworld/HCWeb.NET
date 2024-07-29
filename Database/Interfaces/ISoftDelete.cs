namespace HCWeb.NET.Database.Interfaces;

public interface ISoftDelete
{
    public DateTimeOffset? DeletedAt { get; set; }

    public void Undo()
    {
        DeletedAt = null;
    }
}