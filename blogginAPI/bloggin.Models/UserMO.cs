using System;

namespace bloggin.Models 
{
  public class UserMO
  {
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
  }
}