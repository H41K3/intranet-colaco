# Intranet Colaço

Portal interno da Colaço Soluções Contábeis. Reúne, por setor, os links dos sistemas e portais usados no dia a dia: sites, instaladores para baixar e portais das prefeituras.

O projeto é uma API em ASP.NET Core que também serve a própria página. O front-end é HTML, CSS e JavaScript puros, sem framework e sem etapa de build. **Não há banco de dados nem login**: a lista de links fica no código.

## Tecnologias

- .NET 10 (ASP.NET Core com controllers)
- HTML, CSS e JavaScript puro em `wwwroot/`

## Como rodar

Requisito: SDK do .NET 10.

```bash
dotnet run
```

A aplicação sobe em `http://localhost:5209` (a porta vem de `Properties/launchSettings.json`).

Toda mudança em arquivos `.cs` exige parar e rodar de novo. Mudanças em `wwwroot/` só precisam de recarregar a página (`Ctrl+F5`).

## Estrutura

```
Controllers/
  SetoresController.cs   Endpoint GET /api/Setores/{setorKey}
Models/
  SetoresCatalogo.cs     Setores e links (os dados ficam aqui)
Program.cs               Configuração: HTTPS, proxy, headers de segurança, arquivos estáticos
wwwroot/
  index.html             Página única (início + página de setor)
  style.css              Estilos
  app.js                 Navegação, chamada à API e renderização da lista
appsettings.json         Configurações (HTTPS e proxy reverso)
```

## Como funciona

1. O menu lateral tem um botão por setor, com o atributo `data-dep="chave-do-setor"`.
2. Ao clicar, o `app.js` chama `GET /api/Setores/{chave}`.
3. A API devolve o setor e seus links. O `app.js` desenha a lista.
4. O campo de busca, que só aparece dentro de um setor, filtra a lista na própria página.

Cada item da lista tem um tipo:

| `type` | Enum (C#)           | Comportamento                                                     |
| ------ | ------------------- | ----------------------------------------------------------------- |
| `0`    | `TipoLink.Link`     | Link para um site (abre em nova aba, mostra "Abrir")             |
| `1`    | `TipoLink.Download` | Link para baixar um instalador (mostra "Baixar")                 |
| `2`    | `TipoLink.Subtitulo`| Título que separa grupos (Web, Download, Prefeituras). **Não é clicável** |

> Os números do enum `TipoLink` (C#) e da constante `TIPO` (`app.js`) precisam ser sempre iguais.

## Manutenção do dia a dia

### Adicionar ou alterar links

Edite `Models/SetoresCatalogo.cs`. Use os helpers `Link`, `Download` e `Subtitulo`:

```csharp
["contabil"] = new("Setor Contábil",
[
    Subtitulo("Web"),
    Link("Nome do sistema", "https://exemplo.com.br/login"),

    Subtitulo("Download"),
    Download("Nome do instalador", "https://exemplo.com.br/instalador.exe")
]),
```

- A ordem da lista é a ordem exibida na tela.
- `Subtitulo` é opcional. Um setor só com links web pode listar apenas `Link(...)`.
- Reinicie a aplicação depois de alterar.

Os setores **Contábil** e **Processos** ainda não têm links cadastrados. Enquanto estiverem vazios, a tela mostra o aviso "Ainda não existem portais ou ferramentas vinculadas a este setor."

### Criar um novo setor

1. Em `SetoresCatalogo.cs`, adicione uma entrada no dicionário. A chave é o identificador do setor (minúsculas, sem acento).
2. Em `wwwroot/index.html`, adicione um botão no menu lateral com o mesmo valor em `data-dep`:

   ```html
   <button class="nav-btn" data-dep="chave-do-setor">Nome do Setor</button>
   ```

A chave do C# e o `data-dep` do HTML precisam ser idênticos. A busca da chave ignora maiúsculas e minúsculas.

### Ao alterar CSS ou JavaScript

O `index.html` carrega os arquivos com um número de versão (`style.css?v=15`, `app.js?v=15`). Aumente o número quando mudar o arquivo, para os navegadores não usarem a versão antiga em cache.

## API

`GET /api/Setores/{setorKey}`

Resposta `200`:

```json
{
  "nome": "Setor Financeiro",
  "links": [
    { "titulo": "Web", "url": null, "type": 2 },
    { "titulo": "Cloudfy", "url": "https://contadigital.cloudfy.net.br/login", "type": 0 },
    { "titulo": "Itaú", "url": "https://guardiao.itau.com.br/...", "type": 1 }
  ]
}
```

Resposta `404` quando o setor não existe:

```json
{ "mensagem": "Setor não encontrado." }
```

Chaves existentes: `contabil`, `financeiro`, `fiscal`, `processos`, `sistemas-gerais`.

Todas as respostas em `/api` saem com `Cache-Control: no-store`.

## Configuração

Em `appsettings.json`:

| Chave                          | Tipo           | Descrição                                                                 |
| ------------------------------ | -------------- | ------------------------------------------------------------------------- |
| `Security:RequireHttps`        | `bool`         | Força HTTPS e HSTS. Se ausente, fica ligado fora do ambiente Development |
| `ForwardedHeaders:Enabled`     | `bool`         | Liga a leitura de `X-Forwarded-For` e `X-Forwarded-Proto` (uso atrás de proxy reverso) |
| `ForwardedHeaders:KnownProxies`| lista de IPs   | IPs dos proxies confiáveis. Necessário quando `Enabled` é `true`         |

Exemplo atrás de um proxy reverso:

```json
"ForwardedHeaders": {
  "Enabled": true,
  "KnownProxies": ["10.0.0.5"]
}
```

## Publicação

```bash
dotnet publish -c Release -o ./publish
```

Ajuste `Security` e `ForwardedHeaders` conforme o ambiente onde a aplicação vai rodar.

## Cuidados

- O `Program.cs` define headers de segurança (`X-Frame-Options`, `X-Content-Type-Options`, `Referrer-Policy`, `Permissions-Policy`, `Cross-Origin-Opener-Policy`). Não remova sem saber o motivo.
- **Não coloque senhas, chaves ou strings de conexão em `appsettings*.json`**, porque o arquivo é versionado. Se o projeto voltar a precisar de banco ou JWT, use variáveis de ambiente ou `dotnet user-secrets`.
- As pastas `bin/` e `obj/` são geradas pela compilação e ficam fora do Git (ver `.gitignore`).
- Os textos e nomes do projeto estão em português. Mantenha o padrão.
