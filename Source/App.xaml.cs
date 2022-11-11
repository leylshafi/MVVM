using Autofac;
using Source.Repositories.Abstract;
using Source.Repositories.Concrete;
using Source.ViewModels;
using Source.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Source
{
    public partial class App : Application
    {
        public static IContainer? Container { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<MainViewModel>().AsSelf();
            containerBuilder.RegisterType<FakeCarRepository>().As<ICarRepository>();
            Container= containerBuilder.Build();
        }
        void ApplicationStartup(object sender, StartupEventArgs e)
        {
            ICarRepository _carRepository = new FakeCarRepository();
            MainViewModel mainViewModel = new(_carRepository);
            MainView mainView =new();
            mainView.DataContext = mainViewModel;
            mainView.ShowDialog();
        }
    }
}
