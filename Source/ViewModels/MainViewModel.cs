using Source.Command;
using Source.Models;
using Source.Navigation;
using Source.Repositories.Abstract;
using Source.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Source.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly NavigationStore _navigationStore;

    public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

    public MainViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        navigationStore.CurrentViewModelChanged += NavigationStore_CurrentViewModelChanged;
    }

    private void NavigationStore_CurrentViewModelChanged() => OnPropertyChanged(nameof(CurrentViewModel));
}