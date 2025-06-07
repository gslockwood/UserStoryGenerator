using UserStoryGenerator.Model;

namespace UserStoryGenerator.View
{
    public partial class SettingsForm : Form
    {
        public delegate void NewSettingsEventHandler(Settings settings, bool save);
        public event NewSettingsEventHandler? NewSettings;

        Settings? settings = null;
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
            //
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CenterToParent();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            settings ??= new Settings();

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

            NewSettings?.Invoke(settings, true);

            Dispose();

        }

        private void ButtonUse_Click(object sender, EventArgs e)
        {
            if( settings == null ) return;

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

            NewSettings?.Invoke(settings, false);

            Dispose();

        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
