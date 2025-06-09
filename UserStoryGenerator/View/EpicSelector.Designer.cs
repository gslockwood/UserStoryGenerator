using System.Windows.Forms;

namespace UserStoryGenerator.View
{
    partial class EpicSelector
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
            tableLayoutPanel = new TableLayoutPanel();
            groupBoxExEpic = new GroupBoxEx();
            groupBoxCheckBoxes = new GroupBox();
            flowLayoutPanelChkBox = new FlowLayoutPanel();
            radioButtonEpicName = new RadioButton();
            radioButtonJiraKey = new RadioButton();
            tableLayoutPanel.SuspendLayout();
            groupBoxCheckBoxes.SuspendLayout();
            flowLayoutPanelChkBox.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 221F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(groupBoxExEpic, 1, 0);
            tableLayoutPanel.Controls.Add(groupBoxCheckBoxes, 0, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(853, 78);
            tableLayoutPanel.TabIndex = 0;
            // 
            // groupBoxExEpic
            // 
            groupBoxExEpic.CaptionText = "Epic Name or Jira Key";
            groupBoxExEpic.Dock = DockStyle.Fill;
            groupBoxExEpic.Location = new Point(224, 4);
            groupBoxExEpic.Margin = new Padding(3, 4, 3, 4);
            groupBoxExEpic.Multiline = false;
            groupBoxExEpic.Name = "groupBoxExEpic";
            groupBoxExEpic.PlaceholderText = "";
            groupBoxExEpic.ReadOnly = false;
            groupBoxExEpic.Size = new Size(626, 70);
            groupBoxExEpic.TabIndex = 1;
            groupBoxExEpic.TextAlign = HorizontalAlignment.Left;
            groupBoxExEpic.TextBoxForeColor = SystemColors.WindowText;
            groupBoxExEpic.UseSystemPasswordChar = false;
            groupBoxExEpic.Value = "";
            groupBoxExEpic.ValueChanged +=  GroupBoxExEpic_ValueChanged ;
            // 
            // groupBoxCheckBoxes
            // 
            groupBoxCheckBoxes.Controls.Add(flowLayoutPanelChkBox);
            groupBoxCheckBoxes.Dock = DockStyle.Fill;
            groupBoxCheckBoxes.Location = new Point(3, 4);
            groupBoxCheckBoxes.Margin = new Padding(3, 4, 3, 4);
            groupBoxCheckBoxes.Name = "groupBoxCheckBoxes";
            groupBoxCheckBoxes.Padding = new Padding(3, 4, 3, 4);
            groupBoxCheckBoxes.Size = new Size(215, 70);
            groupBoxCheckBoxes.TabIndex = 2;
            groupBoxCheckBoxes.TabStop = false;
            groupBoxCheckBoxes.Text = "Epic Type";
            // 
            // flowLayoutPanelChkBox
            // 
            flowLayoutPanelChkBox.Controls.Add(radioButtonEpicName);
            flowLayoutPanelChkBox.Controls.Add(radioButtonJiraKey);
            flowLayoutPanelChkBox.Dock = DockStyle.Bottom;
            flowLayoutPanelChkBox.Location = new Point(3, 32);
            flowLayoutPanelChkBox.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelChkBox.Name = "flowLayoutPanelChkBox";
            flowLayoutPanelChkBox.Size = new Size(209, 34);
            flowLayoutPanelChkBox.TabIndex = 0;
            // 
            // radioButtonEpicName
            // 
            radioButtonEpicName.Anchor =   AnchorStyles.Bottom  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            radioButtonEpicName.Checked = true;
            radioButtonEpicName.Location = new Point(3, 4);
            radioButtonEpicName.Margin = new Padding(3, 4, 3, 4);
            radioButtonEpicName.Name = "radioButtonEpicName";
            radioButtonEpicName.Size = new Size(104, 25);
            radioButtonEpicName.TabIndex = 4;
            radioButtonEpicName.TabStop = true;
            radioButtonEpicName.Text = "Epic Name";
            radioButtonEpicName.UseVisualStyleBackColor = true;
            radioButtonEpicName.Click +=  RadioButton_Click ;
            // 
            // radioButtonJiraKey
            // 
            radioButtonJiraKey.Anchor =   AnchorStyles.Bottom  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            radioButtonJiraKey.Location = new Point(113, 4);
            radioButtonJiraKey.Margin = new Padding(3, 4, 3, 4);
            radioButtonJiraKey.Name = "radioButtonJiraKey";
            radioButtonJiraKey.Size = new Size(82, 25);
            radioButtonJiraKey.TabIndex = 5;
            radioButtonJiraKey.Text = "Jira Key";
            radioButtonJiraKey.UseVisualStyleBackColor = true;
            radioButtonJiraKey.Click +=  RadioButton_Click ;
            // 
            // EpicSelector
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel);
            Font = new Font("Segoe UI", 11.25F);
            Margin = new Padding(3, 4, 3, 4);
            Name = "EpicSelector";
            Size = new Size(853, 78);
            tableLayoutPanel.ResumeLayout(false);
            groupBoxCheckBoxes.ResumeLayout(false);
            flowLayoutPanelChkBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel;
        private GroupBoxEx groupBoxExEpic;
        private GroupBox groupBoxCheckBoxes;
        private FlowLayoutPanel flowLayoutPanelChkBox;
        private RadioButton radioButtonEpicName;
        private RadioButton radioButtonJiraKey;
    }
}
