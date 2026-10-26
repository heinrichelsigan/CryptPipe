package eu.cqrxs.cipherpipe;

import org.junit.Test;

import static org.junit.Assert.*;

import java.io.IOException;

import eu.cqrxs.crypt.encoding.EncodeEnum;

/**
 * EncodingUnitTest local unit test,
 * which will verify if all encoding and decodings are correct!
 *
 * @see <a href="http://d.android.com/tools/testing">Testing documentation</a>
 */
public class EncodingUnitTest {
    /*
     * endecoding_isCorrect ensures, that mime base64 encode and decode works
     */
    @Test
    public void endecoding_isCorrect() {

        String plainText = "http://d.android.com/tools/testing";
        EncodeEnum[] encTypes = EncodeEnum.getEncodingTypes().toArray(EncodeEnum[]::new);
        EncodeEnum encType = EncodeEnum.Base64;
        String decoded = "", encoded;

        for (int i = 0; i < encTypes.length; i++) {
            if ((encType = encTypes[i]) == EncodeEnum.None)
                continue;
            ;

            encoded = encType.encode(plainText);
            try {
                decoded = encType.decode(encoded);
            } catch (IOException ioEx) {
            }

            assertEquals(plainText, decoded);
        }
    }
}