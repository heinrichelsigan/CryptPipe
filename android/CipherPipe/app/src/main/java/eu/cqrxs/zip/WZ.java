/**
 * @author           <a href="mailto:heinrich.elsigan@cqrxs.eu">Heinrich Elsigan</a>
 * @version          V 2.26.428
 * @since            API 27 Oreo 8.1
 *
 * eu.cqrxs.zip.WZ
 * Coded 2021-2033 by <a href="mailto:he@area23.at">Heinrich Elsigan</a>
 * <a href="https://heinrichelsigan.area23.at">heinrichelsigan.area23.at</a>
 */

package eu.cqrxs.zip;

import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.zip.DeflaterOutputStream;
import java.util.zip.InflaterInputStream;
import java.util.zip.ZipOutputStream;
import java.util.zip.ZipInputStream;

public class WZ {

    // const int BUFSZE = 1024;
    ZipInputStream in = null;
    OutputStream out = null;


    /**
     * Zip directly
     * @param bytes byte[] to zip
     * @return gzipped byte[]
     */
    public static byte[] zip(final byte[] bytes) throws IOException {
        if (bytes == null || bytes.length == 0) {
            return new byte[0];
        }
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream(byteArrayOutputStream);
        deflaterOutputStream.write(bytes);
        deflaterOutputStream.close();
        byte[] compressedBytes = byteArrayOutputStream.toByteArray();
        byteArrayOutputStream.close();

        return compressedBytes;
        // final ByteArrayOutputStream out = new ByteArrayOutputStream();
        // try (final OutputStream zipOutStream = new ZipOutputStream(out)) {
        //     zipOutStream.write(bytes);
        // }
        // return out.toByteArray();
    }

    /**
     * zips zip's a string
     * @param str String to zip
     * @return zipped String as byte[]
     */
    public static byte[] zips(final String str) {
        if ((str == null) || (str.length() == 0)) {
            throw new IllegalArgumentException("Cannot zip null or empty string");
        }

        try (ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream()) {

            try (ZipOutputStream  zipOutputStream = new ZipOutputStream(byteArrayOutputStream)) {
                zipOutputStream.write(str.getBytes(StandardCharsets.UTF_8));
            }
            return byteArrayOutputStream.toByteArray();

        } catch (IOException e) {
            throw new RuntimeException("Failed to zip content", e);
        }
    }


    /**
     * unzip unzips a byte array
     * @param zipBytes gzipped byte[]
     * @return unzipped plain byte[]
     */
    public static byte[] unzip(final byte[] zipBytes) throws IOException {
        if (zipBytes == null || zipBytes.length == 0) {
            throw new IllegalArgumentException("Cannot unzip null or empty byte array");
        }
        ByteArrayInputStream byteArrayInputStream = new ByteArrayInputStream(zipBytes);
        InflaterInputStream inflaterInputStream = new InflaterInputStream(byteArrayInputStream);
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        int read;
        while ((read = inflaterInputStream.read()) != -1) {
            byteArrayOutputStream.write(read);
        }
        inflaterInputStream.close();
        byteArrayInputStream.close();
        byteArrayOutputStream.close();

        return byteArrayOutputStream.toByteArray();
        /*
        try (final ZipInputStream gunzipStream = new ZipInputStream(new ByteArrayInputStream(zipBytes))) {
            final ByteArrayOutputStream byteArrayOutStream = new ByteArrayOutputStream();
            final byte[] data = new byte[16384];
            int nRead;
            while ((nRead = gunzipStream.read(data)) != -1) {
                byteArrayOutStream.write(data, 0, nRead);
            }
            return byteArrayOutStream.toByteArray();
        }
         */
    }

    /**
     * unzips a zipped byte[] to a plain text String
     * @param compressed gzipped byte[]
     * @return plain text String
     */
    public static String unzips(final byte[] compressed) {
        if ((compressed == null) || (compressed.length == 0)) {
            throw new IllegalArgumentException("Cannot unzip null or empty bytes");
        }
        if (!isZipped(compressed)) {
            return new String(compressed);
        }

        try (ByteArrayInputStream byteArrayInputStream = new ByteArrayInputStream(compressed)) {
            try (ZipInputStream zipInputStream = new ZipInputStream(byteArrayInputStream)) {
                try (InputStreamReader inputStreamReader = new InputStreamReader(zipInputStream, StandardCharsets.UTF_8)) {
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


    public static boolean isZipped(final byte[] compressed) {
        return (compressed[0] == ((byte)80) &&
                compressed[1] == ((byte)75) &&
                compressed[2] == ((byte)3) &&
                compressed[4] == ((byte)4));
    }

}
