# CaBackendTest – Billing API

## Descrição

Esta API foi desenvolvida como parte de um teste para vaga de desenvolvedor. O sistema realiza o gerenciamento de produtos e clientes (CRUD completo) e permite integrar, importar e validar faturas (`billings`) de uma API externa, armazenando-as localmente no banco de dados SQL Server.

A aplicação foi criada usando a estrutura **Clean Architecture** para garantir separação de responsabilidades, testabilidade e facilidade de manutenção.

---

## Funcionalidades

- **CRUD de Produtos**
  - Permite criar, consultar, editar e deletar produtos.
- **CRUD de Clientes**
  - Permite criar, consultar, editar e deletar clientes.
- **Endpoint para listar Billings externas**
  - `/api/Billing` (GET): Lista todas as billings disponíveis na API externa.
- **Endpoint para importar Billings**
  - `/api/Billing/ImportsBillingExternalAPI` (POST): Importa todas as billings externas para o banco local. Valida se os clientes e produtos referenciados existem no banco local:
    - Se **não existirem**, retorna erro de validação.
    - Se **existirem**, cria o registro de billing e suas respectivas linhas (BillingLine) no banco de dados.

- **Seed automático**
  - No primeiro uso, cria 1 cliente e 2 produtos no banco local, com base no primeiro registro retornado da API externa (`https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing`).

---

## Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server 2022 (Docker)
- Docker/Docker Compose
- Entity Framework Core
- Swagger (OpenAPI)
- Clean Architecture
- **Repository Pattern:** Padrão de projeto para abstração do acesso a dados, facilitando manutenção, testes e alinhado com princípios de arquitetura como DDD.

---

## Estrutura do Projeto (Clean Architecture)

O projeto é organizado seguindo os princípios da Clean Architecture:

- **CaBackendTest.Api** – Camada de apresentação e endpoints da API.
- **CaBackendTest.Application** – Serviços de aplicação, DTOs, regras de negócio e interfaces.
- **CaBackendTest.Domain** – Entidades de domínio, interfaces de repositório e contratos de serviço.
- **CaBackendTest.Infrastructure** – Infraestrutura para banco de dados (Contexto, Migrations, Repositórios), serviços externos e persistência.

Essa separação facilita manutenção, testes e evolução da aplicação, promovendo baixo acoplamento entre as camadas.

---

## Como Executar o Projeto

### 1. Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) instalado na máquina.
- [Docker](https://www.docker.com/get-started) instalado e rodando.
  - Caso não tenha Docker:
    - **Windows/Mac:** Acesse https://www.docker.com/products/docker-desktop
    - **Linux:** Siga as instruções oficiais para sua distribuição (ex: Ubuntu: `sudo apt-get install docker.io`)

### 2. Subindo o Banco de Dados com Docker

- Abra um terminal na pasta raiz do projeto onde está o arquivo `docker-compose.yml`.
- Execute o comando:
  *docker-compose up -d*
  

Isso irá baixar a imagem oficial do SQL Server, criar e rodar um container chamado `sqlserver_local` com a porta 1433 exposta.

### 3. Configurando a Connection String

- No arquivo `appsettings.json`, utilize a seguinte connection string (ou ajuste conforme necessário):

`"Server=localhost,1433;Database=ca-backend-test;User Id=sa;Password=Password123!;TrustServerCertificate=True;"`


### 4. Executando o Projeto

- No terminal, execute:
  *dotnet run --project CaBackendTest.Api*

- Ou, se preferir, você pode abrir o projeto no **Visual Studio 2022** e executar diretamente pela interface gráfica clicando no botão de **Play** (Iniciar Depuração) na barra superior.

- Na primeira inicialização:
  - O banco de dados será criado automaticamente (via migrations EF Core).
  - Será criado 1 registro de cliente e 2 de produtos com base no primeiro billing externo.

### 5. Acessando a API

- Acesse a documentação Swagger em: 
  - [http://localhost:7102/swagger/index.html](http://localhost:7102/swagger/index.html) (ajuste a porta se necessário)
- Teste os endpoints de Products, Customers e Billings diretamente pela interface Swagger.

---

## Observações Importantes

- **Validação na importação de billings:** O sistema valida se clientes e produtos existem antes de importar uma billing. Caso algum não exista, retorna erro de validação detalhado.
- **Seed automático:** No primeiro uso, 1 cliente e 2 produtos serão criados automaticamente, correspondendo ao primeiro registro da API externa.
- **Banco criado automaticamente:** Não é necessário criar manualmente o banco. Basta rodar a aplicação após o docker-compose.

---

## Contato

- Desenvolvedor: Anderson
- [Send email](mailto:ander.lemos.jr@email.com) 
