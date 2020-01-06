using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAssets.Models
{
    public interface IHomeServiceRepo
    {
        HomeService GetById(int id);
        IEnumerable<HomeService> GetAllHomeServices();
        IEnumerable<HomeService> GetByServiceType(string type);
        IEnumerable<HomeService> GetByMember(string member);
        IEnumerable<HomeService> GetByLocation(string location);
        public HomeService AddHomeService(HomeService newHomeService);
        public HomeService UpdateHomeService(HomeService changedHomeService);
        public HomeService DeleteHomeService(HomeService homeService);



    }
}
