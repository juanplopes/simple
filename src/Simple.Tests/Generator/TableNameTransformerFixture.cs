using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Generator;

namespace Simple.Tests.Generator
{
    public class TableNameTransformerFixture
    {
        [Test]
        public void NoDefaultMarkResultInSameOutput()
        {
            var transformer = new TableNameTransformer(new[] { "%", "-test" });
            var result = transformer.Transform(new[] { "asd", "-qwe"});

            CollectionAssert.AreEqual(new[] { "asd" }, result.Included);
            CollectionAssert.AreEqual(new[] { "qwe" }, result.Excluded);
        }

        [Test]
        public void DefaultMarkResultInExpandedOutput()
        {
            var transformer = new TableNameTransformer(new[] { "%", "-test" });
            var result = transformer.Transform(new[] { "asd", "-qwe", "$ " });

            CollectionAssert.AreEqual(new[] { "asd", "%" }, result.Included);
            CollectionAssert.AreEqual(new[] { "qwe", "test" }, result.Excluded);
        }

        [Test]
        public void CustomMarkResultInExpandedOutput()
        {
            var transformer = new TableNameTransformer(new[] { "%", "-test" }, "###");
            var result = transformer.Transform(new[] { "asd", "-qwe", "### " });

            CollectionAssert.AreEqual(new[] { "asd", "%" }, result.Included);
            CollectionAssert.AreEqual(new[] { "qwe", "test" }, result.Excluded);
        }
        [Test]
        public void InvertedCustomMarkResultInExpandedInvertedOutput()
        {
            var transformer = new TableNameTransformer(new[] { "%", "-test" }, "###");
            var result = transformer.Transform(new[] { "asd", "-### ", "-qwe" });

            CollectionAssert.AreEqual(new[] { "asd", "test" }, result.Included);
            CollectionAssert.AreEqual(new[] { "%", "qwe" }, result.Excluded);
        }
    }
}
