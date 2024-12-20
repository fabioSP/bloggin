using System;
using bloggin.Models;
using System.Linq;
using System.Collections.Generic;


namespace bloggin.API.Repositories
{
  public static class UserRepository
  {
    public static UserMO GetUser(string username, string password)
    {
      var users = new List<UserMO>();
      users.Add(new UserMO { Id = Guid.NewGuid(), UserName = "batman", Password = "123456", Role = "manager" });
      users.Add(new UserMO { Id = Guid.NewGuid(), UserName = "robin", Password = "123456", Role = "employee" });

      return users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
    }
  }
}