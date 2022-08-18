using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;
using APP4;

namespace App4
{
    [Activity(Label = "AES", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.layout2);
            Button b = (Button)FindViewById(Resource.Id.button1);
            b.Click += B_Click;
            RadioButton ECB = (RadioButton)FindViewById(Resource.Id.radioButton1);
            RadioButton CFB = (RadioButton)FindViewById(Resource.Id.radioButton2);
            ECB.Click += radio_Clicked;
            CFB.Click += radio_Clicked;
            ECB.Checked = true;

            RadioButton Encrypt = (RadioButton)FindViewById(Resource.Id.radioButton3);
            RadioButton Decrypt = (RadioButton)FindViewById(Resource.Id.radioButton4);
            Encrypt.Click += EncorDec_Click;     
            Decrypt.Click += EncorDec_Click;
            Encrypt.Checked = true;
          
        }

        

        private void EncorDec_Click(object sender, EventArgs e)
        {
            RadioButton Encrypt = (RadioButton)FindViewById(Resource.Id.radioButton3);
            RadioButton Decrypt = (RadioButton)FindViewById(Resource.Id.radioButton4);

            if (sender as RadioButton == Encrypt)
            {
                Decrypt.Checked = false;
                ((RadioButton)FindViewById(Resource.Id.radioButton2)).Enabled = true;
            }
            else
            {
                Encrypt.Checked = false;
                ((EditText)FindViewById(Resource.Id.editText2)).Enabled = true;
               
            }
        }
        
