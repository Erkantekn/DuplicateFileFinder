using DuplicateFileFinder.Infrastructure.FileSystem;
using DuplicateFileFinder.UI.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace DuplicateFileFinder.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddSingleton<DirectoryTreeBuilder>();
            services.AddSingleton<FolderSelectionViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();
        }
    }

}
