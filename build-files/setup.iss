[Setup]
AppName=Woofy
AppVersion=@APPVERSION@
AppVerName=Woofy @APPVERSION@
AppMutex=C59EAB54-6C2C-41a0-B516-55452A5AB3D2
DefaultDirName={pf}\Woofy
DefaultGroupName=Woofy
UninstallDisplayIcon={app}\Woofy.exe
Compression=lzma2
SolidCompression=yes
LicenseFile=license.txt
WizardImageFile=WizardImageFile.bmp
OutputDir=.
OutputBaseFilename=Woofy-@APPVERSION@

[Types]
Name: "standard"; Description: "Standard installation"
Name: "portable"; Description: "Portable installation"
Name: "custom"; Description: "Custom installation"; Flags: iscustom

[Components]
Name: "program"; Description: "Woofy"; Types: standard portable custom; Flags: fixed
Name: "program\autostart"; Types: standard; Description: "Start Woofy on system startup"
Name: "program\portable"; Types: portable; Description: "Portable mode"

[Files]
Source: "Woofy\*.*"; DestDir: "{app}"; Components: program
Source: "Woofy\definitions\*.def"; DestDir: "{app}\definitions"; Components: program

;Source: "Woofy.exe.config"; DestDir: "{app}"; Components: program\portable

[Icons]
Name: "{group}\Woofy"; Filename: "{app}\Woofy.exe"
Name: "{group}\{cm:UninstallProgram,Woofy}"; Filename: "{uninstallexe}"

Name: "{userstartup}\Woofy"; Filename: "{app}\Woofy.exe"; Components: program\autostart

[Run]
Filename: {app}\Woofy.exe; Flags: postinstall nowait; Description: Run Woofy when I press Finish