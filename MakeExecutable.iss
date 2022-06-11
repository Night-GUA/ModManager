; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define public Dependency_NoExampleSetup
#include "D:\visualstudio\ModManager5\Installer\CodeDependencies.iss"

#define MyAppName "ModManager"
#define MyAppVersion "5.0.9"
#define MyAppPublisher "Matux"
#define MyAppURL "https://matux.fr"
#define MyAppExeName "ModManager5.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{B0985205-5235-4036-8F71-CC67D10DA0BE}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\ModManager5
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputBaseFilename=ModManagerInstaller
SetupIconFile=D:\visualstudio\ModManager5\ModManager5\modmanager.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
ArchitecturesInstallIn64BitMode=x64

[Code]
function InitializeSetup: Boolean;
begin
  ExtractTemporaryFile('netcorecheck.exe');     
  ExtractTemporaryFile('netcorecheck_x64.exe');
  Dependency_AddDotNet50;
  Dependency_AddDotNet50Desktop;
  


  Result := True;
end;

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "armenian"; MessagesFile: "compiler:Languages\Armenian.isl"
Name: "brazilianportuguese"; MessagesFile: "compiler:Languages\BrazilianPortuguese.isl"
Name: "bulgarian"; MessagesFile: "compiler:Languages\Bulgarian.isl"
Name: "catalan"; MessagesFile: "compiler:Languages\Catalan.isl"
Name: "corsican"; MessagesFile: "compiler:Languages\Corsican.isl"
Name: "czech"; MessagesFile: "compiler:Languages\Czech.isl"
Name: "danish"; MessagesFile: "compiler:Languages\Danish.isl"
Name: "dutch"; MessagesFile: "compiler:Languages\Dutch.isl"
Name: "finnish"; MessagesFile: "compiler:Languages\Finnish.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"
Name: "hebrew"; MessagesFile: "compiler:Languages\Hebrew.isl"
Name: "icelandic"; MessagesFile: "compiler:Languages\Icelandic.isl"
Name: "italian"; MessagesFile: "compiler:Languages\Italian.isl"
Name: "japanese"; MessagesFile: "compiler:Languages\Japanese.isl"
Name: "norwegian"; MessagesFile: "compiler:Languages\Norwegian.isl"
Name: "polish"; MessagesFile: "compiler:Languages\Polish.isl"
Name: "portuguese"; MessagesFile: "compiler:Languages\Portuguese.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"
Name: "slovak"; MessagesFile: "compiler:Languages\Slovak.isl"
Name: "slovenian"; MessagesFile: "compiler:Languages\Slovenian.isl"
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"
Name: "turkish"; MessagesFile: "compiler:Languages\Turkish.isl"
Name: "ukrainian"; MessagesFile: "compiler:Languages\Ukrainian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\visualstudio\ModManager5\ModManager5\bin\Debug\net5.0-windows\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\ModManager5\ModManager5\bin\Debug\net5.0-windows\ModManager5.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\ModManager5\ModManager5\bin\Debug\net5.0-windows\ModManager5.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\ModManager5\ModManager5\bin\Debug\net5.0-windows\ModManager5.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\ModManager5\ModManager5\bin\Debug\net5.0-windows\ModManager5.runtimeconfig.dev.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\ModManager5\ModManager5\bin\Debug\net5.0-windows\ModManager5.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\ModManager5\ModManager5\bin\Debug\net5.0-windows\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\ModManager5\ModManager5\bin\Debug\net5.0-windows\Octokit.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\visualstudio\ModManager5\ModManager5\token.txt"; DestDir: "{app}"; Flags: ignoreversion

Source: "D:\visualstudio\ModManager5\Installer\src\netcorecheck.exe"; Flags: dontcopy noencryption
Source: "D:\visualstudio\ModManager5\Installer\src\netcorecheck_x64.exe"; Flags: dontcopy noencryption

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

