using System;
using System.Linq;
using BlueSky.Common;
using FluentAssertions;
using NUnit.Framework;

namespace BlueSky.TestHelpers.Tests
{
    [TestFixture]
    public class RandomExtensionsFixture
    {
        public class Sample
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private Random _random;

        [SetUp]
        public void SetUp()
        {
            _random = new Random();
        }

        [Test]
        public void ExclusiveInts_RandomAndCountProvided_ReturnsListWithExclusiveInts()
        {
            // Arrange
            var count = _random.Next(20, 50);

            // Act
            var actual = _random.ExclusiveInts(count);

            // Assert
            actual.ForEach(x => actual.Count(y => y == x).Should().Be(1));
        }

        [Test]
        public void FilledListFor_ActionProvided_NoMinAndMaxProvided_ReturnsListBetweenDefaultMinAndMax()
        {
            // Arrange
            // Act
            var actual = _random.FilledListFor<Sample>(x =>
            {
                x.Id = _random.Next();
                x.Name = _random.Next().ToString();
            });

            // Assert
            actual.Count.Should().BeGreaterOrEqualTo(RandomExtensions.MinForList);
            actual.Count.Should().BeLessOrEqualTo(RandomExtensions.MaxForList);
            actual.ToList().ForEach(x =>
            {
                x.Id.Should().BeGreaterThan(0);
                x.Name.Should().NotBeNullOrEmpty();
            });
        }

        [Test]
        public void FilledListFor_ActionProvided_MinAndMaxProvided_ReturnsListBetweenMinAndMax()
        {
            // Arrange
            var next = _random.Next(50, 100);
            var min = RandomExtensions.MinForList + next;
            var max = RandomExtensions.MaxForList + next;

            // Act
            var actual = _random.FilledListFor<Sample>(x =>
            {
                x.Id = _random.Next();
                x.Name = _random.Next().ToString();
            }, min, max);

            // Assert
            actual.Count.Should().BeGreaterOrEqualTo(min);
            actual.Count.Should().BeLessOrEqualTo(max);
            actual.ToList().ForEach(x =>
            {
                x.Id.Should().BeGreaterThan(0);
                x.Name.Should().NotBeNullOrEmpty();
            });
        }

        [Test]
        public void ListFor_MinAndMaxProvided_ReturnsListBetweenMinAndMax()
        {
            // Arrange
            var next = _random.Next(50, 100);
            var min = RandomExtensions.MinForList + next;
            var max = RandomExtensions.MaxForList + next;

            // Act
            var actual = _random.ListFor<Sample>(min, max);

            // Assert
            actual.Count.Should().BeGreaterOrEqualTo(min);
            actual.Count.Should().BeLessOrEqualTo(max);
        }

        [Test]
        public void ListFor_NoMinAndMaxProvided_ReturnsListBetweenDefaultMinAndMax()
        {
            // Arrange
            // Act
            var actual = _random.ListFor<Sample>();

            // Assert
            actual.Count.Should().BeGreaterOrEqualTo(RandomExtensions.MinForList);
            actual.Count.Should().BeLessOrEqualTo(RandomExtensions.MaxForList);
        }
    }
}