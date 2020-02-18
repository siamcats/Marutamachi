using System;
using System.Diagnostics;
using System.Threading.Tasks;
using IccCollection.Core.Models;
using IccCollection.Services;
using Microsoft.EntityFrameworkCore;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Xaml;

namespace IccCollection
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }

            Debug.WriteLine(Windows.Storage.ApplicationData.Current.LocalFolder.Path);

            SQLitePCL.Batteries_V2.Init();
            SQLitePCL.raw.sqlite3_win32_set_directory(/*data directory type*/1, Windows.Storage.ApplicationData.Current.LocalFolder.Path);
            SQLitePCL.raw.sqlite3_win32_set_directory(/*temp directory type*/2, Windows.Storage.ApplicationData.Current.TemporaryFolder.Path);

            using (var db = new BloggingContext())
            {

                //db.Database.EnsureCreated();
                db.Database.Migrate();
            }
        }

        private async Task<bool> IsFileExisted(string fileName)
        {
            var result = await ApplicationData.Current.LocalFolder.TryGetItemAsync(fileName);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.MainPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
    }
}
