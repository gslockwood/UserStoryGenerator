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

        readonly string UserStories = "{\"HierarchyIssueList\":[{\"IssueType\":\"Story\",\"Key\":1649068627,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":587035144,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2454792075,\"Product\":\"RED\",\"Summary\":\"Create test cases for product browsing functionality\"},{\"IssueType\":\"Sub-task\",\"Key\":764291867,\"Product\":\"RED\",\"Summary\":\"Execute test cases on multiple devices and browsers\"},{\"IssueType\":\"Sub-task\",\"Key\":26578318,\"Product\":\"RED\",\"Summary\":\"Document test results and report any defects\"}],\"Summary\":\"Integrated Test: Verify product browsing functionality on various mobile devices and screen sizes\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1270214976,\"Product\":\"RED\",\"Summary\":\"Document the product browsing functionality for the user manual\"},{\"IssueType\":\"Sub-task\",\"Key\":4140430735,\"Product\":\"RED\",\"Summary\":\"Create a tutorial video on how to browse products on the platform\"}],\"Summary\":\"As a user, I want to be able to browse products on the mobile-first e-commerce platform, so that I can find the items I'm interested in purchasing.\"},{\"IssueType\":\"Story\",\"Key\":446649243,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":338169936,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1167366658,\"Product\":\"RED\",\"Summary\":\"Create test cases for adding products to the cart\"},{\"IssueType\":\"Sub-task\",\"Key\":3971705185,\"Product\":\"RED\",\"Summary\":\"Execute test cases to ensure correct product addition and quantity\"},{\"IssueType\":\"Sub-task\",\"Key\":1418386716,\"Product\":\"RED\",\"Summary\":\"Document test results and report any issues\"}],\"Summary\":\"Integrated Test: Verify adding products to the shopping cart functionality\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2486325989,\"Product\":\"RED\",\"Summary\":\"Document how to add products to the shopping cart\"},{\"IssueType\":\"Sub-task\",\"Key\":3361308875,\"Product\":\"RED\",\"Summary\":\"Create help documentation for managing the shopping cart\"}],\"Summary\":\"As a user, I want to be able to add products to a shopping cart, so that I can keep track of the items I want to purchase.\"},{\"IssueType\":\"Story\",\"Key\":1713690331,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2897545317,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3078070374,\"Product\":\"RED\",\"Summary\":\"Create test cases for secure payment processing\"},{\"IssueType\":\"Sub-task\",\"Key\":1301364100,\"Product\":\"RED\",\"Summary\":\"Simulate various payment scenarios to ensure security\"},{\"IssueType\":\"Sub-task\",\"Key\":490574916,\"Product\":\"RED\",\"Summary\":\"Document test results and security measures\"}],\"Summary\":\"Integrated Test: Verify the secure online purchase process\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":413906888,\"Product\":\"RED\",\"Summary\":\"Document the secure purchase process and payment options\"},{\"IssueType\":\"Sub-task\",\"Key\":1055383515,\"Product\":\"RED\",\"Summary\":\"Provide information on security measures taken to protect user data\"}],\"Summary\":\"As a user, I want to be able to complete secure online purchases, so that I can buy the products I want with confidence.\"},{\"IssueType\":\"Story\",\"Key\":3294246566,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3452180028,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1136422089,\"Product\":\"RED\",\"Summary\":\"Create test cases for the checkout process\"},{\"IssueType\":\"Sub-task\",\"Key\":2322527436,\"Product\":\"RED\",\"Summary\":\"Evaluate the checkout flow for user-friendliness\"},{\"IssueType\":\"Sub-task\",\"Key\":2101327108,\"Product\":\"RED\",\"Summary\":\"Document test results and suggest improvements\"}],\"Summary\":\"Integrated Test: Verify the checkout process flow and ease of use\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2593610302,\"Product\":\"RED\",\"Summary\":\"Document the steps involved in the streamlined checkout process\"},{\"IssueType\":\"Sub-task\",\"Key\":867322866,\"Product\":\"RED\",\"Summary\":\"Provide tips for a smooth and quick checkout experience\"}],\"Summary\":\"As a user, I want a streamlined checkout process, so that I can complete my purchase quickly and easily.\"},{\"IssueType\":\"Story\",\"Key\":1664452791,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2993469897,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":920180210,\"Product\":\"RED\",\"Summary\":\"Create test cases for each payment gateway\"},{\"IssueType\":\"Sub-task\",\"Key\":3453890612,\"Product\":\"RED\",\"Summary\":\"Test payment processing with different payment methods\"},{\"IssueType\":\"Sub-task\",\"Key\":1911755044,\"Product\":\"RED\",\"Summary\":\"Document test results and payment gateway performance\"}],\"Summary\":\"Integrated Test: Verify the integration of various payment options\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2378858682,\"Product\":\"RED\",\"Summary\":\"Document all available payment options and their usage\"},{\"IssueType\":\"Sub-task\",\"Key\":4086350689,\"Product\":\"RED\",\"Summary\":\"Provide information on how to add or manage payment methods\"}],\"Summary\":\"As a user, I want diverse payment options, so that I can choose the payment method that is most convenient for me.\"},{\"IssueType\":\"Story\",\"Key\":3837581403,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3999191302,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2071848343,\"Product\":\"RED\",\"Summary\":\"Create test cases for order status updates\"},{\"IssueType\":\"Sub-task\",\"Key\":3381307541,\"Product\":\"RED\",\"Summary\":\"Track orders through various stages to ensure accuracy\"},{\"IssueType\":\"Sub-task\",\"Key\":3471484669,\"Product\":\"RED\",\"Summary\":\"Document test results and tracking accuracy\"}],\"Summary\":\"Integrated Test: Verify the order tracking functionality\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":4097015665,\"Product\":\"RED\",\"Summary\":\"Document how to track orders and interpret order status updates\"},{\"IssueType\":\"Sub-task\",\"Key\":223392629,\"Product\":\"RED\",\"Summary\":\"Provide information on estimated delivery times and possible delays\"}],\"Summary\":\"As a user, I want robust order tracking capabilities, so that I can monitor the status of my order from placement to delivery.\"},{\"IssueType\":\"Story\",\"Key\":2155179632,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2918656087,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1813718233,\"Product\":\"RED\",\"Summary\":\"Plan and conduct usability testing sessions\"},{\"IssueType\":\"Sub-task\",\"Key\":2728375404,\"Product\":\"RED\",\"Summary\":\"Gather user feedback on platform usability\"},{\"IssueType\":\"Sub-task\",\"Key\":2141490697,\"Product\":\"RED\",\"Summary\":\"Document findings and suggest improvements\"}],\"Summary\":\"Integrated Test: Conduct usability testing on the e-commerce platform\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3904504068,\"Product\":\"RED\",\"Summary\":\"Document key features and navigation tips for the platform\"},{\"IssueType\":\"Sub-task\",\"Key\":4111357850,\"Product\":\"RED\",\"Summary\":\"Provide information on how to find and use various features\"}],\"Summary\":\"As a user, I want a user-friendly platform, so that I can easily navigate and use the e-commerce site.\"},{\"IssueType\":\"Story\",\"Key\":3686373086,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2940802464,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":610125674,\"Product\":\"RED\",\"Summary\":\"Test the platform with screen readers and other assistive technologies\"},{\"IssueType\":\"Sub-task\",\"Key\":2968485201,\"Product\":\"RED\",\"Summary\":\"Ensure compliance with accessibility standards\"},{\"IssueType\":\"Sub-task\",\"Key\":1210455768,\"Product\":\"RED\",\"Summary\":\"Document test results and accessibility improvements\"}],\"Summary\":\"Integrated Test: Verify the platform's accessibility features\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2267829901,\"Product\":\"RED\",\"Summary\":\"Document accessibility features and how to use them\"},{\"IssueType\":\"Sub-task\",\"Key\":788623986,\"Product\":\"RED\",\"Summary\":\"Provide information on keyboard navigation and alternative text\"}],\"Summary\":\"As a user, I want an accessible platform, so that I can use the e-commerce site regardless of my abilities.\"},{\"IssueType\":\"Story\",\"Key\":3241479326,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1925050148,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3259563190,\"Product\":\"RED\",\"Summary\":\"Conduct load testing to measure platform performance\"},{\"IssueType\":\"Sub-task\",\"Key\":2668410965,\"Product\":\"RED\",\"Summary\":\"Optimize platform performance for various devices and browsers\"},{\"IssueType\":\"Sub-task\",\"Key\":1289143820,\"Product\":\"RED\",\"Summary\":\"Document test results and performance improvements\"}],\"Summary\":\"Integrated Test: Performance testing of the e-commerce platform\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2084653888,\"Product\":\"RED\",\"Summary\":\"Document performance benchmarks and optimization strategies\"},{\"IssueType\":\"Sub-task\",\"Key\":639343244,\"Product\":\"RED\",\"Summary\":\"Provide tips for users to improve their experience\"}],\"Summary\":\"As a user, I want a highly performant platform, so that I can have a smooth and responsive shopping experience.\"},{\"IssueType\":\"Story\",\"Key\":3711963657,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2980779781,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2841519124,\"Product\":\"RED\",\"Summary\":\"Test the platform on different devices (desktops, tablets, phones)\"},{\"IssueType\":\"Sub-task\",\"Key\":1936314199,\"Product\":\"RED\",\"Summary\":\"Ensure consistent design and functionality across devices\"},{\"IssueType\":\"Sub-task\",\"Key\":2759451306,\"Product\":\"RED\",\"Summary\":\"Document test results and responsive design improvements\"}],\"Summary\":\"Integrated Test: Verify responsive design and cross-device compatibility\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1843491560,\"Product\":\"RED\",\"Summary\":\"Document device compatibility and responsive design features\"},{\"IssueType\":\"Sub-task\",\"Key\":1584415073,\"Product\":\"RED\",\"Summary\":\"Provide tips for users to optimize their experience on each device\"}],\"Summary\":\"As a user, I want a seamless user experience across all devices, so that I can shop on any device without issues.\"},{\"IssueType\":\"Story\",\"Key\":2370202927,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3398619508,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":315387989,\"Product\":\"RED\",\"Summary\":\"Create test script to verify table schema\"},{\"IssueType\":\"Sub-task\",\"Key\":1569810881,\"Product\":\"RED\",\"Summary\":\"Execute test script and validate column types and constraints\"},{\"IssueType\":\"Sub-task\",\"Key\":245324227,\"Product\":\"RED\",\"Summary\":\"Document test results and report any issues\"}],\"Summary\":\"Integrated Test: Verify the creation of the 'Customer' table\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3116101524,\"Product\":\"RED\",\"Summary\":\"Document the schema of the 'Customer' table\"},{\"IssueType\":\"Sub-task\",\"Key\":3524301084,\"Product\":\"RED\",\"Summary\":\"Provide information on how to access and manage the 'Customer' table\"}],\"Summary\":\"As a database administrator, I want to create a 'Customer' table with 'Name' and 'Phone number' columns, so that I can store customer information.\"},{\"IssueType\":\"Story\",\"Key\":1117438076,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":396517634,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3256356285,\"Product\":\"RED\",\"Summary\":\"Check CSS media queries and responsiveness\"},{\"IssueType\":\"Sub-task\",\"Key\":2793165259,\"Product\":\"RED\",\"Summary\":\"Check website on different mobile resolutions\"},{\"IssueType\":\"Sub-task\",\"Key\":552273351,\"Product\":\"RED\",\"Summary\":\"Document test results and report deviations\"}],\"Summary\":\"Integrated Test: Verify that the platform is mobile-first\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3033259711,\"Product\":\"RED\",\"Summary\":\"Document how the platform follows mobile-first principles\"},{\"IssueType\":\"Sub-task\",\"Key\":3999057671,\"Product\":\"RED\",\"Summary\":\"Highlight mobile-specific features and optimizations\"}],\"Summary\":\"As a system administrator, I want to ensure the e-commerce platform is mobile-first in its design, so that it prioritizes the experience for mobile users.\"},{\"IssueType\":\"Story\",\"Key\":4255244087,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":2481869286,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3158309498,\"Product\":\"RED\",\"Summary\":\"Check search accuracy and speed\"},{\"IssueType\":\"Sub-task\",\"Key\":2417084760,\"Product\":\"RED\",\"Summary\":\"Test with different keywords and categories\"},{\"IssueType\":\"Sub-task\",\"Key\":3083886053,\"Product\":\"RED\",\"Summary\":\"Document test results and report any defects\"}],\"Summary\":\"Integrated Test: Verify product search functionality\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2439407812,\"Product\":\"RED\",\"Summary\":\"Document the product search functionality for the user manual\"}],\"Summary\":\"As a user, I want to be able to search for products using keywords, so that I can quickly find what I'm looking for.\"},{\"IssueType\":\"Story\",\"Key\":3441120175,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1873637454,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1056408195,\"Product\":\"RED\",\"Summary\":\"Create test cases for product filtering and sorting\"},{\"IssueType\":\"Sub-task\",\"Key\":1272108635,\"Product\":\"RED\",\"Summary\":\"Execute test cases on multiple filters and sorting options\"},{\"IssueType\":\"Sub-task\",\"Key\":630991445,\"Product\":\"RED\",\"Summary\":\"Document test results and report any defects\"}],\"Summary\":\"Integrated Test: Verify product filtering and sorting functionality\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2114958888,\"Product\":\"RED\",\"Summary\":\"Document how to use product filtering and sorting\"}],\"Summary\":\"As a user, I want to be able to filter and sort products based on various attributes (price, rating, etc.), so that I can narrow down my search.\"},{\"IssueType\":\"Story\",\"Key\":660025231,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":302957051,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3939140480,\"Product\":\"RED\",\"Summary\":\"Check functionality of saving multiple addresses\"},{\"IssueType\":\"Sub-task\",\"Key\":1682969427,\"Product\":\"RED\",\"Summary\":\"Check editing and deleting saved addresses\"},{\"IssueType\":\"Sub-task\",\"Key\":2098846749,\"Product\":\"RED\",\"Summary\":\"Document test results\"}],\"Summary\":\"Integrated Test: Verify saving shipping addresses\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2162833996,\"Product\":\"RED\",\"Summary\":\"Document saving shipping address functionality\"}],\"Summary\":\"As a registered user, I want to be able to save my shipping addresses, so that I don't have to re-enter them every time I make a purchase.\"},{\"IssueType\":\"Story\",\"Key\":2567137657,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3116012250,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3417962979,\"Product\":\"RED\",\"Summary\":\"Check image loading and quality\"},{\"IssueType\":\"Sub-task\",\"Key\":1356830767,\"Product\":\"RED\",\"Summary\":\"Check description accuracy\"},{\"IssueType\":\"Sub-task\",\"Key\":3866632976,\"Product\":\"RED\",\"Summary\":\"Check review display\"}],\"Summary\":\"Integrated Test: Verify product details display\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":346208816,\"Product\":\"RED\",\"Summary\":\"Document viewing product details functionality\"}],\"Summary\":\"As a user, I want to be able to view product details (images, descriptions, reviews), so that I can make informed purchasing decisions.\"},{\"IssueType\":\"Story\",\"Key\":1488175957,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":765275209,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":1749467067,\"Product\":\"RED\",\"Summary\":\"Check email validation\"},{\"IssueType\":\"Sub-task\",\"Key\":2517721082,\"Product\":\"RED\",\"Summary\":\"Check password security\"},{\"IssueType\":\"Sub-task\",\"Key\":2727085544,\"Product\":\"RED\",\"Summary\":\"Document test results\"}],\"Summary\":\"Integrated Test: Verify account creation process\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":400956466,\"Product\":\"RED\",\"Summary\":\"Document the account creation process\"}],\"Summary\":\"As a user, I want to be able to create an account, so that I can save my preferences and order history.\"},{\"IssueType\":\"Story\",\"Key\":2231481159,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":1646438920,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":524443869,\"Product\":\"RED\",\"Summary\":\"Check valid login\"},{\"IssueType\":\"Sub-task\",\"Key\":2109751548,\"Product\":\"RED\",\"Summary\":\"Check invalid login attempts\"},{\"IssueType\":\"Sub-task\",\"Key\":1264878664,\"Product\":\"RED\",\"Summary\":\"Document test results\"}],\"Summary\":\"Integrated Test: Verify login functionality\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":175243526,\"Product\":\"RED\",\"Summary\":\"Document the login process\"}],\"Summary\":\"As a registered user, I want to be able to log in to my account, so that I can access my saved information and order history.\"},{\"IssueType\":\"Story\",\"Key\":612281808,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":427601210,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":2448496601,\"Product\":\"RED\",\"Summary\":\"Check password reset via email\"},{\"IssueType\":\"Sub-task\",\"Key\":4194521521,\"Product\":\"RED\",\"Summary\":\"Check security questions\"},{\"IssueType\":\"Sub-task\",\"Key\":707158260,\"Product\":\"RED\",\"Summary\":\"Document test results\"}],\"Summary\":\"Integrated Test: Verify password reset functionality\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":114221414,\"Product\":\"RED\",\"Summary\":\"Document the password reset process\"}],\"Summary\":\"As a registered user, I want to be able to reset my password if I forget it, so that I can regain access to my account.\"},{\"IssueType\":\"Story\",\"Key\":1274393430,\"LinkedIssues\":[{\"IssueType\":\"Test\",\"Key\":3386218584,\"LinkedIssues\":[],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3765648285,\"Product\":\"RED\",\"Summary\":\"Check email content\"},{\"IssueType\":\"Sub-task\",\"Key\":1095962066,\"Product\":\"RED\",\"Summary\":\"Check email sending after order\"},{\"IssueType\":\"Sub-task\",\"Key\":2104199858,\"Product\":\"RED\",\"Summary\":\"Document test results\"}],\"Summary\":\"Integrated Test: Verify order confirmation emails\"}],\"Product\":\"RED\",\"Subtasks\":[{\"IssueType\":\"Sub-task\",\"Key\":3032997877,\"Product\":\"RED\",\"Summary\":\"Document the order confirmation email process\"}],\"Summary\":\"As a user, I want to receive confirmation emails for my orders, so that I have a record of my purchase.\"}],\"IssueList\":[\"As a user, I want to be able to browse products on the mobile-first e-commerce platform, so that I can find the items I'm interested in purchasing.\",\"As a user, I want to be able to add products to a shopping cart, so that I can keep track of the items I want to purchase.\",\"As a user, I want to be able to complete secure online purchases, so that I can buy the products I want with confidence.\",\"As a user, I want a streamlined checkout process, so that I can complete my purchase quickly and easily.\",\"As a user, I want diverse payment options, so that I can choose the payment method that is most convenient for me.\",\"As a user, I want robust order tracking capabilities, so that I can monitor the status of my order from placement to delivery.\",\"As a user, I want a user-friendly platform, so that I can easily navigate and use the e-commerce site.\",\"As a user, I want an accessible platform, so that I can use the e-commerce site regardless of my abilities.\",\"As a user, I want a highly performant platform, so that I can have a smooth and responsive shopping experience.\",\"As a user, I want a seamless user experience across all devices, so that I can shop on any device without issues.\",\"As a database administrator, I want to create a 'Customer' table with 'Name' and 'Phone number' columns, so that I can store customer information.\",\"As a system administrator, I want to ensure the e-commerce platform is mobile-first in its design, so that it prioritizes the experience for mobile users.\",\"As a user, I want to be able to search for products using keywords, so that I can quickly find what I'm looking for.\",\"As a user, I want to be able to filter and sort products based on various attributes (price, rating, etc.), so that I can narrow down my search.\",\"As a registered user, I want to be able to save my shipping addresses, so that I don't have to re-enter them every time I make a purchase.\",\"As a user, I want to be able to view product details (images, descriptions, reviews), so that I can make informed purchasing decisions.\",\"As a user, I want to be able to create an account, so that I can save my preferences and order history.\",\"As a registered user, I want to be able to log in to my account, so that I can access my saved information and order history.\",\"As a registered user, I want to be able to reset my password if I forget it, so that I can regain access to my account.\",\"As a user, I want to receive confirmation emails for my orders, so that I have a record of my purchase.\"]}";


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

                comboBoxExStoryMin.DataSource = new int[] { 10, 20, 30, 40, 50, 75 };
                comboBoxExStoryMin.SelectedIndex = 2;

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
                this.groupBoxExEpic.Value = "An epic Epic";
                checkBoxAddQATests.Checked = true;
                checkBoxAddSubTasks.Checked = true;


                string testProdDesc = "Implement a mobile-first, fully responsive e-commerce platform for our online store, enabling users to browse products, add them to a shopping cart, and complete secure online purchases. The platform should offer a streamlined checkout process, diverse payment options, and robust order tracking capabilities. We need to ensure that the platform is user-friendly, accessible, and highly performant, with a focus on a seamless user experience across all devices.  Please include this task: Create a Database table called \"Customer\".   Add the \"Name\" and \"Phone number\" and other relevant columns to the \"Customer\" table.";

                textBoxPRD.Value = testProdDesc;
                groupBoxExProductFeature.Value = "Feature X";

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

                if( groupBoxExProductFeature.Value == null ) throw new NullReferenceException(nameof(groupBoxExProductFeature));
                if( comboBoxExStoryMin.SelectedItem == null ) throw new NullReferenceException(nameof(comboBoxExStoryMin.SelectedItem));
                await model.ProcessSingleStory(key, product, groupBoxExProductFeature.Value, userStoryText, this.checkBoxAddQATests.Checked, this.checkBoxAddSubTasks.Checked, (int)comboBoxExStoryMin.SelectedItem);
                buttonProcessStories.Text = "Running";
                //Logger.Info("ProcessSingleStory");
                //
            };

            model.IssueGeneratorCompleted += Model_IssueGeneratorCompleted;
            model.UserStoryGeneratorCompleted += Model_UserStoryGeneratorCompleted;


