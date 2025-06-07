namespace UserStoryGenerator.View
{
    public partial class AICoachingUserControl : UserControl
    {
        public string? IssueInstructions { set { this.groupBoxExGeneralAICoaching.Value = value; } get { return groupBoxExGeneralAICoaching.Value; } }
        public string? QATestInstructions { set { this.groupBoxExQATestAICoaching.Value = value; } get { return groupBoxExQATestAICoaching.Value; } }
        public string? SubTaskInstructions { set { this.groupBoxExSubTaskAICoaching.Value = value; } get { return groupBoxExSubTaskAICoaching.Value; } }

        public AICoachingUserControl()
        {
            InitializeComponent();
        }

    }
}
