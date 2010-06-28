using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Metadata;
using Sample.Project.Generator.Templates;
using Simple;
using Sample.Project.Services;

namespace Sample.Project.Generator.Infra
{
    public class ScaffoldGenerator : TableTemplate
    {
        IList<TableTemplate> _generators = new List<TableTemplate>();

        public override void Execute()
        {
            _generators.Clear();
            
            AddGenerator<EntityTemplate>();
            AddGenerator<MappingTemplate>();
            AddGenerator<ServiceInterfaceTemplate>();
            AddGenerator<ServiceImplTemplate>();
            AddGenerator<ValidatorTemplate>();
            
            base.Execute();
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

        protected void AddGenerator<T>()
            where T : TableTemplate, new()
        {
            _generators.Add(new T() { Tables = this.Tables });
        }
    }
}
