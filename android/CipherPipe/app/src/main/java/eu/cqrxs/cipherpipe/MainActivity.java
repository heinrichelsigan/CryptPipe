/**
 * @author           <a href="mailto:heinrich.elsigan@area23.at">Heinrich Elsigan</a>
 * @version          V 1.0.1
 * @since            API 27 Oreo 8.1
 *
 * Coded 2021-2025 by
 * <a href="mailto:he@area23.at">Heinrich.Elsigan</a><a href="https://heinrichelsigan.area23.at">heinrichelsigan.area23.at</a>
.*/

package eu.cqrxs.cipherpipe;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Toast;


import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import java.util.SortedMap;

import eu.cqrxs.crypt.cipher.CipherEnum;
import eu.cqrxs.crypt.cipher.CipherPipe;
import eu.cqrxs.crypt.cipher.SecureCipherPipe;
import eu.cqrxs.crypt.cipher.CryptParams;
import eu.cqrxs.crypt.cipher.CryptHelper;
import eu.cqrxs.crypt.cipher.CipherMode2;
import eu.cqrxs.crypt.cipher.symmetric.CryptBounceCastle;
import eu.cqrxs.crypt.cipher.symmetric.JAes;
import eu.cqrxs.crypt.cipher.symmetric.ZenMatrix;
import eu.cqrxs.crypt.cipher.symmetric.ZenMatrix2;
import eu.cqrxs.crypt.cipher.symmetric.ZenMatrix3;


import eu.cqrxs.crypt.hash.KeyHash;
import eu.cqrxs.crypt.hash.BCrypt;
import eu.cqrxs.crypt.hash.SCrypt;
import eu.cqrxs.crypt.hash.MD5;
import eu.cqrxs.crypt.hash.OpenBSDCrypt;
import eu.cqrxs.crypt.hash.Sha256;
import eu.cqrxs.crypt.hash.Sha512;

import eu.cqrxs.crypt.encoding.EncodeEnum;
import eu.cqrxs.crypt.encoding.IEncodable;
import eu.cqrxs.crypt.encoding.EnDeCodeHelper;
import eu.cqrxs.crypt.encoding.Hex16Coder;
import eu.cqrxs.crypt.encoding.Hex32Coder;
import eu.cqrxs.crypt.encoding.Hex64Coder;
import eu.cqrxs.crypt.encoding.Base16Coder;
import eu.cqrxs.crypt.encoding.Base64Coder;
import eu.cqrxs.crypt.encoding.UuCoder;
import eu.cqrxs.crypt.encoding.XxEncoder;

import eu.cqrxs.zip.GZ;
import eu.cqrxs.zip.ZipType;


public class MainActivity extends BaseActivity{

    Button btnEncrypt, btnDecrypt, btnSetPipe, btnHashPipe, btnRandText, btnReset;
    ImageButton imgBtnKey, imgBtnHash;
    ImageView imgViewCipherPipe;

    EditText editEncryptKey, showCipherPipe, editTextSource, showTextDestination, editKeyHash;
    Spinner spinnerAlgos; // spinnerHash, spinnerZip,  spinnerEncode;
    ArrayAdapter adapterAlgos = null; //  adapterHash = null, adapterEndoding = null, adapterZip = null,

    CipherEnum cipher;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        try {
            hashStrings = KeyHash.getNames();
            encodingStrings = EncodeEnum.getNames();
            zipStrings = ZipType.getNames();
            algoStrings = CipherEnum.getNames();
        } catch (Exception exi) {
            showException(exi);
            // showTextDestination.setText(exi.toString());
        }
        imgViewCipherPipe = (ImageView) findViewById(R.id.imgViewCipherPipe);
        imgBtnKey =  (ImageButton) findViewById(R.id.imgBtnKey);
        imgBtnHash =  (ImageButton) findViewById(R.id.imgBtnHash);
        btnSetPipe = (Button) findViewById(R.id.btnSetPipe);
        btnHashPipe = (Button) findViewById(R.id.btnHashPipe);
        btnEncrypt = (Button) findViewById(R.id.btnEncrypt);
        btnDecrypt = (Button) findViewById(R.id.btnDecrypt);
        btnRandText = (Button) findViewById(R.id.btnRandText);
        btnReset = (Button) findViewById(R.id.btnReset);
        editEncryptKey = (EditText) findViewById(R.id.editEncryptKey);
        editKeyHash = (EditText) findViewById(R.id.editKeyHash);
        showCipherPipe = (EditText) findViewById(R.id.showCipherPipe);
        editTextSource = (EditText) findViewById(R.id.editTextSource);
        showTextDestination = (EditText) findViewById(R.id.showTextDestination);

