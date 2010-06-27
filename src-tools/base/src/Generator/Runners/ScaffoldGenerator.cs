using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Metadata;

namespace Sample.Project.Generator.Runners
{
    public class ScaffoldGenerator : BaseTableGenerator
    {
        IList<BaseTableGenerator> _generators = new List<BaseTableGenerator>();

        public override void Execute()
        {
            _generators.Clear();
            
            AddGenerator<EntityGenerator>();
            AddGenerator<MappingGenerator>();
            AddGenerator<ServiceInterfaceGenerator>();
            AddGenerator<ServiceImplGenerator>();
            AddGenerator<ValidatorGenerator>();
            
            base.Execute();
        }

        protected void AddGenerator<T>()
            where T : BaseTableGenerator, new()
        {
            _generators.Add(new T() { Tables = this.Tables });
        }

        public override void Create(DbTable table)
        {
            foreach (var g in _generators)
                g.Create(table);
        }

        public override void Delete(DbTable table)
        {
            foreach (var g in _generators)
                g.Delete(table);
        }
    }
}
