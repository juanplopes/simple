Name "Simple.Net project"
OutFile "Simple.exe"
Icon "..\..\doc\SimpleIcon.ico"
RequestExecutionLevel user
AutoCloseWindow true
BrandingText "juanplopes@gmail.com"

;Default installation folder
InstallDir "$EXEDIR\sample-project\"

Page directory
Page instfiles

Section
  SetOutPath $INSTDIR
  File /r /x _svn /x bin /x obj /x *.nsi /x *.suo /x Simple.exe /x Simple.dll *.* 
  File /oname=lib\Simple.dll lib\Simple.dll 
  ExecWait $OUTDIR\SimpleGui.exe
  Delete $OUTDIR\SimpleGui.exe
SectionEnd