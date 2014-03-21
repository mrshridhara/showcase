using Showcase.Wpf.Base;
using Showcase.Wpf.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace Showcase.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IDependencyResolver resolver = IocConfig.RegisterAllDependencies();
            Task templateRegistrationTask = DataTemplateConfig.RegisterAllDataTemplatesAsync(resolver, this.Resources);

            Task.WaitAll(templateRegistrationTask);

            var viewModel = resolver.GetInstance<IMainWindowViewModel>();

            this.MainWindow = new MainWindow(viewModel);
            this.MainWindow.Show();
        }
    }
}
