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
    public class EditVehicleWindowViewModel : ObservableRecipient
    {
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public RestCollection<Vehicle> Vehicles { get; set; }
        private Vehicle selectedVehicle;

        public Vehicle SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                if (value != null)
                {
                    selectedVehicle = new Vehicle()
                    {
                        DisplayReg = value.DisplayReg,
                        Registration = value.Registration,
                        Manufacturer = value.Manufacturer,
                        Model = value.Model,
                        Length = value.Length
                    };
                }
                OnPropertyChanged();
                (DeleteVehicleCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreateVehicleCommand { get; set; }
        public ICommand DeleteVehicleCommand { get; set; }
        public ICommand UpdateVehicleCommand { get; set; }

        public EditVehicleWindowViewModel()
        {

            if (!IsInDesignMode)
            {
                Vehicles = new RestCollection<Vehicle>("http://localhost:47322/", "vehicle", "hub");

                CreateVehicleCommand = new RelayCommand(() =>
                {
                    Vehicles.Add(new Vehicle()
                    {
                        Registration = SelectedVehicle.Registration,
                        DisplayReg = SelectedVehicle.DisplayReg,
                        Manufacturer = SelectedVehicle.Manufacturer,
                        Model = SelectedVehicle.Model,
                        Length = SelectedVehicle.Length,
                        RegistrationDate = DateTime.Now
                    });
                });

                UpdateVehicleCommand = new RelayCommand(() =>
                {
                    //NOT WORKING
                    Vehicles.Update(SelectedVehicle);
                });

                DeleteVehicleCommand = new RelayCommand(() =>
                {
                    Vehicles.Delete<string>(SelectedVehicle.Registration);
                },
                () =>
                {
                    return SelectedVehicle != null;
                }
                );
            }
            SelectedVehicle = new Vehicle();
        }
    }
}
