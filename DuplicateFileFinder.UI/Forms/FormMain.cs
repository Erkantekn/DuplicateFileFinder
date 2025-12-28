using DuplicateFileFinder.Application.Utils;
using DuplicateFileFinder.Domain.Entities;
using DuplicateFileFinder.UI.Presenters;
using DuplicateFileFinder.UI.Views;

namespace DuplicateFileFinder.UI
{
    public partial class FormMain : Form, IMainView
    {
        private MainPresenter _presenter;
        private readonly List<string> _selectedFolders = new();
        public FormMain()
        {
            InitializeComponent();
        }
        private void InitializeListView()
        {
            //lvDuplicates.Columns.Clear();

            //lvDuplicates.Columns.Add("Group", 80);
            //lvDuplicates.Columns.Add("File Path", 520);
            //lvDuplicates.Columns.Add("Size (KB)", 100);
        }
        public void SetPresenter(MainPresenter presenter)
        {
            _presenter = presenter;
        }

        public IReadOnlyList<FolderSelection> GetCheckedFolders()
        {
            var result = new List<FolderSelection>();

            foreach (TreeNode node in tvFolders.Nodes)
                Collect(node, result);

            return result;
        }

        private void Collect(TreeNode node, IList<FolderSelection> result)
        {
            if (node.Checked)
            {
                result.Add(new FolderSelection
                {
                    FullPath = node.Tag.ToString(),
                    ParentChecked = node.Parent?.Checked == true
                });
            }

            foreach (TreeNode child in node.Nodes)
                Collect(child, result);
        }
        public void SetFolderTree(TreeNode rootNode)
        {
            rootNode.Nodes.Clear();
            rootNode.Nodes.Add(rootNode);
            rootNode.ExpandAll();
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            var selectedFolders = _presenter.GetSelectedFolders(tvFolders);

            if (selectedFolders.Count == 0)
            {
                MessageBox.Show("Please select at least one folder.");
                return;
            }

            _presenter.OnStartScan();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitializeListView();

        }

        public void ShowDuplicates(IReadOnlyList<DuplicateGroup> groups)
        {
            tvDuplicates.BeginUpdate();
            tvDuplicates.Nodes.Clear();

            foreach (var group in groups)
            {
                var firstFile = group.Files[0];
                var fileName = Path.GetFileName(firstFile.FullPath);
                var copyCount = group.Files.Count;
                var sizeKb = firstFile.Length / 1024;

                var singleSizeText = FileSizeFormatter.Format(firstFile.Length);
                var totalSizeText = FileSizeFormatter.Format(firstFile.Length * copyCount);

                var rootText =
                    $"{fileName} | {copyCount} copies | {singleSizeText} x {copyCount} = {totalSizeText}";

                var rootNode = new TreeNode(rootText)
                {
                    Tag = group
                };

                foreach (var file in group.Files)
                {
                    var childNode = new TreeNode(file.FullPath)
                    {
                        Tag = file
                    };

                    rootNode.Nodes.Add(childNode);
                }

                tvDuplicates.Nodes.Add(rootNode);
            }

            tvDuplicates.EndUpdate();
        }

        private void btnSelectFolder_Click_1(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select a folder to scan",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = false
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var selectedPath = dialog.SelectedPath;

            if (_selectedFolders.Contains(selectedPath))
                return;

            _selectedFolders.Add(selectedPath);


            AddFolderToTree(selectedPath);
            if (tvFolders.Nodes.Count > 0)
                btnScan.Enabled = true;
        }

        private void AddFolderToTree(string path)
        {
            var rootNode = new TreeNode(path)
            {
                Tag = path,
                Checked = true
            };

            tvFolders.Nodes.Add(rootNode);

            LoadSubDirectories(rootNode);
        }
        private void LoadSubDirectories(TreeNode parentNode)
        {
            try
            {
                var path = parentNode.Tag.ToString();

                var directories = Directory.GetDirectories(path);

                foreach (var dir in directories)
                {
                    var node = new TreeNode(Path.GetFileName(dir))
                    {
                        Tag = dir,
                        Checked = true
                    };

                    parentNode.Nodes.Add(node);
                    LoadSubDirectories(node);
                }
            }
            catch
            {
                // Access denied vb. durumlarý sessizce geç
            }
        }

        public void SetBusy(bool isBusy)
        {
            btnScan.Enabled = !isBusy;
            btnCancel.Enabled = isBusy;
            progressBar.Visible = isBusy;

            if (isBusy)
                progressBar.Value = 0;
        }


        public void UpdateProgress(int percent)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateProgress(percent)));
                return;
            }

            progressBar.Value = Math.Min(100, percent);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.CancelScan();
        }

        private void tvFolders_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown)
                return;

            SetChildCheckedState(e.Node, e.Node.Checked);

            UpdateParentCheckedState(e.Node);
        }
        private void SetChildCheckedState(TreeNode node, bool isChecked)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = isChecked;
                SetChildCheckedState(child, isChecked);
            }
        }
        private void UpdateParentCheckedState(TreeNode node)
        {
            if (node.Parent == null)
                return;

            bool anyChecked = false;

            foreach (TreeNode sibling in node.Parent.Nodes)
            {
                if (sibling.Checked)
                {
                    anyChecked = true;
                    break;
                }
            }

            node.Parent.Checked = anyChecked;

            // Yukarý doðru devam
            UpdateParentCheckedState(node.Parent);
        }
    }
}
