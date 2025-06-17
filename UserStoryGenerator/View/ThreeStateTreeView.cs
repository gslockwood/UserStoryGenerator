using System.Text.Json;
using UserStoryGenerator.Model;
namespace UserStoryGenerator.View
{
    public class TriStateTreeView : System.Windows.Forms.TreeView
    {
        public class TreeNodeEx : TreeNode, Model.IIssue
        {
            public string? Product { get; set; }
            public string? Summary { get; set; }
            public string? IssueType { get; set; }
            public long Key { get; set; }
            public TreeNodeEx(IssueDataBase issue)
            {
                Summary = issue.Summary?.Trim();
                Product = issue.Product?.Trim();
                IssueType = issue.IssueType?.Trim();
                Key = issue.Key;

                // for the TreeView
                Text = Summary;
                Name = issue.Key.ToString();

            }

            public TreeNodeEx() { }
            public TreeNodeEx(string text) : base(text)
            {
                //Text = text;
                Name = text;
            }
            public TreeNodeEx(string text, TreeNode[] children) : base(text, children) { }
        }

        public class TreeNodeExSubTasks(string text) : TreeNodeEx(text) { }

        public class TreeNodeExLinkedIssues(string text) : TreeNodeEx(text) { }


        [Serializable]
        public class DraggableNodeData : Model.IIssue
        {
            public string? Text { get; set; }
            public string? TagJson { get; set; } // Store Tag as JSON string

            // Add any other custom properties from TreeNodeEx you need to transfer                                                 
            //public string CustomProperty { get; set; }

            public string? Product { get; set; }
            public string? Summary { get; set; }
            public string? IssueType { get; set; }
            public long Key { get; set; }


            public List<DraggableNodeData> Children { get; set; } = [];

            public DraggableNodeData() : base()
            {
                Product = string.Empty;
                Summary = string.Empty;
                IssueType = string.Empty;
                Key = -1;
            }


            // Constructor to convert TreeNodeEx to DraggableNodeData
            public DraggableNodeData(TreeNodeEx node)
            {
                Text = node.Text;
                if( node.Tag != null )
                {
                    try
                    {
                        // Attempt to serialize Tag to JSON
                        TagJson = JsonSerializer.Serialize(node.Tag);
                    }
                    catch( Exception ex )
                    {
                        // Handle cases where Tag is not serializable
                        Console.WriteLine($"Warning: Tag '{node.Tag.GetType().Name}' is not JSON serializable. {ex.Message}");
                        TagJson = null; // Or some error indicator
                    }
                }


                // Copy custom properties
                Product = node.Product; // Assuming CustomProperty exists in TreeNodeEx
                Summary = node.Summary;
                IssueType = node.IssueType;
                Key = node.Key;


                foreach( TreeNode childNode in node.Nodes )
                {
                    if( childNode is TreeNodeEx childEx )
                    {
                        Children.Add(new DraggableNodeData(childEx));
                    }
                    // Decide how to handle non-TreeNodeEx children if they exist
                }
            }

            // Method to convert DraggableNodeData back to TreeNodeEx
            public TreeNodeEx? ToTreeNodeEx()
            {
                if( Text == null ) return null;

                TreeNodeEx newNode = new(Text)
                {
                    //newNode.CustomProperty = CustomProperty; // Copy custom properties
                    Product = Product,
                    Summary = Summary,
                    IssueType = IssueType,
                    Key = Key
                };

                if( TagJson != null )
                {
                    try
                    {
                        // Assuming Tag was a simple string or primitive type for demonstration
                        // You might need to know the original type of Tag or use a generic deserialize
                        newNode.Tag = JsonSerializer.Deserialize<string>(TagJson); // Adjust based on your actual Tag type
                    }
                    catch( Exception ex )
                    {
                        Console.WriteLine($"Warning: Could not deserialize Tag JSON: {ex.Message}");
                        newNode.Tag = null;
                    }
                }

                foreach( var childData in Children )
                {
                    TreeNodeEx? child = childData.ToTreeNodeEx();
                    if( child == null ) continue;
                    newNode.Nodes.Add(child);
                }
                return newNode;
            }
        }


        // // // // // // // // // // // // // // // // // // // // 
        // // // // // // // // // // // // // // // // // // // // 
        // // // // // // // // // // // // // // // // // // // // 
        public delegate void CheckEventHandler(IssueCollector issueCollector);
        public event CheckEventHandler? Checked;


