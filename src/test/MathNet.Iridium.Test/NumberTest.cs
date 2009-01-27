//-----------------------------------------------------------------------
// <copyright file="NumberTest.cs" company="Math.NET Project">
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

using MathNet.Numerics;

namespace Iridium.Test
{
    [TestFixture]
    public class NumberTest
    {
        [Test]
        public void TestIncrementDecrementAtZero()
        {
            double x = 2 * double.Epsilon;
            Assert.AreEqual(2 * double.Epsilon, x, "A");
            x = Number.Decrement(x);
            x = Number.Decrement(x);
            Assert.AreEqual(0, x, "B");
            x = Number.Decrement(x);
            x = Number.Decrement(x);
            Assert.AreEqual(-2 * double.Epsilon, x, "C");
            x = Number.Increment(x);
            x = Number.Increment(x);
            Assert.AreEqual(0, x, "D");
            x = Number.Increment(x);
            x = Number.Increment(x);
            Assert.AreEqual(2 * double.Epsilon, x, "E");
        }

        [Test]
        public void TestIncrementDecrementAtMinMax()
        {
            double x = double.MaxValue;
            Assert.AreEqual(double.MaxValue, x, "A");
            x = Number.Decrement(x);
            Assert.IsTrue(x < double.MaxValue, "B");
            x = Number.Increment(x);
            Assert.AreEqual(double.MaxValue, x, "C");
            x = Number.Increment(x);
            Assert.IsTrue(double.IsPositiveInfinity(x), "D");

            x = double.MinValue;
            Assert.AreEqual(double.MinValue, x, "E");
            x = Number.Increment(x);
            Assert.IsTrue(x > double.MinValue, "F");
            x = Number.Decrement(x);
            Assert.AreEqual(double.MinValue, x, "G");
            x = Number.Decrement(x);
            Assert.IsTrue(double.IsNegativeInfinity(x), "H");
        }

        [Test]
        public void TestIncrementDecrementStep()
        {
            double x0 = 1e-100;
            double x1 = 1e+0;
            double x2 = 1e+100;
            double x3 = 1e+200;
            double x4 = -1e+100;

            double y0 = Number.Increment(x0) - x0;
            double y1 = Number.Increment(x1) - x1;
            double y2 = Number.Increment(x2) - x2;
            double y3 = Number.Increment(x3) - x3;
            double y4 = Number.Increment(x4) - x4;

            Assert.IsTrue(y0 < y1, "A");
            Assert.IsTrue(y1 < y2, "B");
            Assert.IsTrue(y2 < y3, "C");
            Assert.IsTrue(y2 == y4, "D");
         }

        [Test]
        public void TestEpsilonOf()
        {
            IFormatProvider format = System.Globalization.CultureInfo.InvariantCulture;
            Assert.AreEqual("1.11022302462516E-16", Number.EpsilonOf(1.0).ToString(format), "A");
            Assert.AreEqual("1.11022302462516E-16", Number.RelativeAccuracy.ToString(format), "A2");
            Assert.AreEqual("2.22044604925031E-16", Number.PositiveRelativeAccuracy.ToString(format), "A3");
            Assert.AreEqual("1.11022302462516E-16", Number.EpsilonOf(-1.0).ToString(format), "B");
            Assert.AreEqual("4.94065645841247E-324", Number.EpsilonOf(0.0).ToString(format), "C");
            Assert.AreEqual("1.94266889222573E+84", Number.EpsilonOf(1.0e+100).ToString(format), "D");
            Assert.AreEqual("1.94266889222573E+84", Number.EpsilonOf(-1.0e+100).ToString(format), "E");
            Assert.AreEqual("1.26897091865782E-116", Number.EpsilonOf(1.0e-100).ToString(format), "F");
            Assert.AreEqual("1.26897091865782E-116", Number.EpsilonOf(-1.0e-100).ToString(format), "G");
            Assert.AreEqual("1.99584030953472E+292", Number.EpsilonOf(double.MaxValue).ToString(format), "H");
            Assert.AreEqual("1.99584030953472E+292", Number.EpsilonOf(double.MinValue).ToString(format), "I");
            Assert.AreEqual("4.94065645841247E-324", Number.EpsilonOf(double.Epsilon).ToString(format), "J");
            Assert.AreEqual("4.94065645841247E-324", Number.EpsilonOf(-double.Epsilon).ToString(format), "K");
            Assert.IsTrue(double.IsNaN(Number.EpsilonOf(double.NaN)), "L");
            Assert.IsTrue(double.IsNaN(Number.EpsilonOf(double.PositiveInfinity)), "M");
            Assert.IsTrue(double.IsNaN(Number.EpsilonOf(double.NegativeInfinity)), "N");
        }

