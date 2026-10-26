/**
 * @author           <a href="mailto:heinrich.elsigan@cqrxs.eu">Heinrich Elsigan</a>
 * @version          V 2.26.428
 * @since            API 27 Oreo 8.1
 *
 * eu.cqrxs.zip.BZip2
 * Coded 2021-2033 by <a href="mailto:he@area23.at">Heinrich Elsigan</a>
 * <a href="https://heinrichelsigan.area23.at">heinrichelsigan.area23.at</a>
 */

package eu.cqrxs.zip;

import java.io.*;
import java.nio.charset.StandardCharsets;
import org.apache.commons.io.input.CloseShieldInputStream;
import org.apache.commons.compress.compressors.bzip2.BZip2CompressorInputStream;
import org.apache.commons.compress.compressors.bzip2.BZip2CompressorOutputStream;

public class BZ2 {

    // const int BUFSZE = 1024;
    BZip2CompressorInputStream in = null;
    OutputStream out = null;


    /**
     * bzip2 directly
     * @param bytes byte[] to zip
     * @return bz2 zipped byte[]
     */
    public static byte[] bzip2(final byte[] bytes) throws IOException {
        if (bytes == null || bytes.length == 0) {
            return new byte[0];
        }
        final ByteArrayOutputStream out = new ByteArrayOutputStream();
        try (final OutputStream bzip2 = new BZip2CompressorOutputStream(out)) {
            bzip2.write(bytes);
        }
        return out.toByteArray();
    }

    /**
     * bzip2s bzip2's a string
     * @param str String to zip
     * @return zipped String as byte[]
     */
    public static byte[] bzip2s(final String str) {
        if ((str == null) || (str.length() == 0)) {
            throw new IllegalArgumentException("Cannot zip null or empty string");
        }

        try (ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream()) {

            try (BZip2CompressorOutputStream bzip2OutputStream = new BZip2CompressorOutputStream(byteArrayOutputStream)) {
                bzip2OutputStream.write(str.getBytes(StandardCharsets.UTF_8));
            }

            return byteArrayOutputStream.toByteArray();

        } catch (IOException e) {
            throw new RuntimeException("Failed to zip content", e);
        }
    }


    /**
     * bunzip2 hunzip2's a byte array (same as bzip2 -d )
     * @param bz2Bytes gzipped byte[]
     * @return unzipped plain byte[]
     */
    public static byte[] bunzip2(final byte[] bz2Bytes) throws IOException {
        if (bz2Bytes == null || bz2Bytes.length == 0) {
            throw new IllegalArgumentException("Cannot unzip null or empty byte array");
        }
        try (final BZip2CompressorInputStream bunzip2Stream = new BZip2CompressorInputStream(new ByteArrayInputStream(bz2Bytes))) {
            final ByteArrayOutputStream byteArrayOutStream = new ByteArrayOutputStream();
            final byte[] data = new byte[16384];
            int nRead;
            while ((nRead = bunzip2Stream.read(data)) != -1) {
                byteArrayOutStream.write(data, 0, nRead);
            }
            return byteArrayOutStream.toByteArray();
        }
    }

    /**
     * bunzip2s a bz2 zipped byte[] to a plain text String
     * @param compressed gzipped byte[]
     * @return plain text String
     */
    public static String bunzip2s(final byte[] compressed) {
        if ((compressed == null) || (compressed.length == 0)) {
            throw new IllegalArgumentException("Cannot unzip null or empty bytes");
        }
        if (!isBZipped(compressed)) {
            return new String(compressed);
        }

        try (ByteArrayInputStream byteArrayInputStream = new ByteArrayInputStream(compressed)) {
            try (BZip2CompressorInputStream bunzip2Stream = new BZip2CompressorInputStream(byteArrayInputStream)) {
                try (InputStreamReader inputStreamReader = new InputStreamReader(bunzip2Stream, StandardCharsets.UTF_8)) {
                    try (BufferedReader bufferedReader = new BufferedReader(inputStreamReader)) {
                        StringBuilder output = new StringBuilder();
                        String line;
                        while ((line = bufferedReader.readLine()) != null) {
                            output.append(line);
                        }
                        return output.toString();
                    }
                }
            }
        } catch (IOException e) {
            throw new RuntimeException("Failed to unzip content", e);
        }
    }


    public static boolean isBZipped(final byte[] compressed) {
        return (compressed[0] == (byte) (0x42) &&
                compressed[1] == (byte) (0x5A) &&
                compressed[2] == (byte) (0x68));
    }

}
