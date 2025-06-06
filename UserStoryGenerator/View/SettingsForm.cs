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

            InitializeComponent();

            //settings = new Settings();  // testing

            this.settings = settings;

            //if( settings == null ) buttonUse.Enabled = false;

            /*


            if( settings.Key.Equals(Model.Model.DEFAULTKEY, StringComparison.CurrentCultureIgnoreCase) )
            {
                groupBoxExGeminiKey.UseSystemPasswordChar = false;
                if( settings.Key != null )
                    groupBoxExGeminiKey.PlaceholderText = settings.Key;
                if( settings.GeneralAITraining != null )
                    groupBoxExGeneralAICoaching.PlaceholderText = settings.GeneralAITraining;
                if( settings.QATestsAITraining != null )
                    groupBoxExQATestAICoaching.PlaceholderText = settings.QATestsAITraining;
                if( settings.SubTaskAITraining != null )
                    groupBoxExSubTaskAICoaching.PlaceholderText = settings.SubTaskAITraining;
            }
            else
            {
                groupBoxExGeminiKey.UseSystemPasswordChar = true;

                //settings = settings ?? new Settings();


                this.groupBoxExGeminiKey.Value = settings.Key;
                this.groupBoxExGeneralAICoaching.Value = settings.GeneralAITraining;
                groupBoxExQATestAICoaching.Value = settings.QATestsAITraining;
                groupBoxExSubTaskAICoaching.Value = settings.SubTaskAITraining;

                if( settings.Projects != null )
                    this.listViewControl.SetItems(settings.Projects);
            }
            */
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
            /*
            settings.Key = groupBoxExGeminiKey.Value;
            settings.GeneralAITraining = groupBoxExGeneralAICoaching.Value;
            settings.SubTaskAITraining = groupBoxExSubTaskAICoaching.Value;
            settings.QATestsAITraining = groupBoxExQATestAICoaching.Value;
            settings.Projects = this.listViewControl.GetItems();

            NewSettings?.Invoke(settings, true);

            Dispose();*/

        }

        private void ButtonUse_Click(object sender, EventArgs e)
        {
            /*
            if( settings == null ) return;

            settings.Key = groupBoxExGeminiKey.Value;
            settings.GeneralAITraining = groupBoxExGeneralAICoaching.Value;
            settings.SubTaskAITraining = groupBoxExSubTaskAICoaching.Value;
            settings.QATestsAITraining = groupBoxExQATestAICoaching.Value;
            settings.Projects = this.listViewControl.GetItems();

            NewSettings?.Invoke(settings, false);

            Dispose();
            */
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
