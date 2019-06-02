using HttpServer.databases;
using HttpServer.helpers;
using HttpServer.managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.database
{
    public static class DatabaseInitialiser
    {
        public static void CreateDatabase(BaseDatabase database)
        {
           database.ExecuteNonQuery(
                @"
                    CREATE TABLE IF NOT EXISTS `users` (
                        `Id` int(11) NOT NULL AUTO_INCREMENT,
                        `Username` varchar(50) DEFAULT NULL,
                        `Password` varchar(150) NOT NULL,
                        PRIMARY KEY (`Id`)
                    )
                "
            );

            database.ExecuteNonQuery(
                @"
                    CREATE TABLE IF NOT EXISTS `characters` (
                        `Id` int(11) NOT NULL AUTO_INCREMENT,
                        `UserId` int(11) DEFAULT NULL,
                        `Data` TEXT NULL,
                        PRIMARY KEY (`Id`)
                    )
                "
                );
        }        
    }
}
