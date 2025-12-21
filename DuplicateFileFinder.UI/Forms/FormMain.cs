using DuplicateFileFinder.UI.Presenters;
using DuplicateFileFinder.UI.Views;

namespace DuplicateFileFinder.UI
{
    public partial class FormMain : Form, IMainView
    {
        private MainPresenter _presenter;
        public FormMain()
        {
            InitializeComponent();
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
    }
}
