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
using System.Windows.Data;
using System.Windows.Input;

namespace Source.ViewModels;

public class EditViewModel : BaseViewModel
{
    private readonly NavigationStore _navigationStore;
    private readonly ICarRepository _carRepository;
    public Car SelectedCar { get; set; }

    public ICommand AcceptCommand { get; set; }
    public ICommand CancelCommand { get; set; }


    public EditViewModel(Car selectedCar, NavigationStore navigationStore, ICarRepository carRepository)
    {
        SelectedCar = selectedCar;
        _navigationStore = navigationStore;
        _carRepository = carRepository;

        AcceptCommand = new RelayCommand(ExecuteAcceptCommand, CanExecuteAcceptCommand);
        CancelCommand = new RelayCommand(ExecuteCancelCommand);
    }

    void ExecuteAcceptCommand(object? parametr)
    {
        if (parametr is UserControl view && view.Content is StackPanel stackPanel)
        {
            foreach (var txt in stackPanel.Children.OfType<TextBox>())
            {
                BindingExpression be = txt.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
            }
        }

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
