// 1 - Bibliotecas
using RestSharp;
// 2 - NameSpace
public class Pet;
// 3 - Classe
public class PetTest
{
    // 3.1 - Atributos
    // Endereço da API
    private const string BASE_URL = "https://petstore.swagger.io/v2/";

    // 3.2 - Funções e Métodos
    [Test, Order(1)]
    public void PostPetTest()
    {
        // Configura

        // Instancia o objeto do tipo RestClient com o endereço da API
        var client = new RestClient(BASE_URL);
        // Instancia o objeto do tipo RestRequest com o complemento de endereço
        // como "pet" e configurando o método para ser um post (inclusão)
        var request = new RestRequest("pet", Method.Post);

        // Armazena o conteudo do arquivo pet.json na memoria
        string jsonBody = File.ReadAllText(@"D:\ESTUDO\ITERASYS\FTS139\petstore139\fixtures\pet1.json")

        // Executa
        

        // Valida
    }
}    