namespace ControlRetenciones
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtRutaArchivo2 = new TextBox();
            txtRutaArchivo1 = new TextBox();
            btnSeleccionarArchivo1 = new Button();
            btnSeleccionarArchivo2 = new Button();
            btnProcesar = new Button();
            pictureBoxLogoEstudio = new PictureBox();
            pictureBoxRuedaCargando = new PictureBox();
            opcionesTipo = new CheckedListBox();
            textBoxReporte = new TextBox();
            buttonCarpeta = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogoEstudio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRuedaCargando).BeginInit();
            SuspendLayout();
            // 
            // txtRutaArchivo2
            // 
            txtRutaArchivo2.BackColor = Color.BlueViolet;
            txtRutaArchivo2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            txtRutaArchivo2.ForeColor = SystemColors.ButtonFace;
            txtRutaArchivo2.Location = new Point(257, 12);
            txtRutaArchivo2.Name = "txtRutaArchivo2";
            txtRutaArchivo2.ReadOnly = true;
            txtRutaArchivo2.Size = new Size(190, 23);
            txtRutaArchivo2.TabIndex = 0;
            txtRutaArchivo2.Text = "Archivo AFIP";
            txtRutaArchivo2.TextAlign = HorizontalAlignment.Center;
            // 
            // txtRutaArchivo1
            // 
            txtRutaArchivo1.BackColor = Color.BlueViolet;
            txtRutaArchivo1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            txtRutaArchivo1.ForeColor = SystemColors.ButtonFace;
            txtRutaArchivo1.Location = new Point(12, 12);
            txtRutaArchivo1.Name = "txtRutaArchivo1";
            txtRutaArchivo1.ReadOnly = true;
            txtRutaArchivo1.Size = new Size(190, 23);
            txtRutaArchivo1.TabIndex = 1;
            txtRutaArchivo1.Text = "Archivo Contabilidad";
            txtRutaArchivo1.TextAlign = HorizontalAlignment.Center;
            txtRutaArchivo1.TextChanged += txtRutaArchivo1_TextChanged;
            // 
            // btnSeleccionarArchivo1
            // 
            btnSeleccionarArchivo1.BackColor = Color.BlueViolet;
            btnSeleccionarArchivo1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSeleccionarArchivo1.ForeColor = SystemColors.ButtonFace;
            btnSeleccionarArchivo1.Location = new Point(12, 54);
            btnSeleccionarArchivo1.Name = "btnSeleccionarArchivo1";
            btnSeleccionarArchivo1.Size = new Size(124, 23);
            btnSeleccionarArchivo1.TabIndex = 2;
            btnSeleccionarArchivo1.Text = "Seleccionar archivo";
            btnSeleccionarArchivo1.UseVisualStyleBackColor = false;
            btnSeleccionarArchivo1.Click += btnSeleccionarArchivo1_Click;
            // 
            // btnSeleccionarArchivo2
            // 
            btnSeleccionarArchivo2.BackColor = Color.BlueViolet;
            btnSeleccionarArchivo2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSeleccionarArchivo2.ForeColor = SystemColors.ButtonFace;
            btnSeleccionarArchivo2.Location = new Point(257, 54);
            btnSeleccionarArchivo2.Name = "btnSeleccionarArchivo2";
            btnSeleccionarArchivo2.Size = new Size(124, 23);
            btnSeleccionarArchivo2.TabIndex = 3;
            btnSeleccionarArchivo2.Text = "Seleccionar archivo";
            btnSeleccionarArchivo2.UseVisualStyleBackColor = false;
            btnSeleccionarArchivo2.Click += btnSeleccionarArchivo2_Click;
            // 
            // btnProcesar
            // 
            btnProcesar.BackColor = Color.BlueViolet;
            btnProcesar.ForeColor = SystemColors.ButtonFace;
            btnProcesar.Location = new Point(850, 11);
            btnProcesar.Name = "btnProcesar";
            btnProcesar.Size = new Size(150, 66);
            btnProcesar.TabIndex = 4;
            btnProcesar.Text = "Procesar";
            btnProcesar.UseVisualStyleBackColor = false;
            btnProcesar.Click += btnProcesar_Click;
            // 
            // pictureBoxLogoEstudio
            // 
            pictureBoxLogoEstudio.BackColor = Color.Purple;
            pictureBoxLogoEstudio.BackgroundImageLayout = ImageLayout.None;
            pictureBoxLogoEstudio.Image = (Image)resources.GetObject("pictureBoxLogoEstudio.Image");
            pictureBoxLogoEstudio.Location = new Point(12, 117);
            pictureBoxLogoEstudio.Name = "pictureBoxLogoEstudio";
            pictureBoxLogoEstudio.Size = new Size(807, 247);
            pictureBoxLogoEstudio.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLogoEstudio.TabIndex = 6;
            pictureBoxLogoEstudio.TabStop = false;
            // 
            // pictureBoxRuedaCargando
            // 
            pictureBoxRuedaCargando.BackColor = Color.Transparent;
            pictureBoxRuedaCargando.Image = (Image)resources.GetObject("pictureBoxRuedaCargando.Image");
            pictureBoxRuedaCargando.Location = new Point(850, 83);
            pictureBoxRuedaCargando.Name = "pictureBoxRuedaCargando";
            pictureBoxRuedaCargando.Size = new Size(85, 65);
            pictureBoxRuedaCargando.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxRuedaCargando.TabIndex = 7;
            pictureBoxRuedaCargando.TabStop = false;
            // 
            // opcionesTipo
            // 
            opcionesTipo.BackColor = Color.BlueViolet;
            opcionesTipo.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            opcionesTipo.ForeColor = SystemColors.Window;
            opcionesTipo.FormattingEnabled = true;
            opcionesTipo.Items.AddRange(new object[] { "Percepción", "Retención" });
            opcionesTipo.Location = new Point(709, 11);
            opcionesTipo.Name = "opcionesTipo";
            opcionesTipo.Size = new Size(126, 40);
            opcionesTipo.TabIndex = 8;
            // 
            // textBoxReporte
            // 
            textBoxReporte.BackColor = Color.BlueViolet;
            textBoxReporte.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxReporte.ForeColor = SystemColors.Window;
            textBoxReporte.Location = new Point(503, 12);
            textBoxReporte.Name = "textBoxReporte";
            textBoxReporte.Size = new Size(190, 23);
            textBoxReporte.TabIndex = 9;
            textBoxReporte.Text = "Carpeta Reporte";
            textBoxReporte.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonCarpeta
            // 
            buttonCarpeta.BackColor = Color.BlueViolet;
            buttonCarpeta.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCarpeta.ForeColor = SystemColors.ButtonFace;
            buttonCarpeta.Location = new Point(503, 54);
            buttonCarpeta.Name = "buttonCarpeta";
            buttonCarpeta.Size = new Size(124, 23);
            buttonCarpeta.TabIndex = 10;
            buttonCarpeta.Text = "Seleccionar carpeta";
            buttonCarpeta.UseVisualStyleBackColor = false;
            buttonCarpeta.Click += btnCarpeta_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Purple;
            ClientSize = new Size(1033, 382);
            Controls.Add(buttonCarpeta);
            Controls.Add(textBoxReporte);
            Controls.Add(opcionesTipo);
            Controls.Add(pictureBoxRuedaCargando);
            Controls.Add(btnProcesar);
            Controls.Add(btnSeleccionarArchivo2);
            Controls.Add(btnSeleccionarArchivo1);
            Controls.Add(txtRutaArchivo1);
            Controls.Add(txtRutaArchivo2);
            Controls.Add(pictureBoxLogoEstudio);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogoEstudio).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRuedaCargando).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtRutaArchivo2;
        private TextBox txtRutaArchivo1;
        private Button btnSeleccionarArchivo1;
        private Button btnSeleccionarArchivo2;
        private Button btnProcesar;
        private PictureBox pictureBoxLogoEstudio;
        private PictureBox pictureBoxRuedaCargando;
        private CheckedListBox opcionesTipo;
        private TextBox textBoxReporte;
        private Button buttonCarpeta;
    }
}