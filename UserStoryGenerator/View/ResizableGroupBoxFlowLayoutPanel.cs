
using UserStoryGenerator.Model;
using UserStoryGenerator.Utilities;
using static UserStoryGenerator.Model.Settings;

namespace UserStoryGenerator.View
{
    public class ResizableGroupBoxFlowLayoutPanelEx : ResizableGroupBoxFlowLayoutPanel
    {
        private readonly GroupBoxEx groupBoxExTotalIssues;

        public ResizableGroupBoxFlowLayoutPanelEx()
        {
            Settings.JiraIssue jiraIssue = new()
            {
                IssueType = "Total",
                Order = -1
            };
            groupBoxExTotalIssues = AddResizableGroupBox(jiraIssue);
        }

        public int Total { get; internal set; }

        internal int SetValues(TriStateTreeView.IssueCollector issueCollector)
        {
            Total = 0;
            foreach( Control control in Controls )
            {
                if( control is GroupBoxEx group && group.Tag != null )
                {
                    group.Value = null;

                    JiraIssue jiraIssue = (JiraIssue)group.Tag;
                    if( jiraIssue != null && jiraIssue.Order > -1 && jiraIssue.IssueType != null )
                    {
                        int count = issueCollector.GetJiraIssuesCountByType(jiraIssue.IssueType);
                        Total += count;
                        group.Value = count.ToString();
                    }
                }
            }

            groupBoxExTotalIssues.Value = Total.ToString();

            if( Total > 250 )
                groupBoxExTotalIssues.ForeColor = Color.Red;
            else
                groupBoxExTotalIssues.ForeColor = SystemColors.ControlText;

            return Total;


        }

        internal int SetValuesByIssue(List<IssueData.Issue> issues)
        {
            //int total = 0;
            int count = 0;
            GroupBoxEx? groupBoxExSubTask = null;
            foreach( Control control in Controls )
            {
                if( control is GroupBoxEx group && group.Tag != null )
                {
                    JiraIssue jiraIssue = (JiraIssue)group.Tag;
                    if( jiraIssue != null && jiraIssue.Order > -1 && jiraIssue.IssueType != null )
                    {
                        if( jiraIssue.Order == 2 )
                        {
                            groupBoxExSubTask = group;
                            continue;
                        }
                        count = issues.FlattenStandardIssues().Where(issue => issue.IssueType != null && issue.IssueType == jiraIssue.IssueType).Count();
                        Total += count;
                        group.Value = count.ToString();
                    }
                }
            }

            if( groupBoxExSubTask != null )
            {
                count = IssueUtilities.GetAllSubTasks(issues).Count;
                groupBoxExSubTask.Value = count.ToString();
                Total += count;
            }

            groupBoxExTotalIssues.Value = Total.ToString();

            return Total;

        }
    }

    public class ResizableGroupBoxFlowLayoutPanel : FlowLayoutPanel, IReset
    {
        private readonly List<GroupBoxEx> _groupBoxes = new();

        public ResizableGroupBoxFlowLayoutPanel()
        {
            this.FlowDirection = FlowDirection.LeftToRight; // Arrange horizontally
            this.WrapContents = false; // Important: ensures controls stay on a single line
            this.AutoScroll = false;   // Crucial: Prevents automatic scrollbars

        }

        /// <summary>
        /// Adds a new GroupBoxEx with a specified title. Its width will be adjusted automatically.
        /// </summary>
        /// <param name="title">The title for the GroupBoxEx.</param>
        /// <returns>The newly created GroupBoxEx.</returns>
        public GroupBoxEx AddResizableGroupBox(Settings.JiraIssue jiraIssue)
        {
            GroupBoxEx newGroupBoxEx = new()
            {
                CaptionText = jiraIssue.IssueType,
                Tag = jiraIssue,
                Dock = DockStyle.Fill,
                Location = new Point(143, 3),
                Multiline = false,
                Name = "groupBoxEx" + jiraIssue.IssueType,
                PlaceholderText = "",
                ReadOnly = true,
                Size = new Size(114, 63),
                TabIndex = 0,
                TextAlign = HorizontalAlignment.Right,
                TextBoxForeColor = SystemColors.WindowText,
                UseSystemPasswordChar = false
            };

            _groupBoxes.Add(newGroupBoxEx);
            this.Controls.Add(newGroupBoxEx);
            this.PerformLayout(); // Trigger a layout recalculation to size the new control
            return newGroupBoxEx;
        }

        /// <summary>
        /// Removes a GroupBoxEx from the panel.
        /// </summary>
        /// <param name="groupBoxEx">The GroupBoxEx to remove.</param>
        public void RemoveResizableGroupBox(GroupBoxEx groupBoxEx)
        {
            if( _groupBoxes.Contains(groupBoxEx) )
            {
                _groupBoxes.Remove(groupBoxEx);
                this.Controls.Remove(groupBoxEx);
                this.PerformLayout(); // Trigger a layout recalculation after removal
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent); // Call the base implementation first

            if( _groupBoxes.Any() )
            {
                // Calculate the total horizontal margin consumed by all GroupBoxEx controls
                // This is the sum of Left+Right margins for each GroupBoxEx.
                int totalHorizontalMargin = _groupBoxes.Sum(gb => gb.Margin.Left + gb.Margin.Right);

                // Calculate the total available width inside the FlowLayoutPanel's display area,
                // after accounting for the margins.
                int availableWidth = this.DisplayRectangle.Width - totalHorizontalMargin;

                // Ensure availableWidth is not negative. This can happen if the panel is very small
                // and margins alone exceed its width.
                if( availableWidth < 0 ) availableWidth = 0;

                // Calculate the width for each individual GroupBoxEx.
                // If there are no GroupBoxEx controls, groupBoxWidth will remain 0.
                int groupBoxWidth = 0;
                if( _groupBoxes.Count > 0 )
                {
                    groupBoxWidth = availableWidth / _groupBoxes.Count;
                }

                // Iterate through each GroupBoxEx and set its dimensions
                foreach( GroupBoxEx gb in _groupBoxes )
                {
                    // Assign the calculated width
                    gb.Width = groupBoxWidth;

                    // Make the GroupBoxEx fill the entire height of the panel's display rectangle,
                    // accounting for its own vertical margins.
                    gb.Height = this.DisplayRectangle.Height - ( gb.Margin.Top + gb.Margin.Bottom );

                    // Ensure height is not negative
                    if( gb.Height < 0 ) gb.Height = 0;
                }
            }
        }

        public void Reset()
        {
            foreach( Control control in this.Controls )
            {
                if( control is IReset reset )
                    reset.Reset();
            }
        }

    }

}
