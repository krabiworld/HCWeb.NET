namespace HCWeb.NET.Forms;

public record PostForm(string Title, string Content, IFormFile? Preview);