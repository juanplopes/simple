osql -E -S .\sqlexpress -d master -i sql/DropDatabases.sql
osql -E -S .\sqlexpress -d master -i sql/CreateDatabases.sql
call build "db:migrate;db:sampledata"