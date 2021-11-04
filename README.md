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
* Navigate to the FinanceAPI.Solution folder and create a file named "appsettings.json" 
* Add the following code to the file:
  ```
  {
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database=finance_api;uid=root;pwd=[YOUR-PASSWORD-HERE];"
    }
  }
  ```
* Navigate to the FinanceAPI folder and run the following commands:
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

## Base URL: `http://localhost:5000`

## Countries Data: /api/countries
These endpoints will return data from this [CSV](https://www.kaggle.com/fernandol/countries-of-the-world)
Only the following properties are parsed: Country name (Name), Region, Population, and GDP. 


`GET /api/countries/load` This must be called first to load the csv.

`GET /api/countries/{id}` Returns a single country by id.

`GET /api/countries` Returns a list of countries with the following optional parameters:

| Parameter | region | Description | Example Query |
| :---: | :---: | :---: | --- |
| region | string | Returns countries in the specified region. | api/countries/?region="europe" |
| minGDP | double | Returns countries with a higher GDP than the specified GDP | api/countries/?minGDP=10000 |
| maxGDP | double | Returns countries with a lower GDP than the specified date | api/countries/?maxGDP=20000 |
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

