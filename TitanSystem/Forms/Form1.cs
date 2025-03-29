using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TitanSystem.Connections;
using TitanSystem.Services;

namespace TitanSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TbNme_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtGravar_Click(object sender, EventArgs e)
        {
            ImportaDadosCliente novaImportacao = new ImportaDadosCliente();

            novaImportacao.ImportarTxt();
            Console.WriteLine("Importação realizada com sucesso");
            this.Close();
        }
    }
}
