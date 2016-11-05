namespace eZstd.Test
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
            this.eZDataGridView1 = new eZstd.UserControls.eZDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // eZDataGridView1
            // 
            this.eZDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eZDataGridView1.KeyDelete = false;
            this.eZDataGridView1.Location = new System.Drawing.Point(13, 13);
            this.eZDataGridView1.ManipulateRows = false;
            this.eZDataGridView1.Name = "eZDataGridView1";
            this.eZDataGridView1.RowTemplate.Height = 23;
            this.eZDataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eZDataGridView1.ShowRowNumber = false;
            this.eZDataGridView1.Size = new System.Drawing.Size(346, 110);
            this.eZDataGridView1.SupportPaste = false;
            this.eZDataGridView1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.eZDataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.eZDataGridView eZDataGridView1;
    }
}