        private void B_Click(object sender, System.EventArgs e)
        {
            EditText et = (EditText)FindViewById(Resource.Id.editText1);
            TextView tv = (TextView)FindViewById(Resource.Id.textView1);
            TextView tv2 = (TextView)FindViewById(Resource.Id.textView2);
            List<byte[][]> Ciphers = new List<byte[][]>();// this should be cleared when I click Encrypt
            byte[][] Key;
            List<byte[][]> Keys = new List<byte[][]>();
            string Message;
            List<byte[][]> Plain;
            Key = StaticFunctions.def2DByte(4, 4);

            if (((EditText)FindViewById(Resource.Id.editText2)).Text == "")
                StaticFunctions.generateRandom2DByteArray(Key, 4);
            else if (((EditText)FindViewById(Resource.Id.editText2)).Text.Length == 16)
            {
                string temp = (((EditText)FindViewById(Resource.Id.editText2)).Text);
                StaticFunctions.take16Byte(temp, Key, 0);
            }
            else
            {
                Toast.MakeText(this, "Enter a Valid Key of Size 16 bytes", ToastLength.Short).Show();
                return;
            }

            StaticFunctions.transpose(Key);//mlhash lazma :S 3shan 5atrak ya dr
            Message = StaticFunctions.msgDivBy16("<P>" + et.Text + "</P>");

           
            
            StaticFunctions.addTolist(Key, Keys);
            for (int i = 0; i < 10; ++i)
            {
                Key = StaticFunctions.KeyExpantion(Key, i);
                StaticFunctions.addTolist(Key, Keys);

            }
            if (((RadioButton)FindViewById(Resource.Id.radioButton3)).Checked)
            {
                if (((RadioButton)FindViewById(Resource.Id.radioButton1)).Checked)
                {

                    Ciphers = BlockCipherModes.ECB(Message, Keys);
                    Message = "";
                    Plain = BlockCipherModes.inverseECB(Ciphers, Keys);
                    for (int i = 0; i < Plain.Count; ++i)
                    {
                        for (int k = 0; k < 4; ++k)
                        {
                            for (int j = 0; j < 4; ++j)
                            {
                                Message += (Convert.ToChar(Plain[i][k][j]) + "");
                            }
                        }
                    }
                    string temp1 = "</P>";
                    for (int i = Message.Length - temp1.Length; i >= 0; --i)
                    {
                        if (Message.Substring(i, temp1.Length) == temp1)
                        {
                            tv.Text = Message.Substring(3, i - 3);
                            break;
                        }
                    }
                    tv.Text += "\n Key:";
                    for (int k = 0; k < 4; ++k)
                    {
                        for (int j = 0; j < 4; ++j)
                        {
                            tv.Text += (Convert.ToChar(Keys[0][j][k]) + "");
                        }
                    }
                    tv2.Text = "Cipher:";
                    for (int i = 0; i < Ciphers.Count; ++i)
                    {
                        for (int k = 0; k < 4; ++k)
                        {
                            for (int j = 0; j < 4; ++j)
                            {
                                tv2.Text += (Convert.ToChar(Ciphers[i][k][j]) + "");
                            }
                        }
                    }

                }
                else
                {
                    byte[][] IVbyteArray;
                    string IV = "";
                    IVbyteArray = StaticFunctions.def2DByte(4, 4);
                    Random r = new Random();
                    for (int i = 0; i < 16; ++i)
                        IV += r.Next() % 2 == 0 ? (char)(r.Next() % 26 + ((int)'a')) : (char)(r.Next() % 26 + ((int)'A'));

                    StaticFunctions.take16Byte(IV, IVbyteArray, 0);
                    Ciphers = BlockCipherModes.CBC(Message, Keys, IVbyteArray);
                    Plain = BlockCipherModes.inversecCBC(Ciphers, Keys, IVbyteArray);
                    Message = "";
                    for (int i = 0; i < Plain.Count; ++i)
                    {
                        for (int k = 0; k < 4; ++k)
                        {
                            for (int j = 0; j < 4; ++j)
                            {
                                Message += (Convert.ToChar(Plain[i][k][j]) + "");
                            }
                        }
                    }
                    //   tv.Text = Message;
                    string temp1 = "</P>";
                    for (int i = Message.Length - temp1.Length; i >= 0; --i)
                    {
                        if (Message.Substring(i, temp1.Length) == temp1)
                        {
                            tv.Text = Message.Substring(3, i - 3);
                            break;
                        }
                    }
                    tv.Text += "\n Key:";
                    for (int k = 0; k < 4; ++k)
                    {
                        for (int j = 0; j < 4; ++j)
                        {
                            tv.Text += (Convert.ToChar(Keys[0][j][k]) + "");
                        }
                    }
                    tv2.Text = "Cipher:";
                    for (int i = 0; i < Ciphers.Count; ++i)
                    {
                        for (int k = 0; k < 4; ++k)
                        {
                            for (int j = 0; j < 4; ++j)
                            {
                                tv2.Text += (Convert.ToChar(Ciphers[i][k][j]) + "");
                                
                            }
                        }
                    }
                }
            }
            else
            {
                Plain = BlockCipherModes.inverseECB(Ciphers, Keys);
                for (int i = 0; i < Plain.Count; ++i)
                {
                    for (int k = 0; k < 4; ++k)
                    {
                        for (int j = 0; j < 4; ++j)
                        {
                            Message += (Convert.ToChar(Plain[i][k][j]) + "");
                        }
                    }
                }
                string temp1 = "</P>";
                for (int i = Message.Length - temp1.Length; i >= 0; --i)
                {
                    if (Message.Substring(i, temp1.Length) == temp1)
                    {
                        tv2.Text = Message.Substring(3, i - 3);
                        break;
                    }
                }
            }
            Ciphers.Clear();
        }

        public void radio_Clicked(object sender, System.EventArgs e)
        {
            RadioButton R = sender as RadioButton;
            RadioButton ECB = (RadioButton) FindViewById(Resource.Id.radioButton1);
            RadioButton CFB = (RadioButton)FindViewById(Resource.Id.radioButton2);
            if (R.Text == "Electronic Codebook")
            {
                CFB.Checked = false;
            }
            else
            {
                ECB.Checked = false;
            }
        }
    }
}

