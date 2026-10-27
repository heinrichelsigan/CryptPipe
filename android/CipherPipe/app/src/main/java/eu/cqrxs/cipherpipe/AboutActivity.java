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
import android.net.Uri;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import java.util.SortedMap;

import eu.cqrxs.crypt.cipher.CipherEnum;
import eu.cqrxs.crypt.cipher.CipherMode2;
import eu.cqrxs.crypt.cipher.CipherPipe;
import eu.cqrxs.crypt.encoding.EncodeEnum;
import eu.cqrxs.crypt.hash.KeyHash;
import eu.cqrxs.zip.ZipType;


/**
 * AboutActivity class implements help text view.
 *
 * @see <a href="https://github.com/heinrichelsigan/schnapslet/wiki</a>
 */
public class AboutActivity extends BaseActivity {

    Button backButton, learnMoreButton;
    TextView helpTextView, builtWithTextView;
    LinearLayout linearRoot;
    ViewGroup rootViewGroup;

    /**
     * Override onCreate
     * @param savedInstanceState saved application state of current instance
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_about);

        linearRoot = (LinearLayout) findViewById(R.id.linearRoot);
        linearRoot.setDrawingCacheEnabled(true);
        rootViewGroup = (ViewGroup) linearRoot;

        backButton = (Button) findViewById(R.id.backButton);
        learnMoreButton =  (Button) findViewById(R.id.learnMoreButton);
        helpTextView = (TextView) findViewById(R.id.helpTextView);
        builtWithTextView = (TextView) findViewById(R.id.builtWithTextView);

        addListenerOnClickables();
    }

    /**
     * onCreateOptionsMenu
     * @param menu options menu
     * @return always (boolean) true for overridden member function
     */
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        myMenu = menu;
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        if (id == R.id.action_exit) {
            finish();
            appExit();
            return true;
        }
        if (id == R.id.action_restart) {
            finish();
            return true;
        }
        if (id == R.id.action_help) {
            openUrl(getString(R.string.help_url));
            return true;
        }

        return super.onOptionsItemSelected(item);
    }


    /**
     * add listeners on all clickables
     */
    public void addListenerOnClickables() {
        backButton = (Button) findViewById(R.id.backButton);
        backButton.setOnClickListener(arg0 -> backButton_Clicked());

        learnMoreButton = (Button) findViewById(R.id.learnMoreButton);
        learnMoreButton.setOnClickListener(arg0 -> learnMoreButton_Clicked());

        helpTextView = (TextView) findViewById(R.id.helpTextView);
        helpTextView.setText(getString(R.string.help_text));

    }


    /**
     * backButton_Clicked finish about activity
     */
    public void backButton_Clicked() {
        // finish activity
        finish();
    }


    /**
     * learnMoreButton_Clicked_Clicked
     */
    public void learnMoreButton_Clicked() {

        Intent intent = new Intent();
        intent.setAction(Intent.ACTION_VIEW);
        String github = getString(R.string.app_idea_url);
        intent.setData(android.net.Uri.parse(github));
        startActivity(intent);

    }

}