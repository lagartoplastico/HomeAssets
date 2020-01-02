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
                    Institution = Institutions.Ypfb,
                    LeasedTo = Members.Gary,
                    ServiceType = ServiceTypes.Gas_Domiciliario,
                    PaymentCriteria = PaymentCriterias.Codigo_de_cliente,
                    PaymentId = "1-00203-J88221",
                    Location = Locations.Senkata
                },
                new HomeService()
                {
                    Id = 2,
                    Institution = Institutions.Tigo,
                    LeasedTo = Members.Joannes,
                    ServiceType = ServiceTypes.Internet,
                    PaymentCriteria = PaymentCriterias.Instancia,
                    PaymentId = "A-0290113",
                    Location= Locations.Tokio

                },
                new HomeService()
                {
                    Id = 3,
                    Institution = Institutions.Entel,
                    LeasedTo = Members.Gladys,
                    ServiceType = ServiceTypes.Internet,
                    PaymentCriteria = PaymentCriterias.Instancia,
                    PaymentId = "3-5935/32",
                    Location = Locations.Arguedas
                },

                new HomeService()
                {
                    Id = 4,
                    Institution = Institutions.Delapaz,
                    LeasedTo = Members.Adhemar,
                    ServiceType = ServiceTypes.Energia_Electrica,
                    PaymentCriteria = PaymentCriterias.Numero_de_medidor,
                    PaymentId = "3-5935/32",
                    Location = Locations.Tunari
                },

                new HomeService()
                {
                    Id = 5,
                    Institution = Institutions.Delapaz,
                    LeasedTo = Members.Jose,
                    ServiceType = ServiceTypes.Energia_Electrica,
                    PaymentCriteria = PaymentCriterias.Codigo_de_cliente,
                    PaymentId = "3-3335/32",
                    Location = Locations.Tunari

                },
                
                new HomeService()
                {
                    Id = 6,
                    Institution = Institutions.Ypfb,
                    LeasedTo = Members.Joannes,
                    ServiceType = ServiceTypes.Gas_Domiciliario,
                    PaymentCriteria = PaymentCriterias.Codigo_de_cliente,
                    PaymentId = "3-5935/32",
                    Location= Locations.Arguedas
                },

                new HomeService()
                {
                    Id = 7,
                    Institution = Institutions.Entel,
                    LeasedTo = Members.Joannes,
                    ServiceType = ServiceTypes.Internet,
                    PaymentCriteria = PaymentCriterias.Instancia,
                    PaymentId = "T-121/32",
                    Location = Locations.Arguedas
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

        public IEnumerable<HomeService> GetByLocation(string location)
        {
            return GetAllHomeServices().Where(x => x.Location.ToString() == location).OrderBy(k => k.ServiceType);
        }

        public HomeService GetById(int id)
        {
            return homeServiceList.FirstOrDefault(item => item.Id == id);
        }

        public HomeService AddHomeService(HomeService newHomeService)
        {
            newHomeService.Id = homeServiceList.Max(n => n.Id) + 1;
            homeServiceList.Add(newHomeService);
            return newHomeService;
        }
    }
}