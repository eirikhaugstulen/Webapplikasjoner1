using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webapplikasjoner1.Models;

namespace Webapplikasjoner1.DAL
{
    public interface IAdminRepository
    {
        Task<bool> OpprettAdmin(Admin admin);

        Task<bool> LoggInn(Admin admin);
    }
}
