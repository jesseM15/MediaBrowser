using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaBrowser;

using System.Collections.Generic;

namespace MediaBrowserTests
{
    [TestClass]
    public class ListHelperTests
    {
        [TestMethod]
        public void ListHelperStringIsCommaSeperatedTest()
        {
            List<string> list = new List<string>(new string[] { "one", "two", "three" });

            string actual = ListHelper.CreateCommaSeperatedString(list);

            string expected = "one,two,three";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ListHelperListIsTrimmedTest()
        {
            List<string> list = new List<string>(new string[] { "one ", "two", " three" });

            List<string> actual = ListHelper.ListTrim(list);

            List<string> expected = new List<string>(new string[] { "one", "two", "three" });
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ListHelperOrderedByLastNamesTest()
        {
            List<string> list = new List<string>(new string[] {"John Doe","Jane Doe","Mike Smith"});

            SortedDictionary<string,string> actual = ListHelper.OrderByLastNames(list);

            SortedDictionary<string, string> expected = new SortedDictionary<string, string>();
            expected.Add("Doe, Jane", "Jane Doe");
            expected.Add("Doe, John", "John Doe");
            expected.Add("Smith, Mike", "Mike Smith");
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ListHelperRemovedParenthesesTest()
        {
            List<string> list = new List<string>(new string[] { "Ted King(screenplay)", "Amanda Lockheart(writer)" });

            List<string> actual = ListHelper.RemoveParentheses(list);

            List<string> expected = new List<string>(new string[] { "Ted King", "Amanda Lockheart" });
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
