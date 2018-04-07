/* vim: set expandtab shiftwidth=4 softtabstop=4 tabstop=4: */

/**
 * RC4.NET 1.0
 *
 * RC4.NET is a petite library that allows you to use RC4
 * encryption easily in the .NET Platform. It's OO and can
 * produce outputs in binary and hex.
 *
 * (C) Copyright 2006 Mukul Sabharwal [http://mjsabby.com]
 *	   All Rights Reserved
 *
 * @link http://rc4dotnet.devhome.org
 * @author Mukul Sabharwal <mjsabby@gmail.com>
 * @version $Id: RC4.cs,v 1.0 2006/03/19 15:35:24 mukul Exp $
 * @copyright Copyright &copy; 2006 Mukul Sabharwal
 * @license http://www.gnu.org/copyleft/lesser.html
 * @package RC4.NET
 */

using System.Text;

/**
 * RC4 Class
 * @package RC4.NET
 */
public class RC4 {

	/* inherits Page for ASP.NET */
	/**
	 * Get ASCII Integer Code
	 *
	 * @param char ch Character to get ASCII value for
	 * @access private
	 * @return int
	 */
	private static int ord (char ch) {
		return (int) (Encoding.GetEncoding(1252).GetBytes(ch + "")[0]);
	}

	/**
	 * The symmetric encryption function
	 *
	 * @param string pwd Key to encrypt with (can be binary of hex)
	 * @param string data Content to be encrypted
	 * @param bool ispwdHex Key passed is in hexadecimal or not
	 * @access public
	 * @return string
	 */
	public static byte[] Encrypt (string data, string pwd) {
		int a, i, j, k, tmp, pwd_length, data_length;
		int[] key, box;
		byte[] cipher;
		//string cipher;

		pwd_length = pwd.Length;
		data_length = data.Length;
		key = new int[256];
		box = new int[256];
		cipher = new byte[data.Length];
		//cipher = "";

		for (i = 0; i < 256; i++) {
			key[i] = ord(pwd[i % pwd_length]);
			box[i] = i;
		}
		for (j = i = 0; i < 256; i++) {
			j = (j + box[i] + key[i]) % 256;
			tmp = box[i];
			box[i] = box[j];
			box[j] = tmp;
		}
		for (a = j = i = 0; i < data_length; i++) {
			a = (a + 1) % 256;
			j = (j + box[a]) % 256;
			tmp = box[a];
			box[a] = box[j];
			box[j] = tmp;
			k = box[((box[a] + box[j]) % 256)];
			cipher[i] = (byte) (ord(data[i]) ^ k);
			//cipher += chr(ord(data[i]) ^ k);
		}

		return cipher;
	}

	public static string Decrypt (byte[] data) {
		return Encoding.UTF8.GetString(Encrypt(Encoding.GetEncoding(1252).GetString(data), CloudSaveSettings.Key));
	}

}