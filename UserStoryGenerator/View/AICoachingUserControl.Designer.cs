namespace UserStoryGenerator.View
{
    partial class AICoachingUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanelForm = new TableLayoutPanel();
            groupBoxExSubTaskAICoaching = new GroupBoxEx();
            groupBoxExQATestAICoaching = new GroupBoxEx();
            groupBoxExGeneralAICoaching = new GroupBoxEx();
            tableLayoutPanelForm.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelForm
            // 
            tableLayoutPanelForm.ColumnCount = 1;
            tableLayoutPanelForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelForm.Controls.Add(groupBoxExSubTaskAICoaching, 0, 2);
            tableLayoutPanelForm.Controls.Add(groupBoxExQATestAICoaching, 0, 1);
            tableLayoutPanelForm.Controls.Add(groupBoxExGeneralAICoaching, 0, 0);
            tableLayoutPanelForm.Dock = DockStyle.Fill;
            tableLayoutPanelForm.Location = new Point(0, 0);
            tableLayoutPanelForm.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelForm.Name = "tableLayoutPanelForm";
            tableLayoutPanelForm.RowCount = 3;
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Percent, 62.4999962F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Percent, 18.7500019F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Percent, 18.7500019F));
            tableLayoutPanelForm.Size = new Size(837, 1005);
            tableLayoutPanelForm.TabIndex = 1;
            // 
            // groupBoxExSubTaskAICoaching
            // 
            groupBoxExSubTaskAICoaching.CaptionText = "Sub-task AI Coaching";
            groupBoxExSubTaskAICoaching.Dock = DockStyle.Fill;
            groupBoxExSubTaskAICoaching.Location = new Point(3, 820);
            groupBoxExSubTaskAICoaching.Margin = new Padding(3, 4, 3, 4);
            groupBoxExSubTaskAICoaching.Multiline = true;
            groupBoxExSubTaskAICoaching.Name = "groupBoxExSubTaskAICoaching";
            groupBoxExSubTaskAICoaching.PlaceholderText = "place ai coaching and rules about creating Jira Sub-Task issues";
            groupBoxExSubTaskAICoaching.ReadOnly = false;
            groupBoxExSubTaskAICoaching.Size = new Size(831, 181);
            groupBoxExSubTaskAICoaching.TabIndex = 5;
            groupBoxExSubTaskAICoaching.TextAlign = HorizontalAlignment.Left;
            groupBoxExSubTaskAICoaching.TextBoxForeColor = SystemColors.WindowText;
            groupBoxExSubTaskAICoaching.UseSystemPasswordChar = false;
            groupBoxExSubTaskAICoaching.Value = "";
            // 
            // groupBoxExQATestAICoaching
            // 
            groupBoxExQATestAICoaching.CaptionText = "QA Test AI Coaching";
            groupBoxExQATestAICoaching.Dock = DockStyle.Fill;
            groupBoxExQATestAICoaching.Location = new Point(3, 632);
            groupBoxExQATestAICoaching.Margin = new Padding(3, 4, 3, 4);
            groupBoxExQATestAICoaching.Multiline = true;
            groupBoxExQATestAICoaching.Name = "groupBoxExQATestAICoaching";
            groupBoxExQATestAICoaching.PlaceholderText = "place ai coaching and rules about creating Jira Testing issues";
            groupBoxExQATestAICoaching.ReadOnly = false;
            groupBoxExQATestAICoaching.Size = new Size(831, 180);
            groupBoxExQATestAICoaching.TabIndex = 4;
            groupBoxExQATestAICoaching.TextAlign = HorizontalAlignment.Left;
            groupBoxExQATestAICoaching.TextBoxForeColor = SystemColors.WindowText;
            groupBoxExQATestAICoaching.UseSystemPasswordChar = false;
            groupBoxExQATestAICoaching.Value = "";
            // 
            // groupBoxExGeneralAICoaching
            // 
            groupBoxExGeneralAICoaching.CaptionText = "General AI Coaching";
            groupBoxExGeneralAICoaching.Dock = DockStyle.Fill;
            groupBoxExGeneralAICoaching.Location = new Point(3, 4);
            groupBoxExGeneralAICoaching.Margin = new Padding(3, 4, 3, 4);
            groupBoxExGeneralAICoaching.Multiline = true;
            groupBoxExGeneralAICoaching.Name = "groupBoxExGeneralAICoaching";
            groupBoxExGeneralAICoaching.PlaceholderText = "place ai coaching and rules about creating Jira user stories";
            groupBoxExGeneralAICoaching.ReadOnly = false;
            groupBoxExGeneralAICoaching.Size = new Size(831, 620);
            groupBoxExGeneralAICoaching.TabIndex = 3;
            groupBoxExGeneralAICoaching.TextAlign = HorizontalAlignment.Left;
            groupBoxExGeneralAICoaching.TextBoxForeColor = SystemColors.WindowText;
            groupBoxExGeneralAICoaching.UseSystemPasswordChar = false;
            groupBoxExGeneralAICoaching.Value = "";
            // 
            // AICoachingUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelForm);
            Name = "AICoachingUserControl";
            Size = new Size(837, 1005);
            tableLayoutPanelForm.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelForm;
        private GroupBoxEx groupBoxExSubTaskAICoaching;
        private GroupBoxEx groupBoxExQATestAICoaching;
        private GroupBoxEx groupBoxExGeneralAICoaching;
    }
}
