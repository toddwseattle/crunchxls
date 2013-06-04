namespace CrunchBaseXLS
{
    partial class CrunchRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CrunchRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CBTab = this.Factory.CreateRibbonTab();
            this.QueryGroup = this.Factory.CreateRibbonGroup();
            this.SearchButton = this.Factory.CreateRibbonButton();
            this.FieldsButton = this.Factory.CreateRibbonButton();
            this.GoButton = this.Factory.CreateRibbonButton();
            this.InsertGroup = this.Factory.CreateRibbonGroup();
            this.CompaniesButon = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.SearchGroup = this.Factory.CreateRibbonGroup();
            this.FreeSearch = this.Factory.CreateRibbonEditBox();
            this.button1 = this.Factory.CreateRibbonButton();
            this.CompanyDetailsButton = this.Factory.CreateRibbonButton();
            this.CBTab.SuspendLayout();
            this.QueryGroup.SuspendLayout();
            this.InsertGroup.SuspendLayout();
            this.SearchGroup.SuspendLayout();
            // 
            // CBTab
            // 
            this.CBTab.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.CBTab.Groups.Add(this.QueryGroup);
            this.CBTab.Groups.Add(this.InsertGroup);
            this.CBTab.Groups.Add(this.SearchGroup);
            this.CBTab.KeyTip = "CB";
            this.CBTab.Label = "CrunchBaseXLS";
            this.CBTab.Name = "CBTab";
            // 
            // QueryGroup
            // 
            this.QueryGroup.Items.Add(this.SearchButton);
            this.QueryGroup.Items.Add(this.FieldsButton);
            this.QueryGroup.Items.Add(this.GoButton);
            this.QueryGroup.Label = "Query";
            this.QueryGroup.Name = "QueryGroup";
            // 
            // SearchButton
            // 
            this.SearchButton.Label = "Search..";
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SearchButton_Click);
            // 
            // FieldsButton
            // 
            this.FieldsButton.Label = "Fields..";
            this.FieldsButton.Name = "FieldsButton";
            // 
            // GoButton
            // 
            this.GoButton.Label = "GO!";
            this.GoButton.Name = "GoButton";
            // 
            // InsertGroup
            // 
            this.InsertGroup.Items.Add(this.CompaniesButon);
            this.InsertGroup.Items.Add(this.button2);
            this.InsertGroup.Label = "Insert All";
            this.InsertGroup.Name = "InsertGroup";
            // 
            // CompaniesButon
            // 
            this.CompaniesButon.Label = "Companies";
            this.CompaniesButon.Name = "CompaniesButon";
            this.CompaniesButon.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CompaniesButon_Click);
            // 
            // button2
            // 
            this.button2.Label = "Finance Firms";
            this.button2.Name = "button2";
            this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click);
            // 
            // SearchGroup
            // 
            this.SearchGroup.Items.Add(this.FreeSearch);
            this.SearchGroup.Items.Add(this.button1);
            this.SearchGroup.Items.Add(this.CompanyDetailsButton);
            this.SearchGroup.Label = "Single Entity";
            this.SearchGroup.Name = "SearchGroup";
            // 
            // FreeSearch
            // 
            this.FreeSearch.Label = "Permalink:";
            this.FreeSearch.Name = "FreeSearch";
            this.FreeSearch.Text = null;
            // 
            // button1
            // 
            this.button1.Label = "Insert";
            this.button1.Name = "button1";
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // CompanyDetailsButton
            // 
            this.CompanyDetailsButton.Label = "Company Details";
            this.CompanyDetailsButton.Name = "CompanyDetailsButton";
            this.CompanyDetailsButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CompanyDetailsButton_Click_1);
            // 
            // CrunchRibbon
            // 
            this.Name = "CrunchRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.CBTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon1_Load);
            this.CBTab.ResumeLayout(false);
            this.CBTab.PerformLayout();
            this.QueryGroup.ResumeLayout(false);
            this.QueryGroup.PerformLayout();
            this.InsertGroup.ResumeLayout(false);
            this.InsertGroup.PerformLayout();
            this.SearchGroup.ResumeLayout(false);
            this.SearchGroup.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab CBTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup InsertGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton CompaniesButon;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup SearchGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox FreeSearch;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup QueryGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton SearchButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton FieldsButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton GoButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton CompanyDetailsButton;
    }

    partial class ThisRibbonCollection
    {
        internal CrunchRibbon Ribbon1
        {
            get { return this.GetRibbon<CrunchRibbon>(); }
        }
    }
}