#if DEBUG 
            //FillUIFromJson(UserStories);
            //treeView.Nodes[0].Expand();
            //if( treeView.Nodes.Count > 0 )
            //    treeView.TopNode = treeView.Nodes[0];
            //treeView.Nodes[0].Checked = false;
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
                //progressBar.Visible = false;

                buttonProcessStories.Enabled = true;

                if( args.Result != null && args.Result.Answer != null )
                {
                    //Logger.Info(args.Answer);

                    treeView.Nodes.Clear();
                    saveStoriesAsJsonToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;
                    saveStoriesAsCSVToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;

                    ProcessIssues(args.Result.Answer);
                }
                else
                {
                    MessageBox.Show("The Issue Generator Completed with no value.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //Logger.Info($"model.IssueGeneratorCompleted : {args.UserStoryKey}");

                if( args.UserStoryKey == -1 )
                {
                    MessageBox.Show("The IssueGenerator Completed with no value.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    if( args.Result == null ) throw new NullReferenceException(nameof(args.Result));

                    buttonProcessStories.Text = args.Counter.ToString();

                    saveStoriesAsJsonToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;
                    saveStoriesAsCSVToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;

                    if( args.Result.Answer == null )
                    {
                        TreeNode[] userStoryNodeArray = treeView.Nodes[0].Nodes.Find(args.UserStoryKey.ToString(), false);
                        string parentText = "";
                        if( userStoryNodeArray.Length > 0 )
                        {
                            TreeNode userStoryNode = userStoryNodeArray.First();
                            parentText = userStoryNode.Text;

                            Color temp = userStoryNode.BackColor;
                            userStoryNode.BackColor = Color.Red;
                            userStoryNode.ForeColor = temp;
                        }

                        //StringTrimming stringTrimming = new()
                        labelStatus.Text = $"Failed to build LinkedIssues for Story: {parentText} {args.Counter}";
                        labelStatus.ForeColor = Color.Red;

                    }
                    else
                    {
                        try
                        {
                            Model.IssueData? data = JsonSerializer.Deserialize<Model.IssueData>(args.Result.Answer);
                            Utilities.Logger.Info(JsonSerializer.Serialize(data));
                            if( data != null && data.Issues != null )
                            {
                                //Logger.Info("Model_IssueGeneratorCompleted " + data.Issues.Count);

                                TreeNode[] found = treeView.Nodes[0].Nodes.Find(args.UserStoryKey.ToString(), false);
                                if( found.Length > 0 )
                                {
                                    TreeNode userStoryNode = found.First();
                                    //if( userStoryNode.ForeColor != SystemColors.WindowText )
                                    //{
                                    //    // reset the colors
                                    //    userStoryNode.BackColor = SystemColors.Window;
                                    //    userStoryNode.ForeColor = SystemColors.WindowText;
                                    //}

                                    TreeNode[] linkedIssuesNodeArray = userStoryNode.Nodes.Find("LinkedIssues", false);
                                    if( linkedIssuesNodeArray.Length > 0 )
                                    {
                                        TriStateTreeView.RemovePreviousCreatedNodesIfTagged(linkedIssuesNodeArray.First().Nodes);

                                        Recursive(data.Issues, linkedIssuesNodeArray.First(), true);

                                        UpdateCountersUI(data.Issues);

                                        labelStatus.Text = $"Processed: {userStoryNode.Text}";
                                        //Logger.Info($"Processed: {node.Text}");

                                        //.Collapse();
                                        linkedIssuesNodeArray.First().Expand();
                                        treeView.TopNode = userStoryNode;
                                        //
                                    }

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

            treeView.Nodes.Clear();
            saveStoriesAsJsonToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;
            saveStoriesAsCSVToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;

            //progressBar.Visible = true;
            buttonProcessStories.Enabled = false;

            string? project = comboBoxJiraProjects.CurrentSelectedItem.ToString();
            if( project == null ) throw new NullReferenceException(nameof(project));

            //string testProdDesc = "Implement a mobile-first, fully responsive e-commerce platform for our online store, enabling users to browse products, add them to a shopping cart, and complete secure online purchases. The platform should offer a streamlined checkout process, diverse payment options, and robust order tracking capabilities. We need to ensure that the platform is user-friendly, accessible, and highly performant, with a focus on a seamless user experience across all devices.  Please include this task: Create a Database table called \"Customer\".   Add the \"Name\" and \"Phone number\" and other relevant columns to the \"Customer\" table.";

            string? testProdDesc = textBoxPRD.Value;
            if( string.IsNullOrEmpty(testProdDesc) ) throw new NullReferenceException(nameof(testProdDesc));

            if( groupBoxExProductFeature.Value == null ) throw new NullReferenceException(nameof(groupBoxExProductFeature));

            stopwatchClockConvertRun.Start();

            if( comboBoxExStoryMin.SelectedItem == null ) throw new NullReferenceException(nameof(comboBoxExStoryMin.SelectedItem));
            await model.ProduceUserStories(project, groupBoxExProductFeature.Value, testProdDesc, this.checkBoxAddQATests.Checked, checkBoxAddSubTasks.Checked, (int)comboBoxExStoryMin.SelectedItem);
            //Logger.Info("Convert_Click: model.ProduceUserStories running");
            //
        }

        private void ResetUI()
        {
            groupBoxExDuration.Value = null;

            this.stopwatchClockConvertRun.Reset();
            this.tableLayoutPanelResultsTop.Reset();
            this.tableLayoutPanelResultsBottom.Reset();

            //progressBar.Visible = false;

            labelStatus.Text = null;
            labelStatus.ForeColor = SystemColors.ControlText;

            saveStoriesAsJsonToolStripMenuItem.Enabled = false;

        }

        private void ProcessIssues(string answer)
        {
            try
            {
                Model.IssueData? data = JsonSerializer.Deserialize<Model.IssueData>(answer);
                //Utilities.Logger.Info(JsonSerializer.Serialize(data));
                if( data != null && data.Issues != null )
                    PopulateUI(data.Issues);

                saveStoriesAsJsonToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;
                saveStoriesAsCSVToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;
                //
            }
            catch( Exception ex )
            {
                MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillUIFromJson(string json)
        {
            try
            {
                TreeSerialization.IssueResults? issueResults = JsonSerializer.Deserialize<TreeSerialization.IssueResults>(json);

                if( issueResults == null ) return;

                List<IssueData.Issue>? issues = issueResults.HierarchyIssueList;

                //Model.IssueData? data = datadata;
                //data.Issues = new List<IssueData.Issue>();

                //Model.IssueData? data = JsonSerializer.Deserialize<Model.IssueData>(json);
                //string result = JsonSerializer.Serialize(data, options);
                ////Utilities.Logger.Info(result);
                if( issues != null )
                {

                    treeView.Nodes.Clear();

                    saveStoriesAsJsonToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;
                    saveStoriesAsCSVToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;

                    PopulateUI(issues);

                    saveStoriesAsJsonToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;
                    saveStoriesAsCSVToolStripMenuItem.Enabled = treeView.Nodes.Count > 0;

                }
                buttonProcessStories.Enabled = true;

            }
            catch( Exception )
            {
                throw;
            }
        }


        private void PopulateUI(List<IssueData.Issue> issues)
        {
            if( issues == null ) return;

            //treeView.Nodes.Clear();
            TreeNode? root = new(this.groupBoxExEpic.Value);
            treeView.Nodes.Add(root);

            Recursive(issues, root);

            //root.Checked = true;            //treeView.SelectAllNodes(false);

            treeView.Nodes[0].Expand();
            if( treeView.Nodes.Count > 0 )
                treeView.TopNode = treeView.Nodes[0];
            treeView.Nodes[0].Checked = false;

            UpdateCountersUI(issues);
            //
        }

        //private void UpdateCountersUI(IssueData data)
        private void UpdateCountersUI(List<IssueData.Issue> issues)
        {
            if( issues == null ) return;

            int total = 0;
            int count = issues.FlattenStandardIssues().Where(issue => issue.IssueType == JiraIssueTypes.STORY).Count();
            total += count;
            groupBoxExStory.Value = count.ToString();

            count = issues.FlattenStandardIssues().Where(issue => issue.IssueType == JiraIssueTypes.TASK).Count();
            total += count;
            groupBoxExTask.Value = count.ToString();
            count = issues.FlattenStandardIssues().Where(issue => issue.IssueType == JiraIssueTypes.TEST).Count();
            total += count;
            groupBoxExTest.Value = count.ToString();
            count = issues.FlattenStandardIssues().Where(issue => issue.IssueType == JiraIssueTypes.BUG).Count();
            total += count;
            groupBoxExBug.Value = count.ToString();

            count = GetAllSubTasks(issues).Count;
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
            buttonConvert.Enabled = !( groupBoxExProductFeature.TextLength == 0 || textBoxPRD.TextLength == 0 );
            //buttonConvert.Enabled = !( groupBoxExProductFeature.TextLength == 0 || textBoxPRD.TextLength == 0 || comboBoxJiraProjects.CurrentSelectedItem.Length == 0 || comboBoxExStoryMin.CurrentSelectedItem.Length == 0 );
        }
        private void TextBoxEpic_TextChanged(object sender, EventArgs e)
        {
            if( groupBoxExEpic.Value == null ) throw new NullReferenceException(nameof(groupBoxExEpic));

            string currentText = this.groupBoxExEpic.Value;

            if( currentText.Length == 0 )//|| currentText.Length> 8
            {
                groupBoxExEpic.TextBoxForeColor = SystemColors.ControlText;
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
                    groupBoxExEpic.TextBoxForeColor = SystemColors.ControlText;
                    TextControls_TextChanged(sender, new EventArgs());
                    return;
                }
                else
                {
                    groupBoxExEpic.TextBoxForeColor = System.Drawing.Color.Red;
                    buttonConvert.Enabled = false;
                    return;
                }
            }
            else
            {
                groupBoxExEpic.TextBoxForeColor = SystemColors.ControlText;
                TextControls_TextChanged(sender, new EventArgs());
            }
        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string? selectedValue = comboBoxJiraProjects.CurrentSelectedItem.ToString();

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
                string text = Clipboard.GetText();

                textBoxPRD.Text = text;

                //Logger.Info(answer);

                //treeView.Nodes.Clear();
                //ProcessIssues(answer);

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
                await model.ProcessStoryList(groupBoxExProductFeature.Value, checkBoxAddQATests.Checked, checkBoxAddSubTasks.Checked, (int)comboBoxExStoryMin.SelectedItem, list);//await
                //Logger.Info("ButtonProcessStories_ClickAsync");
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

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new();
            dialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            dialog.FilterIndex = 1; // Sets the default selected filter to "Text Files"
            dialog.RestoreDirectory = true; // Restores the directory to the previously selected one
            if( dialog.ShowDialog() == DialogResult.OK )
            {
                try
                {
                    string json = File.ReadAllText(dialog.FileName);
                    FillUIFromJson(json);
                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void saveStoriesAsJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new();
            dialog.Filter = "Text Files (*.json)|*.json|All Files (*.*)|*.*";
            dialog.FilterIndex = 1; // Sets the default selected filter to "Text Files"
            dialog.RestoreDirectory = true; // Restores the directory to the previously selected one

            if( dialog.ShowDialog() == DialogResult.OK )
            {
                // Get the selected file path
                string filePath = dialog.FileName;
                try
                {
                    SaveData();
                    bool success = model.SaveUserStoryResultsToJson(filePath);
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
            List<string> storyList = treeView.GetCheckedNodes();

            if( storyList.Count == 0 )
            {
                throw new Exception("No selections were made");
                //
            }

            if( string.IsNullOrEmpty(groupBoxExEpic.Value) )
                throw new Exception("Epic.Value missing");

            List<TreeNode> checkedHierarchy = treeView.GetCheckedNodesHierarchy(true);
            model.SaveDataToFile(this.groupBoxExEpic.Value, storyList, checkedHierarchy);
            //
        }


        private void saveStoriesAsCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new();
            dialog.Filter = "Text Files (*.json)|*.json|All Files (*.*)|*.*";
            dialog.FilterIndex = 1; // Sets the default selected filter to "Text Files"
            dialog.RestoreDirectory = true; // Restores the directory to the previously selected one

            if( dialog.ShowDialog() == DialogResult.OK )
            {
                // Get the selected file path
                string filePath = dialog.FileName;
                try
                {
                    SaveData();
                    if( !string.IsNullOrEmpty(groupBoxExEpic.Value) )
                    {
                        bool success = model.SaveUserStoryResultsToCSV(filePath, groupBoxExEpic.Value);
                        if( !success ) MessageBox.Show("Problem with SaveUserStoryResults.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("groupBoxExEpic.Value missing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                List<string>? stories = model.GetStories();
                if( stories != null )
                {
                    StoryListForm form = new(stories);
                    form.ShowDialog();
                }

            }
            catch( Exception ex )
            {
                //throw ex;
            }

        }
    }
}
