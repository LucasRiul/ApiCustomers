# ApiCustomers

API desenvolvida em .NET para cadastro e consulta de clientes, com integração ao serviço [ViaCEP](https://viacep.com.br) para obtenção automática de endereço a partir do CEP informado.

---

## 🚀 Tecnologias Utilizadas

- **.NET 6 / 7 / 8** (ASP.NET Core Web API)
- **Entity Framework Core** (opcional, se for usar banco)
- **Swagger (Swashbuckle.AspNetCore)** — documentação da API
- **HttpClient** — integração com a API ViaCEP
- **Middleware global de tratamento de erros**
- **Dependency Injection (DI)**
- **LaunchSettings configurado para HTTPS e Swagger**

---

## 📦 Funcionalidades Principais

- Consulta de CEP via API pública do **ViaCEP**
- Retorno de dados estruturados de endereço
- Tratamento global de exceções com middleware customizado
- Documentação automática com **Swagger**
- Estrutura organizada seguindo boas práticas de arquitetura

---
## 🧰 Configuração do Ambiente

### 1️⃣ Pré-requisitos

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- (Opcional) [Visual Studio](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

---

### 2️⃣ Restaurar dependências

```bash
dotnet restore

### 3 Rodar Projeto
A API iniciará em:
http://localhost:5235
A documentação Swagger estará disponível em:
http://localhost:5235/swagger

🧑‍💻 Autor

Lucas Riul Martins
Desenvolvedor .NET | Angular | Flutter
📧 [lucasriul85@gmail.com]
