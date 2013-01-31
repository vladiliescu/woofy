[Setup]
AppName=Woofy
AppVersion=@APPVERSION@
AppVerName=Woofy @APPVERSION@
VersionInfoVersion=@APPFULLVERSION@
AppPublisher=Vlad Iliescu
AppPublisherURL=http://vladiliescu.ro
AppMutex=C59EAB54-6C2C-41a0-B516-55452A5AB3D2
AppCopyright=(c) Vlad Iliescu
DefaultDirName={userappdata}\Woofy\bin
DefaultGroupName=Woofy
PrivilegesRequired=lowest

UsePreviousAppDir=no
Uninstallable=yes
CreateUninstallRegKey=yes
UninstallDisplayIcon={app}\Woofy.exe
DisableProgramGroupPage=yes
DisableDirPage=yes
AlwaysShowDirOnReadyPage=yes

Compression=lzma2
SolidCompression=yes
LicenseFile=license.txt
WizardImageFile=WizardImageFile.bmp
WizardSmallImageFile=Woofy.bmp
SetupIconFile=Woofy.ico
OutputDir=.
OutputBaseFilename=Woofy-@APPVERSION@

[Types]
Name: "standard"; Description: "Standard installation"
Name: "custom"; Description: "Custom installation"; Flags: iscustom

[Components]
Name: "program"; Description: "Woofy"; Types: standard custom; Flags: fixed
Name: "program\autostart"; Types: standard; Description: "Start Woofy on system startup"

[Files]
Source: "Woofy\*.*"; Excludes: "*.exe.config"; DestDir: "{app}"; Components: program
Source: "Woofy\definitions\*.def"; DestDir: "{app}\definitions"; Components: program

[Icons]
Name: "{group}\Woofy"; Filename: "{app}\Woofy.exe";
Name: "{group}\{cm:UninstallProgram,Woofy}"; Filename: "{uninstallexe}";

Name: "{userstartup}\Woofy"; Filename: "{app}\Woofy.exe"; Components: program\autostart

[Run]
Filename: {app}\Woofy.exe; Flags: postinstall nowait; Description: Run Woofy when I press Finish