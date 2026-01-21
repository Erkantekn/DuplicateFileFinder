using DuplicateFileFinder.Application.Interfaces;
using DuplicateFileFinder.Infrastructure.FileSystem;
using DuplicateFileFinder.Infrastructure.Hashing;
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
            var fileScanner = new FileScanner();
            IHashService hashService = new XxHashService();
            var form = new FormMain();
            var presenter = new MainPresenter(form, treeBuilder, fileScanner, hashService);

            form.SetPresenter(presenter);
            System.Windows.Forms.Application.Run(form);
        }
    }
}