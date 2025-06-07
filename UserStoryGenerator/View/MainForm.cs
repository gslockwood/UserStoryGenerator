using System.Text.Json;
using UserStoryGenerator.Model;
using UserStoryGenerator.Utilities;
using static UserStoryGenerator.View.TriStateTreeView;

namespace UserStoryGenerator.View
{
    public partial class MainForm : Form
    {
        readonly JsonSerializerOptions options = new() { WriteIndented = true };

        private readonly Model.Model model;

        readonly string UserStories = "{\"Issues\":[{\"Summary\":\"As a user, I want to be able to browse products on the mobile-first e-commerce platform, so that I can find the items I'm interested in purchasing.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify product browsing functionality on various mobile devices and screen sizes\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Create test cases for product browsing functionality\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Execute test cases on multiple devices and browsers\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and report any defects\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document the product browsing functionality for the user manual\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Create a tutorial video on how to browse products on the platform\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want to be able to add products to a shopping cart, so that I can keep track of the items I want to purchase.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify adding products to the shopping cart functionality\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Create test cases for adding products to the cart\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Execute test cases to ensure correct product addition and quantity\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and report any issues\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document how to add products to the shopping cart\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Create help documentation for managing the shopping cart\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want to be able to complete secure online purchases, so that I can buy the products I want with confidence.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify the secure online purchase process\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Create test cases for secure payment processing\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Simulate various payment scenarios to ensure security\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and security measures\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document the secure purchase process and payment options\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Provide information on security measures taken to protect user data\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want a streamlined checkout process, so that I can complete my purchase quickly and easily.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify the checkout process flow and ease of use\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Create test cases for the checkout process\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Evaluate the checkout flow for user-friendliness\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and suggest improvements\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document the steps involved in the streamlined checkout process\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Provide tips for a smooth and quick checkout experience\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want diverse payment options, so that I can choose the payment method that is most convenient for me.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify the integration of various payment options\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Create test cases for each payment gateway\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Test payment processing with different payment methods\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and payment gateway performance\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document all available payment options and their usage\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Provide information on how to add or manage payment methods\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want robust order tracking capabilities, so that I can monitor the status of my order from placement to delivery.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify the order tracking functionality\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Create test cases for order status updates\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Track orders through various stages to ensure accuracy\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and tracking accuracy\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document how to track orders and interpret order status updates\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Provide information on estimated delivery times and possible delays\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want a user-friendly platform, so that I can easily navigate and use the e-commerce site.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Conduct usability testing on the e-commerce platform\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Plan and conduct usability testing sessions\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Gather user feedback on platform usability\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document findings and suggest improvements\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document key features and navigation tips for the platform\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Provide information on how to find and use various features\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want an accessible platform, so that I can use the e-commerce site regardless of my abilities.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify the platform's accessibility features\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test the platform with screen readers and other assistive technologies\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Ensure compliance with accessibility standards\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and accessibility improvements\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document accessibility features and how to use them\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Provide information on keyboard navigation and alternative text\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want a highly performant platform, so that I can have a smooth and responsive shopping experience.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Performance testing of the e-commerce platform\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Conduct load testing to measure platform performance\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Optimize platform performance for various devices and browsers\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and performance improvements\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document performance benchmarks and optimization strategies\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Provide tips for users to improve their experience\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want a seamless user experience across all devices, so that I can shop on any device without issues.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify responsive design and cross-device compatibility\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Test the platform on different devices (desktops, tablets, phones)\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Ensure consistent design and functionality across devices\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and responsive design improvements\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document device compatibility and responsive design features\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Provide tips for users to optimize their experience on each device\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a database administrator, I want to create a 'Customer' table with 'Name' and 'Phone number' columns, so that I can store customer information.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify the creation of the 'Customer' table\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Create test script to verify table schema\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Execute test script and validate column types and constraints\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and report any issues\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document the schema of the 'Customer' table\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Provide information on how to access and manage the 'Customer' table\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a system administrator, I want to ensure the e-commerce platform is mobile-first in its design, so that it prioritizes the experience for mobile users.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify that the platform is mobile-first\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Check CSS media queries and responsiveness\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Check website on different mobile resolutions\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and report deviations\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document how the platform follows mobile-first principles\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Highlight mobile-specific features and optimizations\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want to be able to search for products using keywords, so that I can quickly find what I'm looking for.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify product search functionality\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Check search accuracy and speed\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Test with different keywords and categories\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and report any defects\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document the product search functionality for the user manual\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want to be able to filter and sort products based on various attributes (price, rating, etc.), so that I can narrow down my search.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify product filtering and sorting functionality\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Create test cases for product filtering and sorting\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Execute test cases on multiple filters and sorting options\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results and report any defects\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document how to use product filtering and sorting\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a registered user, I want to be able to save my shipping addresses, so that I don't have to re-enter them every time I make a purchase.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify saving shipping addresses\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Check functionality of saving multiple addresses\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Check editing and deleting saved addresses\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document saving shipping address functionality\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want to be able to view product details (images, descriptions, reviews), so that I can make informed purchasing decisions.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify product details display\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Check image loading and quality\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Check description accuracy\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Check review display\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document viewing product details functionality\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want to be able to create an account, so that I can save my preferences and order history.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify account creation process\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Check email validation\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Check password security\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document the account creation process\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a registered user, I want to be able to log in to my account, so that I can access my saved information and order history.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify login functionality\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Check valid login\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Check invalid login attempts\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document the login process\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a registered user, I want to be able to reset my password if I forget it, so that I can regain access to my account.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify password reset functionality\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Check password reset via email\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Check security questions\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document the password reset process\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]},{\"Summary\":\"As a user, I want to receive confirmation emails for my orders, so that I have a record of my purchase.\",\"IssueType\":\"Story\",\"Product\":\"RED\",\"LinkedIssues\":[{\"Summary\":\"Integrated Test: Verify order confirmation emails\",\"IssueType\":\"Test\",\"Product\":\"RED\",\"LinkedIssues\":[],\"Subtasks\":[{\"Summary\":\"Check email content\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Check email sending after order\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"},{\"Summary\":\"Document test results\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}],\"Subtasks\":[{\"Summary\":\"Document the order confirmation email process\",\"IssueType\":\"Sub-task\",\"Product\":\"RED\"}]}]}";


        public MainForm()
        {
            try
            {
                model = new();
                if( model.Settings == null ) throw new NullReferenceException(nameof(model.Settings));
                if( model.Settings.Key == null ) throw new NullReferenceException(nameof(model.Settings.Key));


                InitializeComponent();

                ResetUI();

                // for testing missing info situations
                //model.Settings.Key = Model.Model.DEFAULTKEY;
                //model.Settings.Key = "esdfggf9s4Ar-OsfgsgsfgCgclvaA-LitgsumL0";

                comboBoxJiraProjects.DataSource = model.Settings.Projects;
                this.comboBoxJiraProjects.SelectedIndex = 0;

                //again:
                //    PreferencesToolStripMenuItem_Click(this, new EventArgs());
                //    goto again;


                if( model.Settings.Key.Equals(Model.Model.DEFAULTKEY, StringComparison.CurrentCultureIgnoreCase) )
                {
                    MessageBox.Show($"Setting (json) file is needs to be initialized.\n\nA form will appear next, fill it out in order to use this app.\n\nYour settings will persist and you can change them later by using the \"Preferences\" menu item.\n\n", "Set up: Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PreferencesToolStripMenuItem_Click(model.Settings, new EventArgs());
                }
#if DEBUG 
                textBoxEpic.Text = "An epic Epic";
                checkBoxAddQATests.Checked = true;
                checkBoxAddSubTasks.Checked = true;


                string testProdDesc = "Implement a mobile-first, fully responsive e-commerce platform for our online store, enabling users to browse products, add them to a shopping cart, and complete secure online purchases. The platform should offer a streamlined checkout process, diverse payment options, and robust order tracking capabilities. We need to ensure that the platform is user-friendly, accessible, and highly performant, with a focus on a seamless user experience across all devices.  Please include this task: Create a Database table called \"Customer\".   Add the \"Name\" and \"Phone number\" and other relevant columns to the \"Customer\" table.";

                textBoxPRD.Value = testProdDesc;
                textBoxProductName.Text = "Feature X";

#endif
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

            treeView.Checked += (issueCollector) =>
            {
                buttonSave.Enabled = !( issueCollector.Total == 0 );
                UpdateSelectedIssues(issueCollector);
            };

            treeView.ProcessSingleStory += async (key, product, userStoryText) =>//
            {
                ResetUI();

                stopwatchClockConvertRun.Start();

                await model.ProcessSingleStory(key, product, textBoxProductName.Text, userStoryText, this.checkBoxAddQATests.Checked, this.checkBoxAddSubTasks.Checked);
                buttonProcessStories.Text = "Running";
                //Logger.Info("ProcessSingleStory");
                //
            };

            model.IssueGeneratorCompleted += Model_IssueGeneratorCompleted;
            model.UserStoryGeneratorCompleted += Model_UserStoryGeneratorCompleted;

            TestAndFill(UserStories);

#if DEBUG 
            treeView.Nodes[0].Expand();
            if( treeView.Nodes.Count > 0 )
                treeView.TopNode = treeView.Nodes[0];
            treeView.Nodes[0].Checked = false;
#endif

        }

        private void Model_UserStoryGeneratorCompleted(IssueGeneratorBaseArgs args)
        {
            if( this.InvokeRequired )
                this.Invoke(new System.Windows.Forms.MethodInvoker(() => { Model_UserStoryGeneratorCompleted(args); return; }));

            else
            {
                stopwatchClockConvertRun.Stop();
                TimeSpan duration = stopwatchClockConvertRun.ElapsedTime;
                groupBoxExDuration.Value = $"{duration.Minutes}:{duration.Seconds}.{duration.Milliseconds}";
                progressBar.Visible = false;

                buttonProcessStories.Enabled = true;

                if( args.Answer != null )
                {
                    Logger.Info(args.Answer);

                    treeView.Nodes.Clear();

                    ProcessIssues(args.Answer);
                }
                else
                {
                    // error occured!!
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
                Logger.Info($"model.IssueGeneratorCompleted : {args.UserStoryKey}");

                if( args.UserStoryKey == -1 )
                {
                    MessageBox.Show("The IssueGenerator Completed with no value.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    if( args.Answer != null )
                    {
                        buttonProcessStories.Text = args.Counter.ToString();

                        Model.IssueData? data = JsonSerializer.Deserialize<Model.IssueData>(args.Answer);
                        //Utilities.Logger.Info(JsonSerializer.Serialize(data));
                        if( data != null && data.Issues != null )
                        {
                            TreeNode[] found = treeView.Nodes[0].Nodes.Find(args.UserStoryKey.ToString(), false);
                            if( found.Length > 0 )
                            {
                                TreeNode node = found.First();
                                found = node.Nodes.Find("LinkedIssues", false);
                                if( found.Length > 0 )
                                {
                                    //found.First().Nodes.Clear();
                                    TriStateTreeView.RemovePreviousCreateNodes(found.First().Nodes);

                                    Recursive(data.Issues, found.First(), true);

                                    UpdateCountersUI(data);

                                    labelStatus.Text = $"Processed: {node.Text}";
                                    //Logger.Info($"Processed: {node.Text}");

                                    //node.Collapse();
                                    found.First().Expand();
                                    //found.First().ExpandAll();
                                    treeView.TopNode = node;
                                    //
                                }

                            }
                        }
                    }
                    //
                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                finally
                {
                    Logger.Info($"\tProcessed: {args.Counter}");

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

            treeView.Nodes.Clear();


            progressBar.Visible = true;
            buttonProcessStories.Enabled = false;

            if( comboBoxJiraProjects.SelectedValue == null ) throw new NullReferenceException(nameof(comboBoxJiraProjects.SelectedValue));
            string? project = comboBoxJiraProjects.SelectedValue.ToString();
            if( project == null ) throw new NullReferenceException(nameof(project));

            //string testProdDesc = "Implement a mobile-first, fully responsive e-commerce platform for our online store, enabling users to browse products, add them to a shopping cart, and complete secure online purchases. The platform should offer a streamlined checkout process, diverse payment options, and robust order tracking capabilities. We need to ensure that the platform is user-friendly, accessible, and highly performant, with a focus on a seamless user experience across all devices.  Please include this task: Create a Database table called \"Customer\".   Add the \"Name\" and \"Phone number\" and other relevant columns to the \"Customer\" table.";

            string? testProdDesc = textBoxPRD.Value;
            if( string.IsNullOrEmpty(testProdDesc) ) throw new NullReferenceException(nameof(testProdDesc));

            stopwatchClockConvertRun.Start();
            await model.ProduceUserStories(project, textBoxProductName.Text, testProdDesc, this.checkBoxAddQATests.Checked, checkBoxAddSubTasks.Checked);
            //Logger.Info("Convert_Click: model.ProduceUserStories running");
            //
        }

        private void ResetUI()
        {
            groupBoxExDuration.Value = null;

            this.stopwatchClockConvertRun.Reset();
            this.tableLayoutPanelResultsTop.Reset();
            this.tableLayoutPanelResultsBottom.Reset();

            progressBar.Visible = false;

            labelStatus.Text = null;

        }

        private void ProcessIssues(string answer)
        {
            try
            {
                Model.IssueData? data = JsonSerializer.Deserialize<Model.IssueData>(answer);
                //Utilities.Logger.Info(JsonSerializer.Serialize(data));
                if( data != null )
                    PopulateUI(data);
                //
            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TestAndFill(string json)
        {

            try
            {
                Model.IssueData? data = JsonSerializer.Deserialize<Model.IssueData>(json);
                string result = JsonSerializer.Serialize(data, options);
                //Utilities.Logger.Info(result);
                if( data != null )
                    PopulateUI(data);

                buttonProcessStories.Enabled = true;

            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static List<Model.IssueData.SubTask> GetAllSubTasks(List<Model.IssueData.Issue> issues)
        {
            List<Model.IssueData.SubTask> list = [];
            if( issues != null )
            {
                foreach( var issue in issues )
                {
                    if( issue.Subtasks != null )
                        list.AddRange(issue.Subtasks);

                    if( issue.LinkedIssues != null )
                        list.AddRange(GetAllSubTasks(issue.LinkedIssues));

                }
            }
            return list;
        }

        private void PopulateUI(Model.IssueData data)
        {
            if( data.Issues == null ) return;

            TreeNode? root = new(this.textBoxEpic.Text);
            treeView.Nodes.Add(root);

            Recursive(data.Issues, root);

            //root.Checked = true;            //treeView.SelectAllNodes(false);

            treeView.Nodes[0].Expand();
            if( treeView.Nodes.Count > 0 )
                treeView.TopNode = treeView.Nodes[0];
            treeView.Nodes[0].Checked = false;

            UpdateCountersUI(data);
            //
        }

        private void UpdateCountersUI(IssueData data)
        {
            if( data.Issues == null ) return;

            int total = 0;
            int count = data.Issues.FlattenStandardIssues().Where(issue => issue.IssueType == JiraIssueTypes.STORY).Count();
            total += count;
            groupBoxExStory.Value = count.ToString();

            count = data.Issues.FlattenStandardIssues().Where(issue => issue.IssueType == JiraIssueTypes.TASK).Count();
            total += count;
            groupBoxExTask.Value = count.ToString();
            count = data.Issues.FlattenStandardIssues().Where(issue => issue.IssueType == JiraIssueTypes.TEST).Count();
            total += count;
            groupBoxExTest.Value = count.ToString();
            count = data.Issues.FlattenStandardIssues().Where(issue => issue.IssueType == JiraIssueTypes.BUG).Count();
            total += count;
            groupBoxExBug.Value = count.ToString();

            count = GetAllSubTasks(data.Issues).Count;
            total += count;
            groupBoxExSubTask.Value = count.ToString();

            groupBoxExIssueCount.Value = total.ToString();
        }

        private static void Recursive(IList<Model.IssueData.Issue> issues, TreeNode node, bool allIssue = false)
        {
            foreach( Model.IssueData.Issue issue in issues )
            {
                if( issue.Summary == null ) continue;

                TriStateTreeView.TreeNodeEx? issueNode = new(issue)
                {
                    ToolTipText = issue.IssueType,
                    ForeColor = issue.IssueType switch
                    {
                        JiraIssueTypes.STORY => Color.DarkGreen,
                        JiraIssueTypes.TASK => Color.Navy,
                        JiraIssueTypes.BUG => Color.IndianRed,
                        JiraIssueTypes.TEST => Color.DarkGoldenrod,
                        JiraIssueTypes.SUBTASK => Color.DarkSalmon,
                        _ => Color.Black,
                    },
                    // mark the issue as generated by userStory generation  (false) or 'all issue' run for a user story (true)
                    Tag = allIssue
                };

                node.Nodes.Add(issueNode);

                TreeNodeExSubTasks subtasksNode = new("Subtasks");
                issueNode.Nodes.Add(subtasksNode);

                //TreeNode linkedIssuesNode = new TreeNode("LinkedIssues");
                TreeNodeExLinkedIssues linkedIssuesNode = new("LinkedIssues");
                issueNode.Nodes.Add(linkedIssuesNode);


                if( issue.Subtasks != null )
                {
                    foreach( Model.IssueData.SubTask subTask in issue.Subtasks )
                    {
                        TriStateTreeView.TreeNodeEx? newNodeSub = new(subTask)
                        {
                            ToolTipText = subTask.IssueType
                        };
                        subtasksNode.Nodes.Add(newNodeSub);
                    }
                }

                if( issue.LinkedIssues != null )
                {
                    if( issue.LinkedIssues != null )
                        Recursive(issue.LinkedIssues, linkedIssuesNode, allIssue);
                }
                //
            }
        }
        private void UpdateSelectedIssues(IssueCollector issueCollector)
        {
            groupBoxExSelectedIssues.Value = issueCollector.Total.ToString();
            groupBoxExSelectedStories.Value = issueCollector.Stories.Count.ToString();
            groupBoxExSelectedTasks.Value = issueCollector.Tasks.Count.ToString();
            groupBoxExSelectedTests.Value = issueCollector.Tests.Count.ToString();
            groupBoxExSelectedBugs.Value = issueCollector.Bugs.Count.ToString();
            groupBoxExSelectedSubTasks.Value = issueCollector.SubTasks.Count.ToString();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            List<string> storyList = treeView.GetCheckedNodes();
            List<TreeNode> checkedHierarchy = treeView.GetCheckedNodesHierarchy(true);

            if( storyList.Count == 0 )
            {
                MessageBox.Show("No selections were made", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                //
            }

            Model.Model.SaveDataToFile(this.textBoxEpic.Text, storyList, checkedHierarchy);
            //
        }


        private void TextControls_TextChanged(object? sender, EventArgs e)
        {
            buttonConvert.Enabled = !( textBoxProductName.TextLength == 0 || textBoxPRD.TextLength == 0 || comboBoxJiraProjects.CurrentSelectedValue.Length == 0 );
        }
        private void TextBoxEpic_TextChanged(object sender, EventArgs e)
        {
            string currentText = this.textBoxEpic.Text;

            if( currentText.Length == 0 )//|| currentText.Length> 8
            {
                textBoxEpic.ForeColor = SystemColors.ControlText;
                TextControls_TextChanged(sender, new EventArgs());
                return;
            }

            bool found = Utilities.InputValidator.RegexContainsValidation(currentText);
            if( found )
            {
                bool isValid = Utilities.InputValidator.RegexValidation(currentText);

                // Update the label based on the validation result
                if( isValid )
                {
                    textBoxEpic.ForeColor = SystemColors.ControlText;
                    TextControls_TextChanged(sender, new EventArgs());
                    return;
                }
                else
                {
                    textBoxEpic.ForeColor = System.Drawing.Color.Red;
                    buttonConvert.Enabled = false;
                    return;
                }
            }
            else
            {
                textBoxEpic.ForeColor = SystemColors.ControlText;
                TextControls_TextChanged(sender, new EventArgs());
            }
        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? selectedValue = comboBoxJiraProjects.CurrentSelectedValue;

            SettingsForm? form = null;

            form = new SettingsForm(model.Settings);

            if( form != null )
            {
                form.NewSettings += (settings, save) =>
                {
                    model.SetSettings(settings);
                    if( save )
                        model.SaveSettings();

                    comboBoxJiraProjects.DataSource = settings.Projects;

                    int index = comboBoxJiraProjects.FindString(selectedValue);
                    if( index == -1 )
                        index = 0;
                    this.comboBoxJiraProjects.SelectedIndex = index;

                };

                form.ShowDialog();
            }
        }

        private void TreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            if( e.Data == null ) return;

            // Check if the clipboard contains text
            if( Clipboard.ContainsText() )
            {
                // Get the text from the clipboard
                string answer = Clipboard.GetText();
                Logger.Info(answer);

                treeView.Nodes.Clear();

                ProcessIssues(answer);

                return;
            }

            //if( e.Data.GetDataPresent(DataFormats.Text) )
            //{
            //    // If it's text, allow a Copy operation (or Move, Link, etc.)
            //    e.Effect = DragDropEffects.Copy;
            //}
            //else
            //{
            //    // If it's not text, disallow the drop
            //    e.Effect = DragDropEffects.None;
            //}
        }

        private async void ButtonProcessStories_ClickAsync(object sender, EventArgs e)//async
        {
            stopwatchClockConvertRun.Start();

            List<StoryPackage> list = [];
            foreach( TreeNode node in treeView.Nodes[0].Nodes )
            {
                if( !node.Checked ) continue;

                string userStoryText = node.Text;
                if( string.IsNullOrEmpty(userStoryText) ) continue;

                if( node is not TreeNodeEx treeNodeEx ) continue;

                list.Add(new(treeNodeEx));

            }

            if( list.Count > 0 )
            {
                await model.ProcessStoryList(textBoxProductName.Text, list);//await
                //Logger.Info("ButtonProcessStories_ClickAsync");
                buttonProcessStories.Text = "Running";
            }
            else
                MessageBox.Show("None of the user stories were checked.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //
        }
    }
}
