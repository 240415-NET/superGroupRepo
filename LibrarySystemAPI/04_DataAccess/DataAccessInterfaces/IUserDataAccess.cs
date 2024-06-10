using LibrarySystem.API.Models;

namespace LibrarySystem.API.Data;

public interface IUserDataAccess
{
  public Task<User?> CreateNewUserInDBAsync(User userFromServiceClass);
}