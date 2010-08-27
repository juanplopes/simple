!include "WinMessages.nsh"

Name "Simple.Net Project"
OutFile "Simple.exe"
Icon "..\..\gfx\Icon.ico"
RequestExecutionLevel user
SilentInstall silent

Section
  Banner::show "Simple.Net is preparing required files..."

  GetTempFileName $0
  Delete $0
  CreateDirectory $0

  SetOutPath $0\data
  File /r /x _svn /x .svn /x bin /x build /x pkg /x obj /x *.nsi /x *.suo /x *.cache /x *.log /x Simple.exe /x Simple.Gui.exe /x TestResult.xml *.* 
  
  SetOutPath $0
  File Simple.Gui.exe

  SetOutPath $EXEDIR

  Banner::destroy
  ExecWait $0\Simple.Gui.exe

  RMDir /r /REBOOTOK $0
SectionEnd