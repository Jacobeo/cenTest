using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Core;
using Core.Models;

namespace UI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IDataService _dataService;
        private DistrictDetail _districtDetails;
        private District _selectedDistrict;
        private Salesperson _selectedSalespersonToAdd;

        public ICommand AddSalespersonCommand { get; }

        public ObservableCollection<Salesperson> AllSalespersons { get; } = new();

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            AddSalespersonCommand = new RelayCommand(AddSalesperson, CanAddSalesperson);

            Stores = new ObservableCollection<Store>();
            Salespersons = new ObservableCollection<Salesperson>();
            LoadDistrictsAsync();
            LoadAllSalespersons();
        }

        public ObservableCollection<District> Districts { get; } = new();
        public ObservableCollection<Store> Stores { get; set; }
        public ObservableCollection<Salesperson> Salespersons { get; set; }

        public District SelectedDistrict
        {
            get => _selectedDistrict;
            set
            {
                if (_selectedDistrict != value)
                {
                    _selectedDistrict = value;
                    OnPropertyChanged();
                    LoadDistrictDetailsAsync();
                }
            }
        }

        public DistrictDetail DistrictDetails
        {
            get => _districtDetails;
            set
            {
                if (_districtDetails != value)
                {
                    _districtDetails = value;
                    OnPropertyChanged();
                }
            }
        }

        public Salesperson SelectedSalespersonToAdd
        {
            get => _selectedSalespersonToAdd;
            set
            {
                if (_selectedSalespersonToAdd != value)
                {
                    _selectedSalespersonToAdd = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void LoadDistrictsAsync()
        {
            var districts = await _dataService.GetDistrictsAsync();
            foreach (var district in districts)
            {
                Districts.Add(district);
            }
        }

        private async void LoadDistrictDetailsAsync()
        {
            if (SelectedDistrict != null)
            {
                DistrictDetails = await _dataService.GetDistrictDetailsAsync(SelectedDistrict.Id);
                Stores.Clear();
                Salespersons.Clear();

                foreach (var store in DistrictDetails.Stores)
                {
                    Stores.Add(store);
                }

                foreach (var salesperson in DistrictDetails.Salespersons)
                {
                    Salespersons.Add(salesperson);
                }
            }
        }

        private void AddSalesperson()
        {
            if (SelectedDistrict != null && SelectedSalespersonToAdd != null)
            {
                _dataService.AddSalesPersonToDistrict(SelectedDistrict.Id, SelectedSalespersonToAdd.Id, SelectedSalespersonToAdd.IsPrimaryBool);

                // Update the UI.
                if(!Salespersons.Contains(SelectedSalespersonToAdd))
                {
                Salespersons.Add(SelectedSalespersonToAdd);
                }
            }
        }

        private bool CanAddSalesperson()
        {
            return SelectedDistrict != null && SelectedSalespersonToAdd != null;
        }

        private async void LoadAllSalespersons()
        {
            var allSalespersons = await _dataService.GetAllSalesPersons();

            foreach (var salesperson in allSalespersons)
            {
                AllSalespersons.Add(salesperson);
            }
        }
    }
}
