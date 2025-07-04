namespace UserStoryGenerator.View
{
    public partial class EpicSelector : UserControl
    {
        public EpicSelector()
        {
            InitializeComponent();
        }

        public string? Value { get { return groupBoxExEpic.Value; } }

        public string? EpicNameOrKey
        {
            set
            {
                radioButtonJiraKey.Checked = false;
                radioButtonEpicName.Checked = true;

                if( value == null ) return;

                groupBoxExEpic.Value = value;
                radioButtonJiraKey.Checked = Utilities.InputValidator.IsJiraKey(value);
                GroupBoxExEpic_ValueChanged(this, new EventArgs());

            }
        }

        private void GroupBoxExEpic_ValueChanged(object sender, EventArgs e)
        {
            if( radioButtonJiraKey.Checked )
            {
                if( groupBoxExEpic.Value == null ) throw new NullReferenceException(nameof(groupBoxExEpic));

                string currentText = this.groupBoxExEpic.Value;

                if( currentText.Length == 0 )//|| currentText.Length> 8
                {
                    groupBoxExEpic.TextBoxForeColor = SystemColors.ControlText;
                    return;
                }

                bool found = Utilities.InputValidator.RegexContainsValidation(currentText);
                if( found )
                {
                    bool isValid = Utilities.InputValidator.IsJiraKey(currentText);

                    // Update the label based on the validation result
                    if( isValid )
                        groupBoxExEpic.TextBoxForeColor = SystemColors.ControlText;
                    else
                        groupBoxExEpic.TextBoxForeColor = System.Drawing.Color.Red;
                }
                else
                    groupBoxExEpic.TextBoxForeColor = System.Drawing.Color.Red;
            }
            else
                groupBoxExEpic.TextBoxForeColor = SystemColors.ControlText;
        }

        private void RadioButton_Click(object sender, EventArgs e)
        {
            groupBoxExEpic.Value = null;
            if( radioButtonJiraKey.Checked ) groupBoxExEpic.PlaceholderText = "enter an existing Jira Epic key (eg. PRJ-123)";
            else groupBoxExEpic.PlaceholderText = "enter an Epic Summary to create and add to or leave blank for no epic";
        }
    }
}
