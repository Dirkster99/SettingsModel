# Project Description
An API library that supports storage and retrieval of program settings through a generic interface.

# Introduction

Test this framework inside my application:
* [Locult](locult.codeplex.com)

I used to store settings with the build in settings class in .Net but switched to custom serializable classes to make things easier to debug since the build in .Net class has some limitations of its own:.
[http://www.codeproject.com/Articles/475498/Easier-NET-settings?msg=5087731#xx5087731xx.](http://www.codeproject.com/Articles/475498/Easier-NET-settings?msg=5087731#xx5087731xx.)

My software projects evolved to be more and more complex and it kept getting harder to manage a project that stores and retrieves program settings, because the settings project had to reference all custom serializable classes to store and retrieve settings. Additional references were required for the application to the settings project and application to the project containing the custom serializable class...

The result was a mess that did not look good at all. I also wished to be able to configure the settings section in a seamless manner. Meaning, that each application building on a particular module should be able to define an exact set of settings to be stored rather than having to store all settings all the time even when they were not changed...

So, I came up with an idea to build a settings model that supports storage and retrieval of settings through using .Net base classes (like string, int, bool, Color ...) and store and retrieve is values via XML.

I discovered by accident that I can use the **DataSet** and **DataTable** class to read and write arbitrary .Net values and types in XML.

The result of this development is a project that can be used to store and retrieve program settings. The project can be used pretty much in any other .Net project, be it **Winforms**, **DOS**, or **WPF**...

This repository contains 2 sample projects that exemplify the usage of the **SettingsModel** library. The WPF application [SettingsModelWPFDemo](SettingsModelWPFDemo) is a Modern UI reference application that implements:

* 2 themes (Dark and Light),
* supports more than 1 language, and
* support different sizes of fonts.

This includes not only changing these items but also storing and re-loading them upon re-start of the application. This reference may be used for building these function including view, viewmodel, and models required to implement the complete functionality.

# Storage of Encrypted Settings

The **SettingsModel** library looks for settings that are defined with the **SecureString** datatype and encrypts/decrypts these settings upon storage and retrieval. This setting can be used to store and retrieve sensitive information such as login and password related data. I am not completely sure how secure this implementation really is but it should be a lot more secure than using plain text strings.