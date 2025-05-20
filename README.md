# API para DashMottu
Esse projeto é uma API Restful utilizando ASP.NET Core Controllers criada para a solução **Dashmottu: Mapeamento Inteligente do Pátio e Gestão das Motos**.

## 👩‍👦‍👦 Equipe
- Felipe Seiki Hashiguti - RM98985
- Lucas Corradini Silveira - RM555118
- Matheus Gregorio Mota - RM557254

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
A API será iniciada em: https://localhost:7046 ou http://localhost:5046

### 📡 Rotas
- `GET     /api/patio`             - Retorna uma lista com todos os registros de pátios cadastrados no sistema.  
- `GET     /api/patio/{id}`        - Retorna os dados de um pátio específico, com base no ID fornecido.  
- `POST    /api/patio`             - Cadastra um novo pátio com endereço, imagem da planta e informações de login.  
- `POST    /api/patio/login`       - Realiza o login de um pátio com base nas credenciais fornecidas.  
- `PUT     /api/patio/{id}`        - Atualiza os dados de um pátio existente com base no ID fornecido.  
- `DELETE  /api/patio/{id}`        - Remove um pátio do sistema com base no ID fornecido.  

### 📚 Documentação Interativa
- Ao executar a API, acesse a documentação Swagger digitando `/swagger` para testar os endpoints diretamente pelo navegador.

## 🛠️ Tecnologias Utilizadas

- [ASP.NET](http://ASP.NET "smartCard-inline")  Core 8
- Entity Framework Core
- Swashbuckle (Swagger)
- Banco de dados Oracle
- Visual Studio 2022
