﻿namespace Gu.Wpf.Gauges.Tests
{
    using System.Linq;
    using System.Windows;
    using Gu.Wpf.Gauges.Tests.Helpers;
    using NUnit.Framework;

    public class ArcInfoTests
    {
        [TestCase("0,0", 2, 0, "2, 0")]
        [TestCase("0,0", 2, 90, "0, 2")]
        [TestCase("0,0", 2, -90, "0, -2")]
        [TestCase("0,0", 2, 180, "-2, 0")]
        [TestCase("0,0", 2, -180, "-2, 0")]
        [TestCase("1,2", 2, 0, "3, 2")]
        [TestCase("1,2", 2, 90, "1, 4")]
        [TestCase("1,2", 2, -90, "1, 0")]
        [TestCase("1,2", 2, 180, "-1, 2")]
        [TestCase("1,2", 2, -180, "-1, 2")]
        public void GetPoint(string center, double radius, double angle, string expected)
        {
            var arc = new ArcInfo(center.AsPoint(), radius, 0, 0);
            var actual = arc.GetPoint(angle);
            Assert.AreEqual(expected, actual.ToString("F0"));
        }

        [TestCase("0,0", 2, 0, "2, 0")]
        [TestCase("0,0", 2, 90, "0, 2")]
        [TestCase("0,0", 2, -90, "0, -2")]
        [TestCase("0,0", 2, 180, "-2, 0")]
        [TestCase("0,0", 2, -180, "-2, 0")]
        [TestCase("1,2", 2, 0, "3, 2")]
        [TestCase("1,2", 2, 90, "1, 4")]
        [TestCase("1,2", 2, -90, "1, 0")]
        [TestCase("1,2", 2, 180, "-1, 2")]
        [TestCase("1,2", 2, -180, "-1, 2")]
        public void StartPoint(string center, double radius, double angle, string expected)
        {
            var arc = new ArcInfo(center.AsPoint(), radius, angle, 0);
            Assert.AreEqual(expected, arc.StartPoint.ToString("F0"));
        }

        [TestCase("0,0", 2, 0, "2, 0")]
        [TestCase("0,0", 2, 90, "0, 2")]
        [TestCase("0,0", 2, -90, "0, -2")]
        [TestCase("0,0", 2, 180, "-2, 0")]
        [TestCase("0,0", 2, -180, "-2, 0")]
        [TestCase("1,2", 2, 0, "3, 2")]
        [TestCase("1,2", 2, 90, "1, 4")]
        [TestCase("1,2", 2, -90, "1, 0")]
        [TestCase("1,2", 2, 180, "-1, 2")]
        [TestCase("1,2", 2, -180, "-1, 2")]
        public void EndPoint(string center, double radius, double angle, string expected)
        {
            var arc = new ArcInfo(center.AsPoint(), radius, 0, angle);
            Assert.AreEqual(expected, arc.EndPoint.ToString("F0"));
        }

        [TestCase(-180, 0, System.Windows.Media.SweepDirection.Counterclockwise)]
        [TestCase(-180, 0, System.Windows.Media.SweepDirection.Counterclockwise)]
        public void SweepDirection(double startAngle, double endAngle, string expected)
        {
            Assert.Inconclusive();
            var arc = new ArcInfo(default(Point), 1, startAngle, endAngle);
            Assert.AreEqual(expected, arc.SweepDirection);
        }

        [TestCase("0,0", 2, 0, 90, "2, 0 0, 2")]
        [TestCase("0,0", 2, 90, 0, "0, 2 2, 0")]
        [TestCase("0,0", 2, 0, 180, "2, 0 0, 2 -2, 0")]
        [TestCase("0,0", 2, 180, 0, "-2, 0 0, 2 2, 0")]
        public void QuadrantPoints(string center, double radius, double startAngle, double endAngle, string expected)
        {
            var arc = new ArcInfo(center.AsPoint(), radius, startAngle, endAngle);
            Assert.AreEqual(expected, string.Join(" ", arc.QuadrantPoints.Select(p => p.ToString("F0"))));
        }

        [TestCase("0,0", 2, 1, 0, "3, 0")]
        [TestCase("0,0", 2, -1, 0, "1, 0")]
        [TestCase("0,0", 2, 1, 90, "0, 3")]
        [TestCase("0,0", 2, -1, -90, "0, -1")]
        [TestCase("0,0", 2, 1, 180, "-3, 0")]
        [TestCase("0,0", 2, -1, -180, "-1, 0")]
        [TestCase("1,2", 2, 1, 0, "4, 2")]
        [TestCase("1,2", 2, 1, 90, "1, 5")]
        [TestCase("1,2", 2, -1, -90, "1, 1")]
        [TestCase("1,2", 2, 1, 180, "-2, 2")]
        [TestCase("1,2", 2, -1, -180, "0, 2")]
        public void GetPoint(string center, double radius, double offset, double angle, string expected)
        {
            var arc = new ArcInfo(center.AsPoint(), radius, 0, 0);
            var actual = arc.GetPoint(angle, offset);
            Assert.AreEqual(expected, actual.ToString("F0"));
        }

        [TestCase("100, 100", -180, 180, "50, 50", 50)]
        [TestCase("100, 100", -180, 0, "50, 75", 50)]
        public void Fill(string ss, double start, double end, string expectedCentre, double expectedRadius)
        {
            var availableSize = ss.AsSize();
            var arc = ArcInfo.Fit(availableSize, start, end, isDirectionReversed: false);
            Assert.AreEqual(expectedCentre, arc.Center.ToString("F0"));
            Assert.AreEqual(expectedRadius, arc.Radius, 1e-6);
        }
    }
}
