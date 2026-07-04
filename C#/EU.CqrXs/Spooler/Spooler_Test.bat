@echo off 

echo "deleting old files in Encrypt\* Out\*"
del /s /q .\Encrypt\*.*
del /s /q .\Out\*.*

:initial
if "%1"=="-S" goto secpipe
if "%1"=="-s" goto secpipe
if "%1"=="/S" goto secpipe
if "%1"=="/s" goto secpipe

echo "CipherPipe: Encrypting files in .\In\ to .\Encrypt\ using key bar@ba.area23.at"
echo "EU.CqrXs.Spooler.exe -V -M=CFB -k=bar@ba.area23.at -i=.\In\ -o=.\Encrypt\
EU.CqrXs.Spooler.exe -V -M=CFB -k=bar@ba.area23.at -i=.\In\ -o=.\Encrypt\ 
echo "CipherPipe: Decrypting files in .\Encrypt\ to .\Out\ using key bar@ba.area23.at"
echo "EU.CqrXs.Spooler.exe -V -D -M=CFB -i=.\Encrypt\ -o=.\Out\ "
EU.CqrXs.Spooler.exe -V -D -M=CFB -k=bar@ba.area23.at -i=.\Encrypt\ -o=.\Out\ 
goto end


:secpipe
echo "SecureCipherPipe: Encrypting files in .\In\ to .\Encrypt\ using key jo@io.cqrxs.eu "
echo "EU.CqrXs.Spooler.exe -V -S -k=jo@io.cqrxs.eu -i=.\In\ -o=.\Encrypt\  "
EU.CqrXs.Spooler.exe -V -S -k=jo@io.cqrxs.eu -i=.\In\ -o=.\Encrypt\ 
echo "SecureCipherPipe: Decrypting files in .\Encrypt\ to .\Out\ using key jo@io.cqrxs.eu "
echo "EU.CqrXs.Spooler.exe -V -D -S -k=jo@io.cqrxs.eu -i=.\Encrypt\ -o=.\Out\ "
EU.CqrXs.Spooler.exe -V -D -S -k=jo@io.cqrxs.eu -i=.\Encrypt\ -o=.\Out\ 
goto end


:end

REM exit
