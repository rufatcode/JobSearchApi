using System;
using Domain.Entities;

namespace Service.Helpers.Interfaces
{
	public interface ITokenService
	{
        string CreateToken(AppUser user, IList<string> roles);
    }
}

