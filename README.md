# Curso Acesso à dados com .NET. C#, Dapper e SQL Server

## Acesso a Dados de forma nativa ADO.NET

**Passos para acessar o banco com ADO.NET**

1. Cria um string de conexão 
2. Cria uma SqlConnection(), de preferência dentro de um using para não precisar fechar a conexão
3. Damos o comando para abrir a conexão
4. Criamos um objeto do tipo SQLComand, também de preferência usando o using.
5. Definimos a propriedade **Connection** do SQLComand com o valor da conexão que criamos.
6. Definimos a propriedade do SQLComand **CommandType**, exemplo se formos digitar um query, o tipo será **System.Data.CommandType.Text**
7. Definimos a propriedade do SQLComand **CommandText**, digitando a query que iremos usar
8. O metódo do SQLCommand **ExecuteReader**, para casos de query de leitura, retorna um SqlDataReader
9. O Objeto SqlDataReader é um cursor, só podemos avançar na leitura de suas posições, então usamos `while (reader.Read())` para interar sobre as posições
10. Para pegar o valor da coluna dessa posição, usamos metódos do tipo `reader.GetGuid(0)` ou `reader.GetString(1)` o parametro do metódo é a posição da coluna, e o método que chamamos depende do tipo da coluna.

>Quando chamamos o método `connection.close()` ele vai fechar a conexão caso esqueçamos de chama-lo o Garbarege colletor irá chama-ló.

>Quando chamamos o método `connection.Dispose()` ele não só encerra a conexão, mas destroí o objeto, caso precisamos de uma conexão teremos que instanciar o objeto novamente.

>De preferência quando puder fazer todas os comandos do banco de uma vez só, fechar e abrir o banco toda hora complomete a performance

## Utilizando Dapper

**Passos para acessar o banco com Dapper**

1. Asssim como o ADO.NET e necessário criar a string de conexão, e um SQLConnection
2. Na connection chamamos o método `var categories = connection.Query<Category>("Select [Id], [Title] FROM [Category]"); `, O método query é um método génerico, que recebe o tipo de objeto a ser retornado, o tipo do objeto Query é um **IEnumerable**, podemos percorre-lo como uma lista, uma lista do tipo informado. Sendo assim para acessar sua propriedade como de um objeto normalmente.

>A classe modelo que será o retorno precisa estar identica com nomes e tipo da propriedades que estão no banco, caso o nome da propriedade que está na classe for diferente, podemos usar na query comando **AS** para que retorno seja o mesmo nome da nossa propriedade

>Como o que o Dapper só faz esse mapeamento para o objeto, ele é considerado uma forma mais performática que outros ORM

>Evitar fazer processamentos dentro do using, para não ocupar a conexão com processamentos desnecessários, se o código poder ser criado fora do using é melhor

Usar **@** antes da query permite que façamos quebra de linha.

### Cuidados com SQLInjection

>SQLInjection é um ataque que permite criação e manipulação de dados que são enviadas diretamente para o banco. Para se previnir não devemos concatenar string na consultas, invés disso usarmos parâmetros.
