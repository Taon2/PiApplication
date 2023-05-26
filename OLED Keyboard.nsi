     ############################################################################################

!define APP_NAME "OLED Keyboard"
!define COMP_NAME "OLED Keyboard Senior Design Group"
!define WEB_SITE "https://www.msoe.edu/"
!define VERSION "1.0.0.0"
!define COPYRIGHT "None"
!define DESCRIPTION "Windows Companion Application for the OLED Keyboard"
!define INSTALLER_NAME "C:\Users\Zach\Downloads\nsis\Output\OLED Keyboard\OLED Keyboard Setup.exe"
!define MAIN_APP_EXE "OLEDWindowsApplication.exe"
!define INSTALL_TYPE "SetShellVarContext current"
!define REG_ROOT "HKCU"
!define REG_APP_PATH "Software\Microsoft\Windows\CurrentVersion\App Paths\${MAIN_APP_EXE}"
!define UNINSTALL_PATH "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP_NAME}"

!define REG_START_MENU "Start Menu Folder"

var SM_Folder

######################################################################

VIProductVersion  "${VERSION}"
VIAddVersionKey "ProductName"  "${APP_NAME}"
VIAddVersionKey "CompanyName"  "${COMP_NAME}"
VIAddVersionKey "LegalCopyright"  "${COPYRIGHT}"
VIAddVersionKey "FileDescription"  "${DESCRIPTION}"
VIAddVersionKey "FileVersion"  "${VERSION}"

######################################################################

SetCompressor ZLIB
Name "${APP_NAME}"
Caption "${APP_NAME}"
OutFile "${INSTALLER_NAME}"
BrandingText "${APP_NAME}"
XPStyle on
InstallDirRegKey "${REG_ROOT}" "${REG_APP_PATH}" ""
InstallDir "$PROGRAMFILES\OLED Keyboard"

######################################################################

!include "MUI.nsh"

!define MUI_ABORTWARNING
!define MUI_UNABORTWARNING

!insertmacro MUI_PAGE_WELCOME

!ifdef LICENSE_TXT
!insertmacro MUI_PAGE_LICENSE "${LICENSE_TXT}"
!endif

!insertmacro MUI_PAGE_DIRECTORY

!ifdef REG_START_MENU
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "OLED Keyboard"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${REG_ROOT}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${UNINSTALL_PATH}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${REG_START_MENU}"
!insertmacro MUI_PAGE_STARTMENU Application $SM_Folder
!endif

!insertmacro MUI_PAGE_INSTFILES

!define MUI_FINISHPAGE_RUN "$INSTDIR\${MAIN_APP_EXE}"
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM

!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_UNPAGE_FINISH

!insertmacro MUI_LANGUAGE "English"

######################################################################

Section -MainProgram
${INSTALL_TYPE}
SetOverwrite ifnewer
SetOutPath "$INSTDIR"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Aspose.Imaging.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Configuration.Abstractions.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Configuration.Binder.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Configuration.CommandLine.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Configuration.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Configuration.EnvironmentVariables.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Configuration.FileExtensions.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Configuration.Json.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Configuration.UserSecrets.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.DependencyInjection.Abstractions.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.DependencyInjection.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.FileProviders.Abstractions.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.FileProviders.Physical.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.FileSystemGlobbing.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Hosting.Abstractions.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Hosting.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Logging.Abstractions.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Logging.Configuration.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Logging.Console.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Logging.Debug.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Logging.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Logging.EventLog.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Logging.EventSource.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Options.ConfigurationExtensions.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Options.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Microsoft.Extensions.Primitives.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\OLEDWindowsApplication.deps.json"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\OLEDWindowsApplication.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\OLEDWindowsApplication.exe"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\OLEDWindowsApplication.pdb"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\OLEDWindowsApplication.runtimeconfig.json"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\System.CodeDom.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\System.IO.Ports.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\System.Management.dll"
SetOutPath "$INSTDIR\runtimes\win\lib\net6.0"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\runtimes\win\lib\net6.0\System.Diagnostics.EventLog.Messages.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\runtimes\win\lib\net6.0\System.IO.Ports.dll"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\runtimes\win\lib\net6.0\System.Management.dll"
SetOutPath "$INSTDIR\runtimes\unix\lib\net6.0"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\runtimes\unix\lib\net6.0\System.IO.Ports.dll"
SetOutPath "$INSTDIR\runtimes\osx-x64\native"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\runtimes\osx-x64\native\libSystem.IO.Ports.Native.dylib"
SetOutPath "$INSTDIR\runtimes\osx-arm64\native"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\runtimes\osx-arm64\native\libSystem.IO.Ports.Native.dylib"
SetOutPath "$INSTDIR\runtimes\linux-x64\native"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\runtimes\linux-x64\native\libSystem.IO.Ports.Native.so"
SetOutPath "$INSTDIR\runtimes\linux-arm64\native"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\runtimes\linux-arm64\native\libSystem.IO.Ports.Native.so"
SetOutPath "$INSTDIR\runtimes\linux-arm\native"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\runtimes\linux-arm\native\libSystem.IO.Ports.Native.so"
SetOutPath "$INSTDIR\Resources"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\+.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\-.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\0.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\1.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\2.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\3.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\4.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\5.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\6.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\7.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\8.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\9.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\=.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\asterisk.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\back.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\cal.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\del.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\ent.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\esc.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\numlock.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\slash.png"
File "C:\Users\Zach\Documents\Rider\OLED_Keyboard\OLEDKeyboard\OLEDWindowsApplication\bin\Release\net6.0-windows\Resources\tab.png"
SectionEnd

