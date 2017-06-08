namespace SettingsModelDemoConsole
{
    using SettingsModel.Interfaces;
    using SettingsModel.Models;
    using System;
    using System.Collections.Generic;

    public class Program
    {
        static IEngine CreateEngine()
        {
            var engine = Factory.CreateEngine();

            engine.AddOption("Options", "ReloadOpenFilesFromLastSession", typeof(bool), false, true);
            engine.AddOption("Options", "SourceFilePath", typeof(string), false, @"C:\temp\source\");
            engine.AddOption("Options", "LanguageSelected", typeof(string), false, "de-De");
            engine.AddOption("Options", "IsDirty", typeof(bool), false, true);
            engine.AddOption("Options", "DefaultSourceLanguage", typeof(string), false, "en-EN");
            engine.AddOption("Options", "DefaultTargetLanguage", typeof(string), false, "de-DE");
            engine.AddListOption<string>("Options", "Bookmarks", typeof(string), false, new List<string>() { "item1", "item2", "item3" });

            engine.AddOption("Appearance", "ForegroundColor", typeof(string), false, @"C:\temp\source\");
            engine.AddOption("Appearance", "Fontsize", typeof(string), false, 24);

            // this should return false since option is already available
            var bSucc = engine.SetOptionValue("Options", "Bookmarks", "item2");

            // this should return true since option is not yet available in list
            bSucc = engine.SetOptionValue("Options", "Bookmarks", "item4");

            // Normally scenarios do not require a removal of options at run-time but these functions are provided
            // to make the API complete towards:
            // 1> Defining new options
            // 2> Requesting the current value of an option
            // 3> Updating an option value to a new value
            // 4> and removing/deleting an option and its value from the schema (not needed but provided anyway).
            //
            ////engine.RemoveOptionsGroup("Options");
            ////engine.RemoveOption("Options", "Bookmarks");

            return engine;
        }

        static void Main(string[] args)
        {
            string filenamepath = @"C:\TEMP\result.xml";

            Console.WriteLine("This demo program creates a small options engine and saves");
            Console.WriteLine("its values at: '{0}'", filenamepath);

            Console.WriteLine("Press 'e' to exit or any other key to continue...");

            if (Console.ReadKey().Key == ConsoleKey.E)
                return;

            Console.WriteLine("Creating options engine and writing options to: '{0}'", filenamepath);
            var engine = CreateEngine();
            engine.WriteXML(filenamepath);

            Console.WriteLine("Reading options from: '{0}'", filenamepath);
            var readEngine = CreateEngine();
            readEngine.ReadXML(filenamepath);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
