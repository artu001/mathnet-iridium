//-----------------------------------------------------------------------
// <copyright file="SortingTest.cs" company="Math.NET Project">
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
using MathNet.Numerics.RandomSources;

namespace Iridium.Test
{
    [TestFixture]
    public class SortingTest
    {
        [Test]
        public void TestRandomTupleArraySorting()
        {
            const int Len = 0x1 << 10;
            SystemRandomSource random = new SystemRandomSource();

            int[] keys = new int[Len];
            int[] items = new int[Len];
            int[] keysCopy = new int[Len];

            for(int i = 0; i < keys.Length; i++)
            {
                keys[i] = random.Next();
                keysCopy[i] = keys[i];
                items[i] = -keys[i];
            }

            Sorting.Sort(keys, items);

            for(int i = 1; i < keys.Length; i++)
            {
                Assert.IsTrue(keys[i] >= keys[i - 1], "Sort Order - " + i.ToString());
                Assert.AreEqual(-keys[i], items[i], "Items Permutation - " + i.ToString());
            }

            for(int i = 0; i < keysCopy.Length; i++)
            {
                Assert.IsTrue(Array.IndexOf(keys, keysCopy[i]) >= 0, "All keys still there - " + i.ToString());
            }
        }

        [Test]
        public void TestRandomTupleListSorting()
        {
            const int Len = 0x1 << 10;
            SystemRandomSource random = new SystemRandomSource();

            List<int> keys = new List<int>(Len);
            List<int> items = new List<int>(Len);
            int[] keysCopy = new int[Len];

            for(int i = 0; i < Len; i++)
            {
                int value = random.Next();
                keys.Add(value);
                keysCopy[i] = value;
                items.Add(-value);
            }

            Sorting.Sort(keys, items);

            for(int i = 1; i < Len; i++)
            {
                Assert.IsTrue(keys[i] >= keys[i - 1], "Sort Order - " + i.ToString());
                Assert.AreEqual(-keys[i], items[i], "Items Permutation - " + i.ToString());
            }

            for(int i = 0; i < keysCopy.Length; i++)
            {
                Assert.IsTrue(keys.IndexOf(keysCopy[i]) >= 0, "All keys still there - " + i.ToString());
            }
        }

        [Test]
        public void TestRandomTripleArraySorting()
        {
            const int Len = 0x1 << 10;
            SystemRandomSource random = new SystemRandomSource();

            int[] keys = new int[Len];
            int[] items1 = new int[Len];
            int[] items2 = new int[Len];
            int[] keysCopy = new int[Len];

            for(int i = 0; i < keys.Length; i++)
            {
                keys[i] = random.Next();
                keysCopy[i] = keys[i];
                items1[i] = -keys[i];
                items2[i] = keys[i] >> 2;
            }

            Sorting.Sort(keys, items1, items2);

            for(int i = 1; i < keys.Length; i++)
            {
                Assert.IsTrue(keys[i] >= keys[i - 1], "Sort Order - " + i.ToString());
                Assert.AreEqual(-keys[i], items1[i], "Items1 Permutation - " + i.ToString());
                Assert.AreEqual(keys[i] >> 2, items2[i], "Items2 Permutation - " + i.ToString());
            }

            for(int i = 0; i < keysCopy.Length; i++)
            {
                Assert.IsTrue(Array.IndexOf(keys, keysCopy[i]) >= 0, "All keys still there - " + i.ToString());
            }
        }

        [Test]
        public void TestAppliedSetSorting()
        {
            const int Len = 0x1 << 10;
            SystemRandomSource random = new SystemRandomSource();

            Set<int> set = new Set<int>();

            for(int i = 0; i < Len; i++)
            {
                set.Add(random.Next());
            }

            // default sorting (Ascending)
            set.Sort();

            // just check that the order is as expected, not that the items are correct
            for(int i = 1; i < set.Count; i++)
            {
                Assert.IsTrue(set[i] >= set[i - 1], "Sort Order - " + i.ToString());
            }
        }
    }
}
