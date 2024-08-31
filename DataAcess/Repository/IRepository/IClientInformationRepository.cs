using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repository.IRepository
{
    public interface IClientInformationRepository : IRepository<ClientInformation>
    {
        void Update(ClientInformation obj);
    }

}
