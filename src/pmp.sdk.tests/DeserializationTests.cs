using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace pmp.sdk.tests
{
    [TestFixture]
    public class DeserializationTests
    {
        [Test]
        public void CanDeserialize_HomeDocument()
        {
            var json = File.ReadAllText("response.json");
            var obj = PmpSdk.Deserialize(json);

            Assert.IsNotNullOrEmpty(obj.Version);
            Assert.IsNotNullOrEmpty(obj.Href);

            var rels = obj.GetQueryRelTypes();
            rels.ToList().ForEach(r => Console.WriteLine(r));


            var uri = obj.Query("urn:collectiondoc:hreftpl:docs");
        }
    }
}
