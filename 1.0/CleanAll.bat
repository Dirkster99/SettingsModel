@ECHO OFF
pushd "%~dp0"
ECHO.
ECHO.
ECHO.
ECHO This script deletes all temporary build files in their
ECHO corresponding BIN and OBJ Folder contained in the following projects
ECHO.
ECHO Settings
ECHO SettingsModel
ECHO Demos_Tests\ServiceLocator
ECHO Demos_Tests\SettingsModelDemoConsole
ECHO Demos_Tests\SettingsModelTests
ECHO Demos_Tests\SettingsModelWPFDemo
ECHO.
REM Ask the user if hes really sure to continue beyond this point XXXXXXXX
set /p choice=Are you sure to continue (Y/N)?
if not '%choice%'=='Y' Goto EndOfBatch
REM Script does not continue unless user types 'Y' in upper case letter
ECHO.
ECHO XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
ECHO.
ECHO XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
ECHO.
ECHO Removing vs settings folder with *.sou file
ECHO.
RMDIR /S /Q .vs

ECHO.
ECHO Deleting BIN and OBJ Folders in SerialTest2 project folder
ECHO.
RMDIR /S /Q Settings\bin
RMDIR /S /Q Settings\obj
ECHO.


ECHO.
ECHO Deleting BIN and OBJ Folders in SettingsEngine project folder
ECHO.
RMDIR /S /Q SettingsModel\bin
RMDIR /S /Q SettingsModel\obj
ECHO.

ECHO.
ECHO Deleting BIN and OBJ Folders in ServiceLocator project folder
ECHO.
RMDIR /S /Q Demos_Tests\ServiceLocator\bin
RMDIR /S /Q Demos_Tests\ServiceLocator\obj
ECHO.


ECHO.
ECHO Deleting BIN and OBJ Folders in SettingsModelDemoConsole project folder
ECHO.
RMDIR /S /Q Demos_Tests\SettingsModelDemoConsole\bin
RMDIR /S /Q Demos_Tests\SettingsModelDemoConsole\obj
ECHO.

ECHO.
ECHO Deleting BIN and OBJ Folders in SettingsModelTests project folder
ECHO.
RMDIR /S /Q Demos_Tests\SettingsModelTests\bin
RMDIR /S /Q Demos_Tests\SettingsModelTests\obj
ECHO.

ECHO.
ECHO Deleting BIN and OBJ Folders in SettingsModelWPFDemo project folder
ECHO.
RMDIR /S /Q Demos_Tests\SettingsModelWPFDemo\bin
RMDIR /S /Q Demos_Tests\SettingsModelWPFDemo\obj
ECHO.
