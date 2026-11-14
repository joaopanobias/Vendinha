# Vendinha

Sistema em C# desenvolvido para gerenciar o cadastro de clientes e o controle de contas e dívidas em aberto de uma mercearia, contando com validação completa, paginação e sistema de lixeira.

---

## Tecnologias Utilizadas

* **Linguagem:** C#
* **Framework:** .NET 10.0 (`net10.0-windows`)
* **Interface Gráfica:** Windows Forms (WinForms)
* **Acesso a Dados:** ADO.NET (`Microsoft.Data.SqlClient`)
* **Banco de Dados:** SQL Server LocalDB (`(localdb)\mssqllocaldb`)

---

## Como Executar o Projeto

1. Certifique-se de ter o **Visual Studio** (com a carga de trabalho *Desenvolvimento para desktop com .NET*) e o **SQL Server LocalDB** instalados.
2. Abra o arquivo de solução `Vendinha.slnx` no Visual Studio.
3. Pressione **F5** para compilar e executar.
4. Na primeira execução, o sistema criará automaticamente o banco de dados `VendinhaDb`, as tabelas e uma massa inicial de clientes de exemplo.
5. O script SQL estrutural do banco também encontra-se documentado no arquivo `database.sql`.

---

## Regras de Negócio

* **Campos Obrigatórios:** Nome Completo, CPF e Data de Nascimento são indispensáveis no cadastro e na edição.
* **Validação de CPF:** O CPF deve ser matematicamente válido e único no sistema.
* **Restrição de Idade:** O cliente deve ter no mínimo **18 anos** e no máximo **120 anos** (datas futuras ou anos inferiores a 1900 são bloqueados).
* **Campos Opcionais:** E-mail (caso preenchido, exige formato válido) e Dívida Inicial.
* **Controle de Dívidas:** Um cliente só pode ter uma dívida em aberto por vez. Permite lançamentos, pagamentos parciais (abatimento) ou quitação total.
* **Lixeira:** Ao excluir um cliente, ele é apenas inativado (`Ativo = 0`), preservando todo o seu histórico de compras e dívidas, podendo ser restaurado a qualquer momento pela tela de Lixeira.

---

## Exemplos para Teste de Cadastro

Utilize os dados abaixo para testar o cadastro de novos clientes no sistema (todos possuem CPFs válidos e idade dentro da regra de 18 a 120 anos):

### Exemplo 1 (Cadastro Completo)
* **Nome Completo:** Carlos Eduardo Mendes
* **CPF:** `39053344705`
* **Data de Nascimento:** `15/08/1990`
* **E-mail *(Opcional)*:** `carlos.mendes@email.com`
* **Dívida Inicial *(Opcional)*:** `85,50`

### Exemplo 2 (Apenas Campos Obrigatórios)
* **Nome Completo:** Fernanda Lima Souza
* **CPF:** `52998224725`
* **Data de Nascimento:** `22/03/2001`
* **E-mail *(Opcional)*:** *(Deixar em branco)*
* **Dívida Inicial *(Opcional)*:** *(Deixar em branco)*

### Exemplo 3 (Com Dívida Inicial e sem E-mail)
* **Nome Completo:** Roberto Alves Pereira
* **CPF:** `71428793860`
* **Data de Nascimento:** `10/11/1978`
* **E-mail *(Opcional)*:** *(Deixar em branco)*
* **Dívida Inicial *(Opcional)*:** `140.00`

> **Precisa gerar mais CPFs válidos para testes?** Utilize o https://www.geradordecpf.org/.

---

## Estrutura de Pastas

```text
Vendinha/
├── Data/           # Repositório com comandos SQL e transações (ClienteRepository.cs)
├── Models/         # Entidades e cálculos de domínio (Cliente.cs, Divida.cs)
├── Services/       # Regras de negócio e validações (ClienteService.cs)
├── Screens/        # Telas Windows Forms (Listagem, Cadastro, Edição, Dívidas e Lixeira)
├── database.sql    # Script SQL de criação do banco e tabelas
└── README.md       # Documentação do projeto
