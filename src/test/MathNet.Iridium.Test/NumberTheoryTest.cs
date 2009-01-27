//-----------------------------------------------------------------------
// <copyright file="NumberTheoryTest.cs" company="Math.NET Project">
//    Copyright (c) 2002-2009, Christoph R�egg.
//    All Right Reserved.
// </copyright>
// <author>
//    Christoph R�egg, http://christoph.ruegg.name
// </author>
// <product>
//    Math.NET Iridium, part of the Math.NET Project.
//    http://mathnet.opensourcedotnet.info
// </product>
// <license type="opensource" name="LGPL" version="2 or later">
//    This program is free software; you can redistribute it and/or modify
//    it under the terms of the GNU Lesser General Public License as published 
//    by the Free Software Foundation; either version 2 of the License, or
//    any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public 
//    License along with this program; if not, write to the Free Software
//    Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA.
// </license>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using MathNet.Numerics.NumberTheory;

namespace Iridium.Test
{
    [TestFixture]
    public class NumberTheoryTest
    {
        [Test]
        public void TestEvenOdd32()
        {
            Assert.IsTrue(IntegerTheory.IsEven(0), "0 is even");
            Assert.IsFalse(IntegerTheory.IsOdd(0), "0 is not odd");

            Assert.IsFalse(IntegerTheory.IsEven(1), "1 is not even");
            Assert.IsTrue(IntegerTheory.IsOdd(1), "1 is odd");

            Assert.IsFalse(IntegerTheory.IsEven(-1), "-1 is not even");
            Assert.IsTrue(IntegerTheory.IsOdd(-1), "-1 is odd");

            Assert.IsFalse(IntegerTheory.IsEven(Int32.MaxValue), "Int32.Max is not even");
            Assert.IsTrue(IntegerTheory.IsOdd(Int32.MaxValue), "Int32.Max is odd");

            Assert.IsTrue(IntegerTheory.IsEven(Int32.MinValue), "Int32.Min is even");
            Assert.IsFalse(IntegerTheory.IsOdd(Int32.MinValue), "Int32.Min is not odd");
        }

        [Test]
        public void TestEvenOdd64()
        {
            Assert.IsTrue(IntegerTheory.IsEven((long)0), "0 is even");
            Assert.IsFalse(IntegerTheory.IsOdd((long)0), "0 is not odd");

            Assert.IsFalse(IntegerTheory.IsEven((long)1), "1 is not even");
            Assert.IsTrue(IntegerTheory.IsOdd((long)1), "1 is odd");

            Assert.IsFalse(IntegerTheory.IsEven((long)-1), "-1 is not even");
            Assert.IsTrue(IntegerTheory.IsOdd((long)-1), "-1 is odd");

            Assert.IsFalse(IntegerTheory.IsEven(Int64.MaxValue), "Int64.Max is not even");
            Assert.IsTrue(IntegerTheory.IsOdd(Int64.MaxValue), "Int64.Max is odd");

            Assert.IsTrue(IntegerTheory.IsEven(Int64.MinValue), "Int64.Min is even");
            Assert.IsFalse(IntegerTheory.IsOdd(Int64.MinValue), "Int64.Min is not odd");
        }

        [Test]
        public void TestIsPerfectSquare32()
        {
            // Test all known suares
            int lastRadix = (int)Math.Floor(Math.Sqrt(Int32.MaxValue));
            for(int i = 0; i <= lastRadix; i++)
            {
                Assert.IsTrue(IntegerTheory.IsPerfectSquare(i * i), i.ToString() + "^2 (+)");
            }

            // Test 1-offset from all known squares
            for(int i = 2; i <= lastRadix; i++)
            {
                Assert.IsFalse(IntegerTheory.IsPerfectSquare((i * i) - 1), i.ToString() + "^2-1 (-)");
                Assert.IsFalse(IntegerTheory.IsPerfectSquare((i * i) + 1), i.ToString() + "^2+1 (-)");
            }

            // Selected Cases
            Assert.IsTrue(IntegerTheory.IsPerfectSquare(100000000), "100000000 (+)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare(100000001), "100000001 (-)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare(99999999), "99999999 (-)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare(-4), "-4 (-)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare(Int32.MinValue), "Int32.MinValue (-)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare(Int32.MaxValue), "Int32.MaxValue (-)");
            Assert.IsTrue(IntegerTheory.IsPerfectSquare(1), "1 (+)");
            Assert.IsTrue(IntegerTheory.IsPerfectSquare(0), "0 (+)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare(-1), "-1 (-)");
        }

        [Test]
        public void TestIsPerfectSquare64()
        {
            // Test all known suares
            for(int i = 0; i < 32; i++)
            {
                long t = ((long)1) << i;
                Assert.IsTrue(IntegerTheory.IsPerfectSquare(t * t), t.ToString() + "^2 (+)");
            }

            // Test 1-offset from all known squares
            for(int i = 1; i < 32; i++)
            {
                long t = ((long)1) << i;
                Assert.IsFalse(IntegerTheory.IsPerfectSquare((t * t) - 1), t.ToString() + "^2-1 (-)");
                Assert.IsFalse(IntegerTheory.IsPerfectSquare((t * t) + 1), t.ToString() + "^2+1 (-)");
            }

            // Selected Cases
            Assert.IsTrue(IntegerTheory.IsPerfectSquare((long)1000000000000000000), "1000000000000000000 (+)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare((long)1000000000000000001), "1000000000000000001 (-)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare((long)999999999999999999), "999999999999999999 (-)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare((long)999999999999999993), "999999999999999993 (-)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare((long)-4), "-4 (-)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare(Int64.MinValue), "Int32.MinValue (-)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare(Int64.MaxValue), "Int32.MaxValue (-)");
            Assert.IsTrue(IntegerTheory.IsPerfectSquare((long)1), "1 (+)");
            Assert.IsTrue(IntegerTheory.IsPerfectSquare((long)0), "0 (+)");
            Assert.IsFalse(IntegerTheory.IsPerfectSquare((long)-1), "-1 (-)");
        }
    }
}
