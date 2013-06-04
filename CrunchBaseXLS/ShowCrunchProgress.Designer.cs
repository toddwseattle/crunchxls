namespace CrunchBaseXLS
{
    partial class SearchingCrunchBaseProgressForm
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
            this.TotalLabel = new System.Windows.Forms.Label();
            this.MatchCountLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.RetrievedLabel = new System.Windows.Forms.Label();
            this.RetrievedCountLabel = new System.Windows.Forms.Label();
            this.SCBCancelButton = new System.Windows.Forms.Button();
            this.EstimatedTimeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Location = new System.Drawing.Point(13, 13);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(101, 17);
            this.TotalLabel.TabIndex = 0;
            this.TotalLabel.Text = "Total Matches:";
            // 
            // MatchCountLabel
            // 
            this.MatchCountLabel.AutoSize = true;
            this.MatchCountLabel.Location = new System.Drawing.Point(121, 13);
            this.MatchCountLabel.Name = "MatchCountLabel";
            this.MatchCountLabel.Size = new System.Drawing.Size(77, 17);
            this.MatchCountLabel.TabIndex = 1;
            this.MatchCountLabel.Text = "<Matches>";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 69);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(502, 46);
            this.progressBar1.TabIndex = 2;
            // 
            // RetrievedLabel
            // 
            this.RetrievedLabel.AutoSize = true;
            this.RetrievedLabel.Location = new System.Drawing.Point(13, 39);
            this.RetrievedLabel.Name = "RetrievedLabel";
            this.RetrievedLabel.Size = new System.Drawing.Size(73, 17);
            this.RetrievedLabel.TabIndex = 3;
            this.RetrievedLabel.Text = "Retrieved:";
            // 
            // RetrievedCountLabel
            // 
            this.RetrievedCountLabel.AutoSize = true;
            this.RetrievedCountLabel.Location = new System.Drawing.Point(121, 39);
            this.RetrievedCountLabel.Name = "RetrievedCountLabel";
            this.RetrievedCountLabel.Size = new System.Drawing.Size(77, 17);
            this.RetrievedCountLabel.TabIndex = 4;
            this.RetrievedCountLabel.Text = "<Matches>";
            // 
            // SCBCancelButton
            // 
            this.SCBCancelButton.Location = new System.Drawing.Point(197, 125);
            this.SCBCancelButton.Name = "SCBCancelButton";
            this.SCBCancelButton.Size = new System.Drawing.Size(133, 40);
            this.SCBCancelButton.TabIndex = 5;
            this.SCBCancelButton.Text = "Cancel";
            this.SCBCancelButton.UseVisualStyleBackColor = true;
            this.SCBCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // EstimatedTimeLabel
            // 
            this.EstimatedTimeLabel.AutoSize = true;
            this.EstimatedTimeLabel.Location = new System.Drawing.Point(229, 13);
            this.EstimatedTimeLabel.Name = "EstimatedTimeLabel";
            this.EstimatedTimeLabel.Size = new System.Drawing.Size(109, 17);
            this.EstimatedTimeLabel.TabIndex = 6;
            this.EstimatedTimeLabel.Text = "Estimated Time:";
            // 
            // SearchingCrunchBaseProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 171);
            this.Controls.Add(this.EstimatedTimeLabel);
            this.Controls.Add(this.SCBCancelButton);
            this.Controls.Add(this.RetrievedCountLabel);
            this.Controls.Add(this.RetrievedLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.MatchCountLabel);
            this.Controls.Add(this.TotalLabel);
            this.Name = "SearchingCrunchBaseProgressForm";
            this.Text = "Searching Crunchbase";
            this.Load += new System.EventHandler(this.SearchingCrunchBaseProgressForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.Label MatchCountLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label RetrievedLabel;
        private System.Windows.Forms.Label RetrievedCountLabel;
        private System.Windows.Forms.Button SCBCancelButton;
        private System.Windows.Forms.Label EstimatedTimeLabel;
    }
}