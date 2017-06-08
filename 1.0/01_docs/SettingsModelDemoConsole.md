# Console Demo Application SettingsModelDemoConsole

The console demo application is rather simple. It informs the user about the XML file that is going to be written and loaded back into the application:

![](SettingsModelDemoConsole_Step1.png)

...and the application performs these steps if the user did not hit the 'e' key on the keyboard:
![](SettingsModelDemoConsole_Step2.png)

This demo can be used to explore the basic concept with the debugger. It creates an **result.xml** file that looks like this;

{{
<?xml version="1.0" standalone="yes"?>
<NewDataSet>
  <Options>
    <ReloadOpenFilesFromLastSession>true</ReloadOpenFilesFromLastSession>
    <SourceFilePath>C:\temp\source\</SourceFilePath>
    <LanguageSelected>de-De</LanguageSelected>
    <IsDirty>true</IsDirty>
    <DefaultSourceLanguage>en-EN</DefaultSourceLanguage>
    <DefaultTargetLanguage>de-DE</DefaultTargetLanguage>
    <Bookmarks>item1</Bookmarks>
  </Options>
  <_x0024__x007B_Options_x007D__x0024__x007B_Bookmarks_x007D_>
    <Bookmarks>item1</Bookmarks>
  </_x0024__x007B_Options_x007D__x0024__x007B_Bookmarks_x007D_>
  <_x0024__x007B_Options_x007D__x0024__x007B_Bookmarks_x007D_>
    <Bookmarks>item2</Bookmarks>
  </_x0024__x007B_Options_x007D__x0024__x007B_Bookmarks_x007D_>
  <_x0024__x007B_Options_x007D__x0024__x007B_Bookmarks_x007D_>
    <Bookmarks>item3</Bookmarks>
  </_x0024__x007B_Options_x007D__x0024__x007B_Bookmarks_x007D_>
  <_x0024__x007B_Options_x007D__x0024__x007B_Bookmarks_x007D_>
    <Bookmarks>item4</Bookmarks>
  </_x0024__x007B_Options_x007D__x0024__x007B_Bookmarks_x007D_>
  <Appearance>
    <ForegroundColor>C:\temp\source\</ForegroundColor>
    <Fontsize>24</Fontsize>
  </Appearance>
</NewDataSet>
}}

The code below creates a **SettingsModel** with 2 option groups called **Options** and **Appearance**:
{{
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
}}

The code below stores a **SettingsModel** in an XML file:
{{
string filenamepath = @"C:\TEMP\result.xml";
engine.WriteXML(filenamepath);
}}


The code below reads a **SettingsModel** from an XML file:
{{
string filenamepath = @"C:\TEMP\result.xml";
readEngine.ReadXML(filenamepath);
}}

See also console demo application for more details:
[https://settingsmodel.codeplex.com/SourceControl/latest#1.0/Demos_Tests/SettingsModelDemoConsole/Program.cs](https://settingsmodel.codeplex.com/SourceControl/latest#1.0/Demos_Tests/SettingsModelDemoConsole/Program.cs)