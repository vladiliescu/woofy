SetCompressor bzip2

# Defines
!define NAME "ComicPack"
!define NAMEWOOFY "Woofy"
!define REGKEY "SOFTWARE\${NAMEWOOFY}"
!define VERSION 0.4.1.1
!define COMPANY "Vlad Iliescu"
!define URL "http://woofy.sourceforge.net"


# MUI defines
!define MUI_ABORTWARNING
#!define MUI_LICENSEPAGE_CHECKBOX
!define MUI_STARTMENUPAGE_REGISTRY_ROOT HKLM
!define MUI_STARTMENUPAGE_REGISTRY_KEY ${REGKEY}
!define MUI_STARTMENUPAGE_DEFAULTFOLDER ${NAMEWOOFY}
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\orange-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\orange-uninstall.ico"
!define MUI_HEADERIMAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Header\orange-nsis.bmp"
!define MUI_HEADERIMAGE_UNBITMAP "${NSISDIR}\Contrib\Graphics\Header\orange-uninstall-nsis.bmp"
!define MUI_WELCOMEFINISHPAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Wizard\orange-nsis.bmp"
!define MUI_UNFINISHPAGE_NOAUTOCLOSE

# Included files
!include Sections.nsh
!include MUI.nsh


# Installer pages
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

# Installer languages
!insertmacro MUI_LANGUAGE English

# Installer attributes
Name "${NAME} ${VERSION}"
BrandingText "${NAME} ${VERSION}"

OutFile "Drop\${NAME}-${VERSION}-setup.exe"
InstallDir $PROGRAMFILES\${NAMEWOOFY}
InstallDirRegKey HKLM "${REGKEY}" "Path"
CRCCheck on
XPStyle on
ShowInstDetails show

ShowUninstDetails show

# Installer sections
Section "-Default"
    SetOutPath $INSTDIR\ComicDefinitions
    SetOverwrite on
    File /r Files\ComicDefinitions\*
SectionEnd
