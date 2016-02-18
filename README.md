# Dictionary

[![][build-img]][build]
[![][nuget-img]][nuget]

Makes a dictionary out of the given object properties.

[build]:     https://ci.appveyor.com/project/TallesL/net-dictionary
[build-img]: https://ci.appveyor.com/api/projects/status/github/tallesl/net-dictionary?svg=true
[nuget]:     https://www.nuget.org/packages/Dictionary
[nuget-img]: https://badge.fury.io/nu/Dictionary.svg

## Usage

```cs
using DictionaryLibrary;

// dict.Keys are the property names and dict.Values are their corresponding values
IDictionary<string, object> dict = DictionaryMaker.Make(someObject);
```
