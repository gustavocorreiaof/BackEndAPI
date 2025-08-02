FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app/SendEmailWorker

# Copia o código para dentro do container
COPY . /app

# Copia o script wait-for-it.sh
COPY wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

# Realiza a restauração e publicação
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Configuração do runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app/SendEmailWorker

# Copia o código compilado para o ambiente de runtime
COPY --from=build /app/publish .

# Garantir que o RabbitMQ esteja disponível antes de iniciar o Worker
ENTRYPOINT ["/wait-for-it.sh", "rabbitmq:5672", "--", "dotnet", "SendEmailWorker.dll"]
