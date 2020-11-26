using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConverter.Helpers;
using UnitConverter.Models;

namespace UnitConverter.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        private Dictionary<string, List<Unit>> unitByDomain;

        public Dictionary<string, List<Unit>> UnitByDomain
        {
            get { return unitByDomain; }
            set { SetProperty(ref unitByDomain, value); }
        }

        private string selectedUnitType;

        public string SelectedUnitType
        {
            get { return selectedUnitType; }
            set { SetProperty(ref selectedUnitType, value); }
        }


        private List<Unit> selectedUnitList;

        public List<Unit> SelectedUnitList
        {
            get { return selectedUnitList; }
            set { SetProperty(ref selectedUnitList, value); }
        }


        private Unit fromUnit;
        public Unit FromUnit
        {
            get { return fromUnit; }
            set
            {
                SetProperty(ref fromUnit, value);
                Convert();
            }
        }

        private decimal fromValue;
        public decimal FromValue
        {
            get { return fromValue; }
            set
            {
                SetProperty(ref fromValue, value);
                Convert();
            }
        }

        public List<MenuItem> MenuItems { get; set; }

        public ObservableCollection<ConvertResult> ConvertResults { get; set; }

        public Command<string> MenuSelectedCommand { get; set; }

        public HomeViewModel()
        {
            MenuSelectedCommand = new Command<string>(ChangeList);
            MenuItems = new List<MenuItem>();
            ConvertResults = new ObservableCollection<ConvertResult>();
            UnitByDomain = GetUnits();
            if (UnitByDomain?.Count > 0)
            {
                MenuItems = UnitByDomain.Keys.Select(k => new MenuItem() { Name = k }).ToList();
                ChangeList(UnitByDomain.First().Key);
            }

        }

        /// <summary>
        /// For change actual unit screen
        /// </summary>
        /// <param name="key">unit choosen</param>
        private void ChangeList(string key)
        {
            MenuItems.ForEach(m => m.IsSelected = m.Name == key);
            SelectedUnitType = key;
            SelectedUnitList = UnitByDomain[key];
            FromUnit = SelectedUnitList.FirstOrDefault();
        }

        /// <summary>
        /// Convert from unit to all other unit
        /// </summary>
        private void Convert()
        {
            ConvertResults.Clear();
            foreach (var item in SelectedUnitList)
            {
                var res = new ConvertResult();
                res.Name = item.ShowName;
                ConvertResults.Add(res);
                if (item != null && FromUnit != null)
                {
                    if (FromUnit.BaseUnit)
                    {
                        res.Result = FromValue * item.Ratio + item.Add;
                    }
                    else if (item.BaseUnit)
                    {
                        res.Result = (FromValue - FromUnit.Add) * (1 / FromUnit.Ratio);
                    }
                    else if (item == FromUnit)
                    {
                        res.Result = FromValue;
                    }
                    else
                    {
                        res.Result = (FromValue - FromUnit.Add) * (1 / FromUnit.Ratio) * item.Ratio + item.Add;
                    }
                }
            }


        }

        /// <summary>
        /// Deserialize json file to get convert unit and data
        /// </summary>
        /// <returns>List of Unit group by Unit type</returns>
        private Dictionary<string, List<Unit>> GetUnits()
        {
            Dictionary<string, List<Unit>> result = new Dictionary<string, List<Unit>>();
            string directory = Directory.GetCurrentDirectory();
            string dataPath = Path.Combine(directory, "Datas");

            if (Directory.Exists(dataPath))
            {
                var jsonFiles = Directory.GetFiles(dataPath, "*.json");
                if (jsonFiles?.Length > 0)
                {
                    foreach (var item in jsonFiles)
                    {
                        try
                        {
                            var text = File.ReadAllText(item, Encoding.UTF8);
                            List<Unit> units = JsonConvert.DeserializeObject<List<Unit>>(text);
                            if (units?.Count > 0)
                            {
                                string key = Path.GetFileNameWithoutExtension(item);
                                result.Add(key, units);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.Write(ex);
                        }
                    }

                }
            }
            return result;

        }
    }
}
