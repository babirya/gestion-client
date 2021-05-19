using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient; 

namespace gestion_client
{
    class ado
    { 
        //declaration des varibiables  

     public     SqlCommand cmd = new SqlCommand();
     public    SqlConnection con = new SqlConnection();

     public   SqlDataReader rd;
     public DataTable dt = new DataTable(); 
         
        
       // connecter 
          public void connecter ()
        {
              if(con.State==ConnectionState.Closed ||con.State==ConnectionState.Broken)
              {
                  con.ConnectionString=@"Data Source=DESKTOP-3GHSQJ4\SQLEXPRESS;Initial Catalog=gestionclientdb;Integrated Security=True"; 
                  con.Open(); 
              }
        }

        // deconnecter  

        public void deconnecter ()
        {
              if(con.State==ConnectionState.Open)
              {
                  con.Close(); 

              }
        }
    }
}
