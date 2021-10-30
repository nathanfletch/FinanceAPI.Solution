# Finance API

#### An api to provide financial data.


## Technologies Used

* C#
* ASP.NET Core
* Restful Routing Conventions
* Entity Framework Core 
* Swagger

## Setup
<details>
<summary>Setup & Installation Instructions</summary>

#### Installations (if necessary)
* Install C# and .NET using the [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-5.0.100-macos-x64-installer)
* Install [MySql Community Server](https://dev.mysql.com/downloads/file/?id=484914)

#### Setup
* Clone this repository to your local machine
* Navigate to the Template.Solution folder and create a file named "appsettings.json" 
* Add the following code to the file:
  ```
  {
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database=finance_api;uid=root;pwd=[YOUR-PASSWORD-HERE];"
    }
  }
  ```
* Navigate to the Template folder and run the following commands:
* `dotnet restore` to install the necessary dependencies
* `dotnet build` to compile the project.
* `dotnet tool install --global dotnet-ef`
* `dotnet ef migrations add Initial`
* `dotnet ef database update`
* `dotnet run` to start the server.


</details>



## API Documentation

## Swagger
Check out Swagger's auto-generated documentation at `http://localhost:5000/swagger`

## CORS
CORS is enabled only for these specific ports: `5000`, `5001`, `8080`, `8081`.
This is to allow a front end app running on 8080 to call this api.

## Endpoints

Base URL: `http://localhost:5000`

## HTTP Request Structure

```
GET /api/countries
GET /api/countries/load
GET /api/countries/{id}
```

## GET /api/countries

A user can get all countries and customize the list using the following parameters:

| Parameter | region | Description | Example Query |
| :---: | :---: | :---: | --- |
| region | string | Get countries in the specified region. | api/countries/?region="europe" |
| minGDP | double | Gets countries with a higher GDP than the specified GDP | api/countries/?minGDP=10000 |
| maxGDP | double | Gets countries with a lower GDP than the specified date | api/countries/?maxGDP=20000 |
| sortedBy | string | Sorts countries by the following keywords: "name", "GDP", "region", "population"  | api/countries/?sortedBy=GDP |

### Example Query

`http://localhost:5000/api/countries/?minGDP=20000&sorted=true`   

<br>

## Known Issues
* There are no known bugs at this time.
* Please contact me if you find any bugs or have suggestions. 

## Future Plans
* Add more models and tables.

## License

_[MIT](https://opensource.org/licenses/MIT)_  

