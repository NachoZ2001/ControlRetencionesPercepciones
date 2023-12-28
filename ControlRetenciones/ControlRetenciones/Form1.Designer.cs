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
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtRutaArchivo2
            // 
            txtRutaArchivo2.BackColor = Color.BlueViolet;
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
            txtRutaArchivo1.ForeColor = SystemColors.ButtonFace;
            txtRutaArchivo1.Location = new Point(12, 12);
            txtRutaArchivo1.Name = "txtRutaArchivo1";
            txtRutaArchivo1.ReadOnly = true;
            txtRutaArchivo1.Size = new Size(190, 23);
            txtRutaArchivo1.TabIndex = 1;
            txtRutaArchivo1.Text = "Archivo Holistor";
            txtRutaArchivo1.TextAlign = HorizontalAlignment.Center;
            txtRutaArchivo1.TextChanged += txtRutaArchivo1_TextChanged;
            // 
            // btnSeleccionarArchivo1
            // 
            btnSeleccionarArchivo1.BackColor = Color.BlueViolet;
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
            btnProcesar.Location = new Point(542, 11);
            btnProcesar.Name = "btnProcesar";
            btnProcesar.Size = new Size(150, 66);
            btnProcesar.TabIndex = 4;
            btnProcesar.Text = "Procesar";
            btnProcesar.UseVisualStyleBackColor = false;
            btnProcesar.Click += btnProcesar_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Purple;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(125, 93);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(567, 173);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Purple;
            ClientSize = new Size(795, 278);
            Controls.Add(btnProcesar);
            Controls.Add(btnSeleccionarArchivo2);
            Controls.Add(btnSeleccionarArchivo1);
            Controls.Add(txtRutaArchivo1);
            Controls.Add(txtRutaArchivo2);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtRutaArchivo2;
        private TextBox txtRutaArchivo1;
        private Button btnSeleccionarArchivo1;
        private Button btnSeleccionarArchivo2;
        private Button btnProcesar;
        private PictureBox pictureBox1;
    }
}