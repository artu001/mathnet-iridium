//-----------------------------------------------------------------------
// <copyright file="SetTest.cs" company="Math.NET Project">
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

using NUnit.Framework;

namespace Iridium.Test.InfrastructureTests
{
    using MathNet.Numerics;

    [TestFixture]
    public class SetTest
    {
        [Test]
        public void IntersectionTest()
        {
            Set<int> a = new Set<int>();
            for(int i = 0; i < 16; i += 2)
            {
                a.Add(i);
            }

            Set<int> b = new Set<int>();
            for(int i = 0; i < 16; i += 3)
            {
                b.Add(i);
            }

            Set<int> inter = a.Intersect(b);

            Assert.That(inter.Count, Is.EqualTo(3), "A01");
            Assert.That(inter[0], Is.EqualTo(0), "A02");
            Assert.That(inter[1], Is.EqualTo(6), "A03");
            Assert.That(inter[2], Is.EqualTo(12), "A04");
        }
    }
}
