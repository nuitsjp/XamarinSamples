echo off
copy /Y ..\Hello.Fody\bin\Debug\netstandard1.3\Hello.Fody.dll .
copy /Y ..\Hello.Fody\bin\Debug\netstandard1.3\Hello.Fody.pdb .
nuget.exe pack Hello.Fody.nuspec -Exclude nuget.exe -Exclude package.cmd