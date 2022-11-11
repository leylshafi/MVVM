using Source.Command;
using Source.Models;
using Source.Navigation;
using Source.Repositories.Abstract;
using Source.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Source.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private readonly ICarRepository _carRepository;
    private readonly NavigationStore _navigationStore;
    public ObservableCollection<Car> Cars { get; set; }

    private Car? _car;

    public Car? SelectedCar
    {
        get { return _car; }
        set
        {
            _car = value;
            OnPropertyChanged(nameof(SelectedCar));
        }
    }

    public ICommand ShowCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand AddCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public HomeViewModel(ICarRepository carRepository, NavigationStore navigationStore)
    {
        _carRepository = carRepository;
        _navigationStore = navigationStore;
        Cars = new(_carRepository.GetList());

        ShowCommand = new RelayCommand(ExecuteShowCommand, CanExecuteCommand);
        AddCommand = new RelayCommand(ExecuteAddCommand);
        EditCommand = new RelayCommand(ExecuteEditCommand, CanExecuteCommand);
        DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteCommand);
    }


    bool CanExecuteCommand(object? parametr) => SelectedCar is not null;

    void ExecuteShowCommand(object? parametr) => MessageBox.Show(SelectedCar?.Make);

    void ExecuteEditCommand(object? parametr)
    {
        EditViewModel editViewModel = new EditViewModel(SelectedCar, _navigationStore, _carRepository);

        EditView editView = new();
        _navigationStore.CurrentViewModel = editViewModel;
    }

    void ExecuteAddCommand(object? parametr)
    {
        AddViewModel addViewModel = new(_carRepository, _navigationStore);

        AddView addView = new();
        addView.DataContext = addViewModel;

        _navigationStore.CurrentViewModel = addViewModel;
    }

    void ExecuteDeleteCommand(object? parametr) => Cars.Remove(SelectedCar!);
}