######################################################################

Section -Icons_Reg
SetOutPath "$INSTDIR"
WriteUninstaller "$INSTDIR\uninstall.exe"

!ifdef REG_START_MENU
!insertmacro MUI_STARTMENU_WRITE_BEGIN Application
CreateDirectory "$SMPROGRAMS\$SM_Folder"
CreateShortCut "$SMPROGRAMS\$SM_Folder\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
CreateShortCut "$SMPROGRAMS\$SM_Folder\Uninstall ${APP_NAME}.lnk" "$INSTDIR\uninstall.exe"

!ifdef WEB_SITE
WriteIniStr "$INSTDIR\${APP_NAME} website.url" "InternetShortcut" "URL" "${WEB_SITE}"
CreateShortCut "$SMPROGRAMS\$SM_Folder\${APP_NAME} Website.lnk" "$INSTDIR\${APP_NAME} website.url"
!endif
!insertmacro MUI_STARTMENU_WRITE_END
!endif

!ifndef REG_START_MENU
CreateDirectory "$SMPROGRAMS\OLED Keyboard"
CreateShortCut "$SMPROGRAMS\OLED Keyboard\${APP_NAME}.lnk" "$INSTDIR\${MAIN_APP_EXE}"
CreateShortCut "$SMPROGRAMS\OLED Keyboard\Uninstall ${APP_NAME}.lnk" "$INSTDIR\uninstall.exe"

!ifdef WEB_SITE
WriteIniStr "$INSTDIR\${APP_NAME} website.url" "InternetShortcut" "URL" "${WEB_SITE}"
CreateShortCut "$SMPROGRAMS\OLED Keyboard\${APP_NAME} Website.lnk" "$INSTDIR\${APP_NAME} website.url"
!endif
!endif

WriteRegStr ${REG_ROOT} "${REG_APP_PATH}" "" "$INSTDIR\${MAIN_APP_EXE}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayName" "${APP_NAME}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "UninstallString" "$INSTDIR\uninstall.exe"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayIcon" "$INSTDIR\${MAIN_APP_EXE}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "DisplayVersion" "${VERSION}"
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "Publisher" "${COMP_NAME}"

!ifdef WEB_SITE
WriteRegStr ${REG_ROOT} "${UNINSTALL_PATH}"  "URLInfoAbout" "${WEB_SITE}"
!endif
SectionEnd

######################################################################

