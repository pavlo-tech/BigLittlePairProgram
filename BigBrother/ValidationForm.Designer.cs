namespace BigBrother
{
    partial class ValidationForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButtonPanel = new System.Windows.Forms.Panel();
            this.RunValidationButton = new System.Windows.Forms.Button();
            this.FinalizeEditsButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BrothersDataGridView = new System.Windows.Forms.DataGridView();
            this.PledgesDataGridView = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrothersDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PledgesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 485);
            this.panel1.TabIndex = 0;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.Controls.Add(this.FinalizeEditsButton);
            this.ButtonPanel.Controls.Add(this.RunValidationButton);
            this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonPanel.Location = new System.Drawing.Point(0, 485);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(1000, 150);
            this.ButtonPanel.TabIndex = 1;
            // 
            // RunValidationButton
            // 
            this.RunValidationButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RunValidationButton.Location = new System.Drawing.Point(349, 50);
            this.RunValidationButton.MaximumSize = new System.Drawing.Size(125, 50);
            this.RunValidationButton.MinimumSize = new System.Drawing.Size(125, 50);
            this.RunValidationButton.Name = "RunValidationButton";
            this.RunValidationButton.Size = new System.Drawing.Size(125, 50);
            this.RunValidationButton.TabIndex = 0;
            this.RunValidationButton.Text = "Run Validation";
            this.RunValidationButton.UseVisualStyleBackColor = true;
            this.RunValidationButton.Click += new System.EventHandler(this.RunValidationButton_Click);
            // 
            // FinalizeEditsButton
            // 
            this.FinalizeEditsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FinalizeEditsButton.Location = new System.Drawing.Point(527, 50);
            this.FinalizeEditsButton.Margin = new System.Windows.Forms.Padding(50);
            this.FinalizeEditsButton.MaximumSize = new System.Drawing.Size(125, 50);
            this.FinalizeEditsButton.MinimumSize = new System.Drawing.Size(125, 50);
            this.FinalizeEditsButton.Name = "FinalizeEditsButton";
            this.FinalizeEditsButton.Size = new System.Drawing.Size(125, 50);
            this.FinalizeEditsButton.TabIndex = 1;
            this.FinalizeEditsButton.Text = "Finalize Edits";
            this.FinalizeEditsButton.UseVisualStyleBackColor = true;
            this.FinalizeEditsButton.Click += new System.EventHandler(this.FinalizeEditsButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.BrothersDataGridView);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.PledgesDataGridView);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 485);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Brothers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pledges";
            // 
            // BrothersDataGridView
            // 
            this.BrothersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BrothersDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BrothersDataGridView.Location = new System.Drawing.Point(0, 39);
            this.BrothersDataGridView.Name = "BrothersDataGridView";
            this.BrothersDataGridView.Size = new System.Drawing.Size(500, 446);
            this.BrothersDataGridView.TabIndex = 1;
            // 
            // PledgesDataGridView
            // 
            this.PledgesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PledgesDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PledgesDataGridView.Location = new System.Drawing.Point(0, 39);
            this.PledgesDataGridView.Name = "PledgesDataGridView";
            this.PledgesDataGridView.Size = new System.Drawing.Size(496, 446);
            this.PledgesDataGridView.TabIndex = 2;
            // 
            // ValidationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 635);
            this.Controls.Add(this.ButtonPanel);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(1016, 674);
            this.Name = "ValidationForm";
            this.Text = "ValidationForm";
            this.panel1.ResumeLayout(false);
            this.ButtonPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BrothersDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PledgesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel ButtonPanel;
        private System.Windows.Forms.Button FinalizeEditsButton;
        private System.Windows.Forms.Button RunValidationButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView BrothersDataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView PledgesDataGridView;
        private System.Windows.Forms.Label label2;
    }
}