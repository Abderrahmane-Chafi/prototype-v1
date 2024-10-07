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

    public class FreeGuideEmailsRepository : Repository<FreeGuideEmails>, IFreeGuideEmailsRepository
    {
        private ApplicationDbContext _db;
        public FreeGuideEmailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(FreeGuideEmails obj)
        {
            _db.FreeGuideEmails.Update(obj);
        }
    }
}
