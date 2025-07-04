using static UserStoryGenerator.Model.Settings;

namespace UserStoryGenerator.View
{
    partial class IssueTypeUserControl
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
            flowLayoutPanelRadios = new FlowLayoutPanel();
            radioButtonSuper = new RadioButton();
            radioButtonStandard = new RadioButton();
            radioButtonSubtask = new RadioButton();
            buttonFilePath = new Button();
            groupBoxExIssueName = new GroupBoxEx();
            groupBoxImagePath = new GroupBox();
            pictureBoxImagePath = new PictureBox();
            groupBoxRadios = new GroupBox();
            groupBoxForeColor = new GroupBox();
            pictureBoxForeColor = new PictureBox();
            tableLayoutPanel = new TableLayoutPanel();
            flowLayoutPanelRadios.SuspendLayout();
            groupBoxImagePath.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)pictureBoxImagePath ).BeginInit();
            groupBoxRadios.SuspendLayout();
            groupBoxForeColor.SuspendLayout();
            ( (System.ComponentModel.ISupportInitialize)pictureBoxForeColor ).BeginInit();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanelRadios
            // 
            flowLayoutPanelRadios.Anchor =  AnchorStyles.Top  |  AnchorStyles.Right ;
            flowLayoutPanelRadios.Controls.Add(radioButtonSuper);
            flowLayoutPanelRadios.Controls.Add(radioButtonStandard);
            flowLayoutPanelRadios.Controls.Add(radioButtonSubtask);
            flowLayoutPanelRadios.Location = new Point(3, 34);
            flowLayoutPanelRadios.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanelRadios.Name = "flowLayoutPanelRadios";
            flowLayoutPanelRadios.Size = new Size(259, 32);
            flowLayoutPanelRadios.TabIndex = 1;
            // 
            // radioButtonSuper
            // 
            radioButtonSuper.AutoSize = true;
            radioButtonSuper.Location = new Point(3, 4);
            radioButtonSuper.Margin = new Padding(3, 4, 3, 4);
            radioButtonSuper.Name = "radioButtonSuper";
            radioButtonSuper.Size = new Size(65, 24);
            radioButtonSuper.TabIndex = 0;
            radioButtonSuper.Tag = 0;
            radioButtonSuper.Text = "Super";
            radioButtonSuper.UseVisualStyleBackColor = true;
            // 
            // radioButtonStandard
            // 
            radioButtonStandard.AutoSize = true;
            radioButtonStandard.Checked = true;
            radioButtonStandard.Location = new Point(74, 4);
            radioButtonStandard.Margin = new Padding(3, 4, 3, 4);
            radioButtonStandard.Name = "radioButtonStandard";
            radioButtonStandard.Size = new Size(87, 24);
            radioButtonStandard.TabIndex = 1;
            radioButtonStandard.TabStop = true;
            radioButtonStandard.Tag = 1;
            radioButtonStandard.Text = "Standard";
            radioButtonStandard.UseVisualStyleBackColor = true;
            // 
            // radioButtonSubtask
            // 
            radioButtonSubtask.AutoSize = true;
            radioButtonSubtask.Location = new Point(167, 4);
            radioButtonSubtask.Margin = new Padding(3, 4, 3, 4);
            radioButtonSubtask.Name = "radioButtonSubtask";
            radioButtonSubtask.Size = new Size(84, 24);
            radioButtonSubtask.TabIndex = 2;
            radioButtonSubtask.Tag = 2;
            radioButtonSubtask.Text = JiraIssueType.Sub_task;
            radioButtonSubtask.UseVisualStyleBackColor = true;
            // 
            // buttonFilePath
            // 
            buttonFilePath.Location = new Point(257, 49);
            buttonFilePath.Margin = new Padding(3, 4, 3, 4);
            buttonFilePath.Name = "buttonFilePath";
            buttonFilePath.Size = new Size(86, 31);
            buttonFilePath.TabIndex = 3;
            buttonFilePath.Text = "File...";
            buttonFilePath.UseVisualStyleBackColor = true;
            // 
            // groupBoxExIssueName
            // 
            groupBoxExIssueName.CaptionText = "IssueType (Name)";
            groupBoxExIssueName.Dock = DockStyle.Fill;
            groupBoxExIssueName.Location = new Point(3, 4);
            groupBoxExIssueName.Margin = new Padding(3, 4, 3, 4);
            groupBoxExIssueName.Multiline = false;
            groupBoxExIssueName.Name = "groupBoxExIssueName";
            groupBoxExIssueName.PlaceholderText = "enter a type like Story";
            groupBoxExIssueName.ReadOnly = false;
            groupBoxExIssueName.Size = new Size(277, 72);
            groupBoxExIssueName.TabIndex = 4;
            groupBoxExIssueName.TextAlign = HorizontalAlignment.Left;
            groupBoxExIssueName.TextBoxForeColor = SystemColors.WindowText;
            groupBoxExIssueName.UseSystemPasswordChar = false;
            groupBoxExIssueName.Value = "";
            groupBoxExIssueName.ValueChanged +=  GroupBoxExIssueName_ValueChanged ;
            // 
            // groupBoxImagePath
            // 
            groupBoxImagePath.Controls.Add(pictureBoxImagePath);
            groupBoxImagePath.Dock = DockStyle.Fill;
            groupBoxImagePath.Location = new Point(561, 3);
            groupBoxImagePath.Name = "groupBoxImagePath";
            groupBoxImagePath.Size = new Size(99, 74);
            groupBoxImagePath.TabIndex = 5;
            groupBoxImagePath.TabStop = false;
            groupBoxImagePath.Text = "Image Path";
            // 
            // pictureBoxImagePath
            // 
            pictureBoxImagePath.Anchor =  AnchorStyles.Top  |  AnchorStyles.Right ;
            pictureBoxImagePath.Location = new Point(57, 32);
            pictureBoxImagePath.Name = "pictureBoxImagePath";
            pictureBoxImagePath.Size = new Size(32, 32);
            pictureBoxImagePath.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxImagePath.TabIndex = 0;
            pictureBoxImagePath.TabStop = false;
            pictureBoxImagePath.Click +=  PictureBoxImagePath_Click ;
            // 
            // groupBoxRadios
            // 
            groupBoxRadios.Controls.Add(flowLayoutPanelRadios);
            groupBoxRadios.Dock = DockStyle.Fill;
            groupBoxRadios.Location = new Point(286, 3);
            groupBoxRadios.Name = "groupBoxRadios";
            groupBoxRadios.Size = new Size(269, 74);
            groupBoxRadios.TabIndex = 7;
            groupBoxRadios.TabStop = false;
            groupBoxRadios.Text = "Order";
            // 
            // groupBoxForeColor
            // 
            groupBoxForeColor.Controls.Add(pictureBoxForeColor);
            groupBoxForeColor.Dock = DockStyle.Fill;
            groupBoxForeColor.Location = new Point(666, 3);
            groupBoxForeColor.Name = "groupBoxForeColor";
            groupBoxForeColor.Size = new Size(99, 74);
            groupBoxForeColor.TabIndex = 8;
            groupBoxForeColor.TabStop = false;
            groupBoxForeColor.Text = "ForeColor";
            // 
            // pictureBoxForeColor
            // 
            pictureBoxForeColor.Anchor =  AnchorStyles.Top  |  AnchorStyles.Right ;
            pictureBoxForeColor.Location = new Point(57, 32);
            pictureBoxForeColor.Name = "pictureBoxForeColor";
            pictureBoxForeColor.Size = new Size(32, 32);
            pictureBoxForeColor.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxForeColor.TabIndex = 0;
            pictureBoxForeColor.TabStop = false;
            pictureBoxForeColor.Click +=  PictureBoxForeColor_Click ;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 4;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 275F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            tableLayoutPanel.Controls.Add(groupBoxExIssueName, 0, 0);
            tableLayoutPanel.Controls.Add(groupBoxForeColor, 3, 0);
            tableLayoutPanel.Controls.Add(groupBoxRadios, 1, 0);
            tableLayoutPanel.Controls.Add(groupBoxImagePath, 2, 0);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(768, 80);
            tableLayoutPanel.TabIndex = 9;
            // 
            // IssueTypeUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(3, 4, 3, 4);
            Name = "IssueTypeUserControl";
            Size = new Size(768, 80);
            flowLayoutPanelRadios.ResumeLayout(false);
            flowLayoutPanelRadios.PerformLayout();
            groupBoxImagePath.ResumeLayout(false);
            ( (System.ComponentModel.ISupportInitialize)pictureBoxImagePath ).EndInit();
            groupBoxRadios.ResumeLayout(false);
            groupBoxForeColor.ResumeLayout(false);
            ( (System.ComponentModel.ISupportInitialize)pictureBoxForeColor ).EndInit();
            tableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel flowLayoutPanelRadios;
        private RadioButton radioButtonSuper;
        private RadioButton radioButtonStandard;
        private RadioButton radioButtonSubtask;
        private Button buttonFilePath;
        private GroupBoxEx groupBoxExIssueName;
        private GroupBox groupBoxImagePath;
        private GroupBox groupBoxRadios;
        private PictureBox pictureBoxImagePath;
        private GroupBox groupBoxForeColor;
        private PictureBox pictureBoxForeColor;
        private TableLayoutPanel tableLayoutPanel;
    }
}
