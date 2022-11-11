using Autofac;
using Source.Navigation;
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

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            NavigationStore navigationStore = new();

            var builder = new ContainerBuilder();

            builder.RegisterInstance(navigationStore).SingleInstance();

            builder.RegisterType<FakeCarRepository>().As<ICarRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<HomeViewModel>();


            Container = builder.Build();


            navigationStore.CurrentViewModel = Container.Resolve<HomeViewModel>();

            MainView mainView = new();
            mainView.DataContext = Container.Resolve<MainViewModel>();

            mainView.Show();
        }
}
