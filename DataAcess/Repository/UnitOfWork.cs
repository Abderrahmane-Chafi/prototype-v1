using DataAcess.Data;
using DataAcess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IBlogRepository Blog { get; private set; }
        public IClientInformationRepository ClientInformation { get; private set; }
        public IRealisedProjectsRepository RealisedProjects { get; private set; }   
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            Blog = new BlogRepository(_db);
            ClientInformation = new ClientInformationRepository(_db);
            RealisedProjects = new RealisedProjectsRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }

}
