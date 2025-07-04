using UserStoryGenerator.Model;
using static UserStoryGenerator.View.IssueTypeUserControl;

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

        internal void SetSettingsToUI(Dictionary<string, Settings.JiraIssueType> jiraIssueTypes)
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

        /////////////////////////////
        /////////////////////////////
        /////////////////////////////

        public class IsSelectedEventArgs(bool selected) : EventArgs
        {
            public bool Selected { get; } = selected;
        }
        protected virtual void OnSelectedControlChanged(bool selected)
        {
            SelectedControlChanged?.Invoke(this, new IsSelectedEventArgs(selected));
        }

        public IssueTypeUserControl? SelectedControl
        {
            get { return _selectedControl; }
            set
            {
                if( value == null )
                {
                    OnSelectedControlChanged(false);
                }
                else if( _selectedControl != value )
                {
                    // Deselect the old control
                    if( _selectedControl != null )
                    {
                        _selectedControl.IsSelected = false;
                    }

                    _selectedControl = value;

                    // Select the new control
                    if( _selectedControl != null )
                    {
                        _selectedControl.IsSelected = true;
                    }

                    OnSelectedControlChanged(true);
                }
                //else
                //{
                //    OnSelectedControlChanged(false);
                //}
            }
        }
        private IssueTypeUserControl? _selectedControl;
        public event EventHandler? SelectedControlChanged;
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            // If the added control is a UserControlX, subscribe to its selection event
            if( e.Control is IssueTypeUserControl userControlX )
            {
                userControlX.ControlSelected += UserControlX_ControlSelected;
                //// Also ensure it fills the width
                //userControlX.Dock = DockStyle.Top; // Or set Anchor and AutoSize
                //                                   // You might need to adjust the height based on your layout needs
            }

            // Adjust the layout to stretch controls
            //AdjustControlSizes();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            // Unsubscribe from the event when the control is removed
            if( e.Control is IssueTypeUserControl userControlX )
            {
                userControlX.ControlSelected -= UserControlX_ControlSelected;
                // If the removed control was the selected one, clear selection
                if( _selectedControl == userControlX )
                {
                    SelectedControl = null;
                }
            }

            //AdjustControlSizes();
        }
        private void UserControlX_ControlSelected(object? sender, EventArgs e)
        {
            if( sender is IssueTypeUserControl clickedControl )
            {
                this.SelectedControl = clickedControl; // Set the clicked control as selected
            }
        }

        /// //////////////////
        /// //////////////////
        /// //////////////////
        /// //////////////////


        internal Dictionary<string, Settings.JiraIssueType>? GetJiraIssueTypes()
        {
            Dictionary<string, Settings.JiraIssueType>? jiraIssueTypes = [];

            try
            {
                foreach( Control uc in Controls )
                {
                    if( uc is IssueTypeUserControl issueTypeUserControl )
                    {
                        Settings.JiraIssueType jiraIssue = issueTypeUserControl.GetJiraIssue();
                        if( jiraIssue.IssueType == null ) throw new IssueDefinitionException(nameof(jiraIssue.IssueType));
                        jiraIssueTypes.Add(jiraIssue.IssueType, jiraIssue);
                        //
                    }
                }

            }
            catch( Exception ex )
            {
                MessageBox.Show(
                    $"{ex.Message}\n\nNo issue definitions changed.",
                    "Critical Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }

            return jiraIssueTypes;
            //
        }

        internal void AddJiraIssueType()
        {
            Settings.JiraIssueType item = new()
            {
                IssueType = null,
                Order = 1,
                ForeColor = Color.GhostWhite.Name,
                ImagePath = null,// $"{"./Data/Resources"}/unknown.png"
            };

            IssueTypeUserControl uc = new(item)
            {
                Height = 80,
                //BorderStyle = BorderStyle.FixedSingle
            };

            Controls.Add(uc);
        }

        internal void RemoveSelectedItem()
        {
            if( _selectedControl != null )
            {
                Controls.Remove(_selectedControl);
                //SelectedControl = null;
            }
        }
    }

}
