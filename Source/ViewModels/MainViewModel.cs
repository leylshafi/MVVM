using Source.Command;
using Source.Models;
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

public class MainViewModel
{
    private readonly ICarRepository _repository;
    public ObservableCollection<Car> Cars { get; set; }
    public Car? SelectedCar { get; set; }
    public ICommand ShowCommand { get; set; }
    public ICommand AddCommand { get; set; }
    public MainViewModel(ICarRepository repository)
    {
        _repository = repository;
        Cars = new(_repository.GetList() ?? new List<Car>());
        ShowCommand = new RelayCommand(ExecuteShowCommand, CanExecuteShowCommand);
        AddCommand = new RelayCommand(ExecuteAddCommand);
    }
    void ExecuteShowCommand(object? parameter)
    {
        MessageBox.Show(SelectedCar?.Model);
    }
    bool CanExecuteShowCommand(object? parameter)
    => SelectedCar is not null;

    void ExecuteAddCommand(object? parameter)
    {
        Cars.Add(new Car { Id = 4, Make = "New", Model = "New", Year = 2022 });
        MessageBox.Show("Added Successfully","Information",MessageBoxButton.OK,MessageBoxImage.Information);
    }

}
