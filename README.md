# UnitConverter
Universal Unit Converter

A universal unit converter fully customizable.

# Architecture
Based on WPF, this app uses the MVVM pattern.

This app loads unit and conversion system from json file, in order to add easily new units in the app.

Just add Json file in Datas folder, in Visual Studio don't forget to activate copy in the output folder in the property of file.

The app deserializes the file and adds the name of json file in the menu of application.

# Json File

Is a list of object in json format
```
[
  {
    "BaseUnit": true,
    "Name": "Celsius",
    "Sign": "°C",
    "Ratio": 1
  },
  {
    "BaseUnit": false,
    "Name": "Kelvin",
    "Sign": "K",
    "Ratio": 1,
    "Add": 273.15
  },
  {
    "BaseUnit": false,
    "Name": "Fahrenheit",
    "Sign": "°F",
    "Ratio": 1.8,
    "Add": 32
  }
]
```
For temperature, we have 3 units, the Celsius is the unit base. 
'BaseUnit' is needed to set at true for reference unit 
'Name' is the name of unit
'Sign' is the sign of unit
'Ratio' is the current ratio between base unit and current unit, for exemple if base unit is Kilogram, the ratio for gram is 1000
'Add' is for unit base on difference, like Kelvin (Not mandatory)

# To do 
* Error handling
* Add other unit
* Add unit test
