
# Projeto Vendor Service - BNE

Microserviço baseado em um ambiente de criação de pedidos de venda utilizando
a abordagem do DDD para modelagem do software (Domain Driven Design).

Comando para execução do backend utilizando Docker:
$ docker-compose up -d

Após execução, acessar a api através do endereço localhost:8080 no navegador.

A autenticação é feita através de uma conta Admin profile:
Login: gustavokovalski.saporiti@gmail.com
Senha: adminvendor

Demais acessos:
SQL Server: localhost:1433
Login: SA
Senha: 7@RBhrpZNI 

Kafka: kafka:9092

Projeto foi criado utilizando ASP.NET Core 3.1, SQL Server, Dapper, Swagger, Kafka, Zookeeper, Docker, JWT.
