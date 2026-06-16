package eu.cqrxs.cipherpipe;

import org.junit.Test;

import static org.junit.Assert.*;

import eu.cqrxs.crypt.encoding.EncodeEnum;

/**
 * Example local unit test, which will execute on the development machine (host).
 *
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
public class ExampleUnitTest {
    /*
     * endecoding_isCorrect ensures, that mime base64 encode and decode works
     */
    @Test
    public void endecoding_isCorrect() {

        EncodeEnum encType = EncodeEnum.Base64;
        String plainText =  "http://d.android.com/tools/testing";
        String encoded = encType.encode(plainText);
        String decoded = "";
        try {
            decoded = encType.decode(encoded);
        } catch (java.io.IOException ioEx) {
        }

        assertEquals(plainText, decoded);
    }
}