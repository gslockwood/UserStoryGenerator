namespace UserStoryGenerator.View
{
    partial class MainForm
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
            if( disposing && ( components != null ) )
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
            components = new System.ComponentModel.Container();
            buttonConvert = new Button();
            tableLayoutPanelData = new TableLayoutPanel();
            flowLayoutPanelEpic = new FlowLayoutPanel();
            labelEpic = new Label();
            textBoxEpic = new TextBox();
            tableLayoutPanelControls = new TableLayoutPanel();
            progressBar = new ProgressBar();
            flowLayoutPanelCheckBoxes = new FlowLayoutPanel();
            checkBoxAddUnitTests = new CheckBox();
            checkBoxAddQATests = new CheckBox();
            textBoxText = new TextBox();
            flowLayoutPanelProduct = new FlowLayoutPanel();
            Product = new Label();
            textBoxProduct = new TextBox();
            treeView = new TriStateTreeView();
            columnHeaderUserStorySubject = new ColumnHeader();
            flowLayoutPanelMainButtons = new FlowLayoutPanel();
            buttonSave = new Button();
            tableLayoutPanelMainData = new TableLayoutPanel();
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelData.SuspendLayout();
            flowLayoutPanelEpic.SuspendLayout();
            tableLayoutPanelControls.SuspendLayout();
            flowLayoutPanelCheckBoxes.SuspendLayout();
            flowLayoutPanelProduct.SuspendLayout();
            flowLayoutPanelMainButtons.SuspendLayout();
            tableLayoutPanelMainData.SuspendLayout();
            tableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // buttonConvert
            // 
            buttonConvert.Location = new Point(502, 3);
            buttonConvert.Name = "buttonConvert";
            buttonConvert.Size = new Size(64, 22);
            buttonConvert.TabIndex = 0;
            buttonConvert.Text = "Convert";
            buttonConvert.UseVisualStyleBackColor = true;
            buttonConvert.Click +=  Convert_Click ;
            // 
            // tableLayoutPanelData
            // 
            tableLayoutPanelData.ColumnCount = 1;
            tableLayoutPanelData.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelData.Controls.Add(flowLayoutPanelEpic, 0, 1);
            tableLayoutPanelData.Controls.Add(tableLayoutPanelControls, 0, 3);
            tableLayoutPanelData.Controls.Add(textBoxText, 0, 2);
            tableLayoutPanelData.Controls.Add(flowLayoutPanelProduct, 0, 0);
            tableLayoutPanelData.Dock = DockStyle.Fill;
            tableLayoutPanelData.Location = new Point(3, 3);
            tableLayoutPanelData.Name = "tableLayoutPanelData";
            tableLayoutPanelData.RowCount = 4;
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F));
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            tableLayoutPanelData.Size = new Size(575, 672);
            tableLayoutPanelData.TabIndex = 1;
            // 
            // flowLayoutPanelEpic
            // 
            flowLayoutPanelEpic.Controls.Add(labelEpic);
            flowLayoutPanelEpic.Controls.Add(textBoxEpic);
            flowLayoutPanelEpic.Dock = DockStyle.Fill;
            flowLayoutPanelEpic.Location = new Point(3, 40);
            flowLayoutPanelEpic.Name = "flowLayoutPanelEpic";
            flowLayoutPanelEpic.Size = new Size(569, 31);
            flowLayoutPanelEpic.TabIndex = 3;
            // 
            // labelEpic
            // 
            labelEpic.Location = new Point(4, 4);
            labelEpic.Margin = new Padding(4);
            labelEpic.Name = "labelEpic";
            labelEpic.Size = new Size(49, 20);
            labelEpic.TabIndex = 0;
            labelEpic.Text = "Epic";
            labelEpic.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBoxEpic
            // 
            textBoxEpic.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            textBoxEpic.Location = new Point(60, 3);
            textBoxEpic.Name = "textBoxEpic";
            textBoxEpic.Size = new Size(506, 23);
            textBoxEpic.TabIndex = 1;
            textBoxEpic.TextChanged +=  TextBoxEpic_TextChanged ;
            // 
            // tableLayoutPanelControls
            // 
            tableLayoutPanelControls.ColumnCount = 3;
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tableLayoutPanelControls.Controls.Add(buttonConvert, 2, 0);
            tableLayoutPanelControls.Controls.Add(progressBar, 1, 0);
            tableLayoutPanelControls.Controls.Add(flowLayoutPanelCheckBoxes, 0, 0);
            tableLayoutPanelControls.Dock = DockStyle.Fill;
            tableLayoutPanelControls.Location = new Point(3, 641);
            tableLayoutPanelControls.Name = "tableLayoutPanelControls";
            tableLayoutPanelControls.RowCount = 1;
            tableLayoutPanelControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelControls.Size = new Size(569, 28);
            tableLayoutPanelControls.TabIndex = 2;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(403, 4);
            progressBar.Margin = new Padding(4);
            progressBar.MarqueeAnimationSpeed = 30;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(92, 18);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 1;
            progressBar.Visible = false;
            // 
            // flowLayoutPanelCheckBoxes
            // 
            flowLayoutPanelCheckBoxes.Controls.Add(checkBoxAddUnitTests);
            flowLayoutPanelCheckBoxes.Controls.Add(checkBoxAddQATests);
            flowLayoutPanelCheckBoxes.Dock = DockStyle.Fill;
            flowLayoutPanelCheckBoxes.Location = new Point(3, 3);
            flowLayoutPanelCheckBoxes.Name = "flowLayoutPanelCheckBoxes";
            flowLayoutPanelCheckBoxes.Size = new Size(393, 22);
            flowLayoutPanelCheckBoxes.TabIndex = 2;
            // 
            // checkBoxAddUnitTests
            // 
            checkBoxAddUnitTests.AutoSize = true;
            checkBoxAddUnitTests.Location = new Point(3, 3);
            checkBoxAddUnitTests.Name = "checkBoxAddUnitTests";
            checkBoxAddUnitTests.Size = new Size(102, 19);
            checkBoxAddUnitTests.TabIndex = 0;
            checkBoxAddUnitTests.Text = "Add Unit Tests";
            checkBoxAddUnitTests.UseVisualStyleBackColor = true;
            checkBoxAddUnitTests.CheckedChanged +=  CheckBoxAddUnitTests_CheckedChanged ;
            // 
            // checkBoxAddQATests
            // 
            checkBoxAddQATests.AutoSize = true;
            checkBoxAddQATests.Location = new Point(111, 3);
            checkBoxAddQATests.Name = "checkBoxAddQATests";
            checkBoxAddQATests.Size = new Size(97, 19);
            checkBoxAddQATests.TabIndex = 1;
            checkBoxAddQATests.Text = "Add QA Tests";
            checkBoxAddQATests.UseVisualStyleBackColor = true;
            checkBoxAddQATests.CheckedChanged +=  CheckBoxAddQATests_CheckedChanged ;
            // 
            // textBoxText
            // 
            textBoxText.AllowDrop = true;
            textBoxText.Dock = DockStyle.Fill;
            textBoxText.Location = new Point(2, 76);
            textBoxText.Margin = new Padding(2);
            textBoxText.Multiline = true;
            textBoxText.Name = "textBoxText";
            textBoxText.Size = new Size(571, 560);
            textBoxText.TabIndex = 1;
            textBoxText.TextChanged +=  TextControls_TextChanged ;
            // 
            // flowLayoutPanelProduct
            // 
            flowLayoutPanelProduct.Controls.Add(Product);
            flowLayoutPanelProduct.Controls.Add(textBoxProduct);
            flowLayoutPanelProduct.Dock = DockStyle.Fill;
            flowLayoutPanelProduct.Location = new Point(3, 3);
            flowLayoutPanelProduct.Name = "flowLayoutPanelProduct";
            flowLayoutPanelProduct.Size = new Size(569, 31);
            flowLayoutPanelProduct.TabIndex = 2;
            // 
            // Product
            // 
            Product.Location = new Point(4, 4);
            Product.Margin = new Padding(4);
            Product.Name = "Product";
            Product.Size = new Size(70, 20);
            Product.TabIndex = 0;
            Product.Text = "Jira Product";
            Product.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBoxProduct
            // 
            textBoxProduct.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            textBoxProduct.Location = new Point(81, 3);
            textBoxProduct.Name = "textBoxProduct";
            textBoxProduct.Size = new Size(485, 23);
            textBoxProduct.TabIndex = 1;
            textBoxProduct.TextChanged +=  TextControls_TextChanged ;
            // 
            // treeView
            // 
            treeView.Anchor =    AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            treeView.Location = new Point(584, 3);
            treeView.Name = "treeView";
            treeView.Size = new Size(1213, 672);
            treeView.TabIndex = 2;
            treeView.TriStateStyleProperty = TriStateTreeView.TriStateStyles.Standard;
            // 
            // columnHeaderUserStorySubject
            // 
            columnHeaderUserStorySubject.Text = "Issue";
            columnHeaderUserStorySubject.Width = 70;
            // 
            // flowLayoutPanelMainButtons
            // 
            flowLayoutPanelMainButtons.Controls.Add(buttonSave);
            flowLayoutPanelMainButtons.Dock = DockStyle.Fill;
            flowLayoutPanelMainButtons.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanelMainButtons.Location = new Point(3, 687);
            flowLayoutPanelMainButtons.Name = "flowLayoutPanelMainButtons";
            flowLayoutPanelMainButtons.Size = new Size(1800, 26);
            flowLayoutPanelMainButtons.TabIndex = 3;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(1722, 3);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 0;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click +=  ButtonSave_Click ;
            buttonSave.Enabled = false;
            // 
            // tableLayoutPanelMainData
            // 
            tableLayoutPanelMainData.ColumnCount = 2;
            tableLayoutPanelMainData.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 581F));
            tableLayoutPanelMainData.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMainData.Controls.Add(treeView, 1, 0);
            tableLayoutPanelMainData.Controls.Add(tableLayoutPanelData, 0, 0);
            tableLayoutPanelMainData.Dock = DockStyle.Fill;
            tableLayoutPanelMainData.Location = new Point(3, 3);
            tableLayoutPanelMainData.Name = "tableLayoutPanelMainData";
            tableLayoutPanelMainData.RowCount = 1;
            tableLayoutPanelMainData.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMainData.Size = new Size(1800, 678);
            tableLayoutPanelMainData.TabIndex = 4;
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(flowLayoutPanelMainButtons, 0, 1);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelMainData, 0, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanelMain.Size = new Size(1806, 716);
            tableLayoutPanelMain.TabIndex = 5;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1806, 716);
            Controls.Add(tableLayoutPanelMain);
            Name = "MainForm";
            Text = "Tell me a story...";
            tableLayoutPanelData.ResumeLayout(false);
            tableLayoutPanelData.PerformLayout();
            flowLayoutPanelEpic.ResumeLayout(false);
            flowLayoutPanelEpic.PerformLayout();
            tableLayoutPanelControls.ResumeLayout(false);
            flowLayoutPanelCheckBoxes.ResumeLayout(false);
            flowLayoutPanelCheckBoxes.PerformLayout();
            flowLayoutPanelProduct.ResumeLayout(false);
            flowLayoutPanelProduct.PerformLayout();
            flowLayoutPanelMainButtons.ResumeLayout(false);
            tableLayoutPanelMainData.ResumeLayout(false);
            tableLayoutPanelMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TriStateTreeView treeView;
        private Button buttonConvert;
        private TableLayoutPanel tableLayoutPanelData;
        private TextBox textBoxText;
        private ColumnHeader columnHeaderUserStorySubject;
        private ProgressBar progressBar;
        private FlowLayoutPanel flowLayoutPanelMainButtons;
        private Button buttonSave;
        private TableLayoutPanel tableLayoutPanelMainData;
        private TableLayoutPanel tableLayoutPanelMain;
        private FlowLayoutPanel flowLayoutPanelProduct;
        private TextBox textBoxProduct;
        private Label Product;
        private FlowLayoutPanel flowLayoutPanelEpic;
        private Label labelEpic;
        private TextBox textBoxEpic;
        private TableLayoutPanel tableLayoutPanelControls;
        private FlowLayoutPanel flowLayoutPanelCheckBoxes;
        private CheckBox checkBoxAddUnitTests;
        private CheckBox checkBoxAddQATests;
    }


}
