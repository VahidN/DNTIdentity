using ASPNETCoreIdentitySample.Services.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASPNETCoreIdentitySample.MsTests
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2162
    /// </summary>
    [TestClass]
    public class CustomNormalizerTests
    {
        [TestMethod]
        [DataRow("part1.part2@gmail.com", "PART1PART2@GMAIL.COM")]
        [DataRow("part1...part2@gmail.com", "PART1PART2@GMAIL.COM")]
        [DataRow("pa.rt1.par.t2@gmail.com", "PART1PART2@GMAIL.COM")]
        [DataRow("p.ar.t1.pa.rt.2@gmail.com", "PART1PART2@GMAIL.COM")]
        public void Test_Gmail_Address_With_Dots_CanBe_Normalized(string actual, string expected)
        {
            var customNormalizer = new CustomNormalizer();
            Assert.AreEqual(expected, customNormalizer.NormalizeEmail(actual));
        }

        [TestMethod]
        [DataRow("part1.part2+spamsite@gmail.com", "PART1PART2@GMAIL.COM")]
        [DataRow("pa.rt1.par.t2+spam.site@gmail.com", "PART1PART2@GMAIL.COM")]
        public void Test_Gmail_Address_With_Plus_CanBe_Normalized(string actual, string expected)
        {
            var customNormalizer = new CustomNormalizer();
            Assert.AreEqual(expected, customNormalizer.NormalizeEmail(actual));
        }
    }
}