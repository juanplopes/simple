set assemblies=
for %%s in (release\*.dll) do set assemblies=!assemblies! %%s
echo %assemblies%
pause