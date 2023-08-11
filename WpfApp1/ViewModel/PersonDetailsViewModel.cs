using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WpfApp1.Model;
using WpfApp1.Services;

namespace WpfApp1.ViewModel
{
    public interface IPersonDetailsViewModel
    {
        void FetchAllData();        

        List<string> SortComboItems { get; set; }

        ObservableCollection<PersonDataFilter> PersonCountryFilter { get; set; }

        ICommand FilterDrownDownClosedCommand { get; set; }

        ICollectionView PersonDetailsViewSource { get; set; }
    }

    public class PersonDetailsViewModel : PropertyChangedBase, IPersonDetailsViewModel
    {
        IPersonDetailsService _personDetailsService;
        List<PersonDetails> _lstPersonDetails = new List<PersonDetails>();
        private ObservableCollection<PersonDataFilter> _personCountryFilter;
        ICollectionView _colletionBoundToUI;
        List<string> _selectedFilters;
        string _selectedSortDescription;

        public PersonDetailsViewModel(IPersonDetailsService personDetailsService)
        {
            _personDetailsService = personDetailsService;
            SortComboItems = new List<string>() { "Sort by Name,Country", "Sort by Name", "Sort by Country" };
            FilterDrownDownClosedCommand = new RelayCommand(OnFilterData, CanExecute);
            FetchAllData();
        }

        private bool CanExecute()
        {
            return true;
        }

        private void OnFilterData()
        {
            ApplyFilters();
        }

        public string SelectedSortDescription
        {
            get { return _selectedSortDescription; }
            set
            {
                _selectedSortDescription = value;
                OnPropertyChanged();
                ApplySortDescription();
            }
        }

        public List<string> SelectedFilters
        {
            get { return _selectedFilters; }
            set
            {
                _selectedFilters = value;
                OnPropertyChanged();
            }
        }
        
        public List<string> SortComboItems 
        { 
            get; 
            set; 
        }
        public ObservableCollection<PersonDataFilter> PersonCountryFilter 
        {
            get
            {
                return _personCountryFilter;
            }
            set
            {
                _personCountryFilter = value;
                OnPropertyChanged();
            }
        }

        public ICommand FilterDrownDownClosedCommand 
        { 
            get; 
            set; 
        }

        public ICollectionView PersonDetailsViewSource
        {
            get
            {
                return _colletionBoundToUI;
            }
            set
            {
                SetProperty(ref _colletionBoundToUI, value); ;
            }
        }

        public void FetchAllData()
        {
            try
            {
                Task.Run(() =>
                {
                    var result = _personDetailsService.GetAllPersonDetails();

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _lstPersonDetails = result;
                        var distinctCountry = _lstPersonDetails.Select(x => x.country).Distinct().ToList();
                        InitializeFilters(distinctCountry);
                        var items = CollectionViewSource.GetDefaultView(result);
                        PersonDetailsViewSource = items;
                        ApplyFilters();

                    });
                });
            }
            catch(Exception ex)
            {
                //logger log exception
            }
            
        }


        #region Sorting & Filtering

        private void InitializeFilters(List<string> countries)
        {
            try
            {
                PersonCountryFilter = new ObservableCollection<PersonDataFilter>();
                foreach (var country in countries)
                {
                    PersonCountryFilter.Add(new PersonDataFilter() { Country = country, IsSelected = true });
                }
            }
            catch(Exception ex)
            {
                //log exception
            }           

        }

        private void ApplyFilters()
        {
            try
            {
                SelectedFilters = PersonCountryFilter.Where(f => f.IsSelected).Select(f => f.Country).ToList();
                PersonDetailsViewSource.Filter = null;
                PersonDetailsViewSource.Filter += (o) =>
                {
                    var model = o as PersonDetails;
                    if (model != null)
                    {
                        if (_selectedFilters.Contains(model.country))
                        {
                            return true;
                        }
                    }
                    return false;

                };

                PersonDetailsViewSource.Refresh();
            }
            catch(Exception ex)
            {
                //TO DO Log exception with logger
            }

            
        }

        private void ApplySortDescription()
        {
            try
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(PersonDetailsViewSource);
                view.SortDescriptions.Clear();

                if (SelectedSortDescription == "Sort by Name")
                {
                    view.SortDescriptions.Add(new SortDescription("name", ListSortDirection.Ascending));

                }
                else if (SelectedSortDescription == "Sort by Country")
                {
                    view.SortDescriptions.Add(new SortDescription("country", ListSortDirection.Ascending));
                }
                else
                {
                    view.SortDescriptions.Add(new SortDescription("name", ListSortDirection.Ascending));
                    view.SortDescriptions.Add(new SortDescription("country", ListSortDirection.Ascending));
                }

                view.Refresh();
            }
            catch (Exception ex)
            {
                //TO DO Log exception with logger
            }

        }

        #endregion


    }
}
