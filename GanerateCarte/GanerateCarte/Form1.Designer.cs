namespace GanerateCarte
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtMax = new System.Windows.Forms.Label();
            this.txtNombre1 = new DevExpress.XtraEditors.SpinEdit();
            this.txtNombre2 = new DevExpress.XtraEditors.SpinEdit();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(316, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre de Code a Generer";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(279, 82);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(292, 26);
            this.txtCode.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(328, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 43);
            this.button1.TabIndex = 2;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMax
            // 
            this.txtMax.AutoSize = true;
            this.txtMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMax.Location = new System.Drawing.Point(366, 280);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(76, 82);
            this.txtMax.TabIndex = 3;
            this.txtMax.Text = "0";
            this.txtMax.Click += new System.EventHandler(this.txtMax_Click);
            // 
            // txtNombre1
            // 
            this.txtNombre1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNombre1.Location = new System.Drawing.Point(237, 409);
            this.txtNombre1.Name = "txtNombre1";
            this.txtNombre1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre1.Properties.Appearance.Options.UseFont = true;
            this.txtNombre1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtNombre1.Size = new System.Drawing.Size(185, 30);
            this.txtNombre1.TabIndex = 4;
            // 
            // txtNombre2
            // 
            this.txtNombre2.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNombre2.Location = new System.Drawing.Point(444, 409);
            this.txtNombre2.Name = "txtNombre2";
            this.txtNombre2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre2.Properties.Appearance.Options.UseFont = true;
            this.txtNombre2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtNombre2.Size = new System.Drawing.Size(192, 30);
            this.txtNombre2.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(348, 457);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(173, 43);
            this.button2.TabIndex = 6;
            this.button2.Text = "Imprimer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 582);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtNombre2);
            this.Controls.Add(this.txtNombre1);
            this.Controls.Add(this.txtMax);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre2.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label txtMax;
        private DevExpress.XtraEditors.SpinEdit txtNombre1;
        private DevExpress.XtraEditors.SpinEdit txtNombre2;
        private System.Windows.Forms.Button button2;
    }
}

