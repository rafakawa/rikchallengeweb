# rikchallengeweb

Aplicativo Web para testar o desafio do moip.

## Estrutura

Este site foi feito de maneira bem simples, sem muita atenção aos detalhes e layout. Há muitas coisas que podem ser melhoradas, como por exemplo, validações dos campos, tamanhos das entradas, etc.

## Framework e Lingaguem

Escolhi fazer no Asp.net core 2.2 com C# por ter mais familiridade com este padrão para projetos Web. Como precisava fazer algo rápido, optei por esta tecnologia.

Hoje em dia, não é mais necessário ter ambiente Windows para rodar projetos em .net. Com o .net core, estes também rodam em Linux e Mac os.

## Arquitetura

Asp.net core 2.2 se baseia no MVVM (MOde-View-View-Model) que foi criado inicialmente para aplicações desktop com WCF. Ele ajuda a criar um modelo para a página e colocar suas ações nele.

As páginas são escritas com tags seguindo a biblioteca Razor Pages, que mistura Html com código.

## Pages

As páginas estão no folder Pages/Checkout.

O fluxo começa na página Identity, onde o usuário se identifica. 

Cada arquivo tem o html (cshtml) e sua lógica (cshtml.cs).

A integração final com a api de ProcessOrder está em Confirm.cshml.cs.

## Card

A validação do cartão de crédito está no backend, ou no serviço que processamento de cartão. Fiz assim, pois a lógica está em um só lugar, e a biblioteca da Wirecard era mais fácil de integrar com kotlin.
Para um projeto em produção, pensaria em escrever a mesma para .net e colocar direto nesta aplicação.

## Data

Objetos DTO, apenas para transporte de informações entre as páginas.

## Rodar

Para rodar o projeto, é preciso:

- Instalar o .net core 2.2:
Instalar nas diversas plataformas: https://dotnet.microsoft.com/download/dotnet-core/2.2

Baixe o projeto do Github.

No console, entre na pasta do projeto.

Para rodar:

```
dotnet run
```

O serviço estará disponível no:

https://localhost:5001

O endereço correto para acesso a página inicial do checkout:

https://localhost:5001/Checkout/Identity

Nota: O Id do cliente está fixo como "Client1", considerei que esta já era uma página do cliente e que seria necessário apenas as informações do comprador.

Caso aparece algum erro de certificado, aceite ir para a área insegura, que a página aparecerá normalmente.

A partir desta página é só seguir o fluxo. É necessário que o serviço da api já esteja de pé.

Ele está configurado hard coded para o endereço local: https://localhost:8181.