Name "Simple.Net project"
OutFile "Simple.exe"
Icon "..\doc\SimpleIcon.ico"
RequestExecutionLevel user
AutoCloseWindow true
BrandingText "juanplopes@gmail.com"

;Default installation folder
InstallDir "$EXEDIR"

Page directory
Page instfiles

Section
  SetOutPath $INSTDIR
  File /r /x _svn /x bin /x obj /x *.nsi /x Simple.exe *.* 
  ExecWait $OUTDIR\SimpleGui.exe
  Delete $OUTDIR\SimpleGui.exe
SectionEnd