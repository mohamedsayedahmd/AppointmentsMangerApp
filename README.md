# AppointmentsMangerApp

---

### Create a Data Folder that has two folders:

- Migrations
- Models

### Create a file inside the Data Folder called:

- AppDbContext

---

- Create Appointement Model

---

### Before we do the <u>AppDbContext</u> we should install some libraries for Entity framework

- entityframeworkcore (6.0.21)
- microsoft.entityframeworkcore.design (6.0.21)
- microsoft.entityframeworkcore.sqlserver (6.0.21)
- microsoft.entityframeworkcore.tools (6.0.21)

---

### Now we can set AppDbContext

```C#
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        // Here we define db table DbSet<Model>
        public DbSet<Appointement> Appointements { get; set; }
    }
```

---

### Now we need to define the _Connection String_ At

` appsetting.JSON`

```JSON
{
  "Logging": {
      "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
      }
    },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-RI8LI08\\SQLEXPRESS;User Id=sa;Password=admin;DataBase=WebApiReact;TrustServerCertificate=True",
  }
}
```

`Program.cs`

```C#
builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
```

## Implement the <u>migration</u>

### Tools → NuGet Package Manager → Package Manager Console

```shell
Add-migration -Name Initial -OutputDir "Data/Migrations"
```

```
update-database
```
