using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace myRSA
{
    public partial class Form1 : Form
    {
        String publicKey, privateKey; //strings to hold public and private keys
        UnicodeEncoding encoder = new UnicodeEncoding();
        public Form1()
        {
            RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider();
            InitializeComponent();
            privateKey = myRSA.ToXmlString(true);
            publicKey = myRSA.ToXmlString(false);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPlainText.Text = "";
            txtPlainText.Refresh();   
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            txtCypherText.Text = "";
            txtPlainText.Refresh();
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            var myRSA = new RSACryptoServiceProvider();
            //split data into array
            var dataArray = txtCypherText.Text.Split(new char[] { ',' });
            //convert to bytes
            byte[] dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++) dataByte[i] = Convert.ToByte(dataArray[i]);
            //decrypt data array
            myRSA.FromXmlString(privateKey);
            var decryptedBytes = myRSA.Decrypt(dataByte, false);
            //place in text box
            txtPlainText.Text = encoder.GetString(decryptedBytes);
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            var myRSA = new RSACryptoServiceProvider();
            //sets up crypto service with proper key
            myRSA.FromXmlString(publicKey);
            //encode data to encrypt as a byte array
            var dataToEncrypt = encoder.GetBytes(txtPlainText.Text);
            //encrypt byte array
            var encryptedByteArray = myRSA.Encrypt(dataToEncrypt, false).ToArray();
            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            //change each byte in the encrypted byte array to text
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);
                if (item < length) sb.Append(",");
            }
            txtCypherText.Text = sb.ToString();

        }
    }
}
