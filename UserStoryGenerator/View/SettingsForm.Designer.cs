namespace UserStoryGenerator.View
{
    partial class SettingsForm
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
            if( disposing && ( components != null ) )
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
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelForm = new TableLayoutPanel();
            groupBoxExGeminiKey = new GroupBoxEx();
            groupBoxProjects = new GroupBox();
            listViewControl = new ListViewControl();
            tabControl = new TabControl();
            tabPageUserStories = new TabPage();
            aiCoachingUserControlUserStories = new AICoachingUserControl();
            tabPageAllIssues = new TabPage();
            aiCoachingUserControlAllIssues = new AICoachingUserControl();
            flowLayoutPanelButtons = new FlowLayoutPanel();
            buttonClose = new Button();
            buttonUse = new Button();
            buttonSave = new Button();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveasToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelForm.SuspendLayout();
            groupBoxProjects.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageUserStories.SuspendLayout();
            tabPageAllIssues.SuspendLayout();
            flowLayoutPanelButtons.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelForm, 0, 0);
            tableLayoutPanelMain.Controls.Add(flowLayoutPanelButtons, 0, 1);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 24);
            tableLayoutPanelMain.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanelMain.Size = new Size(1248, 1149);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelForm
            // 
            tableLayoutPanelForm.ColumnCount = 1;
            tableLayoutPanelForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelForm.Controls.Add(groupBoxExGeminiKey, 0, 0);
            tableLayoutPanelForm.Controls.Add(groupBoxProjects, 0, 2);
            tableLayoutPanelForm.Controls.Add(tabControl, 0, 1);
            tableLayoutPanelForm.Dock = DockStyle.Fill;
            tableLayoutPanelForm.Location = new Point(3, 4);
            tableLayoutPanelForm.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelForm.Name = "tableLayoutPanelForm";
            tableLayoutPanelForm.RowCount = 3;
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 83F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 250F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelForm.Size = new Size(1242, 1096);
            tableLayoutPanelForm.TabIndex = 0;
            // 
            // groupBoxExGeminiKey
            // 
            groupBoxExGeminiKey.CaptionText = "GeminiKey";
            groupBoxExGeminiKey.Dock = DockStyle.Fill;
            groupBoxExGeminiKey.Location = new Point(3, 4);
            groupBoxExGeminiKey.Margin = new Padding(3, 4, 3, 4);
            groupBoxExGeminiKey.Multiline = false;
            groupBoxExGeminiKey.Name = "groupBoxExGeminiKey";
            groupBoxExGeminiKey.PlaceholderText = "";
            groupBoxExGeminiKey.ReadOnly = false;
            groupBoxExGeminiKey.Size = new Size(1236, 75);
            groupBoxExGeminiKey.TabIndex = 2;
            groupBoxExGeminiKey.TextAlign = HorizontalAlignment.Left;
            groupBoxExGeminiKey.TextBoxForeColor = SystemColors.WindowText;
            groupBoxExGeminiKey.UseSystemPasswordChar = false;
            groupBoxExGeminiKey.Value = "";
            // 
            // groupBoxProjects
            // 
            groupBoxProjects.Controls.Add(listViewControl);
            groupBoxProjects.Dock = DockStyle.Fill;
            groupBoxProjects.Location = new Point(3, 849);
            groupBoxProjects.Name = "groupBoxProjects";
            groupBoxProjects.Size = new Size(1236, 244);
            groupBoxProjects.TabIndex = 6;
            groupBoxProjects.TabStop = false;
            groupBoxProjects.Text = "Jira Projects";
            // 
            // listViewControl
            // 
            listViewControl.Dock = DockStyle.Fill;
            listViewControl.Location = new Point(3, 23);
            listViewControl.Name = "listViewControl";
            listViewControl.Size = new Size(1230, 218);
            listViewControl.TabIndex = 0;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageUserStories);
            tabControl.Controls.Add(tabPageAllIssues);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(3, 86);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1236, 757);
            tabControl.TabIndex = 7;
            // 
            // tabPageUserStories
            // 
            tabPageUserStories.Controls.Add(aiCoachingUserControlUserStories);
            tabPageUserStories.Location = new Point(4, 29);
            tabPageUserStories.Name = "tabPageUserStories";
            tabPageUserStories.Padding = new Padding(3);
            tabPageUserStories.Size = new Size(1228, 724);
            tabPageUserStories.TabIndex = 0;
            tabPageUserStories.Text = "User Stories";
            tabPageUserStories.UseVisualStyleBackColor = true;
            // 
            // aiCoachingUserControlUserStories
            // 
            aiCoachingUserControlUserStories.Dock = DockStyle.Fill;
            aiCoachingUserControlUserStories.IssueInstructions = "";
            aiCoachingUserControlUserStories.Location = new Point(3, 3);
            aiCoachingUserControlUserStories.Margin = new Padding(3, 4, 3, 4);
            aiCoachingUserControlUserStories.Name = "aiCoachingUserControlUserStories";
            aiCoachingUserControlUserStories.QATestInstructions = "";
            aiCoachingUserControlUserStories.Size = new Size(1222, 718);
            aiCoachingUserControlUserStories.SubTaskInstructions = "";
            aiCoachingUserControlUserStories.TabIndex = 0;
            // 
            // tabPageAllIssues
            // 
            tabPageAllIssues.Controls.Add(aiCoachingUserControlAllIssues);
            tabPageAllIssues.Location = new Point(4, 24);
            tabPageAllIssues.Name = "tabPageAllIssues";
            tabPageAllIssues.Padding = new Padding(3);
            tabPageAllIssues.Size = new Size(1228, 729);
            tabPageAllIssues.TabIndex = 1;
            tabPageAllIssues.Text = "All Issues";
            tabPageAllIssues.UseVisualStyleBackColor = true;
            // 
            // aiCoachingUserControlAllIssues
            // 
            aiCoachingUserControlAllIssues.Dock = DockStyle.Fill;
            aiCoachingUserControlAllIssues.IssueInstructions = "";
            aiCoachingUserControlAllIssues.Location = new Point(3, 3);
            aiCoachingUserControlAllIssues.Margin = new Padding(3, 4, 3, 4);
            aiCoachingUserControlAllIssues.Name = "aiCoachingUserControlAllIssues";
            aiCoachingUserControlAllIssues.QATestInstructions = "";
            aiCoachingUserControlAllIssues.Size = new Size(1222, 723);
            aiCoachingUserControlAllIssues.SubTaskInstructions = "";
            aiCoachingUserControlAllIssues.TabIndex = 0;
            // 
            // flowLayoutPanelButtons
            // 
            flowLayoutPanelButtons.Controls.Add(buttonClose);
            flowLayoutPanelButtons.Controls.Add(buttonUse);
            flowLayoutPanelButtons.Dock = DockStyle.Fill;
            flowLayoutPanelButtons.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanelButtons.Location = new Point(3, 1108);
            flowLayoutPanelButtons.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            flowLayoutPanelButtons.Size = new Size(1242, 37);
            flowLayoutPanelButtons.TabIndex = 0;
            // 
            // buttonClose
            // 
            buttonClose.Location = new Point(1153, 4);
            buttonClose.Margin = new Padding(3, 4, 3, 4);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(86, 31);
            buttonClose.TabIndex = 1;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click +=  ButtonClose_Click ;
            // 
            // buttonUse
            // 
            buttonUse.Location = new Point(1061, 4);
            buttonUse.Margin = new Padding(3, 4, 3, 4);
            buttonUse.Name = "buttonUse";
            buttonUse.Size = new Size(86, 31);
            buttonUse.TabIndex = 2;
            buttonUse.Text = "Use";
            buttonUse.UseVisualStyleBackColor = true;
            buttonUse.Click +=  ButtonUse_Click ;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(969, 4);
            buttonSave.Margin = new Padding(3, 4, 3, 4);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(86, 31);
            buttonSave.TabIndex = 3;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click +=  ButtonSave_Click ;
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1248, 24);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveasToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(121, 22);
            openToolStripMenuItem.Text = "&Open...";
            openToolStripMenuItem.Click +=  OpenToolStripMenuItem_Click ;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(121, 22);
            saveToolStripMenuItem.Text = "&Save...";
            saveToolStripMenuItem.Click +=  SaveToolStripMenuItem_Click ;
            // 
            // saveasToolStripMenuItem
            // 
            saveasToolStripMenuItem.Name = "saveasToolStripMenuItem";
            saveasToolStripMenuItem.Size = new Size(121, 22);
            saveasToolStripMenuItem.Text = "Save &as...";
            saveasToolStripMenuItem.Click +=  SaveasToolStripMenuItem_Click ;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonClose;
            ClientSize = new Size(1248, 1173);
            Controls.Add(tableLayoutPanelMain);
            Controls.Add(menuStrip);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = menuStrip;
            Margin = new Padding(3, 4, 3, 4);
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Tag = "Settings Form - ";
            Text = "Settings Form";
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelForm.ResumeLayout(false);
            groupBoxProjects.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabPageUserStories.ResumeLayout(false);
            tabPageAllIssues.ResumeLayout(false);
            flowLayoutPanelButtons.ResumeLayout(false);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelForm;
        private GroupBoxEx groupBoxExGeminiKey;
        private FlowLayoutPanel flowLayoutPanelButtons;
        private Button buttonClose;
        private Button buttonUse;
        private Button buttonSave;
        private GroupBox groupBoxProjects;
        private ListViewControl listViewControl;
        private TabControl tabControl;
        private TabPage tabPageUserStories;
        private TabPage tabPageAllIssues;
        private AICoachingUserControl aiCoachingUserControlUserStories;
        private AICoachingUserControl aiCoachingUserControlAllIssues;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveasToolStripMenuItem;
    }
}