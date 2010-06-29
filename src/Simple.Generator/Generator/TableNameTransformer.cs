using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator
{
    public class TableNameTransformer
    {
        public class Result
        {
            public IList<string> Included { get; internal set; }
            public IList<string> Excluded { get; internal set; }
        }

        private const string DefaultDefaultIdentifier = "$";

        public IEnumerable<string> DefaultNames { get; protected set; }
        public string DefaultIdentifier { get; protected set; }

        public TableNameTransformer(IEnumerable<string> defaultNames, string defaultIdentifier)
        {
            this.DefaultNames = defaultNames ?? new List<string>();
            this.DefaultIdentifier = defaultIdentifier ?? DefaultDefaultIdentifier;
        }

        public TableNameTransformer(IEnumerable<string> defaultNames) : this(defaultNames, null) { }

        protected IEnumerable<string> ExpandDefault(bool reverse)
        {
            foreach (var item in DefaultNames)
                if (!reverse)
                    yield return item;
                else
                {
                    if (item.Length > 0 && item[0] == '-')
                        yield return item.Substring(1);
                    else
                        yield return "-" + item;
                }
        }

        protected IEnumerable<string> SearchIdentifier(IEnumerable<string> input)
        {
            foreach (var item in input)
            {
                if (item.Trim() == DefaultIdentifier)
                    foreach (var def in ExpandDefault(false))
                        yield return def;
                else if (item.Trim() == "-" + DefaultIdentifier)
                    foreach (var def in ExpandDefault(true))
                        yield return def;
                else
                    yield return item;

            }
        }

        public Result Transform(IEnumerable<string> input)
        {
            if (input == null || !input.Any())
                input = new[] { DefaultIdentifier };

            var list = new List<string>(SearchIdentifier(input ?? DefaultNames));

            var excluded = list.Where(x => x.StartsWith("-"));
            var included = list.Except(excluded).ToList();

            return new Result() { Included = included, Excluded = excluded.Select(x => x.Substring(1)).ToList() };

        }


    }
}
