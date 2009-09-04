Name "Simple.Net"
OutFile "Simple.exe"
RequestExecutionLevel user

Section
  SetOutPath $EXEDIR
  File /r /x _svn /x bin /x obj /x *.nsi *.*
  ExecWait $OUTDIR\SimpleGui.exe
  Delete $OUTDIR\SimpleGui.exe
SectionEnd