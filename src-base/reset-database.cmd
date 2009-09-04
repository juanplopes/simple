osql -E -S .\sqlexpress -d master -i sql/DropDatabases.sql > log.txt
osql -E -S .\sqlexpress -d master -i sql/CreateDatabases.sql >> log.txt
