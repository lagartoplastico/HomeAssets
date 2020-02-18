using HomeAssets.Models.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeAssets.ViewModels
{
    public class AuthorizedEmails_vmodel
    {
        public AuthorizedEmails_vmodel()
        {
            AlreadyAuthorizedEmails = new List<string>();
        }
        public List<string> AlreadyAuthorizedEmails { get; set; }
    }
}