namespace HCWeb.NET.Forms;

public record AccountNameForm(string Name);
public record AccountEmailForm(string Email);
public record AccountPasswordForm(string Password, string RetypePassword);