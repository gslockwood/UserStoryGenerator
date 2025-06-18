using System.Text.Json;
using UserStoryGenerator.Model;

namespace UserStoryGenerator.View
{
    public partial class IssueTypeUserControl : UserControl
    {
        private string json;

        //
        public IssueTypeUserControl(Model.Settings.JiraIssue? jiraIssue = null)
        {
            InitializeComponent();

            if( jiraIssue == null ) return;
            if( jiraIssue.ForeColor == null ) return;
            if( jiraIssue.ImagePath == null ) return;

            groupBoxExIssueName.Value = jiraIssue.IssueType;

            Image? image = Utilities.ImageLoader.GetImageFromFilePath(jiraIssue.ImagePath);
            pictureBoxImagePath.Image = image;
            pictureBoxImagePath.Tag = jiraIssue.ImagePath;

            pictureBoxForeColor.BackColor = Color.FromName(jiraIssue.ForeColor);

            if( jiraIssue.Order == 0 ) radioButtonSuper.Checked = true;
            else if( jiraIssue.Order == 2 ) radioButtonSubtask.Checked = true;
            else radioButtonStandard.Checked = true;

            json = JsonSerializer.Serialize(jiraIssue);
            //
        }

        private void PictureBoxImagePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog form = new()
            {
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
                RestoreDirectory = true, // Restores the directory to the previously selected one
            };

            if( form.ShowDialog() == DialogResult.OK )
            {
                Image? image = Utilities.ImageLoader.GetImageFromFilePath(form.FileName);
                pictureBoxImagePath.Image = image;
                pictureBoxImagePath.Tag = form.FileName;
            }
        }

        private void PictureBoxForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog form = new();
            if( form.ShowDialog() == DialogResult.OK )
            {
                pictureBoxForeColor.BackColor = form.Color;
            }
        }

        private void GroupBoxExIssueName_ValueChanged(object sender, EventArgs e)
        {
            if( groupBoxExIssueName.TextLength == 0 )
                groupBoxExIssueName.BackColor = Color.Red;
            else groupBoxExIssueName.BackColor = SystemColors.ControlLightLight;
        }

        internal Settings.JiraIssue GetJiraIssue()
        {
            Settings.JiraIssue jiraIssue = new();

            jiraIssue.IssueType = groupBoxExIssueName.Value;

            foreach( RadioButton rb in this.flowLayoutPanelRadios.Controls )
            {
                if( rb.Checked )
                {
                    if( rb.Tag == null ) throw new NullReferenceException(nameof(rb.Tag));
                    jiraIssue.Order = (int)rb.Tag;
                    break;
                }
            }

            if( pictureBoxImagePath.Tag == null ) throw new NullReferenceException(nameof(pictureBoxImagePath.Tag));
            jiraIssue.ImagePath = pictureBoxImagePath.Tag.ToString();

            jiraIssue.ForeColor = pictureBoxForeColor.BackColor.Name;

            string json = JsonSerializer.Serialize(jiraIssue);
            /*
            if( json.Equals(this.json) )
            {
            }
            else
            {
            }
            */
            return jiraIssue;
        }
    }
}
