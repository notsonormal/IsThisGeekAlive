#define MyAppName "Is This Geek Alive Monitor"
#define MyAppVersion "1.0.0.0"
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
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName=C:\{pf}\{#MyAppName}
DisableDirPage=yes
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputBaseFilename=IsThisGeekAliveMonitorSetup
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Registry]
; When you uninstall the application, remove the "RunApplicationOnStartup" registry key if it was created
root: HKLM; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "IsThisGeekAliveMonitor"; Flags: dontcreatekey uninsdeletevalue

[Files]
Source: "..\bin\Release\IsThisGeekAliveMonitor.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\IsThisGeekAliveMonitor.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\InnoSetup\NetFrameworkInstaller_4.5.2.exe"; DestDir: {tmp}; Flags: deleteafterinstall; Check: Framework45IsNotInstalled
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"

[Run]
Filename: "{tmp}\NetFrameworkInstaller_4.5.2.exe"; Parameters: "/q:a /c:""install /l /q"""; Check: CheckForFramework; StatusMsg: Microsoft Framework 4.5.2 Framework is being installed. Please wait...
Filename: "{app}\IsThisGeekAliveMonitor.exe"; Description: "{cm:LaunchProgram,Is This Geek Alive Monitor}"; Flags: nowait postinstall skipifsilent

[Code]
procedure CurStepChanged(CurStep: TSetupStep);
begin
  if (CurStep=ssInstall) then
  begin
    InstallStep();
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
end

function Framework45IsNotInstalled(): Boolean;
var
  bSuccess: Boolean;
  regVersion: Cardinal;
begin
  Result := True;

  bSuccess := RegQueryDWordValue(HKLM, ‘Software\Microsoft\NET Framework Setup\NDP\v4\Full’, ‘Release’, regVersion);
  if (True = bSuccess) and (regVersion >= 378389) then begin
    Result := False;
  end;
end;