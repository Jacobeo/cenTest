using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Core;
using Core.Models;

namespace UI
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IDataService _dataService;
        private DistrictDetail _districtDetails;
        private District _selectedDistrict;

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            Stores = new ObservableCollection<Store>();
            Salespersons = new ObservableCollection<Salesperson>();
            LoadDistrictsAsync();
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
    }
}
