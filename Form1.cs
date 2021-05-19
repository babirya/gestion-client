using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestion_client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ado d = new ado();

        public void remplir()
        { 
            // pour eviter problem de repetition 

            if(d.dt.Rows.Count!=null)
           {
                d.dt.Rows.Clear(); 
            }
            d.connecter();
            d.cmd.CommandText = "select codeclient, nomclient , prenomclient, nomville,datenaissance from  client inner join ville on client.codeville = ville.codeville ";
            d.cmd.Connection = d.con;
            d.rd = d.cmd.ExecuteReader();
            d.dt.Load(d.rd);
            dataGridView1.DataSource = d.dt;

            d.rd.Close(); 


        } 
        // remplir combobox 
        public void remlirComboBox()
        { 
            if(comboBox1.Items.Count!=0)
            {
                comboBox1.Items.Clear(); 
            }
            d.connecter();
            d.cmd.CommandText = "select distinct codeville from client  ";
            d.cmd.Connection = d.con;
            d.rd.Close(); 
            d.rd = d.cmd.ExecuteReader(); 

            while(d.rd.Read())
            {

                comboBox1.Items.Add(d.rd[0]); 
            }


        }
        
        // pour nouveau 

        public void nouveau(Control f )
        { 
            if(MessageBox.Show("veilller vous creer un nouveaux client","confirmation",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {

       
               foreach(Control ct in f.Controls)
               {
                   if(ct.GetType()==typeof(TextBox))
                   {
                       ct.Text = ""; 
                   }

                   //if (ct.GetType() == typeof(ComboBox))
                   //{
                      
                   //}

                   //if (ct.GetType() == typeof(DateTimePicker))
                   //{
                       
                   //} 


                   // pour  sous control 
                   if(ct.Controls.Count!=0)
                   {
                       nouveau(ct); 
                   }



               }

            }


        } 
        // nagivation 

        int cpt;
        public void navigation()
        {
            textBox1.Text = d.dt.Rows[cpt][0].ToString();
            textBox2.Text = d.dt.Rows[cpt][1].ToString();
            textBox3.Text = d.dt.Rows[cpt][2].ToString(); 
          //comboBox1.SelectedIndex = int.Parse( d.dt.Rows[cpt][5].ToString());
          dateTimePicker1.Value =DateTime.Parse( d.dt.Rows[cpt][4].ToString());

        }
        private void button11_Click(object sender, EventArgs e)
        { 
            if(MessageBox.Show("veillez vous quitter le program","Comfirmation message",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                     this.Close(); 
            }
       
        } 

       
        public int number()
        {
            d.connecter();
         
            d.cmd.CommandText = "select count (codeclient) from client where codeclient ='"+ textBox1.Text + "'";
            d.cmd.Connection = d.con;
          int cptt;
          d.rd.Close();
            cptt = (int) d.cmd.ExecuteScalar(); 

            return cptt; 
        } 

        // ajouter 

        public bool ajouter()
        {
             if(number()==0)
             {
                 d.connecter();
                 d.cmd.CommandText = "insert into client values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','"+comboBox1.SelectedIndex+"','"+dateTimePicker1.Value+"')";
                 d.cmd.Connection = d.con;
                 d.cmd.ExecuteNonQuery(); 

                 return true; 
             }
             return false; 

        }  

        // modifier 

        public bool modifier()
        {
            if (number() != 0)
            {
                if (MessageBox.Show("vuez modifier ce client ", "confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    d.cmd.CommandText = "update client set nomclient='" + textBox2.Text + "',prenomclient= '" + textBox3.Text + "', codeville='" + comboBox1.SelectedIndex + "', datenaissance='" + dateTimePicker1.Value + "'  where codeclient='" + textBox1.Text + "'  ";

                    d.cmd.Connection = d.con;
                    d.cmd.ExecuteNonQuery();
                    return true;

                }
            }
            return false;
        }

        // supprimer  

        public bool suprimmer()
        {
            if (number() != 0)
            {
                if (MessageBox.Show("vouez suprimer ce client ?", "comfirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {


                    d.cmd.CommandText = " delete from client where codeclient = '" + textBox1.Text + "'";

                    d.cmd.Connection = d.con;
                    d.cmd.ExecuteNonQuery();


                    return true;
                }
            }
            return false;

        } 

        private void Form1_Load(object sender, EventArgs e)
        {
            remplir();
            remlirComboBox(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nouveau(this);
            remlirComboBox();
            dateTimePicker1.Value = DateTime.Today; 
            comboBox1.SelectedIndex = 0; 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            cpt = 0;
            navigation(); 

        }

        private void button10_Click(object sender, EventArgs e)
        {
            cpt = d.dt.Rows.Count - 1;
            navigation(); 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {

                cpt++;
                navigation(); 
            }
            catch
            {
                MessageBox.Show("ce la dernier ligne","alert");
                cpt--; 

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                cpt--;
                navigation();
            }
            catch
            {
                MessageBox.Show("ce la premier ligne", "alert");
                cpt++;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || dateTimePicker1.Value==DateTime.Today)
            //{
            //    MessageBox.Show("sill vous remplir votre champs"); 
            //} 
            if(ajouter()==true)
            {
                MessageBox.Show("client bien ajouter");
                remplir();
            }
            else
            {
                MessageBox.Show("ce client deja existe "); 

            }
        }

        private void button5_Click(object sender, EventArgs e)
        { // for supprimer 
            if (textBox1.Text == "")
            {
                MessageBox.Show("s'ill vous plait enter codeclient ");
                return;
            }

            if (suprimmer() == true)
            {

                MessageBox.Show("Client SUPPRIMER AVEC SUCCESS");
                remplir();
            }
            else
            {
                MessageBox.Show("ce client ne existe pas  ");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // for modifier 
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || dateTimePicker1.Value==DateTime.Today)
            {
                MessageBox.Show("s'ill vous plait remplir toutes les champs");
                return;
            }
            if (modifier() == true)
            {
                MessageBox.Show("client bien modifier");
                remplir(); 
            }
            else
            {
                MessageBox.Show("ce client ne exist pas");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(number()!=0)
            {
                MessageBox.Show("ce client est existe");
            } 
            else{ 

                MessageBox.Show("ce client ne pas existe"); 
            }
        }
    }
}
