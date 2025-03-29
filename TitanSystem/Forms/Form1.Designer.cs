namespace TitanSystem
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtLimpar = new System.Windows.Forms.Button();
            this.BtGravar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtLimpar
            // 
            this.BtLimpar.Location = new System.Drawing.Point(12, 154);
            this.BtLimpar.Name = "BtLimpar";
            this.BtLimpar.Size = new System.Drawing.Size(143, 43);
            this.BtLimpar.TabIndex = 0;
            this.BtLimpar.Text = "Limpar";
            this.BtLimpar.UseVisualStyleBackColor = true;
            // 
            // BtGravar
            // 
            this.BtGravar.Location = new System.Drawing.Point(12, 67);
            this.BtGravar.Name = "BtGravar";
            this.BtGravar.Size = new System.Drawing.Size(143, 43);
            this.BtGravar.TabIndex = 1;
            this.BtGravar.Text = "Gravar";
            this.BtGravar.UseMnemonic = false;
            this.BtGravar.UseVisualStyleBackColor = true;
            this.BtGravar.Click += new System.EventHandler(this.BtGravar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 647);
            this.Controls.Add(this.BtGravar);
            this.Controls.Add(this.BtLimpar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtGravar;
        public System.Windows.Forms.Button BtLimpar;
    }
}

