# UnitConverter
Universal Unit Converter

An universal unit converter fully configurable.

# Architecture
Base on WPF, this app use the MVVM pattern.

This app load unit and conversion system from json file, for easily add new units in the app.

Just add Json file in Datas folder, in Visual Studio don't forget to activate copy in the output folder in the property of file.

The app deserialize the file, add the name of json file in the menu of application.

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
'BaseUnit' is need to set at true for reference unit 
'Name' is the name of unit
'Sign' is the sign of unit
'Ratio' is the current ratio between base unit and current unit, for exemple if Base Unit is Kilogram, the ratio for gram is 1000
'Add' is for unit base on difference, like Kelvin (Not mandatory)

# To do 
* Error handling
* Add other unit
* Add unit test
