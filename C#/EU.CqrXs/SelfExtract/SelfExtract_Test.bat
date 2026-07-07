@echo off

echo Staring SelfExtract EU.CqrXs.SelfExtract.exe tests
echo deleting README_MD.base64.crypt.gz README_MD.base64.scrypt.gz  README_MD_1.txt README_MD_2.txt

del /q README_MD.base64.crypt.gz README_MD.base64.scrypt.gz  README_MD_1.txt README_MD_2.txt
@echo on

EU.CqrXs.SelfExtract.exe -V -i=.\README.MD -k=io.cqrxs.eu -z=gzip -e=base64 -o=.\README_MD.base64.crypt.gz
EU.CqrXs.SelfExtract.exe -V -i=.\README.MD -S -k=io.cqrxs.eu -z=gzip -o=.\README_MD.base64.scrypt.gz


EU.CqrXs.SelfExtract.exe -V -D -i=.\README_MD.base64.crypt.gz -k=io.cqrxs.eu -z=gzip -e=base64 -o=.\README_MD_1.txt
EU.CqrXs.SelfExtract.exe -V -D -i=.\README_MD.base64.scrypt.gz -S -k=io.cqrxs.eu -z=gzip -o=.\README_MD_2.txt

timeout 1 > NUL
start notepad README_MD_1.txt
timeout 1 > NUL
start notepad README_MD_2.txt
timeout 1 > NUL

echo finished, waiting 30 seconds to close
timeout 30 > NUL
REM pause
