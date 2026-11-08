/**
 * @author           <a href="mailto:heinrich.elsigan@area23.at">Heinrich Elsigan</a>
 * @version          V 1.0.1
 * @since            API 27 Oreo 8.1
 *
 * Coded 2021-2025 by
 * <a href="mailto:he@area23.at">Heinrich.Elsigan</a><a href="https://heinrichelsigan.area23.at">heinrichelsigan.area23.at</a>
.*/

package eu.cqrxs.cipherpipe;

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

import androidx.activity.EdgeToEdge;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import eu.cqrxs.crypt.cipher.CipherEnum;
import eu.cqrxs.crypt.cipher.CipherMode2;
import eu.cqrxs.crypt.cipher.SecureCipherPipe;
import eu.cqrxs.crypt.encoding.EncodeEnum;
import eu.cqrxs.crypt.hash.KeyHash;
import eu.cqrxs.zip.ZipType;


public class SimpleActivity extends BaseActivity{

    Intent requestFileIntent;;
    Button btnEncrypt, btnDecrypt, btnSetPipe, btnHashPipe, btnRandText, btnReset;
    ImageButton imgBtnKey;
    ImageView imgViewCipherPipe;
    EditText editEncryptKey, showCipherPipe, editTextSource, showTextDestination;
    Spinner spinnerAlgos; // spinnerHash, spinnerZip,  spinnerEncode;
    ArrayAdapter adapterAlgos = null; //  adapterHash = null, adapterEndoding = null, adapterZip = null,

    SecureCipherPipe seCipherPipe;
    CipherEnum cipher;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_simple);
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
        btnSetPipe = (Button) findViewById(R.id.btnSetPipe);
        btnHashPipe = (Button) findViewById(R.id.btnHashPipe);
        btnEncrypt = (Button) findViewById(R.id.btnEncrypt);
        btnDecrypt = (Button) findViewById(R.id.btnDecrypt);
        btnRandText = (Button) findViewById(R.id.btnRandText);
        btnReset = (Button) findViewById(R.id.btnReset);
        editEncryptKey = (EditText) findViewById(R.id.editEncryptKey);
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

        if (adapterAlgos == null)
            adapterAlgos = new ArrayAdapter<>(SimpleActivity.this, android.R.layout.simple_spinner_dropdown_item, algoStrings);
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
                seCipherPipe = new SecureCipherPipe(ciphers, 8, cmode);
                drawCipherPipe(seCipherPipe);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                ;
            }
        });

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
        myMenu = menu;
        int menuId = -1;

        try {
            menuId = getApplicationContext().getResources().getIdentifier(
                    "menu_simple",
                    "menu",
                    getApplicationContext().getPackageName());

            if (menuId >= 0) {
                getMenuInflater().inflate(menuId, menu);
                myMenu = menu;

                return true;
            }
        } catch (Exception menuEx) {
            showException(menuEx);
        }

        getMenuInflater().inflate(R.menu.menu_simple, menu);
        myMenu = menu;

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
            if (mItemId == R.id.action_complex) {
                startComplexActivity();
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
                    "menu_simple",
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

    public void drawCipherPipe(SecureCipherPipe seCPipe) {
        CipherEnum[] cipherEnums = seCPipe.getInPipe();
        String pipeSting = "";
        for (int ci = 0; ci < cipherEnums.length; ci++)
            pipeSting = pipeSting + cipherEnums[ci].getName() + ";";
        showCipherPipe.setText(pipeSting);

        Bitmap bmp = seCPipe.drawCipherPipe(getBaseContext());
        imgViewCipherPipe.setImageBitmap(bmp);
    }

    public void setCipherPipe() {
        String key = editEncryptKey.getText().toString();
        String hashed = keyHash.hash(key);

        seCipherPipe = new SecureCipherPipe(key, cmode);
        drawCipherPipe(seCipherPipe);
    }
    
    public void hashCipherPipe() {
        String key = editEncryptKey.getText().toString();
        String hashed = KeyHash.Whirlpool.hash(KeyHash.SCrypt.hash(key));

        seCipherPipe = new SecureCipherPipe(hashed, cmode);
        drawCipherPipe(seCipherPipe);
    }


    /**
     * performEncrypt encrypt operation
     */
    public void performEncrypt() {
        String key = editEncryptKey.getText().toString();
        String hashed = keyHash.hash(key);

        CipherEnum[] ciphers = getCipherEnums();
        seCipherPipe = new SecureCipherPipe(ciphers, 8, cmode);

        String plain = editTextSource.getText().toString();
        Bitmap bmp = seCipherPipe.drawCipherPipe(getBaseContext());
        imgViewCipherPipe.clearAnimation();
        imgViewCipherPipe.setImageBitmap(bmp);

        String pipeSting = seCipherPipe.getPipeString();
        showMsg(String.format("pipe[%s] encrypt with key=%s, cmode=%s",
                pipeSting, key, cmode), 4, false);

        String encrypted = "";
        try {
            encrypted = seCipherPipe.encrpytTextGoRounds(plain, key, cmode);
        } catch (Exception exEncrypt) {
            showException(exEncrypt);
        }
        showTextDestination.setText(encrypted);
    }

    /**
     * performDecrypt decrypt operation
     */
    public void performDecrypt() {
        String key = editEncryptKey.getText().toString();
        String hashed = keyHash.hash(key);

        CipherEnum[] ciphers = getCipherEnums();
        seCipherPipe = new SecureCipherPipe(ciphers, 8, cmode);

        String encrypted = editTextSource.getText().toString();
        CipherEnum[] cipherEnums = seCipherPipe.getOutPipe();

        String pipeSting = seCipherPipe.getPipeString();
        showMsg(String.format("pipe[%s] decrypt with key=%s, cmode=%s",
                pipeSting, key, cmode), 4, true);

        Bitmap bmp = seCipherPipe.drawDecryptCipherPipe(getBaseContext());
        imgViewCipherPipe.setImageBitmap(bmp);

        String decrypted = "";
        try {
            decrypted =  seCipherPipe.decryptTextRoundsGo(encrypted, key, cmode);
        } catch (Exception exDecrypt) {
            showException(exDecrypt);
        }
        showTextDestination.setText(decrypted);
    }

    /**
     * appRestart restart app
     */
    @Override
    public void appRestart() {
        editEncryptKey.setText("");
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
