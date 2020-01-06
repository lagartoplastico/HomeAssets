using HomeAssets.Models.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAssets.Models
{
    public class NpgsqlHomeServiceRepo : IHomeServiceRepo
    {
        private readonly AppDbContext context;

        public NpgsqlHomeServiceRepo(AppDbContext context)
        {
            this.context = context;
        }
        public HomeService AddHomeService(HomeService newHomeService)
        {
            context.HomeServices.Add(newHomeService);
            context.SaveChanges();
            return newHomeService;
        }

        public HomeService DeleteHomeService(HomeService homeService)
        {
            HomeService HStoDelete = context.HomeServices.Find(homeService.Id);
            if (HStoDelete != null)
            {
                context.HomeServices.Remove(HStoDelete);
                context.SaveChanges();
            }
            return HStoDelete;
        }

        public IEnumerable<HomeService> GetAllHomeServices()
        {
            return context.HomeServices;
        }

        public HomeService GetById(int id)
        {
            return context.HomeServices.Find(id);
        }

        public IEnumerable<HomeService> GetByMember(string member)
        {
            return GetAllHomeServices().Where(x => x.LeasedTo.ToString() == member).OrderBy(k => k.ServiceType);
        }

        public IEnumerable<HomeService> GetByServiceType(string type)
        {
            return GetAllHomeServices().Where(x => x.ServiceType.ToString() == type).OrderBy(k => k.LeasedTo);
        }

        public IEnumerable<HomeService> GetByLocation(string location)
        {
            return GetAllHomeServices().Where(x => x.Location.ToString() == location).OrderBy(k => k.ServiceType);
        }

        public HomeService UpdateHomeService(HomeService changedHomeService)
        {
            var HStoUpdate = context.HomeServices.Attach(changedHomeService);
            HStoUpdate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changedHomeService;
        }
    }
}
