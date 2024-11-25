using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Projeto_Muscle_Tec
{
    public partial class Login : Form
    {
        private MySqlConnection conexao;
        public Login()
        {
            InitializeComponent();
            ConectarBanco();
        }

        private void ConectarBanco()
        {
            string servidor = "localhost"; // ou o IP do seu servidor MySQL
            string banco = "muscletec";
            string usuario = "root"; // ou o usuário configurado
            string senha = "";

            string stringConexao = $"SERVER={servidor}; DATABASE={banco}; UID={usuario}; PASSWORD={senha};";

            try
            {
                conexao = new MySqlConnection(stringConexao);
                conexao.Open();
                MessageBox.Show("Conexão bem-sucedida!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar: {ex.Message}");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Cadastro cadastro = new Cadastro();
            cadastro.Show();
        }
    }
}
