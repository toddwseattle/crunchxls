namespace CrunchBaseXLS
{
    partial class DetailsForm
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
            this.ScopeDesc = new System.Windows.Forms.Label();
            this.TotalEntitiesLabel = new System.Windows.Forms.Label();
            this.AggregateButton = new System.Windows.Forms.Button();
            this.NewSheetCheck = new System.Windows.Forms.CheckBox();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Searching for";
            // 
            // ScopeDesc
            // 
            this.ScopeDesc.AutoSize = true;
            this.ScopeDesc.Location = new System.Drawing.Point(113, 13);
            this.ScopeDesc.Name = "ScopeDesc";
            this.ScopeDesc.Size = new System.Drawing.Size(109, 17);
            this.ScopeDesc.TabIndex = 1;
            this.ScopeDesc.Text = "<search scope>";
            // 
            // TotalEntitiesLabel
            // 
            this.TotalEntitiesLabel.AutoSize = true;
            this.TotalEntitiesLabel.Location = new System.Drawing.Point(241, 13);
            this.TotalEntitiesLabel.Name = "TotalEntitiesLabel";
            this.TotalEntitiesLabel.Size = new System.Drawing.Size(94, 17);
            this.TotalEntitiesLabel.TabIndex = 2;
            this.TotalEntitiesLabel.Text = "Total Entities:";
            // 
            // AggregateButton
            // 
            this.AggregateButton.Location = new System.Drawing.Point(19, 91);
            this.AggregateButton.Name = "AggregateButton";
            this.AggregateButton.Size = new System.Drawing.Size(147, 42);
            this.AggregateButton.TabIndex = 3;
            this.AggregateButton.Text = "Detailed Aggregate Report";
            this.AggregateButton.UseVisualStyleBackColor = true;
            this.AggregateButton.Click += new System.EventHandler(this.AggregateButton_Click);
            // 
            // NewSheetCheck
            // 
            this.NewSheetCheck.AutoSize = true;
            this.NewSheetCheck.Location = new System.Drawing.Point(19, 53);
            this.NewSheetCheck.Name = "NewSheetCheck";
            this.NewSheetCheck.Size = new System.Drawing.Size(144, 21);
            this.NewSheetCheck.TabIndex = 4;
            this.NewSheetCheck.Text = "Create New Sheet";
            this.NewSheetCheck.UseVisualStyleBackColor = true;
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Location = new System.Drawing.Point(357, 13);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(51, 17);
            this.TotalLabel.TabIndex = 5;
            this.TotalLabel.Text = "<total>";
            // 
            // DetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 275);
            this.Controls.Add(this.TotalLabel);
            this.Controls.Add(this.NewSheetCheck);
            this.Controls.Add(this.AggregateButton);
            this.Controls.Add(this.TotalEntitiesLabel);
            this.Controls.Add(this.ScopeDesc);
            this.Controls.Add(this.label1);
            this.Name = "DetailsForm";
            this.Text = "Detailed CrunchBase Report";
            this.Load += new System.EventHandler(this.DetailsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ScopeDesc;
        private System.Windows.Forms.Label TotalEntitiesLabel;
        private System.Windows.Forms.Button AggregateButton;
        private System.Windows.Forms.CheckBox NewSheetCheck;
        private System.Windows.Forms.Label TotalLabel;
    }
}