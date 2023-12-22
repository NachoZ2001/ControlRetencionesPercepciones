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
            txtRutaArchivo2 = new TextBox();
            txtRutaArchivo1 = new TextBox();
            btnSeleccionarArchivo1 = new Button();
            btnSeleccionarArchivo2 = new Button();
            btnProcesar = new Button();
            SuspendLayout();
            // 
            // txtRutaArchivo2
            // 
            txtRutaArchivo2.Location = new Point(438, 32);
            txtRutaArchivo2.Name = "txtRutaArchivo2";
            txtRutaArchivo2.Size = new Size(100, 23);
            txtRutaArchivo2.TabIndex = 0;
            txtRutaArchivo2.Text = "txtRutaArchivo2";
            // 
            // txtRutaArchivo1
            // 
            txtRutaArchivo1.Location = new Point(12, 32);
            txtRutaArchivo1.Name = "txtRutaArchivo1";
            txtRutaArchivo1.Size = new Size(100, 23);
            txtRutaArchivo1.TabIndex = 1;
            txtRutaArchivo1.Text = "txtRutaArchivo1 ";
            // 
            // btnSeleccionarArchivo1
            // 
            btnSeleccionarArchivo1.Location = new Point(116, 75);
            btnSeleccionarArchivo1.Name = "btnSeleccionarArchivo1";
            btnSeleccionarArchivo1.Size = new Size(75, 23);
            btnSeleccionarArchivo1.TabIndex = 2;
            btnSeleccionarArchivo1.Text = "button1";
            btnSeleccionarArchivo1.UseVisualStyleBackColor = true;
            btnSeleccionarArchivo1.Click += btnSeleccionarArchivo1_Click;
            // 
            // btnSeleccionarArchivo2
            // 
            btnSeleccionarArchivo2.Location = new Point(361, 75);
            btnSeleccionarArchivo2.Name = "btnSeleccionarArchivo2";
            btnSeleccionarArchivo2.Size = new Size(75, 23);
            btnSeleccionarArchivo2.TabIndex = 3;
            btnSeleccionarArchivo2.Text = "button2";
            btnSeleccionarArchivo2.UseVisualStyleBackColor = true;
            btnSeleccionarArchivo2.Click += btnSeleccionarArchivo2_Click;
            // 
            // btnProcesar
            // 
            btnProcesar.Location = new Point(232, 75);
            btnProcesar.Name = "btnProcesar";
            btnProcesar.Size = new Size(97, 23);
            btnProcesar.TabIndex = 4;
            btnProcesar.Text = "buttonProcesar";
            btnProcesar.UseVisualStyleBackColor = true;
            btnProcesar.Click += btnProcesar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnProcesar);
            Controls.Add(btnSeleccionarArchivo2);
            Controls.Add(btnSeleccionarArchivo1);
            Controls.Add(txtRutaArchivo1);
            Controls.Add(txtRutaArchivo2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtRutaArchivo2;
        private TextBox txtRutaArchivo1;
        private Button btnSeleccionarArchivo1;
        private Button btnSeleccionarArchivo2;
        private Button btnProcesar;
    }
}