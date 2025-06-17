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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tableLayoutPanelData = new TableLayoutPanel();
            tableLayoutPanelEntryControl = new TableLayoutPanel();
            groupBoxStoryMin = new GroupBox();
            comboBoxExStoryMin = new ComboBoxEx();
            groupBoxExProductFeature = new GroupBoxEx();
            groupBoxProduct = new GroupBox();
            comboBoxJiraProjects = new ComboBoxEx();
            tableLayoutPanelControls = new TableLayoutPanel();
            buttonConvert = new Button();
            flowLayoutPanelCheckBoxes = new FlowLayoutPanel();
            checkBoxAddSubTasks = new CheckBox();
            checkBoxAddQATests = new CheckBox();
            groupBoxExPRD = new GroupBoxEx();
            epicSelector = new EpicSelector();
            treeView = new TriStateTreeView();
            panelResults = new Panel();
            tableLayoutPanelResults = new TableLayoutPanel();
            flowLayoutPanelSelected = new ResizableGroupBoxFlowLayoutPanelEx();
            tableLayoutPanelResultsTotal = new TableLayoutPanelEx();
            buttonProcessStories = new Button();
            stopwatchClockConvertRun = new StopwatchClock();
            groupBoxExDuration = new GroupBoxEx();
            flowLayoutPanelExTotals = new ResizableGroupBoxFlowLayoutPanelEx();
            flowLayoutPanelIssueImages = new FlowLayoutPanelImages();
            groupBoxExIssueCount = new GroupBoxEx();
            columnHeaderUserStorySubject = new ColumnHeader();
            flowLayoutPanelMainButtons = new FlowLayoutPanel();
            buttonSave = new Button();
            labelStatus = new Label();
            tableLayoutPanelMainData = new TableLayoutPanel();
            tableLayoutPanelMain = new TableLayoutPanel();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveStoriesAsJsonToolStripMenuItem = new ToolStripMenuItem();
            saveStoriesAsCSVToolStripMenuItem = new ToolStripMenuItem();
            getUserStoryListToolStripMenuItem = new ToolStripMenuItem();
            preferencesToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanelData.SuspendLayout();
            tableLayoutPanelEntryControl.SuspendLayout();
            groupBoxStoryMin.SuspendLayout();
            groupBoxProduct.SuspendLayout();
            tableLayoutPanelControls.SuspendLayout();
            flowLayoutPanelCheckBoxes.SuspendLayout();
            panelResults.SuspendLayout();
            tableLayoutPanelResults.SuspendLayout();
            tableLayoutPanelResultsTotal.SuspendLayout();
            flowLayoutPanelMainButtons.SuspendLayout();
            tableLayoutPanelMainData.SuspendLayout();
            tableLayoutPanelMain.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelData
            // 
            tableLayoutPanelData.ColumnCount = 1;
            tableLayoutPanelData.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelData.Controls.Add(tableLayoutPanelEntryControl, 0, 0);
            tableLayoutPanelData.Controls.Add(tableLayoutPanelControls, 0, 3);
            tableLayoutPanelData.Controls.Add(groupBoxExPRD, 0, 2);
            tableLayoutPanelData.Controls.Add(epicSelector, 0, 1);
            tableLayoutPanelData.Dock = DockStyle.Fill;
            tableLayoutPanelData.Location = new Point(3, 4);
            tableLayoutPanelData.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelData.Name = "tableLayoutPanelData";
            tableLayoutPanelData.RowCount = 4;
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Absolute, 95F));
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Absolute, 83F));
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelData.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanelData.Size = new Size(776, 871);
            tableLayoutPanelData.TabIndex = 1;
            // 
            // tableLayoutPanelEntryControl
            // 
            tableLayoutPanelEntryControl.ColumnCount = 3;
            tableLayoutPanelEntryControl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tableLayoutPanelEntryControl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelEntryControl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanelEntryControl.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanelEntryControl.Controls.Add(groupBoxStoryMin, 2, 0);
            tableLayoutPanelEntryControl.Controls.Add(groupBoxExProductFeature, 1, 0);
            tableLayoutPanelEntryControl.Controls.Add(groupBoxProduct, 0, 0);
            tableLayoutPanelEntryControl.Dock = DockStyle.Fill;
            tableLayoutPanelEntryControl.Location = new Point(3, 3);
            tableLayoutPanelEntryControl.Name = "tableLayoutPanelEntryControl";
            tableLayoutPanelEntryControl.RowCount = 1;
            tableLayoutPanelEntryControl.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelEntryControl.Size = new Size(770, 89);
            tableLayoutPanelEntryControl.TabIndex = 12;
            // 
            // groupBoxStoryMin
            // 
            groupBoxStoryMin.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            groupBoxStoryMin.Controls.Add(comboBoxExStoryMin);
            groupBoxStoryMin.Location = new Point(673, 3);
            groupBoxStoryMin.Name = "groupBoxStoryMin";
            groupBoxStoryMin.Size = new Size(94, 75);
            groupBoxStoryMin.TabIndex = 13;
            groupBoxStoryMin.TabStop = false;
            groupBoxStoryMin.Text = "Story Min";
            // 
            // comboBoxExStoryMin
            // 
            comboBoxExStoryMin.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            comboBoxExStoryMin.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxExStoryMin.FormattingEnabled = true;
            comboBoxExStoryMin.Location = new Point(21, 37);
            comboBoxExStoryMin.Margin = new Padding(3, 4, 3, 4);
            comboBoxExStoryMin.Name = "comboBoxExStoryMin";
            comboBoxExStoryMin.Size = new Size(67, 28);
            comboBoxExStoryMin.TabIndex = 1;
            // 
            // groupBoxExProductFeature
            // 
            groupBoxExProductFeature.CaptionText = "Product/Feature";
            groupBoxExProductFeature.Dock = DockStyle.Fill;
            groupBoxExProductFeature.Location = new Point(143, 9);
            groupBoxExProductFeature.Margin = new Padding(3, 9, 3, 9);
            groupBoxExProductFeature.Multiline = false;
            groupBoxExProductFeature.Name = "groupBoxExProductFeature";
            groupBoxExProductFeature.PlaceholderText = "enter the product or feature name (required)";
            groupBoxExProductFeature.ReadOnly = false;
            groupBoxExProductFeature.Size = new Size(524, 71);
            groupBoxExProductFeature.TabIndex = 11;
            groupBoxExProductFeature.TextAlign = HorizontalAlignment.Left;
            groupBoxExProductFeature.TextBoxForeColor = SystemColors.WindowText;
            groupBoxExProductFeature.UseSystemPasswordChar = false;
            groupBoxExProductFeature.Value = "";
            groupBoxExProductFeature.ValueChanged +=  TextControls_TextChanged ;
            // 
            // groupBoxProduct
            // 
            groupBoxProduct.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            groupBoxProduct.Controls.Add(comboBoxJiraProjects);
            groupBoxProduct.Location = new Point(3, 3);
            groupBoxProduct.Name = "groupBoxProduct";
            groupBoxProduct.Size = new Size(134, 75);
            groupBoxProduct.TabIndex = 12;
            groupBoxProduct.TabStop = false;
            groupBoxProduct.Text = "Product";
            // 
            // comboBoxJiraProjects
            // 
            comboBoxJiraProjects.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            comboBoxJiraProjects.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxJiraProjects.FormattingEnabled = true;
            comboBoxJiraProjects.Location = new Point(21, 37);
            comboBoxJiraProjects.Margin = new Padding(3, 4, 3, 4);
            comboBoxJiraProjects.Name = "comboBoxJiraProjects";
            comboBoxJiraProjects.Size = new Size(107, 28);
            comboBoxJiraProjects.TabIndex = 1;
            // 
            // tableLayoutPanelControls
            // 
            tableLayoutPanelControls.ColumnCount = 2;
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanelControls.Controls.Add(buttonConvert, 1, 0);
            tableLayoutPanelControls.Controls.Add(flowLayoutPanelCheckBoxes, 0, 0);
            tableLayoutPanelControls.Dock = DockStyle.Fill;
            tableLayoutPanelControls.Location = new Point(3, 830);
            tableLayoutPanelControls.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelControls.Name = "tableLayoutPanelControls";
            tableLayoutPanelControls.RowCount = 1;
            tableLayoutPanelControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelControls.Size = new Size(770, 37);
            tableLayoutPanelControls.TabIndex = 2;
            // 
            // buttonConvert
            // 
            buttonConvert.Dock = DockStyle.Fill;
            buttonConvert.Enabled = false;
            buttonConvert.Location = new Point(653, 4);
            buttonConvert.Margin = new Padding(3, 4, 3, 4);
            buttonConvert.Name = "buttonConvert";
            buttonConvert.Size = new Size(114, 29);
            buttonConvert.TabIndex = 0;
            buttonConvert.Text = "Create Stories";
            buttonConvert.UseVisualStyleBackColor = true;
            buttonConvert.Click +=  Convert_Click ;
            // 
            // flowLayoutPanelCheckBoxes
            // 
            flowLayoutPanelCheckBoxes.Controls.Add(checkBoxAddSubTasks);
            flowLayoutPanelCheckBoxes.Controls.Add(checkBoxAddQATests);
            flowLayoutPanelCheckBoxes.Dock = DockStyle.Fill;
            flowLayoutPanelCheckBoxes.Location = new Point(3, 4);
            flowLayoutPanelCheckBoxes.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelCheckBoxes.Name = "flowLayoutPanelCheckBoxes";
            flowLayoutPanelCheckBoxes.Size = new Size(644, 29);
            flowLayoutPanelCheckBoxes.TabIndex = 2;
            // 
            // checkBoxAddSubTasks
            // 
            checkBoxAddSubTasks.AutoSize = true;
            checkBoxAddSubTasks.Checked = true;
            checkBoxAddSubTasks.CheckState = CheckState.Checked;
            checkBoxAddSubTasks.Location = new Point(3, 4);
            checkBoxAddSubTasks.Margin = new Padding(3, 4, 3, 4);
            checkBoxAddSubTasks.Name = "checkBoxAddSubTasks";
            checkBoxAddSubTasks.Size = new Size(117, 24);
            checkBoxAddSubTasks.TabIndex = 0;
            checkBoxAddSubTasks.Text = "Add Subtasks";
            checkBoxAddSubTasks.UseVisualStyleBackColor = true;
            // 
            // checkBoxAddQATests
            // 
            checkBoxAddQATests.AutoSize = true;
            checkBoxAddQATests.Checked = true;
            checkBoxAddQATests.CheckState = CheckState.Checked;
            checkBoxAddQATests.Location = new Point(126, 4);
            checkBoxAddQATests.Margin = new Padding(3, 4, 3, 4);
            checkBoxAddQATests.Name = "checkBoxAddQATests";
            checkBoxAddQATests.Size = new Size(117, 24);
            checkBoxAddQATests.TabIndex = 1;
            checkBoxAddQATests.Text = "Add QA Tests";
            checkBoxAddQATests.UseVisualStyleBackColor = true;
            // 
            // groupBoxExPRD
            // 
            groupBoxExPRD.AllowDrop = true;
            groupBoxExPRD.CaptionText = "Product Description";
            groupBoxExPRD.Dock = DockStyle.Fill;
            groupBoxExPRD.Location = new Point(2, 181);
            groupBoxExPRD.Margin = new Padding(2, 3, 2, 3);
            groupBoxExPRD.Multiline = true;
            groupBoxExPRD.Name = "groupBoxExPRD";
            groupBoxExPRD.PlaceholderText = "enter the product feature plain text description (required)";
            groupBoxExPRD.ReadOnly = false;
            groupBoxExPRD.Size = new Size(772, 642);
            groupBoxExPRD.TabIndex = 1;
            groupBoxExPRD.TextAlign = HorizontalAlignment.Left;
            groupBoxExPRD.TextBoxForeColor = SystemColors.WindowText;
            groupBoxExPRD.UseSystemPasswordChar = false;
            groupBoxExPRD.Value = "";
            groupBoxExPRD.ValueChanged +=  TextControls_TextChanged ;
            groupBoxExPRD.TextChanged +=  TextControls_TextChanged ;
            groupBoxExPRD.DragDrop +=  GroupBoxExPRD_DragDrop ;
            groupBoxExPRD.DragEnter +=  GroupBoxExPRD_DragEnter ;
            // 
            // epicSelector
            // 
            epicSelector.Dock = DockStyle.Fill;
            epicSelector.Font = new Font("Segoe UI", 11.25F);
            epicSelector.Location = new Point(3, 99);
            epicSelector.Margin = new Padding(3, 4, 3, 4);
            epicSelector.Name = "epicSelector";
            epicSelector.Size = new Size(770, 75);
            epicSelector.TabIndex = 13;
            // 
            // treeView
            // 
            treeView.AllowDrop = true;
            treeView.Dock = DockStyle.Fill;
            treeView.ImageIndex = 0;
            treeView.Location = new Point(3, 115);
            treeView.Margin = new Padding(3, 4, 3, 4);
            treeView.Name = "treeView";
            treeView.SelectedImageIndex = 0;
            treeView.ShowNodeToolTips = true;
            treeView.Size = new Size(1264, 677);
            treeView.TabIndex = 2;
            treeView.TriStateStyleProperty = TriStateTreeView.TriStateStyles.Standard;
            treeView.ItemDrag +=  TreeView_ItemDrag ;
            treeView.DragDrop +=  TreeView_DragDrop ;
            treeView.DragEnter +=  TreeView_DragEnter ;
            // 
            // panelResults
            // 
            panelResults.Controls.Add(tableLayoutPanelResults);
            panelResults.Dock = DockStyle.Fill;
            panelResults.Location = new Point(785, 4);
            panelResults.Margin = new Padding(3, 4, 3, 4);
            panelResults.Name = "panelResults";
            panelResults.Size = new Size(1270, 871);
            panelResults.TabIndex = 2;
            // 
            // tableLayoutPanelResults
            // 
            tableLayoutPanelResults.ColumnCount = 1;
            tableLayoutPanelResults.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelResults.Controls.Add(flowLayoutPanelSelected, 0, 3);
            tableLayoutPanelResults.Controls.Add(tableLayoutPanelResultsTotal, 0, 0);
            tableLayoutPanelResults.Controls.Add(treeView, 0, 2);
            tableLayoutPanelResults.Controls.Add(flowLayoutPanelIssueImages, 0, 1);
            tableLayoutPanelResults.Dock = DockStyle.Fill;
            tableLayoutPanelResults.Location = new Point(0, 0);
            tableLayoutPanelResults.Name = "tableLayoutPanelResults";
            tableLayoutPanelResults.RowCount = 4;
            tableLayoutPanelResults.RowStyles.Add(new RowStyle(SizeType.Absolute, 87F));
            tableLayoutPanelResults.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanelResults.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelResults.RowStyles.Add(new RowStyle(SizeType.Absolute, 75F));
            tableLayoutPanelResults.Size = new Size(1270, 871);
            tableLayoutPanelResults.TabIndex = 0;
            // 
            // flowLayoutPanelSelected
            // 
            flowLayoutPanelSelected.Dock = DockStyle.Fill;
            flowLayoutPanelSelected.Location = new Point(3, 799);
            flowLayoutPanelSelected.Name = "flowLayoutPanelSelected";
            flowLayoutPanelSelected.Size = new Size(1264, 69);
            flowLayoutPanelSelected.TabIndex = 3;
            flowLayoutPanelSelected.WrapContents = false;
            // 
            // tableLayoutPanelResultsTotal
            // 
            tableLayoutPanelResultsTotal.ColumnCount = 4;
            tableLayoutPanelResultsTotal.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanelResultsTotal.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            tableLayoutPanelResultsTotal.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelResultsTotal.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanelResultsTotal.Controls.Add(buttonProcessStories, 0, 0);
            tableLayoutPanelResultsTotal.Controls.Add(stopwatchClockConvertRun, 1, 0);
            tableLayoutPanelResultsTotal.Controls.Add(groupBoxExDuration, 2, 0);
            tableLayoutPanelResultsTotal.Controls.Add(flowLayoutPanelExTotals, 3, 0);
            tableLayoutPanelResultsTotal.Dock = DockStyle.Fill;
            tableLayoutPanelResultsTotal.Location = new Point(3, 3);
            tableLayoutPanelResultsTotal.Name = "tableLayoutPanelResultsTotal";
            tableLayoutPanelResultsTotal.RowCount = 1;
            tableLayoutPanelResultsTotal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelResultsTotal.Size = new Size(1264, 81);
            tableLayoutPanelResultsTotal.TabIndex = 0;
            // 
            // buttonProcessStories
            // 
            buttonProcessStories.Dock = DockStyle.Fill;
            buttonProcessStories.Enabled = false;
            buttonProcessStories.Location = new Point(3, 3);
            buttonProcessStories.Name = "buttonProcessStories";
            buttonProcessStories.Size = new Size(74, 75);
            buttonProcessStories.TabIndex = 8;
            buttonProcessStories.Tag = "Process Stories";
            buttonProcessStories.Text = "Process Stories";
            buttonProcessStories.UseVisualStyleBackColor = true;
            buttonProcessStories.Click +=  ButtonProcessStories_ClickAsync ;
            // 
            // stopwatchClockConvertRun
            // 
            stopwatchClockConvertRun.CenterDotColor = Color.Black;
            stopwatchClockConvertRun.ClockFaceColor = SystemColors.Control;
            stopwatchClockConvertRun.Dock = DockStyle.Fill;
            stopwatchClockConvertRun.HasCenterDot = true;
            stopwatchClockConvertRun.IsTicked = true;
            stopwatchClockConvertRun.Location = new Point(85, 5);
            stopwatchClockConvertRun.Margin = new Padding(5);
            stopwatchClockConvertRun.Name = "stopwatchClockConvertRun";
            stopwatchClockConvertRun.SecondHandColor = Color.Blue;
            stopwatchClockConvertRun.Size = new Size(50, 71);
            stopwatchClockConvertRun.TabIndex = 2;
            stopwatchClockConvertRun.TickMarkColor = Color.Gray;
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
            groupBoxExDuration.Size = new Size(114, 75);
            groupBoxExDuration.TabIndex = 0;
            groupBoxExDuration.TextAlign = HorizontalAlignment.Right;
            groupBoxExDuration.TextBoxForeColor = SystemColors.WindowText;
            groupBoxExDuration.UseSystemPasswordChar = false;
            groupBoxExDuration.Value = "";
            // 
            // flowLayoutPanelExTotals
            // 
            flowLayoutPanelExTotals.Dock = DockStyle.Fill;
            flowLayoutPanelExTotals.Location = new Point(263, 3);
            flowLayoutPanelExTotals.Name = "flowLayoutPanelExTotals";
            flowLayoutPanelExTotals.Size = new Size(998, 75);
            flowLayoutPanelExTotals.TabIndex = 9;
            flowLayoutPanelExTotals.WrapContents = false;
            // 
            // flowLayoutPanelIssueImages
            // 
            flowLayoutPanelIssueImages.Dock = DockStyle.Fill;
            flowLayoutPanelIssueImages.Location = new Point(3, 90);
            flowLayoutPanelIssueImages.Name = "flowLayoutPanelIssueImages";
            flowLayoutPanelIssueImages.Size = new Size(1264, 18);
            flowLayoutPanelIssueImages.TabIndex = 4;
            flowLayoutPanelIssueImages.WrapContents = false;
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
            groupBoxExIssueCount.TextBoxForeColor = SystemColors.WindowText;
            groupBoxExIssueCount.UseSystemPasswordChar = false;
            groupBoxExIssueCount.Value = "";
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
            buttonSave.Visible = false;
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
            tableLayoutPanelMainData.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 782F));
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
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, preferencesToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 3, 0, 3);
            menuStrip.Size = new Size(2064, 25);
            menuStrip.TabIndex = 6;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveStoriesAsJsonToolStripMenuItem, saveStoriesAsCSVToolStripMenuItem, getUserStoryListToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 19);
            fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(178, 22);
            openToolStripMenuItem.Text = "&Open...";
            openToolStripMenuItem.Click +=  OpenToolStripMenuItem_Click ;
            // 
            // saveStoriesAsJsonToolStripMenuItem
            // 
            saveStoriesAsJsonToolStripMenuItem.Enabled = false;
            saveStoriesAsJsonToolStripMenuItem.Name = "saveStoriesAsJsonToolStripMenuItem";
            saveStoriesAsJsonToolStripMenuItem.Size = new Size(178, 22);
            saveStoriesAsJsonToolStripMenuItem.Text = "&Save as Json...";
            saveStoriesAsJsonToolStripMenuItem.Click +=  SaveStoriesAsJsonToolStripMenuItem_Click ;
            // 
            // saveStoriesAsCSVToolStripMenuItem
            // 
            saveStoriesAsCSVToolStripMenuItem.Enabled = false;
            saveStoriesAsCSVToolStripMenuItem.Name = "saveStoriesAsCSVToolStripMenuItem";
            saveStoriesAsCSVToolStripMenuItem.Size = new Size(178, 22);
            saveStoriesAsCSVToolStripMenuItem.Text = "Save &as CSV...";
            saveStoriesAsCSVToolStripMenuItem.Click +=  SaveStoriesAsCSVToolStripMenuItem_Click ;
            // 
            // getUserStoryListToolStripMenuItem
            // 
            getUserStoryListToolStripMenuItem.Enabled = false;
            getUserStoryListToolStripMenuItem.Name = "getUserStoryListToolStripMenuItem";
            getUserStoryListToolStripMenuItem.Size = new Size(178, 22);
            getUserStoryListToolStripMenuItem.Text = "&Get User Story List...";
            getUserStoryListToolStripMenuItem.Click +=  GetUserStoryListToolStripMenuItem_Click ;
            // 
            // preferencesToolStripMenuItem
            // 
            preferencesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, aboutToolStripMenuItem });
            preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            preferencesToolStripMenuItem.Size = new Size(80, 19);
            preferencesToolStripMenuItem.Text = "&Preferences";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(125, 22);
            settingsToolStripMenuItem.Text = "&Settings...";
            settingsToolStripMenuItem.Click +=  PreferencesToolStripMenuItem_Click ;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(125, 22);
            aboutToolStripMenuItem.Text = "About...";
            aboutToolStripMenuItem.Click +=  AboutToolStripMenuItem_Click ;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2100, 955);
            Controls.Add(tableLayoutPanelMain);
            Controls.Add(menuStrip);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            Text = "Tell me a story...";
            tableLayoutPanelData.ResumeLayout(false);
            tableLayoutPanelEntryControl.ResumeLayout(false);
            groupBoxStoryMin.ResumeLayout(false);
            groupBoxProduct.ResumeLayout(false);
            tableLayoutPanelControls.ResumeLayout(false);
            flowLayoutPanelCheckBoxes.ResumeLayout(false);
            flowLayoutPanelCheckBoxes.PerformLayout();
            panelResults.ResumeLayout(false);
            tableLayoutPanelResults.ResumeLayout(false);
            tableLayoutPanelResultsTotal.ResumeLayout(false);
            flowLayoutPanelMainButtons.ResumeLayout(false);
            tableLayoutPanelMainData.ResumeLayout(false);
            tableLayoutPanelMain.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TriStateTreeView treeView;
        private TableLayoutPanel tableLayoutPanelData;
        private GroupBoxEx groupBoxExPRD;
        private ColumnHeader columnHeaderUserStorySubject;
        private FlowLayoutPanel flowLayoutPanelMainButtons;
        private Button buttonSave;
        private TableLayoutPanel tableLayoutPanelMainData;
        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelControls;
        private FlowLayoutPanel flowLayoutPanelCheckBoxes;
        private CheckBox checkBoxAddSubTasks;
        private CheckBox checkBoxAddQATests;
        private ComboBoxEx comboBoxJiraProjects;
        private MenuStrip menuStrip;
        private ToolStripMenuItem preferencesToolStripMenuItem;
        private Panel panelResults;
        private TableLayoutPanel tableLayoutPanelResults;
        private GroupBoxEx groupBoxExDuration;
        private GroupBoxEx groupBoxExIssueCount;
        private StopwatchClock stopwatchClockConvertRun;
        private TableLayoutPanelEx tableLayoutPanelResultsTotal;
        private Button buttonProcessStories;
        private Label labelStatus;
        private TableLayoutPanel tableLayoutPanelEntryControl;
        private GroupBoxEx groupBoxExProductFeature;
        private GroupBox groupBoxStoryMin;
        private ComboBoxEx comboBoxExStoryMin;
        private GroupBox groupBoxProduct;
        private Button buttonConvert;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveStoriesAsJsonToolStripMenuItem;
        private ToolStripMenuItem saveStoriesAsCSVToolStripMenuItem;
        private ToolStripMenuItem getUserStoryListToolStripMenuItem;
        private EpicSelector epicSelector;
        private FlowLayoutPanelImages flowLayoutPanelIssueImages;
        private ResizableGroupBoxFlowLayoutPanelEx flowLayoutPanelSelected;
        private ResizableGroupBoxFlowLayoutPanelEx flowLayoutPanelExTotals;
    }

    public interface IReset
    {
        void Reset();
    }
    public class ComboBoxEx : ComboBox
    {
        public object CurrentSelectedItem
        {
            get
            {
                if( this.SelectedItem == null ) throw new NullReferenceException("ComboBoxEx.SelectedItem");
                //return (string)this.SelectedItem ?? throw new NullReferenceException("ComboBoxEx.SelectedItem");
                return this.SelectedItem;
            }
        }
    }

    public class FlowLayoutPanelImages : FlowLayoutPanel
    {
        public FlowLayoutPanelImages()
        {
            // Set some default properties for the FlowLayoutPanel
            this.FlowDirection = FlowDirection.LeftToRight;
            this.WrapContents = false; // Important to ensure they lay out horizontally

            //InitializePictureboxes();
            //
        }

        //private void InitializePictureboxes()
        //{
        //    CreateImageControl(Properties.Resources.epic, "Epic");
        //    CreateImageControl(Properties.Resources.story, "Story");
        //    CreateImageControl(Properties.Resources.task, "Task");
        //    CreateImageControl(Properties.Resources.test, "Test");
        //    CreateImageControl(Properties.Resources.Sub_task, "Sub-task");
        //}

        private void CreateImageControl(string issueType,Bitmap bitmap)
        {
            int desiredWidth = 16;
            int desiredHeight = 16;

            issueType = "   " + issueType;// trying to get a space in front of a long issueType

            // Create a new Bitmap with the desired size
            Bitmap resizedImage = new Bitmap(desiredWidth, desiredHeight);

            // Create a Graphics object from the new Bitmap
            using( Graphics g = Graphics.FromImage(resizedImage) )
            {
                // Set high-quality interpolation mode for better resizing
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                // Draw the original image onto the new Bitmap, scaling it
                g.DrawImage(bitmap, 0, 0, desiredWidth, desiredHeight);
            }

            // Dispose the original image if it's no longer needed
            bitmap.Dispose();

            Label control = new Label
            {
                Text = issueType,
                AutoSize = false,
                Margin = new Padding(4),
                Image = resizedImage,
                TextAlign = ContentAlignment.MiddleRight,
                ImageAlign = ContentAlignment.MiddleLeft,
            };

            Size textSize = TextRenderer.MeasureText(
                issueType,
                control.Font,
                Size.Empty, // A "no wrapping" constraint
                TextFormatFlags.SingleLine | TextFormatFlags.NoPadding
            );

            control.Width = textSize.Width+ desiredWidth + 20; 

            Controls.Add(control);
            //
        }

        internal void AddImage(string issueType, Image image)
        {
            CreateImageControl(issueType, (Bitmap)image);
        }
    }


    public class TableLayoutPanelEx : TableLayoutPanel, IReset
    {
        public void Reset()
        {
            foreach( Control control in this.Controls )
            {
                if( control is IReset reset )
                    reset.Reset();
            }
        }
    }

}