        // <remarks>
        // CheckedState is an enum of all allowable nodes states
        // </remarks>
        public enum CheckedState : int { UnInitialised = -1, UnChecked, Checked, Mixed };

        // <remarks>
        // IgnoreClickAction is used to ignore messages generated by setting the node.
        // Checked flag in code
        // Do not set <c>e.Cancel = true</c> in <c>OnBeforeCheck</c> 
        // otherwise the Checked state will be lost
        // </remarks>
        int IgnoreClickAction = 0;
        // <remarks>

        // TriStateStyles is an enum of all allowable tree styles
        // All styles check children when parent is checked
        // Installer automatically checks parent if all children are checked, 
        // and unchecks parent if at least one child is unchecked
        // Standard never changes the checked status of a parent
        // </remarks>
        public enum TriStateStyles : int { Standard = 0, Installer };

        // Create a private member for the tree style, and allow it to be 
        // set on the property sheer
        private TriStateStyles TriStateStyle = TriStateStyles.Standard;

        [System.ComponentModel.Category("Tri-State Tree View")]
        [System.ComponentModel.DisplayName("Style")]
        [System.ComponentModel.Description("Style of the Tri-State Tree View")]
        public TriStateStyles TriStateStyleProperty
        {
            get { return TriStateStyle; }
            set { TriStateStyle = value; }
        }

        //public Dictionary<string, Settings.JiraIssue>? JiraIssueTypes { get; internal set; }

        string? subTaskIssueType = null;
        public Dictionary<string, Settings.JiraIssue> jiraIssueTypes;
        public Dictionary<string, Settings.JiraIssue> JiraIssueTypes
        {
            get { return jiraIssueTypes; }
            internal set
            {
                jiraIssueTypes = value;
                IEnumerable<KeyValuePair<string, Settings.JiraIssue>> any = jiraIssueTypes.Where(type => type.Value.Order == 2);
                if( !any.Any() ) throw new NullReferenceException("subTaskIssueType is missing");
                if( any.Count() > 1 ) throw new NullReferenceException("more than 1 subTaskIssueType");

                subTaskIssueType = any.First().Value.IssueType;

            }
        }

        public TriStateTreeView() : base()
        {
            this.CheckBoxes = true;
            this.DrawMode = TreeViewDrawMode.Normal;
            TriStateStyleProperty = TriStateTreeView.TriStateStyles.Standard;

            ShowNodeToolTips = true;


            ImageList = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(16, 16),
                TransparentColor = Color.Transparent
            };



            StateImageList = new System.Windows.Forms.ImageList();

            // populate the image list, using images from the 
            // System.Windows.Forms.CheckBoxRenderer class
            for( int i = 0; i < 3; i++ )
            {
                // Create a bitmap which holds the relevant check box style
                // see http://msdn.microsoft.com/en-us/library/ms404307.aspx and 
                // http://msdn.microsoft.com/en-us/library/
                // system.windows.forms.checkboxrenderer.aspx

                System.Drawing.Bitmap bmp = new(16, 16);
                System.Drawing.Graphics chkGraphics =
                        System.Drawing.Graphics.FromImage(bmp);
                switch( i )
                {
                    // 0,1 - offset the checkbox slightly so it 
                    // positions in the correct place
                    case 0:
                        System.Windows.Forms.CheckBoxRenderer.DrawCheckBox
                        (chkGraphics, new System.Drawing.Point(0, 1),
                        System.Windows.Forms.VisualStyles.
                        CheckBoxState.UncheckedNormal);
                        break;
                    case 1:
                        System.Windows.Forms.CheckBoxRenderer.DrawCheckBox
                        (chkGraphics, new System.Drawing.Point(0, 1),
                        System.Windows.Forms.VisualStyles.CheckBoxState.
                        CheckedNormal);
                        break;
                    case 2:
                        System.Windows.Forms.CheckBoxRenderer.DrawCheckBox
                        (chkGraphics, new System.Drawing.Point(0, 1),
                        System.Windows.Forms.VisualStyles.
                        CheckBoxState.MixedNormal);
                        break;
                }

                StateImageList.Images.Add(bmp);
            }

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            CheckBoxes = false; // Disable default CheckBox functionality if 
                                // it's been enabled

            // Give every node an initial 'unchecked' image
            IgnoreClickAction++;    // we're making changes to the tree, 
                                    // ignore any other change requests
            UpdateChildState(this.Nodes, (int)CheckedState.UnChecked, false, true);
            IgnoreClickAction--;
        }


