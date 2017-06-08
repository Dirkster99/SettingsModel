namespace SettingsModelTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SettingsModel.Interfaces;
    using SettingsModel.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Media;

    [TestClass]
    public class UnitTestEngineAPI
    {
        /// <summary>
        /// Tests whether an option engine can be created or not.
        /// </summary>
        [TestMethod]
        public void TestCreateEngine()
        {
            var engine = Factory.CreateEngine();

            Assert.AreNotEqual(engine , null);
        }

        /// <summary>
        /// Creates an engine and tests removal of an option.
        /// </summary>
        [TestMethod]
        public void TestAddRemoveOptions()
        {
            var engine = CreateEngineWithOptions();

            Assert.AreNotEqual(engine, null);

            // Assume that the options group 'Options' exists and holds 9 option definitions
            Assert.AreEqual(engine.GetOptionGroup("Options").GetOptionDefinitions().Count(), 9);

            // Test removing option
            engine.RemoveOption("Options", "LanguageSelected");
            Assert.AreEqual(engine.GetOptionGroup("Options").GetOptionDefinitions().Count(), 8);

            // Assume that 2 option groups 'Options' and 'Appearance' are available here
            Assert.AreEqual(engine.GetOptionGroups().Count(), 2);

            // Test removing options group
            engine.RemoveOptionsGroup("Options");
            Assert.AreEqual(engine.GetOptionGroups().Count(), 1);
        }

        /// <summary>
        /// Gets an option and tests whether updating will result in a dirty flag.
        /// </summary>
        [TestMethod]
        public void TestGetAndUpdateOptionValues()
        {
            var engine = CreateEngineWithOptions();

            Assert.AreNotEqual(engine, null);

            // Assume that the options group 'Options' exists and holds a "DefaultSourceLanguage" option
            // with the value "en-EN"
            Assert.AreEqual(engine.GetOptionValue<string>("Options", "DefaultSourceLanguage"), "en-EN");

            object optionValue;
            Assert.AreEqual(engine.GetOptionValue("Options", "DefaultSourceLanguage", out optionValue), true);
            Assert.AreEqual(optionValue is string, true);
            Assert.AreEqual(optionValue as string, "en-EN");

            // Engine was just created so it should be dirty since we changed the schema ...
            Assert.AreEqual(engine.IsDirty, true);

            // User of library can override dirty flag at any time
            engine.SetUndirty();
            Assert.AreEqual(engine.IsDirty, false);

            // Check if changing a value will result in the expected value change ... 
            engine.SetOptionValue("Options", "DefaultSourceLanguage", "in-ES");
            Assert.AreEqual(engine.GetOptionValue<string>("Options", "DefaultSourceLanguage"), "in-ES");

            // A change of a value should result in a change of the dirty flag
            Assert.AreEqual(engine.IsDirty, true);
        }

        /// <summary>
        /// Tests loading and saving of options via XML persistance layer.
        /// </summary>
        [TestMethod]
        public void TestWriteAndReadXML()
        {
            var engine = CreateEngineWithOptions();

            Assert.AreNotEqual(engine, null);

            string xmlString = engine.WriteXML();
            Assert.AreEqual(string.IsNullOrEmpty(xmlString), false);
        }

        /// <summary>
        /// Test whether 2 option engines with equal data are equal.
        /// </summary>
        [TestMethod]
        public void TestSettingsEquality()
        {
            var engine = CreateEngineWithOptions();
            Assert.AreNotEqual(engine, null);

            var engine1 = CreateEngineWithOptions();
            Assert.AreNotEqual(engine, null);

            // Checking equality of 2 option models
            // Equality checks refer only to data that is peristed (eg IsDirty is ignored)
            // since XML in source and target are compared in casesensitive mode.
            Assert.AreEqual(engine.Equals(engine1), true);

            engine.RemoveOption("Options", "LanguageSelected");

            Assert.AreEqual(engine.Equals(engine1), false);
        }

        /// <summary>
        /// Test equality by loading and saving data form one engine to the other.
        /// </summary>
        [TestMethod]
        public void TestLoadSaveSettingsEquality()
        {
            var engine = CreateEngineWithOptions();
            Assert.AreNotEqual(engine, null);

            var engine1 = CreateEngineWithOptions();
            Assert.AreNotEqual(engine, null);

            // Check if changing a value will result in the expected value change ... 
            engine.SetOptionValue("Options", "DefaultSourceLanguage", "in-ES");
            Assert.AreEqual(engine.GetOptionValue<string>("Options", "DefaultSourceLanguage"), "in-ES");

            var xmlString = engine.WriteXML();

            using (TextReader reader = new StringReader(xmlString))
            {
                engine1.ReadXML(reader);
            }

            // We saved and loaded XML from one engine to the other, so they should be equal now
            Assert.AreEqual(engine.Equals(engine1), true);
        }

        /// <summary>
        /// Creates a seetings model engine with test settings and returns it.
        /// </summary>
        /// <returns></returns>
        private IEngine CreateEngineWithOptions()
        {
            var engine = Factory.CreateEngine();

            engine.AddOption("Options", "ReloadOpenFilesFromLastSession", typeof(bool), false, true);
            engine.AddOption("Options", "SourceFilePath", typeof(string), false, @"C:\temp\source\");
            engine.AddOption("Options", "LanguageSelected", typeof(string), false, "de-De");
            engine.AddOption("Options", "IsDirty", typeof(bool), false, true);
            engine.AddOption("Options", "DefaultSourceLanguage", typeof(string), false, "en-EN");
            engine.AddOption("Options", "DefaultTargetLanguage", typeof(string), false, "de-DE");
            engine.AddOption("Options", "DefaultIconSize", typeof(int), false, 16);
            engine.AddOption("Options", "DefaultFontSize", typeof(int), false, 24);
            engine.AddListOption<string>("Options", "Bookmarks", typeof(string), false, new List<string>() { "item1", "item2", "item3" });

            engine.AddOption("Appearance", "BackgroundColor", typeof(Color), false, Color.FromRgb(33, 88, 99));
            engine.AddOption("Appearance", "ForegroundColor", typeof(string), false, @"C:\temp\source\");
            engine.AddOption("Appearance", "Fontsize", typeof(string), false, 24);

            return engine;
        }
    }
}
