#!/usr/bin/bash
#
# INITFST 0 ... create spooler directory structure
# INITFST 1 ... spooler directory structure already exist
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
#
# SECPIPE 0 ... no secure cipher pipe switch
# SECPIPE 0 ... secure cipher pipe enabled
SECPIPE=0
if [ $# -gt 0 ] ; then
	if [ $1 =="-S" ] ; then SECPIPE=1; fi
	if [ $1 =="-s" ] ; then SECPIPE=1; fi
	if [ $1 =="/S" ] ; then SECPIPE=1; fi
	if [ $1 =="/s" ] ; then SECPIPE=1; fi
fi
#
# cleaning up
#
echo "$0: deleting old files in Encrypt/* Out/*"
rm -f  ./Encrypt/*.*
rm -f  ./Out/*.*
#
# switch $SECPIPE
#
if [ $SECPIPE -lt 1 ] ; then
# spool & encrypt
	echo "$0: Encrypting files in ./In/ to ./Encrypt/ using key bar@ba.area23.at"
	echo "./EU.CqrXs.Spooler -V -M=CFB -k=bar@ba.area23.at -i=./In/ -o=./Encrypt/"
	./EU.CqrXs.Spooler -V -M=CFB -k=bar@ba.area23.at -i=./In/ -o=./Encrypt/
# spool & decrypt
	echo "$0: Decrypting files in ./Encrypt/ to ./Out/ using key bar@ba.area23.at"
	echo "./EU.CqrXs.Spooler.exe -V -D -M=CFB -i=./Encrypt/ -o=./Out/ "
	./EU.CqrXs.Spooler -V -D -M=CFB -k=bar@ba.area23.at -i=./Encrypt/ -o=./Out/
else 
# SECPIPE spool & encrypt
	echo "$0: Encrypting files in ./In/ to ./Encrypt/ using key jo@io.cqrxs.eu "
	echo "./EU.CqrXs.Spooler -V -S -k=jo@io.cqrxs.eu -i=./In/ -o=./Encrypt/  "
	./EU.CqrXs.Spooler -V -S -k=jo@io.cqrxs.eu -i=./In/ -o=./Encrypt/
# SECPIPE spool & decrypt
	echo "$0: Decrypting files in ./Encrypt/ to ./Out/ using key jo@io.cqrxs.eu "
	echo "./EU.CqrXs.Spooler -V -D -S -k=jo@io.cqrxs.eu -i=./Encrypt/ -o=./Out/ "
	./EU.CqrXs.Spooler -V -D -S -k=jo@io.cqrxs.eu -i=./Encrypt/ -o=./Out/
fi
#
# spooler finished
# 
echo "$0: finished."