        [Test]
        public void TestLexicographicalOrder()
        {
            Assert.AreEqual(2, Number.ToLexicographicalOrderedUInt64(2 * double.Epsilon), "A");
            Assert.AreEqual(1, Number.ToLexicographicalOrderedUInt64(1 * double.Epsilon), "B");
            Assert.AreEqual(0, Number.ToLexicographicalOrderedUInt64(0.0), "C");
            Assert.AreEqual(0xFFFFFFFFFFFFFFFF, Number.ToLexicographicalOrderedUInt64(-1 * double.Epsilon), "D");
            Assert.AreEqual(0xFFFFFFFFFFFFFFFE, Number.ToLexicographicalOrderedUInt64(-2 * double.Epsilon), "E");

            Assert.AreEqual(2, Number.ToLexicographicalOrderedInt64(2 * double.Epsilon), "N");
            Assert.AreEqual(1, Number.ToLexicographicalOrderedInt64(1 * double.Epsilon), "O");
            Assert.AreEqual(0, Number.ToLexicographicalOrderedInt64(0.0), "P");
            Assert.AreEqual(-1, Number.ToLexicographicalOrderedInt64(-1 * double.Epsilon), "Q");
            Assert.AreEqual(-2, Number.ToLexicographicalOrderedInt64(-2 * double.Epsilon), "R");
        }

        [Test]
        public void TestSignedMagnitudeToTwosComplement()
        {
            Assert.AreEqual(2, Number.SignedMagnitudeToTwosComplementUInt64(2), "A");
            Assert.AreEqual(1, Number.SignedMagnitudeToTwosComplementUInt64(1), "B");
            Assert.AreEqual(0, Number.SignedMagnitudeToTwosComplementUInt64(0), "C");
            Assert.AreEqual(0, Number.SignedMagnitudeToTwosComplementUInt64(-9223372036854775808), "D");
            Assert.AreEqual(0xFFFFFFFFFFFFFFFF, Number.SignedMagnitudeToTwosComplementUInt64(-9223372036854775808 + 1), "E");
            Assert.AreEqual(0xFFFFFFFFFFFFFFFE, Number.SignedMagnitudeToTwosComplementUInt64(-9223372036854775808 + 2), "F");

            Assert.AreEqual(2, Number.SignedMagnitudeToTwosComplementInt64(2), "M");
            Assert.AreEqual(1, Number.SignedMagnitudeToTwosComplementInt64(1), "O");
            Assert.AreEqual(0, Number.SignedMagnitudeToTwosComplementInt64(0), "P");
            Assert.AreEqual(0, Number.SignedMagnitudeToTwosComplementInt64(-9223372036854775808), "Q");
            Assert.AreEqual(-1, Number.SignedMagnitudeToTwosComplementInt64(-9223372036854775808 + 1), "R");
            Assert.AreEqual(-2, Number.SignedMagnitudeToTwosComplementInt64(-9223372036854775808 + 2), "S");
        }

