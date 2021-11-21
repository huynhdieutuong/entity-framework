using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            // 0. Install dotnet-ef: dotnet tool install --global dotnet-ef

            // Migration (create a snapshot for database)

            /*
            1. Create migration: 
                `````` dotnet ef migrations add MigrationName
                => automatically create Migrations folder
            
            dotnet ef migrations add V0
            dotnet ef migrations add V1 (if code V1 in Models not changed, 20211121070844_V1 is nothing)

            2. Check list & status migrations:
                `````` dotnet ef migrations list
                => 20211121065741_V0 (Pending) => V0: MigrationName | Pending: not yet updated to database

            3. Remove the latest migration:
                `````` dotnet ef migrations remove
                => Remove migration V1

            4. Update migrations to database:
                `````` dotnet ef database update [MigrationName]
                => Create database & tables (if not MigrationName, select the latest migrations)

            5. Delete database:
                `````` dotnet ef database drop -f
                => Drop database
            */


            /* 
            If a model change (Name -> Title):
                1. Create new migration: dotnet ef migrations add V1
                2. Check status: dotnet ef migrations list => (V0 | V1 (Pending))
                3. Update migration V1: dotnet ef database update => Database updated (V0 | V1)

            Want to restore V0:
                1. Update migration V0: dotnet ef database update V0
                2. Check status: dotnet ef migrations list => (V0 | V1 (Pending))
            */


            /*
            Want to change PK "string TagId" -> "int TagId"
                1. Comment "string TagId", create "int NewTagId"
                2. Create new migration: dotnet ef migrations add V2-RemoveTagId
                3. Update migration V2-RemoveTagId: dotnet ef database update
                4. Create new migration: dotnet ef migrations add V2-RenameTagId
                5. Update migration V2-RenameTagId: dotnet ef database update

            */
        }
    }
}
