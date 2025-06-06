namespace UserStoryGenerator.View
{
    public partial class ListViewControl : UserControl
    {
        public ListViewControl()
        {
            InitializeComponent();

        }
        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            // Get the text from the input TextBox.
            string newItemText = txtNewItem.Text.Trim();

            // Check if the text is not empty or just whitespace.
            if( !string.IsNullOrEmpty(newItemText) )
            {
                // Create a new ListViewItem with the entered text.
                ListViewItem newItem = new ListViewItem(newItemText);

                // Add the new item to the ListView's Items collection.
                listViewItems.Items.Add(newItem);

                // Clear the TextBox after adding the item, and set focus back to it
                // for easy entry of the next item.
                txtNewItem.Clear();
                txtNewItem.Focus();
                labelError.Text = null;
            }
            else
            {
                // Optionally, provide feedback to the user if the input is empty.
                // Using a custom message box instead of alert() as per instructions.
                //MessageBox.Show("Please enter an item to add.", "Empty Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                labelError.Text = "Please enter an item to add.";
            }
        }

        // Event handler for when the selection in the ListView changes.
        private void listViewItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if any item is selected.
            if( listViewItems.SelectedItems.Count > 0 )
            {
                // Get the text of the first selected item and populate the edit TextBox.
                txtEditItem.Text = listViewItems.SelectedItems[0].Text;
                txtEditItem.Enabled = true;    // Enable edit controls
                btnUpdateItem.Enabled = true;  // Enable update button

                buttonDelete.Enabled = true;
            }
            else
            {
                // If no item is selected, clear the edit TextBox and disable edit controls.
                txtEditItem.Clear();
                txtEditItem.Enabled = false;
                btnUpdateItem.Enabled = false;
                buttonDelete.Enabled = false;
            }
        }

        // Event handler for the 'Update Item' button click.
        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            // Ensure an item is selected before attempting to update.
            if( listViewItems.SelectedItems.Count > 0 )
            {
                // Get the selected item.
                ListViewItem selectedItem = listViewItems.SelectedItems[0];

                // Get the new text from the edit TextBox.
                string updatedText = txtEditItem.Text.Trim();

                // Check if the updated text is not empty.
                if( !string.IsNullOrEmpty(updatedText) )
                {
                    labelError.Text = null;

                    // Update the text of the selected ListViewItem.
                    selectedItem.Text = updatedText;
                    txtEditItem.Clear(); // Clear the edit box after update
                    txtEditItem.Enabled = false; // Disable edit controls
                    btnUpdateItem.Enabled = false;
                }
                else
                {
                    //MessageBox.Show("Please enter a value to update the item.", "Empty Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    labelError.Text = "Please enter a value to update the item.";
                }
            }
        }

        // Public method to retrieve the current list of items from the ListView.
        // Returns a List<string> containing the text of each item.
        public List<string> GetItems()
        {
            // Create a new list to hold the extracted item texts.
            List<string> itemsList = new List<string>();

            // Iterate through each ListViewItem in the ListView's Items collection.
            foreach( ListViewItem item in listViewItems.Items )
            {
                // Add the text of the current ListViewItem to the list.
                itemsList.Add(item.Text);
            }

            // Return the compiled list of strings.
            return itemsList;
        }

        // You can also add a method to clear all items from the listview
        public void ClearItems()
        {
            listViewItems.Items.Clear();
        }

        // You might want to expose a way to pre-populate the list
        public void SetItems(IEnumerable<string> items)
        {
            ClearItems(); // Clear existing items first
            foreach( string item in items )
            {
                listViewItems.Items.Add(new ListViewItem(item));
            }
        }
    }
}
