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
    public partial class Cadastro : Form
    {
        private MySqlConnection conexao;
        public Cadastro()
        {
            InitializeComponent();
            ConectarBanco();

            label8.Visible = false;
            textBox5.Visible = false;
        }

        private void ConectarBanco()
        {
            string servidor = "localhost";
            string banco = "muscletec";
            string usuario = "root"; 
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
                label8.Visible = true;
                textBox5.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
                label8.Visible = false; // Oculta a label
                textBox5.Visible = false;
        }

        private void CadastrarUsuario()
        {
            string nome = textBox1.Text;
            string email = textBox2.Text;
            string senha = textBox3.Text;
            string cpf = textBox4.Text;

            string tipoUsuario = "";
            if (radioButton1.Checked)
            {
                tipoUsuario = "Instrutor";
            }
            else if (radioButton2.Checked)
            {
                tipoUsuario = "Aluno";
            }

            // Insere na tabela usuario
            string queryUsuario = "INSERT INTO usuario (nome, email, senha, cpf) VALUES (@nome, @email, @senha, @cpf)";
            MySqlCommand cmdUsuario = new MySqlCommand(queryUsuario, conexao);
            cmdUsuario.Parameters.AddWithValue("@nome", nome);
            cmdUsuario.Parameters.AddWithValue("@email", email);
            cmdUsuario.Parameters.AddWithValue("@senha", senha);
            cmdUsuario.Parameters.AddWithValue("@cpf", cpf);
            cmdUsuario.Parameters.AddWithValue("@tipo", tipoUsuario); //arrumar
            cmdUsuario.ExecuteNonQuery();

            int idUsuario = (int)cmdUsuario.LastInsertedId; // Recupera o ID gerado

            // Verifica o tipo de usuário
            if (radioButton1.Checked)
            {
                string cref = textBox5.Text;
                string queryInstrutor = "INSERT INTO instrutor (cref, idUsuario) VALUES (@cref, @idUsuario)";
                MySqlCommand cmdInstrutor = new MySqlCommand(queryInstrutor, conexao);
                cmdInstrutor.Parameters.AddWithValue("@cref", cref);
                cmdInstrutor.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmdInstrutor.ExecuteNonQuery();
            }

            //adicionar else if
        }


        private void button1_Click(object sender, EventArgs e)
        {
            CadastrarUsuario();

            Login login = new Login();
            login.Show();
        }
    }
}
