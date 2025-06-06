namespace UserStoryGenerator.View
{
    partial class ListViewControl
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
            txtNewItem = new TextBox();
            btnAddItem = new Button();
            listViewItems = new ListView();
            txtEditItem = new TextBox();
            btnUpdateItem = new Button();
            labelError = new Label();
            tableLayoutPanelAdd = new TableLayoutPanel();
            tableLayoutPanelEdit = new TableLayoutPanel();
            tableLayoutPanelControls = new TableLayoutPanel();
            buttonDelete = new Button();
            tableLayoutPanelAdd.SuspendLayout();
            tableLayoutPanelEdit.SuspendLayout();
            tableLayoutPanelControls.SuspendLayout();
            SuspendLayout();
            // 
            // txtNewItem
            // 
            txtNewItem.Dock = DockStyle.Fill;
            txtNewItem.Location = new Point(3, 13);
            txtNewItem.Margin = new Padding(3, 13, 3, 3);
            txtNewItem.Name = "txtNewItem";
            txtNewItem.Size = new Size(307, 23);
            txtNewItem.TabIndex = 0;
            // 
            // btnAddItem
            // 
            btnAddItem.Dock = DockStyle.Fill;
            btnAddItem.Location = new Point(316, 11);
            btnAddItem.Margin = new Padding(3, 11, 3, 11);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(74, 26);
            btnAddItem.TabIndex = 1;
            btnAddItem.Text = "Add Item";
            btnAddItem.UseVisualStyleBackColor = true;
            btnAddItem.Click +=  BtnAddItem_Click ;
            // 
            // listViewItems
            // 
            listViewItems.Anchor =    AnchorStyles.Top  |  AnchorStyles.Bottom   |  AnchorStyles.Left   |  AnchorStyles.Right ;
            listViewItems.Location = new Point(3, 63);
            listViewItems.Name = "listViewItems";
            listViewItems.Size = new Size(879, 157);
            listViewItems.TabIndex = 2;
            listViewItems.UseCompatibleStateImageBehavior = false;
            listViewItems.View = System.Windows.Forms.View.List;
            listViewItems.SelectedIndexChanged +=  listViewItems_SelectedIndexChanged ;
            // 
            // txtEditItem
            // 
            txtEditItem.Dock = DockStyle.Fill;
            txtEditItem.Enabled = false;
            txtEditItem.Location = new Point(3, 13);
            txtEditItem.Margin = new Padding(3, 13, 3, 3);
            txtEditItem.Name = "txtEditItem";
            txtEditItem.Size = new Size(307, 23);
            txtEditItem.TabIndex = 3;
            // 
            // btnUpdateItem
            // 
            btnUpdateItem.Dock = DockStyle.Fill;
            btnUpdateItem.Enabled = false;
            btnUpdateItem.Location = new Point(316, 11);
            btnUpdateItem.Margin = new Padding(3, 11, 3, 11);
            btnUpdateItem.Name = "btnUpdateItem";
            btnUpdateItem.Size = new Size(74, 26);
            btnUpdateItem.TabIndex = 4;
            btnUpdateItem.Text = "Update";
            btnUpdateItem.UseVisualStyleBackColor = true;
            btnUpdateItem.Click +=  btnUpdateItem_Click ;
            // 
            // labelError
            // 
            labelError.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            labelError.ForeColor = Color.LightCoral;
            labelError.Location = new Point(3, 224);
            labelError.Name = "labelError";
            labelError.Size = new Size(879, 17);
            labelError.TabIndex = 5;
            // 
            // tableLayoutPanelAdd
            // 
            tableLayoutPanelAdd.ColumnCount = 2;
            tableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelAdd.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanelAdd.Controls.Add(btnAddItem, 1, 0);
            tableLayoutPanelAdd.Controls.Add(txtNewItem, 0, 0);
            tableLayoutPanelAdd.Dock = DockStyle.Fill;
            tableLayoutPanelAdd.Location = new Point(3, 3);
            tableLayoutPanelAdd.Name = "tableLayoutPanelAdd";
            tableLayoutPanelAdd.RowCount = 1;
            tableLayoutPanelAdd.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelAdd.Size = new Size(393, 48);
            tableLayoutPanelAdd.TabIndex = 7;
            // 
            // tableLayoutPanelEdit
            // 
            tableLayoutPanelEdit.ColumnCount = 2;
            tableLayoutPanelEdit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelEdit.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanelEdit.Controls.Add(txtEditItem, 0, 0);
            tableLayoutPanelEdit.Controls.Add(btnUpdateItem, 1, 0);
            tableLayoutPanelEdit.Dock = DockStyle.Fill;
            tableLayoutPanelEdit.Location = new Point(402, 3);
            tableLayoutPanelEdit.Name = "tableLayoutPanelEdit";
            tableLayoutPanelEdit.RowCount = 1;
            tableLayoutPanelEdit.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelEdit.Size = new Size(393, 48);
            tableLayoutPanelEdit.TabIndex = 8;
            // 
            // tableLayoutPanelControls
            // 
            tableLayoutPanelControls.Anchor =   AnchorStyles.Top  |  AnchorStyles.Left   |  AnchorStyles.Right ;
            tableLayoutPanelControls.ColumnCount = 3;
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanelControls.Controls.Add(tableLayoutPanelAdd, 0, 0);
            tableLayoutPanelControls.Controls.Add(tableLayoutPanelEdit, 1, 0);
            tableLayoutPanelControls.Controls.Add(buttonDelete, 2, 0);
            tableLayoutPanelControls.Location = new Point(3, 3);
            tableLayoutPanelControls.Name = "tableLayoutPanelControls";
            tableLayoutPanelControls.RowCount = 1;
            tableLayoutPanelControls.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelControls.Size = new Size(879, 54);
            tableLayoutPanelControls.TabIndex = 9;
            // 
            // buttonDelete
            // 
            buttonDelete.Dock = DockStyle.Fill;
            buttonDelete.Enabled = false;
            buttonDelete.Location = new Point(801, 13);
            buttonDelete.Margin = new Padding(3, 13, 3, 13);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(75, 28);
            buttonDelete.TabIndex = 9;
            buttonDelete.Text = "Delete";
            buttonDelete.UseVisualStyleBackColor = true;
            // 
            // ListViewControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelControls);
            Controls.Add(labelError);
            Controls.Add(listViewItems);
            Name = "ListViewControl";
            Size = new Size(885, 250);
            tableLayoutPanelAdd.ResumeLayout(false);
            tableLayoutPanelAdd.PerformLayout();
            tableLayoutPanelEdit.ResumeLayout(false);
            tableLayoutPanelEdit.PerformLayout();
            tableLayoutPanelControls.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.TextBox txtNewItem;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.ListView listViewItems;
        private System.Windows.Forms.TextBox txtEditItem;   // New TextBox for editing
        private System.Windows.Forms.Button btnUpdateItem; // New Button for updating

        #endregion

        private Label labelError;
        private TableLayoutPanel tableLayoutPanelAdd;
        private TableLayoutPanel tableLayoutPanelEdit;
        private TableLayoutPanel tableLayoutPanelControls;
        private Button buttonDelete;
    }
}
