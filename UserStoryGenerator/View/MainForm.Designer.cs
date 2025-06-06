using System.Collections.Generic;
using System.Windows.Forms;

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
            flowLayoutPanelProductName = new FlowLayoutPanel();
            labelProductName = new Label();
            textBoxProductName = new TextBox();
            flowLayoutPanelEpic = new FlowLayoutPanel();
            labelEpic = new Label();
            textBoxEpic = new TextBox();
            tableLayoutPanelControls = new TableLayoutPanel();
            progressBar = new ProgressBar();
            flowLayoutPanelCheckBoxes = new FlowLayoutPanel();
            checkBoxAddSubTasks = new CheckBox();
            checkBoxAddQATests = new CheckBox();
            textBoxPRD = new GroupBoxEx();
            flowLayoutPanelProduct = new FlowLayoutPanel();
            Product = new Label();
            comboBoxJiraProjects = new ComboBoxEx();
            treeView = new TriStateTreeView();
            panelResults = new Panel();
            tableLayoutPanelResults = new TableLayoutPanel();
            tableLayoutPanelResultsBottom = new TableLayoutPanelEx();
            groupBoxExSelectedSubTasks = new GroupBoxEx();
            groupBoxExSelectedBugs = new GroupBoxEx();
            groupBoxExSelectedTests = new GroupBoxEx();
            groupBoxExSelectedTasks = new GroupBoxEx();
            groupBoxExSelectedStories = new GroupBoxEx();
            groupBoxExSelectedIssues = new GroupBoxEx();
            tableLayoutPanelResultsTop = new TableLayoutPanelEx();
            groupBoxExSubTask = new GroupBoxEx();
            groupBoxExBug = new GroupBoxEx();
            groupBoxExTest = new GroupBoxEx();
            groupBoxExTask = new GroupBoxEx();
            groupBoxExStory = new GroupBoxEx();
            stopwatchClockConvertRun = new StopwatchClock();
            groupBoxExIssueCount = new GroupBoxEx();
            groupBoxExDuration = new GroupBoxEx();
            buttonProcessStories = new Button();
            columnHeaderUserStorySubject = new ColumnHeader();
            flowLayoutPanelMainButtons = new FlowLayoutPanel();
            buttonSave = new Button();
            labelStatus = new Label();
            tableLayoutPanelMainData = new TableLayoutPanel();
            tableLayoutPanelMain = new TableLayoutPanel();
            menuStrip1 = new MenuStrip();
            preferencesToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanelData.SuspendLayout();
            flowLayoutPanelProductName.SuspendLayout();
            flowLayoutPanelEpic.SuspendLayout();
            tableLayoutPanelControls.SuspendLayout();
            flowLayoutPanelCheckBoxes.SuspendLayout();
            flowLayoutPanelProduct.SuspendLayout();
            panelResults.SuspendLayout();
            tableLayoutPanelResults.SuspendLayout();
            tableLayoutPanelResultsBottom.SuspendLayout();
            tableLayoutPanelResultsTop.SuspendLayout();
            flowLayoutPanelMainButtons.SuspendLayout();
            tableLayoutPanelMainData.SuspendLayout();
            tableLayoutPanelMain.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonConvert
            // 
            buttonConvert.Location = new Point(575, 4);
            buttonConvert.Margin = new Padding(3, 4, 3, 4);
            buttonConvert.Name = "buttonConvert";
            buttonConvert.Size = new Size(73, 29);
            buttonConvert.TabIndex = 0;
            buttonConvert.Text = "Convert";
            buttonConvert.UseVisualStyleBackColor = true;
            buttonConvert.Click +=  Convert_Click ;
            // 
            // tableLayoutPanelData
            // 
            tableLayoutPanelData.ColumnCount = 1;
            tableLayoutPanelData.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelData.Controls.Add(flowLayoutPanelProductName, 0, 2);
            tableLayoutPanelData.Controls.Add(flowLayoutPanelEpic, 0, 1);
            tableLayoutPanelData.Controls.Add(tableLayoutPanelControls, 0, 4);
            tableLayoutPanelData.Controls.Add(textBoxPRD, 0, 3);
            tableLayoutPanelData.Controls.Add(flowLayoutPanelProduct, 0, 0);
            tableLayoutPanelData.Dock = DockStyle.Fill;
            tableLayoutPanelData.Location = new Point(3, 4);
            tableLayoutPanelData.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelData.Name = "tableLayoutPanelData";
            tableLayoutPanelData.RowCount = 5;
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanelData.Size = new Size(658, 871);
            tableLayoutPanelData.TabIndex = 1;
            // 
            // flowLayoutPanelProductName
            // 
            flowLayoutPanelProductName.Controls.Add(labelProductName);
            flowLayoutPanelProductName.Controls.Add(textBoxProductName);
            flowLayoutPanelProductName.Dock = DockStyle.Fill;
            flowLayoutPanelProductName.Location = new Point(3, 102);
            flowLayoutPanelProductName.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelProductName.Name = "flowLayoutPanelProductName";
            flowLayoutPanelProductName.Size = new Size(652, 41);
            flowLayoutPanelProductName.TabIndex = 4;
            // 
            // labelProductName
            // 
            labelProductName.Location = new Point(5, 5);
            labelProductName.Margin = new Padding(5);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new Size(116, 27);
            labelProductName.TabIndex = 0;
            labelProductName.Text = "Product/Feature";
            labelProductName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBoxProductName
            // 
            textBoxProductName.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            textBoxProductName.Location = new Point(129, 4);
            textBoxProductName.Margin = new Padding(3, 4, 3, 4);
            textBoxProductName.Name = "textBoxProductName";
            textBoxProductName.PlaceholderText = "enter the product or feature name (required)";
            textBoxProductName.Size = new Size(519, 27);
            textBoxProductName.TabIndex = 1;
            textBoxProductName.TextChanged +=  TextControls_TextChanged ;
            // 
            // flowLayoutPanelEpic
            // 
            flowLayoutPanelEpic.Controls.Add(labelEpic);
            flowLayoutPanelEpic.Controls.Add(textBoxEpic);
            flowLayoutPanelEpic.Dock = DockStyle.Fill;
            flowLayoutPanelEpic.Location = new Point(3, 53);
            flowLayoutPanelEpic.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelEpic.Name = "flowLayoutPanelEpic";
            flowLayoutPanelEpic.Size = new Size(652, 41);
            flowLayoutPanelEpic.TabIndex = 3;
            // 
            // labelEpic
            // 
            labelEpic.Location = new Point(5, 5);
            labelEpic.Margin = new Padding(5);
            labelEpic.Name = "labelEpic";
            labelEpic.Size = new Size(116, 27);
            labelEpic.TabIndex = 0;
            labelEpic.Text = "Epic";
            labelEpic.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBoxEpic
            // 
            textBoxEpic.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            textBoxEpic.Location = new Point(129, 4);
            textBoxEpic.Margin = new Padding(3, 4, 3, 4);
            textBoxEpic.Name = "textBoxEpic";
            textBoxEpic.PlaceholderText = "enter existing epic Key, enter a title of new epic, or leave blank";
            textBoxEpic.Size = new Size(519, 27);
            textBoxEpic.TabIndex = 1;
            textBoxEpic.TextChanged +=  TextBoxEpic_TextChanged ;
            // 
            // tableLayoutPanelControls
            // 
            tableLayoutPanelControls.ColumnCount = 3;
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 114F));
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanelControls.Controls.Add(buttonConvert, 2, 0);
            tableLayoutPanelControls.Controls.Add(progressBar, 1, 0);
            tableLayoutPanelControls.Controls.Add(flowLayoutPanelCheckBoxes, 0, 0);
            tableLayoutPanelControls.Dock = DockStyle.Fill;
            tableLayoutPanelControls.Location = new Point(3, 830);
            tableLayoutPanelControls.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelControls.Name = "tableLayoutPanelControls";
            tableLayoutPanelControls.RowCount = 1;
            tableLayoutPanelControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelControls.Size = new Size(652, 37);
            tableLayoutPanelControls.TabIndex = 2;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(463, 5);
            progressBar.Margin = new Padding(5);
            progressBar.MarqueeAnimationSpeed = 30;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(104, 24);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 1;
            progressBar.Visible = false;
            // 
            // flowLayoutPanelCheckBoxes
            // 
            flowLayoutPanelCheckBoxes.Controls.Add(checkBoxAddSubTasks);
            flowLayoutPanelCheckBoxes.Controls.Add(checkBoxAddQATests);
            flowLayoutPanelCheckBoxes.Dock = DockStyle.Fill;
            flowLayoutPanelCheckBoxes.Location = new Point(3, 4);
            flowLayoutPanelCheckBoxes.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelCheckBoxes.Name = "flowLayoutPanelCheckBoxes";
            flowLayoutPanelCheckBoxes.Size = new Size(452, 29);
            flowLayoutPanelCheckBoxes.TabIndex = 2;
            // 
            // checkBoxAddSubTasks
            // 
            checkBoxAddSubTasks.AutoSize = true;
            checkBoxAddSubTasks.Location = new Point(3, 4);
            checkBoxAddSubTasks.Margin = new Padding(3, 4, 3, 4);
            checkBoxAddSubTasks.Name = "checkBoxAddSubTasks";
            checkBoxAddSubTasks.Size = new Size(118, 24);
            checkBoxAddSubTasks.TabIndex = 0;
            checkBoxAddSubTasks.Text = "Add SubTasks";
            checkBoxAddSubTasks.UseVisualStyleBackColor = true;
            // 
            // checkBoxAddQATests
            // 
            checkBoxAddQATests.AutoSize = true;
            checkBoxAddQATests.Location = new Point(127, 4);
            checkBoxAddQATests.Margin = new Padding(3, 4, 3, 4);
            checkBoxAddQATests.Name = "checkBoxAddQATests";
            checkBoxAddQATests.Size = new Size(117, 24);
            checkBoxAddQATests.TabIndex = 1;
            checkBoxAddQATests.Text = "Add QA Tests";
            checkBoxAddQATests.UseVisualStyleBackColor = true;
            // 
            // textBoxPRD
            // 
            textBoxPRD.AllowDrop = true;
            textBoxPRD.CaptionText = "Product Description";
            textBoxPRD.Dock = DockStyle.Fill;
            textBoxPRD.Location = new Point(2, 150);
            textBoxPRD.Margin = new Padding(2, 3, 2, 3);
            textBoxPRD.Multiline = true;
            textBoxPRD.Name = "textBoxPRD";
            textBoxPRD.PlaceholderText = "enter the product feature plain text description (required)";
            textBoxPRD.ReadOnly = false;
            textBoxPRD.Size = new Size(654, 673);
            textBoxPRD.TabIndex = 1;
            textBoxPRD.TextAlign = HorizontalAlignment.Left;
            textBoxPRD.UseSystemPasswordChar = false;
            textBoxPRD.Value = "";
            textBoxPRD.TextChanged +=  TextControls_TextChanged ;
            // 
            // flowLayoutPanelProduct
            // 
            flowLayoutPanelProduct.Controls.Add(Product);
            flowLayoutPanelProduct.Controls.Add(comboBoxJiraProjects);
            flowLayoutPanelProduct.Dock = DockStyle.Fill;
            flowLayoutPanelProduct.Location = new Point(3, 4);
            flowLayoutPanelProduct.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelProduct.Name = "flowLayoutPanelProduct";
            flowLayoutPanelProduct.Size = new Size(652, 41);
            flowLayoutPanelProduct.TabIndex = 2;
            // 
            // Product
            // 
            Product.Location = new Point(5, 5);
            Product.Margin = new Padding(5);
            Product.Name = "Product";
            Product.Size = new Size(116, 27);
            Product.TabIndex = 0;
            Product.Text = "Jira Product";
            Product.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comboBoxJiraProjects
            // 
            comboBoxJiraProjects.FormattingEnabled = true;
            comboBoxJiraProjects.Location = new Point(129, 4);
            comboBoxJiraProjects.Margin = new Padding(3, 4, 3, 4);
            comboBoxJiraProjects.Name = "comboBoxJiraProjects";
            comboBoxJiraProjects.Size = new Size(259, 28);
            comboBoxJiraProjects.TabIndex = 1;
            // 
            // treeView
            // 
            treeView.AllowDrop = true;
            treeView.Dock = DockStyle.Fill;
            treeView.Location = new Point(3, 79);
            treeView.Margin = new Padding(3, 4, 3, 4);
            treeView.Name = "treeView";
            treeView.ShowNodeToolTips = true;
            treeView.Size = new Size(1382, 713);
            treeView.TabIndex = 2;
            treeView.TriStateStyleProperty = TriStateTreeView.TriStateStyles.Standard;
            treeView.DragDrop +=  TreeView_DragDrop ;
            treeView.DragEnter +=  TreeView_DragEnter ;
            // 
            // panelResults
            // 
            panelResults.Controls.Add(tableLayoutPanelResults);
            panelResults.Dock = DockStyle.Fill;
            panelResults.Location = new Point(667, 4);
            panelResults.Margin = new Padding(3, 4, 3, 4);
            panelResults.Name = "panelResults";
            panelResults.Size = new Size(1388, 871);
            panelResults.TabIndex = 2;
            // 
            // tableLayoutPanelResults
            // 
            tableLayoutPanelResults.ColumnCount = 1;
            tableLayoutPanelResults.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelResults.Controls.Add(tableLayoutPanelResultsBottom, 0, 2);
            tableLayoutPanelResults.Controls.Add(tableLayoutPanelResultsTop, 0, 0);
            tableLayoutPanelResults.Controls.Add(treeView, 0, 1);
            tableLayoutPanelResults.Dock = DockStyle.Fill;
            tableLayoutPanelResults.Location = new Point(0, 0);
            tableLayoutPanelResults.Name = "tableLayoutPanelResults";
            tableLayoutPanelResults.RowCount = 3;
            tableLayoutPanelResults.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tableLayoutPanelResults.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelResults.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tableLayoutPanelResults.Size = new Size(1388, 871);
            tableLayoutPanelResults.TabIndex = 0;
            // 
            // tableLayoutPanelResultsBottom
            // 
            tableLayoutPanelResultsBottom.ColumnCount = 9;
            tableLayoutPanelResultsBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanelResultsBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelResultsBottom.Controls.Add(groupBoxExSelectedSubTasks, 7, 0);
            tableLayoutPanelResultsBottom.Controls.Add(groupBoxExSelectedBugs, 6, 0);
            tableLayoutPanelResultsBottom.Controls.Add(groupBoxExSelectedTests, 5, 0);
            tableLayoutPanelResultsBottom.Controls.Add(groupBoxExSelectedTasks, 4, 0);
            tableLayoutPanelResultsBottom.Controls.Add(groupBoxExSelectedStories, 3, 0);
            tableLayoutPanelResultsBottom.Controls.Add(groupBoxExSelectedIssues, 2, 0);
            tableLayoutPanelResultsBottom.Dock = DockStyle.Fill;
            tableLayoutPanelResultsBottom.Location = new Point(3, 799);
            tableLayoutPanelResultsBottom.Name = "tableLayoutPanelResultsBottom";
            tableLayoutPanelResultsBottom.RowCount = 1;
            tableLayoutPanelResultsBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelResultsBottom.Size = new Size(1382, 69);
            tableLayoutPanelResultsBottom.TabIndex = 3;
            // 
            // groupBoxExSelectedSubTasks
            // 
            groupBoxExSelectedSubTasks.CaptionText = "SubTasks";
            groupBoxExSelectedSubTasks.Dock = DockStyle.Fill;
            groupBoxExSelectedSubTasks.Location = new Point(823, 3);
            groupBoxExSelectedSubTasks.Multiline = false;
            groupBoxExSelectedSubTasks.Name = "groupBoxExSelectedSubTasks";
            groupBoxExSelectedSubTasks.PlaceholderText = "";
            groupBoxExSelectedSubTasks.ReadOnly = true;
            groupBoxExSelectedSubTasks.Size = new Size(114, 63);
            groupBoxExSelectedSubTasks.TabIndex = 7;
            groupBoxExSelectedSubTasks.TextAlign = HorizontalAlignment.Right;
            groupBoxExSelectedSubTasks.UseSystemPasswordChar = false;
            groupBoxExSelectedSubTasks.Value = "";
            // 
            // groupBoxExSelectedBugs
            // 
            groupBoxExSelectedBugs.CaptionText = "Bugs";
            groupBoxExSelectedBugs.Dock = DockStyle.Fill;
            groupBoxExSelectedBugs.Location = new Point(703, 3);
            groupBoxExSelectedBugs.Multiline = false;
            groupBoxExSelectedBugs.Name = "groupBoxExSelectedBugs";
            groupBoxExSelectedBugs.PlaceholderText = "";
            groupBoxExSelectedBugs.ReadOnly = true;
            groupBoxExSelectedBugs.Size = new Size(114, 63);
            groupBoxExSelectedBugs.TabIndex = 6;
            groupBoxExSelectedBugs.TextAlign = HorizontalAlignment.Right;
            groupBoxExSelectedBugs.UseSystemPasswordChar = false;
            groupBoxExSelectedBugs.Value = "";
            // 
            // groupBoxExSelectedTests
            // 
            groupBoxExSelectedTests.CaptionText = "Tests";
            groupBoxExSelectedTests.Dock = DockStyle.Fill;
            groupBoxExSelectedTests.Location = new Point(583, 3);
            groupBoxExSelectedTests.Multiline = false;
            groupBoxExSelectedTests.Name = "groupBoxExSelectedTests";
            groupBoxExSelectedTests.PlaceholderText = "";
            groupBoxExSelectedTests.ReadOnly = true;
            groupBoxExSelectedTests.Size = new Size(114, 63);
            groupBoxExSelectedTests.TabIndex = 5;
            groupBoxExSelectedTests.TextAlign = HorizontalAlignment.Right;
            groupBoxExSelectedTests.UseSystemPasswordChar = false;
            groupBoxExSelectedTests.Value = "";
            // 
            // groupBoxExSelectedTasks
            // 
            groupBoxExSelectedTasks.CaptionText = "Tasks";
            groupBoxExSelectedTasks.Dock = DockStyle.Fill;
            groupBoxExSelectedTasks.Location = new Point(463, 3);
            groupBoxExSelectedTasks.Multiline = false;
            groupBoxExSelectedTasks.Name = "groupBoxExSelectedTasks";
            groupBoxExSelectedTasks.PlaceholderText = "";
            groupBoxExSelectedTasks.ReadOnly = true;
            groupBoxExSelectedTasks.Size = new Size(114, 63);
            groupBoxExSelectedTasks.TabIndex = 4;
            groupBoxExSelectedTasks.TextAlign = HorizontalAlignment.Right;
            groupBoxExSelectedTasks.UseSystemPasswordChar = false;
            groupBoxExSelectedTasks.Value = "";
            // 
            // groupBoxExSelectedStories
            // 
            groupBoxExSelectedStories.CaptionText = "Stories";
            groupBoxExSelectedStories.Dock = DockStyle.Fill;
            groupBoxExSelectedStories.Location = new Point(343, 3);
            groupBoxExSelectedStories.Multiline = false;
            groupBoxExSelectedStories.Name = "groupBoxExSelectedStories";
            groupBoxExSelectedStories.PlaceholderText = "";
            groupBoxExSelectedStories.ReadOnly = true;
            groupBoxExSelectedStories.Size = new Size(114, 63);
            groupBoxExSelectedStories.TabIndex = 3;
            groupBoxExSelectedStories.TextAlign = HorizontalAlignment.Right;
            groupBoxExSelectedStories.UseSystemPasswordChar = false;
            groupBoxExSelectedStories.Value = "";
            // 
            // groupBoxExSelectedIssues
            // 
            groupBoxExSelectedIssues.CaptionText = "Issues";
            groupBoxExSelectedIssues.Dock = DockStyle.Fill;
            groupBoxExSelectedIssues.Location = new Point(223, 3);
            groupBoxExSelectedIssues.Multiline = false;
            groupBoxExSelectedIssues.Name = "groupBoxExSelectedIssues";
            groupBoxExSelectedIssues.PlaceholderText = "";
            groupBoxExSelectedIssues.ReadOnly = true;
            groupBoxExSelectedIssues.Size = new Size(114, 63);
            groupBoxExSelectedIssues.TabIndex = 1;
            groupBoxExSelectedIssues.TextAlign = HorizontalAlignment.Right;
            groupBoxExSelectedIssues.UseSystemPasswordChar = false;
            groupBoxExSelectedIssues.Value = "";
            // 
            // tableLayoutPanelResultsTop
            // 
            tableLayoutPanelResultsTop.ColumnCount = 10;
            tableLayoutPanelResultsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 81F));
            tableLayoutPanelResultsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 59F));
            tableLayoutPanelResultsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelResultsTop.Controls.Add(groupBoxExSubTask, 8, 0);
            tableLayoutPanelResultsTop.Controls.Add(groupBoxExBug, 7, 0);
            tableLayoutPanelResultsTop.Controls.Add(groupBoxExTest, 6, 0);
            tableLayoutPanelResultsTop.Controls.Add(groupBoxExTask, 5, 0);
            tableLayoutPanelResultsTop.Controls.Add(groupBoxExStory, 4, 0);
            tableLayoutPanelResultsTop.Controls.Add(stopwatchClockConvertRun, 1, 0);
            tableLayoutPanelResultsTop.Controls.Add(groupBoxExIssueCount, 3, 0);
            tableLayoutPanelResultsTop.Controls.Add(groupBoxExDuration, 2, 0);
            tableLayoutPanelResultsTop.Controls.Add(buttonProcessStories, 0, 0);
            tableLayoutPanelResultsTop.Dock = DockStyle.Fill;
            tableLayoutPanelResultsTop.Location = new Point(3, 3);
            tableLayoutPanelResultsTop.Name = "tableLayoutPanelResultsTop";
            tableLayoutPanelResultsTop.RowCount = 1;
            tableLayoutPanelResultsTop.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelResultsTop.Size = new Size(1382, 69);
            tableLayoutPanelResultsTop.TabIndex = 0;
            // 
            // groupBoxExSubTask
            // 
            groupBoxExSubTask.CaptionText = "SubTasks";
            groupBoxExSubTask.Dock = DockStyle.Fill;
            groupBoxExSubTask.Location = new Point(863, 3);
            groupBoxExSubTask.Multiline = false;
            groupBoxExSubTask.Name = "groupBoxExSubTask";
            groupBoxExSubTask.PlaceholderText = "";
            groupBoxExSubTask.ReadOnly = true;
            groupBoxExSubTask.Size = new Size(114, 63);
            groupBoxExSubTask.TabIndex = 7;
            groupBoxExSubTask.TextAlign = HorizontalAlignment.Right;
            groupBoxExSubTask.UseSystemPasswordChar = false;
            groupBoxExSubTask.Value = "";
            // 
            // groupBoxExBug
            // 
            groupBoxExBug.CaptionText = "Bugs";
            groupBoxExBug.Dock = DockStyle.Fill;
            groupBoxExBug.Location = new Point(743, 3);
            groupBoxExBug.Multiline = false;
            groupBoxExBug.Name = "groupBoxExBug";
            groupBoxExBug.PlaceholderText = "";
            groupBoxExBug.ReadOnly = true;
            groupBoxExBug.Size = new Size(114, 63);
            groupBoxExBug.TabIndex = 6;
            groupBoxExBug.TextAlign = HorizontalAlignment.Right;
            groupBoxExBug.UseSystemPasswordChar = false;
            groupBoxExBug.Value = "";
            // 
            // groupBoxExTest
            // 
            groupBoxExTest.CaptionText = "Tests";
            groupBoxExTest.Dock = DockStyle.Fill;
            groupBoxExTest.Location = new Point(623, 3);
            groupBoxExTest.Multiline = false;
            groupBoxExTest.Name = "groupBoxExTest";
            groupBoxExTest.PlaceholderText = "";
            groupBoxExTest.ReadOnly = true;
            groupBoxExTest.Size = new Size(114, 63);
            groupBoxExTest.TabIndex = 5;
            groupBoxExTest.TextAlign = HorizontalAlignment.Right;
            groupBoxExTest.UseSystemPasswordChar = false;
            groupBoxExTest.Value = "";
            // 
            // groupBoxExTask
            // 
            groupBoxExTask.CaptionText = "Tasks";
            groupBoxExTask.Dock = DockStyle.Fill;
            groupBoxExTask.Location = new Point(503, 3);
            groupBoxExTask.Multiline = false;
            groupBoxExTask.Name = "groupBoxExTask";
            groupBoxExTask.PlaceholderText = "";
            groupBoxExTask.ReadOnly = true;
            groupBoxExTask.Size = new Size(114, 63);
            groupBoxExTask.TabIndex = 4;
            groupBoxExTask.TextAlign = HorizontalAlignment.Right;
            groupBoxExTask.UseSystemPasswordChar = false;
            groupBoxExTask.Value = "";
            // 
            // groupBoxExStory
            // 
            groupBoxExStory.CaptionText = "Stories";
            groupBoxExStory.Dock = DockStyle.Fill;
            groupBoxExStory.Location = new Point(383, 3);
            groupBoxExStory.Multiline = false;
            groupBoxExStory.Name = "groupBoxExStory";
            groupBoxExStory.PlaceholderText = "";
            groupBoxExStory.ReadOnly = true;
            groupBoxExStory.Size = new Size(114, 63);
            groupBoxExStory.TabIndex = 3;
            groupBoxExStory.TextAlign = HorizontalAlignment.Right;
            groupBoxExStory.UseSystemPasswordChar = false;
            groupBoxExStory.Value = "";
            // 
            // stopwatchClockConvertRun
            // 
            stopwatchClockConvertRun.CenterDotColor = Color.Black;
            stopwatchClockConvertRun.ClockFaceColor = SystemColors.Control;
            stopwatchClockConvertRun.Dock = DockStyle.Fill;
            stopwatchClockConvertRun.HasCenterDot = true;
            stopwatchClockConvertRun.IsTicked = true;
            stopwatchClockConvertRun.Location = new Point(86, 5);
            stopwatchClockConvertRun.Margin = new Padding(5);
            stopwatchClockConvertRun.Name = "stopwatchClockConvertRun";
            stopwatchClockConvertRun.SecondHandColor = Color.Blue;
            stopwatchClockConvertRun.Size = new Size(49, 59);
            stopwatchClockConvertRun.TabIndex = 2;
            stopwatchClockConvertRun.TickMarkColor = Color.Gray;
            // 
            // groupBoxExIssueCount
            // 
            groupBoxExIssueCount.CaptionText = "Issues";
            groupBoxExIssueCount.Dock = DockStyle.Fill;
            groupBoxExIssueCount.Location = new Point(263, 3);
            groupBoxExIssueCount.Multiline = false;
            groupBoxExIssueCount.Name = "groupBoxExIssueCount";
            groupBoxExIssueCount.PlaceholderText = "";
            groupBoxExIssueCount.ReadOnly = true;
            groupBoxExIssueCount.Size = new Size(114, 63);
            groupBoxExIssueCount.TabIndex = 1;
            groupBoxExIssueCount.TextAlign = HorizontalAlignment.Right;
            groupBoxExIssueCount.UseSystemPasswordChar = false;
            groupBoxExIssueCount.Value = "";
            // 
            // groupBoxExDuration
            // 
            groupBoxExDuration.CaptionText = "Duration";
            groupBoxExDuration.Dock = DockStyle.Fill;
            groupBoxExDuration.Location = new Point(143, 3);
            groupBoxExDuration.Multiline = false;
            groupBoxExDuration.Name = "groupBoxExDuration";
            groupBoxExDuration.PlaceholderText = "";
            groupBoxExDuration.ReadOnly = true;
            groupBoxExDuration.Size = new Size(114, 63);
            groupBoxExDuration.TabIndex = 0;
            groupBoxExDuration.TextAlign = HorizontalAlignment.Right;
            groupBoxExDuration.UseSystemPasswordChar = false;
            groupBoxExDuration.Value = "";
            // 
            // buttonProcessStories
            // 
            buttonProcessStories.Dock = DockStyle.Fill;
            buttonProcessStories.Enabled = false;
            buttonProcessStories.Location = new Point(3, 3);
            buttonProcessStories.Name = "buttonProcessStories";
            buttonProcessStories.Size = new Size(75, 63);
            buttonProcessStories.TabIndex = 8;
            buttonProcessStories.Tag = "Process Stories";
            buttonProcessStories.Text = "Process Stories";
            buttonProcessStories.UseVisualStyleBackColor = true;
            buttonProcessStories.Click +=  ButtonProcessStories_ClickAsync ;
            // 
            // columnHeaderUserStorySubject
            // 
            columnHeaderUserStorySubject.Text = "Issue";
            columnHeaderUserStorySubject.Width = 70;
            // 
            // flowLayoutPanelMainButtons
            // 
            flowLayoutPanelMainButtons.Controls.Add(buttonSave);
            flowLayoutPanelMainButtons.Controls.Add(labelStatus);
            flowLayoutPanelMainButtons.Dock = DockStyle.Fill;
            flowLayoutPanelMainButtons.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanelMainButtons.Location = new Point(3, 891);
            flowLayoutPanelMainButtons.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelMainButtons.Name = "flowLayoutPanelMainButtons";
            flowLayoutPanelMainButtons.Size = new Size(2058, 35);
            flowLayoutPanelMainButtons.TabIndex = 3;
            // 
            // buttonSave
            // 
            buttonSave.Enabled = false;
            buttonSave.Location = new Point(1969, 4);
            buttonSave.Margin = new Padding(3, 4, 3, 4);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(86, 31);
            buttonSave.TabIndex = 0;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click +=  ButtonSave_Click ;
            // 
            // labelStatus
            // 
            labelStatus.Location = new Point(667, 6);
            labelStatus.Margin = new Padding(3, 6, 3, 3);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(1296, 26);
            labelStatus.TabIndex = 1;
            labelStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanelMainData
            // 
            tableLayoutPanelMainData.ColumnCount = 2;
            tableLayoutPanelMainData.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 664F));
            tableLayoutPanelMainData.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMainData.Controls.Add(panelResults, 1, 0);
            tableLayoutPanelMainData.Controls.Add(tableLayoutPanelData, 0, 0);
            tableLayoutPanelMainData.Dock = DockStyle.Fill;
            tableLayoutPanelMainData.Location = new Point(3, 4);
            tableLayoutPanelMainData.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelMainData.Name = "tableLayoutPanelMainData";
            tableLayoutPanelMainData.RowCount = 1;
            tableLayoutPanelMainData.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMainData.Size = new Size(2058, 879);
            tableLayoutPanelMainData.TabIndex = 4;
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(flowLayoutPanelMainButtons, 0, 1);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelMainData, 0, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 25);
            tableLayoutPanelMain.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            tableLayoutPanelMain.Size = new Size(2064, 930);
            tableLayoutPanelMain.TabIndex = 5;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { preferencesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(2064, 25);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // preferencesToolStripMenuItem
            // 
            preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            preferencesToolStripMenuItem.Size = new Size(89, 19);
            preferencesToolStripMenuItem.Text = "&Preferences...";
            preferencesToolStripMenuItem.Click +=  PreferencesToolStripMenuItem_Click ;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2064, 955);
            Controls.Add(tableLayoutPanelMain);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Tell me a story...";
            tableLayoutPanelData.ResumeLayout(false);
            flowLayoutPanelProductName.ResumeLayout(false);
            flowLayoutPanelProductName.PerformLayout();
            flowLayoutPanelEpic.ResumeLayout(false);
            flowLayoutPanelEpic.PerformLayout();
            tableLayoutPanelControls.ResumeLayout(false);
            flowLayoutPanelCheckBoxes.ResumeLayout(false);
            flowLayoutPanelCheckBoxes.PerformLayout();
            flowLayoutPanelProduct.ResumeLayout(false);
            panelResults.ResumeLayout(false);
            tableLayoutPanelResults.ResumeLayout(false);
            tableLayoutPanelResultsBottom.ResumeLayout(false);
            tableLayoutPanelResultsTop.ResumeLayout(false);
            flowLayoutPanelMainButtons.ResumeLayout(false);
            tableLayoutPanelMainData.ResumeLayout(false);
            tableLayoutPanelMain.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TriStateTreeView treeView;
        private Button buttonConvert;
        private TableLayoutPanel tableLayoutPanelData;
        private GroupBoxEx textBoxPRD;
        private ColumnHeader columnHeaderUserStorySubject;
        private ProgressBar progressBar;
        private FlowLayoutPanel flowLayoutPanelMainButtons;
        private Button buttonSave;
        private TableLayoutPanel tableLayoutPanelMainData;
        private TableLayoutPanel tableLayoutPanelMain;
        private FlowLayoutPanel flowLayoutPanelProduct;
        private Label Product;
        private FlowLayoutPanel flowLayoutPanelEpic;
        private Label labelEpic;
        private TextBox textBoxEpic;
        private TableLayoutPanel tableLayoutPanelControls;
        private FlowLayoutPanel flowLayoutPanelCheckBoxes;
        private CheckBox checkBoxAddSubTasks;
        private CheckBox checkBoxAddQATests;
        private ComboBoxEx comboBoxJiraProjects;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem preferencesToolStripMenuItem;
        private Panel panelResults;
        private TableLayoutPanel tableLayoutPanelResults;
        private GroupBoxEx groupBoxExDuration;
        private GroupBoxEx groupBoxExIssueCount;
        private StopwatchClock stopwatchClockConvertRun;
        private GroupBoxEx groupBoxExSubTask;
        private GroupBoxEx groupBoxExBug;
        private GroupBoxEx groupBoxExTest;
        private GroupBoxEx groupBoxExTask;
        private GroupBoxEx groupBoxExStory;
        private GroupBoxEx groupBoxExSelectedSubTasks;
        private GroupBoxEx groupBoxExSelectedBugs;
        private GroupBoxEx groupBoxExSelectedTests;
        private GroupBoxEx groupBoxExSelectedTasks;
        private GroupBoxEx groupBoxExSelectedStories;
        private GroupBoxEx groupBoxExSelectedIssues;

        private TableLayoutPanelEx tableLayoutPanelResultsTop;
        private TableLayoutPanelEx tableLayoutPanelResultsBottom;
        private Button buttonProcessStories;
        private Label labelStatus;
        private FlowLayoutPanel flowLayoutPanelProductName;
        private Label labelProductName;
        private TextBox textBoxProductName;
    }

    public interface IReset
    {
        void Reset();
    }
    public class ComboBoxEx : ComboBox
    {
        public string CurrentSelectedValue
        {
            get
            {
                if( this.SelectedValue == null ) throw new NullReferenceException("ComboBoxEx.SelectedValue");
                return (string)this.SelectedValue ?? throw new NullReferenceException("ComboBoxEx.SelectedValue");
            }
        }
    }

    public class TableLayoutPanelEx : TableLayoutPanel, IReset
    {
        public void Reset()
        {
            foreach( Control control in this.Controls )
            {
                IReset reset = control as IReset;
                if( reset != null )
                    reset.Reset();
            }
        }
    }

}
