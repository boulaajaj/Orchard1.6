
@echo off

echo zipping...

del Richinoz.zip

set path="C:\Program Files\WinRAR\";%path%

winrar a -afzip -r -xweb.config -x/myFiles -x*.bat -x*.cs -x*.csproj -x*.user -x*svn*\ -x*\svn\ Richinoz

echo zipped!


echo uploading...

upload Richinoz.zip Richinoz
rem upload "C:\Users\rich\Documents\Visual Studio 2010\Projects\ArtSite\ArtSite\Richinoz.zip"


pause