        public void SelectAllNodes(bool isChecked)
        {
            foreach( TreeNode node in this.Nodes )
                SelectNodeRecursive(node, isChecked);
        }

        private static void SelectNodeRecursive(TreeNode node, bool isChecked)
        {
            node.Checked = isChecked;
            if( node.Nodes.Count > 0 )
                foreach( TreeNode childNode in node.Nodes )
                    SelectNodeRecursive(childNode, isChecked);
        }


        protected override void OnAfterCheck(System.Windows.Forms.TreeViewEventArgs e)
        {
            base.OnAfterCheck(e);

            if( e.Node == null ) return;

            if( IgnoreClickAction > 0 )
                return;

            IgnoreClickAction++;    // we're making changes to the tree, 
                                    // ignore any other change requests

            // the checked state has already been changed, 
            // we just need to update the state index

            // node is either ticked or unticked. Ignore mixed state, 
            // as the node is still only ticked or unticked regardless of state of children
            System.Windows.Forms.TreeNode tn = e.Node;

            tn.StateImageIndex = tn.Checked ? (int)CheckedState.Checked :
                        (int)CheckedState.UnChecked;
            // force all children to inherit the same state as the current node
            UpdateChildState(e.Node.Nodes, e.Node.StateImageIndex, e.Node.Checked, false);

            // populate state up the tree, possibly resulting in parents with mixed state
            UpdateParentState(e.Node.Parent);

            IgnoreClickAction--;

            //GetCheckedNodeCount(this.Nodes);
            GetAllCheckedNodes();
            //
        }

        public class IssueCollector
        {
            public Dictionary<string, List<TreeNodeEx>> JiraIssuesByType { get; set; } = [];


            public int Total
            {
                get
                {
                    int total = 0;
                    foreach( List<TreeNodeEx> value in JiraIssuesByType.Values )
                        total += value.Count;
                    return total;
                }
            }

            public void AddJiraIssuesByType(TreeNodeEx node)
            {
                if( string.IsNullOrEmpty(node.IssueType) ) return;

                if( !JiraIssuesByType.ContainsKey(node.IssueType) )
                    JiraIssuesByType[node.IssueType] = [];

                JiraIssuesByType[node.IssueType].Add(node);

            }

            public List<TreeNodeEx> GetJiraIssuesByType(string issueType)
            {
                return JiraIssuesByType[issueType];
            }

            public int GetJiraIssuesCountByType(string issueType)
            {
                if( JiraIssuesByType.TryGetValue(issueType, out List<TreeNodeEx>? value) )
                    return value.Count;

                return 0;
            }

        }

        private IssueCollector GetAllCheckedNodes()
        {
            List<TreeNode> checkedHierarchy = GetCheckedNodesHierarchy(true);

            TreeNode dummyParent = new();
            dummyParent.Nodes.AddRange([.. checkedHierarchy]);

            IssueCollector issueCollector = new();
            GetCheckedNodeCount(dummyParent.Nodes, issueCollector);

            //Logger.Info($"issueCollector={issueCollector} Total={issueCollector.Total.ToString()}");

            Checked?.Invoke(issueCollector);

            return issueCollector;
        }

        private static void GetCheckedNodeCount(TreeNodeCollection nodes, IssueCollector issueCollector)
        {
            foreach( TreeNodeEx node in nodes )
            {
                if( !node.Text.Equals("LinkedIssues") && !node.Text.Equals("Subtasks") )
                    issueCollector.AddJiraIssuesByType(node);

                if( node.Nodes != null )
                    GetCheckedNodeCount(node.Nodes, issueCollector);
                //
            }
        }


        //private int GetCheckedNodeCount(TreeNodeCollection nodes, List<TreeNodeEx> storyNodes)
        //{
        //    int count = 0;
        //    foreach( TreeNode node in nodes )
        //    {
        //        if( node.StateImageIndex == (int)CheckedState.Checked || node.StateImageIndex == (int)CheckedState.Mixed )
        //        {
        //            count++;
        //        }
        //        // Recursively call for child nodes
        //        if( node.Nodes.Count > 0 )
        //        {
        //            count += GetCheckedNodeCount(node.Nodes);
        //        }
        //    }

        //    Checked?.Invoke(count);

