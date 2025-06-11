using System.Text.Json;
using UserStoryGenerator.Model;

namespace UserStoryGenerator.View
{
    public partial class SettingsForm : Form
    {
        public delegate void ResetSettingProjectsEventHandler();
        public event ResetSettingProjectsEventHandler? ResetSettingProjects;

        readonly JsonSerializerOptions options = new() { WriteIndented = true };

        private readonly Settings settings;
        private static string currentFileName = @$"Settings.json";
        private string CurrentFileName
        {
            get { return currentFileName; }
            set
            {
                currentFileName = value;
                FileInfo fileInfo = new(currentFileName);
                this.Text = $"{Tag}{fileInfo.Name}";
            }
        } //= 

        public SettingsForm(Settings settings)// = null
        {
            if( settings == null ) throw new NullReferenceException(nameof(settings));
            if( settings.Key == null ) throw new NullReferenceException(nameof(settings.Key));
            if( settings.UserStoryCoaching == null ) throw new NullReferenceException(nameof(settings.UserStoryCoaching));
            if( settings.AllIssueCoaching == null ) throw new NullReferenceException(nameof(settings.AllIssueCoaching));

            InitializeComponent();

            //settings = new Settings();  // testing

            this.settings = settings;

            //if( settings == null ) buttonUse.Enabled = false;

            SetSettingsToUI(this.settings);

            Tag = "Settings Form - ";
            CurrentFileName = @$"Settings.json";
            //
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CenterToParent();
        }

        private void ButtonUse_Click(object sender, EventArgs e)
        {
            FillSettingsFromUI();

            ResetSettingProjects?.Invoke();

            //Dispose();

        }

        private void FillSettingsFromUI()
        {
            settings.Key = groupBoxExGeminiKey.Value;

            if( settings.UserStoryCoaching == null ) throw new NullReferenceException(nameof(settings.UserStoryCoaching));

            settings.UserStoryCoaching.IssueInstructions = aiCoachingUserControlUserStories.IssueInstructions;
            settings.UserStoryCoaching.QATestInstructions = aiCoachingUserControlUserStories.QATestInstructions;
            settings.UserStoryCoaching.SubTaskInstructions = aiCoachingUserControlUserStories.SubTaskInstructions;

            if( settings.AllIssueCoaching == null ) throw new NullReferenceException(nameof(settings.AllIssueCoaching));

            settings.AllIssueCoaching.IssueInstructions = aiCoachingUserControlAllIssues.IssueInstructions;
            settings.AllIssueCoaching.QATestInstructions = aiCoachingUserControlAllIssues.QATestInstructions;
            settings.AllIssueCoaching.SubTaskInstructions = aiCoachingUserControlAllIssues.SubTaskInstructions;
            settings.Projects = this.listViewControl.GetItems();

        }

        private void SetSettingsToUI(Settings settings)
        {
            if( settings == null ) throw new NullReferenceException(nameof(settings));
            if( settings.Key == null ) throw new NullReferenceException(nameof(settings.Key));
            if( settings.UserStoryCoaching == null ) throw new NullReferenceException(nameof(settings.UserStoryCoaching));
            if( settings.AllIssueCoaching == null ) throw new NullReferenceException(nameof(settings.AllIssueCoaching));

            if( settings.Key.Equals(Model.Model.DEFAULTKEY, StringComparison.CurrentCultureIgnoreCase) )
            {
                groupBoxExGeminiKey.UseSystemPasswordChar = false;
                if( settings.Key != null )
                    groupBoxExGeminiKey.PlaceholderText = settings.Key;

                if( settings.Projects != null )
                    this.listViewControl.SetItems(settings.Projects);
            }
            else
            {
                groupBoxExGeminiKey.UseSystemPasswordChar = true;

                this.groupBoxExGeminiKey.Value = settings.Key;

                aiCoachingUserControlUserStories.IssueInstructions = settings.UserStoryCoaching.IssueInstructions;
                aiCoachingUserControlUserStories.QATestInstructions = settings.UserStoryCoaching.QATestInstructions;
                aiCoachingUserControlUserStories.SubTaskInstructions = settings.UserStoryCoaching.SubTaskInstructions;

                aiCoachingUserControlAllIssues.IssueInstructions = settings.AllIssueCoaching.IssueInstructions;
                aiCoachingUserControlAllIssues.QATestInstructions = settings.AllIssueCoaching.QATestInstructions;
                aiCoachingUserControlAllIssues.SubTaskInstructions = settings.AllIssueCoaching.SubTaskInstructions;

                if( settings.Projects != null )
                    this.listViewControl.SetItems(settings.Projects);
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            //Dispose();
            Hide();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentSettings();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // currentFileName is correct
            SaveCurrentSettings();
        }

        private void SaveCurrentSettings()
        {
            try
            {
                FillSettingsFromUI();

                ResetSettingProjects?.Invoke();

                File.WriteAllText(CurrentFileName, JsonSerializer.Serialize(settings, options));

            }
            catch( System.UnauthorizedAccessException ex )
            {
                MessageBox.Show(ex.Message, "UnauthorizedAccess Exception Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message, "Critical Exception Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                FilterIndex = 1, // Sets the default selected filter to "Text Files"
                RestoreDirectory = true // Restores the directory to the previously selected one
            };
            if( dialog.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    string json = File.ReadAllText(dialog.FileName) ?? throw new Exception($"{dialog.FileName} is blank.");
                    Settings? settings = JsonSerializer.Deserialize<Settings>(json);
                    if( settings == null ) return;

                    SetSettingsToUI(settings);
                    FillSettingsFromUI();

                    CurrentFileName = dialog.FileName;
                    //
                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //base.OnFormClosing(e);
            e.Cancel = true;
            Hide();
        }


        private void SaveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new()
            {
                FileName = CurrentFileName,
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                FilterIndex = 1, // Sets the default selected filter to "Text Files"
                RestoreDirectory = true // Restores the directory to the previously selected one
            };

            if( dialog.ShowDialog() == DialogResult.OK )
            {
                // Get the selected file path
                CurrentFileName = dialog.FileName;
                SaveCurrentSettings();
            }

        }
    }
}
