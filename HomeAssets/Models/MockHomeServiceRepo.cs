using System.Collections.Generic;
using System.Linq;

namespace HomeAssets.Models
{
    public class MockHomeServiceRepo : IHomeServiceRepo
    {
        private List<HomeService> homeServiceList;

        public MockHomeServiceRepo()
        {
            homeServiceList = new List<HomeService>()
            {
                new HomeService()
                {
                    Id = 1,
                    Institution = "YPFB",
                    LeasedTo = Members.Gary,
                    ServiceType = ServiceTypes.Gas_Domiciliario,
                    PaymentCriteria = "Codigo de usuario",
                    PaymentId = "1-00203-J88221"
                },
                new HomeService()
                {
                    Id = 2,
                    Institution = "Tigo",
                    LeasedTo = Members.Joannes,
                    ServiceType = ServiceTypes.Internet,
                    PaymentCriteria = "Instancia",
                    PaymentId = "A-0290113"
                },
                new HomeService()
                {
                    Id = 3,
                    Institution = "Entel",
                    LeasedTo = Members.Gladys,
                    ServiceType = ServiceTypes.Internet,
                    PaymentCriteria = "Instancia",
                    PaymentId = "3-5935/32"
                },

                new HomeService()
                {
                    Id = 4,
                    Institution = "DELAPAZ",
                    LeasedTo = Members.Adhemar,
                    ServiceType = ServiceTypes.Energia_Electrica,
                    PaymentCriteria = "Codigo de Cliente",
                    PaymentId = "3-5935/32"
                },

                new HomeService()
                {
                    Id = 4,
                    Institution = "DELAPAZ",
                    LeasedTo = Members.Jose,
                    ServiceType = ServiceTypes.Energia_Electrica,
                    PaymentCriteria = "Codigo de Cliente",
                    PaymentId = "3-3335/32"
                },
                
                new HomeService()
                {
                    Id = 4,
                    Institution = "Viva",
                    LeasedTo = Members.Joannes,
                    ServiceType = ServiceTypes.Gas_Domiciliario,
                    PaymentCriteria = "Codigo de Cliente",
                    PaymentId = "3-5935/32"
                },

                new HomeService()
                {
                    Id = 4,
                    Institution = "Entel",
                    LeasedTo = Members.Joannes,
                    ServiceType = ServiceTypes.Internet,
                    PaymentCriteria = "instancia",
                    PaymentId = "T-121/32"
                }
        };
        }

        public IEnumerable<HomeService> GetAllHomeServices()
        {
            return homeServiceList.OrderBy(k=>k.ServiceType);
        }

        public IEnumerable<HomeService> GetByMember(string member)
        {
            return GetAllHomeServices().Where(x => x.LeasedTo.ToString() == member).OrderBy(k => k.ServiceType);
        }

        public IEnumerable<HomeService> GetByServiceType(string type)
        {
            return GetAllHomeServices().Where(x => x.ServiceType.ToString() == type).OrderBy(k=>k.LeasedTo);
        }

        public HomeService GetById(int id)
        {
            return homeServiceList.FirstOrDefault(item => item.Id == id);
        }
    }
}