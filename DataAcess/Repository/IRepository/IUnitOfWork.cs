using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }
        IBlogRepository Blog { get; }
        IClientInformationRepository ClientInformation { get; }
        IRealisedProjectsRepository RealisedProjects { get; }
        IFreeGuideEmailsRepository FreeGuideEmails { get; }
        void Save();
    }
}
