/**
 *
 * @author           Heinrich Elsigan
 * @version          V 0.2
 * @since            JDK 8
 * Coded 2021-2025 by
 * <a href="mailto:he@area23.at">Heinrich.Elsigan</a><a href="https://heinrichelsigan.area23.at">heinrichelsigan.area23.at</a>
 */ 
package eu.cqrxs.gui;

import eu.cqrxs.crypt.cipher.CipherEnum;
import eu.cqrxs.crypt.cipher.CipherMode2;
import eu.cqrxs.crypt.encoding.EncodeEnum;
import eu.cqrxs.crypt.hash.KeyHash;
import eu.cqrxs.util.Constants;
import eu.cqrxs.util.DbgWriter;
import eu.cqrxs.zip.ZipType;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.io.File;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.nio.file.Files;
import java.nio.file.Path;
import java.time.Duration;

/**
 * class CqrJdFrame is main form for PermAgainCrypt in java
 */
public class CqrBaseFrame extends JFrame {

	protected static byte[] openFileBytes, saveFileBytes;
	/// at/net/res/img/crypt/file.png");/

	public KeyHash keyHash = KeyHash.Empty;
	public ZipType zipType = ZipType.None;
	public CipherEnum cipherEnum = CipherEnum.Aes;
	protected String cipherString, encodeString, openFileName, saveFileName, saveFileSuffix = "";
	protected EncodeEnum encodeType = EncodeEnum.Base64;
	protected CipherMode2 cmode2 = CipherMode2.ECB;

	protected eu.cqrxs.gui.CqrJDialog cqrJDialog;
	protected Font menuFont, cryptFont, monoSpaceFont, monoSpaced = new Font("Monospaced", Font.PLAIN, 10);
	protected static Color defaultMenuItemBg, selectionBg, backColor, bgColor;
	JLabel jLabel_infoMessage = new JLabel();

	/**
	 * main constructor for CqrBaseFrame
	 */
	public CqrBaseFrame() {

		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	}

	public void Init() {

		defaultMenuItemBg = UIManager.getColor("MenuItem.background");
		// Get the color when a menu item is selected/hovered
		selectionBg = UIManager.getColor("MenuItem.selectionBackground");
		monoSpaceFont = new Font(Font.MONOSPACED, Font.PLAIN, 11);
		cryptFont = new Font("Dialog", Font.PLAIN, 11);
		bgColor = Color.LIGHT_GRAY;
		backColor = Color.decode("#acdccc");
		// setBackground(backColor);



		jLabel_infoMessage = new JLabel();
		jLabel_infoMessage.setFont(cryptFont);
		// jLabel_infoMessage.setBounds(512, 244, 468, 25);
		jLabel_infoMessage.setText("");
		jLabel_infoMessage.setBackground(Color.YELLOW);
	}

	protected String MakeWebRequest() {

		HttpClient client = HttpClient.newBuilder()
				.connectTimeout(Duration.ofSeconds(10))
				.build();

		String area23R = "https://cqrxs.eu/net/R.aspx";
		URI uri23 = URI.create(area23R);

		//.version(HttpClient.Version.HTTP_2)

		// HttpClient client = new HttpClient();
		// .uri(URI.create("https://area23.at/net/R.aspx"))
		// .followRedirects(Redirect.NORMAL)
		// .version(Version.HTTP_1_1)
		// .connectTimeout(Duration.ofSeconds(20))
		// .authenticator(Authenticator.getDefault())
		// .build();

		// HttpRequest.newBuilder(new URI("https://area23.at/net/R.aspx"))

		HttpRequest request = HttpRequest.newBuilder()
				.uri(URI.create(area23R))
				.GET()
				.build();

		HttpResponse<String> response;

		StringBuilder sb = new StringBuilder();

		try {
			response = client.send(request, HttpResponse.BodyHandlers.ofString());

			sb.append("GET " + area23R + " status = " + response.statusCode() + "\n");
			sb.append("Headers: " + response.headers().allValues("content-type"));
			sb.append("Body: \n " + response.body());
		} catch (Exception ioEx) {
			DbgWriter.msgex(ioEx, true);
			sb.append("Exception: " + ioEx + "\n");
		}

		return sb.toString();
	}

	/**
	 * about_action
	 * shows about dialog
	 * @param event {@link ActionEvent)
	 */
	protected void about_action(ActionEvent event, CqrBaseFrame baseFrame) {
		try {
			cqrJDialog = new CqrJDialog();
			cqrJDialog.showDialog(baseFrame);
		} catch (Exception exIO) {
			DbgWriter.msgex(exIO, true);
		}
	}

