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
            this.txtRutaArchivo2 = new System.Windows.Forms.TextBox();
            this.txtRutaArchivo1 = new System.Windows.Forms.TextBox();
            this.btnSeleccionarArchivo1 = new System.Windows.Forms.Button();
            this.btnSeleccionarArchivo2 = new System.Windows.Forms.Button();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtRutaArchivo2
            // 
            this.txtRutaArchivo2.BackColor = System.Drawing.Color.MediumPurple;
            this.txtRutaArchivo2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.txtRutaArchivo2.Location = new System.Drawing.Point(257, 32);
            this.txtRutaArchivo2.Name = "txtRutaArchivo2";
            this.txtRutaArchivo2.Size = new System.Drawing.Size(190, 23);
            this.txtRutaArchivo2.TabIndex = 0;
            this.txtRutaArchivo2.Text = "Archivo AFIP";
            this.txtRutaArchivo2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRutaArchivo1
            // 
            this.txtRutaArchivo1.BackColor = System.Drawing.Color.MediumPurple;
            this.txtRutaArchivo1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.txtRutaArchivo1.Location = new System.Drawing.Point(12, 32);
            this.txtRutaArchivo1.Name = "txtRutaArchivo1";
            this.txtRutaArchivo1.Size = new System.Drawing.Size(190, 23);
            this.txtRutaArchivo1.TabIndex = 1;
            this.txtRutaArchivo1.Text = "Archivo Holistor";
            this.txtRutaArchivo1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRutaArchivo1.TextChanged += new System.EventHandler(this.txtRutaArchivo1_TextChanged);
            // 
            // btnSeleccionarArchivo1
            // 
            this.btnSeleccionarArchivo1.BackColor = System.Drawing.Color.SlateBlue;
            this.btnSeleccionarArchivo1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSeleccionarArchivo1.Location = new System.Drawing.Point(12, 75);
            this.btnSeleccionarArchivo1.Name = "btnSeleccionarArchivo1";
            this.btnSeleccionarArchivo1.Size = new System.Drawing.Size(124, 23);
            this.btnSeleccionarArchivo1.TabIndex = 2;
            this.btnSeleccionarArchivo1.Text = "Seleccionar archivo";
            this.btnSeleccionarArchivo1.UseVisualStyleBackColor = false;
            // 
            // btnSeleccionarArchivo2
            // 
            this.btnSeleccionarArchivo2.BackColor = System.Drawing.Color.SlateBlue;
            this.btnSeleccionarArchivo2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSeleccionarArchivo2.Location = new System.Drawing.Point(257, 75);
            this.btnSeleccionarArchivo2.Name = "btnSeleccionarArchivo2";
            this.btnSeleccionarArchivo2.Size = new System.Drawing.Size(124, 23);
            this.btnSeleccionarArchivo2.TabIndex = 3;
            this.btnSeleccionarArchivo2.Text = "Seleccionar archivo";
            this.btnSeleccionarArchivo2.UseVisualStyleBackColor = false;
            this.btnSeleccionarArchivo2.Click += new System.EventHandler(this.btnSeleccionarArchivo2_Click_1);
            // 
            // btnProcesar
            // 
            this.btnProcesar.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.btnProcesar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnProcesar.Location = new System.Drawing.Point(547, 32);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(150, 66);
            this.btnProcesar.TabIndex = 4;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BlueViolet;
            this.ClientSize = new System.Drawing.Size(735, 152);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.btnSeleccionarArchivo2);
            this.Controls.Add(this.btnSeleccionarArchivo1);
            this.Controls.Add(this.txtRutaArchivo1);
            this.Controls.Add(this.txtRutaArchivo2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtRutaArchivo2;
        private TextBox txtRutaArchivo1;
        private Button btnSeleccionarArchivo1;
        private Button btnSeleccionarArchivo2;
        private Button btnProcesar;
    }
}