        //    return count;
        //    //
        //}

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if( e.Button == MouseButtons.Right )
            {
                // Get the TreeNode at the mouse click location
                TreeNode clickedNode = this.GetNodeAt(e.Location);

                if( clickedNode == null ) return;

                ContextMenuStrip nodeContextMenu = new();
                this.ContextMenuStrip = nodeContextMenu;

                ToolStripMenuItem itemExpandChild = new($"Expand all child nodes");
                if( !clickedNode.IsExpanded )
                    nodeContextMenu.Items.Add(itemExpandChild);
                itemExpandChild.Click += (sender, eventArgs) =>
                {
                    clickedNode.ExpandAll();
                    this.TopNode = clickedNode;
                };

                ToolStripMenuItem itemChildCollapse = new($"Collapse all child nodes");
                if( clickedNode.IsExpanded )
                    nodeContextMenu.Items.Add(itemChildCollapse);
                itemChildCollapse.Click += (sender, eventArgs) =>
                {
                    clickedNode.Collapse();
                };

                // Show the context menu at the mouse pointer's location
                nodeContextMenu.Show(this, e.Location);

            }
        }

        protected override void OnAfterExpand(System.Windows.Forms.TreeViewEventArgs e)
        {
            // If any child node is new, give it the same check state as the current node
            // So if current node is ticked, child nodes will also be ticked
            base.OnAfterExpand(e);

            if( e.Node == null ) return;

            IgnoreClickAction++;    // we're making changes to the tree, 
                                    // ignore any other change requests
            UpdateChildState(e.Node.Nodes, e.Node.StateImageIndex, e.Node.Checked, true);
            IgnoreClickAction--;
        }

        protected static void UpdateChildState(System.Windows.Forms.TreeNodeCollection Nodes,
    int StateImageIndex, bool Checked, bool ChangeUninitialisedNodesOnly)
        {
            foreach( System.Windows.Forms.TreeNode tnChild in Nodes )
            {
                if( !ChangeUninitialisedNodesOnly || tnChild.StateImageIndex == -1 )
                {
                    tnChild.StateImageIndex = StateImageIndex;
                    tnChild.Checked = Checked;  // override 'checked' state
                                                // of child with that of parent

                    if( tnChild.Nodes.Count > 0 )
                    {
                        UpdateChildState(tnChild.Nodes, StateImageIndex,
                        Checked, ChangeUninitialisedNodesOnly);
                    }
                }
            }
        }
        protected void UpdateParentState(System.Windows.Forms.TreeNode tn)
        {
            // Node needs to check all of it's children to see if any of them 
            // are ticked or mixed
            if( tn == null )
                return;

            int OrigStateImageIndex = tn.StateImageIndex;

            int UnCheckedNodes = 0, CheckedNodes = 0, MixedNodes = 0;

            // The parent needs to know how many of it's children are Checked or Mixed
            foreach( System.Windows.Forms.TreeNode tnChild in tn.Nodes )
            {
                if( tnChild.StateImageIndex == (int)CheckedState.Checked )
                    CheckedNodes++;
                else if( tnChild.StateImageIndex == (int)CheckedState.Mixed )
                {
                    MixedNodes++;
                    break;
                }
                else
                    UnCheckedNodes++;
            }

            if( TriStateStyle == TriStateStyles.Installer )
            {
                // In Installer mode, if all child nodes are checked 
                // then parent is checked
                // If at least one child is unchecked, then parent is unchecked
                if( MixedNodes == 0 )
                {
                    if( UnCheckedNodes == 0 )
                    {
                        // all children are checked, 
                        // so parent must be checked
                        tn.Checked = true;
                    }
                    else
                    {
                        // at least one child is unchecked, 
                        // so parent must be unchecked
                        tn.Checked = false;
                    }
                }
            }
            else
            {
            }

            // Determine the parent's new Image State
            if( MixedNodes > 0 )
            {
                // at least one child is mixed, so parent must be mixed
                tn.StateImageIndex = (int)CheckedState.Mixed;
            }
            else if( CheckedNodes > 0 && UnCheckedNodes == 0 )
            {
                //// all children are checked
                //if( tn.Checked )
                //    tn.StateImageIndex = (int)CheckedState.Checked;
                //else
                //    tn.StateImageIndex = (int)CheckedState.Mixed;

                tn.StateImageIndex = (int)CheckedState.Checked;

            }
            else if( CheckedNodes > 0 )
            {
                // some children are checked, the rest are unchecked
                tn.StateImageIndex = (int)CheckedState.Mixed;
            }
            else
            {
                //// all children are unchecked
                //if( tn.Checked )
                //    tn.StateImageIndex = (int)CheckedState.Mixed;
                //else
                //    tn.StateImageIndex = (int)CheckedState.UnChecked;

                tn.StateImageIndex = (int)CheckedState.UnChecked;
            }

            if( OrigStateImageIndex != tn.StateImageIndex && tn.Parent != null )
            {
                // Parent's state has changed, notify the parent's parent
                UpdateParentState(tn.Parent);
            }

            //Logger.Info($"UnCheckedNodes={UnCheckedNodes} CheckedNodes={CheckedNodes} MixedNodes={MixedNodes}");

        }

        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // is the keypress a space?  If not, discard it
            if( e.KeyCode == System.Windows.Forms.Keys.Space )
            {
                // toggle the node's checked status.  
                // This will then fire OnAfterCheck
                SelectedNode.Checked = !SelectedNode.Checked;
            }
        }

        protected override void OnNodeMouseClick(System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            // is the click on the checkbox?  If not, discard it
            System.Windows.Forms.TreeViewHitTestInfo info = HitTest(e.X, e.Y);
            if( info == null || info.Location !=
                System.Windows.Forms.TreeViewHitTestLocations.StateImage )
            {
                return;
            }

            // toggle the node's checked status.  This will then fire OnAfterCheck
            System.Windows.Forms.TreeNode tn = e.Node;
            tn.Checked = !tn.Checked;
        }

        /// <summary>
        /// Builds a new hierarchical list of all checked nodes within the TreeView.
        /// The returned list contains only the checked nodes, preserving their
        /// parent-child relationships from the original TreeView.
        /// </summary>
        /// <returns>A List of TreeNode objects representing the top-level checked nodes
        /// and their checked descendants, maintaining their original hierarchy.</returns>
        public List<TreeNode> GetCheckedNodesHierarchy(bool full)
        {
            // Start the recursive traversal from the top-level nodes of the original TreeView.
            // The helper method will build and return the new hierarchical structure of checked nodes.
            return GetCheckedNodesRecursiveHierarchy(this.Nodes[0].Nodes, full);// skipping the root
        }

        /// <summary>
        /// Recursively traverses a source TreeNodeCollection to find checked nodes
        /// and builds a new hierarchical structure of these nodes in a target
        /// TreeNodeCollection.
        /// </summary>
        /// <param name="sourceNodes">The TreeNodeCollection from the original TreeView to traverse.</param>
        /// <param name="targetNodes">The TreeNodeCollection where the new hierarchical checked nodes will be added.</param>
        private static void BuildCheckedNodesHierarchyRecursive(TreeNodeCollection sourceNodes, TreeNodeCollection targetNodes)
        {
            foreach( TreeNode sourceNode in sourceNodes )
            {
                // If the current source node is checked, create a new node for it
                // and add it to the target collection.
                if( sourceNode.Checked )
                {
                    // Create a new TreeNode. We only copy essential properties like Text.
                    // Other properties (e.g., Tag, ImageIndex) could be copied if needed.
                    TreeNode newCheckedNode = new(sourceNode.Text)
                    {
                        // Optionally copy other properties if desired
                        Tag = sourceNode.Tag,
                        ImageIndex = sourceNode.ImageIndex,
                        SelectedImageIndex = sourceNode.SelectedImageIndex
                    };

                    TreeNodeEx treeNodeEx = (TreeNodeEx)sourceNode;

                    targetNodes.Add(treeNodeEx);
                    //targetNodes.Add(newCheckedNode);

                    // Recursively call the method for the children of the *source* node,
                    // but add the resulting checked children to the *newly created* node's children collection.
                    if( sourceNode.Nodes.Count > 0 )
                    {
                        BuildCheckedNodesHierarchyRecursive(sourceNode.Nodes, newCheckedNode.Nodes);
                    }
                }
                else
                {
                    // If the current node is not checked, but it might have checked children,
                    // we still need to traverse its children.
                    // This ensures that if a parent is unchecked but a child is checked,
                    // the child (and its checked descendants) will still appear in the hierarchy,
                    // but under a newly created parent node representing the path.
                    // To strictly retain hierarchy *only for checked parents*, this 'else' block
                    // would be removed, and the recursive call would only happen if the parent is checked.
                    // For this request, we assume "retain hierarchy" means if a child is checked,
                    // its path up to the root should be represented, even if intermediate parents are unchecked.
                    // If the intent is *only* to include checked nodes and their *checked* parents,
                    // the logic would need to be adjusted to create parent nodes on the fly
                    // only when a descendant is found to be checked.

                    // For now, let's assume the user wants the *path* to a checked node.
                    // If the parent is not checked, we still need to check its children.
                    // However, we don't add the *unchecked* parent to the targetNodes directly.
                    // Instead, we check if any *descendants* are checked. If so, we'd need
                    // to construct the parent path.

                    // A simpler interpretation of "retain hierarchy" for *only* checked nodes:
                    // If a node is checked, it's added. Then its children are processed.
                    // This means if a parent is unchecked, its checked children will appear
                    // as top-level nodes in the result, losing their parent context.
                    // To truly retain hierarchy, we need to create a parent node if any of its
                    // *descendants* are checked, even if the parent itself is not.

                    // Let's refine: The request implies that if a node is checked, its children (if also checked)
                    // should appear under it. If a parent is *not* checked, but its child *is* checked,
                    // the child should appear as a top-level node in the result.
                    // The previous `GetCheckedNodes` flattened the list.
                    // To retain hierarchy, we need to create a new tree structure.

                    // Let's go with the interpretation that if a node is checked, it's added,
                    // and then its *checked children* are added as its children in the *new* hierarchy.
                    // If a node is *not* checked, we simply continue traversing its children
                    // to see if any of them are checked. If they are, they will be added
                    // to the *current* targetNodes (which would be the root of the result or a checked parent's children).

                    // Re-evaluating: The most natural interpretation of "retain hierarchy" for checked nodes
                    // is that if a node is checked, it appears. If its child is also checked, the child
                    // appears as a child of the *newly created* parent node. If a parent is *not* checked,
                    // but its child *is* checked, then the child should appear at the same level
                    // as its *unchecked* parent would have appeared if it were checked.
                    // This means we only create new nodes for *checked* items.

                    // The current logic within the 'if (sourceNode.Checked)' block correctly handles
                    // adding the checked node and then recursively building its checked children.
                    // The 'else' block is not strictly necessary for this interpretation,
                    // as we only care about adding nodes that are *themselves* checked.
                    // If we want to include unchecked parents as "path nodes" to checked children,
                    // that would require more complex logic (e.g., checking if any descendant is checked).

                    // Let's stick to the simpler, more direct interpretation:
                    // Only checked nodes are added to the new hierarchy.
                    // If a checked node has checked children, those children become its children
                    // in the new hierarchy. If an unchecked node has checked children, those
                    // children will appear at the same level as the unchecked parent.

                    // The current structure of the 'if (sourceNode.Checked)' block and the recursive call
                    // within it already achieves this. The 'else' block is not needed to achieve
                    // this specific "retain hierarchy" behavior for *only* checked nodes.
                    // We just need to ensure the recursive call continues even if the parent isn't checked.

                    // Corrected logic for retaining hierarchy:
                    // If a node is checked, add it to the current targetNodes and then recursively process its children
                    // to add them to the *newly added node's* children.
                    // If a node is NOT checked, we still need to traverse its children, but those children
                    // (if checked) will be added to the *same level* as the current unchecked node.
                    // This means the recursive call should always happen, but the 'targetNodes' parameter
                    // will depend on whether the current 'sourceNode' was checked.

                    // Let's simplify the recursive method signature and logic for clarity:
                    // It should return a list of nodes that are checked within its subtree.
                    // This will allow for building the hierarchy more naturally.

                    // Let's revert to the original GetCheckedNodesRecursive name and modify its return type.
                    // This is a more common pattern for building a filtered, hierarchical view.

                    // The original `GetCheckedNodesRecursive` worked by modifying a passed list.
                    // To retain hierarchy, we need to build a *new* tree structure.

                    // The current `BuildCheckedNodesHierarchyRecursive` approach is correct for
                    // building a new hierarchical structure of *only* checked nodes.
                    // If a `sourceNode` is NOT checked, we still need to check its children,
                    // but those children (if checked) should be added to the *same level* as the
                    // `sourceNode` would have been if it were checked.

                    // So, the recursive call should happen regardless of whether the `sourceNode` is checked.
                    // The `targetNodes` parameter for the recursive call should be `newCheckedNode.Nodes`
                    // if `sourceNode` was checked, and `targetNodes` (the same collection) if `sourceNode` was not checked.

                    // This requires a slight change in the recursive call for the 'else' case.

                    // Let's refine `BuildCheckedNodesHierarchyRecursive` to be more robust.
                    // If a parent is unchecked but has checked children, those children should
                    // appear as top-level items in the result, or as children of the immediate
                    // checked ancestor.

                    // The current implementation of `BuildCheckedNodesHierarchyRecursive` (within the `if (sourceNode.Checked)` block)
                    // correctly adds a checked node and then builds its checked children *under* it.
                    // If a `sourceNode` is *not* checked, we still need to traverse its children.
                    // Any checked children of an *unchecked* parent should be added to the *same level*
                    // in the `targetNodes` collection as the unchecked parent.

                    // So, the recursive call should always happen on `sourceNode.Nodes`.
                    // The `targetNodes` for the recursive call should be `newCheckedNode.Nodes` if `sourceNode` was checked,
                    // otherwise it should be the same `targetNodes` passed to the current call.

                    // This implies the `BuildCheckedNodesHierarchyRecursive` should always be called for children.
                    // Let's make the `BuildCheckedNodesHierarchyRecursive` return a `List<TreeNode>`
                    // which represents the checked children of the current `sourceNode`.

                    // Re-thinking the recursive approach:
                    // A recursive function that returns a list of its *checked children* is often cleaner.

                    // Let's modify `GetCheckedNodesHierarchy` to call a recursive helper that returns a `List<TreeNode>`.
                    // This helper will be responsible for building the new hierarchy.

                    // New approach for `GetCheckedNodesHierarchy`:
                    // It will call a recursive helper that processes each node.
                    // If a node is checked, a new `TreeNode` is created for it.
                    // Then, its children are processed recursively. The results of these recursive calls
                    // are added as children to the *newly created* node.
                    // If a node is NOT checked, its children are still processed recursively,
                    // and the results are returned to the caller, effectively promoting them
                    // to the level of the unchecked parent.

                    // This is the most common way to "retain hierarchy" for a filtered view.

                    // Let's rename the recursive method for clarity.
                }
            }
        }

        /// <summary>
        /// Helper method to recursively build the hierarchical list of checked nodes.
        /// </summary>
        /// <param name="sourceNodes">The collection of nodes from the original TreeView to process.</param>
        /// <returns>A List of TreeNode objects representing the checked nodes and their
        /// checked descendants from the given source collection, maintaining hierarchy.</returns>
        private static List<TreeNode> GetCheckedNodesRecursiveHierarchy(TreeNodeCollection sourceNodes, bool full = false)
        {
            List<TreeNode> resultNodes = [];

            foreach( TreeNode sourceNode in sourceNodes )
            {
                // Recursively get the relevant children first.
                // This list will contain children that are checked or have checked descendants.
                List<TreeNode> relevantChildren = GetCheckedNodesRecursiveHierarchy(sourceNode.Nodes, full);

                // A node is included in the result if:
                // 1. It is checked itself, OR
                // 2. It has any relevant (checked or path-to-checked) children.
                //if( sourceNode.Checked || relevantChildren.Count != 0 )
                if( sourceNode.StateImageIndex == (int)TriStateTreeView.CheckedState.Checked || sourceNode.StateImageIndex == (int)TriStateTreeView.CheckedState.Mixed || relevantChildren.Count != 0 )
                {
                    //// Create a new TreeNode for the current sourceNode.
                    //// We copy its text and other relevant properties.
                    //TreeNode newTreeNode = new(sourceNode.Text)
                    //{
                    //    Tag = sourceNode.Tag, // Copy Tag property
                    //    ImageIndex = sourceNode.ImageIndex,
                    //    SelectedImageIndex = sourceNode.SelectedImageIndex
                    //};

                    //// Add the relevant children (obtained from the recursive call)
                    //// to the children collection of this new node.
                    //foreach( TreeNode child in relevantChildren )
                    //{
                    //    newTreeNode.Nodes.Add(child);
                    //}

                    //if( sourceNode is TreeNodeExSubTasks ) continue;

                    TreeNodeEx treeNodeEx = (TreeNodeEx)sourceNode;
                    if( full )
                    {
                        TreeNodeEx treeNodeExRef = (TreeNodeEx)sourceNode;
                        treeNodeEx = new()
                        {
                            Summary = treeNodeExRef.Summary,
                            IssueType = treeNodeExRef.IssueType,
                            Product = treeNodeExRef.Product,
                            Text = treeNodeExRef.Text
                        };

                        foreach( TreeNode child in relevantChildren )
                        {
                            treeNodeEx.Nodes.Add(child);
                        }

                    }

                    resultNodes.Add(treeNodeEx); // Add the newly created node to the result list
                    //resultNodes.Add(newTreeNode); // Add the newly created node to the result list
                }
                // If the node is not checked and has no checked descendants, it is skipped.
            }
            return resultNodes;

            //List<TreeNode> resultNodes = new List<TreeNode>();

            //foreach( TreeNode sourceNode in sourceNodes )
            //{
            //    if( sourceNode.Checked )
            //    {
            //        // If the current node is checked, create a new TreeNode for it.
            //        TreeNode newCheckedNode = new TreeNode(sourceNode.Text);
            //        newCheckedNode.Tag = sourceNode.Tag; // Copy Tag property
            //        newCheckedNode.ImageIndex = sourceNode.ImageIndex;
            //        newCheckedNode.SelectedImageIndex = sourceNode.SelectedImageIndex;

            //        // Recursively get the checked children of the sourceNode
            //        // and add them to the newCheckedNode's children collection.
            //        List<TreeNode> checkedChildren = GetCheckedNodesRecursiveHierarchy(sourceNode.Nodes);
            //        foreach( TreeNode child in checkedChildren )
            //        {
            //            newCheckedNode.Nodes.Add(child);
            //        }

            //        resultNodes.Add(newCheckedNode); // Add the newly created checked node to the result
            //    }
            //    else
            //    {
            //        // If the current node is NOT checked, but it might have checked children,
            //        // we still need to process its children. The checked children will be
            //        // added to the 'resultNodes' of the *current* level, effectively
            //        // promoting them up the hierarchy if their direct parent is unchecked.
            //        List<TreeNode> checkedChildren = GetCheckedNodesRecursiveHierarchy(sourceNode.Nodes);
            //        foreach( TreeNode child in checkedChildren )
            //        {
            //            resultNodes.Add(child);
            //        }
            //    }
            //}
            //return resultNodes;
        }

        public List<string> GetStorySummaries()
        {
            List<string> strings = [];
            List<TreeNode> checkedNodes = GetCheckedNodesRecursiveHierarchy(this.Nodes[0].Nodes);// skipp the root
            foreach( TreeNode node in checkedNodes )
            {
                strings.Add(node.Text);
                if( node.Nodes.Count > 0 )
                {
                    GetCheckedNodesRecursive(node.Nodes, strings);
                }
            }
            return strings;
        }
        private void GetCheckedNodesRecursive(TreeNodeCollection nodes, List<string> strings)
        {
            foreach( TreeNode node in nodes )
            {
                if( node == null ) continue;
                if( !node.Checked ) continue;

                // skip to exclude these nodes' text
                if( node is TreeNodeExSubTasks || node is TreeNodeExLinkedIssues ) continue;

                var issueType = ( (TreeNodeEx)node ).IssueType;
                if( issueType == null ) continue;

                if( node.Nodes.Count > 0 )
                    GetCheckedNodesRecursive(node.Nodes, strings);
            }

        }

        public static void RemovePreviousCreatedNodesIfTagged(TreeNodeCollection nodes)
        {
            foreach( TreeNode node in nodes )
            {
                if( node == null || node.Tag == null ) continue;
                if( node.Tag is bool v )
                    if( v )
                        nodes.Remove(node);
            }
        }

        public static void ResetTreeViewNodeColors(TreeNodeCollection nodes)
        {
            foreach( TreeNode node in nodes )
                ResetNodeColorsRecursive(node);
        }
        private static void ResetNodeColorsRecursive(TreeNode node)
        {
            // Reset the BackColor and ForeColor to their default values
            // The default values for TreeView nodes are usually SystemColors.Window for BackColor
            // and SystemColors.WindowText for ForeColor, but it's safer to just null them
            // or set to default if you've explicitly set them.
            // If you've only set them on selected nodes, and want to revert to the default
            // TreeView appearance, setting them to Color.Empty or their default
            // system colors is the way to go.
            node.BackColor = SystemColors.Window; // Or Color.Empty to let the TreeView control it
            node.ForeColor = SystemColors.WindowText; // Or Color.Empty

            // Recursively call for all child nodes
            foreach( TreeNode childNode in node.Nodes )
                ResetNodeColorsRecursive(childNode);
        }


    }

}