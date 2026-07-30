#!/usr/bin/bash

# basic once init
INITFST=0 
if [ -d ./In/ ] ; then
  INITFST=1;
fi
if [ $INITFST -lt 1 ] ; then
  mkdir -p In 
  mkdir -p Out 
  mkdir -p Encrypt
  cp -ax ../../../../../../docu/html/* ./In/.
  INITFST=1;
fi

SECPIPE=0
echo "$0: deleting old files in Encrypt/* Out/*"
rm -f  ./Encrypt/*.*
rm -f  ./Out/*.*

if [ $# -gt 0 ] ; then
        if [ $1 =="-S" ] ; then SECPIPE=1; fi
        if [ $1 =="-s" ] ; then SECPIPE=1; fi
        if [ $1 =="/S" ] ; then SECPIPE=1; fi
        if [ $1 =="/s" ] ; then SECPIPE=1; fi
fi

if [ $SECPIPE -lt 1 ] ; then

        echo "$0: Encrypting files in ./In/ to ./Encrypt/ using key bar@ba.area23.at"
        echo "./EU.CqrXs.Spooler -V -M=CFB -k=bar@ba.area23.at -i=./In/ -o=./Encrypt/"
        ./EU.CqrXs.Spooler -V -M=CFB -k=bar@ba.area23.at -i=./In/ -o=./Encrypt/

        echo "$0: Decrypting files in ./Encrypt/ to ./Out/ using key bar@ba.area23.at"
        echo "./EU.CqrXs.Spooler.exe -V -D -M=CFB -i=./Encrypt/ -o=./Out/ "
        ./EU.CqrXs.Spooler -V -D -M=CFB -k=bar@ba.area23.at -i=./Encrypt/ -o=./Out/

else 

        echo "$0: Encrypting files in ./In/ to ./Encrypt/ using key jo@io.cqrxs.eu "
        echo "./EU.CqrXs.Spooler -V -S -k=jo@io.cqrxs.eu -i=./In/ -o=./Encrypt/  "
        ./EU.CqrXs.Spooler -V -S -k=jo@io.cqrxs.eu -i=./In/ -o=./Encrypt/
        echo "$0: Decrypting files in ./Encrypt/ to ./Out/ using key jo@io.cqrxs.eu "
        echo "./EU.CqrXs.Spooler -V -D -S -k=jo@io.cqrxs.eu -i=./Encrypt/ -o=./Out/ "
        ./EU.CqrXs.Spooler -V -D -S -k=jo@io.cqrxs.eu -i=./Encrypt/ -o=./Out/

fi
