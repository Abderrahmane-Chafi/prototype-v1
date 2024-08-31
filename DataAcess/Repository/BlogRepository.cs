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

    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private ApplicationDbContext _db;
        public BlogRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Blog obj)
        {
            _db.Blogs.Update(obj);
        }
    }
}
