using System.Linq;
using Iql.Data.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.Search
{
    [TestClass]
    public class SearchTermExtractionTests
    {
        [TestMethod]
        public void TestExtractSearchTerms()
        {
            var extractor = new SearchTermExtractor();
            var terms = extractor.ExtrapolateSearchTerms(
                    @"I am ""here to"" test this ""function"" out on ""you")
                .Select(_ => _.Value).ToArray();
            Assert.AreEqual(9, terms.Length);
            var index = 0;
            Assert.AreEqual("here to", terms[index++]);
            Assert.AreEqual("function", terms[index++]);
            Assert.AreEqual("I", terms[index++]);
            Assert.AreEqual("am", terms[index++]);
            Assert.AreEqual("test", terms[index++]);
            Assert.AreEqual("this", terms[index++]);
            Assert.AreEqual("out", terms[index++]);
            Assert.AreEqual("on", terms[index++]);
            Assert.AreEqual("\"you", terms[index++]);
        }
    }
}