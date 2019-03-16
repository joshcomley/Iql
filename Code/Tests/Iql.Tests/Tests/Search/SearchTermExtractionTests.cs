using System.Linq;
using Iql.Entities.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.Search
{
    [TestClass]
    public class SearchTermExtractionTests
    {
        [TestMethod]
        public void TestExtractSearchTerms()
        {
            var terms = new IqlSearchText(@"I am ""here to"" test this ""function"" out on ""you")
                .Terms
                .Select(_ => _.Value).ToArray();
            Assert.AreEqual(9, terms.Length);
            var index = 0;
            Assert.AreEqual("I", terms[index++]);
            Assert.AreEqual("am", terms[index++]);
            Assert.AreEqual("here to", terms[index++]);
            Assert.AreEqual("test", terms[index++]);
            Assert.AreEqual("this", terms[index++]);
            Assert.AreEqual("function", terms[index++]);
            Assert.AreEqual("out", terms[index++]);
            Assert.AreEqual("on", terms[index++]);
            Assert.AreEqual("\"you", terms[index++]);
        }

        [TestMethod]
        public void TestMutateSearchTerms()
        {
            var terms = new IqlSearchText(@"    I   am ""here to"" test this   ""function"" out on ""you  ");
            Assert.AreEqual(@"I am ""here to"" test this ""function"" out on ""you", terms.SearchText);
            var copy = terms.Terms.ToList();
            copy.RemoveAt(terms.Terms.Length - 1);
            terms.Terms = copy.ToArray();
            Assert.AreEqual(@"I am ""here to"" test this ""function"" out on", terms.SearchText);
        }

        [TestMethod]
        public void TestMutateSearchText()
        {
            var terms = new IqlSearchText(@"I am ""here to"" test this ""function"" out on ""you");
            Assert.AreEqual(9, terms.Terms.Length);
            terms.SearchText = "";
            Assert.AreEqual(0, terms.Terms.Length);
        }
    }
}