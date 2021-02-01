FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu

RUN mkdir -p /opt/scriptsDB
COPY createDB.sql /opt/scriptsql/

ENV ACCEPT_EULA Y
ENV SA_PASSWORD 7@RBhrpZNI
ENV MSSQL_PID Express
RUN /opt/mssql/bin/sqlservr & sleep 60; /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P '7@RBhrpZNI' -d master -i /opt/scriptsql/createDB.sql