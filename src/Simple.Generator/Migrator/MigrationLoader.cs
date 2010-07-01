using System;
using System.Collections.Generic;
using System.Reflection;
using Simple.Migrator.Framework;

namespace Simple.Migrator
{
    /// <summary>
    /// Handles inspecting code to find all of the Migrations in assemblies and reading
    /// other metadata such as the last revision, etc.
    /// </summary>
    public class MigrationLoader
    {
        private readonly ITransformationProvider _provider;
        private readonly List<Type> _migrationsTypes = new List<Type>();

        public MigrationLoader(ITransformationProvider provider, IList<Type> types, bool trace)
        {
            _provider = provider;
            AddMigrations(types);

            if (trace)
            {
                provider.Logger.Trace("Loaded migrations:");
                foreach (Type t in _migrationsTypes)
                {
                    provider.Logger.Trace("{0} {1}", GetMigrationVersion(t).ToString().PadLeft(5), StringUtils.ToHumanName(t.Name));
                }
            }
        }

        public void AddMigrations(IList<Type> types)
        {
            if (types != null)
                _migrationsTypes.AddRange(types);
        }

        /// <summary>
        /// Returns registered migration <see cref="System.Type">types</see>.
        /// </summary>
        public List<Type> MigrationsTypes
        {
            get { return _migrationsTypes; }
        }

        /// <summary>
        /// Returns the last version of the migrations.
        /// </summary>
        public long LastVersion
        {
            get
            {
                if (_migrationsTypes.Count == 0)
                    return 0;
                return GetMigrationVersion(_migrationsTypes[_migrationsTypes.Count - 1]);
            }
        }

        /// <summary>
        /// Check for duplicated version in migrations.
        /// </summary>
        /// <exception cref="CheckForDuplicatedVersion">CheckForDuplicatedVersion</exception>
        public void CheckForDuplicatedVersion()
        {
            List<long> versions = new List<long>();
            foreach (Type t in _migrationsTypes)
            {
                long version = GetMigrationVersion(t);

                if (versions.Contains(version))
                    throw new DuplicatedVersionException(version);

                versions.Add(version);
            }
        }

       

        /// <summary>
        /// Returns the version of the migration
        /// <see cref="MigrationAttribute">MigrationAttribute</see>.
        /// </summary>
        /// <param name="t">Migration type.</param>
        /// <returns>Version number sepcified in the attribute</returns>
        public static long GetMigrationVersion(Type t)
        {
            MigrationAttribute attrib = (MigrationAttribute)
                                        Attribute.GetCustomAttribute(t, typeof(MigrationAttribute));

            return attrib.Version;
        }

        public List<long> GetAvailableMigrations()
        {
        	//List<int> availableMigrations = new List<int>();
            _migrationsTypes.Sort(new MigrationTypeComparer(true));
            return _migrationsTypes.ConvertAll(new Converter<Type, long>(GetMigrationVersion));
        }
        
        public IMigration GetMigration(long version)
        {
            foreach (Type t in _migrationsTypes)
            {
                if (GetMigrationVersion(t) == version)
                {
                    IMigration migration = (IMigration)Activator.CreateInstance(t);
                    migration.Database = _provider;
                    return migration;
                }
            }

            return null;
        }
    }
}
