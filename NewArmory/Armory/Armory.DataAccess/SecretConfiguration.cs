using System;
using System.Collections.Generic;
using System.Text;

namespace Armory.DataAccess
{
    internal static class SecretConfiguration
    {
        internal static readonly string ConnectionString = "Server=tcp:1907-training-yandukin-sql.database.windows.net,1433;Initial Catalog=ArmoryDb;Persist Security Info=False;User ID=ayandukin7;Password=12Reva34;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //dotnet ef dbcontext scaffold "Server=tcp:1907-training-yandukin-sql.database.windows.net,1433;Initial Catalog=ArmoryDb;Persist Security Info=False;User ID=ayandukin7;Password=12Reva34;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer --startup-project ../ArmoryDb.App --force --output-dir Entities
    }
}
