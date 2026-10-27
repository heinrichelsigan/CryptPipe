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
import android.net.Uri;
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

import java.util.Locale;
import java.util.SortedMap;

import eu.cqrxs.crypt.cipher.CipherEnum;
import eu.cqrxs.crypt.cipher.CipherMode2;
import eu.cqrxs.crypt.cipher.CipherPipe;
import eu.cqrxs.crypt.encoding.EncodeEnum;
import eu.cqrxs.crypt.hash.KeyHash;
import eu.cqrxs.zip.ZipType;


public class BaseActivity extends AppCompatActivity {


    protected String[] hashStrings, encodingStrings, zipStrings, algoStrings;
    protected SortedMap<String, String> sortedAlgoMap, sortedHashMap, sortedEncodingMap;
    protected String selectedHash = "", selectEncodeType = "", selectZipType = "", selectCipherAlgo = "", selectedZip = "";
    protected KeyHash keyHash = KeyHash.Empty;
    protected EncodeEnum encodeType = EncodeEnum.Base64;
    protected ZipType zipType = ZipType.None;
    protected CipherMode2 cmode = CipherMode2.CFB;

    protected static boolean firstTimeInit = true, firstTimeInitEncodings = true;
    protected static int errNum = 0;


    protected android.view.View rootView = null;
    protected Menu myMenu;


    CipherEnum cipher;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        if (rootView == null)
            rootView = getWindow().getDecorView().getRootView();
        rootView.setDrawingCacheEnabled(false);
    }


    /**
     * onCreateOptionsMenu
     *
     * @param menu - the menu item, that has been selected
     * @return true, if menu successfully created, otherwise false
     */
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        myMenu = menu;
        int menuId = -1;

        try {
            menuId = getApplicationContext().getResources().getIdentifier(
                    "menu_main",
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

        return false;
    }

    /**
     * onOptionsItemSelected
     *
     * @param item - the menu item, that has been selected
     * @return false to allow normal menu processing to proceed, true to consume it here.
     */
    public boolean onOptionsItemSelected(MenuItem item) {

        int mItemId = (item != null) ? item.getItemId() : -1;
        if (mItemId >= 0) {
            // reset now all checkboxes for language menu items
            // menuResetCheckboxes(myMenu);

            if (mItemId == R.id.action_restart) {
                appRestart();
            }
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



    public void appRestart() {

    }


    /**
     * encryptString
     * @param plain original String to encrypt
     * @param key private encryption key
     * @param hashed hashed private key
     * @param pipe {@link CipherPipe}
     * @return encrypted String
     */
    public String encryptString(String plain, String key, String hashed, CipherPipe pipe) {

        String encrypted = "";
        try {
            encrypted = pipe.encrpytTextGoRounds(plain, key, hashed, encodeType, zipType, keyHash, cmode);
        } catch (Exception exEncrypt) {
            showException(exEncrypt);
        }
        return encrypted;
    }

    /**
     * decryptedString decrypts a String
     * @param encrypted String to decrypt
     * @param key private encryption key
     * @param hashed hashed private key
     * @param pipe {@link CipherPipe}
     * @return decrypted String
     */
    public String decryptedString(String encrypted, String key, String hashed, CipherPipe pipe) {

        String decrypted = "";
        try {
            decrypted =  pipe.decryptTextRoundsGo(encrypted, key, hashed, encodeType, zipType, keyHash, cmode);
        } catch (Exception exDecrypt) {
            showException(exDecrypt);
        }
        return decrypted;
    }


    public void showMsg(String text, int gravity, boolean showLong) {
        Toast toast = Toast.makeText(getBaseContext(), "Info Message", showLong ? Toast.LENGTH_LONG : Toast.LENGTH_SHORT);
        toast.setText(text);
        toast.setGravity(4, 40, 40);
        toast.setMargin(0.8F, 0.4F);
        toast.show();
    }

    public void showMessage(String msg) {
        showMsg(msg, 4, true);
    }

    /**
     * showError simple dummy error handler
     *
     * @param myErr       java.lang.Throwable
     * @param showMessage triggers, that a Toast Widget shows the current Error / Exception
     */
    public void showError(java.lang.Throwable myErr, boolean showMessage) {
        if (myErr != null) {
            synchronized (this) {
                ++errNum;
            }
            CharSequence text = "CRITICAL ERROR #" + errNum + " " + myErr.getMessage() + "\nMessage: " + myErr.getLocalizedMessage() + "\n";
            if (showMessage)
                showMessage(text.toString());
            myErr.printStackTrace();
        }
    }

    /**
     * showError simple dummy error handler
     *
     * @param myEx - tje exceütion, that has been thrown
     */
    public void showException(java.lang.Exception myEx) {
        showError(myEx, true);
    }

    //endregion



    /**
     * startAboutActivity
     * starts About Activity
     */
    public void startAboutActivity() {
        // try {
        //     Thread.currentThread().sleep(10);
        // } catch (Exception exInt) {
        //     errHandler(exInt);
        // }
        // tDbg.setText(R.string.help_text);
        Intent intent = new Intent(this, AboutActivity.class);
        startActivity(intent);
    }

    /**
     * openUrl opens a  defined url as ACTION_VIEW Intent
     * @param urlString url to open
     */
    public void openUrl(String urlString) {

        Uri openUri;
        String url2Open = (urlString != null && urlString.length() > 1) ?
                urlString : getString(R.string.help_url);
        try {
            openUri = android.net.Uri.parse(url2Open);
        } catch (Exception exUriParse) {
            exUriParse.printStackTrace();
            openUri = android.net.Uri.parse(getString(R.string.app_idea_url));
        }

        Intent intent = new Intent();
        intent.setAction(Intent.ACTION_VIEW);
        intent.setData(openUri);
        startActivity(intent);
    }

    /**
     * appExit() exit game
     */
    public void appExit() {
        finishAffinity();
        System.exit(0);
    }



}
