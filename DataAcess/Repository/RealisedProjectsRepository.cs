using DataAcess.Data;
using DataAcess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repository
{
    public class RealisedProjectsRepository : Repository<RealisedProjects>, IRealisedProjectsRepository
    {
        private ApplicationDbContext _db;
        public RealisedProjectsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(RealisedProjects obj)
        {
            _db.RealisedProjects.Update(obj);
        }
    }
}
