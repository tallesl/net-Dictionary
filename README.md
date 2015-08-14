# PropertiesHash

[![][build-img]][build]
[![][nuget-img]][nuget]

Makes a dictionary out of the given object properties.

[build]:     https://ci.appveyor.com/project/TallesL/PropertiesHash
[build-img]: https://ci.appveyor.com/api/projects/status/github/tallesl/PropertiesHash

[nuget]:     http://badge.fury.io/nu/PropertiesHash
[nuget-img]: https://badge.fury.io/nu/PropertiesHash.png

## Usage

```cs
using PropertiesHash;

// cmd.Keys are the property names and cmd.Values are their corresponding values
IDictionary<string, object> cmd = PropertiesHasher.Make(someObject);
```
