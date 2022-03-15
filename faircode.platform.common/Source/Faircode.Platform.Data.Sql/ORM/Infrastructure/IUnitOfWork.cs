using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faircode.Platform.Data.Sql.ORM.Infrastructure
{
    public interface IUnitOfWork
    {
        void SubmitChanges();
        DbContext GetDbContext();
    }
}
