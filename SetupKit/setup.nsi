SetCompressor bzip2

# Defines
!define NAME "Woofy"
!define REGKEY "SOFTWARE\${NAME}"
!define VERSION 0.4b
!define COMPANY "Vlad Iliescu"
!define URL "http://woofy.sourceforge.net"


# MUI defines
!define MUI_ABORTWARNING
#!define MUI_LICENSEPAGE_CHECKBOX
!define MUI_STARTMENUPAGE_REGISTRY_ROOT HKLM
!define MUI_STARTMENUPAGE_REGISTRY_KEY ${REGKEY}
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME StartMenuGroup
!define MUI_STARTMENUPAGE_DEFAULTFOLDER ${NAME}
!define MUI_FINISHPAGE_RUN $INSTDIR\Woofy.exe
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\orange-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\orange-uninstall.ico"
!define MUI_HEADERIMAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Header\orange-nsis.bmp"
!define MUI_HEADERIMAGE_UNBITMAP "${NSISDIR}\Contrib\Graphics\Header\orange-uninstall-nsis.bmp"
!define MUI_WELCOMEFINISHPAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Wizard\orange-nsis.bmp"
!define MUI_UNFINISHPAGE_NOAUTOCLOSE

# Included files
!include Sections.nsh
!include MUI.nsh

# Variables
Var StartMenuGroup

# Installer pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE Files\license.txt
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_STARTMENU Application $StartMenuGroup
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

# Installer languages
!insertmacro MUI_LANGUAGE English

# Installer attributes
Name "${NAME} ${VERSION}"
BrandingText "${NAME} ${VERSION}"

OutFile "${NAME}-${VERSION}-setup.exe"
InstallDir $PROGRAMFILES\${NAME}
InstallDirRegKey HKLM "${REGKEY}" "Path"
CRCCheck on
XPStyle on
ShowInstDetails show
#VIProductVersion 0.2.0.0
#VIAddVersionKey ProductName "${NAME}"
#VIAddVersionKey ProductVersion "${VERSION}"
#VIAddVersionKey CompanyName "${COMPANY}"
#VIAddVersionKey FileVersion ""
#VIAddVersionKey FileDescription ""
#VIAddVersionKey LegalCopyright ""

ShowUninstDetails show

# Installer sections
Section "-Default"
    DeleteRegKey HKLM "${REGKEY}\Components"
SectionEnd

Section Woofy SEC0000
    SectionIn RO

    SetOutPath $INSTDIR
    
    SetOverwrite off
    File Files\data.s3db
    
    SetOverwrite on
    File Files\license.txt
    File Files\System.Data.SQLite.DLL
    File Files\log4net.dll
    File Files\Woofy.exe
    File Files\Woofy.exe.config

    SetOutPath $INSTDIR\ComicInfos
    SetOverwrite on
    File /r Files\ComicInfos\*
    
    SetOutPath $SMPROGRAMS\$StartMenuGroup
    CreateShortcut "$SMPROGRAMS\$StartMenuGroup\$(^Name).lnk" $INSTDIR\Woofy.exe
    
    ExecWait '"$WINDIR\Microsoft.NET\Framework\v2.0.50727\ngen.exe" install "$INSTDIR\System.Data.SQLite.DLL"'
    ExecWait '"$WINDIR\Microsoft.NET\Framework\v2.0.50727\ngen.exe" install "$INSTDIR\log4net.dll"'
    ExecWait '"$WINDIR\Microsoft.NET\Framework\v2.0.50727\ngen.exe" install "$INSTDIR\Woofy.exe"'
    
    WriteRegStr HKLM "${REGKEY}\Components" "Woofy" 1
SectionEnd

Section /o "Debug Symbols" SEC0001
    SetOutPath $INSTDIR
    SetOverwrite on
    File Files\Woofy.pdb
    WriteRegStr HKLM "${REGKEY}\Components" "Debug Symbols" 1
