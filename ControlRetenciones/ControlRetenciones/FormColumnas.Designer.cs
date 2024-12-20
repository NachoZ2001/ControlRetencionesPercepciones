namespace ControlRetenciones
{
    partial class FormColumnas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxColumna = new TextBox();
            textBoxNumeroColumna = new TextBox();
            textBoxCUIT = new TextBox();
            numericUpDownCUIT = new NumericUpDown();
            textBoxFecha = new TextBox();
            numericUpDownFecha = new NumericUpDown();
            textBoxCertificado = new TextBox();
            numericUpDownCertificado = new NumericUpDown();
            textBoxImporte = new TextBox();
            numericUpDownImporte = new NumericUpDown();
            buttonCancelar = new Button();
            buttonGuardar = new Button();
            textBoxFormato = new TextBox();
            textBoxFormatoInput = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCUIT).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFecha).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCertificado).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImporte).BeginInit();
            SuspendLayout();
            // 
            // textBoxColumna
            // 
            textBoxColumna.BackColor = Color.BlueViolet;
            textBoxColumna.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxColumna.ForeColor = Color.White;
            textBoxColumna.Location = new Point(49, 36);
            textBoxColumna.Margin = new Padding(3, 4, 3, 4);
            textBoxColumna.Name = "textBoxColumna";
            textBoxColumna.Size = new Size(114, 29);
            textBoxColumna.TabIndex = 0;
            textBoxColumna.Text = "Columna";
            textBoxColumna.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxNumeroColumna
            // 
            textBoxNumeroColumna.BackColor = Color.BlueViolet;
            textBoxNumeroColumna.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxNumeroColumna.ForeColor = Color.White;
            textBoxNumeroColumna.Location = new Point(226, 36);
            textBoxNumeroColumna.Margin = new Padding(3, 4, 3, 4);
            textBoxNumeroColumna.Name = "textBoxNumeroColumna";
            textBoxNumeroColumna.Size = new Size(114, 29);
            textBoxNumeroColumna.TabIndex = 1;
            textBoxNumeroColumna.Text = "Índice";
            textBoxNumeroColumna.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxCUIT
            // 
            textBoxCUIT.BackColor = Color.BlueViolet;
            textBoxCUIT.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxCUIT.ForeColor = Color.White;
            textBoxCUIT.Location = new Point(49, 111);
            textBoxCUIT.Margin = new Padding(3, 4, 3, 4);
            textBoxCUIT.Name = "textBoxCUIT";
            textBoxCUIT.Size = new Size(114, 29);
            textBoxCUIT.TabIndex = 2;
            textBoxCUIT.Text = "CUIT";
            textBoxCUIT.TextAlign = HorizontalAlignment.Center;
            // 
            // numericUpDownCUIT
            // 
            numericUpDownCUIT.BackColor = Color.BlueViolet;
            numericUpDownCUIT.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            numericUpDownCUIT.ForeColor = Color.White;
            numericUpDownCUIT.Location = new Point(229, 113);
            numericUpDownCUIT.Margin = new Padding(3, 4, 3, 4);
            numericUpDownCUIT.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numericUpDownCUIT.Name = "numericUpDownCUIT";
            numericUpDownCUIT.Size = new Size(112, 29);
            numericUpDownCUIT.TabIndex = 3;
            numericUpDownCUIT.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxFecha
            // 
            textBoxFecha.BackColor = Color.BlueViolet;
            textBoxFecha.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxFecha.ForeColor = Color.White;
            textBoxFecha.Location = new Point(49, 185);
            textBoxFecha.Margin = new Padding(3, 4, 3, 4);
            textBoxFecha.Name = "textBoxFecha";
            textBoxFecha.Size = new Size(114, 29);
            textBoxFecha.TabIndex = 4;
            textBoxFecha.Text = "Fecha";
            textBoxFecha.TextAlign = HorizontalAlignment.Center;
            // 
            // numericUpDownFecha
            // 
            numericUpDownFecha.BackColor = Color.BlueViolet;
            numericUpDownFecha.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            numericUpDownFecha.ForeColor = Color.White;
            numericUpDownFecha.Location = new Point(226, 187);
            numericUpDownFecha.Margin = new Padding(3, 4, 3, 4);
            numericUpDownFecha.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numericUpDownFecha.Name = "numericUpDownFecha";
            numericUpDownFecha.Size = new Size(112, 29);
            numericUpDownFecha.TabIndex = 5;
            numericUpDownFecha.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxCertificado
            // 
            textBoxCertificado.BackColor = Color.BlueViolet;
            textBoxCertificado.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxCertificado.ForeColor = Color.White;
            textBoxCertificado.Location = new Point(49, 263);
            textBoxCertificado.Margin = new Padding(3, 4, 3, 4);
            textBoxCertificado.Name = "textBoxCertificado";
            textBoxCertificado.Size = new Size(114, 29);
            textBoxCertificado.TabIndex = 6;
            textBoxCertificado.Text = "Certificado";
            textBoxCertificado.TextAlign = HorizontalAlignment.Center;
            // 
            // numericUpDownCertificado
            // 
            numericUpDownCertificado.BackColor = Color.BlueViolet;
            numericUpDownCertificado.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            numericUpDownCertificado.ForeColor = Color.White;
            numericUpDownCertificado.Location = new Point(226, 264);
            numericUpDownCertificado.Margin = new Padding(3, 4, 3, 4);
            numericUpDownCertificado.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numericUpDownCertificado.Name = "numericUpDownCertificado";
            numericUpDownCertificado.Size = new Size(112, 29);
            numericUpDownCertificado.TabIndex = 7;
            numericUpDownCertificado.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxImporte
            // 
            textBoxImporte.BackColor = Color.BlueViolet;
            textBoxImporte.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxImporte.ForeColor = Color.White;
            textBoxImporte.Location = new Point(49, 332);
            textBoxImporte.Margin = new Padding(3, 4, 3, 4);
            textBoxImporte.Name = "textBoxImporte";
            textBoxImporte.Size = new Size(114, 29);
            textBoxImporte.TabIndex = 8;
            textBoxImporte.Text = "Importe";
            textBoxImporte.TextAlign = HorizontalAlignment.Center;
            // 
            // numericUpDownImporte
            // 
            numericUpDownImporte.BackColor = Color.BlueViolet;
            numericUpDownImporte.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            numericUpDownImporte.ForeColor = Color.White;
            numericUpDownImporte.Location = new Point(226, 333);
            numericUpDownImporte.Margin = new Padding(3, 4, 3, 4);
            numericUpDownImporte.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            numericUpDownImporte.Name = "numericUpDownImporte";
            numericUpDownImporte.Size = new Size(112, 29);
            numericUpDownImporte.TabIndex = 9;
            numericUpDownImporte.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonCancelar
            // 
            buttonCancelar.BackColor = Color.BlueViolet;
            buttonCancelar.FlatStyle = FlatStyle.Flat;
            buttonCancelar.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCancelar.ForeColor = Color.White;
            buttonCancelar.Location = new Point(77, 463);
            buttonCancelar.Margin = new Padding(3, 4, 3, 4);
            buttonCancelar.Name = "buttonCancelar";
            buttonCancelar.Size = new Size(110, 37);
            buttonCancelar.TabIndex = 10;
            buttonCancelar.Text = "Cancelar";
            buttonCancelar.UseVisualStyleBackColor = false;
            buttonCancelar.Click += buttonCancelar_Click_1;
            // 
            // buttonGuardar
            // 
            buttonGuardar.BackColor = Color.BlueViolet;
            buttonGuardar.FlatStyle = FlatStyle.Flat;
            buttonGuardar.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            buttonGuardar.ForeColor = Color.White;
            buttonGuardar.Location = new Point(193, 463);
            buttonGuardar.Margin = new Padding(3, 4, 3, 4);
            buttonGuardar.Name = "buttonGuardar";
            buttonGuardar.Size = new Size(110, 37);
            buttonGuardar.TabIndex = 11;
            buttonGuardar.Text = "Guardar";
            buttonGuardar.UseVisualStyleBackColor = false;
            buttonGuardar.Click += buttonGuardar_Click_1;
            // 
            // textBoxFormato
            // 
            textBoxFormato.BackColor = Color.BlueViolet;
            textBoxFormato.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxFormato.ForeColor = Color.White;
            textBoxFormato.Location = new Point(49, 398);
            textBoxFormato.Margin = new Padding(3, 4, 3, 4);
            textBoxFormato.Name = "textBoxFormato";
            textBoxFormato.Size = new Size(114, 29);
            textBoxFormato.TabIndex = 12;
            textBoxFormato.Text = "Formato";
            textBoxFormato.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxFormatoInput
            // 
            textBoxFormatoInput.BackColor = Color.BlueViolet;
            textBoxFormatoInput.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxFormatoInput.ForeColor = Color.White;
            textBoxFormatoInput.Location = new Point(226, 398);
            textBoxFormatoInput.Margin = new Padding(3, 4, 3, 4);
            textBoxFormatoInput.Name = "textBoxFormatoInput";
            textBoxFormatoInput.Size = new Size(114, 29);
            textBoxFormatoInput.TabIndex = 13;
            textBoxFormatoInput.TextAlign = HorizontalAlignment.Center;
            // 
            // FormColumnas
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Purple;
            ClientSize = new Size(399, 546);
            Controls.Add(textBoxFormatoInput);
            Controls.Add(textBoxFormato);
            Controls.Add(buttonGuardar);
            Controls.Add(buttonCancelar);
            Controls.Add(numericUpDownImporte);
            Controls.Add(textBoxImporte);
            Controls.Add(numericUpDownCertificado);
            Controls.Add(textBoxCertificado);
            Controls.Add(numericUpDownFecha);
            Controls.Add(textBoxFecha);
            Controls.Add(numericUpDownCUIT);
            Controls.Add(textBoxCUIT);
            Controls.Add(textBoxNumeroColumna);
            Controls.Add(textBoxColumna);
            ForeColor = Color.Purple;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormColumnas";
            Text = "FormColumnas";
            ((System.ComponentModel.ISupportInitialize)numericUpDownCUIT).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFecha).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCertificado).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImporte).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxColumna;
        private TextBox textBoxNumeroColumna;
        private TextBox textBoxCUIT;
        private NumericUpDown numericUpDownCUIT;
        private TextBox textBoxFecha;
        private NumericUpDown numericUpDownFecha;
        private TextBox textBoxCertificado;
        private NumericUpDown numericUpDownCertificado;
        private TextBox textBoxImporte;
        private NumericUpDown numericUpDownImporte;
        private Button buttonCancelar;
        private Button buttonGuardar;
        private TextBox textBoxFormato;
        private TextBox textBoxFormatoInput;
    }
}