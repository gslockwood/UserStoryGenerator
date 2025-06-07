using System.ComponentModel;

namespace UserStoryGenerator.View
{
    public partial class GroupBoxEx : UserControl, IReset
    {
        [Category("Misc")] // Groups the property in the Properties window
        [Description("Sets the text displayed in the control.")] // Provides a tooltip/description
        public string CaptionText
        {
            get { return groupBox.Text; } // Gets the current text from the internal label
            set { groupBox.Text = value; } // Sets the text of the internal label
        }
        [Category("Misc")] // Groups the property in the Properties window
        //[Description("Sets the text displayed in the control.")] // Provides a tooltip/description
        public bool Multiline
        {
            get { return textBox.Multiline; }
            set
            {
                textBox.Multiline = value;
                textBox.ScrollBars = ScrollBars.Vertical;
            }
        }
        [Category("Misc")] // Groups the property in the Properties window
        //[Description("Sets the text displayed in the control.")] // Provides a tooltip/description
        public bool UseSystemPasswordChar
        {
            get { return textBox.UseSystemPasswordChar; }
            set
            {
                textBox.UseSystemPasswordChar = value;
                if( value )
                {
                    textBox.PasswordChar = '*';
                    textBox.Enabled = false;
                }

            }
        }

        [Category("Misc")] // Groups the property in the Properties window
        public bool ReadOnly
        {
            get { return textBox.ReadOnly; }
            set { textBox.ReadOnly = value; }
        }

        [Category("Misc")] // Groups the property in the Properties window
        public string PlaceholderText
        {
            get { return textBox.PlaceholderText; }
            set { textBox.PlaceholderText = value; }
        }

        [Category("Misc")] // Groups the property in the Properties window
        public HorizontalAlignment TextAlign
        {
            get { return textBox.TextAlign; }
            set { textBox.TextAlign = value; }
        }

        [Category("Misc")]
        public Color TextBoxForeColor
        {
            get { return textBox.ForeColor; }
            set { textBox.ForeColor = value; }
        }


        public string? Value
        {
            get { return textBox.Text; }
            set
            {
                textBox.Text = value;
                if( Multiline )
                {
                    textBox.SelectionStart = 0;
                    textBox.SelectionLength = 0;
                }
            }
        }


        public int TextLength { get { return textBox.TextLength; } }

        public GroupBoxEx()
        {
            InitializeComponent();

            textBox.TextAlign = HorizontalAlignment.Left;

            textBox.TextChanged += (s, e) => { ValueChanged?.Invoke(this, new EventArgs()); };//this.OnTextChanged(new EventArgs());

        }

        public event EventHandler? ValueChanged;

        public void Reset()
        {
            textBox.Text = null;
        }
    }
}