SectionEnd

Section -post SEC0002
    WriteRegStr HKLM "${REGKEY}" Path $INSTDIR
    SetOutPath $INSTDIR
    WriteUninstaller $INSTDIR\uninstall.exe
    !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    SetOutPath $SMPROGRAMS\$StartMenuGroup
    CreateShortcut "$SMPROGRAMS\$StartMenuGroup\Uninstall.lnk" $INSTDIR\uninstall.exe
    !insertmacro MUI_STARTMENU_WRITE_END

    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayName "$(^Name)"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayVersion "${VERSION}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" Publisher "${COMPANY}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayIcon $INSTDIR\Woofy.exe
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" UninstallString $INSTDIR\uninstall.exe
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoModify 1
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoRepair 1
SectionEnd

; Section descriptions
!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC0000} "Installs Woofy"
  !insertmacro MUI_DESCRIPTION_TEXT ${SEC0001} "Installs the debug symbols, useful for analyzing crashes"
!insertmacro MUI_FUNCTION_DESCRIPTION_END

# Macro for selecting uninstaller sections
!macro SELECT_UNSECTION SECTION_NAME UNSECTION_ID
    Push $R0
    ReadRegStr $R0 HKLM "${REGKEY}\Components" "${SECTION_NAME}"
    StrCmp $R0 1 0 next${UNSECTION_ID}
    !insertmacro SelectSection "${UNSECTION_ID}"
    GoTo done${UNSECTION_ID}
next${UNSECTION_ID}:
    !insertmacro UnselectSection "${UNSECTION_ID}"
done${UNSECTION_ID}:
    Pop $R0
!macroend

# Uninstaller sections
Section /o "un.Debug Symbols" UNSEC0001
    Delete /REBOOTOK $INSTDIR\Woofy.pdb
    DeleteRegValue HKLM "${REGKEY}\Components" "Debug Symbols"
SectionEnd

Section /o un.Woofy UNSEC0000
    ExecWait '"$WINDIR\Microsoft.NET\Framework\v2.0.50727\ngen.exe" uninstall "$INSTDIR\Woofy.exe"'
    ExecWait '"$WINDIR\Microsoft.NET\Framework\v2.0.50727\ngen.exe" uninstall "$INSTDIR\log4net.dll"'
    ExecWait '"$WINDIR\Microsoft.NET\Framework\v2.0.50727\ngen.exe" uninstall "$INSTDIR\System.Data.SQLite.DLL"'

    RMDir /r /REBOOTOK $INSTDIR
    RMDir /r /REBOOTOK $SMPROGRAMS\$StartMenuGroup
    DeleteRegValue HKLM "${REGKEY}\Components" Woofy
SectionEnd

Section un.post UNSEC0002
    DeleteRegKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)"    
    DeleteRegValue HKLM "${REGKEY}" StartMenuGroup
    DeleteRegValue HKLM "${REGKEY}" Path
    DeleteRegKey /IfEmpty HKLM "${REGKEY}\Components"
    DeleteRegKey /IfEmpty HKLM "${REGKEY}"

    Push $R0
    StrCpy $R0 $StartMenuGroup 1
    StrCmp $R0 ">" no_smgroup
no_smgroup:
    Pop $R0
SectionEnd

# Installer functions
Function .onInit
    InitPluginsDir

#If the user had previously selected to install the Debug Symbols, then check it automatically
    ReadRegStr $0 HKLM "${REGKEY}\Components" "Debug Symbols"
    SectionSetFlags 2 $0
FunctionEnd


# Uninstaller functions
Function un.onInit
    ReadRegStr $INSTDIR HKLM "${REGKEY}" Path
    !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuGroup
    !insertmacro SELECT_UNSECTION Woofy ${UNSEC0000}
    !insertmacro SELECT_UNSECTION "Debug Symbols" ${UNSEC0001}
FunctionEnd


