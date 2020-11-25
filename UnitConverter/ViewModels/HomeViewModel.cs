using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        private Unit toUnit;
        public Unit ToUnit
        {
            get { return toUnit; }
            set
            {
                SetProperty(ref toUnit, value);
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

        private decimal toValue;
        public decimal ToValue
        {
            get { return toValue; }
            set { SetProperty(ref toValue, value); }
        }

       // public Command ShareCommand { get; set; }

        public HomeViewModel()
        {
            UnitByDomain = GetUnits();
            SelectedUnitList = UnitByDomain.First().Value;
            FromUnit = SelectedUnitList.First();
            ToUnit = SelectedUnitList.First();

        }


        private void Convert()
        {
            if (ToUnit != null && FromUnit != null)
            {
                if (FromUnit.BaseUnit)
                {
                    ToValue = FromValue * ToUnit.Ratio + ToUnit.Add;
                }
                else if (ToUnit.BaseUnit)
                {
                    ToValue = (FromValue - FromUnit.Add) * (1 / FromUnit.Ratio);
                }
                else if (ToUnit == FromUnit)
                {
                    ToValue = FromValue;
                }
                else
                {
                    ToValue = (FromValue - FromUnit.Add) * (1 / FromUnit.Ratio) * ToUnit.Ratio + ToUnit.Add;
                }
            }
        }

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
                            var text = File.ReadAllText(item, Encoding.Latin1);
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
