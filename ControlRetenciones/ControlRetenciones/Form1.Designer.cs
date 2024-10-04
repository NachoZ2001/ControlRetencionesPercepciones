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
            textBoxReporte = new TextBox();
            buttonCarpeta = new Button();
            buttonCrearEsquema = new Button();
            buttonEditarEsquema = new Button();
            comboBoxEsquemas = new ComboBox();
            textBoxSeleccionarEsquema = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogoEstudio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRuedaCargando).BeginInit();
            SuspendLayout();
            // 
            // txtRutaArchivo2
            // 
            txtRutaArchivo2.BackColor = Color.BlueViolet;
            txtRutaArchivo2.BorderStyle = BorderStyle.FixedSingle;
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
            txtRutaArchivo1.BorderStyle = BorderStyle.FixedSingle;
            txtRutaArchivo1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            txtRutaArchivo1.ForeColor = SystemColors.ButtonFace;
            txtRutaArchivo1.Location = new Point(23, 11);
            txtRutaArchivo1.Name = "txtRutaArchivo1";
            txtRutaArchivo1.ReadOnly = true;
            txtRutaArchivo1.Size = new Size(190, 23);
            txtRutaArchivo1.TabIndex = 1;
            txtRutaArchivo1.Text = "Archivo Contabilidad";
            txtRutaArchivo1.TextAlign = HorizontalAlignment.Center;
            // 
            // btnSeleccionarArchivo1
            // 
            btnSeleccionarArchivo1.BackColor = Color.BlueViolet;
            btnSeleccionarArchivo1.FlatStyle = FlatStyle.Popup;
            btnSeleccionarArchivo1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSeleccionarArchivo1.ForeColor = SystemColors.ButtonFace;
            btnSeleccionarArchivo1.Location = new Point(47, 54);
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
            btnSeleccionarArchivo2.FlatStyle = FlatStyle.Popup;
            btnSeleccionarArchivo2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnSeleccionarArchivo2.ForeColor = SystemColors.ButtonFace;
            btnSeleccionarArchivo2.Location = new Point(284, 54);
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
            btnProcesar.FlatStyle = FlatStyle.Popup;
            btnProcesar.ForeColor = SystemColors.ButtonFace;
            btnProcesar.Location = new Point(740, 11);
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
            pictureBoxLogoEstudio.Location = new Point(47, 183);
            pictureBoxLogoEstudio.Name = "pictureBoxLogoEstudio";
            pictureBoxLogoEstudio.Size = new Size(611, 174);
            pictureBoxLogoEstudio.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLogoEstudio.TabIndex = 6;
            pictureBoxLogoEstudio.TabStop = false;
            pictureBoxLogoEstudio.Click += pictureBoxLogoEstudio_Click;
            // 
            // pictureBoxRuedaCargando
            // 
            pictureBoxRuedaCargando.BackColor = Color.Transparent;
            pictureBoxRuedaCargando.Image = (Image)resources.GetObject("pictureBoxRuedaCargando.Image");
            pictureBoxRuedaCargando.Location = new Point(773, 107);
            pictureBoxRuedaCargando.Name = "pictureBoxRuedaCargando";
            pictureBoxRuedaCargando.Size = new Size(85, 65);
            pictureBoxRuedaCargando.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxRuedaCargando.TabIndex = 7;
            pictureBoxRuedaCargando.TabStop = false;
            // 
            // textBoxReporte
            // 
            textBoxReporte.BackColor = Color.BlueViolet;
            textBoxReporte.BorderStyle = BorderStyle.FixedSingle;
            textBoxReporte.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxReporte.ForeColor = SystemColors.Window;
            textBoxReporte.Location = new Point(503, 12);
            textBoxReporte.Name = "textBoxReporte";
            textBoxReporte.ReadOnly = true;
            textBoxReporte.Size = new Size(190, 23);
            textBoxReporte.TabIndex = 9;
            textBoxReporte.Text = "Carpeta Reporte";
            textBoxReporte.TextAlign = HorizontalAlignment.Center;
            textBoxReporte.TextChanged += textBoxReporte_TextChanged;
            // 
            // buttonCarpeta
            // 
            buttonCarpeta.BackColor = Color.BlueViolet;
            buttonCarpeta.FlatStyle = FlatStyle.Popup;
            buttonCarpeta.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCarpeta.ForeColor = SystemColors.ButtonFace;
            buttonCarpeta.Location = new Point(534, 54);
            buttonCarpeta.Name = "buttonCarpeta";
            buttonCarpeta.Size = new Size(124, 23);
            buttonCarpeta.TabIndex = 10;
            buttonCarpeta.Text = "Seleccionar carpeta";
            buttonCarpeta.UseVisualStyleBackColor = false;
            buttonCarpeta.Click += btnCarpeta_Click;
            // 
            // buttonCrearEsquema
            // 
            buttonCrearEsquema.BackColor = Color.BlueViolet;
            buttonCrearEsquema.FlatStyle = FlatStyle.Popup;
            buttonCrearEsquema.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCrearEsquema.ForeColor = SystemColors.ButtonFace;
            buttonCrearEsquema.Location = new Point(47, 132);
            buttonCrearEsquema.Name = "buttonCrearEsquema";
            buttonCrearEsquema.Size = new Size(124, 23);
            buttonCrearEsquema.TabIndex = 11;
            buttonCrearEsquema.Text = "Crear esquema";
            buttonCrearEsquema.UseVisualStyleBackColor = false;
            buttonCrearEsquema.Click += buttonCrearEsquema_Click;
            // 
            // buttonEditarEsquema
            // 
            buttonEditarEsquema.BackColor = Color.BlueViolet;
            buttonEditarEsquema.FlatStyle = FlatStyle.Popup;
            buttonEditarEsquema.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditarEsquema.ForeColor = SystemColors.ButtonFace;
            buttonEditarEsquema.Location = new Point(534, 132);
            buttonEditarEsquema.Name = "buttonEditarEsquema";
            buttonEditarEsquema.Size = new Size(124, 23);
            buttonEditarEsquema.TabIndex = 12;
            buttonEditarEsquema.Text = "Editar esquema";
            buttonEditarEsquema.UseVisualStyleBackColor = false;
            buttonEditarEsquema.Click += buttonEditarEsquema_Click;
            // 
            // comboBoxEsquemas
            // 
            comboBoxEsquemas.BackColor = Color.BlueViolet;
            comboBoxEsquemas.FlatStyle = FlatStyle.Popup;
            comboBoxEsquemas.ForeColor = Color.White;
            comboBoxEsquemas.FormattingEnabled = true;
            comboBoxEsquemas.Location = new Point(271, 133);
            comboBoxEsquemas.Name = "comboBoxEsquemas";
            comboBoxEsquemas.Size = new Size(176, 23);
            comboBoxEsquemas.TabIndex = 13;
            // 
            // textBoxSeleccionarEsquema
            // 
            textBoxSeleccionarEsquema.BackColor = Color.Purple;
            textBoxSeleccionarEsquema.BorderStyle = BorderStyle.None;
            textBoxSeleccionarEsquema.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxSeleccionarEsquema.ForeColor = Color.White;
            textBoxSeleccionarEsquema.Location = new Point(284, 107);
            textBoxSeleccionarEsquema.Name = "textBoxSeleccionarEsquema";
            textBoxSeleccionarEsquema.Size = new Size(150, 20);
            textBoxSeleccionarEsquema.TabIndex = 14;
            textBoxSeleccionarEsquema.Text = "Seleccionar esquema";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Purple;
            ClientSize = new Size(901, 382);
            Controls.Add(textBoxSeleccionarEsquema);
            Controls.Add(comboBoxEsquemas);
            Controls.Add(buttonEditarEsquema);
            Controls.Add(buttonCrearEsquema);
            Controls.Add(buttonCarpeta);
            Controls.Add(textBoxReporte);
            Controls.Add(pictureBoxRuedaCargando);
            Controls.Add(btnProcesar);
            Controls.Add(btnSeleccionarArchivo2);
            Controls.Add(btnSeleccionarArchivo1);
            Controls.Add(txtRutaArchivo1);
            Controls.Add(txtRutaArchivo2);
            Controls.Add(pictureBoxLogoEstudio);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
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
        private TextBox textBoxReporte;
        private Button buttonCarpeta;
        private Button buttonCrearEsquema;
        private Button buttonEditarEsquema;
        private ComboBox comboBoxEsquemas;
        private TextBox textBoxSeleccionarEsquema;
    }
}