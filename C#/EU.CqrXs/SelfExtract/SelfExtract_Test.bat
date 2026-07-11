@echo off

set "__NAME=%~n0"
set "__SOURCE=EU.CqrXs.SelfExtract.exe"
if "%~1"=="" goto :test
set OutCryptFile=%1
set "__OutFile=%~1"
set "__ExeFile=%__OutFile%.exe"
if "%~2"=="" set "__ExeFile=%~2" 
if exist %__OutFile% goto :copy
echo  %__NAME%: file %__OutFile% doesn't exist.
exit 1


:copy 
echo %__NAME%: copy /b  %__SOURCE% + %__OutFile% %__ExeFile%
timeout 2 > NUL
copy /b %__SOURCE% + %__OutFile% %__ExeFile%
goto :end



:test

echo %__NAME%: Staring EU.CqrXs.SelfExtract.exe tests
echo %__NAME%: deleting README_MD.base64.crypt.gz README_MD.base64.scrypt.gz  README_MD_1.txt README_MD_2.txt
del /q README_MD.base64.crypt.gz README_MD.base64.scrypt.gz  README_MD_1.txt README_MD_2.txt

@echo on

EU.CqrXs.SelfExtract.exe -V -X -i=.\README.MD -k=io.cqrxs.eu -z=gzip -e=base64 -o=.\README_MD.base64.crypt.gz
EU.CqrXs.SelfExtract.exe -V -X i=.\README.MD -S -k=io.cqrxs.eu -z=gzip -o=.\README_MD.base64.scrypt.gz


REM EU.CqrXs.SelfExtract.exe -V -D -i=.\README_MD.base64.crypt.gz -k=io.cqrxs.eu -z=gzip -e=base64 -o=.\README_MD_1.txt
REM EU.CqrXs.SelfExtract.exe -V -D -i=.\README_MD.base64.scrypt.gz -S -k=io.cqrxs.eu -z=gzip -o=.\README_MD_2.txt

timeout 1 > NUL
REM start notepad README_MD_1.txt
REM timeout 1 > NUL
REM start notepad README_MD_2.txt
REM timeout 2 > NUL
goto :end


:end

echo %__NAME%: finished, waiting 30 seconds to close
timeout 30 > NUL
REM pause
