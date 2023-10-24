// 1 - Bibliotecas
using RestSharp;
using Newtonsoft.Json;
using Models; // Dependencia para o JsonConvert
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
        String jsonBody = File.ReadAllText(@"D:\ESTUDO\ITERASYS\FTS139\petstore139\fixtures\pet1.json");
        // Adiciona na requisição o conteudo do arquivo pet1.json
        request.AddBody(jsonBody);
        // Executa
        // Executa a requisição conforme a configuração realizada
        // Guarda o json retornado no objeto response
        var response = client.Execute(request);

        // Valida
        var responseBody = JsonConvert.DeserializeObject<dynamic>(response.Content);

        // Exibe o responseBody no console
        Console.WriteLine(responseBody);

        // Valide que na resposta o status code é igual ao resultado esperado (200)
        Assert.That((int)response.StatusCode, Is.EqualTo(200));

        // Valida o petId
        int petId = responseBody.id;
        Assert.That(petId, Is.EqualTo(3048454));


        // Valida o nome do animal na resposta
        String name = responseBody.name.ToString();
        Assert.That(name, Is.EqualTo("Lagertha"));
        // OU
        // Assert.That(responseBody.name.ToString(), Is.EqualTo("Lagertha"));

        
        // Valida o status do animal na resposta
        string status = responseBody.status;
        Assert.That(status, Is.EqualTo("available")); 

        // Armazenar os dados obtidos para usar nos proximos testes
        Environment.SetEnvironmentVariable("petId", petId.ToString());

    }

    [Test, Order(2)]
    public void GetPetTest()
    {
        // Configura
        int petId = 3048454;            // Campo de pesquisa
        String petName = "Lagertha";    // Resultado esperado
        String categoryName = "cat";
        String tagsName = "vacinado";
        var client = new RestClient(BASE_URL);
        var request = new RestRequest($"pet/{petId}", Method.Get);
        //var request = RestRequest("pet/" + petId, Method.Get); // Outra forma de fazer com concatenação

        // Executa
        var response = client.Execute(request);

        // Valida
        var responseBody = JsonConvert.DeserializeObject<dynamic>(response.Content);
        Console.WriteLine(responseBody);

        Assert.That((int)response.StatusCode, Is.EqualTo(200));
        Assert.That((int)responseBody.id, Is.EqualTo(petId));
        Assert.That((String)responseBody.name, Is.EqualTo(petName));
        Assert.That((String)responseBody.category.name, Is.EqualTo(categoryName));
        Assert.That((String)responseBody.tags[0].name, Is.EqualTo(tagsName));
    }

    [Test, Order(3)]
    public void PutPetTest()
    {
        // Configura
        // Os dados de entrada vão formar o body da alteração
        // Vamos usar uma classe de modelo
        PetModel petModel = new PetModel();
        petModel.id = 3048454;
        petModel.category = new Category(1, "cat");
        petModel.name = "Lagertha";
        petModel.photoUrls = new String[]{""};
        petModel.tags = new Tag[]{new Tag(1, "vacinado"),
                                  new Tag(2, "castrado")};
        petModel.status = "pending";                          

        // Transformar o modelo acima em arquivo json
        String jsonBody = JsonConvert.SerializeObject(petModel, Formatting.Indented);
        Console.WriteLine(jsonBody);

        var client = new RestClient(BASE_URL);
        var request = new RestRequest("pet", Method.Put);
        request.AddBody(jsonBody);

        // Executa
        var response = client.Execute(request);

        // Valida
        var responseBody = JsonConvert.DeserializeObject<dynamic>(response.Content);
        Console.WriteLine(responseBody);

        Assert.That((int)response.StatusCode, Is.EqualTo(200));
        Assert.That((int)responseBody.id, Is.EqualTo(petModel.id));
        Assert.That((String)responseBody.tags[1].name, Is.EqualTo(petModel.tags[1].name));
        Assert.That((String)responseBody.status, Is.EqualTo(petModel.status));

    }
    [Test, Order(4)]
    public void DeletePetTest()
    {
        // Configura
        String petId = Environment.GetEnvironmentVariable("petId");
        var client = new RestClient(BASE_URL);
        var request = new RestRequest($"pet/{petId}", Method.Delete);
        
        // Executa
        var response = client.Execute(request);
        
        // Valida
        var responseBody = JsonConvert.DeserializeObject<dynamic>(response.Content);


        Assert.That((int)response.StatusCode, Is.EqualTo(200));
        Assert.That((int)responseBody.code, Is.EqualTo(200));
        Assert.That((String)responseBody.message, Is.EqualTo(petId));
        

    }    

}    