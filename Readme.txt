Script 
Update-Database -Script -SourceMigration $InitialDatabase

Copy the content to this file in the properly SQL file
Migration.sql --> when the migration is about this project

Update SeedsScripts.sql with the data required in the SQL server, the scrpt has to be writen to be executed 
more that once without issue, so it has to have  this format as example