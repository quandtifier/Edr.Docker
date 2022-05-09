# Wait for SQL server to be started then run the sql script
./wait-for-it.sh sql_in_dc:1433 --timeout=0 --strict -- sleep 5s && \
/opt/mssql-tools/bin/sqlcmd -S localhost  -U sa -P "$SA_PASSWORD"