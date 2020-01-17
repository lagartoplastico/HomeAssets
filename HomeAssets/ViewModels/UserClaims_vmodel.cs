using System.Collections.Generic;

namespace HomeAssets.ViewModels
{
    public class UserClaims_vmodel
    {
        public UserClaims_vmodel()
        {
            Claims = new List<UserClaim>();
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<UserClaim> Claims { get; set; }
    }
}