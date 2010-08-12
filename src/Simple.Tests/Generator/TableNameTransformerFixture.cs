using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Generator;

namespace Simple.Tests.Generator
{
    public class TableNameTransformerFixture
    {
        [Test]
        public void EmptyListReturnsDefaultTables()
        {
            var transformer = new TableNameTransformer(new[] { "%", "-test" });
            var result = transformer.Transform(new string[] { });

            result.Included.Should().Have.SameSequenceAs(new[] { "%" });
            result.Excluded.Should().Have.SameSequenceAs(new[] { "test" });
        }

        [Test]
        public void NullListReturnsDefaultTables()
        {
            var transformer = new TableNameTransformer(new[] { "%", "-test" });
            var result = transformer.Transform(null);

            result.Included.Should().Have.SameSequenceAs(new[] { "%" });
            result.Excluded.Should().Have.SameSequenceAs(new[] { "test" });
        }

        [Test]
        public void NoDefaultMarkResultInSameOutput()
        {
            var transformer = new TableNameTransformer(new[] { "%", "-test" });
            var result = transformer.Transform(new[] { "asd", "-qwe" });

            result.Included.Should().Have.SameSequenceAs(new[] { "asd" });
            result.Excluded.Should().Have.SameSequenceAs(new[] { "qwe" });
        }

        [Test]
        public void DefaultMarkResultInExpandedOutput()
        {
            var transformer = new TableNameTransformer(new[] { "%", "-test" });
            var result = transformer.Transform(new[] { "asd", "-qwe", "$ " });

            result.Included.Should().Have.SameSequenceAs(new[] { "asd", "%" });
            result.Excluded.Should().Have.SameSequenceAs(new[] { "qwe", "test" });
        }

        [Test]
        public void CustomMarkResultInExpandedOutput()
        {
            var transformer = new TableNameTransformer(new[] { "%", "-test" }, "###");
            var result = transformer.Transform(new[] { "asd", "-qwe", "### " });

            result.Included.Should().Have.SameSequenceAs(new[] { "asd", "%" });
            result.Excluded.Should().Have.SameSequenceAs(new[] { "qwe", "test" });
        }
        [Test]
        public void InvertedCustomMarkResultInExpandedInvertedOutput()
        {
            var transformer = new TableNameTransformer(new[] { "%", "-test" }, "###");
            var result = transformer.Transform(new[] { "asd", "-### ", "-qwe" });

            result.Included.Should().Have.SameSequenceAs(new[] { "asd", "test" });
            result.Excluded.Should().Have.SameSequenceAs(new[] { "%", "qwe" });
        }
    }
}
