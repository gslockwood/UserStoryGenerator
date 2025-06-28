using System.Reflection;
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
        }

        public SettingsForm(Settings settings, string settingsFileName)// = null
        {
            if( settings == null ) throw new NullReferenceException(nameof(settings));
            if( settings.Key == null ) throw new NullReferenceException(nameof(settings.Key));
            if( settings.UserStoryCoaching == null ) throw new NullReferenceException(nameof(settings.UserStoryCoaching));
            if( settings.AllIssueCoaching == null ) throw new NullReferenceException(nameof(settings.AllIssueCoaching));

            InitializeComponent();

            //settings = new Settings();  // testing

            this.settings = settings;


            this.flowLayoutPanelIssues.SelectedControlChanged += (s, e) =>
            {
                if( e is StretchedFlowLayoutPanel.IsSelectedEventArgs args )
                    this.buttonDelete.Enabled = args.Selected;
            };


            Type constantsType = typeof(Mscc.GenerativeAI.Model);

            // Get all public static fields from the class
            FieldInfo[] fields = constantsType.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach( FieldInfo field in fields )
            {
                // Check if the field is a literal (i.e., a const) and is of type string
                if( field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string) )
                {
                    comboBoxMsccModels.Items.Add(field);
                    /*
                    // Get the constant string value
                    object? temp = field.GetRawConstantValue();
                    if( temp != null )
                    {
                        string constantValue = (string)temp;
                        constantValue = field.Name;
                    }
                    */
                }
            }


            //if( settings == null ) buttonUse.Enabled = false;

            SetSettingsToUI(this.settings);

            Tag = "Settings Form - ";
            CurrentFileName = settingsFileName;// @$"Settings.json";
            //
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CenterToParent();

            //tabControl.SelectedIndex = 2; testing of isstypes control

        }

        private void ButtonUse_Click(object sender, EventArgs e)
        {
            GetSettingsFromUI();

            ResetSettingProjects?.Invoke();

            //Dispose();
            Hide();

        }
        private void GetSettingsFromUI()
        {
            settings.Key = groupBoxExGeminiKey.Value;

            FieldInfo? selectedItem = this.comboBoxMsccModels.SelectedItem as FieldInfo;
            if( selectedItem != null )
                settings.GeminiModel = selectedItem.Name;

            if( settings.UserStoryCoaching == null ) throw new NullReferenceException(nameof(settings.UserStoryCoaching));

            settings.UserStoryCoaching.IssueInstructions = aiCoachingUserControlUserStories.IssueInstructions;
            settings.UserStoryCoaching.QATestInstructions = aiCoachingUserControlUserStories.QATestInstructions;
            settings.UserStoryCoaching.SubTaskInstructions = aiCoachingUserControlUserStories.SubTaskInstructions;

            if( settings.AllIssueCoaching == null ) throw new NullReferenceException(nameof(settings.AllIssueCoaching));

            settings.AllIssueCoaching.IssueInstructions = aiCoachingUserControlAllIssues.IssueInstructions;
            settings.AllIssueCoaching.QATestInstructions = aiCoachingUserControlAllIssues.QATestInstructions;
            settings.AllIssueCoaching.SubTaskInstructions = aiCoachingUserControlAllIssues.SubTaskInstructions;

            settings.Projects = this.listViewControl.GetItems();

            Dictionary<string, Settings.JiraIssueType>? jiraIssueTypes = flowLayoutPanelIssues.GetJiraIssueTypes();
            if( jiraIssueTypes != null )
                settings.JiraIssueTypes = jiraIssueTypes;

            settings.FundamentalInstructions = groupBoxExFundamentalInstructions.Value;
            //
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

            }

            int tempModel = comboBoxMsccModels.FindString(settings.GeminiModel);
            if( tempModel == -1 )
            {
                settings.GeminiModel = "Gemini20FlashLite001";//  "Gemini20Flash001";//Gemini25Flash
                tempModel = comboBoxMsccModels.FindString(settings.GeminiModel);
            }
            comboBoxMsccModels.SelectedIndex = tempModel;


            if( settings.Projects != null )
                this.listViewControl.SetItems(settings.Projects);

            if( settings.JiraIssueTypes != null )
                flowLayoutPanelIssues.SetSettingsToUI(settings.JiraIssueTypes);

            groupBoxExFundamentalInstructions.Value = settings.FundamentalInstructions;

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
            SaveCurrentSettings();
        }

        private void SaveCurrentSettings()
        {
            try
            {
                GetSettingsFromUI();

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
                InitialDirectory = "Data",
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                FilterIndex = 1, // Sets the default selected filter to "Text Files"
                //RestoreDirectory = true // Restores the directory to the previously selected one
            };
            if( dialog.ShowDialog() == DialogResult.OK )
            {
                dialog.RestoreDirectory = true;
                try
                {
                    string json = File.ReadAllText(dialog.FileName) ?? throw new Exception($"{dialog.FileName} is blank.");
                    Settings? settings = JsonSerializer.Deserialize<Settings>(json);
                    if( settings == null ) return;

                    SetSettingsToUI(settings);
                    GetSettingsFromUI();

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
                InitialDirectory = "Data",
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                FilterIndex = 1, // Sets the default selected filter to "Text Files"
                //RestoreDirectory = true // Restores the directory to the previously selected one
            };

            if( dialog.ShowDialog() == DialogResult.OK )
            {
                dialog.RestoreDirectory = true;
                // Get the selected file path
                CurrentFileName = dialog.FileName;
                SaveCurrentSettings();
            }

        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            flowLayoutPanelIssues.AddJiraIssueType();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            flowLayoutPanelIssues.RemoveSelectedItem();
        }
    }
}
