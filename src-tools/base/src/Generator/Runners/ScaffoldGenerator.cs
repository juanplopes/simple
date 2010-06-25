using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Project.Generator.Runners
{
    public class ScaffoldGenerator : BaseTableGenerator
    {
        IList<BaseTableGenerator> _generators = new List<BaseTableGenerator>();

        public override void Execute()
        {
            _generators.Add(new EntityGenerator() { Tables = this.Tables });
            _generators.Add(new MappingGenerator() { Tables = this.Tables });
            base.Execute();
        }

        public override void ExecuteSingle(Simple.Metadata.DbTable table)
        {
            foreach (var g in _generators)
                g.ExecuteSingle(table);
        }
    }
}