	/**
	 * help_action
	 * shows online help im browser
	 * @param event {@link ActionEvent}}
	 */
	@SuppressWarnings("deprecation")
	protected void help_action(ActionEvent event) {

		String os = System.getProperty("os.name").toLowerCase();
		Runtime rt = Runtime.getRuntime();
		String url = "https://io.cqrxs.eu/help";
		boolean success = false;

		if (Desktop.isDesktopSupported()) {
			Desktop desktop = Desktop.getDesktop();
			try {
				desktop.browse(new URI(url));
				success = true;
			} catch (URISyntaxException e) {
				DbgWriter.msgex(e, true);
			} catch (Exception ex) {
				DbgWriter.msgex(ex, true);
			}
		}
		if (!success) {
			try {
				if (os.contains("win"))
					rt.exec("rundll32 url.dll,FileProtocolHandler " + url);
				else if (os.contains("mac"))
					rt.exec("open " + url);
				else // if (os.indexOf("x") >=0 || os.indexOf("bsd") >= 0)
					rt.exec("xdg-open "  + url);
			} catch (Exception rtException) {
				DbgWriter.msgex(rtException, true);
			}
		}
	}


	/**
	 * exit_action
	 * exits the application
	 * @param event {@link ActionEvent)
	 */
	protected void exit_action(ActionEvent event) {
		// We don't log exit events ;)
		System.exit(0);
	}


	protected void dbgMsg(String s, int level, boolean ignoreDbg) {
		if (s != null && s.length() > 0 && (Constants.DEBUG || ignoreDbg)) {
			DbgWriter.dbgmsg(s, level, ignoreDbg);
		}
	}



	/**
	 * saveFileToTemp saves an binary content to file in temp directory
	 * @param fileName {@link String}
	 * @param fileBytes {@link byte[]}
	 * @return file name of savaed file without path
	 */
	protected String saveFileToTemp(String fileName, byte[] fileBytes) {
		String temp = System.getenv("TEMP");
		if (temp.isEmpty())
			temp = System.getenv("TMP");
		if (temp.isEmpty())
			temp = System.getenv("temp");
		if (temp.isEmpty())
			temp = ".";

		String dirSep = (File.pathSeparatorChar == ':') ? "/" : "\\";
		String destName = fileName;
		int idx = 0;
		while ((idx = destName.indexOf(dirSep)) > -1) {
			int len = destName.length();
			destName = destName.substring(idx + 1, len -1);
		}

		String desPath = temp + dirSep + destName;
		DbgWriter.msg("original file name: " + fileName + " destination name: " + destName + " desPath = " + desPath, false);
		Path fpath = java.nio.file.Paths.get(desPath);

		try {
			if (fileBytes != null && fileBytes.length > 0) {
				Files.write(fpath, fileBytes);
				DbgWriter.msg("file: " + destName + " " + fileBytes.length + " bytes written.", false);
			} else
				throw new java.lang.IllegalStateException("fileBytes is null or len == 0");
		} catch (Exception ex) {
			setInfoMsg("Exception during file save.");
		}

		return destName;
	}


	/**
	 * setInfoMsg - displays an info message
	 * @param msg the message
	 */
	protected void setInfoMsg(String msg) {
		if (jLabel_infoMessage != null)
			jLabel_infoMessage.setText(msg);
	}



	/**
	 * selectItemByString sets a JComboBox and JMenu with selected string s
	 * @param cb {@link JComboBox}
	 * @param m {@link JMenu}
	 * @param s {@link String}
	 */
	protected static void selectItemByString(JComboBox cb, JMenu m, String s) {
		if (cb != null) {
			for (int i = 0; i < cb.getItemCount(); i++) {
				if (cb.getItemAt(i).toString().equals(s) ||
						cb.getItemAt(i).toString().equalsIgnoreCase(s)) {
					cb.setSelectedIndex(i);
					break;
				}
			}
		}
		selectMenuItemByString(m, s);
	}

	/**
	 * selectMenuItemByString marks inside a JMenu MenuItem that names like s
	 * @param m {@link JMenu}
	 * @param s {@link String}
	 */
	protected static void selectMenuItemByString(JMenu m, String s) {

		if (m != null) {
			for (int i = 0; i < m.getItemCount(); i++) {
				JMenuItem item = m.getItem(i);
				if (item.getText().equalsIgnoreCase(s))
					item.setBackground(selectionBg); // item.setEnabled(enable);
				else
					item.setBackground(defaultMenuItemBg);
			}
		}
	}


	/**
	 * toogleMenuItem marks and unmarks MenuItem
	 * @param item {@link JMenuItem}
	 */
	protected static void toogleMenuItem(JMenuItem item) {

		if (item != null) {
			if (item.getBackground().equals(selectionBg))
				item.setBackground(defaultMenuItemBg);
			else
				item.setBackground(selectionBg); // item.setEnabled(enable);
		}
	}

	public static boolean isMenuItemChecked(JMenuItem menuItem) {
		boolean isChecked = false;
		isChecked = (menuItem.getBackground().equals(selectionBg));
		return isChecked;
	}



}

