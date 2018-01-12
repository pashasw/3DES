using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _3des
{
    public partial class Form1 : Form
    {
        //public static String key1 = "";
        //public static String key2 = "";
        //public static String key3 = "";
        public static String path = "test.bin";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string key1 = textBox3.Text;
            string key2 = textBox4.Text;
            string key3 = textBox5.Text;
            if (key1 == "" || key2 == "" || key3 == "")
            { MessageBox.Show("Сгенерируйте ключи"); }
            else
            {
                String message = textBox1.Text;
                String cipher = _3DES.code(message, key1, key2, key3);
                textBox6.Text = cipher;
            }
        }
        private String readFile()
        {
            String message = "";
            try
            {
                using (StreamReader reader = new StreamReader(path, System.Text.Encoding.Default))
                {
                    message = reader.ReadToEnd();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return message;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = readFile();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string key1 = des.GenerKey();
            string key2 = des.GenerKey();
            string key3 = des.GenerKey();
            textBox3.Text = key1;
            textBox4.Text = key2;
            textBox5.Text = key3;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string key1 = textBox3.Text;
            string key2 = textBox4.Text;
            string key3 = textBox5.Text;
            String message = textBox6.Text;
            String cipher = _3DES.decode(message, key1, key2, key3);
            textBox2.Text = cipher;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (FileStream fstream = new FileStream(path, FileMode.Truncate))
                {
                    // преобразуем строку в байты
                    byte[] array = System.Text.Encoding.Default.GetBytes(textBox1.Text);
                    // запись массива байтов в файл
                    fstream.Write(array, 0, array.Length);
                    MessageBox.Show("Файл успешно записан");
                    textBox1.Text = "";
                }
            }
            catch (Exception exc)
            {

                Console.WriteLine(exc.Message);
            }
        }
    }
}
