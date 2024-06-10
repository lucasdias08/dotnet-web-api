# dotnet-web-api

Sistema desenvolvido como teste de conhecimento, como trabalho da disciplina Sistemas distribuídos e Mobile, do curso de Análise e Desenvolvimento de Sistema da UNP, semestre 2024.1. Esse projeto consiste em criar um sistema distribuído com a tecnologia java ou c#(que é o caso desse projeto), onde deveria ser realizado CRUD com dados e persistir e manipular esses dados em um banco de dados SQL ou NoSql. O banco de dados escolhido para o projeto foi o `Realtime Database` do `Firebase`.

- O sistema do projeto em questão se baseia em um front-end desktop criado com `C#`, `dotnet` e `WinForms`, um back-end do tipo API REST desenvolvido com `C#`, `dotnet` & Banco de dados `Realtime Database` do `Firebase`;

- Back-end se encontra em que integra com essa API: 
>https://github.com/lucasdias08/front-venx-challenge

- Seguir o passo a passo para clonar e executar, caso queira.

## Preparando ambiente

- __1.__ Clone esse repositório e o abra com o Visual Studio, de preferência;

- __2.__ Execute, caso queira, via Visual Studo da opção `executar`;

- __3.__ Execute, caso queira, via terminal `dotnet run`;

  
# Manipulando a API

### Rotas

Para trabalhar com as rotas via algum clientHttp, como o Insomnia ou o Postman, é necessário apenas apontar para a url, apenas. Não autenticação nem autorização;

### Tasks (tarefas)

Todos os end-point manipulam tarefas, com campos `id`, `title` e `description`. Todas essas rotas possuem retorno (se sucesso, se falha);

- __Rota GET__. Rota padrão para saber se o servidor está rodando, a priori, sem mairoes problemas;
>http://localhost:5100/testingServer

- __Rota GET__. Espera-se que retorne todos as tarefas e seus dados cadastrados, no formato JSON;
>http://localhost:5100/api/tasks

- __Rota GET__, __parametrizada__. Espera-se que retorne a tarefa especificada pelo id na forma de parametro, no formato JSON;
>http://localhost:5100/api/tasks/{id_task}

- __Rota PUT__. Espera-se que seja passado o ID de identificação do registro e os dados cujo JSON deve ser algo próximo a:

{
    "title": "titulo atualizado",
    "description": "descrição atualizada"
}

. Se tudo der certo, será retornado o JSON dos dados __atualizados__ no cadastrado e o Status Code competente;
>http://localhost:5100/api/tasks/{id_task}

- __Rota DELETE__. Espera-se que seja passado o ID de identificação do registro. Você pode conferir se realmente foi excluído o registro utilizando a rota GET acima;
>http://localhost:5100/api/tasks/{id_task}
