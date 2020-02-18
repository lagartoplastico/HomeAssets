using HomeAssets.Models.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeAssets.Models
{
    public class NpgsqlAuthorizedEmailRepo : IAuthorizedEmailRepo
    {
        private readonly AppDbContext context;

        public NpgsqlAuthorizedEmailRepo(AppDbContext context)
        {
            this.context = context;
        }

        public AuthorizedEmail AddAuthorizedEmail(string newAuthorizedEmail)
        {
            AuthorizedEmail newAuthEmail = new AuthorizedEmail()
            {
                EmailAddress = newAuthorizedEmail,
                DateOfCreation = DateTime.Now
            };
            context.AuthorizedEmails.Add(newAuthEmail);
            context.SaveChanges();
            return newAuthEmail;
        }

        public IEnumerable<AuthorizedEmail> GetAllAuthorizedEmails()
        {
            return context.AuthorizedEmails;
        }

        public AuthorizedEmail GetByEmail(string email)
        {
            return GetAllAuthorizedEmails().FirstOrDefault(e => e.EmailAddress == email);
        }

        public AuthorizedEmail RemoveAuthorizedEmail(string emailToRemove)
        {
            AuthorizedEmail authEmailToDelete = GetByEmail(emailToRemove);
            if (authEmailToDelete != null)
            {
                context.AuthorizedEmails.Remove(authEmailToDelete);
                context.SaveChanges();
            }
            return authEmailToDelete;
        }
    }
}