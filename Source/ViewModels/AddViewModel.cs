using Source.Command;
using Source.Models;
using Source.Navigation;
using Source.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;

using System.Windows.Input;

using System.Windows;

namespace Source.ViewModels;

public class AddViewModel : BaseViewModel
{
    private ICarRepository _carRepository;
    private NavigationStore _navigationStore;

    public Car NewCar { get; set; } = new();

    public ICommand AcceptCommand { get; set; }
    public ICommand CancelCommand { get; set; }



    public AddViewModel(ICarRepository carRepository, NavigationStore navigationStore)
    {
        _carRepository = carRepository;
        _navigationStore = navigationStore;

        AcceptCommand = new RelayCommand(ExecuteAcceptCommand, CanExecuteAcceptCommand);
        CancelCommand = new RelayCommand(ExecuteCancelCommand);
    }

    void ExecuteAcceptCommand(object? parametr)
    {
        MessageBox.Show(NewCar.Model);
        _carRepository.Add(NewCar);
        _navigationStore.CurrentViewModel = new HomeViewModel(_carRepository, _navigationStore);
    }

    bool CanExecuteAcceptCommand(object? parametr)
    {

        if (parametr is UserControl view && view.Content is StackPanel stackPanel)
        {
            foreach (var txt in stackPanel.Children.OfType<TextBox>())
                if (txt.Text.Length < 2)
                    return false;

            return true;
        }

        return false;
    }

    void ExecuteCancelCommand(object? parametr)
    {
        _navigationStore.CurrentViewModel = new HomeViewModel(_carRepository, _navigationStore);
    }

}

