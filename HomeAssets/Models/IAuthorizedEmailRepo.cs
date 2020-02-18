using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAssets.Models
{
    public interface IAuthorizedEmailRepo
    {
        AuthorizedEmail AddAuthorizedEmail(string newAuthorizedEmail);
        AuthorizedEmail RemoveAuthorizedEmail(string emailToRemove);
        AuthorizedEmail GetByEmail(string email);
        IEnumerable<AuthorizedEmail> GetAllAuthorizedEmails();
    }
}
