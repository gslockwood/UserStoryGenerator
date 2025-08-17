using System.Diagnostics;
using UserStoryGenerator.Model;
using UserStoryGenerator.Utilities;
using static UserStoryGenerator.Model.Settings;
using static UserStoryGenerator.View.TriStateTreeView;

namespace UserStoryGenerator.View
{
    public partial class MainForm : Form
    {
        private readonly Model.Model model;
        private SettingsForm? form;


        public MainForm()
        {
            try
            {
                InitializeComponent();

                model = new();

            }
            catch( System.Text.Json.JsonException ex )
            {
                MessageBox.Show($"Setting (json) file had for following problem: {ex.Message}\n\n", "Critical Error: Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
            //
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if( model.Settings == null ) throw new NullReferenceException(nameof(model.Settings));
            if( model.Settings.JiraIssueTypes == null ) throw new NullReferenceException(nameof(model.Settings.JiraIssueTypes));

            treeView.JiraIssueTypes = model.Settings.JiraIssueTypes;

            foreach( var jiraIssueType in model.Settings.JiraIssueTypes )
            {
                if( jiraIssueType.Value == null || jiraIssueType.Value.IssueType == null || jiraIssueType.Value.ImagePath == null ) continue;
                Image? image = Utilities.ImageLoader.GetImageFromFilePath(jiraIssueType.Value.ImagePath);
                if( image != null )
                {
                    treeView.ImageList.Images.Add(jiraIssueType.Value.IssueType, image);
                    flowLayoutPanelIssueImages.AddImage(jiraIssueType.Value.IssueType, image);

                    flowLayoutPanelSelected.AddResizableGroupBox(jiraIssueType.Value);
                    flowLayoutPanelExTotals.AddResizableGroupBox(jiraIssueType.Value);
                }
                else
                {
                    image = Properties.Resources.Light;
                    treeView.ImageList.Images.Add(jiraIssueType.Value.IssueType, image);
                    flowLayoutPanelIssueImages.AddImage(jiraIssueType.Value.IssueType, image);

                    flowLayoutPanelSelected.AddResizableGroupBox(jiraIssueType.Value);
                    flowLayoutPanelExTotals.AddResizableGroupBox(jiraIssueType.Value);
                }
            }

            Image? imageCircle = Properties.Resources.circle;
            if( imageCircle != null )
                treeView.ImageList.Images.Add("Circle", imageCircle);


            Utilities.IssueUtilities.SetImageList(treeView.ImageList);
            Utilities.IssueUtilities.SetJiraIssueTypes(treeView.JiraIssueTypes);

            treeView.Checked += (issueCollector) =>
            {
                bool checkedNodes = !( issueCollector.Total == 0 );
                SetMenuItem(checkedNodes);

                UpdateSelectedIssues(issueCollector);
            };

            model.UserStoryGeneratorCompleted += Model_UserStoryGeneratorCompleted;
            model.IssueGeneratorCompleted += Model_IssueGeneratorCompleted;
            model.CompletedInError += (error) =>
            {
                string errorMesage = "UNK";

                if( error is IRequestFailed reqFailed )
                {
                    if( reqFailed.error != null && reqFailed.error.message != null )
                        errorMesage = reqFailed.error.message;
                }

                MessageBox.Show(errorMesage, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            };

            ResetUI();

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if( model.Settings == null ) throw new NullReferenceException(nameof(model.Settings));
            if( model.Settings.Key == null ) throw new NullReferenceException(nameof(model.Settings.Key));

            form = new SettingsForm(model.Settings, model.SettingsFileName);


            comboBoxExStoryMin.DataSource = new int[] { 10, 20, 30, 40, 50, 75 };
            comboBoxExStoryMin.SelectedIndex = 2;

            comboBoxJiraProjects.DataSource = model.Settings.Projects;
            this.comboBoxJiraProjects.SelectedIndex = 0;


            // for testing missing info situations
            //model.Settings.Key = Model.Model.DEFAULTKEY;
            //model.Settings.Key = "esdf5gf9s4Ar-OsfgsgsfgCgclvaA-LitgsumL0";

            if( model.Settings.Key.Equals(Model.Model.DEFAULTKEY, StringComparison.CurrentCultureIgnoreCase) )
            {
                MessageBox.Show($"Setting (json) file is needs to be initialized.\n\nA form will appear next, fill it out in order to use this app.\n\nYour settings will persist and you can change them later by using the \"Preferences\" menu item.\n\n", "Set up: Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PreferencesToolStripMenuItem_Click(model.Settings, new EventArgs());
            }


#if DEBUG
            TreeSerialization.IssueResults? issueResults = model.CreateUserStories();
            if( issueResults == null )
                throw new NullReferenceException(nameof(issueResults));

            groupBoxExPRD.Value = issueResults.ProductDescription;
            groupBoxExProductFeature.Value = issueResults.ProductOrFeature;
            epicSelector.EpicNameOrKey = issueResults.EpicNameOrKey;

            if( treeView.Nodes.Count > 0 )
                treeView.Nodes[0].Text = issueResults.EpicNameOrKey;

            checkBoxAddQATests.Checked = true;
            checkBoxAddSubTasks.Checked = true;
            comboBoxExStoryMin.SelectedIndex = 0;

            //PreferencesToolStripMenuItem_Click(this, new EventArgs());
            //
#endif


        }

        private void Model_UserStoryGeneratorCompleted(IssueGeneratorBaseArgs args)
        {
            if( this.InvokeRequired )
                this.Invoke(new System.Windows.Forms.MethodInvoker(() => { Model_UserStoryGeneratorCompleted(args); return; }));

            else
            {
                buttonConvert.Enabled = true;
                stopwatchClockConvertRun.Stop();
                TimeSpan duration = stopwatchClockConvertRun.ElapsedTime;
                groupBoxExDuration.Value = $"{duration.Minutes}:{duration.Seconds}.{duration.Milliseconds}";

                buttonProcessStories.Enabled = true;

                if( args.ErrorCode != 0 )
                {
                    MessageBox.Show(args.ErrorMsg, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
                else
                {
                    if( args.Issues != null )
                    {
                        treeView.Nodes.Clear();
                        PopulateUI(args.Issues);
                    }
                    else
                    {
                        MessageBox.Show("The Issue Generator Completed with no value.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //
            }
            //
        }

        private void Model_IssueGeneratorCompleted(IssueGeneratorBaseArgsEx args)
        {
            if( this.InvokeRequired )
                this.Invoke(new System.Windows.Forms.MethodInvoker(() => { Model_IssueGeneratorCompleted(args); return; }));

            else
            {
                try
                {
                    if( args.ErrorCode != 0 )
                    {
                        MessageBox.Show(args.ErrorMsg, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        TreeNode[] userStoryNodeArray = treeView.Nodes[0].Nodes.Find(args.UserStoryKey.ToString(), false);
                        if( userStoryNodeArray.Length > 0 )
                        {
                            TreeNode userStoryNode = userStoryNodeArray.First();
                            string parentText = userStoryNode.Text;

                            Color temp = userStoryNode.BackColor;
                            userStoryNode.BackColor = Color.Red;
                            userStoryNode.ForeColor = temp;

                            labelStatus.Text = $"Failed to build LinkedIssues for Story: {parentText} {args.Counter}";
                            labelStatus.ForeColor = Color.Red;

                        }

                        return;
                        //
                    }


                    if( args.Result == null ) throw new NullReferenceException(nameof(args.Result));

                    buttonProcessStories.Text = args.Counter.ToString();

                    if( args.Issues == null || args.Issues.Count == 0 )
                    {
                        TreeNode[] userStoryNodeArray = treeView.Nodes[0].Nodes.Find(args.UserStoryKey.ToString(), false);
                        if( userStoryNodeArray.Length > 0 )
                        {
                            TreeNode userStoryNode = userStoryNodeArray.First();
                            string parentText = userStoryNode.Text;

                            Color temp = userStoryNode.BackColor;
                            userStoryNode.BackColor = Color.Red;
                            userStoryNode.ForeColor = temp;

                            //StringTrimming stringTrimming = new()
                            labelStatus.Text = $"Failed to build LinkedIssues for Story: {parentText} {args.Counter}";
                            labelStatus.ForeColor = Color.Red;
                        }

                    }
                    else
                    {
                        try
                        {
                            List<Issue> Issues = args.Issues;

                            //Logger.Info("Model_IssueGeneratorCompleted " + data.Issues.Count);

                            TreeNode[] found = treeView.Nodes[0].Nodes.Find(args.UserStoryKey.ToString(), false);
                            if( found.Length > 0 )
                            {
                                TreeNode userStoryNode = found.First();

                                TreeNode[] linkedIssuesNodeArray = userStoryNode.Nodes.Find(TreeNodeExLinkedIssues.NodeName, false);
                                if( linkedIssuesNodeArray.Length > 0 )
                                {
                                    TriStateTreeView.RemovePreviousCreatedNodesIfTagged(linkedIssuesNodeArray.First().Nodes);

                                    RecursiveWorker recursiveWorker = new(model.Settings.JiraIssueTypes, treeView.ImageList.Images.IndexOfKey("Circle"));
                                    //RecursiveWorker recursiveWorker = new(model, treeView);
                                    recursiveWorker.CreateNodes(Issues, linkedIssuesNodeArray.First(), true);

                                    UpdateCountersUI(Issues);

                                    labelStatus.Text = $"Processed: {userStoryNode.Text}";
                                    //Logger.Info($"Processed: {node.Text}");

                                    //.Collapse();
                                    linkedIssuesNodeArray.First().Expand();
                                    treeView.TopNode = userStoryNode;
                                    //
                                }

                            }

                        }
                        catch( System.Text.Json.JsonException ex )
                        {
                            MessageBox.Show(ex.Message, "Critical JSON Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if( args.Result != null && args.Result.Answer != null )
                                Logger.Info(args.Result.Answer);

                        }
                        catch( Exception ex )
                        {
                            MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    //
                }

                finally
                {
                    //Logger.Info($"\tProcessed: {args.Counter}");

                    if( args.Counter == 0 )
                    {
                        stopwatchClockConvertRun.Stop();
                        TimeSpan duration = stopwatchClockConvertRun.ElapsedTime;
                        groupBoxExDuration.Value = $"{duration.Minutes}:{duration.Seconds}.{duration.Milliseconds}";

                        if( buttonProcessStories.Tag != null )
                            buttonProcessStories.Text = buttonProcessStories.Tag.ToString();

                    }
                }
            }
        }

        private async void Convert_Click(object? sender, EventArgs e)//async
        {
            ResetUI();
            buttonConvert.Enabled = false;

            treeView.Nodes.Clear();
            buttonProcessStories.Enabled = false;

            string? project = comboBoxJiraProjects.CurrentSelectedItem.ToString();
            if( project == null ) throw new NullReferenceException(nameof(project));

            //string testProdDesc = "Implement a mobile-first, fully responsive e-commerce platform for our online store, enabling users to browse products, add them to a shopping cart, and complete secure online purchases. The platform should offer a streamlined checkout process, diverse payment options, and robust order tracking capabilities. We need to ensure that the platform is user-friendly, accessible, and highly performant, with a focus on a seamless user experience across all devices.  Please include this task: Create a Database table called \"Customer\".   Add the \"Name\" and \"Phone number\" and other relevant columns to the \"Customer\" table.";

            string? testProdDesc = groupBoxExPRD.Value;
            if( string.IsNullOrEmpty(testProdDesc) ) throw new NullReferenceException(nameof(testProdDesc));

            if( groupBoxExProductFeature.Value == null ) throw new NullReferenceException(nameof(groupBoxExProductFeature));

            stopwatchClockConvertRun.Start();

            if( comboBoxExStoryMin.SelectedItem == null ) throw new NullReferenceException(nameof(comboBoxExStoryMin.SelectedItem));


            try
            {
                Customization customizations = new()
                {
                    AddQATests = checkBoxAddQATests.Checked,
                    AddSubTasks = checkBoxAddSubTasks.Checked,
                    AddDescriptions = checkBoxAddDescriptions.Checked,
                    AddEstimates = checkBoxEstimates.Checked,

                };

                await model.ProduceUserStories(project, groupBoxExProductFeature.Value, testProdDesc, customizations, (int)comboBoxExStoryMin.SelectedItem);
                //await model.ProduceUserStories(project, groupBoxExProductFeature.Value, testProdDesc, this.checkBoxAddQATests.Checked, checkBoxAddSubTasks.Checked, checkBox1AddDescriptions.Checked, (int)comboBoxExStoryMin.SelectedItem);

            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonConvert.Enabled = true;
            }
            //
        }

        private void ResetUI()
        {
            tableLayoutPanelResultsTotal.Reset();
            flowLayoutPanelSelected.Reset();

            labelStatus.Text = null;
            labelStatus.ForeColor = SystemColors.ControlText;

        }

        private void PopulateUI(List<Issue> issues)
        {
            if( issues == null ) return;

            try
            {
                treeView.Enabled = false;

                TreeNode? root = new(this.epicSelector.Value)
                {
                    ImageIndex = Utilities.IssueUtilities.GetImageIndex(JiraIssueType.Epic)
                };
                treeView.Nodes.Add(root);

                RecursiveWorker recursiveWorker = new(model.Settings.JiraIssueTypes, treeView.ImageList.Images.IndexOfKey("Circle"));
                recursiveWorker.CreateNodes(issues, root);

                //root.Checked = true;            //treeView.SelectAllNodes(false);

                treeView.Nodes[0].Expand();
                if( treeView.Nodes.Count > 0 )
                    treeView.TopNode = treeView.Nodes[0];
                treeView.Nodes[0].Checked = false;

                UpdateCountersUI(issues);
                SetMenuItem(false);
            }
            finally
            {
                treeView.Enabled = true;
            }
            //
        }

        private int UpdateCountersUI(List<Issue> issues)
        {
            if( issues == null ) return 0;
            return flowLayoutPanelExTotals.UpdateCountersByIssues(issues);
            //
        }
        private int UpdateSelectedIssues(IssueCollector issueCollector)
        {
            return flowLayoutPanelSelected.SetValues(issueCollector);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TextControls_TextChanged(object? sender, EventArgs e)
        {
            buttonConvert.Enabled = !( groupBoxExProductFeature.TextLength == 0 || groupBoxExPRD.TextLength == 0 );
        }

        private void SetMenuItem(bool enabled)
        {
            saveStoriesAsJsonToolStripMenuItem.Enabled = enabled;
            saveStoriesAsCSVToolStripMenuItem.Enabled = enabled;
            getUserStoryListToolStripMenuItem.Enabled = enabled;

            // may remove buttonSave from code
            buttonSave.Enabled = enabled;

        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( form != null )
            {
                string? selectedValue = comboBoxJiraProjects.CurrentSelectedItem.ToString();

                form.ResetSettingProjects += () =>
                {
                    comboBoxJiraProjects.DataSource = this.model.Settings.Projects;

                    int index = comboBoxJiraProjects.FindString(selectedValue);
                    if( index == -1 )
                        index = 0;
                    this.comboBoxJiraProjects.SelectedIndex = index;

                };

                form.Show();
                //
            }
        }

        private async void ButtonProcessStories_ClickAsync(object sender, EventArgs e)//async
        {
            ResetUI();

            //TriStateTreeView.ResetTreeViewNodeColors(treeView.Nodes);

            stopwatchClockConvertRun.Start();

            List<StoryPackage> list = [];
            foreach( TreeNode node in treeView.Nodes[0].Nodes )
            {
                if( !node.Checked ) continue;

                if( node.ForeColor != SystemColors.WindowText )
                {
                    // reset the colors
                    node.BackColor = SystemColors.Window;
                    node.ForeColor = SystemColors.WindowText;
                }

                string userStoryText = node.Text;
                if( string.IsNullOrEmpty(userStoryText) ) continue;

                if( node is not TreeNodeEx treeNodeEx ) continue;

                list.Add(new(treeNodeEx));

            }

            if( list.Count > 0 )
            {
                if( groupBoxExProductFeature.Value == null ) throw new NullReferenceException(nameof(groupBoxExProductFeature));
                if( comboBoxExStoryMin.SelectedItem == null ) throw new NullReferenceException(nameof(comboBoxExStoryMin.SelectedItem));

                //await model.ProcessStoryList(groupBoxExProductFeature.Value, checkBoxAddQATests.Checked, checkBoxAddSubTasks.Checked, checkBox1AddDescriptions.Checked, (int)comboBoxExStoryMin.SelectedItem, list);
                Customization customizations = new()
                {
                    AddQATests = checkBoxAddQATests.Checked,
                    AddSubTasks = checkBoxAddSubTasks.Checked,
                    AddDescriptions = checkBoxAddDescriptions.Checked,
                    AddEstimates = checkBoxEstimates.Checked,
                };
                await model.ProcessStoryList(groupBoxExProductFeature.Value, customizations, (int)comboBoxExStoryMin.SelectedItem, list);
                buttonProcessStories.Text = "Running";
            }
            else
                MessageBox.Show("None of the user stories were checked.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AboutBox();
            form.ShowDialog();
        }

        private void OpenJsonToolStripMenuItem_Click(object sender, EventArgs e)
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
                try
                {
                    labelStatus.Text = $"Loading {dialog.FileName}";
                    dialog.RestoreDirectory = true;
                    string json = File.ReadAllText(dialog.FileName) ?? throw new Exception($"{dialog.FileName} is blank.");
                    TreeSerialization.IssueResults? issueResults = model.CreateUserStories(json) ?? throw new NullReferenceException($"Reading the json file {dialog.FileName} gave null results.");

                    labelStatus.Text = null;
                    groupBoxExPRD.Value = issueResults.ProductDescription;
                    groupBoxExProductFeature.Value = issueResults.ProductOrFeature;
                    epicSelector.EpicNameOrKey = issueResults.EpicNameOrKey;
                    string? jiraProject = issueResults.JiraProject;
                    int index = comboBoxJiraProjects.FindString(jiraProject);
                    if( index == -1 )
                        index = 0;
                    this.comboBoxJiraProjects.SelectedIndex = index;
                    //
                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void OpenPRDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                InitialDirectory = "Data",
                Filter = "Product Description Files (*.PRD)|*.prd|(*.TXT)|*.txt|All Files (*.*)|*.*",
                FilterIndex = 1,
                //RestoreDirectory = true
            };
            if( dialog.ShowDialog() == DialogResult.OK )
            {
                dialog.RestoreDirectory = true;
                try
                {
                    groupBoxExPRD.Value = File.ReadAllText(dialog.FileName);

                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void OpenCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                InitialDirectory = "Data",
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                FilterIndex = 1,
                //RestoreDirectory = true
            };
            if( dialog.ShowDialog() == DialogResult.OK )
            {
                dialog.RestoreDirectory = true;
                Process.Start(new ProcessStartInfo(dialog.FileName) { UseShellExecute = true });
            }
        }

        private void SaveStoriesAsJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new()
            {
                InitialDirectory = "Data",
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*",
                FilterIndex = 1, // Sets the default selected filter to "Text Files"
                //RestoreDirectory = true // Restores the directory to the previously selected one
            };

            if( dialog.ShowDialog() == DialogResult.OK )
            {
                dialog.RestoreDirectory = true;
                // Get the selected file path
                string filePath = dialog.FileName;
                try
                {
                    SaveData();

                    if( groupBoxExProductFeature.Value == null ) throw new NullReferenceException(nameof(groupBoxExProductFeature.Value));
                    if( epicSelector.Value == null ) throw new NullReferenceException(nameof(epicSelector.Value));

                    bool success = model.SaveUserStoryResultsAsJson(filePath, comboBoxJiraProjects.CurrentSelectedItem.ToString(), groupBoxExPRD.Value, groupBoxExProductFeature.Value, epicSelector.Value);
                    if( !success ) MessageBox.Show("Problem with SaveUserStoryResults.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
        private void SaveData()
        {
            List<string> storySummaries = treeView.GetStorySummaries();

            if( storySummaries.Count == 0 )
                throw new Exception("No selections were made");

            List<TreeNode> checkedHierarchy = treeView.GetCheckedNodesHierarchy(true);

            string epicText = string.Empty;

            //var adfa = checkedHierarchy[0].Nodes[1].Nodes[0] is TreeNodeExTask;

            if( epicSelector.Value != null )
                epicText = epicSelector.Value;

            model.SaveDataToFile(epicText, storySummaries, checkedHierarchy);
            //
        }

        private void SaveStoriesAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( this.flowLayoutPanelSelected.Total > 250 )
            {
                DialogResult dialogResult = MessageBox.Show(
                    "You need to be an administrator to create more than 250 issues per CSV file.\n\nIf you don't have that permission, when importing the CSV, you will not get a full import.\n\nDo you want to proceed?",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                bool proceed = ( dialogResult == DialogResult.Yes );
                if( !proceed ) return;
                //
            }

            SaveFileDialog dialog = new()
            {
                InitialDirectory = "Data",
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                FilterIndex = 1, // Sets the default selected filter to "Text Files"
                //RestoreDirectory = true // Restores the directory to the previously selected one
            };

            if( dialog.ShowDialog() == DialogResult.OK )
            {
                dialog.RestoreDirectory = true;
                // Get the selected file path
                string filePath = dialog.FileName;
                try
                {
                    SaveData();

                    string epicText = string.Empty;
                    if( epicSelector.Value != null )
                        epicText = epicSelector.Value;

                    bool success = model.SaveUserStoryResultsToCSV(filePath, epicText);
                    if( !success ) MessageBox.Show("Problem with SaveUserStoryResults.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }

        private void GetUserStoryListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> storySummaries = treeView.GetStorySummaries();
                if( storySummaries != null )
                {
                    StoryListForm form = new(storySummaries);
                    form.ShowDialog();
                }

            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            if( e.Data == null ) return;

            if( e.Data.GetDataPresent(DataFormats.Text) )
            {
                string? data = e.Data.GetData(DataFormats.Text) as string;
                if( !string.IsNullOrEmpty(data) )
                {
                    data = data.Replace("\"", "");

                    string[] summaries = data.Split(Environment.NewLine);

                    int x = this.Left + ( this.Width / 2 ) - 200;
                    int y = this.Top + ( this.Height / 2 ) - 100;

                    string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the Jira Project name (eg. 'PROJ')",
                       "Information",
                       "PROJ",
                       x,
                       y);

                    if( string.IsNullOrEmpty(input) )
                        return;

                    List<Issue> issues = [];
                    foreach( string summary in summaries )
                    {
                        string summary0 = summary.Trim();
                        summary0 = summary0.TrimEnd(',');

                        if( summary0.Length < 3 ) continue;

                        Issue issue = new()
                        {
                            IssueType = Model.Settings.JiraIssueType.Story,
                            Summary = summary0,
                            Product = input
                        };
                        issues.Add(issue);
                    }


                    if( MessageBox.Show("Do you want to first clear the Tree?", "Instructions", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes )
                        treeView.Nodes.Clear();

                    TreeNode? root = new(this.epicSelector.Value);
                    treeView.Nodes.Add(root);

                    RecursiveWorker recursiveWorker = new(model.Settings.JiraIssueTypes, treeView.ImageList.Images.IndexOfKey("Circle"));
                    recursiveWorker.CreateNodes(issues, root);
                    treeView.Nodes[0].Expand();
                    //
                }
            }

            else if( e.Data.GetDataPresent(TriStateTreeView.TREENODEDATA) )
            {
                try
                {
                    // Retrieve the serialized data string
                    string? jsonNodeData = e.Data.GetData(TriStateTreeView.TREENODEDATA) as string;
                    if( !string.IsNullOrEmpty(jsonNodeData) )
                    {
                        TreeNodeEx? treeNodeEx = System.Text.Json.JsonSerializer.Deserialize<TreeNodeEx>(jsonNodeData);

                        /*
                        if( treeNodeEx != null && treeNodeEx.Nodes[1].Nodes[0] is TreeNodeExTask task )
                        {
                            Logger.Info(task.Component.Name);
                            Logger.Info(task.Component.SubComponent);
                        }
                        */

                        if( treeNodeEx != null )
                        {
                            Point clientPoint = treeView.PointToClient(new Point(e.X, e.Y));
                            TreeNode droppedUponNode = treeView.GetNodeAt(clientPoint);

                            TreeNodeEx droppedUponNodeEx = (TreeNodeEx)droppedUponNode;

                            TreeNodeEx? nodeToUse = null;
                            if( droppedUponNode == treeView.Nodes[0] )
                            {
                                // use the root (the epic) node to drop upon
                                nodeToUse = (TreeNodeEx?)this.treeView.Nodes[0];
                            }
                            else
                            {
                                // check that it is a story then put them into the LinkedIssues collection
                                if( droppedUponNodeEx.Level == 1 )
                                {
                                    TreeNode[] linkedIssueNode = droppedUponNodeEx.Nodes.Find(TreeNodeExLinkedIssues.NodeName, false);
                                    if( linkedIssueNode.Length > 0 )
                                    {
                                        nodeToUse = (TreeNodeEx?)linkedIssueNode.First();
                                    }
                                }
                            }

                            if( nodeToUse != null )
                            {
                                nodeToUse.Nodes.Add(treeNodeEx);
                                droppedUponNodeEx.ExpandAll();
                            }

                        }

                    }
                }
                catch( Exception ex )
                {
                    Console.WriteLine($"DragDrop: Error deserializing custom node data: {ex.Message}");
                    MessageBox.Show($"Error processing dropped data: {ex.Message}", "Drag/Drop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Effect = DragDropEffects.None;
                    return;
                }
            }

        }

        private void GroupBoxExPRD_DragDrop(object sender, DragEventArgs e)
        {
            if( e.Data == null ) return;

            if( e != null && e.Data != null && e.Data.GetDataPresent(DataFormats.Text) )
            {
                var obj = e.Data.GetData(DataFormats.Text);
                if( obj != null )
                    groupBoxExPRD.Value = obj.ToString();
            }

            //// Check if the clipboard contains text
            //if( Clipboard.ContainsText() )
            //{
            //    // Get the text from the clipboard
            //    string text = Clipboard.GetText();

            //    groupBoxExPRD.Text = text;

            //    return;
            //}
        }

        private void GroupBoxExPRD_DragEnter(object sender, DragEventArgs e)
        {
            if( e == null || e.Data == null ) return;

            if( e.Data.GetDataPresent(DataFormats.Text) )
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

    }
}
