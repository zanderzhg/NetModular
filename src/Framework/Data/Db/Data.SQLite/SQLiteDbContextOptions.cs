﻿using System;
using System.Data;
using System.IO;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core;

namespace NetModular.Lib.Data.SQLite
{
    /// <summary>
    /// SQLite数据库上下文配置项
    /// </summary>
    public class SQLiteDbContextOptions : DbContextOptionsAbstract
    {
        public SQLiteDbContextOptions(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new SQLiteAdapter(dbOptions, options, loggerFactory), loggerFactory, loginInfo)
        {
            SqlMapper.AddTypeHandler(new GuidTypeHandler());

            if (options.ConnectionString.IsNull())
            {
                options.Version = dbOptions.Version;
                string dbFilePath = Path.Combine(AppContext.BaseDirectory, "Db");
                if (DbOptions.Server.NotNull())
                {
                    dbFilePath = Path.GetFullPath(DbOptions.Server);
                }

                dbFilePath = Path.Combine(dbFilePath, options.Database);

                var connStrBuilder = new SqliteConnectionStringBuilder
                {
                    DataSource = $"{dbFilePath}.db",
                    Mode = SqliteOpenMode.ReadWriteCreate
                };

                options.ConnectionString = connStrBuilder.ToString();
            }
        }

        public override IDbConnection NewConnection()
        {
            return new SqliteConnection(DbModuleOptions.ConnectionString);
        }
    }
}
