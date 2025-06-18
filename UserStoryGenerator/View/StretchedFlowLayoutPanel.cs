using UserStoryGenerator.Model;

namespace UserStoryGenerator.View
{
    public class StretchedFlowLayoutPanel : FlowLayoutPanel
    {
        public StretchedFlowLayoutPanel()
        {
            // Set the flow direction to top-down
            this.FlowDirection = FlowDirection.TopDown;
            // Crucial: Prevent wrapping to force controls into a single column
            this.WrapContents = false;
            // Enable auto-scrolling if content exceeds panel height
            this.AutoScroll = true;

            // Important: Use ControlAdded and Resize events to trigger width adjustments
            // The Layout event is also a good candidate for general layout adjustments
            this.ControlAdded += StretchedFlowLayoutPanel_ControlAdded;
            this.ControlRemoved += StretchedFlowLayoutPanel_ControlRemoved;
            this.Resize += StretchedFlowLayoutPanel_Resize;
        }

        private void StretchedFlowLayoutPanel_ControlAdded(object? sender, ControlEventArgs e)
        {
            if( e.Control == null ) return;
            // Set Anchor for the added control for general robustness, though
            // the explicit width adjustment will be the primary mechanism here.
            e.Control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            AdjustControlWidths();
        }

        private void StretchedFlowLayoutPanel_ControlRemoved(object? sender, ControlEventArgs e)
        {
            AdjustControlWidths();
        }

        private void StretchedFlowLayoutPanel_Resize(object? sender, EventArgs e)
        {
            AdjustControlWidths();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            // Also call AdjustControlWidths during the layout pass to ensure correctness
            AdjustControlWidths();
        }

        private void AdjustControlWidths()
        {
            // If the panel is not yet initialized or has no controls, do nothing
            if( this.Controls.Count == 0 || this.IsDisposed )
            {
                return;
            }

            // Calculate the available width for controls.
            // This is the client width minus internal padding and potentially the scrollbar width.
            int availableWidth = this.ClientSize.Width - this.Padding.Horizontal;

            // Account for vertical scrollbar if present.
            // It's important to check this *before* setting control widths.
            if( this.AutoScroll && this.VerticalScroll.Visible )
            {
                availableWidth -= SystemInformation.VerticalScrollBarWidth;
            }

            foreach( Control control in this.Controls )
            {
                // Ensure the control's width is set to the available width minus its own horizontal margins.
                int desiredWidth = availableWidth - control.Margin.Horizontal;

                // Ensure the width is not negative
                if( desiredWidth < 0 ) desiredWidth = 0;

                control.Width = desiredWidth;
            }

            // Invalidate and refresh to ensure layout updates
            this.Invalidate();
            this.Update();
        }

        internal void SetSettingsToUI(Dictionary<string, Settings.JiraIssue> jiraIssueTypes)
        {
            if( jiraIssueTypes != null )
            {
                List<Control> list = [];
                foreach( var item in jiraIssueTypes.Values )
                {
                    IssueTypeUserControl uc = new(item)
                    {
                        Height = 80,
                        //BorderStyle = BorderStyle.FixedSingle
                    };

                    list.Add(uc);
                }

                Control[] array = [.. list];
                SuspendLayout();
                Controls.Clear();
                Controls.AddRange(array);
                ResumeLayout();
                //
            }
        }

        internal Dictionary<string, Settings.JiraIssue>? GetJiraIssueTypes()
        {
            Dictionary<string, Settings.JiraIssue>? jiraIssueTypes = [];
            foreach( Control uc in Controls )
            {
                if( uc is IssueTypeUserControl issueTypeUserControl )
                {
                    Settings.JiraIssue jiraIssue = issueTypeUserControl.GetJiraIssue();
                    if( jiraIssue.IssueType == null ) throw new NullReferenceException(nameof(jiraIssue.IssueType));
                    jiraIssueTypes.Add(jiraIssue.IssueType, jiraIssue);
                    //
                }
            }

            return jiraIssueTypes;
            //
        }
    }

}
