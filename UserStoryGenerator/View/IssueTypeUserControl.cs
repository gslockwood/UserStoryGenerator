using UserStoryGenerator.Model;

namespace UserStoryGenerator.View
{
    public partial class IssueTypeUserControl : UserControl
    {
        public event EventHandler? ControlSelected;
        //private readonly string? json;

        //
        public IssueTypeUserControl(Model.Settings.JiraIssueType? jiraIssue = null)
        {
            InitializeComponent();

            if( jiraIssue == null ) return;
            if( jiraIssue.ForeColor == null ) return;
            //if( jiraIssue.ImagePath == null ) return;

            this.Click += UserControlX_Click; // Subscribe to its own Click event
            foreach( Control control in this.Controls )
            {
                control.Click += ChildControl_Click; // Also subscribe child controls' clicks
                if( control.Controls.Count > 0 )
                    Recursive(control);

            }


            groupBoxExIssueName.Value = jiraIssue.IssueType;

            Image? image = null;
            if( jiraIssue.ImagePath == null )
            {
                image = Properties.Resources.unknown;
                jiraIssue.ImagePath = "unknown";
            }
            else
                image = Utilities.ImageLoader.GetImageFromFilePath(jiraIssue.ImagePath);


            //Image? image = Utilities.ImageLoader.GetImageFromFilePath(jiraIssue.ImagePath);
            pictureBoxImagePath.Image = image;
            pictureBoxImagePath.Tag = jiraIssue.ImagePath;

            pictureBoxForeColor.BackColor = Color.FromName(jiraIssue.ForeColor);

            if( jiraIssue.Order == 0 ) radioButtonSuper.Checked = true;
            else if( jiraIssue.Order == 2 ) radioButtonSubtask.Checked = true;
            else radioButtonStandard.Checked = true;

            //json = JsonSerializer.Serialize(jiraIssue);
            //
        }

        private void Recursive(Control control)
        {
            foreach( Control subControl in control.Controls )
            {
                subControl.Click += ChildControl_Click;
                //Logger.Info(subControl.Name);
                if( subControl.Controls.Count > 0 )
                    Recursive(subControl);

            }
        }

        private void PictureBoxImagePath_Click(object? sender, EventArgs e)
        {
            OpenFileDialog form = new()
            {
                //D:\repos\UserStoryGenerator\UserStoryGenerator\bin\Debug\net8.0-windows\Data\Resources
                InitialDirectory = @"Data\Resources",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tif;*.tiff;*.webp;*.ico;*.svg|" +
                "JPEG Images (*.jpg;*.jpeg;*.jfif;*.jpe)|*.jpg;*.jpeg;*.jfif;*.jpe|" +
                "PNG Images (*.png)|*.png|" +
                "GIF Images (*.gif)|*.gif|" +
                "BMP Images (*.bmp;*.dib;*.rle)|*.bmp;*.dib;*.rle|" +
                "TIFF Images (*.tif;*.tiff)|*.tif;*.tiff|" +
                "WebP Images (*.webp)|*.webp|" +
                "SVG Images (*.svg)|*.svg|" +
                "Icon Files (*.ico)|*.ico|" +
                "All Files (*.*)|*.*",
                FilterIndex = 1, // Sets the default selected filter to "Text Files"
                //RestoreDirectory = true, // Restores the directory to the previously selected one
            };

            if( form.ShowDialog() == DialogResult.OK )
            {
                form.RestoreDirectory = true;
                try
                {
                    Image? image = Utilities.ImageLoader.GetImageFromFilePath(form.FileName);
                    pictureBoxImagePath.Image = image;
                    pictureBoxImagePath.Tag = form.FileName;

                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PictureBoxForeColor_Click(object? sender, EventArgs e)
        {
            ColorDialog form = new();
            if( form.ShowDialog() == DialogResult.OK )
            {
                pictureBoxForeColor.BackColor = form.Color;
            }
        }

        public class IssueDefinitionException(string? message) : Exception(message) { }

        private void GroupBoxExIssueName_ValueChanged(object? sender, EventArgs e)
        {
            if( groupBoxExIssueName.TextLength == 0 )
                groupBoxExIssueName.BackColor = Color.Red;
            else groupBoxExIssueName.BackColor = SystemColors.ControlLightLight;
        }

        internal Settings.JiraIssueType GetJiraIssue()
        {
            Settings.JiraIssueType jiraIssue = new()
            {
                IssueType = groupBoxExIssueName.Value
            };

            if( string.IsNullOrEmpty(jiraIssue.IssueType) ) throw new IssueDefinitionException($"A blank {this.groupBoxExIssueName.CaptionText} was enncountered.  Cannot proceed.");

            foreach( RadioButton rb in this.flowLayoutPanelRadios.Controls )
            {
                if( rb.Checked )
                {
                    if( rb.Tag == null ) throw new NullReferenceException(nameof(rb.Tag));
                    jiraIssue.Order = (int)rb.Tag;
                    break;
                }
            }

            if( pictureBoxImagePath.Tag == null ) throw new IssueDefinitionException(nameof(pictureBoxImagePath.Tag));
            jiraIssue.ImagePath = pictureBoxImagePath.Tag.ToString();

            if( string.IsNullOrEmpty(jiraIssue.ImagePath) ) throw new IssueDefinitionException($"A blank ImagePath was enncountered.  Cannot proceed.");
            if( jiraIssue.ImagePath.Contains($"unknown") ) throw new IssueDefinitionException($"The Image was not updated.  Cannot proceed.");
            //if( jiraIssue.ImagePath.Equals($"{"./Data/Resources"}/unknown.png") ) throw new IssueDefinitionException($"The Image was not updated.  Cannot proceed.");

            jiraIssue.ForeColor = pictureBoxForeColor.BackColor.Name;

            /*
            string json = JsonSerializer.Serialize(jiraIssue);
            if( json.Equals(this.json) )
            {
            }
            else
            {
            }
            */
            return jiraIssue;
        }

        private void UserControlX_Click(object? sender, EventArgs e)
        {
            OnControlSelected();
        }

        private void ChildControl_Click(object? sender, EventArgs e)
        {
            OnControlSelected(); // Propagate click from child controls
        }

        protected virtual void OnControlSelected()
        {
            ControlSelected?.Invoke(this, EventArgs.Empty);
        }

        // Optional: Add properties for visual selection feedback
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if( _isSelected != value )
                {
                    _isSelected = value;
                    // You'll need to implement actual visual feedback here
                    // e.g., changing border, background color, etc.
                    if( _isSelected )
                    {
                        this.BackColor = Color.LightBlue; // Example selection color
                        this.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else
                    {
                        this.BackColor = SystemColors.Control; // Default background
                        this.BorderStyle = BorderStyle.None;
                    }
                    this.Invalidate(); // Redraw the control
                }
            }
        }

    }
}
