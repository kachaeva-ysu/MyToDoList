﻿using System;
using Xunit;
using Xunit.Extensions;

namespace Set.Tests
{
    public class SetTests
    {
        [Fact]
        public void CreateEmptySetAndAddElementsSuccessful()
        {
            var set = new Set<int>();
            var expectedSet = new Set<int>(new[] { 1, 2 });
            set.Add(1);
            set.Add(2);
            set.Add(2);
            Assert.Equal(expectedSet, set);
        }

        [Fact]
        public void InitializeByNullAndGetExceptionSuccessful()
        {
            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                int[] intArray = null;
                var set = new Set<int>(intArray);
            });
        }

        [Fact]
        public void InitializeByNegativeCapacityAndGetExceptionSuccessful()
        {
            Assert.Throws(typeof (ArgumentOutOfRangeException), () =>
            {
                var set = new Set<int>(-5);
            });
        }

        [Fact]
        public void InitializeByCollectionAndEnumerateSuccessful()
        {
            var set = new Set<int>(new[] { 1, 3, 5, 5, 7, 7, 3, 1 });
            var expectedSet = new Set<int>(new[] { 1, 3, 5, 7 });
            Assert.Equal(expectedSet, set);
        }

        [Fact]
        public void ClearSetSuccessful()
        {
            var set = new Set<int>(new[] { 0, 2, 4 });
            set.Clear();
            Assert.Equal(0, set.Count);
        }

        [Theory]
        [InlineData(5, true)]
        [InlineData(4, false)]
        public void PassElementContains(int element, bool expected)
        {
            var set = new Set<int>(new[] { 1, 3, 5, 7, 9 });
            bool actual = set.Contains(element);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(5, new[] { 1, 3, 7, 9 })]
        [InlineData(1, new[] { 3, 5, 7, 9 })]
        [InlineData(9, new[] { 1, 3, 5, 7 })]
        public void RemoveElementSuccessful(int element, int[] expected)
        {
            var set = new Set<int>(new[] { 1, 3, 5, 7, 9 });
            var expectedSet = new Set<int>(expected);
            set.Remove(element);
            Assert.Equal(expectedSet, set);
        }

        [Theory]
        [InlineData(new int[0], new[] { 1, 3 }, new[] { 1, 3 })]
        [InlineData(new[] { 1, 3, 5, 7 }, new[] { 5, 9, 3, 11, 7, 1 }, new[] { 1, 3, 5, 7, 9, 11 })]
        [InlineData(new[] { 1, 3 }, new[] { 5, 9 }, new[] { 1, 3, 5, 9 })]
        public void UninonWithOtherSetSuccessful(int[] firstArray, int[] secondArray, int[] expected)
        {
            var firstSet = new Set<int>(firstArray);
            var secondSet = new Set<int>(secondArray);
            var expectedSet = new Set<int>(expected);
            firstSet.UnionWith(secondSet);
            Assert.Equal(expectedSet, firstSet);
        }

        [Theory]
        [InlineData(new int[0], new[] { 1, 3 }, new int[0])]
        [InlineData(new[] { 1, 3, 5, 7 }, new int[0], new int[0])]
        [InlineData(new[] { 1, 3, 5, 7, 9 }, new[] { 11, 7, 13, 3, 9, 15 }, new[] { 3, 7, 9 })]
        public void IntersectWithOtherSetSuccessful(int[] firstArray, int[] secondArray, int[] expected)
        {
            var firstSet = new Set<int>(firstArray);
            var secondSet = new Set<int>(secondArray);
            var expectedSet = new Set<int>(expected);
            firstSet.IntersectWith(secondSet);
            Assert.Equal(expectedSet, firstSet);
        }


        [Theory]
        [InlineData(new int[0], new[] { 1, 3 }, new int[0])]
        [InlineData(new[] { 1, 3, 5, 7 }, new int[0], new[] { 1, 3, 5, 7 })]
        [InlineData(new[] { 1, 3, 5, 7, 9 }, new[] { 11, 7, 13, 3, 9, 15 }, new[] { 1, 5 })]
        public void ExceptWithOtherSetSuccessful(int[] firstArray, int[] secondArray, int[] expected)
        {
            var firstSet = new Set<int>(firstArray);
            var secondSet = new Set<int>(secondArray);
            var expectedSet = new Set<int>(expected);
            firstSet.ExceptWith(secondSet);
            Assert.Equal(expectedSet, firstSet);
        }

        [Fact]
        public void ExceptWithSelfSuccessful()
        {
            var set = new Set<int>(new[] { 1, 3, 5, 7, 9 });
            var expectedSet = new Set<int>();
            set.ExceptWith(set);
            Assert.Equal(expectedSet, set);
        }

        [Theory]
        [InlineData(new int[0], new[] { 1, 3 }, new[] { 1, 3 })]
        [InlineData(new[] { 1, 3, 5, 7 }, new int[0], new[] { 1, 3, 5, 7 })]
        [InlineData(new[] { 1, 3, 5, 7, 9 }, new[] { 11, 7, 13, 3, 9, 15 }, new[] { 1, 5, 11, 13, 15 })]
        public void SymmetricExceptWithOtherSetSuccessful(int[] firstArray, int[] secondArray, int[] expected)
        {
            var firstSet = new Set<int>(firstArray);
            var secondSet = new Set<int>(secondArray);
            var expectedSet = new Set<int>(expected);
            firstSet.SymmetricExceptWith(secondSet);
            Assert.Equal(expectedSet, firstSet);
        }

        [Fact]
        public void SymmetricExceptWithSelfSuccessful()
        {
            var set = new Set<int>(new[] { 1, 3, 5, 7, 9 });
            var expectedSet = new Set<int>();
            set.ExceptWith(set);
            Assert.Equal(expectedSet, set);
        }

        [Theory]
        [InlineData(new int[0], new[] { 1, 3 }, true)]
        [InlineData(new[] { 1, 3, 5, 7 }, new [] {1, 7}, false)]
        [InlineData(new[] { 1, 3, 5, 7, 9 }, new[] { 11, 7, 13, 3, 9, 15, 1, 5 }, true)]
        public void IsSubsetOfOtherSetSuccessful(int[] firstArray, int[] secondArray, bool expected)
        {
            var firstSet = new Set<int>(firstArray);
            var secondSet = new Set<int>(secondArray);
            bool actual = firstSet.IsSubsetOf(secondSet);
            Assert.Equal(expected, actual);
        }
    }
}