Section Uninstall
${INSTALL_TYPE}
Delete "$INSTDIR\Aspose.Imaging.dll"
Delete "$INSTDIR\Microsoft.Extensions.Configuration.Abstractions.dll"
Delete "$INSTDIR\Microsoft.Extensions.Configuration.Binder.dll"
Delete "$INSTDIR\Microsoft.Extensions.Configuration.CommandLine.dll"
Delete "$INSTDIR\Microsoft.Extensions.Configuration.dll"
Delete "$INSTDIR\Microsoft.Extensions.Configuration.EnvironmentVariables.dll"
Delete "$INSTDIR\Microsoft.Extensions.Configuration.FileExtensions.dll"
Delete "$INSTDIR\Microsoft.Extensions.Configuration.Json.dll"
Delete "$INSTDIR\Microsoft.Extensions.Configuration.UserSecrets.dll"
Delete "$INSTDIR\Microsoft.Extensions.DependencyInjection.Abstractions.dll"
Delete "$INSTDIR\Microsoft.Extensions.DependencyInjection.dll"
Delete "$INSTDIR\Microsoft.Extensions.FileProviders.Abstractions.dll"
Delete "$INSTDIR\Microsoft.Extensions.FileProviders.Physical.dll"
Delete "$INSTDIR\Microsoft.Extensions.FileSystemGlobbing.dll"
Delete "$INSTDIR\Microsoft.Extensions.Hosting.Abstractions.dll"
Delete "$INSTDIR\Microsoft.Extensions.Hosting.dll"
Delete "$INSTDIR\Microsoft.Extensions.Logging.Abstractions.dll"
Delete "$INSTDIR\Microsoft.Extensions.Logging.Configuration.dll"
Delete "$INSTDIR\Microsoft.Extensions.Logging.Console.dll"
Delete "$INSTDIR\Microsoft.Extensions.Logging.Debug.dll"
Delete "$INSTDIR\Microsoft.Extensions.Logging.dll"
Delete "$INSTDIR\Microsoft.Extensions.Logging.EventLog.dll"
Delete "$INSTDIR\Microsoft.Extensions.Logging.EventSource.dll"
Delete "$INSTDIR\Microsoft.Extensions.Options.ConfigurationExtensions.dll"
Delete "$INSTDIR\Microsoft.Extensions.Options.dll"
Delete "$INSTDIR\Microsoft.Extensions.Primitives.dll"
Delete "$INSTDIR\OLEDWindowsApplication.deps.json"
Delete "$INSTDIR\OLEDWindowsApplication.dll"
Delete "$INSTDIR\OLEDWindowsApplication.exe"
Delete "$INSTDIR\OLEDWindowsApplication.pdb"
Delete "$INSTDIR\OLEDWindowsApplication.runtimeconfig.json"
Delete "$INSTDIR\System.CodeDom.dll"
Delete "$INSTDIR\System.IO.Ports.dll"
Delete "$INSTDIR\System.Management.dll"
Delete "$INSTDIR\runtimes\win\lib\net6.0\System.Diagnostics.EventLog.Messages.dll"
Delete "$INSTDIR\runtimes\win\lib\net6.0\System.IO.Ports.dll"
Delete "$INSTDIR\runtimes\win\lib\net6.0\System.Management.dll"
Delete "$INSTDIR\runtimes\unix\lib\net6.0\System.IO.Ports.dll"
Delete "$INSTDIR\runtimes\osx-x64\native\libSystem.IO.Ports.Native.dylib"
Delete "$INSTDIR\runtimes\osx-arm64\native\libSystem.IO.Ports.Native.dylib"
Delete "$INSTDIR\runtimes\linux-x64\native\libSystem.IO.Ports.Native.so"
Delete "$INSTDIR\runtimes\linux-arm64\native\libSystem.IO.Ports.Native.so"
Delete "$INSTDIR\runtimes\linux-arm\native\libSystem.IO.Ports.Native.so"
Delete "$INSTDIR\Resources\+.png"
Delete "$INSTDIR\Resources\-.png"
Delete "$INSTDIR\Resources\0.png"
Delete "$INSTDIR\Resources\1.png"
Delete "$INSTDIR\Resources\2.png"
Delete "$INSTDIR\Resources\3.png"
Delete "$INSTDIR\Resources\4.png"
Delete "$INSTDIR\Resources\5.png"
Delete "$INSTDIR\Resources\6.png"
Delete "$INSTDIR\Resources\7.png"
Delete "$INSTDIR\Resources\8.png"
Delete "$INSTDIR\Resources\9.png"
Delete "$INSTDIR\Resources\=.png"
Delete "$INSTDIR\Resources\asterisk.png"
Delete "$INSTDIR\Resources\back.png"
Delete "$INSTDIR\Resources\cal.png"
Delete "$INSTDIR\Resources\del.png"
Delete "$INSTDIR\Resources\ent.png"
Delete "$INSTDIR\Resources\esc.png"
Delete "$INSTDIR\Resources\numlock.png"
Delete "$INSTDIR\Resources\slash.png"
Delete "$INSTDIR\Resources\tab.png"
 
RmDir "$INSTDIR\Resources"
RmDir "$INSTDIR\runtimes\linux-arm\native"
RmDir "$INSTDIR\runtimes\linux-arm64\native"
RmDir "$INSTDIR\runtimes\linux-x64\native"
RmDir "$INSTDIR\runtimes\osx-arm64\native"
RmDir "$INSTDIR\runtimes\osx-x64\native"
RmDir "$INSTDIR\runtimes\unix\lib\net6.0"
RmDir "$INSTDIR\runtimes\win\lib\net6.0"
 
Delete "$INSTDIR\uninstall.exe"
!ifdef WEB_SITE
Delete "$INSTDIR\${APP_NAME} website.url"
!endif

RmDir "$INSTDIR"

!ifdef REG_START_MENU
!insertmacro MUI_STARTMENU_GETFOLDER "Application" $SM_Folder
Delete "$SMPROGRAMS\$SM_Folder\${APP_NAME}.lnk"
Delete "$SMPROGRAMS\$SM_Folder\Uninstall ${APP_NAME}.lnk"
!ifdef WEB_SITE
Delete "$SMPROGRAMS\$SM_Folder\${APP_NAME} Website.lnk"
!endif
RmDir "$SMPROGRAMS\$SM_Folder"
!endif

!ifndef REG_START_MENU
Delete "$SMPROGRAMS\OLED Keyboard\${APP_NAME}.lnk"
Delete "$SMPROGRAMS\OLED Keyboard\Uninstall ${APP_NAME}.lnk"
!ifdef WEB_SITE
Delete "$SMPROGRAMS\OLED Keyboard\${APP_NAME} Website.lnk"
!endif
RmDir "$SMPROGRAMS\OLED Keyboard"
!endif

DeleteRegKey ${REG_ROOT} "${REG_APP_PATH}"
DeleteRegKey ${REG_ROOT} "${UNINSTALL_PATH}"
SectionEnd

######################################################################

