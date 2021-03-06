﻿using IndependentUtils.Configuration.IntegrationTests.AutogeneratedSection;
using IndependentUtils.Testing.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace IndependentUtils.Configuration.IntegrationTests
{
    [TestClass]
    public class ConfigurationElementCollectionTests
    {
        /// <summary>
        /// Tests that the list can be generated from the IExampleSection
        /// interface without needing to create class manually.
        /// </summary>
        [TestMethod]
        [IntegrationTest]
        public void TestReadConfigurationCollectionFromSection()
        {
            // Arrange
            var defaultSection = new DefaultConfigurationManager();

            // Act
            var section = defaultSection.GetSection<IExampleSection>();
            var importantDates = section.ImportantDates;
            var mandatoryDates = section.MandatoryDates;
            var name = section.Name;
            var timeFormats = section.TimeFormats;

            // Assert
            Assert.AreEqual("SampleName", name);
            Assert.AreEqual("dd/mm/yyyy", timeFormats.Format);
            Assert.AreEqual(false, timeFormats.IsLocal);
            CollectionAssert.AreEqual(new[]
            {
                new DateTime(2017, 8, 12),
                new DateTime(2018, 3, 25)
            }, 
            importantDates.Select(t => new DateTime(t.Year, t.Month, t.Day)).ToList());
            CollectionAssert.AreEqual(new[]
            {
                new DateTime(2017, 7, 1),
                new DateTime(2017, 7, 2),
                new DateTime(2019, 3, 8)
            }, 
            mandatoryDates.Select(t => new DateTime(t.Year, t.Month, t.Day)).ToList());
        }
    }
}
