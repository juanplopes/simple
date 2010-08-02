using System;
using System.Collections.Generic;
using System.Reflection;
using Simple.Migrator.Framework;
using Simple.Migrator.Framework.Loggers;

namespace Simple.Migrator
{
    /// <summary>
    /// Migrations mediator.
    /// </summary>
    public class DbMigrator : IDisposable
    {
        private readonly ITransformationProvider _provider;

        private readonly MigrationLoader _migrationLoader;

        private ILogger _logger = new Logger(false);
        private MigratorOptions _options = null;
        protected bool _dryrun;
        private string[] _args;

        public string[] args
        {
            get { return _args; }
            set { _args = value; }
        }

        public DbMigrator(MigratorOptions options)
        {
            var provider = ProviderFactory.Create(options.Provider, options.ConnectionString);
            _migrationLoader = new MigrationLoader(provider, options.MigrationTypes, false);
            _migrationLoader.CheckForDuplicatedVersion();
            _provider = provider;
            Logger = new Logger(false, new ConsoleWriter());
            _options = options;
            provider.Writer = _options.Commands;

        }

        protected DbMigrator(ITransformationProvider provider, bool trace, ILogger logger)
        {
        }


        /// <summary>
        /// Returns registered migration <see cref="System.Type">types</see>.
        /// </summary>
        public List<Type> MigrationsTypes
        {
            get { return _migrationLoader.MigrationsTypes; }
        }

        public void Migrate(long? version)
        {
            if (version.HasValue)
                MigrateTo(version.Value, _options.SchemaInfoTable);
            else
                MigrateToLastVersion(_options.SchemaInfoTable);
        }

        /// <summary>
        /// Run all migrations up to the latest.  Make no changes to database if
        /// dryrun is true.
        /// </summary>
        public void MigrateToLastVersion(string schemainfoname)
        {
            MigrateTo(_migrationLoader.LastVersion, schemainfoname);
        }

        /// <summary>
        /// Returns the current migrations applied to the database.
        /// </summary>
        public List<long> AppliedMigrations
        {
            get { return _provider.AppliedMigrations; }
        }

        /// <summary>
        /// Get or set the event logger.
        /// </summary>
        public ILogger Logger
        {
            get { return _logger; }
            set
            {
                _logger = value;
                _provider.Logger = value;
            }
        }

        public virtual bool DryRun
        {
            get { return _dryrun; }
            set { _dryrun = value; }
        }

        /// <summary>
        /// Migrate the database to a specific version.
        /// Runs all migration between the actual version and the
        /// specified version.
        /// If <c>version</c> is greater then the current version,
        /// the <c>Up()</c> method will be invoked.
        /// If <c>version</c> lower then the current version,
        /// the <c>Down()</c> method of previous migration will be invoked.
        /// If <c>dryrun</c> is set, don't write any changes to the database.
        /// </summary>
        /// <param name="version">The version that must became the current one</param>
        public void MigrateTo(long version, string schemainfoname)
        {

            if (_migrationLoader.MigrationsTypes.Count == 0)
            {
                _logger.Warn("No public classes with the Migration attribute were found.");
                return;
            }

            bool firstRun = true;
            BaseMigrate migrate = BaseMigrate.GetInstance(_migrationLoader.GetAvailableMigrations(), _provider, _logger, schemainfoname);
            migrate.DryRun = DryRun;
            Logger.Started(migrate.AppliedVersions, version);

            while (migrate.Continue(version))
            {
                IMigration migration = _migrationLoader.GetMigration(migrate.Current);
                if (null == migration)
                {
                    _logger.Skipping(migrate.Current);
                    migrate.Iterate();
                    continue;
                }

                try
                {
                    if (firstRun)
                    {
                        migration.InitializeOnce(_args);
                        firstRun = false;
                    }

                    migrate.Migrate(migration, schemainfoname);
                }
                catch (Exception ex)
                {
                    Logger.Exception(migrate.Current, migration.Name, ex);

                    // Oho! error! We rollback changes.
                    Logger.RollingBack(migrate.Previous);
                    _provider.Rollback();

                    throw;
                }

                migrate.Iterate();
            }

            Logger.Finished(migrate.AppliedVersions, version);
        }

        #region IDisposable Members

        public void Dispose()
        {
            _provider.Dispose();
        }

        #endregion
    }
}
