# API para DashMottu
Esse projeto é uma API Restful utilizando ASP.NET Core Controllers criada para a solução **Dashmottu: Mapeamento Inteligente do Pátio e Gestão das Motos**.

## 👩‍👦‍👦 Equipe
- Felipe Seiki Hashiguti - RM98985
- Lucas Corradini Silveira - RM555118
- Matheus Gregorio Mota - RM557254

## 🏛️ Justificativa da arquitetura

A aplicação foi desenvolvida seguindo a arquitetura **em camadas (Layered Architecture)**, separando responsabilidades em **Controllers**, **Application Services**, **Domain** e **Infraestrutura**.

- **Controllers**: Responsáveis por receber as requisições HTTP, tratar os parâmetros e devolver as respostas adequadas (com suporte a Swagger e exemplos de resposta).
- **Application Services**: Contêm a lógica de orquestração da aplicação, chamando os repositórios, validando regras e retornando DTOs apropriados.
- **Domain**: Define as entidades e regras de negócio, mantendo a lógica independente de frameworks.
- **Infraestrutura**: Camada dedicada a persistência de dados, repositórios e integração com banco de dados.


## ⚙ Configuração do Banco de Dados
No arquivo `appsettings.Development.json` em `appsettings.json`, configure os dados do banco Oracle, alterando o `HOST`, `User Id` e a `Password`:
```
"ConnectionStrings": {
  "Oracle": "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=host)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=userId};Password=senha;"
}
```

## ⚡ Executando o Projeto
### ✅ Pré-requisitos
- .NET SDK 8.0 ou superior instalado
- Oracle Database acessível
- Visual Studio 2022 ou VS Code com extensões C#

Verifique se a pasta `Migrations`  existe no seu diretorio, caso não tenha abra o Console Gerenciador de Pacotes e execute os seguintes comandos
```
// Gerar a migration caso necessário
Add-Migration intitdb 

// Atualizar o banco de dados
Update-Database
```

### 🖥️ Com Visual Studio
- Abra a solução dashmottu.API.sln
- Defina o projeto dashmottu.API como startup
- Execute com F5 ou Ctrl+F5

### 💻 Com terminal
```bash
cd dashmottu.API
dotnet build
dotnet run
```

## 📌 Exemplos de uso dos endpoints

### 🚀 **MotoController**

#### ➕ Adicionar uma moto

`POST /api/moto`
```
{
  "codTag": "sd12cas",
  "modelo": "MOTO_SPORT",
  "placa": "2314acs",
  "status": "ATIVA"
}
```

#### ✏️ Atualizar uma moto
`PUT /api/moto/1`
```
{
  "codTag": "sd12cas",
  "modelo": "MOTO_CUSTOM",
  "placa": "2314acs",
  "status": "INATIVA"
}
```

#### 🔎 Obter moto por ID
`GET /api/moto/1`

#### 📋 Obter todas as motos (paginado)
`GET /api/moto?Deslocamento=0&Limite=5`

#### ❌ Deletar uma moto
`DELETE /api/moto/1`

### 🚀 **PatioController**

#### 📋 Obter todos os pátios
`GET /api/patio?Deslocamento=0&Limite=3`

#### 🔎 Obter pátio por ID
`GET /api/patio/1`

#### ➕ Criar pátio
`POST /api/patio`
```
{
  "nome": "Pátio Central",
  "endereco": "Rua Principal, 123",
  "imagemPlanta": "planta.jpg"
}
```

#### ✏️ Atualizar pátio
`PUT /api/patio/1`
```
{
  "nome": "Pátio Atualizado",
  "endereco": "Rua Secundária, 456",
  "imagemPlanta": "planta2.jpg"
}
```

#### ❌ Deletar pátio
`DELETE /api/patio/1`

--- 

### 🚀 **AuthController**

#### 🔎 Obter login por ID do pátio
`GET /api/auth/1`

#### ➕ Criar login para um pátio
`POST /api/auth?idPatio=1`
```
{
  "username": "admin",
  "password": "123456"
}
```

#### 🔑 Validar login
`POST /login`
```
{
  "username": "admin",
  "password": "123456"
}
```

#### ✏️ Editar login
`PUT /api/auth?idPatio=1`
```
{
  "username": "admin",
  "password": "654321"
}
```

#### ❌ Deletar login
`DELETE /api/auth?idPatio=1` 

--- 
### 🚀 **HealthController**
  Endpoint criado para verificar os status da nossa api
#### 🔎 Liveness (o processo está vivo?)
`GET /api/Health/live` 

- Objetivo: detectar travamentos/loops ou deadlocks no processo da API.
- Ação típica do orquestrador: reiniciar o container quando unhealthy.
- Exemplos: responder “OK”/200, verificar thread pool/heap básico, “self check”.  
```
{
  "status": "Healthy",
  "checks": [
    {
      "name": "self",
      "status": "Healthy",
      "description": null,
      "error": null
    }
  ]
```

#### 🔎 Readiness (a instância está pronta para tráfego?)
`GET /api/Health/read` 

- Objetivo: garantir que a instância pode atender requisições de negócio.
- Ação típica do orquestrador/Load Balancer: anexar/desanexar do pool de tráfego conforme Healthy/Unhealthy.
- Exemplos: ping leve ao banco, teste de cache, verificação de filas/credenciais.
```
{
  "status": "Healthy",
  "checks": [
    {
      "name": "oracle_query",
      "status": "Healthy",
      "description": "Banco de dados esta online",
      "error": null
    }
  ]
}
```

---

### Teste Unitários
Para executar testes individuais ou todos de uma vez, basta clicar com o botão direito em um teste ou no menu Teste > Gerenciador de Testes (ou `Ctrl+E, T`). Irá abrir uma caixa com os teste, só clicar em executar.
<img width="1400" height="718" alt="image" src="https://github.com/user-attachments/assets/dc4f8954-6b10-4479-8e00-dd9f2fed1879" />


### 📚 Documentação Interativa
- Ao executar a API, acesse a documentação Swagger digitando `/swagger` para testar os endpoints diretamente pelo navegador.

## 🛠️ Tecnologias Utilizadas

- [ASP.NET](http://ASP.NET "smartCard-inline")  Core 8
- Entity Framework Core
- Swashbuckle (Swagger)
- Banco de dados Oracle
- Visual Studio 2022
