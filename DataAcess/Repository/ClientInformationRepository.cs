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
    public class ClientInformationRepository : Repository<ClientInformation>, IClientInformationRepository
    {
        private ApplicationDbContext _db;
        public ClientInformationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClientInformation obj)
        {
            _db.ClientInformation.Update(obj);
        }
    }
}