        [Test]
        public void TestNumbersBetween()
        {
            Assert.AreEqual(0, Number.NumbersBetween(1.0, 1.0), "A");
            Assert.AreEqual(0, Number.NumbersBetween(0, 0), "B");
            Assert.AreEqual(0, Number.NumbersBetween(-1.0, -1.0), "C");
            Assert.AreEqual(1, Number.NumbersBetween(0, double.Epsilon), "D");
            Assert.AreEqual(1, Number.NumbersBetween(0, -double.Epsilon), "E");
            Assert.AreEqual(1, Number.NumbersBetween(double.Epsilon, 0), "D2");
            Assert.AreEqual(1, Number.NumbersBetween(-double.Epsilon, 0), "E2");
            Assert.AreEqual(2, Number.NumbersBetween(0, 2*double.Epsilon), "F");
            Assert.AreEqual(2, Number.NumbersBetween(0, -2 * double.Epsilon), "G");
            Assert.AreEqual(3, Number.NumbersBetween(-double.Epsilon, 2 * double.Epsilon), "H");
            Assert.AreEqual(3, Number.NumbersBetween(double.Epsilon, -2 * double.Epsilon), "I");

            double test = Math.PI * 1e+150;
            Assert.AreEqual(10, Number.NumbersBetween(test, test + (10 * Number.EpsilonOf(test))), "J");
            Assert.AreEqual(10, Number.NumbersBetween(test, test - (10 * Number.EpsilonOf(test))), "K");

            Assert.AreEqual(450359962737, Number.NumbersBetween(1.0001, 1.0002), "L");
            Assert.AreEqual(54975582, Number.NumbersBetween(10000.0001, 10000.0002), "M");
            Assert.AreEqual(53687, Number.NumbersBetween(10000000.0001, 10000000.0002), "N");
            Assert.AreEqual(53, Number.NumbersBetween(10000000000.0001, 10000000000.0002), "O");

            Assert.AreEqual(0xFFDFFFFFFFFFFFFE, Number.NumbersBetween(double.MinValue, double.MaxValue), "R");
        }

        [Test]
        public void TestAlmostEqual()
        {
            IFormatProvider format = System.Globalization.CultureInfo.InvariantCulture;

            double max = double.MaxValue;
            double min = double.MinValue;

            Assert.IsTrue(Number.AlmostEqual(0.0, 0.0, 0), "A");
            Assert.IsTrue(Number.AlmostEqual(0.0, 0.0, 50), "B");
            Assert.IsTrue(Number.AlmostEqual(max, max, 0), "C");
            Assert.IsTrue(Number.AlmostEqual(min, min, 0), "D");

            Assert.IsFalse(Number.AlmostEqual(0.0, 0.0 + double.Epsilon, 0), "E");
            Assert.IsTrue(Number.AlmostEqual(0.0, 0.0 + double.Epsilon, 1), "F");

            Assert.IsFalse(Number.AlmostEqual(max, max - (2 * Number.EpsilonOf(max)), 0), "G");
            Assert.IsFalse(Number.AlmostEqual(max, max - (2 * Number.EpsilonOf(max)), 1), "H");
            Assert.IsTrue(Number.AlmostEqual(max, max - (2 * Number.EpsilonOf(max)), 2), "I");

            Assert.IsTrue(Convert.ToDouble("3.170404", format) == 3.170404, "J");
            Assert.IsFalse(Convert.ToDouble("4.170404", format) == 4.170404, "K");

            Assert.IsTrue(Number.AlmostEqual(Convert.ToDouble("3.170404", format), 3.170404, 0), "L");
            Assert.IsFalse(Number.AlmostEqual(Convert.ToDouble("4.170404", format), 4.170404, 0), "M");
            Assert.IsTrue(Number.AlmostEqual(Convert.ToDouble("4.170404", format), 4.170404, 1), "N");

            Assert.IsFalse(Number.AlmostEqual(double.NaN, double.NaN, 25), "O");
            Assert.IsFalse(Number.AlmostEqual(double.PositiveInfinity, double.NegativeInfinity, 25), "P");
            Assert.IsTrue(Number.AlmostEqual(double.PositiveInfinity, double.PositiveInfinity, 25), "Q");
        }

