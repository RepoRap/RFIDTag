using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO.Ports;

namespace RFIDTagging
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            serialPort1.Open(); 

        }

    
       

        private void label1_Click(object sender, EventArgs e) // name label
        {

        }

        private void button1_Click(object sender, EventArgs e) // new patient button
        {
            
            String name1 = textBox2.Text;
            String mr = textBox3.Text;
            String UID = textBox1.Text;
            String det = "server=DESKTOP-G1BG8LK\\SQLEXPRESS;database=rfid;UID=sa;password=HelloWorld!1"; // linking the details from the server
            String que = "insert into nha (Name,MR,UID) values ('{0}','{1}','{2}')";
            String ad = String.Format(que, name1.Trim(), mr.Trim(), UID.Trim());
            SqlConnection con = new SqlConnection(det);
            SqlCommand cmd = new SqlCommand(ad, con);
            String ps = @"c:\fp\{0}";
            String k = String.Format(ps,mr.Trim());
            System.IO.Directory.CreateDirectory(k);
            con.Open();
            SqlDataReader nd = cmd.ExecuteReader();
            while (nd.Read()) { }
            MessageBox.Show("Registered");
            nd.Close();
            con.Close();

           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e) // read card button
        {
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox2.Text = "";
            textBox3.Text = "";

            button1.Enabled = true;
            label6.Text = "Not Registered";
            String getter = serialPort1.ReadExisting();
            textBox1.Text = getter; // UID 
            String ac = textBox1.Text;  // uid moves to ac
            String det = "server=DESKTOP-G1BG8LK\\SQLEXPRESS;database=rfid;UID=sa;password=HelloWorld!1"; // linking the details from the server
            String que = "SELECT * FROM nha WHERE UID='" + ac.Trim() + "'"; // takes the UID number from card
            SqlConnection con = new SqlConnection(det);
            SqlCommand cmd = new SqlCommand(que, con);
            con.Open();
            SqlDataReader readj = cmd.ExecuteReader();
            if (readj.Read() == true)
            {
                if (ac.Trim() == readj["UID"].ToString())
                {   // reads the UID
                    label6.Text = "Registered";
                    textBox2.Enabled = false;
                    textBox3.Enabled = false;
                    button1.Enabled = false;
                    // MR number 
                }
                
                
                //prints message 
            }
            readj.Close();       //closing readj
            con.Close();         // closing
            
        }
        private void button2_Click(object sender, EventArgs e) // get details button
        {
            String ac = textBox1.Text;  // uid moves to ac
            String det = "server=DESKTOP-G1BG8LK\\SQLEXPRESS;database=rfid;UID=sa;password=HelloWorld!1"; // linking the details from the server
            String que = "SELECT * FROM nha WHERE UID='"+ac.Trim()+"'"; // takes the UID number from card
            SqlConnection con = new SqlConnection(det);
            SqlCommand cmd = new SqlCommand(que,con);
         
            con.Open();
            SqlDataReader readj = cmd.ExecuteReader();
            if (readj.Read()==true)
            {
                if (ac.Trim() == readj["UID"].ToString()) {   // reads the UID
                textBox2.Text = readj["Name"].ToString();    //  name of patient
                textBox3.Text = readj["MR"].ToString();
                    label6.Text = "Registered";
                    // MR number 
            }
            //prints message 
        }
            readj.Close();       //closing readj
            con.Close();         // closing
            
            
        }

        private void button4_Click(object sender, EventArgs e) // more details button
        {
            String a = @"c:\fp\"+textBox3.Text.Trim(); // patient folder destination 
            
            Process.Start("explorer.exe",a);        // calling the patient folder
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

