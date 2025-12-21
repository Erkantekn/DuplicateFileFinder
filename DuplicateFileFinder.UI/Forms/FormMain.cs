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
            lvDuplicates.Columns.Clear();

            lvDuplicates.Columns.Add("Group", 80);
            lvDuplicates.Columns.Add("File Path", 520);
            lvDuplicates.Columns.Add("Size (KB)", 100);
        }
        public void SetPresenter(MainPresenter presenter)
        {
            _presenter = presenter;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            _presenter.OnSelectRootFolder();
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

            _presenter.OnStartScan(selectedFolders);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitializeListView();

        }

        public void ShowDuplicates(IReadOnlyList<DuplicateGroup> groups)
        {
            lvDuplicates.BeginUpdate();
            lvDuplicates.Items.Clear();

            int groupIndex = 1;

            foreach (var group in groups)
            {
                foreach (var file in group.Files)
                {
                    var item = new ListViewItem(groupIndex.ToString());
                    item.SubItems.Add(file.FullPath);
                    item.SubItems.Add((file.Length / 1024).ToString("N0"));

                    lvDuplicates.Items.Add(item);
                }

                groupIndex++;
            }

            lvDuplicates.EndUpdate();
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
    }
}
