using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    [TestFixture]
    static class Connect4Test
    {

        private static string case1 = @"-------
                                        -------
                                        -------
                                        -------
                                        -------
                                        x-xx-xx
                                       ";

        private static string case2 = @"-------
                                        -------
                                        -------
                                        ---x--x
                                        ---x--x
                                        x-xx-xx
                                       ";

        private static string case3 = @"-------
                                        -------
                                        -------
                                        ---xx-x
                                        ---ooxo
                                        xoxxoxx
                                       ";

        private static string case4 = @"-------
                                        -------
                                        -------
                                        --xox-x
                                        -xoooxo
                                        xoxxoxx
                                       ";

        private static string case5 = @"-------
                                        -------
                                        -------
                                        --x-x-x
                                        -0x-xxo
                                        xoxxoxx
                                       ";

        [Test]
        public static void Test()
        {
            Assert.AreEqual(new int[] { 2, 5 }, Connect4.Start(case1), "Case 1");
            Assert.AreEqual(new int[] { 2, 4, 5, 7 }, Connect4.Start(case2), "Case 2");
            Assert.AreEqual(new int[] { 4, 6 }, Connect4.Start(case3), "Case 3");
            Assert.AreEqual(new int[] { 4 }, Connect4.Start(case4), "Case 4");
            Assert.AreEqual(new int[] { 3, 4 }, Connect4.Start(case5), "Case 5");
        }


    }

}