        // spinnerHash = (Spinner) findViewById(R.id.spinnerHash);
        // spinnerZip = (Spinner) findViewById(R.id.spinnerZip);
        spinnerAlgos = (Spinner) findViewById(R.id.spinnerAlgos);
        // spinnerEncode = (Spinner) findViewById(R.id.spinnerEncode);

        btnSetPipe.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setCipherPipe();
            }
        });

        btnHashPipe.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                hashCipherPipe();
            }
        });

        btnRandText.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                randomText();
            }
        });

        btnReset.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                appRestart();
            }
        });

        btnEncrypt.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                performEncrypt();
            }
        });

        btnDecrypt.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                performDecrypt();
            }
        });

        /*
        if (adapterHash == null)
            adapterHash = new ArrayAdapter<>(MainActivity.this, android.R.layout.simple_spinner_dropdown_item, hashStrings);
        adapterHash.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerHash.setAdapter(adapterHash);
        spinnerHash.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

                try {
                    String msg = "adapterViewId: " + parent.getId() + " adapterView: " + parent.getAdapter().toString();
                    Log.d("onItemSelected", msg);
                } catch (Exception exAdapter) {
                }

                selectedHash = parent.getSelectedItem().toString();
                keyHash = KeyHash.getEnum(selectedHash);
                hashKey(keyHash);

                CipherEnum[] ciphers = getCipherEnums();
                cipherPipe = new CipherPipe(ciphers, 8, encodeType, zipType, keyHash, cmode);
                drawCipherPipe(cipherPipe);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                // parent.setSelection(6);
                ;
            }
        });
        if (firstTimeInitEncodings)
            spinnerHash.setSelection(5);
         */

        /*
        if (adapterEndoding == null)
            adapterEndoding = new ArrayAdapter<>(MainActivity.this, android.R.layout.simple_spinner_dropdown_item, encodingStrings);
        adapterEndoding.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerEncode.setAdapter(adapterEndoding);
        spinnerEncode.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                try {
                    String msg = "adapterViewId: " + parent.getId() + " adapterView: " + parent.getAdapter().toString();
                    Log.d("onItemSelected", msg);
                } catch (Exception exAdapter) {
                }

                selectEncodeType = parent.getSelectedItem().toString();
                encodeType = EncodeEnum.getEnum(selectEncodeType);

                CipherEnum[] ciphers = getCipherEnums();
                cipherPipe = new CipherPipe(ciphers, 8, encodeType, zipType, keyHash, cmode);
                drawCipherPipe(cipherPipe);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                ;
            }
        });

        if (firstTimeInitEncodings) {
            spinnerEncode.setSelection(6);
            firstTimeInitEncodings = false;
        }
         */

        if (adapterAlgos == null)
            adapterAlgos = new ArrayAdapter<>(MainActivity.this, android.R.layout.simple_spinner_dropdown_item, algoStrings);
        adapterAlgos.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerAlgos.setAdapter(adapterAlgos);
        spinnerAlgos.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                try {
                    String msg = "adapterViewId: " + parent.getId() + " adapterView: " + parent.getAdapter().toString();
                    Log.d("onItemSelected", msg);
                } catch (Exception exAdapter) {
                }
                showCipherPipe = (EditText) findViewById(R.id.showCipherPipe);

                selectCipherAlgo = parent.getSelectedItem().toString();
                cipher = CipherEnum.getEnum(selectCipherAlgo);
                String currentPipe = showCipherPipe.getText().toString();
                if (firstTimeInit) {
                    showCipherPipe.setText(cipher.getName() + ";");
                    firstTimeInit = false;
                } else
                    showCipherPipe.setText(currentPipe + cipher.getName() + ";");

                CipherEnum[] ciphers = getCipherEnums();
                cipherPipe = new CipherPipe(ciphers, 8, encodeType, zipType, keyHash, cmode);
                drawCipherPipe(cipherPipe);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                ;
            }
        });

        /*
        if (adapterZip == null)
            adapterZip = new ArrayAdapter<>(MainActivity.this, android.R.layout.simple_spinner_dropdown_item, zipStrings);
        adapterZip.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerZip.setAdapter(adapterZip);
        spinnerZip.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                try {
                    String msg = "adapterViewId: " + parent.getId() + " adapterView: " + parent.getAdapter().toString();
                    Log.d("onItemSelected", msg);
                } catch (Exception exAdapter) {
                }

                selectedZip = parent.getSelectedItem().toString();
                zipType = ZipType.getEnum(selectedZip);

                CipherEnum[] ciphers = getCipherEnums();
                cipherPipe = new CipherPipe(ciphers, 8, encodeType, zipType, keyHash, cmode);
                drawCipherPipe(cipherPipe);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                ;
            }
        });
        */

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        Intent intent = this.getIntent();
        String action = intent.getAction();
        String type = intent.getType();
        if (Intent.ACTION_SEND.equals(action) && type != null) {
            if ("text/plain".equals(type)) {

                String sharedText = intent.getStringExtra(Intent.EXTRA_TEXT);
                editTextSource.setText(sharedText);
            } else {
                byte[] sharedBytes = intent.getByteArrayExtra(Intent.EXTRA_CHOOSER_RESULT);

            }
        }
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        if (!super.onCreateOptionsMenu(menu)) {
            getMenuInflater().inflate(R.menu.menu_main, menu);
            myMenu = menu;
        }
        if (myMenu != null) {
            return true;
        }
        return false;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        super.onOptionsItemSelected(item);
        int mItemId = (item != null) ? item.getItemId() : -1;
        if (mItemId >= 0) {
            // reset now all checkboxes for language menu items
            // menuResetCheckboxes(myMenu);

            if (mItemId == R.id.action_ciphermode_CBC ||
                mItemId == R.id.action_ciphermode_CFB ||
                mItemId == R.id.action_ciphermode_ECB) {
                actionCipherMode(item);
            }
            if (mItemId == R.id.action_ziptype_none ||
                mItemId == R.id.action_ziptype_gzip||
                mItemId == R.id.action_ziptype_bzip ||
                mItemId == R.id.action_ziptype_zip) {
                actionCompressionMode(item);
            }
            if (mItemId == R.id.action_hash_bcrypt ||
                    mItemId == R.id.action_hash_blake2xs ||
                    mItemId == R.id.action_hash_cshake ||
                    mItemId == R.id.action_hash_dstu7564 ||
                    mItemId == R.id.action_hash_empty ||
                    mItemId == R.id.action_hash_hex ||
                    mItemId == R.id.action_hash_md5 ||
                    mItemId == R.id.action_hash_oct ||
                    mItemId == R.id.action_hash_openbsd ||
                    mItemId == R.id.action_hash_ripemd356 ||
                    mItemId == R.id.action_hash_scrypt ||
                    mItemId == R.id.action_hash_sha1 ||
                    mItemId == R.id.action_hash_sha256 ||
                    mItemId == R.id.action_hash_sha384 ||
                    mItemId == R.id.action_hash_sha512 ||
                    mItemId == R.id.action_hash_tuplehash ||
                    mItemId == R.id.action_hash_whirlpool)  {
                actionHashMode(item);
            }
            if (mItemId == R.id.action_encoding_none ||
                    mItemId == R.id.action_encoding_hex16 ||
                    mItemId == R.id.action_encoding_base16 ||
                    mItemId == R.id.action_encoding_hex32 ||
                    mItemId == R.id.action_encoding_base32 ||
                    mItemId == R.id.action_encoding_uu||
                    mItemId == R.id.action_encoding_xx ||
                    mItemId == R.id.action_encoding_hex64 ||
                    mItemId == R.id.action_encoding_base64 ||
                    mItemId == R.id.action_encoding_ascii85) {
                actionEncodingMode(item);
            }
            if (mItemId == R.id.action_action_setpipe)
                setCipherPipe();
            else if (mItemId == R.id.action_action_hashpipe)
                hashCipherPipe();
            else if (mItemId == R.id.action_action_randomtext)
                randomText();
            else if (mItemId == R.id.action_action_encrypt)
                performEncrypt();
            else if (mItemId == R.id.action_action_decrypt)
                performDecrypt();
            else if (mItemId == R.id.action_action_restart)
                appRestart();

            if (mItemId == R.id.action_exit) {
                appExit();
                return true;
            }
            if (mItemId == R.id.action_exit) {
                appExit();
                return true;
            }

            if (mItemId == R.id.action_about) {
                startAboutActivity();
                return true;
            }
            if (mItemId == R.id.action_help) {
                openUrl(getString(R.string.help_url));
                return true;
            }
        }

        return super.onOptionsItemSelected(item);
    }


    protected void actionCipherMode(MenuItem item) {
        int mItemId = (item != null) ? item.getItemId() : -1;

        if (myMenu == null) {
            int menuId = getApplicationContext().getResources().getIdentifier(
                    "menu_main",
                    "menu",
                    getApplicationContext().getPackageName());
            getMenuInflater().inflate(menuId, myMenu);
        }
        MenuItem mi = myMenu.findItem(R.id.action_ciphermode_CBC);
        if (mi != null && mi.isChecked())
            mi.setChecked(false);
        mi = myMenu.findItem(R.id.action_ciphermode_CFB);
        if (mi != null && mi.isChecked())
            mi.setChecked(false);
        mi = myMenu.findItem(R.id.action_ciphermode_ECB);
        if (mi != null && mi.isChecked())
            mi.setChecked(false);

        if (item != null)
            item.setChecked(true);

        if (mItemId == R.id.action_ciphermode_CBC)
            cmode = CipherMode2.CBC;
        else if (mItemId == R.id.action_ciphermode_CFB)
            cmode = CipherMode2.CFB;
        else
            cmode = CipherMode2.ECB;

    }

    public void actionHashMode(MenuItem item) {
        int mItemId = (item != null) ? item.getItemId() : -1;

        if (myMenu == null) {
            int menuId = getApplicationContext().getResources().getIdentifier(
                    "menu_main",
                    "menu",
                    getApplicationContext().getPackageName());
            getMenuInflater().inflate(menuId, myMenu);
        }

        int[] rIds = { R.id.action_hash_bcrypt,
                R.id.action_hash_blake2xs, R.id.action_hash_cshake,
                R.id.action_hash_dstu7564, R.id.action_hash_empty,
                R.id.action_hash_hex, R.id.action_hash_md5,
                R.id.action_hash_oct, R.id.action_hash_openbsd,
                R.id.action_hash_ripemd356, R.id.action_hash_scrypt,
                R.id.action_hash_sha1,  R.id.action_hash_sha256,
                R.id.action_hash_sha384, R.id.action_hash_sha512,
                R.id.action_hash_tuplehash, R.id.action_hash_whirlpool };
        for (int rId : rIds) {
            MenuItem mi = myMenu.findItem(rId);
            if (mi != null && mi.isChecked())
                mi.setChecked(false);
        }

        if (item != null)
            item.setChecked(true);

        if (mItemId == R.id.action_hash_bcrypt)
            keyHash = KeyHash.BCrypt;
        else if (mItemId == R.id.action_hash_blake2xs)
            keyHash = KeyHash.Blake2xs;
        else if (mItemId == R.id.action_hash_cshake)
            keyHash = KeyHash.CShake;
        else if (mItemId == R.id.action_hash_dstu7564)
            keyHash = KeyHash.Dstu7564;
        else if (mItemId == R.id.action_hash_hex)
            keyHash = KeyHash.Hex;
        else if (mItemId == R.id.action_hash_md5)
            keyHash = KeyHash.MD5;
        else if (mItemId == R.id.action_hash_oct)
                keyHash = KeyHash.Oct;
        else if (mItemId == R.id.action_hash_openbsd)
            keyHash = KeyHash.OpenBSDCrypt;
        else if (mItemId == R.id.action_hash_ripemd356)
            keyHash = KeyHash.RipeMD256;
        else if (mItemId == R.id.action_hash_scrypt)
                keyHash = KeyHash.SCrypt;
        else if (mItemId == R.id.action_hash_sha1)
            keyHash = KeyHash.Sha1;
        else if (mItemId == R.id.action_hash_sha256)
                keyHash = KeyHash.Sha256;
        else if (mItemId == R.id.action_hash_sha384)
            keyHash = KeyHash.Sha384;
        else if (mItemId == R.id.action_hash_sha512)
            keyHash = KeyHash.Sha512;
        else if (mItemId == R.id.action_hash_tuplehash)
                keyHash = KeyHash.TupleHash;
        else if (mItemId == R.id.action_hash_whirlpool)
            keyHash = KeyHash.Whirlpool;
        else
            keyHash = KeyHash.Empty;

        hashKey(keyHash);
    }

    public void actionEncodingMode(MenuItem item) {
        int mItemId = (item != null) ? item.getItemId() : -1;

        int[] rEncIds = { R.id.action_encoding_none,
                R.id.action_encoding_hex16, R.id.action_encoding_base16,
                R.id.action_encoding_hex32, R.id.action_encoding_base32,
                R.id.action_encoding_uu, R.id.action_encoding_xx,
                R.id.action_encoding_hex64, R.id.action_encoding_base64,
                R.id.action_encoding_ascii85  };
        for (int rEncId : rEncIds) {
            MenuItem mi = myMenu.findItem(rEncId);
            if (mi != null && mi.isChecked())
                mi.setChecked(false);
        }

        if (item != null)
            item.setChecked(true);

        if (mItemId == R.id.action_encoding_none)
            encodeType = EncodeEnum.None;
        if (mItemId == R.id.action_encoding_hex16)
            encodeType = EncodeEnum.Hex16;
        else if (mItemId == R.id.action_encoding_base16)
            encodeType = EncodeEnum.Base16;
        if (mItemId == R.id.action_encoding_hex32)
            encodeType = EncodeEnum.Hex32;
        else if (mItemId == R.id.action_encoding_base32)
            encodeType = EncodeEnum.Base32;
        else if (mItemId == R.id.action_encoding_uu)
            encodeType = EncodeEnum.Uu;
        else if (mItemId == R.id.action_encoding_xx)
            encodeType = EncodeEnum.Xx;
        // if (mItemId == R.id.action_encoding_hex64)
        //  encodeType = EncodeEnum.Hex64;
        else if (mItemId == R.id.action_encoding_base64)
            encodeType = EncodeEnum.Base64;
        else if (mItemId == R.id.action_encoding_ascii85)
            encodeType = EncodeEnum.Ascii85;
        else
            encodeType = EncodeEnum.Base64;
    }

    public void actionCompressionMode(MenuItem item) {
        int mItemId = (item != null) ? item.getItemId() : -1;

        int[] rZipIds = { R.id.action_ziptype_none, R.id.action_ziptype_gzip,
                R.id.action_ziptype_bzip, R.id.action_ziptype_zip };
        for (int rZipId : rZipIds) {
            MenuItem mi = myMenu.findItem(rZipId);
            if (mi != null && mi.isChecked())
                mi.setChecked(false);
        }

        if (item != null)
            item.setChecked(true);

        if (mItemId == R.id.action_ziptype_gzip)
            zipType = ZipType.GZip;
        else if (mItemId == R.id.action_ziptype_bzip)
            zipType = ZipType.BZip2;
        else if (mItemId == R.id.action_ziptype_zip)
            zipType = ZipType.Zip;
        else
            zipType = ZipType.None;

    }

    public void hashKey(KeyHash kh) {
        if (editEncryptKey == null)
            editEncryptKey = (EditText) findViewById(R.id.editEncryptKey);
        String keyValue = "";
        try {
            keyValue = editEncryptKey.getText().toString();
        } catch (Exception exi) {
            keyValue = "zen@area23.at";
        }
        String hashed = "";
        try {
            hashed = kh.hash(keyValue);
            editKeyHash.setText(hashed);
        } catch (Exception exh) {
        }
    }

    public CipherEnum[] getCipherEnums() {
        String cipherPipeString = showCipherPipe.getText().toString();
        CipherEnum[] ciphers = new CipherEnum[0];
        if (cipherPipeString.length() > 0) {
            ciphers = CipherEnum.parsePipeText(cipherPipeString);
        }
        return ciphers;
    }

    public void drawCipherPipe(CipherPipe cPipe) {
        CipherEnum[] cipherEnums = cPipe.getInPipe();
        String pipeSting = "";
        for (int ci = 0; ci < cipherEnums.length; ci++)
            pipeSting = pipeSting + cipherEnums[ci].getName() + ";";
        showCipherPipe.setText(pipeSting);

        Bitmap bmp = cPipe.drawCipherPipe(getBaseContext());
        imgViewCipherPipe.setImageBitmap(bmp);
    }

    public void setCipherPipe() {
        String key = editEncryptKey.getText().toString();
        String hashed = keyHash.hash(key);
        editKeyHash.setText(hashed);

        cipherPipe = new CipherPipe(key, hashed, encodeType, zipType, keyHash, cmode);
        drawCipherPipe(cipherPipe);
    }
    
    public void hashCipherPipe() {
        String key = editEncryptKey.getText().toString();
        String hashed = keyHash.hash(key);
        editKeyHash.setText(hashed);

        cipherPipe = new CipherPipe(hashed, key, encodeType, zipType, keyHash, cmode);
        drawCipherPipe(cipherPipe);
    }


    /**
     * performEncrypt encrypt operation
     */
    public void performEncrypt() {
        String key = editEncryptKey.getText().toString();
        String hashed = keyHash.hash(key);
        editKeyHash.setText(hashed);

        CipherEnum[] ciphers = getCipherEnums();
        cipherPipe = new CipherPipe(ciphers, 8, encodeType, zipType, keyHash, cmode);

        String plain = editTextSource.getText().toString();
        Bitmap bmp = cipherPipe.drawCipherPipe(getBaseContext());
        imgViewCipherPipe.clearAnimation();
        imgViewCipherPipe.setImageBitmap(bmp);

        String pipeSting = cipherPipe.getPipeString();
        showMsg(String.format("pipe[%s] encrypt with key=%s, hash=%s, \nencode=%s keyHash=%s, zip=%s",
                pipeSting, key, hashed, encodeType.getName(), keyHash.getName(), zipType.getName()), 4, false);

        String encrypted = encryptString(plain, key, hashed, cipherPipe);
        showTextDestination.setText(encrypted);
    }

    /**
     * performDecrypt decrypt operation
     */
    public void performDecrypt() {
        String key = editEncryptKey.getText().toString();
        String hashed = keyHash.hash(key);
        editKeyHash.setText(hashed);

        CipherEnum[] ciphers = getCipherEnums();
        cipherPipe = new CipherPipe(ciphers, 8, encodeType, zipType, keyHash, cmode);

        String encrypted = editTextSource.getText().toString();
        CipherEnum[] cipherEnums = cipherPipe.getOutPipe();

        String pipeSting = cipherPipe.getPipeString();
        showMsg(String.format("pipe[%s] decrypt with key=%s, hash=%s, \nencode=%s keyHash=%s, zip=%s",
                pipeSting, key, hashed, encodeType.getName(), keyHash.getName(), zipType.getName()), 4, true);

        Bitmap bmp = cipherPipe.drawDecryptCipherPipe(getBaseContext());
        imgViewCipherPipe.setImageBitmap(bmp);

        String decrypted = decryptedString(encrypted, key, hashed, cipherPipe);
        showTextDestination.setText(decrypted);
    }

    /**
     * appRestart restart app
     */
    @Override
    public void appRestart() {
        editEncryptKey.setText("");
        editKeyHash.setText("");
        showCipherPipe.setText("");
        editTextSource.setText("");
        showTextDestination.setText("");
        Bitmap imgPipeDefault = BitmapFactory.decodeResource(getResources(), R.drawable.cipherpipeblank);
        imgViewCipherPipe.setImageBitmap(imgPipeDefault);
    }


    /**
     * randomText
     */
    public void randomText() {
        String fortune = eu.cqrxs.util.Fortune.getFortune();
        editTextSource.setText(fortune);
    }




}
