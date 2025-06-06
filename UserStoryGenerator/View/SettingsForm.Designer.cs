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
            groupBoxExSubTaskAICoaching = new GroupBoxEx();
            groupBoxExQATestAICoaching = new GroupBoxEx();
            groupBoxExGeminiKey = new GroupBoxEx();
            groupBoxExGeneralAICoaching = new GroupBoxEx();
            groupBoxProjects = new GroupBox();
            listViewControl = new ListViewControl();
            flowLayoutPanelButtons = new FlowLayoutPanel();
            buttonClose = new Button();
            buttonUse = new Button();
            buttonSave = new Button();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelForm.SuspendLayout();
            groupBoxProjects.SuspendLayout();
            flowLayoutPanelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelForm, 0, 0);
            tableLayoutPanelMain.Controls.Add(flowLayoutPanelButtons, 0, 1);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanelMain.Size = new Size(1248, 1173);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelForm
            // 
            tableLayoutPanelForm.ColumnCount = 1;
            tableLayoutPanelForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelForm.Controls.Add(groupBoxExSubTaskAICoaching, 0, 3);
            tableLayoutPanelForm.Controls.Add(groupBoxExQATestAICoaching, 0, 2);
            tableLayoutPanelForm.Controls.Add(groupBoxExGeminiKey, 0, 0);
            tableLayoutPanelForm.Controls.Add(groupBoxExGeneralAICoaching, 0, 1);
            tableLayoutPanelForm.Controls.Add(groupBoxProjects, 0, 4);
            tableLayoutPanelForm.Dock = DockStyle.Fill;
            tableLayoutPanelForm.Location = new Point(3, 4);
            tableLayoutPanelForm.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelForm.Name = "tableLayoutPanelForm";
            tableLayoutPanelForm.RowCount = 5;
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 83F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 160F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 250F));
            tableLayoutPanelForm.Size = new Size(1242, 1120);
            tableLayoutPanelForm.TabIndex = 0;
            // 
            // groupBoxExSubTaskAICoaching
            // 
            groupBoxExSubTaskAICoaching.CaptionText = "Sub-task AI Coaching";
            groupBoxExSubTaskAICoaching.Dock = DockStyle.Fill;
            groupBoxExSubTaskAICoaching.Location = new Point(3, 714);
            groupBoxExSubTaskAICoaching.Margin = new Padding(3, 4, 3, 4);
            groupBoxExSubTaskAICoaching.Multiline = true;
            groupBoxExSubTaskAICoaching.Name = "groupBoxExSubTaskAICoaching";
            groupBoxExSubTaskAICoaching.Size = new Size(1236, 152);
            groupBoxExSubTaskAICoaching.TabIndex = 5;
            groupBoxExSubTaskAICoaching.UseSystemPasswordChar = false;
            groupBoxExSubTaskAICoaching.Value = "";
            // 
            // groupBoxExQATestAICoaching
            // 
            groupBoxExQATestAICoaching.CaptionText = "QA Test AI Coaching";
            groupBoxExQATestAICoaching.Dock = DockStyle.Fill;
            groupBoxExQATestAICoaching.Location = new Point(3, 554);
            groupBoxExQATestAICoaching.Margin = new Padding(3, 4, 3, 4);
            groupBoxExQATestAICoaching.Multiline = true;
            groupBoxExQATestAICoaching.Name = "groupBoxExQATestAICoaching";
            groupBoxExQATestAICoaching.Size = new Size(1236, 152);
            groupBoxExQATestAICoaching.TabIndex = 4;
            groupBoxExQATestAICoaching.UseSystemPasswordChar = false;
            groupBoxExQATestAICoaching.Value = "";
            // 
            // groupBoxExGeminiKey
            // 
            groupBoxExGeminiKey.CaptionText = "GeminiKey";
            groupBoxExGeminiKey.Dock = DockStyle.Fill;
            groupBoxExGeminiKey.Location = new Point(3, 4);
            groupBoxExGeminiKey.Margin = new Padding(3, 4, 3, 4);
            groupBoxExGeminiKey.Multiline = false;
            groupBoxExGeminiKey.Name = "groupBoxExGeminiKey";
            groupBoxExGeminiKey.Size = new Size(1236, 75);
            groupBoxExGeminiKey.TabIndex = 2;
            groupBoxExGeminiKey.UseSystemPasswordChar = false;
            groupBoxExGeminiKey.Value = "";
            // 
            // groupBoxExGeneralAICoaching
            // 
            groupBoxExGeneralAICoaching.CaptionText = "General AI Coaching";
            groupBoxExGeneralAICoaching.Dock = DockStyle.Fill;
            groupBoxExGeneralAICoaching.Location = new Point(3, 87);
            groupBoxExGeneralAICoaching.Margin = new Padding(3, 4, 3, 4);
            groupBoxExGeneralAICoaching.Multiline = true;
            groupBoxExGeneralAICoaching.Name = "groupBoxExGeneralAICoaching";
            groupBoxExGeneralAICoaching.Size = new Size(1236, 459);
            groupBoxExGeneralAICoaching.TabIndex = 3;
            groupBoxExGeneralAICoaching.UseSystemPasswordChar = false;
            groupBoxExGeneralAICoaching.Value = "";
            // 
            // groupBoxProjects
            // 
            groupBoxProjects.Controls.Add(listViewControl);
            groupBoxProjects.Dock = DockStyle.Fill;
            groupBoxProjects.Location = new Point(3, 873);
            groupBoxProjects.Name = "groupBoxProjects";
            groupBoxProjects.Size = new Size(1236, 244);
            groupBoxProjects.TabIndex = 6;
            groupBoxProjects.TabStop = false;
            groupBoxProjects.Text = "Projects";
            // 
            // listViewControl
            // 
            listViewControl.Dock = DockStyle.Fill;
            listViewControl.Location = new Point(3, 23);
            listViewControl.Name = "listViewControl";
            listViewControl.Size = new Size(1230, 218);
            listViewControl.TabIndex = 0;
            // 
            // flowLayoutPanelButtons
            // 
            flowLayoutPanelButtons.Controls.Add(buttonClose);
            flowLayoutPanelButtons.Controls.Add(buttonUse);
            flowLayoutPanelButtons.Controls.Add(buttonSave);
            flowLayoutPanelButtons.Dock = DockStyle.Fill;
            flowLayoutPanelButtons.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanelButtons.Location = new Point(3, 1132);
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
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonClose;
            ClientSize = new Size(1248, 1173);
            Controls.Add(tableLayoutPanelMain);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(3, 4, 3, 4);
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SettingsForm";
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelForm.ResumeLayout(false);
            groupBoxProjects.ResumeLayout(false);
            flowLayoutPanelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelForm;
        private GroupBoxEx groupBoxExGeminiKey;
        private GroupBoxEx groupBoxExGeneralAICoaching;
        private FlowLayoutPanel flowLayoutPanelButtons;
        private Button buttonClose;
        private Button buttonUse;
        private Button buttonSave;
        private GroupBoxEx groupBoxExSubTaskAICoaching;
        private GroupBoxEx groupBoxExQATestAICoaching;
        private GroupBox groupBoxProjects;
        private ListViewControl listViewControl;
    }
}