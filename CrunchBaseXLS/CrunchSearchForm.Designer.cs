namespace CrunchBaseXLS
{
    partial class CrunchSearchForm
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
            this.components = new System.ComponentModel.Container();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SwitchToFieldsButton = new System.Windows.Forms.Button();
            this.SearchResultsGrid = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.permalinkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namespaceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.overviewDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchResultsListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.InsertResultsButton = new System.Windows.Forms.Button();
            this.checkBoxTags = new System.Windows.Forms.CheckBox();
            this.EntityLabel = new System.Windows.Forms.Label();
            this.EntityCombo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.SearchResultsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchResultsListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(119, 21);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(159, 22);
            this.NameTextBox.TabIndex = 1;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(31, 20);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(82, 17);
            this.NameLabel.TabIndex = 4;
            this.NameLabel.Text = "Search For:";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(599, 12);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(79, 39);
            this.SearchButton.TabIndex = 3;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SwitchToFieldsButton
            // 
            this.SwitchToFieldsButton.Location = new System.Drawing.Point(892, 12);
            this.SwitchToFieldsButton.Name = "SwitchToFieldsButton";
            this.SwitchToFieldsButton.Size = new System.Drawing.Size(101, 39);
            this.SwitchToFieldsButton.TabIndex = 5;
            this.SwitchToFieldsButton.Text = "Details >";
            this.SwitchToFieldsButton.UseVisualStyleBackColor = true;
            this.SwitchToFieldsButton.Click += new System.EventHandler(this.SwitchToFieldsButton_Click);
            // 
            // SearchResultsGrid
            // 
            this.SearchResultsGrid.AllowUserToAddRows = false;
            this.SearchResultsGrid.AutoGenerateColumns = false;
            this.SearchResultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SearchResultsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.permalinkDataGridViewTextBoxColumn,
            this.namespaceDataGridViewTextBoxColumn,
            this.overviewDataGridViewTextBoxColumn});
            this.SearchResultsGrid.DataSource = this.searchResultsListBindingSource;
            this.SearchResultsGrid.Location = new System.Drawing.Point(34, 57);
            this.SearchResultsGrid.Name = "SearchResultsGrid";
            this.SearchResultsGrid.RowTemplate.Height = 24;
            this.SearchResultsGrid.Size = new System.Drawing.Size(959, 340);
            this.SearchResultsGrid.TabIndex = 6;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // permalinkDataGridViewTextBoxColumn
            // 
            this.permalinkDataGridViewTextBoxColumn.DataPropertyName = "permalink";
            this.permalinkDataGridViewTextBoxColumn.HeaderText = "permalink";
            this.permalinkDataGridViewTextBoxColumn.Name = "permalinkDataGridViewTextBoxColumn";
            // 
            // namespaceDataGridViewTextBoxColumn
            // 
            this.namespaceDataGridViewTextBoxColumn.DataPropertyName = "Namespace";
            this.namespaceDataGridViewTextBoxColumn.HeaderText = "Namespace";
            this.namespaceDataGridViewTextBoxColumn.Name = "namespaceDataGridViewTextBoxColumn";
            // 
            // overviewDataGridViewTextBoxColumn
            // 
            this.overviewDataGridViewTextBoxColumn.DataPropertyName = "overview";
            this.overviewDataGridViewTextBoxColumn.HeaderText = "overview";
            this.overviewDataGridViewTextBoxColumn.Name = "overviewDataGridViewTextBoxColumn";
            // 
            // searchResultsListBindingSource
            // 
            this.searchResultsListBindingSource.DataSource = typeof(CrunchBaseXLS.SearchResultsList);
            // 
            // InsertResultsButton
            // 
            this.InsertResultsButton.Location = new System.Drawing.Point(684, 12);
            this.InsertResultsButton.Name = "InsertResultsButton";
            this.InsertResultsButton.Size = new System.Drawing.Size(202, 39);
            this.InsertResultsButton.TabIndex = 4;
            this.InsertResultsButton.Text = "Insert Grid Contents to Sheet";
            this.InsertResultsButton.UseVisualStyleBackColor = true;
            this.InsertResultsButton.Click += new System.EventHandler(this.InsertResultsButton_Click);
            // 
            // checkBoxTags
            // 
            this.checkBoxTags.AutoSize = true;
            this.checkBoxTags.Location = new System.Drawing.Point(473, 22);
            this.checkBoxTags.Name = "checkBoxTags";
            this.checkBoxTags.Size = new System.Drawing.Size(111, 21);
            this.checkBoxTags.TabIndex = 2;
            this.checkBoxTags.Text = "Search Tags";
            this.checkBoxTags.UseVisualStyleBackColor = true;
            // 
            // EntityLabel
            // 
            this.EntityLabel.AutoSize = true;
            this.EntityLabel.Location = new System.Drawing.Point(284, 23);
            this.EntityLabel.Name = "EntityLabel";
            this.EntityLabel.Size = new System.Drawing.Size(23, 17);
            this.EntityLabel.TabIndex = 7;
            this.EntityLabel.Text = "in:";
            // 
            // EntityCombo
            // 
            this.EntityCombo.FormattingEnabled = true;
            this.EntityCombo.Items.AddRange(new object[] {
            "Companies",
            "Financial Organizations"});
            this.EntityCombo.Location = new System.Drawing.Point(313, 20);
            this.EntityCombo.Name = "EntityCombo";
            this.EntityCombo.Size = new System.Drawing.Size(154, 24);
            this.EntityCombo.TabIndex = 2;
            // 
            // CrunchSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 418);
            this.Controls.Add(this.EntityCombo);
            this.Controls.Add(this.EntityLabel);
            this.Controls.Add(this.checkBoxTags);
            this.Controls.Add(this.InsertResultsButton);
            this.Controls.Add(this.SearchResultsGrid);
            this.Controls.Add(this.SwitchToFieldsButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.NameLabel);
            this.Name = "CrunchSearchForm";
            this.Text = "Search CrunchBase";
            ((System.ComponentModel.ISupportInitialize)(this.SearchResultsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchResultsListBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button SwitchToFieldsButton;
        private System.Windows.Forms.DataGridView SearchResultsGrid;
        private System.Windows.Forms.Button InsertResultsButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn permalinkDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn namespaceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn overviewDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource searchResultsListBindingSource;
        private System.Windows.Forms.CheckBox checkBoxTags;
        private System.Windows.Forms.Label EntityLabel;
        private System.Windows.Forms.ComboBox EntityCombo;
    }
}