        [Test]
        public void TestCoerceZero()
        {
            Assert.IsTrue(0d == Number.CoerceZero(0d), "A1");
            Assert.IsTrue(0d == Number.CoerceZero(Number.Increment(0d)), "A2");
            Assert.IsTrue(0d == Number.CoerceZero(Number.Decrement(0d)), "A3");

            Assert.IsTrue(1d == Number.CoerceZero(1d), "B1");
            Assert.IsTrue(-1d == Number.CoerceZero(-1d), "B2");
            Assert.IsTrue(0.5d == Number.CoerceZero(0.5d), "B3");
            Assert.IsTrue(-0.5d == Number.CoerceZero(-0.5d), "B4");

            Assert.IsTrue(double.PositiveInfinity == Number.CoerceZero(double.PositiveInfinity), "C1");
            Assert.IsTrue(double.NegativeInfinity == Number.CoerceZero(double.NegativeInfinity), "C2");
            Assert.IsTrue(double.MaxValue == Number.CoerceZero(double.MaxValue), "C3");
            Assert.IsTrue(double.MinValue == Number.CoerceZero(double.MinValue), "C4");
            Assert.IsTrue(double.IsNaN(Number.CoerceZero(double.NaN)), "C5");

            Assert.IsTrue(0d == Number.CoerceZero(double.Epsilon), "D1");
            Assert.IsTrue(0d == Number.CoerceZero(double.Epsilon * 1e3), "D2");
            Assert.IsTrue(0d == Number.CoerceZero(double.Epsilon * 1e6), "D3");
            Assert.IsTrue(0d == Number.CoerceZero(double.Epsilon * 1e200), "D4");
            Assert.IsTrue(0d == Number.CoerceZero(double.Epsilon), "D5");
            Assert.IsTrue(0d == Number.CoerceZero(double.Epsilon * -1e200), "D6");

            Assert.IsTrue(0d == Number.CoerceZero(Number.PositiveEpsilonOf(1e-5)), "E1");
            Assert.IsTrue(0d == Number.CoerceZero(Number.PositiveEpsilonOf(1)), "E2");
            Assert.IsFalse(0d == Number.CoerceZero(Number.PositiveEpsilonOf(1e+1)), "E3");
            Assert.IsFalse(0d == Number.CoerceZero(Number.PositiveEpsilonOf(1e+5)), "E4");
            Assert.IsTrue(0d == Number.CoerceZero(Number.PositiveEpsilonOf(-1)), "E5");
            Assert.IsFalse(0d == Number.CoerceZero(Number.PositiveEpsilonOf(-1e+1)), "E6");

            Assert.IsTrue(0d == Number.CoerceZero(1e-15), "F1");
            Assert.IsFalse(0d == Number.CoerceZero(1e-14), "F2");
            Assert.IsTrue(0d == Number.CoerceZero(-1e-15), "F3");
            Assert.IsFalse(0d == Number.CoerceZero(-1e-14), "F4");

            Assert.IsTrue(0d == Number.CoerceZero(1e-6, 1e-5), "G1");
            Assert.IsFalse(0d == Number.CoerceZero(1e-4, 1e-5), "G2");
            Assert.IsFalse(0d == Number.CoerceZero(1e-5, 1e-5), "G3");
            Assert.IsTrue(0d == Number.CoerceZero(-1e-6, 1e-5), "G4");
            Assert.IsFalse(0d == Number.CoerceZero(-1e-4, 1e-5), "G5");
            Assert.IsFalse(0d == Number.CoerceZero(-1e-5, 1e-5), "G6");

            Assert.IsFalse(0d == Number.CoerceZero(1e+6, 1e+5), "H1");
            Assert.IsTrue(0d == Number.CoerceZero(1e+4, 1e+5), "H2");
            Assert.IsFalse(0d == Number.CoerceZero(1e+5, 1e+5), "H3");
            Assert.IsFalse(0d == Number.CoerceZero(-1e+6, 1e+5), "H4");
            Assert.IsTrue(0d == Number.CoerceZero(-1e+4, 1e+5), "H5");
            Assert.IsFalse(0d == Number.CoerceZero(-1e+5, 1e+5), "H6");
        }
    }
}
