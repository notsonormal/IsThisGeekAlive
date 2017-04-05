#define MyAppName "Is This Geek Alive Monitor"
#define MyAppVersion "1.0"
#define MyAppPublisher "Astounding Applications"
#define MyAppURL "http://www.astoundingapplications.com/"
#define MyAppExeName "IsThisGeekAliveMonitor"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{B28ED083-688E-4EA8-8713-12565BF6450D}}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableDirPage=yes
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputBaseFilename={#MyAppExeName}Setup
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin
UninstallDisplayIcon={app}\{#MyAppExeName}.exe
UninstallDisplayName={#MyAppName}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Registry]
; When you uninstall the application, remove the "RunApplicationOnStartup" registry key
root: HKLM; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "IsThisGeekAliveMonitor"; ValueData: """{app}\{#MyAppExeName}.exe"""; Flags: uninsdeletevalue

[Files]
Source: "..\bin\Release\IsThisGeekAliveMonitor.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\InnoSetup\NetFrameworkInstaller_4.5.2.exe"; DestDir: {tmp}; Flags: deleteafterinstall; Check: Framework45IsNotInstalled
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"

[Run]
Filename: "{tmp}\NetFrameworkInstaller_4.5.2.exe"; Parameters: "/q:a /c:""install /l /q"""; Check: Framework45IsNotInstalled; StatusMsg: Microsoft Framework 4.5.2 Framework is being installed. Please wait...
Filename: "{app}\IsThisGeekAliveMonitor.exe"; Description: "{cm:LaunchProgram,Is This Geek Alive Monitor}"; Flags: nowait postinstall skipifsilent

[Dirs]
Name: {app}; Permissions: users-full

[Code]
function GetUninstallString(AppId: String): String;
var
  sUnInstPath: String;
  sUnInstallString: String;
begin
  sUnInstPath := 'Software\Microsoft\Windows\CurrentVersion\Uninstall\' + AppId + '_is1';

  sUnInstallString := '';
  if not RegQueryStringValue(HKLM, sUnInstPath, 'UninstallString', sUnInstallString) then
    RegQueryStringValue(HKCU, sUnInstPath, 'UninstallString', sUnInstallString);
  Result := sUnInstallString;
end;

function IsUpgrade(AppId: String): Boolean;
begin
  Result := (GetUninstallString(AppId) <> '');
end;

function UnInstallOldVersion(AppId: String): Integer;
var
  sUnInstallString: String;
  iResultCode: Integer;
begin
// Return Values:
// 1 - uninstall string is empty
// 2 - error executing the UnInstallString
// 3 - successfully executed the UnInstallString

  // default return value
  Result := 0;

  // get the uninstall string of the old app
  sUnInstallString := GetUninstallString(AppId);
  if sUnInstallString <> '' then begin
    sUnInstallString := RemoveQuotes(sUnInstallString);
    if Exec(sUnInstallString, '/SILENT /NORESTART /SUPPRESSMSGBOXES','', SW_HIDE, ewWaitUntilTerminated, iResultCode) then
      Result := 3
    else
      Result := 2;
  end else
    Result := 1;
end;

function Framework45IsNotInstalled(): Boolean;
var
  bSuccess: Boolean;
  regVersion: Cardinal;
begin
  Result := True;

  bSuccess := RegQueryDWordValue(HKLM, 'Software\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', regVersion);
  if (True = bSuccess) and (regVersion >= 378389) then begin
    Result := False;
  end;
end;

procedure InstallStep();
var
  AppId: String;
begin
	AppId := ExpandConstant('{#emit SetupSetting("AppId")}');

    if (IsUpgrade(AppId)) then
    begin
      UnInstallOldVersion(AppId);
    end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if (CurStep=ssInstall) then
  begin
    InstallStep();
  end;
end;