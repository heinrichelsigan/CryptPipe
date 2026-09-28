/**
 * @author           <a href="mailto:heinrich.elsigan@cqrxs.eu">Heinrich Elsigan</a>
 * @version          V 2.26.428
 * @since            API 27 Oreo 8.1
 *
 * eu.cqrxs.zip.ZipType
 * Coded 2021-2033 by <a href="mailto:he@area23.at">Heinrich Elsigan</a>
 * <a href="https://heinrichelsigan.area23.at">heinrichelsigan.area23.at</a>
 */

package eu.cqrxs.zip;

import eu.cqrxs.util.NotImplementedError;
import eu.cqrxs.zip.GZ;
import eu.cqrxs.zip.ZipType;
import java.io.IOException;
import java.io.Serializable;
import java.lang.String;
import java.util.*;

/**
 * ZipType represents the enumerator for all Encoding to ascii algorithms
 */
public enum ZipType /* implements Serializable */ {
    None(0x00),
	Zip(0x10),
	GZip(0x20),
	BZip2(0x30),
	Z7(0x40);
	

    /**
     * NOTE: Enum constructor must have private or package scope. You can not use the public access modifier.
     */
    ZipType(int value) {
        this.value = value;
    }

    private final int value;

    /**
     * getValue
     * @return (@link int) value
     */
    public int getValue() { return value; }


	/**
	 * zip compresses bytes
	 * @param plainBytes plain bytes
	 * @return compressed bytes
	 * @throws IOException
	 */
    public byte[] zip(byte[] plainBytes) throws IOException {
        return switch (this) {
            case GZip -> GZ.gzip(plainBytes);
            case Zip -> WZ.zip(plainBytes);
            case BZip2 -> BZ2.bzip2(plainBytes);
            case None -> plainBytes;
            default -> throw new NotImplementedError("unzipping not implemented");
        };
    }

	/**
	 * unzip unzip compressed bytes
	 * @param zippedBytes compressed bytes
	 * @return decompressed bytes
	 * @throws IOException
	 */
    public byte[] unzip(byte[] zippedBytes) throws IOException {
        return switch (this) {
            case GZip -> GZ.gunzip(zippedBytes);
            case Zip -> WZ.unzip(zippedBytes);
            case BZip2 -> BZ2.bunzip2(zippedBytes);
            case None -> zippedBytes;
            default -> throw new NotImplementedError("unzipping not implemented");
        };
    }

    /**
     * getName
     * @return name of enum
     */
    public String getName() {
		int xval = getValue();
        return switch (xval) {
            case 0x00 -> "None";
            case 0x10 -> "Zip";
            case 0x20 -> "GZip";
            case 0x30 -> "BZip2";
            case 0x40 -> "Z7";
            default -> "None";
        };
    }


	/**
	 * getNames
	 * @return string array of zip enum names
	 */
	public static String[] getNames() {
		int cnt = 0;
		List<String> zipTypeList = new ArrayList<>();
		for (ZipType zipType : ZipType.values())  {
			zipTypeList.add(zipType.getName());
			cnt++;
		}
		
		return zipTypeList.toArray(new String[cnt]);		
    }

	/**
	 * getZipTypeFromString
	 * @param zipFileExtension file extension
	 * @return {@link ZipType}
	 */
	public static ZipType getZipTypeFromString(String zipFileExtension) {
		if (zipFileExtension != null && !zipFileExtension.isEmpty()) {
			switch (zipFileExtension.toLowerCase(Locale.getDefault())) {
				case "zip":
					return ZipType.Zip;
					
				case "gz":
				case "gzip":
					return ZipType.GZip;
					
				case "bz":
				case "bz2":
				case "bzip":
				case "bzip2":
					return ZipType.BZip2;

				case "7z":
				case "z7":
				case "7zip":
					return ZipType.Z7;
					
				default:										
					break;
			}
		}
		return ZipType.None;
	}


	/**
	 * getZipTypes
	 * @return {@link Set<ZipType>} Set pf ZipType
	 */
	public static Set<ZipType> getZipTypes() {
		Set<ZipType> allElementsInZipType = EnumSet.allOf(ZipType.class);
		return allElementsInZipType;
	}

	/**
	 * getZipTypeExtension
	 * @param zipType {@link ZipType}
	 * @return {@link String} zip type extension
	 */
	 public static String getZipTypeExtension(ZipType zipType) {
         return switch (zipType.getValue()) {
             case 0x10 -> ".zip";
             case 0x20 -> ".gz";
             case 0x30 -> ".bz2";
             case 0x40 -> ".7z";
             default -> "";
         };
     }

    /**
     * getEnum
     * @param eName zip type name
     * @return the enum {@link ZipType}
     */
    public static ZipType getEnum(String eName) {
        for (ZipType zipType : ZipType.values()) {
            if (zipType.getName() == eName)
                return zipType;
        }
        return ZipType.None;
    }

}
