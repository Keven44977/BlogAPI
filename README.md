# BlogAPI
 blog API for Nwaretech test
 
 ## Setup DB
 Create a local postgreSQL database in your localhost environnement and migrate the database using the entity framwork command "update-database"
 https://entityframeworkcore.com/migrations

### If needed update connexion string
if your local db uses another connexion string than the one in the Database.DatabaseConnexion.cs file, change it there. The default connexion string is "Host=localhost;Database=blog;Username=postgres;password=admin"
