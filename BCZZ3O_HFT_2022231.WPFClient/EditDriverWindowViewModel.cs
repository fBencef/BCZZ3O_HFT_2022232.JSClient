using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using VehicleFleetDb.Models;

namespace BCZZ3O_HFT_2022231.WPFClient
{
    public class EditDriverWindowViewModel : ObservableRecipient
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public RestCollection<Driver> Drivers { get; set; }
        private Driver selectedDriver;

        public Driver SelectedDriver
        {
            get { return selectedDriver; }
            set
            {
                if (value != null)
                {
                    selectedDriver = new Driver()
                    {
                        DriverId = value.DriverId,
                        Name = value.Name,
                        Age = value.Age,
                        Shifts = value.Shifts
                    };
                }
                OnPropertyChanged();
                (DeleteDriverCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreateDriverCommand { get; set; }
        public ICommand DeleteDriverCommand { get; set; }
        public ICommand UpdateDriverCommand { get; set; }

        public EditDriverWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                Drivers = new RestCollection<Driver>("http://localhost:47322/", "driver", "hub");

                CreateDriverCommand = new RelayCommand(() =>
                {
                    Drivers.Add(new Driver()
                    {
                        //DriverId = SelectedDriver.DriverId,
                        Name = SelectedDriver.Name,
                        Age = SelectedDriver.Age,
                        Shifts = SelectedDriver.Shifts
                    });
                });

                UpdateDriverCommand = new RelayCommand(() =>
                {
                    //NOT WORKING
                    Drivers.Update(SelectedDriver);
                });

                DeleteDriverCommand = new RelayCommand(() =>
                {
                    Drivers.Delete<int>(SelectedDriver.DriverId);
                },
                () =>
                {
                    return SelectedDriver != null;
                }
                );
            }
            SelectedDriver = new Driver();
        }
    }
}
