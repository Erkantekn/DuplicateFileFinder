using DuplicateFileFinder.Infrastructure.FileSystem;
using DuplicateFileFinder.UI.Presenters;

namespace DuplicateFileFinder.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var treeBuilder = new DirectoryTreeBuilder();

            var form = new FormMain();
            var presenter = new MainPresenter(form, treeBuilder);

            form.SetPresenter(presenter);
            System.Windows.Forms.Application.Run(new FormMain());
        }
    }
}