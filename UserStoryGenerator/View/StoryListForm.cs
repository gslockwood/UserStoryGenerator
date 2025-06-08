namespace UserStoryGenerator.View
{
    public partial class StoryListForm : Form
    {
        public StoryListForm(List<string>? stories = null)
        {
            InitializeComponent();

            if( stories != null )
            {
                foreach( var story in stories )
                    richTextBox.Text = story;
            }
        }
    }
}
