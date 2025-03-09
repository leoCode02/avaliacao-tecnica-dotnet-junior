# File2SQLite - Monitoramento de Arquivo para SQLite

Aplicação em **.NET 8** que monitora um arquivo de texto (`.txt`) e insere novas linhas em um banco de dados **SQLite**, evitando duplicações.

---

## 📋 Pré-requisitos

Antes de executar o projeto, certifique-se de ter os seguintes requisitos:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Um arquivo de texto (`.txt`) para monitorar.
- [Visual Studio](https://visualstudio.microsoft.com/) (opcional, mas recomendado para abrir o arquivo de solução)

---

## 🚀 Como Executar

### Abrindo o Projeto

Você pode abrir o projeto de duas maneiras:

1. **Via Visual Studio**  
   Abra o arquivo de solução (`.sln`) que foi incluído no repositório para carregar todos os projetos e configurações de forma organizada.

2. **Via Linha de Comando**  
   Se preferir trabalhar via terminal, siga os passos abaixo:

   1. **Clone o repositório**:
      ```bash
      git clone https://github.com/leoCode02/avaliacao-tecnica-dotnet-junior.git
      cd avaliacao-tecnica-dotnet-junior
      ```

   2. **Restaure as dependências**:
      ```bash
      dotnet restore
      ```

   3. **Execute o projeto**:
      ```bash
      dotnet run
      ```

   4. **Siga as instruções no console**:
      - Quando solicitado, insira o caminho completo do arquivo de texto que deseja monitorar (exemplo: `C:\pasta\arquivo.txt`).
      - O sistema iniciará o monitoramento automático a cada **30 segundos**.

---

## 🛠️ Controles

- **ESC** → Encerra o programa.  
- **R** → Força uma verificação manual de novas linhas.

---

## 🗃️ Estrutura do Banco de Dados

O banco de dados `dados.db` é criado **automaticamente** na primeira execução.

**Tabela: `FileLines`**
| Campo      | Tipo       | Descrição                      |
|------------|------------|--------------------------------|
| `Id`       | INTEGER    | Chave primária, autoincremento |
| `Content`  | TEXT       | Conteúdo único da linha        |
| `CreatedAt`| DATETIME   | Data/hora de criação           |


---

## 📑 Documentação de Testes

📄 [Documento de Evidências de Testes - File2SQLite.pdf](Documento%20de%20Evid%C3%AAncias%20de%20Testes%20-%20File2SQLite.pdf) - Documento com os testes realizados.
